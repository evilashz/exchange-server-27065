using System;
using Microsoft.Exchange.HttpProxy.Common;

namespace Microsoft.Exchange.HttpProxy.Routing.RoutingKeys
{
	// Token: 0x0200002F RID: 47
	internal class ServerRoutingKey : RoutingKeyBase
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x00003968 File Offset: 0x00001B68
		public ServerRoutingKey(string fqdn) : this(fqdn, null)
		{
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003985 File Offset: 0x00001B85
		public ServerRoutingKey(string fqdn, int? version)
		{
			if (string.IsNullOrEmpty(fqdn))
			{
				throw new ArgumentException("The FQDN should not be null or empty.", "fqdn");
			}
			this.fqdn = fqdn;
			this.version = version;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000039B3 File Offset: 0x00001BB3
		public override RoutingItemType RoutingItemType
		{
			get
			{
				return RoutingItemType.Server;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000039B8 File Offset: 0x00001BB8
		public override string Value
		{
			get
			{
				if (this.version != null)
				{
					return this.fqdn + '~' + this.version;
				}
				return this.fqdn;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000039F9 File Offset: 0x00001BF9
		public string Server
		{
			get
			{
				return this.fqdn;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00003A01 File Offset: 0x00001C01
		public int? Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003A0C File Offset: 0x00001C0C
		public static bool TryParse(string value, out ServerRoutingKey key)
		{
			key = null;
			if (string.IsNullOrEmpty(value))
			{
				return false;
			}
			string text;
			string text2;
			Utilities.GetTwoSubstrings(value, '~', out text, out text2);
			if (string.IsNullOrEmpty(text2))
			{
				key = new ServerRoutingKey(text);
				return true;
			}
			int value2;
			if (!int.TryParse(text2, out value2))
			{
				return false;
			}
			key = new ServerRoutingKey(text, new int?(value2));
			return true;
		}

		// Token: 0x04000056 RID: 86
		private const char Separator = '~';

		// Token: 0x04000057 RID: 87
		private readonly string fqdn;

		// Token: 0x04000058 RID: 88
		private readonly int? version;
	}
}
