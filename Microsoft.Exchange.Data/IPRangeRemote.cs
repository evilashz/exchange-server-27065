using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000162 RID: 354
	[Serializable]
	public class IPRangeRemote : IPRange
	{
		// Token: 0x06000B80 RID: 2944 RVA: 0x0002451F File Offset: 0x0002271F
		public IPRangeRemote(IPRange ipRange) : base(ipRange.LowerBound, ipRange.UpperBound)
		{
			this.size = base.Size;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00024549 File Offset: 0x00022749
		public static int Compare(IPRangeRemote v1, IPRangeRemote v2)
		{
			return IPvxAddress.Compare(v1.size, v2.size);
		}

		// Token: 0x04000715 RID: 1813
		private readonly IPvxAddress size;
	}
}
