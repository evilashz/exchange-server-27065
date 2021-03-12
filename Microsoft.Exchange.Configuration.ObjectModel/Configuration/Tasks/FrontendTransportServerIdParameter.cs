using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200018C RID: 396
	[Serializable]
	public class FrontendTransportServerIdParameter : ExchangeTransportServerIdParameter
	{
		// Token: 0x06000E71 RID: 3697 RVA: 0x0002AEC8 File Offset: 0x000290C8
		public FrontendTransportServerIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x0002AED1 File Offset: 0x000290D1
		public FrontendTransportServerIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x0002AEDA File Offset: 0x000290DA
		public FrontendTransportServerIdParameter()
		{
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x0002AEE2 File Offset: 0x000290E2
		protected FrontendTransportServerIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x0002AEEB File Offset: 0x000290EB
		public static FrontendTransportServerIdParameter Parse(string identity)
		{
			return new FrontendTransportServerIdParameter(identity);
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0002AEF4 File Offset: 0x000290F4
		public static FrontendTransportServerIdParameter CreateIdentity(FrontendTransportServerIdParameter identityPassedIn)
		{
			return new FrontendTransportServerIdParameter("Frontend")
			{
				identityPassedIn = identityPassedIn
			};
		}
	}
}
