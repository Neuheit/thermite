using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Thermite.Core;

namespace Thermite.Utilities
{
    internal static class ThrowHelpers
    {
        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowInvalidOperationException(string message)
        {
            throw new InvalidOperationException(message);
        }

        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowArgumentOutOfRangeException(string paramName)
        {
            throw new ArgumentOutOfRangeException(paramName);
        }

        [DoesNotReturn]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void ThrowInvalidUriException(string paramName,
            Uri location)
        {
            throw new InvalidUriException(paramName, location);
        }
    }
}