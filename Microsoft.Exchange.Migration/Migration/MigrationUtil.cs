using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Migration.Logging;
using Microsoft.Exchange.Transport.Sync.Common;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000028 RID: 40
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class MigrationUtil
	{
		// Token: 0x060001AB RID: 427 RVA: 0x00009004 File Offset: 0x00007204
		public static void AssertOrThrow(bool condition, string formatString, params object[] parameters)
		{
			if (!condition)
			{
				throw new MigrationPermanentException(Strings.MigrationGenericError)
				{
					InternalError = string.Format(formatString, parameters)
				};
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000902E File Offset: 0x0000722E
		internal static void ThrowOnNullArgument(object argument, string argumentName)
		{
			if (argument == null)
			{
				throw new ArgumentNullException(argumentName);
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000903A File Offset: 0x0000723A
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

		// Token: 0x060001AE RID: 430 RVA: 0x00009058 File Offset: 0x00007258
		internal static void ThrowOnCollectionEmptyArgument(IEnumerable argument, string argumentName)
		{
			if (argument == null)
			{
				throw new ArgumentNullException(argumentName);
			}
			bool flag = argument is ICollection;
			if ((flag && ((ICollection)argument).Count == 0) || (!flag && !argument.GetEnumerator().MoveNext()))
			{
				throw new ArgumentException(argumentName);
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000090A0 File Offset: 0x000072A0
		internal static void ThrowOnLessThanZeroArgument(long argument, string argumentName)
		{
			if (argument < 0L)
			{
				throw new ArgumentOutOfRangeException(argumentName);
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000090AE File Offset: 0x000072AE
		internal static void ThrowOnGuidEmptyArgument(Guid argument, string argumentName)
		{
			if (Guid.Empty == argument)
			{
				throw new ArgumentException(argumentName);
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000090C4 File Offset: 0x000072C4
		internal static string EncryptedStringToClearText(string encryptedString)
		{
			if (string.IsNullOrEmpty(encryptedString))
			{
				return null;
			}
			string result;
			using (SecureString secureString = MigrationServiceFactory.Instance.GetCryptoAdapter().EncryptedStringToSecureString(encryptedString))
			{
				result = secureString.AsUnsecureString();
			}
			return result;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00009110 File Offset: 0x00007310
		internal static string GetCurrentStackTrace()
		{
			StringBuilder stringBuilder = new StringBuilder();
			StackTrace stackTrace = new StackTrace(2, false);
			foreach (StackFrame stackFrame in stackTrace.GetFrames().Take(10))
			{
				MethodBase method = stackFrame.GetMethod();
				stringBuilder.AppendFormat("{0}:{1};", method.DeclaringType, method.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00009198 File Offset: 0x00007398
		internal static TimeSpan MinTimeSpan(TimeSpan timespan1, TimeSpan timespan2)
		{
			if (timespan1 < timespan2)
			{
				return timespan1;
			}
			return timespan2;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000091BC File Offset: 0x000073BC
		internal static void RunTimedOperation(Action operation, object debugInfo)
		{
			MigrationUtil.RunTimedOperation<int>(delegate()
			{
				operation();
				return 0;
			}, debugInfo);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000091EC File Offset: 0x000073EC
		internal static T RunTimedOperation<T>(Func<T> operation, object debugInfo)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			T result;
			try
			{
				result = operation();
			}
			finally
			{
				stopwatch.Stop();
				TimeSpan config = ConfigBase<MigrationServiceConfigSchema>.GetConfig<TimeSpan>("MigrationSlowOperationThreshold");
				if (config < stopwatch.Elapsed)
				{
					MigrationLogger.Log(MigrationEventType.Error, "SLOW Operation: took {0}s using '{1}' stack trace {2}", new object[]
					{
						stopwatch.Elapsed.Seconds,
						debugInfo,
						MigrationUtil.GetCurrentStackTrace()
					});
				}
				else
				{
					MigrationLogger.Log(MigrationEventType.Instrumentation, "Operation: took {0} using '{1}'", new object[]
					{
						stopwatch.Elapsed,
						debugInfo
					});
				}
			}
			return result;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000092A0 File Offset: 0x000074A0
		internal static bool IsFeatureBlocked(MigrationFeature features)
		{
			MigrationFeature config = ConfigBase<MigrationServiceConfigSchema>.GetConfig<MigrationFeature>("BlockedMigrationFeatures");
			return (config & features) != MigrationFeature.None;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000092C1 File Offset: 0x000074C1
		internal static bool HasUnicodeCharacters(string token)
		{
			return !string.IsNullOrEmpty(token) && SyncUtilities.HasUnicodeCharacters(token);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000092D3 File Offset: 0x000074D3
		internal static bool IsTransientException(Exception exception)
		{
			return exception is TransientException || CommonUtils.IsTransientException(exception) || (exception.InnerException != null && CommonUtils.IsTransientException(exception.InnerException));
		}
	}
}
