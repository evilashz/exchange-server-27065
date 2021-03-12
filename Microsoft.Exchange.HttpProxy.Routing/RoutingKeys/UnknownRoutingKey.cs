using System;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingKeys
{
	// Token: 0x02000031 RID: 49
	internal class UnknownRoutingKey : RoutingKeyBase
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x00003AE0 File Offset: 0x00001CE0
		public UnknownRoutingKey(string type, string value)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.type = type;
			this.value = value;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00003B12 File Offset: 0x00001D12
		public override RoutingItemType RoutingItemType
		{
			get
			{
				return RoutingItemType.Unknown;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00003B15 File Offset: 0x00001D15
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00003B1D File Offset: 0x00001D1D
		public override string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0400005A RID: 90
		private readonly string type;

		// Token: 0x0400005B RID: 91
		private readonly string value;
	}
}
