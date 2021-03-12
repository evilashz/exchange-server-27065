using System;
using System.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.DetailsTemplates
{
	// Token: 0x02000018 RID: 24
	internal class DetailsTemplateFilterBuilder : IExchangeCommandFilterBuilder
	{
		// Token: 0x060000AE RID: 174 RVA: 0x00005494 File Offset: 0x00003694
		public virtual void BuildFilter(out string parameterList, out string filter, out string preArgs, DataRow row)
		{
			ADObjectId adobjectId = row["Identity"] as ADObjectId;
			parameterList = ((adobjectId != null) ? string.Format(" -Identity '{0}'", adobjectId) : null);
			filter = null;
			preArgs = null;
		}
	}
}
