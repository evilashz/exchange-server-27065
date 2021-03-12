using System;
using System.Data;
using System.Text;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000258 RID: 600
	internal class AcceptedDomainFilterBuilder : IExchangeCommandFilterBuilder
	{
		// Token: 0x06001A69 RID: 6761 RVA: 0x00074B99 File Offset: 0x00072D99
		private bool HasTrueValue(DataRow row, string columnName)
		{
			return row.Table.Columns.Contains(columnName) && !DBNull.Value.Equals(row[columnName]) && (bool)row[columnName];
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x00074BD0 File Offset: 0x00072DD0
		public void BuildFilter(out string parameterList, out string filter, out string preArgs, DataRow row)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.HasTrueValue(row, "ExcludeExternalRelay"))
			{
				stringBuilder.Append(" | Filter-PropertyNotEqualTo -Property 'DomainType' -Value 'ExternalRelay'");
			}
			if (this.HasTrueValue(row, "ExcludeAuthoritative"))
			{
				stringBuilder.Append(" | Filter-PropertyNotEqualTo -Property 'DomainType' -Value 'Authoritative'");
			}
			if (this.HasTrueValue(row, "ExcludeInternalRelay"))
			{
				stringBuilder.Append(" | Filter-PropertyNotEqualTo -Property 'DomainType' -Value 'InternalRelay'");
			}
			if (this.HasTrueValue(row, "ExcludeDomainWithSubDomain"))
			{
				stringBuilder.Append(" | Filter-PropertyStringNotContains -Property 'DomainName' -SearchText '*'");
			}
			filter = stringBuilder.ToString();
			parameterList = null;
			preArgs = null;
		}
	}
}
