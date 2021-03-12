using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009CA RID: 2506
	[Cmdlet("Set", "FrontendTransportService", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetFrontendTransportService : SetSystemConfigurationObjectTask<FrontendTransportServerIdParameter, FrontendTransportServerPresentationObject, FrontendTransportServer>
	{
		// Token: 0x17001A99 RID: 6809
		// (get) Token: 0x0600594B RID: 22859 RVA: 0x001772EA File Offset: 0x001754EA
		// (set) Token: 0x0600594C RID: 22860 RVA: 0x00177301 File Offset: 0x00175501
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan AgentLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)base.Fields["AgentLogMaxAge"];
			}
			set
			{
				base.Fields["AgentLogMaxAge"] = value;
			}
		}

		// Token: 0x17001A9A RID: 6810
		// (get) Token: 0x0600594D RID: 22861 RVA: 0x00177319 File Offset: 0x00175519
		// (set) Token: 0x0600594E RID: 22862 RVA: 0x00177330 File Offset: 0x00175530
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> AgentLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)base.Fields["AgentLogMaxDirectorySize"];
			}
			set
			{
				base.Fields["AgentLogMaxDirectorySize"] = value;
			}
		}

		// Token: 0x17001A9B RID: 6811
		// (get) Token: 0x0600594F RID: 22863 RVA: 0x00177348 File Offset: 0x00175548
		// (set) Token: 0x06005950 RID: 22864 RVA: 0x0017735F File Offset: 0x0017555F
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> AgentLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)base.Fields["AgentLogMaxFileSize"];
			}
			set
			{
				base.Fields["AgentLogMaxFileSize"] = value;
			}
		}

		// Token: 0x17001A9C RID: 6812
		// (get) Token: 0x06005951 RID: 22865 RVA: 0x00177377 File Offset: 0x00175577
		// (set) Token: 0x06005952 RID: 22866 RVA: 0x0017738E File Offset: 0x0017558E
		[Parameter(Mandatory = false)]
		public LocalLongFullPath AgentLogPath
		{
			get
			{
				return (LocalLongFullPath)base.Fields["AgentLogPath"];
			}
			set
			{
				base.Fields["AgentLogPath"] = value;
			}
		}

		// Token: 0x17001A9D RID: 6813
		// (get) Token: 0x06005953 RID: 22867 RVA: 0x001773A1 File Offset: 0x001755A1
		// (set) Token: 0x06005954 RID: 22868 RVA: 0x001773B8 File Offset: 0x001755B8
		[Parameter(Mandatory = false)]
		public bool AgentLogEnabled
		{
			get
			{
				return (bool)base.Fields["AgentLogEnabled"];
			}
			set
			{
				base.Fields["AgentLogEnabled"] = value;
			}
		}

		// Token: 0x17001A9E RID: 6814
		// (get) Token: 0x06005955 RID: 22869 RVA: 0x001773D0 File Offset: 0x001755D0
		// (set) Token: 0x06005956 RID: 22870 RVA: 0x001773E7 File Offset: 0x001755E7
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan DnsLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)base.Fields["DnsLogMaxAge"];
			}
			set
			{
				base.Fields["DnsLogMaxAge"] = value;
			}
		}

		// Token: 0x17001A9F RID: 6815
		// (get) Token: 0x06005957 RID: 22871 RVA: 0x001773FF File Offset: 0x001755FF
		// (set) Token: 0x06005958 RID: 22872 RVA: 0x00177416 File Offset: 0x00175616
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> DnsLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)base.Fields["DnsLogMaxDirectorySize"];
			}
			set
			{
				base.Fields["DnsLogMaxDirectorySize"] = value;
			}
		}

		// Token: 0x17001AA0 RID: 6816
		// (get) Token: 0x06005959 RID: 22873 RVA: 0x0017742E File Offset: 0x0017562E
		// (set) Token: 0x0600595A RID: 22874 RVA: 0x00177445 File Offset: 0x00175645
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> DnsLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)base.Fields["DnsLogMaxFileSize"];
			}
			set
			{
				base.Fields["DnsLogMaxFileSize"] = value;
			}
		}

		// Token: 0x17001AA1 RID: 6817
		// (get) Token: 0x0600595B RID: 22875 RVA: 0x0017745D File Offset: 0x0017565D
		// (set) Token: 0x0600595C RID: 22876 RVA: 0x00177474 File Offset: 0x00175674
		[Parameter(Mandatory = false)]
		public LocalLongFullPath DnsLogPath
		{
			get
			{
				return (LocalLongFullPath)base.Fields["DnsLogPath"];
			}
			set
			{
				base.Fields["DnsLogPath"] = value;
			}
		}

		// Token: 0x17001AA2 RID: 6818
		// (get) Token: 0x0600595D RID: 22877 RVA: 0x00177487 File Offset: 0x00175687
		// (set) Token: 0x0600595E RID: 22878 RVA: 0x0017749E File Offset: 0x0017569E
		[Parameter(Mandatory = false)]
		public bool DnsLogEnabled
		{
			get
			{
				return (bool)base.Fields["DnsLogEnabled"];
			}
			set
			{
				base.Fields["DnsLogEnabled"] = value;
			}
		}

		// Token: 0x17001AA3 RID: 6819
		// (get) Token: 0x0600595F RID: 22879 RVA: 0x001774B6 File Offset: 0x001756B6
		// (set) Token: 0x06005960 RID: 22880 RVA: 0x001774CD File Offset: 0x001756CD
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan ResourceLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)base.Fields["ResourceLogMaxAge"];
			}
			set
			{
				base.Fields["ResourceLogMaxAge"] = value;
			}
		}

		// Token: 0x17001AA4 RID: 6820
		// (get) Token: 0x06005961 RID: 22881 RVA: 0x001774E5 File Offset: 0x001756E5
		// (set) Token: 0x06005962 RID: 22882 RVA: 0x001774FC File Offset: 0x001756FC
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ResourceLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)base.Fields["ResourceLogMaxDirectorySize"];
			}
			set
			{
				base.Fields["ResourceLogMaxDirectorySize"] = value;
			}
		}

		// Token: 0x17001AA5 RID: 6821
		// (get) Token: 0x06005963 RID: 22883 RVA: 0x00177514 File Offset: 0x00175714
		// (set) Token: 0x06005964 RID: 22884 RVA: 0x0017752B File Offset: 0x0017572B
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ResourceLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)base.Fields["ResourceLogMaxFileSize"];
			}
			set
			{
				base.Fields["ResourceLogMaxFileSize"] = value;
			}
		}

		// Token: 0x17001AA6 RID: 6822
		// (get) Token: 0x06005965 RID: 22885 RVA: 0x00177543 File Offset: 0x00175743
		// (set) Token: 0x06005966 RID: 22886 RVA: 0x0017755A File Offset: 0x0017575A
		[Parameter(Mandatory = false)]
		public LocalLongFullPath ResourceLogPath
		{
			get
			{
				return (LocalLongFullPath)base.Fields["ResourceLogPath"];
			}
			set
			{
				base.Fields["ResourceLogPath"] = value;
			}
		}

		// Token: 0x17001AA7 RID: 6823
		// (get) Token: 0x06005967 RID: 22887 RVA: 0x0017756D File Offset: 0x0017576D
		// (set) Token: 0x06005968 RID: 22888 RVA: 0x00177584 File Offset: 0x00175784
		[Parameter(Mandatory = false)]
		public bool ResourceLogEnabled
		{
			get
			{
				return (bool)base.Fields["ResourceLogEnabled"];
			}
			set
			{
				base.Fields["ResourceLogEnabled"] = value;
			}
		}

		// Token: 0x17001AA8 RID: 6824
		// (get) Token: 0x06005969 RID: 22889 RVA: 0x0017759C File Offset: 0x0017579C
		// (set) Token: 0x0600596A RID: 22890 RVA: 0x001775B3 File Offset: 0x001757B3
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan AttributionLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)base.Fields["AttributionLogMaxAge"];
			}
			set
			{
				base.Fields["AttributionLogMaxAge"] = value;
			}
		}

		// Token: 0x17001AA9 RID: 6825
		// (get) Token: 0x0600596B RID: 22891 RVA: 0x001775CB File Offset: 0x001757CB
		// (set) Token: 0x0600596C RID: 22892 RVA: 0x001775E2 File Offset: 0x001757E2
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> AttributionLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)base.Fields["AttributionLogMaxDirectorySize"];
			}
			set
			{
				base.Fields["AttributionLogMaxDirectorySize"] = value;
			}
		}

		// Token: 0x17001AAA RID: 6826
		// (get) Token: 0x0600596D RID: 22893 RVA: 0x001775FA File Offset: 0x001757FA
		// (set) Token: 0x0600596E RID: 22894 RVA: 0x00177611 File Offset: 0x00175811
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> AttributionLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)base.Fields["AttributionLogMaxFileSize"];
			}
			set
			{
				base.Fields["AttributionLogMaxFileSize"] = value;
			}
		}

		// Token: 0x17001AAB RID: 6827
		// (get) Token: 0x0600596F RID: 22895 RVA: 0x00177629 File Offset: 0x00175829
		// (set) Token: 0x06005970 RID: 22896 RVA: 0x00177640 File Offset: 0x00175840
		[Parameter(Mandatory = false)]
		public LocalLongFullPath AttributionLogPath
		{
			get
			{
				return (LocalLongFullPath)base.Fields["AttributionLogPath"];
			}
			set
			{
				base.Fields["AttributionLogPath"] = value;
			}
		}

		// Token: 0x17001AAC RID: 6828
		// (get) Token: 0x06005971 RID: 22897 RVA: 0x00177653 File Offset: 0x00175853
		// (set) Token: 0x06005972 RID: 22898 RVA: 0x0017766A File Offset: 0x0017586A
		[Parameter(Mandatory = false)]
		public bool AttributionLogEnabled
		{
			get
			{
				return (bool)base.Fields["AttributionLogEnabled"];
			}
			set
			{
				base.Fields["AttributionLogEnabled"] = value;
			}
		}

		// Token: 0x17001AAD RID: 6829
		// (get) Token: 0x06005973 RID: 22899 RVA: 0x00177682 File Offset: 0x00175882
		// (set) Token: 0x06005974 RID: 22900 RVA: 0x00177699 File Offset: 0x00175899
		[Parameter(Mandatory = false)]
		public int MaxReceiveTlsRatePerMinute
		{
			get
			{
				return (int)base.Fields["MaxReceiveTlsRatePerMinute"];
			}
			set
			{
				base.Fields["MaxReceiveTlsRatePerMinute"] = value;
			}
		}

		// Token: 0x17001AAE RID: 6830
		// (get) Token: 0x06005975 RID: 22901 RVA: 0x001776B1 File Offset: 0x001758B1
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetFrontendTransportServer(this.Identity.ToString());
			}
		}

		// Token: 0x06005976 RID: 22902 RVA: 0x001776C4 File Offset: 0x001758C4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (base.Fields.IsModified("AgentLogMaxAge"))
			{
				this.DataObject.AgentLogMaxAge = this.AgentLogMaxAge;
			}
			if (base.Fields.IsModified("AgentLogMaxDirectorySize"))
			{
				this.DataObject.AgentLogMaxDirectorySize = this.AgentLogMaxDirectorySize;
			}
			if (base.Fields.IsModified("AgentLogMaxFileSize"))
			{
				this.DataObject.AgentLogMaxFileSize = this.AgentLogMaxFileSize;
			}
			if (base.Fields.IsModified("AgentLogPath"))
			{
				this.DataObject.AgentLogPath = this.AgentLogPath;
			}
			if (base.Fields.IsModified("AgentLogEnabled"))
			{
				this.DataObject.AgentLogEnabled = this.AgentLogEnabled;
			}
			if (base.Fields.IsModified("DnsLogMaxAge"))
			{
				this.DataObject.DnsLogMaxAge = this.DnsLogMaxAge;
			}
			if (base.Fields.IsModified("DnsLogMaxDirectorySize"))
			{
				this.DataObject.DnsLogMaxDirectorySize = this.DnsLogMaxDirectorySize;
			}
			if (base.Fields.IsModified("DnsLogMaxFileSize"))
			{
				this.DataObject.DnsLogMaxFileSize = this.DnsLogMaxFileSize;
			}
			if (base.Fields.IsModified("DnsLogPath"))
			{
				this.DataObject.DnsLogPath = this.DnsLogPath;
			}
			if (base.Fields.IsModified("DnsLogEnabled"))
			{
				this.DataObject.DnsLogEnabled = this.DnsLogEnabled;
			}
			if (base.Fields.IsModified("ResourceLogMaxAge"))
			{
				this.DataObject.ResourceLogMaxAge = this.ResourceLogMaxAge;
			}
			if (base.Fields.IsModified("ResourceLogMaxDirectorySize"))
			{
				this.DataObject.ResourceLogMaxDirectorySize = this.ResourceLogMaxDirectorySize;
			}
			if (base.Fields.IsModified("ResourceLogMaxFileSize"))
			{
				this.DataObject.ResourceLogMaxFileSize = this.ResourceLogMaxFileSize;
			}
			if (base.Fields.IsModified("ResourceLogPath"))
			{
				this.DataObject.ResourceLogPath = this.ResourceLogPath;
			}
			if (base.Fields.IsModified("ResourceLogEnabled"))
			{
				this.DataObject.ResourceLogEnabled = this.ResourceLogEnabled;
			}
			if (base.Fields.IsModified("AttributionLogMaxAge"))
			{
				this.DataObject.AttributionLogMaxAge = this.AttributionLogMaxAge;
			}
			if (base.Fields.IsModified("AttributionLogMaxDirectorySize"))
			{
				this.DataObject.AttributionLogMaxDirectorySize = this.AttributionLogMaxDirectorySize;
			}
			if (base.Fields.IsModified("AttributionLogMaxFileSize"))
			{
				this.DataObject.AttributionLogMaxFileSize = this.AttributionLogMaxFileSize;
			}
			if (base.Fields.IsModified("AttributionLogPath"))
			{
				this.DataObject.AttributionLogPath = this.AttributionLogPath;
			}
			if (base.Fields.IsModified("AttributionLogEnabled"))
			{
				this.DataObject.AttributionLogEnabled = this.AttributionLogEnabled;
			}
			if (base.Fields.IsModified("MaxReceiveTlsRatePerMinute"))
			{
				this.DataObject.MaxReceiveTlsRatePerMinute = this.MaxReceiveTlsRatePerMinute;
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x06005977 RID: 22903 RVA: 0x001779C0 File Offset: 0x00175BC0
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (this.Instance.IsModified(ADObjectSchema.Name))
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorServerNameModified), ErrorCategory.InvalidOperation, this.Identity);
			}
			if (this.Identity != null)
			{
				this.Identity = FrontendTransportServerIdParameter.CreateIdentity(this.Identity);
			}
			base.InternalValidate();
			if (!this.DataObject.IsFrontendTransportServer)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorTaskRunningLocationFrontendOnly), ErrorCategory.InvalidOperation, null);
			}
			if (base.HasErrors)
			{
				return;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04003325 RID: 13093
		private const string AgentLogMaxAgeKey = "AgentLogMaxAge";

		// Token: 0x04003326 RID: 13094
		private const string AgentLogMaxDirectorySizeKey = "AgentLogMaxDirectorySize";

		// Token: 0x04003327 RID: 13095
		private const string AgentLogMaxFileSizeKey = "AgentLogMaxFileSize";

		// Token: 0x04003328 RID: 13096
		private const string AgentLogPathKey = "AgentLogPath";

		// Token: 0x04003329 RID: 13097
		private const string AgentLogEnabledKey = "AgentLogEnabled";

		// Token: 0x0400332A RID: 13098
		private const string DnsLogMaxAgeKey = "DnsLogMaxAge";

		// Token: 0x0400332B RID: 13099
		private const string DnsLogMaxDirectorySizeKey = "DnsLogMaxDirectorySize";

		// Token: 0x0400332C RID: 13100
		private const string DnsLogMaxFileSizeKey = "DnsLogMaxFileSize";

		// Token: 0x0400332D RID: 13101
		private const string DnsLogPathKey = "DnsLogPath";

		// Token: 0x0400332E RID: 13102
		private const string DnsLogEnabledKey = "DnsLogEnabled";

		// Token: 0x0400332F RID: 13103
		private const string ResourceLogMaxAgeKey = "ResourceLogMaxAge";

		// Token: 0x04003330 RID: 13104
		private const string ResourceLogMaxDirectorySizeKey = "ResourceLogMaxDirectorySize";

		// Token: 0x04003331 RID: 13105
		private const string ResourceLogMaxFileSizeKey = "ResourceLogMaxFileSize";

		// Token: 0x04003332 RID: 13106
		private const string ResourceLogPathKey = "ResourceLogPath";

		// Token: 0x04003333 RID: 13107
		private const string ResourceLogEnabledKey = "ResourceLogEnabled";

		// Token: 0x04003334 RID: 13108
		private const string AttributionLogMaxAgeKey = "AttributionLogMaxAge";

		// Token: 0x04003335 RID: 13109
		private const string AttributionLogMaxDirectorySizeKey = "AttributionLogMaxDirectorySize";

		// Token: 0x04003336 RID: 13110
		private const string AttributionLogMaxFileSizeKey = "AttributionLogMaxFileSize";

		// Token: 0x04003337 RID: 13111
		private const string AttributionLogPathKey = "AttributionLogPath";

		// Token: 0x04003338 RID: 13112
		private const string AttributionLogEnabledKey = "AttributionLogEnabled";

		// Token: 0x04003339 RID: 13113
		private const string MaxReceiveTlsRatePerMinuteKey = "MaxReceiveTlsRatePerMinute";
	}
}
