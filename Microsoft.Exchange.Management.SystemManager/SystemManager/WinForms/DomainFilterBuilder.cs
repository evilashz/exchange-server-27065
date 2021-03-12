using System;
using System.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000263 RID: 611
	internal class DomainFilterBuilder : IExchangeCommandFilterBuilder
	{
		// Token: 0x06001A84 RID: 6788 RVA: 0x00075385 File Offset: 0x00073585
		public void BuildFilter(out string parameterList, out string filter, out string preArgs, DataRow row)
		{
			filter = " | Filter-PropertyEqualTo -Property 'Type' -Value 'Domain'";
			preArgs = null;
			parameterList = null;
		}
	}
}
