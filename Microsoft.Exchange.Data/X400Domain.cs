using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001E7 RID: 487
	[Serializable]
	public class X400Domain
	{
		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060010C8 RID: 4296 RVA: 0x00033438 File Offset: 0x00031638
		internal IList<string> Components
		{
			get
			{
				return this.components;
			}
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x00033440 File Offset: 0x00031640
		private X400Domain(IList<string> components)
		{
			this.components = components;
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x00033450 File Offset: 0x00031650
		public static X400Domain Parse(string s)
		{
			X400Domain result;
			if (X400Domain.TryParse(s, out result))
			{
				return result;
			}
			throw new FormatException(DataStrings.InvalidX400Domain(s));
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x0003347C File Offset: 0x0003167C
		public static bool TryParse(string s, out X400Domain result)
		{
			IList<string> list;
			if (X400AddressParser.TryParse(s, out list))
			{
				int i = list.Count - 1;
				while (i >= 0 && string.IsNullOrEmpty(list[i]))
				{
					list.RemoveAt(i--);
				}
				for (i = 0; i < list.Count; i++)
				{
					if (list[i] == string.Empty)
					{
						list[i] = null;
					}
					else if (!X400Domain.IsValidComponent(i, list[i]))
					{
						result = null;
						return false;
					}
				}
				if (list.Count > 0 && list.Count < 8)
				{
					result = new X400Domain(list);
					return true;
				}
			}
			result = null;
			return false;
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x00033520 File Offset: 0x00031720
		public override string ToString()
		{
			return X400AddressParser.ToCanonicalString(this.components);
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x00033530 File Offset: 0x00031730
		public bool Match(RoutingX400Address address)
		{
			if (address == null || address.ComponentsCount < this.components.Count)
			{
				return false;
			}
			for (int i = 0; i < this.components.Count; i++)
			{
				if (!X400Domain.MatchOneComponent(this.components[i], address[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x00033588 File Offset: 0x00031788
		private static bool MatchOneComponent(string domainComponent, string addressComponent)
		{
			if (domainComponent == null)
			{
				return string.IsNullOrEmpty(addressComponent);
			}
			return string.Equals(domainComponent, addressComponent, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x0003359C File Offset: 0x0003179C
		private static bool IsValidComponent(int type, string value)
		{
			return value == null || (!value.Contains("*") && value.IndexOf('%') == -1 && ((type == 1 && string.Equals(value, " ", StringComparison.Ordinal)) || value.Trim().Length > 0));
		}

		// Token: 0x04000A73 RID: 2675
		private IList<string> components;
	}
}
