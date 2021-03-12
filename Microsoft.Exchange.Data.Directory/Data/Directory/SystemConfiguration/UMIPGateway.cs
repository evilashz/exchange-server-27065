using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200060F RID: 1551
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class UMIPGateway : ADConfigurationObject
	{
		// Token: 0x17001835 RID: 6197
		// (get) Token: 0x06004954 RID: 18772 RVA: 0x0010FCBE File Offset: 0x0010DEBE
		internal override ADObjectSchema Schema
		{
			get
			{
				return UMIPGateway.schema;
			}
		}

		// Token: 0x17001836 RID: 6198
		// (get) Token: 0x06004955 RID: 18773 RVA: 0x0010FCC5 File Offset: 0x0010DEC5
		internal override string MostDerivedObjectClass
		{
			get
			{
				return UMIPGateway.mostDerivedClass;
			}
		}

		// Token: 0x17001837 RID: 6199
		// (get) Token: 0x06004956 RID: 18774 RVA: 0x0010FCCC File Offset: 0x0010DECC
		internal override ADObjectId ParentPath
		{
			get
			{
				return UMIPGateway.parentPath;
			}
		}

		// Token: 0x17001838 RID: 6200
		// (get) Token: 0x06004957 RID: 18775 RVA: 0x0010FCD3 File Offset: 0x0010DED3
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17001839 RID: 6201
		// (get) Token: 0x06004958 RID: 18776 RVA: 0x0010FCDA File Offset: 0x0010DEDA
		internal override bool ExchangeVersionUpgradeSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700183A RID: 6202
		// (get) Token: 0x06004959 RID: 18777 RVA: 0x0010FCDD File Offset: 0x0010DEDD
		// (set) Token: 0x0600495A RID: 18778 RVA: 0x0010FCEF File Offset: 0x0010DEEF
		[Parameter(Mandatory = false)]
		public UMSmartHost Address
		{
			get
			{
				return (UMSmartHost)this[UMIPGatewaySchema.Address];
			}
			set
			{
				this[UMIPGatewaySchema.Address] = value;
			}
		}

		// Token: 0x1700183B RID: 6203
		// (get) Token: 0x0600495B RID: 18779 RVA: 0x0010FCFD File Offset: 0x0010DEFD
		// (set) Token: 0x0600495C RID: 18780 RVA: 0x0010FD0F File Offset: 0x0010DF0F
		[Parameter(Mandatory = false)]
		public bool OutcallsAllowed
		{
			get
			{
				return (bool)this[UMIPGatewaySchema.OutcallsAllowed];
			}
			set
			{
				this[UMIPGatewaySchema.OutcallsAllowed] = value;
			}
		}

		// Token: 0x1700183C RID: 6204
		// (get) Token: 0x0600495D RID: 18781 RVA: 0x0010FD22 File Offset: 0x0010DF22
		// (set) Token: 0x0600495E RID: 18782 RVA: 0x0010FD34 File Offset: 0x0010DF34
		[Parameter(Mandatory = false)]
		public GatewayStatus Status
		{
			get
			{
				return (GatewayStatus)this[UMIPGatewaySchema.Status];
			}
			set
			{
				this[UMIPGatewaySchema.Status] = value;
			}
		}

		// Token: 0x1700183D RID: 6205
		// (get) Token: 0x0600495F RID: 18783 RVA: 0x0010FD47 File Offset: 0x0010DF47
		// (set) Token: 0x06004960 RID: 18784 RVA: 0x0010FD59 File Offset: 0x0010DF59
		[Parameter(Mandatory = false)]
		public int Port
		{
			get
			{
				return (int)this[UMIPGatewaySchema.Port];
			}
			set
			{
				this[UMIPGatewaySchema.Port] = value;
			}
		}

		// Token: 0x1700183E RID: 6206
		// (get) Token: 0x06004961 RID: 18785 RVA: 0x0010FD6C File Offset: 0x0010DF6C
		// (set) Token: 0x06004962 RID: 18786 RVA: 0x0010FD7E File Offset: 0x0010DF7E
		[Parameter(Mandatory = false)]
		public bool Simulator
		{
			get
			{
				return (bool)this[UMIPGatewaySchema.Simulator];
			}
			set
			{
				this[UMIPGatewaySchema.Simulator] = value;
			}
		}

		// Token: 0x1700183F RID: 6207
		// (get) Token: 0x06004963 RID: 18787 RVA: 0x0010FD91 File Offset: 0x0010DF91
		// (set) Token: 0x06004964 RID: 18788 RVA: 0x0010FDA3 File Offset: 0x0010DFA3
		[Parameter(Mandatory = false)]
		public IPAddressFamily IPAddressFamily
		{
			get
			{
				return (IPAddressFamily)this[UMIPGatewaySchema.IPAddressFamily];
			}
			set
			{
				this[UMIPGatewaySchema.IPAddressFamily] = value;
			}
		}

		// Token: 0x17001840 RID: 6208
		// (get) Token: 0x06004965 RID: 18789 RVA: 0x0010FDB6 File Offset: 0x0010DFB6
		// (set) Token: 0x06004966 RID: 18790 RVA: 0x0010FDC8 File Offset: 0x0010DFC8
		[Parameter(Mandatory = false)]
		public bool DelayedSourcePartyInfoEnabled
		{
			get
			{
				return (bool)this[UMIPGatewaySchema.DelayedSourcePartyInfoEnabled];
			}
			set
			{
				this[UMIPGatewaySchema.DelayedSourcePartyInfoEnabled] = value;
			}
		}

		// Token: 0x17001841 RID: 6209
		// (get) Token: 0x06004967 RID: 18791 RVA: 0x0010FDDB File Offset: 0x0010DFDB
		// (set) Token: 0x06004968 RID: 18792 RVA: 0x0010FDED File Offset: 0x0010DFED
		[Parameter(Mandatory = false)]
		public bool MessageWaitingIndicatorAllowed
		{
			get
			{
				return (bool)this[UMIPGatewaySchema.MessageWaitingIndicatorAllowed];
			}
			set
			{
				this[UMIPGatewaySchema.MessageWaitingIndicatorAllowed] = value;
			}
		}

		// Token: 0x17001842 RID: 6210
		// (get) Token: 0x06004969 RID: 18793 RVA: 0x0010FE00 File Offset: 0x0010E000
		// (set) Token: 0x0600496A RID: 18794 RVA: 0x0010FE12 File Offset: 0x0010E012
		public MultiValuedProperty<UMHuntGroup> HuntGroups
		{
			get
			{
				return (MultiValuedProperty<UMHuntGroup>)this[UMIPGatewaySchema.HuntGroups];
			}
			private set
			{
				this[UMIPGatewaySchema.HuntGroups] = value;
			}
		}

		// Token: 0x17001843 RID: 6211
		// (get) Token: 0x0600496B RID: 18795 RVA: 0x0010FE20 File Offset: 0x0010E020
		// (set) Token: 0x0600496C RID: 18796 RVA: 0x0010FE32 File Offset: 0x0010E032
		public UMGlobalCallRoutingScheme GlobalCallRoutingScheme
		{
			get
			{
				return (UMGlobalCallRoutingScheme)this[UMIPGatewaySchema.GlobalCallRoutingScheme];
			}
			set
			{
				this[UMIPGatewaySchema.GlobalCallRoutingScheme] = value;
			}
		}

		// Token: 0x17001844 RID: 6212
		// (get) Token: 0x0600496D RID: 18797 RVA: 0x0010FE45 File Offset: 0x0010E045
		// (set) Token: 0x0600496E RID: 18798 RVA: 0x0010FE4D File Offset: 0x0010E04D
		public string ForwardingAddress
		{
			get
			{
				return this.forwardingAddress;
			}
			internal set
			{
				this.forwardingAddress = value;
			}
		}

		// Token: 0x0600496F RID: 18799 RVA: 0x0010FE58 File Offset: 0x0010E058
		internal override void Initialize()
		{
			base.Initialize();
			IConfigurationSession session = base.Session;
			UMHuntGroup[] values = session.FindPaged<UMHuntGroup>(base.Id, QueryScope.OneLevel, null, null, 0).ReadAllPages();
			this.HuntGroups = new MultiValuedProperty<UMHuntGroup>(true, null, values);
		}

		// Token: 0x040032DD RID: 13021
		private static UMIPGatewaySchema schema = ObjectSchema.GetInstance<UMIPGatewaySchema>();

		// Token: 0x040032DE RID: 13022
		private static string mostDerivedClass = "msExchUMIPGateway";

		// Token: 0x040032DF RID: 13023
		private static ADObjectId parentPath = new ADObjectId("CN=UM IPGateway Container");

		// Token: 0x040032E0 RID: 13024
		private string forwardingAddress = string.Empty;
	}
}
