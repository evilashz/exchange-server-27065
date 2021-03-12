using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000106 RID: 262
	public struct FilterFactorHint
	{
		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x00036AB8 File Offset: 0x00034CB8
		public bool IsEquality
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x00036ABB File Offset: 0x00034CBB
		public bool AnyValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x00036ABE File Offset: 0x00034CBE
		public object Value
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x00036AC1 File Offset: 0x00034CC1
		// (set) Token: 0x06000A9C RID: 2716 RVA: 0x00036AC9 File Offset: 0x00034CC9
		public double FilterFactor { get; set; }
	}
}
