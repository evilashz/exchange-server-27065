using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000A6 RID: 166
	public class AutoAttendantDataObjectStatusSetter : IPropertySetter
	{
		// Token: 0x06000542 RID: 1346 RVA: 0x000141F8 File Offset: 0x000123F8
		public void Set(object dataObject, object value)
		{
			((UMAutoAttendant)dataObject).SetStatus((StatusEnum)value);
		}
	}
}
