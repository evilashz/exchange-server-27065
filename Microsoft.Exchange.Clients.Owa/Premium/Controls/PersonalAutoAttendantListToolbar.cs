using System;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003DC RID: 988
	public class PersonalAutoAttendantListToolbar : Toolbar
	{
		// Token: 0x06002462 RID: 9314 RVA: 0x000D3923 File Offset: 0x000D1B23
		public PersonalAutoAttendantListToolbar() : base(ToolbarType.Options)
		{
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x000D392C File Offset: 0x000D1B2C
		protected override void RenderButtons()
		{
			base.RenderButton(ToolbarButtons.NewPersonalAutoAttendant);
			base.RenderButton(ToolbarButtons.EditTextOnly);
			base.RenderButton(ToolbarButtons.DeleteWithText);
			base.RenderButton(ToolbarButtons.MoveUp);
			base.RenderButton(ToolbarButtons.MoveDown);
		}
	}
}
