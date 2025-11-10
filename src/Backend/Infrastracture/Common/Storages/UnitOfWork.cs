using Application.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Common.Storages;

public class UnitOfWork<TContext>(IServiceScopeFactory scopeFactory) : IUnitOfWorkWithContext<TContext>
    where TContext : DbContext
{
    public async Task<IUnitOfWorkTransaction> StartScopeAsync(CancellationToken cancellationToken)
    {
        var scope = scopeFactory.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();
        var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        return new UnitOfWorkTransaction(scope, transaction);
    }

    async Task<IUnitOfWorkTransaction> IUnitOfWork.StartScope(CancellationToken cancellationToken)
        => await StartScopeAsync(cancellationToken);

    internal sealed class UnitOfWorkTransaction(
        IServiceScope scope,
        IDbContextTransaction transaction) : IUnitOfWorkTransaction
    {
        public async Task Commit(CancellationToken cancellationToken)
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();
            await dbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }

        public TStorage GetStorage<TStorage>() where TStorage : IStorage
        {
            return scope.ServiceProvider.GetRequiredService<TStorage>();
        }

        public async ValueTask DisposeAsync()
        {
            if (scope is IAsyncDisposable scopeAsyncDisposable)
            {
                await scopeAsyncDisposable.DisposeAsync();
            }
            else
            {
                scope.Dispose();
            }

            await transaction.DisposeAsync();
        }
    }
}

public interface IUnitOfWorkWithContext<TContext> : IUnitOfWork
    where TContext : DbContext
{
    Task<IUnitOfWorkTransaction> StartScopeAsync(CancellationToken cancellationToken);
}

public class UnifOfWorkBase1(IServiceScopeFactory scopeFactory)
    : UnitOfWork<ContextBase1>(scopeFactory), IUnitOfWorkBase1
{
}

public class UnifOfWorkBase2(IServiceScopeFactory scopeFactory)
    : UnitOfWork<ContextBase2>(scopeFactory), IUnitOfWorkBase2
{
}