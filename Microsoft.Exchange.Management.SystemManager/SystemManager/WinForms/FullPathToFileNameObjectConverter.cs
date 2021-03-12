using System;
using System.IO;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200013D RID: 317
	public class FullPathToFileNameObjectConverter : ValueToDisplayObjectConverter
	{
		// Token: 0x06000C79 RID: 3193 RVA: 0x0002CF39 File Offset: 0x0002B139
		public object Convert(object valueObject)
		{
			return Path.GetFileName((string)valueObject);
		}
	}
}
