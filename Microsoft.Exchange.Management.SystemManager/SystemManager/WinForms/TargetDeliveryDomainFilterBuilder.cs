using System;
using System.Data;
using System.Text;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000259 RID: 601
	internal class TargetDeliveryDomainFilterBuilder : IExchangeCommandFilterBuilder
	{
		// Token: 0x06001A6C RID: 6764 RVA: 0x00074C68 File Offset: 0x00072E68
		public void BuildFilter(out string parameterList, out string filter, out string preArgs, DataRow row)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (row.Table.Columns.Contains("TargetDeliveryDomainOnly") && true.Equals(row["TargetDeliveryDomainOnly"]))
			{
				stringBuilder.Append(" | Filter-PropertyEqualTo -Property 'TargetDeliveryDomain' -Value true");
			}
			else
			{
				stringBuilder.Append(" | Filter-PropertyStringNotContains -Property 'DomainName' -SearchText '*'");
			}
			filter = stringBuilder.ToString();
			parameterList = null;
			preArgs = null;
		}
	}
}
