using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000708 RID: 1800
	[Serializable]
	public class ExchangeServerRole : ADPresentationObject
	{
		// Token: 0x17001C43 RID: 7235
		// (get) Token: 0x060054CC RID: 21708 RVA: 0x0013277F File Offset: 0x0013097F
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return ExchangeServerRole.schema;
			}
		}

		// Token: 0x060054CD RID: 21709 RVA: 0x00132786 File Offset: 0x00130986
		public ExchangeServerRole()
		{
		}

		// Token: 0x060054CE RID: 21710 RVA: 0x0013278E File Offset: 0x0013098E
		public ExchangeServerRole(Server dataObject) : base(dataObject)
		{
		}

		// Token: 0x17001C44 RID: 7236
		// (get) Token: 0x060054CF RID: 21711 RVA: 0x00132797 File Offset: 0x00130997
		// (set) Token: 0x060054D0 RID: 21712 RVA: 0x001327A9 File Offset: 0x001309A9
		[Parameter(Mandatory = false)]
		public bool IsHubTransportServer
		{
			get
			{
				return (bool)this[ExchangeServerRoleSchema.IsHubTransportServer];
			}
			set
			{
				this[ExchangeServerRoleSchema.IsHubTransportServer] = value;
			}
		}

		// Token: 0x17001C45 RID: 7237
		// (get) Token: 0x060054D1 RID: 21713 RVA: 0x001327BC File Offset: 0x001309BC
		// (set) Token: 0x060054D2 RID: 21714 RVA: 0x001327CE File Offset: 0x001309CE
		[Parameter(Mandatory = false)]
		public bool IsClientAccessServer
		{
			get
			{
				return (bool)this[ExchangeServerRoleSchema.IsClientAccessServer];
			}
			set
			{
				this[ExchangeServerRoleSchema.IsClientAccessServer] = value;
			}
		}

		// Token: 0x17001C46 RID: 7238
		// (get) Token: 0x060054D3 RID: 21715 RVA: 0x001327E1 File Offset: 0x001309E1
		public bool IsExchange2007OrLater
		{
			get
			{
				return (bool)this[ExchangeServerRoleSchema.IsExchange2007OrLater];
			}
		}

		// Token: 0x17001C47 RID: 7239
		// (get) Token: 0x060054D4 RID: 21716 RVA: 0x001327F3 File Offset: 0x001309F3
		// (set) Token: 0x060054D5 RID: 21717 RVA: 0x00132805 File Offset: 0x00130A05
		[Parameter(Mandatory = false)]
		public bool IsEdgeServer
		{
			get
			{
				return (bool)this[ExchangeServerRoleSchema.IsEdgeServer];
			}
			set
			{
				this[ExchangeServerRoleSchema.IsEdgeServer] = value;
			}
		}

		// Token: 0x17001C48 RID: 7240
		// (get) Token: 0x060054D6 RID: 21718 RVA: 0x00132818 File Offset: 0x00130A18
		// (set) Token: 0x060054D7 RID: 21719 RVA: 0x0013282A File Offset: 0x00130A2A
		[Parameter(Mandatory = false)]
		public bool IsMailboxServer
		{
			get
			{
				return (bool)this[ExchangeServerRoleSchema.IsMailboxServer];
			}
			set
			{
				this[ExchangeServerRoleSchema.IsMailboxServer] = value;
			}
		}

		// Token: 0x17001C49 RID: 7241
		// (get) Token: 0x060054D8 RID: 21720 RVA: 0x0013283D File Offset: 0x00130A3D
		// (set) Token: 0x060054D9 RID: 21721 RVA: 0x0013284F File Offset: 0x00130A4F
		[Parameter(Mandatory = false)]
		public bool IsUnifiedMessagingServer
		{
			get
			{
				return (bool)this[ExchangeServerRoleSchema.IsUnifiedMessagingServer];
			}
			set
			{
				this[ExchangeServerRoleSchema.IsUnifiedMessagingServer] = value;
			}
		}

		// Token: 0x17001C4A RID: 7242
		// (get) Token: 0x060054DA RID: 21722 RVA: 0x00132862 File Offset: 0x00130A62
		// (set) Token: 0x060054DB RID: 21723 RVA: 0x00132874 File Offset: 0x00130A74
		[Parameter(Mandatory = false)]
		public bool IsProvisionedServer
		{
			get
			{
				return (bool)this[ExchangeServerRoleSchema.IsProvisionedServer];
			}
			set
			{
				this[ExchangeServerRoleSchema.IsProvisionedServer] = value;
			}
		}

		// Token: 0x17001C4B RID: 7243
		// (get) Token: 0x060054DC RID: 21724 RVA: 0x00132887 File Offset: 0x00130A87
		// (set) Token: 0x060054DD RID: 21725 RVA: 0x00132899 File Offset: 0x00130A99
		[Parameter(Mandatory = false)]
		public bool IsCafeServer
		{
			get
			{
				return (bool)this[ExchangeServerRoleSchema.IsCafeServer];
			}
			set
			{
				this[ExchangeServerRoleSchema.IsCafeServer] = value;
			}
		}

		// Token: 0x17001C4C RID: 7244
		// (get) Token: 0x060054DE RID: 21726 RVA: 0x001328AC File Offset: 0x00130AAC
		// (set) Token: 0x060054DF RID: 21727 RVA: 0x001328BE File Offset: 0x00130ABE
		[Parameter(Mandatory = false)]
		public bool IsFrontendTransportServer
		{
			get
			{
				return (bool)this[ExchangeServerRoleSchema.IsFrontendTransportServer];
			}
			set
			{
				this[ExchangeServerRoleSchema.IsFrontendTransportServer] = value;
			}
		}

		// Token: 0x040038F1 RID: 14577
		private static ExchangeServerRoleSchema schema = ObjectSchema.GetInstance<ExchangeServerRoleSchema>();
	}
}
