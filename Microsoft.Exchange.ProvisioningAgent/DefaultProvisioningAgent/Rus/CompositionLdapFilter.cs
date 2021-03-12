using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Microsoft.Exchange.DefaultProvisioningAgent.Rus
{
	// Token: 0x02000043 RID: 67
	internal abstract class CompositionLdapFilter : LdapFilter
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x0000C39A File Offset: 0x0000A59A
		public void Add(LdapFilter subFilter)
		{
			this.subFilters.Add(subFilter);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000C3A8 File Offset: 0x0000A5A8
		public void AddRange(IEnumerable<LdapFilter> filters)
		{
			this.subFilters.AddRange(filters);
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000C3B6 File Offset: 0x0000A5B6
		public ReadOnlyCollection<LdapFilter> SubFilters
		{
			get
			{
				return this.subFilters.AsReadOnly();
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000C3C4 File Offset: 0x0000A5C4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (LdapFilter ldapFilter in this.SubFilters)
			{
				stringBuilder.Append(ldapFilter.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000E3 RID: 227
		private List<LdapFilter> subFilters = new List<LdapFilter>();
	}
}
