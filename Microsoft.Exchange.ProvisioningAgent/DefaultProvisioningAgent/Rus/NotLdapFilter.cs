using System;
using System.Text;

namespace Microsoft.Exchange.DefaultProvisioningAgent.Rus
{
	// Token: 0x02000046 RID: 70
	internal class NotLdapFilter : CompositionLdapFilter
	{
		// Token: 0x060001DF RID: 479 RVA: 0x0000C56E File Offset: 0x0000A76E
		public NotLdapFilter(LdapFilter subfFilter)
		{
			base.Add(subfFilter);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000C57D File Offset: 0x0000A77D
		public NotLdapFilter()
		{
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000C588 File Offset: 0x0000A788
		public override bool Evaluate(object[] marshalledAttributes)
		{
			bool result = false;
			int index = 0;
			if (base.SubFilters.Count != 0)
			{
				result = !base.SubFilters[index].Evaluate(marshalledAttributes);
			}
			return result;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000C5C0 File Offset: 0x0000A7C0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(!");
			stringBuilder.Append(base.ToString());
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}
	}
}
