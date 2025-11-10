using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Validation
{
    internal static class ValidationErrorCode
    {
        public const string Empty = nameof(Empty);
        public const string TooLong = nameof(TooLong);
        public const string TooSmall = nameof(TooSmall);
        public const string Invalid = nameof(Invalid);
        public const string InvalidImageType = nameof(InvalidImageType);
        public const string InvalidExtensionType = nameof(InvalidExtensionType);
    }
}
