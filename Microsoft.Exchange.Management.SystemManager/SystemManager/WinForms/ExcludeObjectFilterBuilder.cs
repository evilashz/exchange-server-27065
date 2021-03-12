using System;
using System.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200025C RID: 604
	internal class ExcludeObjectFilterBuilder : IExchangeCommandFilterBuilder
	{
		// Token: 0x06001A72 RID: 6770 RVA: 0x00074D3C File Offset: 0x00072F3C
		public virtual void BuildFilter(out string parameterList, out string filter, out string preArgs, DataRow row)
		{
			ADObjectId adobjectId = null;
			if (!DBNull.Value.Equals(row["ExcludeObject"]))
			{
				adobjectId = (row["ExcludeObject"] as ADObjectId);
			}
			filter = null;
			if (adobjectId != null)
			{
				filter = string.Format(" | Filter-PropertyNotEqualTo -Property 'Identity' -Value '{0}'", adobjectId.ObjectGuid.ToQuotationEscapedString());
			}
			parameterList = null;
			preArgs = null;
		}
	}
}
