using System;
using System.ComponentModel;
using System.Web.UI;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020005DC RID: 1500
	public class ValuePair : Control
	{
		// Token: 0x17002629 RID: 9769
		// (get) Token: 0x06004375 RID: 17269 RVA: 0x000CC434 File Offset: 0x000CA634
		// (set) Token: 0x06004376 RID: 17270 RVA: 0x000CC43C File Offset: 0x000CA63C
		[DefaultValue("")]
		public string Name { get; set; }

		// Token: 0x1700262A RID: 9770
		// (get) Token: 0x06004377 RID: 17271 RVA: 0x000CC445 File Offset: 0x000CA645
		// (set) Token: 0x06004378 RID: 17272 RVA: 0x000CC44D File Offset: 0x000CA64D
		[DefaultValue("")]
		public virtual object Value { get; set; }
	}
}
