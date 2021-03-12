using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.DefaultProvisioningAgent.Rus
{
	// Token: 0x02000044 RID: 68
	internal class AndLdapFilter : CompositionLdapFilter
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x0000C437 File Offset: 0x0000A637
		public AndLdapFilter(IEnumerable<LdapFilter> subfFilters)
		{
			base.AddRange(subfFilters);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000C446 File Offset: 0x0000A646
		public AndLdapFilter()
		{
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000C450 File Offset: 0x0000A650
		public override bool Evaluate(object[] marshalledAttributes)
		{
			bool flag = true;
			int num = 0;
			while (flag && num < base.SubFilters.Count)
			{
				flag = (flag && base.SubFilters[num].Evaluate(marshalledAttributes));
				num++;
			}
			return flag;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000C494 File Offset: 0x0000A694
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(&");
			stringBuilder.Append(base.ToString());
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}
	}
}
