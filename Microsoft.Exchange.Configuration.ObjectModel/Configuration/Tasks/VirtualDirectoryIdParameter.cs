using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000196 RID: 406
	[Serializable]
	public class VirtualDirectoryIdParameter : ServerBasedIdParameter
	{
		// Token: 0x06000EAD RID: 3757 RVA: 0x0002B1C1 File Offset: 0x000293C1
		public VirtualDirectoryIdParameter()
		{
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0002B1C9 File Offset: 0x000293C9
		public VirtualDirectoryIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0002B1D2 File Offset: 0x000293D2
		public VirtualDirectoryIdParameter(ADVirtualDirectory virtualDirectory) : base(virtualDirectory.Id)
		{
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0002B1E0 File Offset: 0x000293E0
		public VirtualDirectoryIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0002B1E9 File Offset: 0x000293E9
		protected VirtualDirectoryIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x0002B1F4 File Offset: 0x000293F4
		internal string Name
		{
			get
			{
				ADObjectId internalADObjectId = base.InternalADObjectId;
				if (internalADObjectId == null || string.IsNullOrEmpty(internalADObjectId.DistinguishedName))
				{
					return base.CommonName;
				}
				string text = internalADObjectId.Rdn.ToString();
				return text.Substring(text.LastIndexOf('=') + 1);
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000EB3 RID: 3763 RVA: 0x0002B23C File Offset: 0x0002943C
		internal string Server
		{
			get
			{
				ADObjectId internalADObjectId = base.InternalADObjectId;
				if (internalADObjectId == null || string.IsNullOrEmpty(internalADObjectId.DistinguishedName))
				{
					return base.ServerName;
				}
				ADObjectId adobjectId = internalADObjectId.AncestorDN(3);
				if (adobjectId == null)
				{
					return null;
				}
				string text = adobjectId.Rdn.ToString();
				return text.Substring(text.LastIndexOf('=') + 1);
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x0002B290 File Offset: 0x00029490
		protected override ServerRole RoleRestriction
		{
			get
			{
				return ServerRole.Cafe | ServerRole.Mailbox | ServerRole.ClientAccess | ServerRole.UnifiedMessaging | ServerRole.HubTransport | ServerRole.FrontendTransport | ServerRole.FfoWebService | ServerRole.OSP;
			}
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0002B297 File Offset: 0x00029497
		public static VirtualDirectoryIdParameter Parse(string identity)
		{
			return new VirtualDirectoryIdParameter(identity);
		}
	}
}
