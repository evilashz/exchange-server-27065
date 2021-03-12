using System;
using System.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000267 RID: 615
	internal class UMDialPlanFilterBuilder : IExchangeCommandFilterBuilder
	{
		// Token: 0x06001A8A RID: 6794 RVA: 0x00075470 File Offset: 0x00073670
		public void BuildFilter(out string parameterList, out string filter, out string preArgs, DataRow row)
		{
			filter = null;
			parameterList = null;
			preArgs = null;
			if (true.Equals(row["ExcludeSipNameURIType"]))
			{
				filter = " | Filter-PropertyNotEqualTo -Property URIType -Value SipName";
			}
		}
	}
}
