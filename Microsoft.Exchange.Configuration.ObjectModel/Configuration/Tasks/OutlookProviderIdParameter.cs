using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000136 RID: 310
	[Serializable]
	public class OutlookProviderIdParameter : ADIdParameter
	{
		// Token: 0x06000B12 RID: 2834 RVA: 0x00023B37 File Offset: 0x00021D37
		public OutlookProviderIdParameter(ADObjectId objectId) : base(objectId)
		{
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00023B40 File Offset: 0x00021D40
		public OutlookProviderIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00023B49 File Offset: 0x00021D49
		public OutlookProviderIdParameter()
		{
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00023B51 File Offset: 0x00021D51
		public OutlookProviderIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x00023B5A File Offset: 0x00021D5A
		public static OutlookProviderIdParameter Parse(string rawString)
		{
			return new OutlookProviderIdParameter(rawString);
		}
	}
}
