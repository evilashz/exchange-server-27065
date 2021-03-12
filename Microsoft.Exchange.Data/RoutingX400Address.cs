using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000198 RID: 408
	[Serializable]
	public class RoutingX400Address
	{
		// Token: 0x06000D38 RID: 3384 RVA: 0x00029219 File Offset: 0x00027419
		private RoutingX400Address(IList<string> components)
		{
			this.components = components;
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000D39 RID: 3385 RVA: 0x00029228 File Offset: 0x00027428
		public int ComponentsCount
		{
			get
			{
				return this.components.Count;
			}
		}

		// Token: 0x1700040B RID: 1035
		public string this[int componentIndex]
		{
			get
			{
				return this.components[componentIndex];
			}
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00029243 File Offset: 0x00027443
		public override string ToString()
		{
			return X400AddressParser.ToCanonicalString(this.components);
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x00029250 File Offset: 0x00027450
		public static bool TryParse(string s, out RoutingX400Address address)
		{
			return RoutingX400Address.TryParse(s, false, false, out address);
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0002925B File Offset: 0x0002745B
		public static bool TryParseAddressSpace(string s, bool locallyScoped, out RoutingX400Address address)
		{
			return RoutingX400Address.TryParse(s, true, locallyScoped, out address);
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00029268 File Offset: 0x00027468
		private static bool TryParse(string s, bool addressSpace, bool locallyScoped, out RoutingX400Address address)
		{
			address = null;
			IList<string> list = null;
			if (!X400AddressParser.TryParse(s, 8, addressSpace, locallyScoped, out list))
			{
				return false;
			}
			int i = 8;
			if (addressSpace)
			{
				while (i > 0)
				{
					if (list[i - 1] != null)
					{
						break;
					}
					list.RemoveAt(--i);
				}
				while (i > 0)
				{
					string text = list[i - 1];
					if (text == null || !text.Equals("*", StringComparison.OrdinalIgnoreCase))
					{
						break;
					}
					list.RemoveAt(--i);
				}
			}
			while (i > 0)
			{
				if (list[i - 1] == null)
				{
					list[i - 1] = string.Empty;
				}
				i--;
			}
			address = new RoutingX400Address(list);
			return true;
		}

		// Token: 0x040007F4 RID: 2036
		public const char SingleCharWildcard = '%';

		// Token: 0x040007F5 RID: 2037
		public const string AdmdWildcard = " ";

		// Token: 0x040007F6 RID: 2038
		public const string Wildcard = "*";

		// Token: 0x040007F7 RID: 2039
		internal const int RoutingComponentsCount = 8;

		// Token: 0x040007F8 RID: 2040
		private IList<string> components;
	}
}
