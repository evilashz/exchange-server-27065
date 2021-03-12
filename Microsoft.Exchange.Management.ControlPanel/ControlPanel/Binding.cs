using System;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000586 RID: 1414
	public abstract class Binding
	{
		// Token: 0x17002567 RID: 9575
		// (get) Token: 0x06004184 RID: 16772 RVA: 0x000C7BB2 File Offset: 0x000C5DB2
		// (set) Token: 0x06004185 RID: 16773 RVA: 0x000C7BBA File Offset: 0x000C5DBA
		public string Name { get; set; }

		// Token: 0x06004186 RID: 16774
		public abstract string ToJavaScript(IControlResolver resolver);
	}
}
