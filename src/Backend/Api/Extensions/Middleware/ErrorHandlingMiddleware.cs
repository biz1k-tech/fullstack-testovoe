using Domain.Exceptions.Base;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Extensions.Middleware;

public class ErrorHandlingMiddleware(
    RequestDelegate next)
{
    public async Task InvokeAsync(
        HttpContext httpContext,
        ProblemDetailsFactory problemDetailsFactory)
    {
        try
        {
            await next.Invoke(httpContext);
        }
        catch (Exception exception)
        {
            ProblemDetails problemDetails;
            switch (exception)
            {
                case ValidationException validationException:
                    problemDetails = problemDetailsFactory.CreateFrom(httpContext, validationException);
                    break;

                case DomainException domainException:
                    problemDetails = problemDetailsFactory.CreateFrom(httpContext, domainException);
                    break;

                default:
                    problemDetails = problemDetailsFactory.CreateProblemDetails(
                        httpContext, StatusCodes.Status500InternalServerError,
                        $"Unhandled error! Erorr: {exception.Message}/ {exception.InnerException}");
                    break;
            }

            httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, problemDetails.GetType());
        }
    }
}

public static class ProblemDetailsFactoryExtensions
{
    public static ProblemDetails CreateFrom(this ProblemDetailsFactory factory, HttpContext httpContext,
        DomainException domainException)
    {
        return factory.CreateProblemDetails(httpContext,
            domainException.DomainErrorCode switch
            {
                DomainErrorCode.NotFound => StatusCodes.Status404NotFound,
                DomainErrorCode.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            },
            domainException.Message);
    }

    public static ProblemDetails CreateFrom(this ProblemDetailsFactory factory, HttpContext httpContext,
        ValidationException validationException)
    {
        var modelStateDictionary = new ModelStateDictionary();
        foreach (var error in validationException.Errors)
            modelStateDictionary.AddModelError(error.PropertyName, error.ErrorCode);

        return factory.CreateValidationProblemDetails(httpContext,
            modelStateDictionary,
            StatusCodes.Status400BadRequest,
            "Validation failed");
    }
}