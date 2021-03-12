using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200019A RID: 410
	public sealed class RadioButtonBulkEditorAdapter : ButtonBaseBulkEditorAdapter
	{
		// Token: 0x0600105D RID: 4189 RVA: 0x00040310 File Offset: 0x0003E510
		public RadioButtonBulkEditorAdapter(AutoHeightRadioButton radioButton) : base(radioButton)
		{
			this.bulkEditSupport.Entering += this.OnEntering;
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x00040330 File Offset: 0x0003E530
		private void OnEntering(object sender, HandledEventArgs e)
		{
			e.Handled = (base["Checked"] != 0);
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x0004034C File Offset: 0x0003E54C
		protected override void OnOwnerDraw(Graphics g)
		{
			RadioButton radioButton = base.HostControl as RadioButton;
			RadioButtonState state = radioButton.Focused ? RadioButtonState.UncheckedHot : RadioButtonState.UncheckedNormal;
			System.Drawing.ContentAlignment checkAlign = radioButton.CheckAlign;
			Size glyphSize = RadioButtonRenderer.GetGlyphSize(g, state);
			Rectangle rectangle = base.CalculateCheckBounds(checkAlign, glyphSize);
			if (base["Checked"] == 3)
			{
				int num = LayoutHelper.IsRightToLeft(base.HostControl) ? 12 : 8;
				Rectangle targetRect = new Rectangle(rectangle.Left + rectangle.Width - num, rectangle.Top, 8, 8);
				Color color = radioButton.Enabled ? radioButton.BackColor : SystemColors.Control;
				using (new SolidBrush(color))
				{
					g.DrawIcon(Icons.LockIcon, targetRect);
				}
				radioButton.Enabled = false;
				return;
			}
			if (Application.RenderWithVisualStyles)
			{
				RadioButtonRenderer.DrawRadioButton(g, rectangle.Location, state);
				return;
			}
			rectangle.X--;
			ControlPaint.DrawRadioButton(g, rectangle, ButtonState.Normal);
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x00040450 File Offset: 0x0003E650
		protected override void OnStateChanged(BulkEditorAdapter sender, BulkEditorStateEventArgs e)
		{
			base.OnStateChanged(sender, e);
			AutoHeightRadioButton autoHeightRadioButton = base.HostControl as AutoHeightRadioButton;
			bool @checked = autoHeightRadioButton.Checked;
			RadioButtonBulkEditorAdapter.UpdatePeerRadioButtons(autoHeightRadioButton, base["Checked"]);
			if (base["Checked"] != null && base["Checked"] != 3)
			{
				this.forceAllowCheckedChangedEvent = true;
				autoHeightRadioButton.Checked = autoHeightRadioButton.BulkEditDefaultChecked;
				this.forceAllowCheckedChangedEvent = false;
				autoHeightRadioButton.Checked = @checked;
				Binding binding = autoHeightRadioButton.DataBindings["Checked"];
				if (binding != null)
				{
					binding.WriteValue();
				}
				autoHeightRadioButton.TabStop = true;
			}
			else if (base["Checked"] != 3)
			{
				autoHeightRadioButton.Checked = !autoHeightRadioButton.Checked;
			}
			autoHeightRadioButton.Invalidate();
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0004050C File Offset: 0x0003E70C
		private static void UpdatePeerRadioButtons(RadioButton radioButton, BulkEditorState state)
		{
			if (radioButton.Parent != null)
			{
				foreach (object obj in radioButton.Parent.Controls)
				{
					Control control = (Control)obj;
					if (control is RadioButton)
					{
						IBulkEditor bulkEditor = control as IBulkEditor;
						RadioButtonBulkEditorAdapter radioButtonBulkEditorAdapter = bulkEditor.BulkEditorAdapter as RadioButtonBulkEditorAdapter;
						if (radioButtonBulkEditorAdapter != null)
						{
							radioButtonBulkEditorAdapter["Checked"] = state;
						}
					}
				}
			}
		}
	}
}
