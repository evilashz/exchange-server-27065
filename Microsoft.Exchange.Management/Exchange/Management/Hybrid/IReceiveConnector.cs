using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x020008FE RID: 2302
	internal interface IReceiveConnector : IEntity<IReceiveConnector>
	{
		// Token: 0x1700187E RID: 6270
		// (get) Token: 0x060051A1 RID: 20897
		ADObjectId Server { get; }

		// Token: 0x1700187F RID: 6271
		// (get) Token: 0x060051A2 RID: 20898
		SmtpX509Identifier TlsCertificateName { get; }

		// Token: 0x17001880 RID: 6272
		// (get) Token: 0x060051A3 RID: 20899
		SmtpReceiveDomainCapabilities TlsDomainCapabilities { get; }
	}
}
