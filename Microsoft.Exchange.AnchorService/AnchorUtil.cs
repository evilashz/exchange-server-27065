using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x02000019 RID: 25
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class AnchorUtil
	{
		// Token: 0x0600011C RID: 284 RVA: 0x00004C78 File Offset: 0x00002E78
		public static void AssertOrThrow(bool condition, string formatString, params object[] parameters)
		{
			if (!condition)
			{
				throw new MigrationDataCorruptionException(string.Format(formatString, parameters));
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004C8C File Offset: 0x00002E8C
		public static void RunOperationWithCulture(CultureInfo culture, Action operation)
		{
			CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
			CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
			try
			{
				Thread.CurrentThread.CurrentCulture = culture;
				Thread.CurrentThread.CurrentUICulture = culture;
				operation();
			}
			finally
			{
				Thread.CurrentThread.CurrentCulture = currentCulture;
				Thread.CurrentThread.CurrentUICulture = currentUICulture;
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004D0C File Offset: 0x00002F0C
		internal static void RunTimedOperation(AnchorContext context, Action operation, object debugInfo)
		{
			AnchorUtil.RunTimedOperation<int>(context, delegate()
			{
				operation();
				return 0;
			}, debugInfo);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004D3C File Offset: 0x00002F3C
		internal static T RunTimedOperation<T>(AnchorContext context, Func<T> operation, object debugInfo)
		{
			TimedOperationRunner timedOperationRunner = context.CreateOperationRunner();
			return timedOperationRunner.RunOperation<T>(operation, debugInfo);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004D58 File Offset: 0x00002F58
		internal static void ThrowOnNullArgument(object argument, string argumentName)
		{
			if (argument == null)
			{
				throw new ArgumentNullException(argumentName);
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004D64 File Offset: 0x00002F64
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

		// Token: 0x06000122 RID: 290 RVA: 0x00004D7F File Offset: 0x00002F7F
		internal static void ThrowOnCollectionEmptyArgument(IEnumerable argument, string argumentName)
		{
			if (argument == null)
			{
				throw new ArgumentNullException(argumentName);
			}
			if (!argument.GetEnumerator().MoveNext())
			{
				throw new ArgumentException(argumentName);
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004D9F File Offset: 0x00002F9F
		internal static void ThrowOnLessThanZeroArgument(long argument, string argumentName)
		{
			if (argument < 0L)
			{
				throw new ArgumentOutOfRangeException(argumentName);
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004DAD File Offset: 0x00002FAD
		internal static void ThrowOnGuidEmptyArgument(Guid argument, string argumentName)
		{
			if (Guid.Empty == argument)
			{
				throw new ArgumentException(argumentName);
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004DC4 File Offset: 0x00002FC4
		internal static string GetCurrentStackTrace()
		{
			StringBuilder stringBuilder = new StringBuilder();
			StackTrace stackTrace = new StackTrace(2, false);
			foreach (StackFrame stackFrame in stackTrace.GetFrames().Take(10))
			{
				MethodBase method = stackFrame.GetMethod();
				stringBuilder.AppendFormat("{0}:{1};", method.DeclaringType, method);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004E44 File Offset: 0x00003044
		internal static bool IsTransientException(Exception exception)
		{
			return exception is TransientException;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005080 File Offset: 0x00003280
		internal static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> entries, int batchSize)
		{
			if (batchSize <= 0)
			{
				throw new ArgumentException("batchSize must be greater than 0.", "batchSize");
			}
			List<T> batch = new List<T>(batchSize);
			foreach (T entry in entries)
			{
				batch.Add(entry);
				if (batch.Count >= batchSize)
				{
					yield return batch;
					batch = new List<T>(batchSize);
				}
			}
			if (batch.Count > 0)
			{
				yield return batch;
			}
			yield break;
		}
	}
}
