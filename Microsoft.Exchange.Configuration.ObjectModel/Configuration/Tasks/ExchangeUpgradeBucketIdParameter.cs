using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000133 RID: 307
	[Serializable]
	public sealed class ExchangeUpgradeBucketIdParameter : ADIdParameter
	{
		// Token: 0x06000B01 RID: 2817 RVA: 0x00023A9A File Offset: 0x00021C9A
		public ExchangeUpgradeBucketIdParameter()
		{
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00023AA2 File Offset: 0x00021CA2
		public ExchangeUpgradeBucketIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00023AAB File Offset: 0x00021CAB
		public ExchangeUpgradeBucketIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00023AB4 File Offset: 0x00021CB4
		public ExchangeUpgradeBucketIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00023ABD File Offset: 0x00021CBD
		public ExchangeUpgradeBucketIdParameter(ExchangeUpgradeBucket upgradeBucket) : base(upgradeBucket.Id)
		{
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00023ACB File Offset: 0x00021CCB
		public static ExchangeUpgradeBucketIdParameter Parse(string identity)
		{
			return new ExchangeUpgradeBucketIdParameter(identity);
		}
	}
}
