using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000197 RID: 407
	public sealed class ComboBoxBulkEditorAdapter : BulkEditorAdapter
	{
		// Token: 0x06001048 RID: 4168 RVA: 0x0003FE50 File Offset: 0x0003E050
		public ComboBoxBulkEditorAdapter(ExchangeComboBox comboBox) : base(comboBox)
		{
			comboBox.Painted += this.OnAppearancePainted;
			comboBox.FocusSetted += this.OnAppearancePainted;
			comboBox.FocusKilled += this.OnAppearancePainted;
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0003FE9C File Offset: 0x0003E09C
		protected override void OnStateChanged(BulkEditorAdapter sender, BulkEditorStateEventArgs e)
		{
			base.OnStateChanged(sender, e);
			base.HostControl.Invalidate();
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x0003FEB1 File Offset: 0x0003E0B1
		private void OnAppearancePainted(object sender, EventArgs e)
		{
			if (base["SelectedValue"] == 3)
			{
				this.DrawBulkEditorLockedState(Icons.LockIcon);
				return;
			}
			if (base["SelectedValue"] != null)
			{
				this.DrawBulkEditorInitialState(base.BulkEditingIndicatorText);
			}
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0003FEE8 File Offset: 0x0003E0E8
		private void DrawBulkEditorLockedState(Icon icon)
		{
			ExchangeComboBox exchangeComboBox = base.HostControl as ExchangeComboBox;
			using (Graphics graphics = exchangeComboBox.CreateGraphics())
			{
				Rectangle rectangle = new Rectangle(exchangeComboBox.ClientRectangle.Right - 8, exchangeComboBox.ClientRectangle.Top, 8, 8);
				Color color = exchangeComboBox.Enabled ? exchangeComboBox.BackColor : SystemColors.Control;
				using (new SolidBrush(color))
				{
					graphics.DrawIcon(icon, LayoutHelper.MirrorRectangle(rectangle, base.HostControl));
				}
			}
			exchangeComboBox.Enabled = false;
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0003FFA0 File Offset: 0x0003E1A0
		private void DrawBulkEditorInitialState(string cueBanner)
		{
			if (!string.IsNullOrEmpty(cueBanner))
			{
				ComboBoxBulkEditorAdapter.DrawComboBoxText(base.HostControl as ComboBox, cueBanner);
			}
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x0003FFBC File Offset: 0x0003E1BC
		public static void DrawComboBoxText(ComboBox comboBox, string text)
		{
			using (Graphics graphics = comboBox.CreateGraphics())
			{
				TextFormatFlags textFormatFlags = TextFormatFlags.EndEllipsis | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding;
				Rectangle clientRectangle = comboBox.ClientRectangle;
				clientRectangle.Width -= SystemInformation.VerticalScrollBarWidth;
				if (Application.RenderWithVisualStyles)
				{
					clientRectangle.Inflate(-2, -2);
				}
				else
				{
					clientRectangle.Inflate(-3, -3);
				}
				if (LayoutHelper.IsRightToLeft(comboBox))
				{
					textFormatFlags |= TextFormatFlags.Right;
					clientRectangle.Offset(SystemInformation.VerticalScrollBarWidth + 1, 0);
				}
				using (SolidBrush solidBrush = new SolidBrush(comboBox.Enabled ? SystemColors.Window : SystemColors.Control))
				{
					graphics.FillRectangle(solidBrush, clientRectangle);
				}
				TextRenderer.DrawText(graphics, text, comboBox.Font, clientRectangle, comboBox.ForeColor, comboBox.Enabled ? SystemColors.Window : SystemColors.Control, textFormatFlags);
				if (comboBox.ContainsFocus)
				{
					ControlPaint.DrawFocusRectangle(graphics, clientRectangle);
				}
			}
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x000400B8 File Offset: 0x0003E2B8
		protected override BulkEditorState InnerGetState(string propertyName)
		{
			return base.InnerGetState("SelectedValue");
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x000400C5 File Offset: 0x0003E2C5
		protected override void InnerSetState(string propertyName, BulkEditorState state)
		{
			base.InnerSetState("SelectedValue", state);
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x000400D4 File Offset: 0x0003E2D4
		protected override IList<string> InnerGetManagedProperties()
		{
			IList<string> list = base.InnerGetManagedProperties();
			list.Add("SelectedValue");
			list.Add("SelectedIndex");
			list.Add("SelectedItem");
			return list;
		}

		// Token: 0x0400065A RID: 1626
		private const string ManagedPropertyName = "SelectedValue";
	}
}
