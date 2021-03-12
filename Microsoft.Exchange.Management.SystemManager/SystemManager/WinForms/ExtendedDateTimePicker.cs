using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Windows.Forms;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001DE RID: 478
	public class ExtendedDateTimePicker : DateTimePicker, IBulkEditor, IOwnerDrawBulkEditSupport, IBulkEditSupport
	{
		// Token: 0x060015A7 RID: 5543 RVA: 0x000591BE File Offset: 0x000573BE
		public ExtendedDateTimePicker()
		{
			base.Name = "ExtendedDateTimePicker";
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x000591D1 File Offset: 0x000573D1
		// (set) Token: 0x060015A9 RID: 5545 RVA: 0x000591D9 File Offset: 0x000573D9
		public override bool RightToLeftLayout
		{
			get
			{
				return LayoutHelper.IsRightToLeft(this);
			}
			set
			{
			}
		}

		// Token: 0x1400008E RID: 142
		// (add) Token: 0x060015AA RID: 5546 RVA: 0x000591DC File Offset: 0x000573DC
		// (remove) Token: 0x060015AB RID: 5547 RVA: 0x00059214 File Offset: 0x00057414
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler<PropertyChangedEventArgs> UserModified;

		// Token: 0x060015AC RID: 5548 RVA: 0x00059249 File Offset: 0x00057449
		private void OnUserModified(EventArgs e)
		{
			if (this.UserModified != null)
			{
				this.UserModified(this, new PropertyChangedEventArgs("Value"));
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x060015AD RID: 5549 RVA: 0x00059269 File Offset: 0x00057469
		BulkEditorAdapter IBulkEditor.BulkEditorAdapter
		{
			get
			{
				if (this.bulkEditorAdapter == null)
				{
					this.bulkEditorAdapter = new DateTimePickerBulkEditorAdapter(this);
				}
				return this.bulkEditorAdapter;
			}
		}

		// Token: 0x1400008F RID: 143
		// (add) Token: 0x060015AE RID: 5550 RVA: 0x00059288 File Offset: 0x00057488
		// (remove) Token: 0x060015AF RID: 5551 RVA: 0x000592C0 File Offset: 0x000574C0
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler Painted;

		// Token: 0x060015B0 RID: 5552 RVA: 0x000592F5 File Offset: 0x000574F5
		private void OnPainted(EventArgs e)
		{
			if (this.Painted != null)
			{
				this.Painted(this, e);
			}
		}

		// Token: 0x14000090 RID: 144
		// (add) Token: 0x060015B1 RID: 5553 RVA: 0x0005930C File Offset: 0x0005750C
		// (remove) Token: 0x060015B2 RID: 5554 RVA: 0x00059344 File Offset: 0x00057544
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler FocusSetted;

		// Token: 0x060015B3 RID: 5555 RVA: 0x00059379 File Offset: 0x00057579
		private void OnFocusSetted(EventArgs e)
		{
			if (this.FocusSetted != null)
			{
				this.FocusSetted(this, e);
			}
		}

		// Token: 0x14000091 RID: 145
		// (add) Token: 0x060015B4 RID: 5556 RVA: 0x00059390 File Offset: 0x00057590
		// (remove) Token: 0x060015B5 RID: 5557 RVA: 0x000593C8 File Offset: 0x000575C8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler FocusKilled;

		// Token: 0x060015B6 RID: 5558 RVA: 0x000593FD File Offset: 0x000575FD
		private void OnFocusKilled(EventArgs e)
		{
			if (this.FocusKilled != null)
			{
				this.FocusKilled(this, e);
			}
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x00059414 File Offset: 0x00057614
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

		// Token: 0x040007D3 RID: 2003
		private DateTimePickerBulkEditorAdapter bulkEditorAdapter;
	}
}
