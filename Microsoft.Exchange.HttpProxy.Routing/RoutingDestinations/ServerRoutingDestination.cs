using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingDestinations
{
	// Token: 0x02000019 RID: 25
	internal class ServerRoutingDestination : RoutingDestinationBase
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00002FCD File Offset: 0x000011CD
		public ServerRoutingDestination(string fqdn, int? version)
		{
			if (fqdn == null)
			{
				throw new ArgumentNullException("fqdn");
			}
			this.fqdn = fqdn;
			this.version = version;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002FF1 File Offset: 0x000011F1
		public string Fqdn
		{
			get
			{
				return this.fqdn;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002FF9 File Offset: 0x000011F9
		public int? Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003001 File Offset: 0x00001201
		public override RoutingItemType RoutingItemType
		{
			get
			{
				return RoutingItemType.Server;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003004 File Offset: 0x00001204
		public override string Value
		{
			get
			{
				return this.fqdn;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600006F RID: 111 RVA: 0x0000300C File Offset: 0x0000120C
		public override IList<string> Properties
		{
			get
			{
				if (this.version != null)
				{
					return new string[]
					{
						this.version.ToString()
					};
				}
				return Array<string>.Empty;
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003050 File Offset: 0x00001250
		public static bool TryParse(string value, IList<string> properties, out ServerRoutingDestination destination)
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
			if (properties.Count == 0)
			{
				destination = new ServerRoutingDestination(value, null);
				return true;
			}
			int value2;
			if (properties.Count > 0 && int.TryParse(properties[0], out value2))
			{
				destination = new ServerRoutingDestination(value, new int?(value2));
				return true;
			}
			return false;
		}

		// Token: 0x04000028 RID: 40
		private readonly string fqdn;

		// Token: 0x04000029 RID: 41
		private readonly int? version;
	}
}
