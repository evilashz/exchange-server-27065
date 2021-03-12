using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200019F RID: 415
	public sealed class DateTimePickerBulkEditorAdapter : BulkEditorAdapter
	{
		// Token: 0x06001073 RID: 4211 RVA: 0x00040FB4 File Offset: 0x0003F1B4
		public DateTimePickerBulkEditorAdapter(ExtendedDateTimePicker dateTimePicker) : base(dateTimePicker)
		{
			dateTimePicker.Painted += this.OnAppearancePainted;
			dateTimePicker.FocusSetted += this.OnAppearancePainted;
			dateTimePicker.FocusKilled += this.OnAppearancePainted;
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x00041000 File Offset: 0x0003F200
		private void OnAppearancePainted(object sender, EventArgs e)
		{
			if (base["Value"] == 3)
			{
				this.DrawBulkEditorLockedState(Icons.LockIcon);
				return;
			}
			if (base["Value"] != null)
			{
				this.DrawBulkEditorInitialState(base.BulkEditingIndicatorText);
			}
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x00041038 File Offset: 0x0003F238
		private void DrawBulkEditorLockedState(Icon icon)
		{
			using (Graphics graphics = base.HostControl.CreateGraphics())
			{
				Rectangle targetRect = new Rectangle(base.HostControl.ClientRectangle.Right - 8, base.HostControl.ClientRectangle.Top, 8, 8);
				Color color = base.HostControl.Enabled ? base.HostControl.BackColor : SystemColors.Control;
				using (new SolidBrush(color))
				{
					graphics.DrawIcon(icon, targetRect);
				}
			}
			base.HostControl.Enabled = false;
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x000410F4 File Offset: 0x0003F2F4
		private void DrawBulkEditorInitialState(string cueBanner)
		{
			if (!string.IsNullOrEmpty(cueBanner))
			{
				ExtendedDateTimePicker extendedDateTimePicker = base.HostControl as ExtendedDateTimePicker;
				using (Graphics graphics = extendedDateTimePicker.CreateGraphics())
				{
					TextFormatFlags flags = TextFormatFlags.EndEllipsis | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding;
					Rectangle clientRectangle = extendedDateTimePicker.ClientRectangle;
					clientRectangle.Width -= SystemInformation.VerticalScrollBarWidth;
					if (Application.RenderWithVisualStyles)
					{
						clientRectangle.Offset(2, 2);
						clientRectangle.Width--;
						clientRectangle.Height -= 4;
					}
					else
					{
						clientRectangle.Inflate(-2, -2);
					}
					Color color = extendedDateTimePicker.Enabled ? extendedDateTimePicker.BackColor : SystemColors.Control;
					using (SolidBrush solidBrush = new SolidBrush(color))
					{
						graphics.FillRectangle(solidBrush, clientRectangle);
					}
					TextRenderer.DrawText(graphics, cueBanner, extendedDateTimePicker.Font, clientRectangle, extendedDateTimePicker.ForeColor, color, flags);
				}
			}
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x000411F0 File Offset: 0x0003F3F0
		protected override void OnStateChanged(BulkEditorAdapter sender, BulkEditorStateEventArgs e)
		{
			base.OnStateChanged(sender, e);
			base.HostControl.Invalidate();
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x00041208 File Offset: 0x0003F408
		protected override IList<string> InnerGetManagedProperties()
		{
			IList<string> list = base.InnerGetManagedProperties();
			list.Add("Value");
			return list;
		}

		// Token: 0x04000667 RID: 1639
		private const string ManagedPropertyName = "Value";
	}
}
