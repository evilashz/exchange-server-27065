using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000110 RID: 272
	public class ADObjectIdToNameConverter : ValueToDisplayObjectConverter
	{
		// Token: 0x060009FE RID: 2558 RVA: 0x00022914 File Offset: 0x00020B14
		public object Convert(object valueObject)
		{
			ADObjectId adobjectId = (ADObjectId)valueObject;
			return adobjectId.Name;
		}
	}
}
