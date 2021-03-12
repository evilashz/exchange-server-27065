using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007ED RID: 2029
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class GroupMessageEscalationLogConfiguration : ILogConfiguration
	{
		// Token: 0x06004C00 RID: 19456 RVA: 0x0013BE04 File Offset: 0x0013A004
		private GroupMessageEscalationLogConfiguration()
		{
			this.prefix = "GroupMessageEscalation_" + ApplicationName.Current.UniqueId + "_";
			this.directoryPath = (GroupMessageEscalationLogConfiguration.DirectoryPath.Value ?? Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\GroupMessageEscalationLogs\\"));
		}

		// Token: 0x170015C2 RID: 5570
		// (get) Token: 0x06004C01 RID: 19457 RVA: 0x0013BE59 File Offset: 0x0013A059
		public static GroupMessageEscalationLogConfiguration Default
		{
			get
			{
				if (GroupMessageEscalationLogConfiguration.defaultInstance == null)
				{
					GroupMessageEscalationLogConfiguration.defaultInstance = new GroupMessageEscalationLogConfiguration();
				}
				return GroupMessageEscalationLogConfiguration.defaultInstance;
			}
		}

		// Token: 0x170015C3 RID: 5571
		// (get) Token: 0x06004C02 RID: 19458 RVA: 0x0013BE71 File Offset: 0x0013A071
		public bool IsLoggingEnabled
		{
			get
			{
				return GroupMessageEscalationLogConfiguration.Enabled.Value;
			}
		}

		// Token: 0x170015C4 RID: 5572
		// (get) Token: 0x06004C03 RID: 19459 RVA: 0x0013BE7D File Offset: 0x0013A07D
		public bool IsActivityEventHandler
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170015C5 RID: 5573
		// (get) Token: 0x06004C04 RID: 19460 RVA: 0x0013BE80 File Offset: 0x0013A080
		public string LogPath
		{
			get
			{
				return this.directoryPath;
			}
		}

		// Token: 0x170015C6 RID: 5574
		// (get) Token: 0x06004C05 RID: 19461 RVA: 0x0013BE88 File Offset: 0x0013A088
		public string LogPrefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x170015C7 RID: 5575
		// (get) Token: 0x06004C06 RID: 19462 RVA: 0x0013BE90 File Offset: 0x0013A090
		public string LogComponent
		{
			get
			{
				return "GroupMessageEscalationLog";
			}
		}

		// Token: 0x170015C8 RID: 5576
		// (get) Token: 0x06004C07 RID: 19463 RVA: 0x0013BE97 File Offset: 0x0013A097
		public string LogType
		{
			get
			{
				return "Group Message Escalation Log";
			}
		}

		// Token: 0x170015C9 RID: 5577
		// (get) Token: 0x06004C08 RID: 19464 RVA: 0x0013BEA0 File Offset: 0x0013A0A0
		public long MaxLogDirectorySizeInBytes
		{
			get
			{
				return (long)GroupMessageEscalationLogConfiguration.MaxDirectorySize.Value.ToBytes();
			}
		}

		// Token: 0x170015CA RID: 5578
		// (get) Token: 0x06004C09 RID: 19465 RVA: 0x0013BEC0 File Offset: 0x0013A0C0
		public long MaxLogFileSizeInBytes
		{
			get
			{
				return (long)GroupMessageEscalationLogConfiguration.MaxFileSize.Value.ToBytes();
			}
		}

		// Token: 0x170015CB RID: 5579
		// (get) Token: 0x06004C0A RID: 19466 RVA: 0x0013BEDF File Offset: 0x0013A0DF
		public TimeSpan MaxLogAge
		{
			get
			{
				return GroupMessageEscalationLogConfiguration.MaxAge.Value;
			}
		}

		// Token: 0x04002954 RID: 10580
		private const string Type = "Group Message Escalation Log";

		// Token: 0x04002955 RID: 10581
		private const string Component = "GroupMessageEscalationLog";

		// Token: 0x04002956 RID: 10582
		private static readonly Trace Tracer = ExTraceGlobals.ModernGroupsTracer;

		// Token: 0x04002957 RID: 10583
		private static readonly BoolAppSettingsEntry Enabled = new BoolAppSettingsEntry("GroupMessageEscalationLogEnabled", true, GroupMessageEscalationLogConfiguration.Tracer);

		// Token: 0x04002958 RID: 10584
		private static readonly StringAppSettingsEntry DirectoryPath = new StringAppSettingsEntry("GroupMessageEscalationLogPath", null, GroupMessageEscalationLogConfiguration.Tracer);

		// Token: 0x04002959 RID: 10585
		private static readonly TimeSpanAppSettingsEntry MaxAge = new TimeSpanAppSettingsEntry("GroupMessageEscalationLogLogMaxAge", TimeSpanUnit.Minutes, TimeSpan.FromDays(7.0), GroupMessageEscalationLogConfiguration.Tracer);

		// Token: 0x0400295A RID: 10586
		private static readonly ByteQuantifiedSizeAppSettingsEntry MaxDirectorySize = new ByteQuantifiedSizeAppSettingsEntry("MailboxAssociationLogMaxDirectorySize", ByteQuantifiedSize.FromMB(250UL), GroupMessageEscalationLogConfiguration.Tracer);

		// Token: 0x0400295B RID: 10587
		private static readonly ByteQuantifiedSizeAppSettingsEntry MaxFileSize = new ByteQuantifiedSizeAppSettingsEntry("MailboxAssociationLogMaxFileSize", ByteQuantifiedSize.FromMB(10UL), GroupMessageEscalationLogConfiguration.Tracer);

		// Token: 0x0400295C RID: 10588
		private static GroupMessageEscalationLogConfiguration defaultInstance;

		// Token: 0x0400295D RID: 10589
		private readonly string prefix;

		// Token: 0x0400295E RID: 10590
		private readonly string directoryPath;
	}
}
