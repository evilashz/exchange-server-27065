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
	// Token: 0x020001AC RID: 428
	[Designer(typeof(AutoHeightControlDesigner))]
	public class AutoHeightCheckBox : CheckBox, IBulkEditor, IButtonBaseBulkEditSupport, IOwnerDrawBulkEditSupport, IBulkEditSupport
	{
		// Token: 0x060010D1 RID: 4305 RVA: 0x00042BE5 File Offset: 0x00040DE5
		public AutoHeightCheckBox()
		{
			this.TextAlign = ContentAlignment.TopLeft;
			this.CheckAlign = ContentAlignment.TopLeft;
			this.AutoSize = true;
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x060010D2 RID: 4306 RVA: 0x00042C19 File Offset: 0x00040E19
		// (set) Token: 0x060010D3 RID: 4307 RVA: 0x00042C21 File Offset: 0x00040E21
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

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x060010D4 RID: 4308 RVA: 0x00042C2A File Offset: 0x00040E2A
		// (set) Token: 0x060010D5 RID: 4309 RVA: 0x00042C32 File Offset: 0x00040E32
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

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x060010D6 RID: 4310 RVA: 0x00042C3B File Offset: 0x00040E3B
		// (set) Token: 0x060010D7 RID: 4311 RVA: 0x00042C43 File Offset: 0x00040E43
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

		// Token: 0x060010D8 RID: 4312 RVA: 0x00042C4C File Offset: 0x00040E4C
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

		// Token: 0x060010D9 RID: 4313 RVA: 0x00042CA8 File Offset: 0x00040EA8
		protected override void OnTextChanged(EventArgs e)
		{
			this.CacheTextSize();
			base.OnTextChanged(e);
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x00042CB7 File Offset: 0x00040EB7
		protected override void OnFontChanged(EventArgs e)
		{
			this.CacheTextSize();
			base.OnFontChanged(e);
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x00042CC8 File Offset: 0x00040EC8
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

		// Token: 0x14000061 RID: 97
		// (add) Token: 0x060010DC RID: 4316 RVA: 0x00042DC8 File Offset: 0x00040FC8
		// (remove) Token: 0x060010DD RID: 4317 RVA: 0x00042E00 File Offset: 0x00041000
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public event EventHandler Painted;

		// Token: 0x060010DE RID: 4318 RVA: 0x00042E35 File Offset: 0x00041035
		private void OnPainted(EventArgs e)
		{
			if (this.Painted != null)
			{
				this.Painted(this, e);
			}
		}

		// Token: 0x14000062 RID: 98
		// (add) Token: 0x060010DF RID: 4319 RVA: 0x00042E4C File Offset: 0x0004104C
		// (remove) Token: 0x060010E0 RID: 4320 RVA: 0x00042E84 File Offset: 0x00041084
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler FocusSetted;

		// Token: 0x060010E1 RID: 4321 RVA: 0x00042EB9 File Offset: 0x000410B9
		private void OnFocusSetted(EventArgs e)
		{
			if (this.FocusSetted != null)
			{
				this.FocusSetted(this, e);
			}
		}

		// Token: 0x14000063 RID: 99
		// (add) Token: 0x060010E2 RID: 4322 RVA: 0x00042ED0 File Offset: 0x000410D0
		// (remove) Token: 0x060010E3 RID: 4323 RVA: 0x00042F08 File Offset: 0x00041108
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public event EventHandler FocusKilled;

		// Token: 0x060010E4 RID: 4324 RVA: 0x00042F3D File Offset: 0x0004113D
		private void OnFocusKilled(EventArgs e)
		{
			if (this.FocusKilled != null)
			{
				this.FocusKilled(this, e);
			}
		}

		// Token: 0x14000064 RID: 100
		// (add) Token: 0x060010E5 RID: 4325 RVA: 0x00042F54 File Offset: 0x00041154
		// (remove) Token: 0x060010E6 RID: 4326 RVA: 0x00042F8C File Offset: 0x0004118C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler<PropertyChangedEventArgs> UserModified;

		// Token: 0x060010E7 RID: 4327 RVA: 0x00042FC1 File Offset: 0x000411C1
		private void OnUserModified(EventArgs e)
		{
			if (this.UserModified != null)
			{
				this.UserModified(this, new PropertyChangedEventArgs("Checked"));
			}
		}

		// Token: 0x14000065 RID: 101
		// (add) Token: 0x060010E8 RID: 4328 RVA: 0x00042FE4 File Offset: 0x000411E4
		// (remove) Token: 0x060010E9 RID: 4329 RVA: 0x0004301C File Offset: 0x0004121C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event HandledEventHandler CheckedChangedRaising;

		// Token: 0x060010EA RID: 4330 RVA: 0x00043051 File Offset: 0x00041251
		private void OnCheckedChangedRaising(HandledEventArgs e)
		{
			if (this.CheckedChangedRaising != null)
			{
				this.CheckedChangedRaising(this, e);
			}
		}

		// Token: 0x14000066 RID: 102
		// (add) Token: 0x060010EB RID: 4331 RVA: 0x00043068 File Offset: 0x00041268
		// (remove) Token: 0x060010EC RID: 4332 RVA: 0x000430A0 File Offset: 0x000412A0
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public event HandledEventHandler Entering;

		// Token: 0x060010ED RID: 4333 RVA: 0x000430D5 File Offset: 0x000412D5
		private void OnEntering(HandledEventArgs e)
		{
			if (this.Entering != null)
			{
				this.Entering(this, e);
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x060010EE RID: 4334 RVA: 0x000430EC File Offset: 0x000412EC
		// (set) Token: 0x060010EF RID: 4335 RVA: 0x000430F4 File Offset: 0x000412F4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x060010F0 RID: 4336 RVA: 0x00043100 File Offset: 0x00041300
		protected override void OnEnter(EventArgs e)
		{
			HandledEventArgs handledEventArgs = new HandledEventArgs();
			this.OnEntering(handledEventArgs);
			if (!handledEventArgs.Handled)
			{
				base.OnEnter(e);
			}
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x0004312C File Offset: 0x0004132C
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

		// Token: 0x060010F2 RID: 4338 RVA: 0x0004317F File Offset: 0x0004137F
		protected override void OnClick(EventArgs e)
		{
			this.OnUserModified(e);
			base.OnClick(e);
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x00043190 File Offset: 0x00041390
		protected override void OnCheckedChanged(EventArgs e)
		{
			HandledEventArgs handledEventArgs = new HandledEventArgs();
			this.OnCheckedChangedRaising(handledEventArgs);
			if (!handledEventArgs.Handled)
			{
				base.OnCheckedChanged(e);
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x060010F4 RID: 4340 RVA: 0x000431B9 File Offset: 0x000413B9
		BulkEditorAdapter IBulkEditor.BulkEditorAdapter
		{
			get
			{
				if (this.bulkEditorAdapter == null)
				{
					this.bulkEditorAdapter = new CheckBoxBulkEditorAdapter(this);
				}
				return this.bulkEditorAdapter;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x000431D5 File Offset: 0x000413D5
		// (set) Token: 0x060010F6 RID: 4342 RVA: 0x000431DD File Offset: 0x000413DD
		[DefaultValue(false)]
		public bool BulkEditDefaultChecked { get; set; }

		// Token: 0x0400068E RID: 1678
		private Size cachedSizeOfOneLineOfText = Size.Empty;

		// Token: 0x0400068F RID: 1679
		private Dictionary<Size, Size> preferredSizeHash = new Dictionary<Size, Size>(3);

		// Token: 0x04000696 RID: 1686
		private CheckBoxBulkEditorAdapter bulkEditorAdapter;
	}
}
