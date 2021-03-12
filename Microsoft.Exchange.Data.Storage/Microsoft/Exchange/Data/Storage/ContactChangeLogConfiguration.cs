using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000558 RID: 1368
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ContactChangeLogConfiguration : ILogConfiguration
	{
		// Token: 0x170011EA RID: 4586
		// (get) Token: 0x060039B8 RID: 14776 RVA: 0x000EC97A File Offset: 0x000EAB7A
		public static ContactChangeLogConfiguration Default
		{
			get
			{
				if (ContactChangeLogConfiguration.defaultInstance == null)
				{
					ContactChangeLogConfiguration.defaultInstance = new ContactChangeLogConfiguration();
				}
				return ContactChangeLogConfiguration.defaultInstance;
			}
		}

		// Token: 0x170011EB RID: 4587
		// (get) Token: 0x060039B9 RID: 14777 RVA: 0x000EC992 File Offset: 0x000EAB92
		public bool IsLoggingEnabled
		{
			get
			{
				return ContactChangeLogConfiguration.Enabled.Value;
			}
		}

		// Token: 0x170011EC RID: 4588
		// (get) Token: 0x060039BA RID: 14778 RVA: 0x000EC99E File Offset: 0x000EAB9E
		public bool IsActivityEventHandler
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170011ED RID: 4589
		// (get) Token: 0x060039BB RID: 14779 RVA: 0x000EC9A1 File Offset: 0x000EABA1
		public string LogPath
		{
			get
			{
				return this.directoryPath;
			}
		}

		// Token: 0x170011EE RID: 4590
		// (get) Token: 0x060039BC RID: 14780 RVA: 0x000EC9A9 File Offset: 0x000EABA9
		public string LogPrefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x170011EF RID: 4591
		// (get) Token: 0x060039BD RID: 14781 RVA: 0x000EC9B1 File Offset: 0x000EABB1
		public string LogComponent
		{
			get
			{
				return "ContactChangeLogging";
			}
		}

		// Token: 0x170011F0 RID: 4592
		// (get) Token: 0x060039BE RID: 14782 RVA: 0x000EC9B8 File Offset: 0x000EABB8
		public string LogType
		{
			get
			{
				return "Contact Change Log";
			}
		}

		// Token: 0x170011F1 RID: 4593
		// (get) Token: 0x060039BF RID: 14783 RVA: 0x000EC9C0 File Offset: 0x000EABC0
		public long MaxLogDirectorySizeInBytes
		{
			get
			{
				return (long)ContactChangeLogConfiguration.MaxDirectorySize.Value.ToBytes();
			}
		}

		// Token: 0x170011F2 RID: 4594
		// (get) Token: 0x060039C0 RID: 14784 RVA: 0x000EC9E0 File Offset: 0x000EABE0
		public long MaxLogFileSizeInBytes
		{
			get
			{
				return (long)ContactChangeLogConfiguration.MaxFileSize.Value.ToBytes();
			}
		}

		// Token: 0x170011F3 RID: 4595
		// (get) Token: 0x060039C1 RID: 14785 RVA: 0x000EC9FF File Offset: 0x000EABFF
		public TimeSpan MaxLogAge
		{
			get
			{
				return ContactChangeLogConfiguration.MaxAge.Value;
			}
		}

		// Token: 0x060039C2 RID: 14786 RVA: 0x000ECA0C File Offset: 0x000EAC0C
		private ContactChangeLogConfiguration()
		{
			this.prefix = "ContactChange_" + ApplicationName.Current.UniqueId + "_";
			this.directoryPath = ((ContactChangeLogConfiguration.DirectoryPath.Value != null) ? ContactChangeLogConfiguration.DirectoryPath.Value : Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\ContactChangeLogging\\"));
		}

		// Token: 0x04001ED9 RID: 7897
		private const string Type = "Contact Change Log";

		// Token: 0x04001EDA RID: 7898
		private const string Component = "ContactChangeLogging";

		// Token: 0x04001EDB RID: 7899
		private static readonly Trace Tracer = ExTraceGlobals.ContactChangeLoggingTracer;

		// Token: 0x04001EDC RID: 7900
		private static readonly BoolAppSettingsEntry Enabled = new BoolAppSettingsEntry("ContactChangeLoggingEnabled", true, ContactChangeLogConfiguration.Tracer);

		// Token: 0x04001EDD RID: 7901
		private static readonly StringAppSettingsEntry DirectoryPath = new StringAppSettingsEntry("ContactChangeLoggingPath", null, ContactChangeLogConfiguration.Tracer);

		// Token: 0x04001EDE RID: 7902
		private static readonly TimeSpanAppSettingsEntry MaxAge = new TimeSpanAppSettingsEntry("ContactChangeLoggingMaxAge", TimeSpanUnit.Minutes, TimeSpan.FromDays(30.0), ContactChangeLogConfiguration.Tracer);

		// Token: 0x04001EDF RID: 7903
		private static readonly ByteQuantifiedSizeAppSettingsEntry MaxDirectorySize = new ByteQuantifiedSizeAppSettingsEntry("ContactChangeLoggingMaxDirectorySize", ByteQuantifiedSize.FromGB(1UL), ContactChangeLogConfiguration.Tracer);

		// Token: 0x04001EE0 RID: 7904
		private static readonly ByteQuantifiedSizeAppSettingsEntry MaxFileSize = new ByteQuantifiedSizeAppSettingsEntry("ContactChangeLoggingMaxFileSize", ByteQuantifiedSize.FromMB(10UL), ContactChangeLogConfiguration.Tracer);

		// Token: 0x04001EE1 RID: 7905
		private readonly string prefix;

		// Token: 0x04001EE2 RID: 7906
		private readonly string directoryPath;

		// Token: 0x04001EE3 RID: 7907
		private static ContactChangeLogConfiguration defaultInstance;
	}
}
