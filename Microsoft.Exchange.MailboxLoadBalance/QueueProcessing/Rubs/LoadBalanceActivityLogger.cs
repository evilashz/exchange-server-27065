using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MigrationWorkflowService;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.MailboxLoadBalance.Config;

namespace Microsoft.Exchange.MailboxLoadBalance.QueueProcessing.Rubs
{
	// Token: 0x020000E7 RID: 231
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LoadBalanceActivityLogger : ActivityContextLogger
	{
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x00014319 File Offset: 0x00012519
		protected override string FileNamePrefix
		{
			get
			{
				return this.logConfiguration.Value.FilenamePrefix;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x0001432B File Offset: 0x0001252B
		protected override string LogComponentName
		{
			get
			{
				return this.logConfiguration.Value.LogComponentName;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x0001433D File Offset: 0x0001253D
		protected override string LogTypeName
		{
			get
			{
				return "Mailbox Load Balance Activities";
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x00014344 File Offset: 0x00012544
		protected override int TimestampField
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x00014347 File Offset: 0x00012547
		protected override Trace Tracer
		{
			get
			{
				return ExTraceGlobals.MigrationWorkflowServiceTracer;
			}
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0001434E File Offset: 0x0001254E
		protected override string[] GetLogFields()
		{
			return Enum.GetNames(typeof(LoadBalanceActivityLogger.LogFields));
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0001435F File Offset: 0x0001255F
		protected override ActivityContextLogFileSettings GetLogFileSettings()
		{
			return this.logConfiguration.Value;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0001436C File Offset: 0x0001256C
		protected override void InternalLogActivityEvent(IActivityScope activityScope, ActivityEventType eventType)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(base.LogSchema);
			logRowFormatter[1] = activityScope.ActivityType;
			logRowFormatter[2] = ActivityContextLogger.ActivityEventTypeDictionary[eventType];
			logRowFormatter[3] = activityScope.Status;
			logRowFormatter[4] = activityScope.StartTime;
			logRowFormatter[5] = activityScope.EndTime;
			logRowFormatter[6] = WorkloadManagementLogger.FormatWlmActivity(activityScope, true);
			base.AppendLog(logRowFormatter);
		}

		// Token: 0x040002B9 RID: 697
		private const string LoadBalanceActivityLogTypeName = "Mailbox Load Balance Activities";

		// Token: 0x040002BA RID: 698
		private readonly Lazy<LoadBalanceActivityLogger.ContextLogSettings> logConfiguration = new Lazy<LoadBalanceActivityLogger.ContextLogSettings>(() => new LoadBalanceActivityLogger.ContextLogSettings());

		// Token: 0x020000E8 RID: 232
		private enum LogFields
		{
			// Token: 0x040002BD RID: 701
			Timestamp,
			// Token: 0x040002BE RID: 702
			Activity,
			// Token: 0x040002BF RID: 703
			EventType,
			// Token: 0x040002C0 RID: 704
			Status,
			// Token: 0x040002C1 RID: 705
			StartTime,
			// Token: 0x040002C2 RID: 706
			EndTime,
			// Token: 0x040002C3 RID: 707
			CustomData
		}

		// Token: 0x020000E9 RID: 233
		private class ContextLogSettings : ActivityContextLogFileSettings
		{
			// Token: 0x17000237 RID: 567
			// (get) Token: 0x06000721 RID: 1825 RVA: 0x0001442B File Offset: 0x0001262B
			public string FilenamePrefix
			{
				get
				{
					return this.logConfig.Value.FilenamePrefix;
				}
			}

			// Token: 0x17000238 RID: 568
			// (get) Token: 0x06000722 RID: 1826 RVA: 0x0001443D File Offset: 0x0001263D
			public string LogComponentName
			{
				get
				{
					return this.logConfig.Value.LogComponentName;
				}
			}

			// Token: 0x17000239 RID: 569
			// (get) Token: 0x06000723 RID: 1827 RVA: 0x0001444F File Offset: 0x0001264F
			protected override string LogSubFolderName
			{
				get
				{
					return "Activity";
				}
			}

			// Token: 0x1700023A RID: 570
			// (get) Token: 0x06000724 RID: 1828 RVA: 0x00014456 File Offset: 0x00012656
			protected override string LogTypeName
			{
				get
				{
					return "Mailbox Load Balance Activities";
				}
			}

			// Token: 0x1700023B RID: 571
			// (get) Token: 0x06000725 RID: 1829 RVA: 0x0001445D File Offset: 0x0001265D
			protected override Trace Tracer
			{
				get
				{
					return ExTraceGlobals.MailboxLoadBalanceTracer;
				}
			}

			// Token: 0x06000726 RID: 1830 RVA: 0x00014464 File Offset: 0x00012664
			protected override void LoadCustomSettings()
			{
				base.DirectoryPath = this.logConfig.Value.LoggingFolder;
				base.MaxDirectorySize = ByteQuantifiedSize.FromBytes((ulong)this.logConfig.Value.MaxLogDirSize);
				base.MaxFileSize = ByteQuantifiedSize.FromBytes((ulong)this.logConfig.Value.MaxLogFileSize);
				base.MaxAge = this.logConfig.Value.MaxLogAge;
			}

			// Token: 0x040002C4 RID: 708
			private readonly Lazy<LoadBalanceLoggingConfig> logConfig = new Lazy<LoadBalanceLoggingConfig>(() => new LoadBalanceLoggingConfig("MailboxLoadBalanceActivity"));
		}
	}
}
