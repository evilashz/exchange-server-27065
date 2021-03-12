using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x0200008F RID: 143
	internal static class Util
	{
		// Token: 0x060003C7 RID: 967 RVA: 0x0000C540 File Offset: 0x0000A740
		internal static void ThrowOnNullArgument(object argument, string argumentName)
		{
			if (argument == null)
			{
				throw new ArgumentNullException(argumentName);
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000C54C File Offset: 0x0000A74C
		internal static void ThrowOnConditionFailed(bool condition, string message)
		{
			if (!condition)
			{
				throw new InvalidOperationException(message);
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000C558 File Offset: 0x0000A758
		internal static void ThrowOnNullOrEmptyArgument(string argument, string argumentName)
		{
			if (argument == null)
			{
				throw new ArgumentNullException(argumentName);
			}
			if (argument.Length == 0)
			{
				throw new ArgumentException(argumentName);
			}
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000C573 File Offset: 0x0000A773
		internal static void ThrowOnNullOrEmptyArgument(ICollection argument, string argumentName)
		{
			if (argument == null)
			{
				throw new ArgumentNullException(argumentName);
			}
			if (argument.Count == 0)
			{
				throw new ArgumentException("Argument should not be empty.", argumentName);
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000C593 File Offset: 0x0000A793
		internal static void ThrowOnNullOrEmptyArgument<TElement>(ICollection<TElement> argument, string argumentName)
		{
			if (argument == null)
			{
				throw new ArgumentNullException(argumentName);
			}
			if (argument.Count == 0)
			{
				throw new ArgumentException("Argument should not be empty.", argumentName);
			}
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000C5B4 File Offset: 0x0000A7B4
		internal static void ThrowOnMismatchType<T>(object o, string argumentName)
		{
			Type typeFromHandle = typeof(T);
			Type type = o.GetType();
			if (type != typeFromHandle && !typeFromHandle.IsAssignableFrom(type))
			{
				throw new ArgumentException(string.Format("Type mismatch for object: {0}, expected: {1}, actual: {2}", argumentName, typeof(T), o.GetType()));
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000C608 File Offset: 0x0000A808
		internal static string StringizeException(Exception exception)
		{
			StringBuilder stringBuilder = new StringBuilder();
			Util.StringizeException(stringBuilder, exception, 0U);
			return stringBuilder.ToString();
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000C629 File Offset: 0x0000A829
		internal static DateTime NormalizeDateTime(DateTime datetime)
		{
			return new DateTime(datetime.Year, datetime.Month, datetime.Day, datetime.Hour, datetime.Minute, datetime.Second, 0, datetime.Kind);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000C662 File Offset: 0x0000A862
		internal static DateTime NormalizeDateTimeToMinutes(DateTime datetime)
		{
			return new DateTime(datetime.Year, datetime.Month, datetime.Day, datetime.Hour, datetime.Minute, 0, 0, datetime.Kind);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000C695 File Offset: 0x0000A895
		internal static bool ShouldRethrowException(Exception ex)
		{
			return ex is OutOfMemoryException || ex is StackOverflowException || ex is ThreadAbortException;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000C6B4 File Offset: 0x0000A8B4
		internal static bool TryGetExceptionOrInnerOfType<TException>(Exception exception, out Exception matchedException) where TException : Exception
		{
			matchedException = null;
			for (Exception ex = exception; ex != null; ex = ex.InnerException)
			{
				if (ex is TException)
				{
					matchedException = ex;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000C6E0 File Offset: 0x0000A8E0
		private static void StringizeException(StringBuilder builder, Exception exception, uint depth)
		{
			if (depth > 10U)
			{
				builder.AppendLine("-----------------------------------------------------------");
				builder.AppendLine("  There's at least one more inner exception (not shown)");
				builder.AppendLine("-----------------------------------------------------------");
				return;
			}
			builder.AppendLine("-----------------------------------------------------------");
			builder.AppendLine(string.Format("  {0} exception: {1}", (depth == 0U) ? "Main" : "Inner", exception.GetType().ToString()));
			builder.AppendLine("-----------------------------------------------------------");
			builder.AppendLine(exception.Message);
			builder.AppendLine(exception.StackTrace);
			if (exception.InnerException != null)
			{
				Util.StringizeException(builder, exception.InnerException, depth + 1U);
			}
		}

		// Token: 0x040001A8 RID: 424
		private const int StringizedExceptionMaxDepth = 10;
	}
}
