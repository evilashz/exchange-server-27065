using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;
using Microsoft.ManagementGUI.WinForms;
using Microsoft.ManagementGUI.WinForms.Design;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001AD RID: 429
	[Designer(typeof(AutoHeightControlDesigner))]
	public class AutoHeightRadioButton : RadioButton, IBulkEditor, IButtonBaseBulkEditSupport, IOwnerDrawBulkEditSupport, IBulkEditSupport
	{
		// Token: 0x060010F7 RID: 4343 RVA: 0x000431E6 File Offset: 0x000413E6
		public AutoHeightRadioButton()
		{
			this.TextAlign = ContentAlignment.TopLeft;
			this.CheckAlign = ContentAlignment.TopLeft;
			this.AutoSize = true;
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x060010F8 RID: 4344 RVA: 0x0004321A File Offset: 0x0004141A
		// (set) Token: 0x060010F9 RID: 4345 RVA: 0x00043222 File Offset: 0x00041422
		[DefaultValue(true)]
		public new bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x060010FA RID: 4346 RVA: 0x0004322B File Offset: 0x0004142B
		// (set) Token: 0x060010FB RID: 4347 RVA: 0x00043233 File Offset: 0x00041433
		[DefaultValue(ContentAlignment.TopLeft)]
		public override ContentAlignment TextAlign
		{
			get
			{
				return base.TextAlign;
			}
			set
			{
				base.TextAlign = value;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x0004323C File Offset: 0x0004143C
		// (set) Token: 0x060010FD RID: 4349 RVA: 0x00043244 File Offset: 0x00041444
		[DefaultValue(ContentAlignment.TopLeft)]
		public new ContentAlignment CheckAlign
		{
			get
			{
				return base.CheckAlign;
			}
			set
			{
				base.CheckAlign = value;
			}
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x00043250 File Offset: 0x00041450
		private void CacheTextSize()
		{
			this.preferredSizeHash.Clear();
			if (string.IsNullOrEmpty(this.Text))
			{
				this.cachedSizeOfOneLineOfText = Size.Empty;
				return;
			}
			this.cachedSizeOfOneLineOfText = TextRenderer.MeasureText(this.Text, this.Font, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.TextBoxControl | TextFormatFlags.WordBreak);
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x000432AC File Offset: 0x000414AC
		protected override void OnTextChanged(EventArgs e)
		{
			this.CacheTextSize();
			base.OnTextChanged(e);
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x000432BB File Offset: 0x000414BB
		protected override void OnFontChanged(EventArgs e)
		{
			this.CacheTextSize();
			base.OnFontChanged(e);
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x000432CC File Offset: 0x000414CC
		public override Size GetPreferredSize(Size proposedSize)
		{
			Size size = base.GetPreferredSize(proposedSize);
			if (size.Width > proposedSize.Width && !string.IsNullOrEmpty(this.Text) && !proposedSize.Height.Equals(2147483647))
			{
				Size size2 = size - this.cachedSizeOfOneLineOfText;
				Size size3 = proposedSize - size2;
				if (!this.preferredSizeHash.ContainsKey(size3))
				{
					size3.Width = ((size3.Width > 0) ? size3.Width : (base.Size.Width - size2.Width));
					size3.Height = ((size3.Height > 0) ? size3.Height : int.MaxValue);
					size = size2 + TextRenderer.MeasureText(this.Text, this.Font, size3, TextFormatFlags.TextBoxControl | TextFormatFlags.WordBreak);
					this.preferredSizeHash[size3] = size;
				}
				else
				{
					size = this.preferredSizeHash[size3];
				}
			}
			return size;
		}

		// Token: 0x14000067 RID: 103
		// (add) Token: 0x06001102 RID: 4354 RVA: 0x000433CC File Offset: 0x000415CC
		// (remove) Token: 0x06001103 RID: 4355 RVA: 0x00043404 File Offset: 0x00041604
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public event EventHandler Painted;

		// Token: 0x06001104 RID: 4356 RVA: 0x00043439 File Offset: 0x00041639
		private void OnPainted(EventArgs e)
		{
			if (this.Painted != null)
			{
				this.Painted(this, e);
			}
		}

		// Token: 0x14000068 RID: 104
		// (add) Token: 0x06001105 RID: 4357 RVA: 0x00043450 File Offset: 0x00041650
		// (remove) Token: 0x06001106 RID: 4358 RVA: 0x00043488 File Offset: 0x00041688
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public event EventHandler FocusSetted;

		// Token: 0x06001107 RID: 4359 RVA: 0x000434BD File Offset: 0x000416BD
		private void OnFocusSetted(EventArgs e)
		{
			if (this.FocusSetted != null)
			{
				this.FocusSetted(this, e);
			}
		}

		// Token: 0x14000069 RID: 105
		// (add) Token: 0x06001108 RID: 4360 RVA: 0x000434D4 File Offset: 0x000416D4
		// (remove) Token: 0x06001109 RID: 4361 RVA: 0x0004350C File Offset: 0x0004170C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler FocusKilled;

		// Token: 0x0600110A RID: 4362 RVA: 0x00043541 File Offset: 0x00041741
		private void OnFocusKilled(EventArgs e)
		{
			if (this.FocusKilled != null)
			{
				this.FocusKilled(this, e);
			}
		}

		// Token: 0x1400006A RID: 106
		// (add) Token: 0x0600110B RID: 4363 RVA: 0x00043558 File Offset: 0x00041758
		// (remove) Token: 0x0600110C RID: 4364 RVA: 0x00043590 File Offset: 0x00041790
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler<PropertyChangedEventArgs> UserModified;

		// Token: 0x0600110D RID: 4365 RVA: 0x000435C5 File Offset: 0x000417C5
		private void OnUserModified(EventArgs e)
		{
			if (this.UserModified != null)
			{
				this.UserModified(this, new PropertyChangedEventArgs("Checked"));
			}
		}

		// Token: 0x1400006B RID: 107
		// (add) Token: 0x0600110E RID: 4366 RVA: 0x000435E8 File Offset: 0x000417E8
		// (remove) Token: 0x0600110F RID: 4367 RVA: 0x00043620 File Offset: 0x00041820
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event HandledEventHandler Entering;

		// Token: 0x06001110 RID: 4368 RVA: 0x00043655 File Offset: 0x00041855
		private void OnEntering(HandledEventArgs e)
		{
			if (this.Entering != null)
			{
				this.Entering(this, e);
			}
		}

		// Token: 0x1400006C RID: 108
		// (add) Token: 0x06001111 RID: 4369 RVA: 0x0004366C File Offset: 0x0004186C
		// (remove) Token: 0x06001112 RID: 4370 RVA: 0x000436A4 File Offset: 0x000418A4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public event HandledEventHandler CheckedChangedRaising;

		// Token: 0x06001113 RID: 4371 RVA: 0x000436D9 File Offset: 0x000418D9
		private void OnCheckedChangedRaising(HandledEventArgs e)
		{
			if (this.CheckedChangedRaising != null)
			{
				this.CheckedChangedRaising(this, e);
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x000436F0 File Offset: 0x000418F0
		// (set) Token: 0x06001115 RID: 4373 RVA: 0x000436F8 File Offset: 0x000418F8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CheckedValue
		{
			get
			{
				return base.Checked;
			}
			set
			{
				base.Checked = value;
			}
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x00043704 File Offset: 0x00041904
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			if (m.Msg == 15)
			{
				this.OnPainted(EventArgs.Empty);
				return;
			}
			if (m.Msg == 7)
			{
				this.OnFocusSetted(EventArgs.Empty);
				return;
			}
			if (m.Msg == 8)
			{
				this.OnFocusKilled(EventArgs.Empty);
			}
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x00043758 File Offset: 0x00041958
		protected override void OnEnter(EventArgs e)
		{
			HandledEventArgs handledEventArgs = new HandledEventArgs();
			this.OnEntering(handledEventArgs);
			if (!handledEventArgs.Handled)
			{
				base.OnEnter(e);
			}
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x00043784 File Offset: 0x00041984
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessMnemonic(char charCode)
		{
			bool flag = base.ProcessMnemonic(charCode);
			if (flag)
			{
				base.PerformClick();
			}
			return flag;
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x000437A3 File Offset: 0x000419A3
		protected override void OnClick(EventArgs e)
		{
			this.OnUserModified(EventArgs.Empty);
			base.OnClick(e);
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x000437B8 File Offset: 0x000419B8
		protected override void OnCheckedChanged(EventArgs e)
		{
			HandledEventArgs handledEventArgs = new HandledEventArgs();
			this.OnCheckedChangedRaising(handledEventArgs);
			if (!handledEventArgs.Handled)
			{
				base.OnCheckedChanged(e);
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x000437E1 File Offset: 0x000419E1
		BulkEditorAdapter IBulkEditor.BulkEditorAdapter
		{
			get
			{
				if (this.bulkEditorAdapter == null)
				{
					this.bulkEditorAdapter = new RadioButtonBulkEditorAdapter(this);
				}
				return this.bulkEditorAdapter;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x0600111C RID: 4380 RVA: 0x000437FD File Offset: 0x000419FD
		// (set) Token: 0x0600111D RID: 4381 RVA: 0x00043805 File Offset: 0x00041A05
		[DefaultValue(false)]
		public bool BulkEditDefaultChecked { get; set; }

		// Token: 0x04000698 RID: 1688
		private Size cachedSizeOfOneLineOfText = Size.Empty;

		// Token: 0x04000699 RID: 1689
		private Dictionary<Size, Size> preferredSizeHash = new Dictionary<Size, Size>(3);

		// Token: 0x040006A0 RID: 1696
		private RadioButtonBulkEditorAdapter bulkEditorAdapter;
	}
}
