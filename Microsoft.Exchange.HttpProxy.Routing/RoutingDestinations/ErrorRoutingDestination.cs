using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingDestinations
{
	// Token: 0x02000018 RID: 24
	internal class ErrorRoutingDestination : RoutingDestinationBase
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00002F6C File Offset: 0x0000116C
		public ErrorRoutingDestination(string message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			this.message = message;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002F89 File Offset: 0x00001189
		public string Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002F91 File Offset: 0x00001191
		public override RoutingItemType RoutingItemType
		{
			get
			{
				return RoutingItemType.Error;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002F94 File Offset: 0x00001194
		public override string Value
		{
			get
			{
				return this.Message;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002F9C File Offset: 0x0000119C
		public override IList<string> Properties
		{
			get
			{
				return Array<string>.Empty;
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002FA3 File Offset: 0x000011A3
		public static bool TryParse(string value, IList<string> properties, out ErrorRoutingDestination destination)
		{
			destination = null;
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			destination = new ErrorRoutingDestination(value);
			return true;
		}

		// Token: 0x04000027 RID: 39
		private readonly string message;
	}
}
