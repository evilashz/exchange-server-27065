using System;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000211 RID: 529
	public class DomainNameConverter : ValueToDisplayObjectConverter
	{
		// Token: 0x060017FB RID: 6139 RVA: 0x00065333 File Offset: 0x00063533
		public object Convert(object valueObject)
		{
			if (valueObject != null)
			{
				return "@" + valueObject.ToString().ToLowerInvariant();
			}
			return string.Empty;
		}
	}
}
