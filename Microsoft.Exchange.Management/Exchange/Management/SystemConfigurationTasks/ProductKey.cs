using System;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009C3 RID: 2499
	[Serializable]
	public struct ProductKey
	{
		// Token: 0x060058FA RID: 22778 RVA: 0x00175098 File Offset: 0x00173298
		public ProductKey(string productKey)
		{
			this.productKey = productKey;
			this.sku = Sku.GetSku(productKey);
			if (this.Sku == null)
			{
				throw new InvalidProductKeyException();
			}
		}

		// Token: 0x060058FB RID: 22779 RVA: 0x001750BB File Offset: 0x001732BB
		public static ProductKey Parse(string productKey)
		{
			return new ProductKey(productKey);
		}

		// Token: 0x17001A83 RID: 6787
		// (get) Token: 0x060058FC RID: 22780 RVA: 0x001750C3 File Offset: 0x001732C3
		public bool IsEmpty
		{
			get
			{
				return string.IsNullOrEmpty(this.productKey);
			}
		}

		// Token: 0x060058FD RID: 22781 RVA: 0x001750D0 File Offset: 0x001732D0
		public override string ToString()
		{
			return this.productKey ?? "";
		}

		// Token: 0x17001A84 RID: 6788
		// (get) Token: 0x060058FE RID: 22782 RVA: 0x001750E1 File Offset: 0x001732E1
		internal Sku Sku
		{
			get
			{
				return this.sku;
			}
		}

		// Token: 0x060058FF RID: 22783 RVA: 0x001750E9 File Offset: 0x001732E9
		internal string GenerateProductID()
		{
			if (this.IsEmpty)
			{
				throw new InvalidProductKeyException();
			}
			return this.Sku.GenerateProductID(this.productKey);
		}

		// Token: 0x04003307 RID: 13063
		private readonly string productKey;

		// Token: 0x04003308 RID: 13064
		private readonly Sku sku;

		// Token: 0x04003309 RID: 13065
		public static readonly ProductKey Empty = default(ProductKey);
	}
}
