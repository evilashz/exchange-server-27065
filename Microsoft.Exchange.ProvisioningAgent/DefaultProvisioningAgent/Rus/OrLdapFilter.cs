using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.DefaultProvisioningAgent.Rus
{
	// Token: 0x02000045 RID: 69
	internal class OrLdapFilter : CompositionLdapFilter
	{
		// Token: 0x060001DB RID: 475 RVA: 0x0000C4D2 File Offset: 0x0000A6D2
		public OrLdapFilter(IEnumerable<LdapFilter> subfFilters)
		{
			base.AddRange(subfFilters);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000C4E1 File Offset: 0x0000A6E1
		public OrLdapFilter()
		{
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000C4EC File Offset: 0x0000A6EC
		public override bool Evaluate(object[] marshalledAttributest)
		{
			bool flag = false;
			int num = 0;
			while (!flag && num < base.SubFilters.Count)
			{
				flag = (flag || base.SubFilters[num].Evaluate(marshalledAttributest));
				num++;
			}
			return flag;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000C530 File Offset: 0x0000A730
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(|");
			stringBuilder.Append(base.ToString());
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}
	}
}
