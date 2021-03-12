using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.EhfHybridMailflow
{
	// Token: 0x0200087A RID: 2170
	[Serializable]
	public sealed class HybridMailflowDatacenterIPs : ConfigurableObject
	{
		// Token: 0x1700164D RID: 5709
		// (get) Token: 0x06004B25 RID: 19237 RVA: 0x00137785 File Offset: 0x00135985
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return HybridMailflowDatacenterIPs.schema;
			}
		}

		// Token: 0x1700164E RID: 5710
		// (get) Token: 0x06004B26 RID: 19238 RVA: 0x0013778C File Offset: 0x0013598C
		// (set) Token: 0x06004B27 RID: 19239 RVA: 0x00137794 File Offset: 0x00135994
		public List<IPRange> DatacenterIPs
		{
			get
			{
				return this.myDatacenterIPs;
			}
			set
			{
				this.myDatacenterIPs = value;
			}
		}

		// Token: 0x06004B28 RID: 19240 RVA: 0x0013779D File Offset: 0x0013599D
		internal HybridMailflowDatacenterIPs() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x06004B29 RID: 19241 RVA: 0x001377AA File Offset: 0x001359AA
		internal HybridMailflowDatacenterIPs(IList<IPRange> datacenterIPs) : base(new SimpleProviderPropertyBag())
		{
			this.DatacenterIPs = new List<IPRange>(datacenterIPs);
		}

		// Token: 0x04002D23 RID: 11555
		private const string MostDerivedClass = "msHybridMailflowDatacenterIPs";

		// Token: 0x04002D24 RID: 11556
		private static HybridMailflowDatacenterIPsSchema schema = ObjectSchema.GetInstance<HybridMailflowDatacenterIPsSchema>();

		// Token: 0x04002D25 RID: 11557
		private List<IPRange> myDatacenterIPs;
	}
}
