using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000A5 RID: 165
	public class AutoAttendantDataObjectUMDialPlanSetter : IPropertySetter
	{
		// Token: 0x06000540 RID: 1344 RVA: 0x000141DD File Offset: 0x000123DD
		public void Set(object dataObject, object value)
		{
			((UMAutoAttendant)dataObject).SetDialPlan((ADObjectId)value);
		}
	}
}
