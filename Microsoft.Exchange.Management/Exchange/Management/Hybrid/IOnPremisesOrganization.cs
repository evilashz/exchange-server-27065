using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x020008F1 RID: 2289
	internal interface IOnPremisesOrganization : IEntity<IOnPremisesOrganization>
	{
		// Token: 0x17001847 RID: 6215
		// (get) Token: 0x06005125 RID: 20773
		Guid OrganizationGuid { get; }

		// Token: 0x17001848 RID: 6216
		// (get) Token: 0x06005126 RID: 20774
		string OrganizationName { get; }

		// Token: 0x17001849 RID: 6217
		// (get) Token: 0x06005127 RID: 20775
		MultiValuedProperty<SmtpDomain> HybridDomains { get; }

		// Token: 0x1700184A RID: 6218
		// (get) Token: 0x06005128 RID: 20776
		ADObjectId InboundConnector { get; }

		// Token: 0x1700184B RID: 6219
		// (get) Token: 0x06005129 RID: 20777
		ADObjectId OutboundConnector { get; }

		// Token: 0x1700184C RID: 6220
		// (get) Token: 0x0600512A RID: 20778
		string Name { get; }

		// Token: 0x1700184D RID: 6221
		// (get) Token: 0x0600512B RID: 20779
		ADObjectId OrganizationRelationship { get; }
	}
}
