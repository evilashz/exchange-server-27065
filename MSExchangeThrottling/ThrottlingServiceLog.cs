using System;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ThrottlingService
{
	// Token: 0x02000009 RID: 9
	internal class ThrottlingServiceLog
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00003160 File Offset: 0x00001360
		public static void Start()
		{
			if (!ThrottlingAppConfig.LoggingEnabled)
			{
				return;
			}
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			ThrottlingServiceLog.throttlingServiceLogSchema = new LogSchema("Microsoft Exchange Server", executingAssembly.GetName().Version.ToString(), "Throttling Service Log", ThrottlingServiceLog.Fields);
			string logPath = ThrottlingAppConfig.LogPath;
			if (!string.IsNullOrEmpty(logPath))
			{
				ThrottlingServiceLog.log = new Log("TRTL", new LogHeaderFormatter(ThrottlingServiceLog.throttlingServiceLogSchema), "ThrottlingServiceLogs");
				ThrottlingServiceLog.log.Configure(logPath, ThrottlingAppConfig.LoggingMaxAge, (long)ThrottlingAppConfig.LoggingDirectorySize.ToBytes(), (long)ByteQuantifiedSize.FromMB(10UL).ToBytes(), 1048576, TimeSpan.FromSeconds(30.0));
				ThrottlingServiceLog.enabled = true;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003218 File Offset: 0x00001418
		public static void LogServiceStart()
		{
			if (!ThrottlingServiceLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(ThrottlingServiceLog.throttlingServiceLogSchema);
			logRowFormatter[10] = "Started MsExchangeThrottling service.";
			logRowFormatter[1] = ThrottlingServiceLog.ThrottlingEventId.START;
			ThrottlingServiceLog.log.Append(logRowFormatter, 0);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003260 File Offset: 0x00001460
		public static void LogObtainTokens<T>(ObtainTokensRequest<T> request, object tokenInfo, bool result)
		{
			if (!ThrottlingServiceLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(ThrottlingServiceLog.throttlingServiceLogSchema);
			ThrottlingServiceLog.PopulateRequestParmeters<T>(logRowFormatter, request);
			logRowFormatter[9] = result;
			logRowFormatter[10] = tokenInfo;
			if (result)
			{
				logRowFormatter[1] = ThrottlingServiceLog.ThrottlingEventId.APPROVE;
			}
			else
			{
				logRowFormatter[1] = ThrottlingServiceLog.ThrottlingEventId.DENY;
			}
			ThrottlingServiceLog.log.Append(logRowFormatter, 0);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000032C9 File Offset: 0x000014C9
		public static void Stop()
		{
			if (ThrottlingServiceLog.log != null)
			{
				ThrottlingServiceLog.enabled = false;
				ThrottlingServiceLog.log.Close();
				ThrottlingServiceLog.log = null;
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000032E8 File Offset: 0x000014E8
		public static void LogTokenExpiry(object key, DailyTokenBucket dailyTokenBucket)
		{
			if (!ThrottlingServiceLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(ThrottlingServiceLog.throttlingServiceLogSchema);
			logRowFormatter[5] = key;
			logRowFormatter[10] = string.Format("Expiring entry. Details: {0}", dailyTokenBucket);
			logRowFormatter[1] = ThrottlingServiceLog.ThrottlingEventId.EXPIRE;
			ThrottlingServiceLog.log.Append(logRowFormatter, 0);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000333C File Offset: 0x0000153C
		internal static void LogThrottlingBypassed<T>(ObtainTokensRequest<T> request)
		{
			if (!ThrottlingServiceLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(ThrottlingServiceLog.throttlingServiceLogSchema);
			ThrottlingServiceLog.PopulateRequestParmeters<T>(logRowFormatter, request);
			logRowFormatter[10] = "Throttling is bypassed.";
			logRowFormatter[1] = ThrottlingServiceLog.ThrottlingEventId.BYPASS;
			ThrottlingServiceLog.log.Append(logRowFormatter, 0);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000338C File Offset: 0x0000158C
		private static string[] InitializeFields()
		{
			string[] array = new string[Enum.GetValues(typeof(ThrottlingServiceLog.Field)).Length];
			array[0] = "date-time";
			array[1] = "event-id";
			array[2] = "client-hostname";
			array[3] = "client-info";
			array[4] = "client-processname";
			array[5] = "mailbox";
			array[9] = "response";
			array[6] = "requested-action";
			array[7] = "quota";
			array[8] = "requested-token-count";
			array[10] = "data";
			return array;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003410 File Offset: 0x00001610
		private static void PopulateRequestParmeters<T>(LogRowFormatter row, ObtainTokensRequest<T> request)
		{
			row[2] = request.ClientHostName;
			row[4] = request.ClientProcessName;
			row[3] = request.ClientType;
			row[5] = request.MailboxGuid;
			row[6] = request.RequestedAction;
			row[8] = request.RequestedTokenCount;
			row[7] = request.TotalTokenCount;
		}

		// Token: 0x04000028 RID: 40
		private const string LogComponentName = "ThrottlingServiceLogs";

		// Token: 0x04000029 RID: 41
		private static readonly string[] Fields = ThrottlingServiceLog.InitializeFields();

		// Token: 0x0400002A RID: 42
		private static LogSchema throttlingServiceLogSchema;

		// Token: 0x0400002B RID: 43
		private static Log log;

		// Token: 0x0400002C RID: 44
		private static bool enabled;

		// Token: 0x0200000A RID: 10
		internal enum Field
		{
			// Token: 0x0400002E RID: 46
			Time,
			// Token: 0x0400002F RID: 47
			EventId,
			// Token: 0x04000030 RID: 48
			ClientHostName,
			// Token: 0x04000031 RID: 49
			ClientInfo,
			// Token: 0x04000032 RID: 50
			ClientProcessName,
			// Token: 0x04000033 RID: 51
			Mailbox,
			// Token: 0x04000034 RID: 52
			RequestedAction,
			// Token: 0x04000035 RID: 53
			Quota,
			// Token: 0x04000036 RID: 54
			RequestedToken,
			// Token: 0x04000037 RID: 55
			Response,
			// Token: 0x04000038 RID: 56
			Data
		}

		// Token: 0x0200000B RID: 11
		private enum ThrottlingEventId
		{
			// Token: 0x0400003A RID: 58
			APPROVE,
			// Token: 0x0400003B RID: 59
			BYPASS,
			// Token: 0x0400003C RID: 60
			DENY,
			// Token: 0x0400003D RID: 61
			EXPIRE,
			// Token: 0x0400003E RID: 62
			START
		}
	}
}
