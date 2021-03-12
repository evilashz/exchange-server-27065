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
	// Token: 0x020009CE RID: 2510
	public class SetTransportServiceBase : SetTopologySystemConfigurationObjectTask<ServerIdParameter, TransportServer, Server>
	{
		// Token: 0x17001AC2 RID: 6850
		// (get) Token: 0x060059AA RID: 22954 RVA: 0x00178492 File Offset: 0x00176692
		// (set) Token: 0x060059AB RID: 22955 RVA: 0x001784A9 File Offset: 0x001766A9
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan QueueLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)base.Fields["QueueLogMaxAge"];
			}
			set
			{
				base.Fields["QueueLogMaxAge"] = value;
			}
		}

		// Token: 0x17001AC3 RID: 6851
		// (get) Token: 0x060059AC RID: 22956 RVA: 0x001784C1 File Offset: 0x001766C1
		// (set) Token: 0x060059AD RID: 22957 RVA: 0x001784D8 File Offset: 0x001766D8
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> QueueLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)base.Fields["QueueLogMaxDirectorySize"];
			}
			set
			{
				base.Fields["QueueLogMaxDirectorySize"] = value;
			}
		}

		// Token: 0x17001AC4 RID: 6852
		// (get) Token: 0x060059AE RID: 22958 RVA: 0x001784F0 File Offset: 0x001766F0
		// (set) Token: 0x060059AF RID: 22959 RVA: 0x00178507 File Offset: 0x00176707
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> QueueLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)base.Fields["QueueLogMaxFileSize"];
			}
			set
			{
				base.Fields["QueueLogMaxFileSize"] = value;
			}
		}

		// Token: 0x17001AC5 RID: 6853
		// (get) Token: 0x060059B0 RID: 22960 RVA: 0x0017851F File Offset: 0x0017671F
		// (set) Token: 0x060059B1 RID: 22961 RVA: 0x00178536 File Offset: 0x00176736
		[Parameter(Mandatory = false)]
		public LocalLongFullPath QueueLogPath
		{
			get
			{
				return (LocalLongFullPath)base.Fields["QueueLogPath"];
			}
			set
			{
				base.Fields["QueueLogPath"] = value;
			}
		}

		// Token: 0x17001AC6 RID: 6854
		// (get) Token: 0x060059B2 RID: 22962 RVA: 0x00178549 File Offset: 0x00176749
		// (set) Token: 0x060059B3 RID: 22963 RVA: 0x00178560 File Offset: 0x00176760
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan WlmLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)base.Fields["WlmLogMaxAge"];
			}
			set
			{
				base.Fields["WlmLogMaxAge"] = value;
			}
		}

		// Token: 0x17001AC7 RID: 6855
		// (get) Token: 0x060059B4 RID: 22964 RVA: 0x00178578 File Offset: 0x00176778
		// (set) Token: 0x060059B5 RID: 22965 RVA: 0x0017858F File Offset: 0x0017678F
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> WlmLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)base.Fields["WlmLogMaxDirectorySize"];
			}
			set
			{
				base.Fields["WlmLogMaxDirectorySize"] = value;
			}
		}

		// Token: 0x17001AC8 RID: 6856
		// (get) Token: 0x060059B6 RID: 22966 RVA: 0x001785A7 File Offset: 0x001767A7
		// (set) Token: 0x060059B7 RID: 22967 RVA: 0x001785BE File Offset: 0x001767BE
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> WlmLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)base.Fields["WlmLogMaxFileSize"];
			}
			set
			{
				base.Fields["WlmLogMaxFileSize"] = value;
			}
		}

		// Token: 0x17001AC9 RID: 6857
		// (get) Token: 0x060059B8 RID: 22968 RVA: 0x001785D6 File Offset: 0x001767D6
		// (set) Token: 0x060059B9 RID: 22969 RVA: 0x001785ED File Offset: 0x001767ED
		[Parameter(Mandatory = false)]
		public LocalLongFullPath WlmLogPath
		{
			get
			{
				return (LocalLongFullPath)base.Fields["WlmLogPath"];
			}
			set
			{
				base.Fields["WlmLogPath"] = value;
			}
		}

		// Token: 0x17001ACA RID: 6858
		// (get) Token: 0x060059BA RID: 22970 RVA: 0x00178600 File Offset: 0x00176800
		// (set) Token: 0x060059BB RID: 22971 RVA: 0x00178617 File Offset: 0x00176817
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

		// Token: 0x17001ACB RID: 6859
		// (get) Token: 0x060059BC RID: 22972 RVA: 0x0017862F File Offset: 0x0017682F
		// (set) Token: 0x060059BD RID: 22973 RVA: 0x00178646 File Offset: 0x00176846
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

		// Token: 0x17001ACC RID: 6860
		// (get) Token: 0x060059BE RID: 22974 RVA: 0x0017865E File Offset: 0x0017685E
		// (set) Token: 0x060059BF RID: 22975 RVA: 0x00178675 File Offset: 0x00176875
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

		// Token: 0x17001ACD RID: 6861
		// (get) Token: 0x060059C0 RID: 22976 RVA: 0x0017868D File Offset: 0x0017688D
		// (set) Token: 0x060059C1 RID: 22977 RVA: 0x001786A4 File Offset: 0x001768A4
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

		// Token: 0x17001ACE RID: 6862
		// (get) Token: 0x060059C2 RID: 22978 RVA: 0x001786B7 File Offset: 0x001768B7
		// (set) Token: 0x060059C3 RID: 22979 RVA: 0x001786CE File Offset: 0x001768CE
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

		// Token: 0x17001ACF RID: 6863
		// (get) Token: 0x060059C4 RID: 22980 RVA: 0x001786E6 File Offset: 0x001768E6
		// (set) Token: 0x060059C5 RID: 22981 RVA: 0x001786FD File Offset: 0x001768FD
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan FlowControlLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)base.Fields["FlowControlLogMaxAge"];
			}
			set
			{
				base.Fields["FlowControlLogMaxAge"] = value;
			}
		}

		// Token: 0x17001AD0 RID: 6864
		// (get) Token: 0x060059C6 RID: 22982 RVA: 0x00178715 File Offset: 0x00176915
		// (set) Token: 0x060059C7 RID: 22983 RVA: 0x0017872C File Offset: 0x0017692C
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> FlowControlLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)base.Fields["FlowControlLogMaxDirectorySize"];
			}
			set
			{
				base.Fields["FlowControlLogMaxDirectorySize"] = value;
			}
		}

		// Token: 0x17001AD1 RID: 6865
		// (get) Token: 0x060059C8 RID: 22984 RVA: 0x00178744 File Offset: 0x00176944
		// (set) Token: 0x060059C9 RID: 22985 RVA: 0x0017875B File Offset: 0x0017695B
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> FlowControlLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)base.Fields["FlowControlLogMaxFileSize"];
			}
			set
			{
				base.Fields["FlowControlLogMaxFileSize"] = value;
			}
		}

		// Token: 0x17001AD2 RID: 6866
		// (get) Token: 0x060059CA RID: 22986 RVA: 0x00178773 File Offset: 0x00176973
		// (set) Token: 0x060059CB RID: 22987 RVA: 0x0017878A File Offset: 0x0017698A
		[Parameter(Mandatory = false)]
		public LocalLongFullPath FlowControlLogPath
		{
			get
			{
				return (LocalLongFullPath)base.Fields["FlowControlLogPath"];
			}
			set
			{
				base.Fields["FlowControlLogPath"] = value;
			}
		}

		// Token: 0x17001AD3 RID: 6867
		// (get) Token: 0x060059CC RID: 22988 RVA: 0x0017879D File Offset: 0x0017699D
		// (set) Token: 0x060059CD RID: 22989 RVA: 0x001787B4 File Offset: 0x001769B4
		[Parameter(Mandatory = false)]
		public bool FlowControlLogEnabled
		{
			get
			{
				return (bool)base.Fields["FlowControlLogEnabled"];
			}
			set
			{
				base.Fields["FlowControlLogEnabled"] = value;
			}
		}

		// Token: 0x17001AD4 RID: 6868
		// (get) Token: 0x060059CE RID: 22990 RVA: 0x001787CC File Offset: 0x001769CC
		// (set) Token: 0x060059CF RID: 22991 RVA: 0x001787E3 File Offset: 0x001769E3
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan ProcessingSchedulerLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)base.Fields["ProcessingSchedulerLogMaxAge"];
			}
			set
			{
				base.Fields["ProcessingSchedulerLogMaxAge"] = value;
			}
		}

		// Token: 0x17001AD5 RID: 6869
		// (get) Token: 0x060059D0 RID: 22992 RVA: 0x001787FB File Offset: 0x001769FB
		// (set) Token: 0x060059D1 RID: 22993 RVA: 0x00178812 File Offset: 0x00176A12
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ProcessingSchedulerLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)base.Fields["ProcessingSchedulerLogMaxDirectorySize"];
			}
			set
			{
				base.Fields["ProcessingSchedulerLogMaxDirectorySize"] = value;
			}
		}

		// Token: 0x17001AD6 RID: 6870
		// (get) Token: 0x060059D2 RID: 22994 RVA: 0x0017882A File Offset: 0x00176A2A
		// (set) Token: 0x060059D3 RID: 22995 RVA: 0x00178841 File Offset: 0x00176A41
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ProcessingSchedulerLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)base.Fields["ProcessingSchedulerLogMaxFileSize"];
			}
			set
			{
				base.Fields["ProcessingSchedulerLogMaxFileSize"] = value;
			}
		}

		// Token: 0x17001AD7 RID: 6871
		// (get) Token: 0x060059D4 RID: 22996 RVA: 0x00178859 File Offset: 0x00176A59
		// (set) Token: 0x060059D5 RID: 22997 RVA: 0x00178870 File Offset: 0x00176A70
		[Parameter(Mandatory = false)]
		public LocalLongFullPath ProcessingSchedulerLogPath
		{
			get
			{
				return (LocalLongFullPath)base.Fields["ProcessingSchedulerLogPath"];
			}
			set
			{
				base.Fields["ProcessingSchedulerLogPath"] = value;
			}
		}

		// Token: 0x17001AD8 RID: 6872
		// (get) Token: 0x060059D6 RID: 22998 RVA: 0x00178883 File Offset: 0x00176A83
		// (set) Token: 0x060059D7 RID: 22999 RVA: 0x0017889A File Offset: 0x00176A9A
		[Parameter(Mandatory = false)]
		public bool ProcessingSchedulerLogEnabled
		{
			get
			{
				return (bool)base.Fields["ProcessingSchedulerLogEnabled"];
			}
			set
			{
				base.Fields["ProcessingSchedulerLogEnabled"] = value;
			}
		}

		// Token: 0x17001AD9 RID: 6873
		// (get) Token: 0x060059D8 RID: 23000 RVA: 0x001788B2 File Offset: 0x00176AB2
		// (set) Token: 0x060059D9 RID: 23001 RVA: 0x001788C9 File Offset: 0x00176AC9
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

		// Token: 0x17001ADA RID: 6874
		// (get) Token: 0x060059DA RID: 23002 RVA: 0x001788E1 File Offset: 0x00176AE1
		// (set) Token: 0x060059DB RID: 23003 RVA: 0x001788F8 File Offset: 0x00176AF8
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

		// Token: 0x17001ADB RID: 6875
		// (get) Token: 0x060059DC RID: 23004 RVA: 0x00178910 File Offset: 0x00176B10
		// (set) Token: 0x060059DD RID: 23005 RVA: 0x00178927 File Offset: 0x00176B27
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

		// Token: 0x17001ADC RID: 6876
		// (get) Token: 0x060059DE RID: 23006 RVA: 0x0017893F File Offset: 0x00176B3F
		// (set) Token: 0x060059DF RID: 23007 RVA: 0x00178956 File Offset: 0x00176B56
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

		// Token: 0x17001ADD RID: 6877
		// (get) Token: 0x060059E0 RID: 23008 RVA: 0x00178969 File Offset: 0x00176B69
		// (set) Token: 0x060059E1 RID: 23009 RVA: 0x00178980 File Offset: 0x00176B80
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

		// Token: 0x17001ADE RID: 6878
		// (get) Token: 0x060059E2 RID: 23010 RVA: 0x00178998 File Offset: 0x00176B98
		// (set) Token: 0x060059E3 RID: 23011 RVA: 0x001789AF File Offset: 0x00176BAF
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

		// Token: 0x17001ADF RID: 6879
		// (get) Token: 0x060059E4 RID: 23012 RVA: 0x001789C7 File Offset: 0x00176BC7
		// (set) Token: 0x060059E5 RID: 23013 RVA: 0x001789DE File Offset: 0x00176BDE
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

		// Token: 0x17001AE0 RID: 6880
		// (get) Token: 0x060059E6 RID: 23014 RVA: 0x001789F6 File Offset: 0x00176BF6
		// (set) Token: 0x060059E7 RID: 23015 RVA: 0x00178A0D File Offset: 0x00176C0D
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

		// Token: 0x17001AE1 RID: 6881
		// (get) Token: 0x060059E8 RID: 23016 RVA: 0x00178A25 File Offset: 0x00176C25
		// (set) Token: 0x060059E9 RID: 23017 RVA: 0x00178A3C File Offset: 0x00176C3C
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

		// Token: 0x17001AE2 RID: 6882
		// (get) Token: 0x060059EA RID: 23018 RVA: 0x00178A4F File Offset: 0x00176C4F
		// (set) Token: 0x060059EB RID: 23019 RVA: 0x00178A66 File Offset: 0x00176C66
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

		// Token: 0x17001AE3 RID: 6883
		// (get) Token: 0x060059EC RID: 23020 RVA: 0x00178A7E File Offset: 0x00176C7E
		// (set) Token: 0x060059ED RID: 23021 RVA: 0x00178A95 File Offset: 0x00176C95
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan JournalLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)base.Fields["JournalLogMaxAge"];
			}
			set
			{
				base.Fields["JournalLogMaxAge"] = value;
			}
		}

		// Token: 0x17001AE4 RID: 6884
		// (get) Token: 0x060059EE RID: 23022 RVA: 0x00178AAD File Offset: 0x00176CAD
		// (set) Token: 0x060059EF RID: 23023 RVA: 0x00178AC4 File Offset: 0x00176CC4
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> JournalLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)base.Fields["JournalLogMaxDirectorySize"];
			}
			set
			{
				base.Fields["JournalLogMaxDirectorySize"] = value;
			}
		}

		// Token: 0x17001AE5 RID: 6885
		// (get) Token: 0x060059F0 RID: 23024 RVA: 0x00178ADC File Offset: 0x00176CDC
		// (set) Token: 0x060059F1 RID: 23025 RVA: 0x00178AF3 File Offset: 0x00176CF3
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> JournalLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)base.Fields["JournalLogMaxFileSize"];
			}
			set
			{
				base.Fields["JournalLogMaxFileSize"] = value;
			}
		}

		// Token: 0x17001AE6 RID: 6886
		// (get) Token: 0x060059F2 RID: 23026 RVA: 0x00178B0B File Offset: 0x00176D0B
		// (set) Token: 0x060059F3 RID: 23027 RVA: 0x00178B22 File Offset: 0x00176D22
		[Parameter(Mandatory = false)]
		public LocalLongFullPath JournalLogPath
		{
			get
			{
				return (LocalLongFullPath)base.Fields["JournalLogPath"];
			}
			set
			{
				base.Fields["JournalLogPath"] = value;
			}
		}

		// Token: 0x17001AE7 RID: 6887
		// (get) Token: 0x060059F4 RID: 23028 RVA: 0x00178B35 File Offset: 0x00176D35
		// (set) Token: 0x060059F5 RID: 23029 RVA: 0x00178B4C File Offset: 0x00176D4C
		[Parameter(Mandatory = false)]
		public bool JournalLogEnabled
		{
			get
			{
				return (bool)base.Fields["JournalLogEnabled"];
			}
			set
			{
				base.Fields["JournalLogEnabled"] = value;
			}
		}

		// Token: 0x17001AE8 RID: 6888
		// (get) Token: 0x060059F6 RID: 23030 RVA: 0x00178B64 File Offset: 0x00176D64
		// (set) Token: 0x060059F7 RID: 23031 RVA: 0x00178B7B File Offset: 0x00176D7B
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan TransportMaintenanceLogMaxAge
		{
			get
			{
				return (EnhancedTimeSpan)base.Fields["TransportMaintenanceLogMaxAge"];
			}
			set
			{
				base.Fields["TransportMaintenanceLogMaxAge"] = value;
			}
		}

		// Token: 0x17001AE9 RID: 6889
		// (get) Token: 0x060059F8 RID: 23032 RVA: 0x00178B93 File Offset: 0x00176D93
		// (set) Token: 0x060059F9 RID: 23033 RVA: 0x00178BAA File Offset: 0x00176DAA
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> TransportMaintenanceLogMaxDirectorySize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)base.Fields["TransportMaintenanceLogMaxDirectorySize"];
			}
			set
			{
				base.Fields["TransportMaintenanceLogMaxDirectorySize"] = value;
			}
		}

		// Token: 0x17001AEA RID: 6890
		// (get) Token: 0x060059FA RID: 23034 RVA: 0x00178BC2 File Offset: 0x00176DC2
		// (set) Token: 0x060059FB RID: 23035 RVA: 0x00178BD9 File Offset: 0x00176DD9
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> TransportMaintenanceLogMaxFileSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)base.Fields["TransportMaintenanceLogMaxFileSize"];
			}
			set
			{
				base.Fields["TransportMaintenanceLogMaxFileSize"] = value;
			}
		}

		// Token: 0x17001AEB RID: 6891
		// (get) Token: 0x060059FC RID: 23036 RVA: 0x00178BF1 File Offset: 0x00176DF1
		// (set) Token: 0x060059FD RID: 23037 RVA: 0x00178C08 File Offset: 0x00176E08
		[Parameter(Mandatory = false)]
		public LocalLongFullPath TransportMaintenanceLogPath
		{
			get
			{
				return (LocalLongFullPath)base.Fields["TransportMaintenanceLogPath"];
			}
			set
			{
				base.Fields["TransportMaintenanceLogPath"] = value;
			}
		}

		// Token: 0x17001AEC RID: 6892
		// (get) Token: 0x060059FE RID: 23038 RVA: 0x00178C1B File Offset: 0x00176E1B
		// (set) Token: 0x060059FF RID: 23039 RVA: 0x00178C32 File Offset: 0x00176E32
		[Parameter(Mandatory = false)]
		public bool TransportMaintenanceLogEnabled
		{
			get
			{
				return (bool)base.Fields["TransportMaintenanceLogEnabled"];
			}
			set
			{
				base.Fields["TransportMaintenanceLogEnabled"] = value;
			}
		}

		// Token: 0x17001AED RID: 6893
		// (get) Token: 0x06005A00 RID: 23040 RVA: 0x00178C4A File Offset: 0x00176E4A
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetTransportServer(this.Identity.ToString());
			}
		}

		// Token: 0x06005A01 RID: 23041 RVA: 0x00178C5C File Offset: 0x00176E5C
		protected override IConfigurable PrepareDataObject()
		{
			Server server = (Server)base.PrepareDataObject();
			this.CheckServerRoles(server);
			return server;
		}

		// Token: 0x06005A02 RID: 23042 RVA: 0x00178C80 File Offset: 0x00176E80
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			Server server = (Server)dataObject;
			this.existingPipelineTracingEnabled = server.PipelineTracingEnabled;
			base.StampChangesOn(dataObject);
		}

		// Token: 0x06005A03 RID: 23043 RVA: 0x00178CA8 File Offset: 0x00176EA8
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (this.Instance.IsModified(ADObjectSchema.Name))
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorServerNameModified), ErrorCategory.InvalidOperation, this.Identity);
			}
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (!this.DataObject.IsHubTransportServer && !this.DataObject.IsEdgeServer && !this.DataObject.IsFrontendTransportServer)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorTaskRunningLocation), ErrorCategory.InvalidOperation, null);
			}
			if (this.DataObject.IsModified(ADTransportServerSchema.UseDowngradedExchangeServerAuth))
			{
				if (this.DataObject.IsEdgeServer && this.DataObject.UseDowngradedExchangeServerAuth)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorCannotSetDowngradedExchangeServerAuthOnEdge), ErrorCategory.InvalidOperation, this.Identity);
				}
				if (!this.DataObject.UseDowngradedExchangeServerAuth)
				{
					foreach (ReceiveConnector receiveConnector in base.DataSession.FindPaged<ReceiveConnector>(null, this.DataObject.Identity, true, null, 0))
					{
						if (receiveConnector.SuppressXAnonymousTls)
						{
							base.WriteError(new InvalidOperationException(Strings.ErrorCannotUnsetDowngradedExchangeServerAuthIfReceiveConnectorHasSuppressXAnonmyousTlsSet(receiveConnector.Name)), ErrorCategory.InvalidOperation, this.Identity);
						}
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06005A04 RID: 23044 RVA: 0x00178E0C File Offset: 0x0017700C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (base.Fields.IsModified("QueueLogMaxAge"))
			{
				this.DataObject.QueueLogMaxAge = this.QueueLogMaxAge;
			}
			if (base.Fields.IsModified("QueueLogMaxDirectorySize"))
			{
				this.DataObject.QueueLogMaxDirectorySize = this.QueueLogMaxDirectorySize;
			}
			if (base.Fields.IsModified("QueueLogMaxFileSize"))
			{
				this.DataObject.QueueLogMaxFileSize = this.QueueLogMaxFileSize;
			}
			if (base.Fields.IsModified("QueueLogPath"))
			{
				this.DataObject.QueueLogPath = this.QueueLogPath;
			}
			if (base.Fields.IsModified("WlmLogMaxAge"))
			{
				this.DataObject.WlmLogMaxAge = this.WlmLogMaxAge;
			}
			if (base.Fields.IsModified("WlmLogMaxDirectorySize"))
			{
				this.DataObject.WlmLogMaxDirectorySize = this.WlmLogMaxDirectorySize;
			}
			if (base.Fields.IsModified("WlmLogMaxFileSize"))
			{
				this.DataObject.WlmLogMaxFileSize = this.WlmLogMaxFileSize;
			}
			if (base.Fields.IsModified("WlmLogPath"))
			{
				this.DataObject.WlmLogPath = this.WlmLogPath;
			}
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
			if (base.Fields.IsModified("FlowControlLogMaxAge"))
			{
				this.DataObject.FlowControlLogMaxAge = this.FlowControlLogMaxAge;
			}
			if (base.Fields.IsModified("FlowControlLogMaxDirectorySize"))
			{
				this.DataObject.FlowControlLogMaxDirectorySize = this.FlowControlLogMaxDirectorySize;
			}
			if (base.Fields.IsModified("FlowControlLogMaxFileSize"))
			{
				this.DataObject.FlowControlLogMaxFileSize = this.FlowControlLogMaxFileSize;
			}
			if (base.Fields.IsModified("FlowControlLogPath"))
			{
				this.DataObject.FlowControlLogPath = this.FlowControlLogPath;
			}
			if (base.Fields.IsModified("FlowControlLogEnabled"))
			{
				this.DataObject.FlowControlLogEnabled = this.FlowControlLogEnabled;
			}
			if (base.Fields.IsModified("ProcessingSchedulerLogMaxAge"))
			{
				this.DataObject.ProcessingSchedulerLogMaxAge = this.ProcessingSchedulerLogMaxAge;
			}
			if (base.Fields.IsModified("ProcessingSchedulerLogMaxDirectorySize"))
			{
				this.DataObject.ProcessingSchedulerLogMaxDirectorySize = this.ProcessingSchedulerLogMaxDirectorySize;
			}
			if (base.Fields.IsModified("ProcessingSchedulerLogMaxFileSize"))
			{
				this.DataObject.ProcessingSchedulerLogMaxFileSize = this.ProcessingSchedulerLogMaxFileSize;
			}
			if (base.Fields.IsModified("ProcessingSchedulerLogPath"))
			{
				this.DataObject.ProcessingSchedulerLogPath = this.ProcessingSchedulerLogPath;
			}
			if (base.Fields.IsModified("ProcessingSchedulerLogEnabled"))
			{
				this.DataObject.ProcessingSchedulerLogEnabled = this.ProcessingSchedulerLogEnabled;
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
			if (base.Fields.IsModified("JournalLogMaxAge"))
			{
				this.DataObject.JournalLogMaxAge = this.JournalLogMaxAge;
			}
			if (base.Fields.IsModified("JournalLogMaxDirectorySize"))
			{
				this.DataObject.JournalLogMaxDirectorySize = this.JournalLogMaxDirectorySize;
			}
			if (base.Fields.IsModified("JournalLogMaxFileSize"))
			{
				this.DataObject.JournalLogMaxFileSize = this.JournalLogMaxFileSize;
			}
			if (base.Fields.IsModified("JournalLogPath"))
			{
				this.DataObject.JournalLogPath = this.JournalLogPath;
			}
			if (base.Fields.IsModified("JournalLogEnabled"))
			{
				this.DataObject.JournalLogEnabled = this.JournalLogEnabled;
			}
			if (base.Fields.IsModified("TransportMaintenanceLogMaxAge"))
			{
				this.DataObject.TransportMaintenanceLogMaxAge = this.TransportMaintenanceLogMaxAge;
			}
			if (base.Fields.IsModified("TransportMaintenanceLogMaxDirectorySize"))
			{
				this.DataObject.TransportMaintenanceLogMaxDirectorySize = this.TransportMaintenanceLogMaxDirectorySize;
			}
			if (base.Fields.IsModified("TransportMaintenanceLogMaxFileSize"))
			{
				this.DataObject.TransportMaintenanceLogMaxFileSize = this.TransportMaintenanceLogMaxFileSize;
			}
			if (base.Fields.IsModified("TransportMaintenanceLogPath"))
			{
				this.DataObject.TransportMaintenanceLogPath = this.TransportMaintenanceLogPath;
			}
			if (base.Fields.IsModified("TransportMaintenanceLogEnabled"))
			{
				this.DataObject.TransportMaintenanceLogEnabled = this.TransportMaintenanceLogEnabled;
			}
			bool flag = base.IsObjectStateChanged() && this.DataObject.IsModified(TransportServerSchema.PipelineTracingPath);
			base.InternalProcessRecord();
			if (this.DataObject.PipelineTracingEnabled && !this.existingPipelineTracingEnabled)
			{
				this.WriteWarning(Strings.WarningSecurePipelineTracingPath);
			}
			else if ((!this.DataObject.PipelineTracingEnabled && this.existingPipelineTracingEnabled) || flag)
			{
				this.WriteWarning(Strings.WarningCleanExistingPipelineTracingContent);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06005A05 RID: 23045 RVA: 0x0017946C File Offset: 0x0017766C
		private void CheckServerRoles(Server server)
		{
			ITopologyConfigurationSession topologyConfigurationSession = (ITopologyConfigurationSession)base.DataSession;
			Server server2;
			try
			{
				server2 = topologyConfigurationSession.ReadLocalServer();
			}
			catch (TransientException exception)
			{
				base.WriteError(exception, ErrorCategory.ResourceUnavailable, null);
				return;
			}
			if (server2 == null || !server2.IsEdgeServer)
			{
				if (server.IsEdgeServer)
				{
					base.WriteError(new CannotSetEdgeTransportServerOnAdException(), ErrorCategory.InvalidOperation, null);
					return;
				}
			}
			else if (server2.IsEdgeServer && !server.IsEdgeServer)
			{
				base.WriteError(new CannotSetHubTransportServerOnAdamException(), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x0400334B RID: 13131
		private const string QueueLogMaxAgeKey = "QueueLogMaxAge";

		// Token: 0x0400334C RID: 13132
		private const string QueueLogMaxDirectorySizeKey = "QueueLogMaxDirectorySize";

		// Token: 0x0400334D RID: 13133
		private const string QueueLogMaxFileSizeKey = "QueueLogMaxFileSize";

		// Token: 0x0400334E RID: 13134
		private const string QueueLogPathKey = "QueueLogPath";

		// Token: 0x0400334F RID: 13135
		private const string WlmLogMaxAgeKey = "WlmLogMaxAge";

		// Token: 0x04003350 RID: 13136
		private const string WlmLogMaxDirectorySizeKey = "WlmLogMaxDirectorySize";

		// Token: 0x04003351 RID: 13137
		private const string WlmLogMaxFileSizeKey = "WlmLogMaxFileSize";

		// Token: 0x04003352 RID: 13138
		private const string WlmLogPathKey = "WlmLogPath";

		// Token: 0x04003353 RID: 13139
		private const string AgentLogMaxAgeKey = "AgentLogMaxAge";

		// Token: 0x04003354 RID: 13140
		private const string AgentLogMaxDirectorySizeKey = "AgentLogMaxDirectorySize";

		// Token: 0x04003355 RID: 13141
		private const string AgentLogMaxFileSizeKey = "AgentLogMaxFileSize";

		// Token: 0x04003356 RID: 13142
		private const string AgentLogPathKey = "AgentLogPath";

		// Token: 0x04003357 RID: 13143
		private const string AgentLogEnabledKey = "AgentLogEnabled";

		// Token: 0x04003358 RID: 13144
		private const string FlowControlLogMaxAgeKey = "FlowControlLogMaxAge";

		// Token: 0x04003359 RID: 13145
		private const string FlowControlLogMaxDirectorySizeKey = "FlowControlLogMaxDirectorySize";

		// Token: 0x0400335A RID: 13146
		private const string FlowControlLogMaxFileSizeKey = "FlowControlLogMaxFileSize";

		// Token: 0x0400335B RID: 13147
		private const string FlowControlLogPathKey = "FlowControlLogPath";

		// Token: 0x0400335C RID: 13148
		private const string FlowControlLogEnabledKey = "FlowControlLogEnabled";

		// Token: 0x0400335D RID: 13149
		private const string ProcessingSchedulerLogMaxAgeKey = "ProcessingSchedulerLogMaxAge";

		// Token: 0x0400335E RID: 13150
		private const string ProcessingSchedulerLogMaxDirectorySizeKey = "ProcessingSchedulerLogMaxDirectorySize";

		// Token: 0x0400335F RID: 13151
		private const string ProcessingSchedulerLogMaxFileSizeKey = "ProcessingSchedulerLogMaxFileSize";

		// Token: 0x04003360 RID: 13152
		private const string ProcessingSchedulerLogPathKey = "ProcessingSchedulerLogPath";

		// Token: 0x04003361 RID: 13153
		private const string ProcessingSchedulerLogEnabledKey = "ProcessingSchedulerLogEnabled";

		// Token: 0x04003362 RID: 13154
		private const string ResourceLogMaxAgeKey = "ResourceLogMaxAge";

		// Token: 0x04003363 RID: 13155
		private const string ResourceLogMaxDirectorySizeKey = "ResourceLogMaxDirectorySize";

		// Token: 0x04003364 RID: 13156
		private const string ResourceLogMaxFileSizeKey = "ResourceLogMaxFileSize";

		// Token: 0x04003365 RID: 13157
		private const string ResourceLogPathKey = "ResourceLogPath";

		// Token: 0x04003366 RID: 13158
		private const string ResourceLogEnabledKey = "ResourceLogEnabled";

		// Token: 0x04003367 RID: 13159
		private const string DnsLogMaxAgeKey = "DnsLogMaxAge";

		// Token: 0x04003368 RID: 13160
		private const string DnsLogMaxDirectorySizeKey = "DnsLogMaxDirectorySize";

		// Token: 0x04003369 RID: 13161
		private const string DnsLogMaxFileSizeKey = "DnsLogMaxFileSize";

		// Token: 0x0400336A RID: 13162
		private const string DnsLogPathKey = "DnsLogPath";

		// Token: 0x0400336B RID: 13163
		private const string DnsLogEnabledKey = "DnsLogEnabled";

		// Token: 0x0400336C RID: 13164
		private const string JournalLogMaxAgeKey = "JournalLogMaxAge";

		// Token: 0x0400336D RID: 13165
		private const string JournalLogMaxDirectorySizeKey = "JournalLogMaxDirectorySize";

		// Token: 0x0400336E RID: 13166
		private const string JournalLogMaxFileSizeKey = "JournalLogMaxFileSize";

		// Token: 0x0400336F RID: 13167
		private const string JournalLogPathKey = "JournalLogPath";

		// Token: 0x04003370 RID: 13168
		private const string JournalLogEnabledKey = "JournalLogEnabled";

		// Token: 0x04003371 RID: 13169
		private const string TransportMaintenanceLogMaxAgeKey = "TransportMaintenanceLogMaxAge";

		// Token: 0x04003372 RID: 13170
		private const string TransportMaintenanceLogMaxDirectorySizeKey = "TransportMaintenanceLogMaxDirectorySize";

		// Token: 0x04003373 RID: 13171
		private const string TransportMaintenanceLogMaxFileSizeKey = "TransportMaintenanceLogMaxFileSize";

		// Token: 0x04003374 RID: 13172
		private const string TransportMaintenanceLogPathKey = "TransportMaintenanceLogPath";

		// Token: 0x04003375 RID: 13173
		private const string TransportMaintenanceLogEnabledKey = "TransportMaintenanceLogEnabled";

		// Token: 0x04003376 RID: 13174
		private bool existingPipelineTracingEnabled;
	}
}
