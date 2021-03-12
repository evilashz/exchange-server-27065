using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingDestinations
{
	// Token: 0x0200001A RID: 26
	internal class UnknownRoutingDestination : RoutingDestinationBase
	{
		// Token: 0x06000071 RID: 113 RVA: 0x000030C4 File Offset: 0x000012C4
		public UnknownRoutingDestination(string type, string value, IList<string> properties)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			this.type = type;
			this.value = value;
			this.properties = properties;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003116 File Offset: 0x00001316
		public override RoutingItemType RoutingItemType
		{
			get
			{
				return RoutingItemType.Unknown;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003119 File Offset: 0x00001319
		public override IList<string> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003121 File Offset: 0x00001321
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003129 File Offset: 0x00001329
		public override string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0400002A RID: 42
		private readonly string type;

		// Token: 0x0400002B RID: 43
		private readonly IList<string> properties;

		// Token: 0x0400002C RID: 44
		private readonly string value;
	}
}
