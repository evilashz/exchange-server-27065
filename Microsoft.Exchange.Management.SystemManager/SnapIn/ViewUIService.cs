using System;
using Microsoft.ManagementConsole;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x020002A0 RID: 672
	internal class ViewUIService : SnapInUIService
	{
		// Token: 0x06001C6B RID: 7275 RVA: 0x0007B052 File Offset: 0x00079252
		public ViewUIService(FormView view) : base(view.SnapIn, view.Control)
		{
			this.view = view;
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x0007B06D File Offset: 0x0007926D
		public override void SetUIDirty()
		{
			this.view.IsModified = true;
		}

		// Token: 0x04000A99 RID: 2713
		private View view;
	}
}
