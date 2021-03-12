using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000199 RID: 409
	public abstract class ButtonBaseBulkEditorAdapter : BulkEditorAdapter
	{
		// Token: 0x06001057 RID: 4183 RVA: 0x0004010C File Offset: 0x0003E30C
		public ButtonBaseBulkEditorAdapter(ButtonBase control) : base(control)
		{
			this.bulkEditSupport = (control as IButtonBaseBulkEditSupport);
			this.bulkEditSupport.Painted += this.OnAppearancePainted;
			this.bulkEditSupport.FocusSetted += this.OnAppearancePainted;
			this.bulkEditSupport.FocusKilled += this.OnAppearancePainted;
			this.bulkEditSupport.CheckedChangedRaising += this.OnCheckedChangedRaising;
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x00040188 File Offset: 0x0003E388
		internal void OnCheckedChangedRaising(object sender, HandledEventArgs e)
		{
			e.Handled = (base["Checked"] != null && !this.forceAllowCheckedChangedEvent);
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x000401AC File Offset: 0x0003E3AC
		internal void OnAppearancePainted(object sender, EventArgs e)
		{
			if (base["Checked"] != null)
			{
				using (Graphics graphics = base.HostControl.CreateGraphics())
				{
					this.OnOwnerDraw(graphics);
				}
			}
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x000401F8 File Offset: 0x0003E3F8
		protected Rectangle CalculateCheckBounds(ContentAlignment alignment, Size fullCheckSize)
		{
			Rectangle clientRectangle = base.HostControl.ClientRectangle;
			Rectangle rectangle = new Rectangle(clientRectangle.Location, fullCheckSize);
			if (fullCheckSize.Width > 0)
			{
				if ((alignment & (ContentAlignment)1092) != (ContentAlignment)0)
				{
					rectangle.X = clientRectangle.X + clientRectangle.Width - rectangle.Width;
				}
				else if ((alignment & (ContentAlignment)546) != (ContentAlignment)0)
				{
					rectangle.X = clientRectangle.X + (clientRectangle.Width - rectangle.Width) / 2;
				}
				if ((alignment & (ContentAlignment)1792) != (ContentAlignment)0)
				{
					rectangle.Y = clientRectangle.Y + clientRectangle.Height - rectangle.Height;
				}
				else if ((alignment & (ContentAlignment)7) != (ContentAlignment)0)
				{
					rectangle.Y = clientRectangle.Y + 2;
				}
				else
				{
					rectangle.Y = clientRectangle.Y + (clientRectangle.Height - rectangle.Height) / 2;
				}
			}
			return LayoutHelper.MirrorRectangle(rectangle, base.HostControl);
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x000402EB File Offset: 0x0003E4EB
		protected virtual void OnOwnerDraw(Graphics g)
		{
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x000402F0 File Offset: 0x0003E4F0
		protected override IList<string> InnerGetManagedProperties()
		{
			IList<string> list = base.InnerGetManagedProperties();
			list.Add("Checked");
			return list;
		}

		// Token: 0x0400065B RID: 1627
		protected const string ManagedPropertyName = "Checked";

		// Token: 0x0400065C RID: 1628
		protected bool forceAllowCheckedChangedEvent;

		// Token: 0x0400065D RID: 1629
		protected IButtonBaseBulkEditSupport bulkEditSupport;
	}
}
