using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200015F RID: 351
	[Serializable]
	public class UMIPGatewayIdParameter : ADIdParameter
	{
		// Token: 0x06000CA5 RID: 3237 RVA: 0x00027BAC File Offset: 0x00025DAC
		public UMIPGatewayIdParameter()
		{
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00027BB4 File Offset: 0x00025DB4
		public UMIPGatewayIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00027BBD File Offset: 0x00025DBD
		public UMIPGatewayIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x00027BC6 File Offset: 0x00025DC6
		public UMIPGatewayIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00027BCF File Offset: 0x00025DCF
		public static UMIPGatewayIdParameter Parse(string identity)
		{
			return new UMIPGatewayIdParameter(identity);
		}
	}
}
