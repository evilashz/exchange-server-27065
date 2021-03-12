using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200012B RID: 299
	[Serializable]
	public sealed class MailFlowPartnerIdParameter : ADIdParameter
	{
		// Token: 0x06000AB6 RID: 2742 RVA: 0x00022FD0 File Offset: 0x000211D0
		public MailFlowPartnerIdParameter()
		{
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00022FD8 File Offset: 0x000211D8
		public MailFlowPartnerIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00022FE1 File Offset: 0x000211E1
		public MailFlowPartnerIdParameter(ADObjectId objectId) : base(objectId)
		{
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00022FEA File Offset: 0x000211EA
		public MailFlowPartnerIdParameter(MailFlowPartner connector) : base(connector.Id)
		{
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00022FF8 File Offset: 0x000211F8
		public MailFlowPartnerIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00023001 File Offset: 0x00021201
		public static MailFlowPartnerIdParameter Parse(string identity)
		{
			return new MailFlowPartnerIdParameter(identity);
		}
	}
}
