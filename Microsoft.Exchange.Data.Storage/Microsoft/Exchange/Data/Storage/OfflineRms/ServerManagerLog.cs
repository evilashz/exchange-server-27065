using System;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.OfflineRms
{
	// Token: 0x02000AC5 RID: 2757
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ServerManagerLog
	{
		// Token: 0x0600646F RID: 25711 RVA: 0x001A9BB0 File Offset: 0x001A7DB0
		public static string GetExceptionLogString(Exception e, ServerManagerLog.ExceptionLogOption option)
		{
			Exception ex = e;
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			for (;;)
			{
				stringBuilder.Append("[Message:");
				stringBuilder.Append(ex.Message);
				stringBuilder.Append("]");
				stringBuilder.Append("[Type:");
				stringBuilder.Append(ex.GetType().ToString());
				stringBuilder.Append("]");
				if ((option & ServerManagerLog.ExceptionLogOption.IncludeStack) == ServerManagerLog.ExceptionLogOption.IncludeStack)
				{
					stringBuilder.Append("[Stack:");
					stringBuilder.Append(string.IsNullOrEmpty(ex.StackTrace) ? string.Empty : ex.StackTrace.Replace("\r\n", string.Empty));
					stringBuilder.Append("]");
				}
				if ((option & ServerManagerLog.ExceptionLogOption.IncludeInnerException) != ServerManagerLog.ExceptionLogOption.IncludeInnerException || ex.InnerException == null || num > 10)
				{
					break;
				}
				ex = ex.InnerException;
				num++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006470 RID: 25712 RVA: 0x001A9C90 File Offset: 0x001A7E90
		public static void LogEvent(ServerManagerLog.Subcomponent subcomponent, ServerManagerLog.EventType eventType, RmsClientManagerContext clientManagerContext, string data)
		{
			if (string.IsNullOrEmpty(data))
			{
				throw new ArgumentNullException("data");
			}
			ServerManagerLog.InitializeIfNeeded();
			LogRowFormatter logRowFormatter = new LogRowFormatter(ServerManagerLog.LogSchema);
			logRowFormatter[1] = subcomponent;
			logRowFormatter[2] = eventType;
			if (clientManagerContext != null)
			{
				logRowFormatter[3] = clientManagerContext.OrgId.OrganizationalUnit.ToString();
				logRowFormatter[6] = clientManagerContext.TransactionId.ToString();
				if (clientManagerContext.ContextID != RmsClientManagerContext.ContextId.None && !string.IsNullOrEmpty(clientManagerContext.ContextStringForm))
				{
					logRowFormatter[5] = clientManagerContext.ContextStringForm;
				}
			}
			logRowFormatter[4] = data;
			ServerManagerLog.instance.logInstance.Append(logRowFormatter, 0);
		}

		// Token: 0x06006471 RID: 25713 RVA: 0x001A9D4C File Offset: 0x001A7F4C
		private static void InitializeIfNeeded()
		{
			if (!ServerManagerLog.instance.initialized)
			{
				lock (ServerManagerLog.instance.initializeLockObject)
				{
					if (!ServerManagerLog.instance.initialized)
					{
						ServerManagerLog.instance.Initialize();
						ServerManagerLog.instance.initialized = true;
					}
				}
			}
		}

		// Token: 0x06006472 RID: 25714 RVA: 0x001A9DB8 File Offset: 0x001A7FB8
		private void Initialize()
		{
			ServerManagerLog.instance.logInstance = new Log(RmsClientManagerUtils.GetUniqueFileNameForProcess("OfflineRMSLog", true), new LogHeaderFormatter(ServerManagerLog.LogSchema), "OfflineRMSServerLog");
			ServerManagerLog.instance.logInstance.Configure(Path.Combine(ExchangeSetupContext.InstallPath, "TransportRoles\\Logs\\OfflineRMS\\"), ServerManagerLog.LogMaxAge, 262144000L, 10485760L);
		}

		// Token: 0x040038F3 RID: 14579
		internal const string CRLF = "\r\n";

		// Token: 0x040038F4 RID: 14580
		private const string DefaultLogPath = "TransportRoles\\Logs\\OfflineRMS\\";

		// Token: 0x040038F5 RID: 14581
		private const string LogType = "OfflineRMS Server Log";

		// Token: 0x040038F6 RID: 14582
		private const string LogComponent = "OfflineRMSServerLog";

		// Token: 0x040038F7 RID: 14583
		private const string LogSuffix = "OfflineRMSLog";

		// Token: 0x040038F8 RID: 14584
		private const int MaxLogDirectorySize = 262144000;

		// Token: 0x040038F9 RID: 14585
		private const int MaxLogFileSize = 10485760;

		// Token: 0x040038FA RID: 14586
		private static readonly EnhancedTimeSpan LogMaxAge = EnhancedTimeSpan.FromDays(30.0);

		// Token: 0x040038FB RID: 14587
		private static readonly ServerManagerLog instance = new ServerManagerLog();

		// Token: 0x040038FC RID: 14588
		private static readonly string[] Fields = new string[]
		{
			"date-time",
			"subcomponent",
			"event-type",
			"tenant-id",
			"data",
			"context",
			"transaction-id"
		};

		// Token: 0x040038FD RID: 14589
		private static readonly LogSchema LogSchema = new LogSchema("Microsoft Exchange Server", Assembly.GetExecutingAssembly().GetName().Version.ToString(), "OfflineRMS Server Log", ServerManagerLog.Fields);

		// Token: 0x040038FE RID: 14590
		private readonly object initializeLockObject = new object();

		// Token: 0x040038FF RID: 14591
		private Log logInstance;

		// Token: 0x04003900 RID: 14592
		private bool initialized;

		// Token: 0x02000AC6 RID: 2758
		internal enum Subcomponent
		{
			// Token: 0x04003902 RID: 14594
			ServerInit,
			// Token: 0x04003903 RID: 14595
			AcquireUseLicense,
			// Token: 0x04003904 RID: 14596
			AcquireTenantLicenses,
			// Token: 0x04003905 RID: 14597
			DirectoryServiceProvider,
			// Token: 0x04003906 RID: 14598
			RpcClientWrapper,
			// Token: 0x04003907 RID: 14599
			RpcServerWrapper,
			// Token: 0x04003908 RID: 14600
			TrustedPublishingDomainPrivateKeyProvider,
			// Token: 0x04003909 RID: 14601
			PerTenantRMSTrustedPublishingDomainConfiguration
		}

		// Token: 0x02000AC7 RID: 2759
		internal enum EventType
		{
			// Token: 0x0400390B RID: 14603
			Entry,
			// Token: 0x0400390C RID: 14604
			Verbose,
			// Token: 0x0400390D RID: 14605
			Success,
			// Token: 0x0400390E RID: 14606
			Warning,
			// Token: 0x0400390F RID: 14607
			Error,
			// Token: 0x04003910 RID: 14608
			Statistics
		}

		// Token: 0x02000AC8 RID: 2760
		[Flags]
		internal enum ExceptionLogOption
		{
			// Token: 0x04003912 RID: 14610
			Default = 1,
			// Token: 0x04003913 RID: 14611
			IncludeStack = 2,
			// Token: 0x04003914 RID: 14612
			IncludeInnerException = 4
		}

		// Token: 0x02000AC9 RID: 2761
		private enum Field
		{
			// Token: 0x04003916 RID: 14614
			Time,
			// Token: 0x04003917 RID: 14615
			Subcomponent,
			// Token: 0x04003918 RID: 14616
			EventType,
			// Token: 0x04003919 RID: 14617
			TenantId,
			// Token: 0x0400391A RID: 14618
			Data,
			// Token: 0x0400391B RID: 14619
			Context,
			// Token: 0x0400391C RID: 14620
			TransactionId
		}
	}
}
