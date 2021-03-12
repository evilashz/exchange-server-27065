using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001DC RID: 476
	public class ExchangeComboBox : ComboBox, IBulkEditor, IOwnerDrawBulkEditSupport, IBulkEditSupport, IFormatModeProvider, IBindableComponent, IComponent, IDisposable
	{
		// Token: 0x06001574 RID: 5492 RVA: 0x000585C0 File Offset: 0x000567C0
		public ExchangeComboBox()
		{
			object oldSelectedItem = null;
			base.DataSourceChanged += delegate(object param0, EventArgs param1)
			{
				oldSelectedItem = this.SelectedItem;
			};
			base.SelectedValueChanged += delegate(object param0, EventArgs param1)
			{
				if (oldSelectedItem != null)
				{
					this.SelectedItem = oldSelectedItem;
					oldSelectedItem = null;
				}
			};
			base.DataBindings.CollectionChanged += this.DataBindings_CollectionChanged;
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x00058624 File Offset: 0x00056824
		private void DataBindings_CollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			if (!base.DesignMode && base.DropDownStyle != ComboBoxStyle.DropDownList)
			{
				Binding binding = (Binding)e.Element;
				if (e.Action == CollectionChangeAction.Add && binding.PropertyName == "Text" && this.constraintProvider == null)
				{
					this.constraintProvider = new TextBoxConstraintProvider(this, this);
				}
			}
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x0005867E File Offset: 0x0005687E
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			base.Select();
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x0005868D File Offset: 0x0005688D
		protected override void OnDropDown(EventArgs e)
		{
			this.UpdateDropDownWidth();
			base.OnDropDown(e);
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x0005869C File Offset: 0x0005689C
		private void UpdateDropDownWidth()
		{
			float num = 0f;
			foreach (object item in base.Items)
			{
				num = Math.Max(num, (float)TextRenderer.MeasureText(base.GetItemText(item), this.Font).Width);
			}
			num += (float)((base.MaxDropDownItems < base.Items.Count) ? SystemInformation.VerticalScrollBarWidth : 0);
			int num2 = (int)decimal.Round((decimal)num, 0);
			num2 = Math.Min(Screen.GetWorkingArea(this).Width, num2);
			base.DropDownWidth = Math.Max(num2, base.Width);
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x00058770 File Offset: 0x00056970
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 308)
			{
				this.PositionDropDown(m.LParam);
			}
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

		// Token: 0x0600157A RID: 5498 RVA: 0x000587DC File Offset: 0x000569DC
		private void PositionDropDown(IntPtr hWnd)
		{
			Rectangle workingArea = Screen.GetWorkingArea(this);
			Rectangle rectangle = base.RectangleToScreen(base.ClientRectangle);
			int num = rectangle.Left - workingArea.X;
			if (base.DropDownWidth > workingArea.Width - num && rectangle.Right < workingArea.Right)
			{
				int num2 = base.ItemHeight * Math.Min(base.Items.Count, base.MaxDropDownItems);
				int num3 = rectangle.Top - workingArea.Top - 2;
				int num4 = workingArea.Height - rectangle.Bottom;
				int num5;
				if (num2 <= num4 || num3 <= num4)
				{
					num5 = rectangle.Bottom;
				}
				else
				{
					num5 = num3 - num2 + workingArea.Top;
				}
				int num6 = workingArea.Right - base.DropDownWidth;
				UnsafeNativeMethods.SetWindowPos(hWnd, IntPtr.Zero, num6, num5, 0, 0, 1U);
			}
		}

		// Token: 0x14000088 RID: 136
		// (add) Token: 0x0600157B RID: 5499 RVA: 0x000588C8 File Offset: 0x00056AC8
		// (remove) Token: 0x0600157C RID: 5500 RVA: 0x00058900 File Offset: 0x00056B00
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler<PropertyChangedEventArgs> UserModified;

		// Token: 0x0600157D RID: 5501 RVA: 0x00058935 File Offset: 0x00056B35
		private void OnUserModified(EventArgs e)
		{
			if (this.UserModified != null)
			{
				this.UserModified(this, new PropertyChangedEventArgs("SelectedValue"));
			}
		}

		// Token: 0x14000089 RID: 137
		// (add) Token: 0x0600157E RID: 5502 RVA: 0x00058958 File Offset: 0x00056B58
		// (remove) Token: 0x0600157F RID: 5503 RVA: 0x00058990 File Offset: 0x00056B90
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler Painted;

		// Token: 0x06001580 RID: 5504 RVA: 0x000589C5 File Offset: 0x00056BC5
		private void OnPainted(EventArgs e)
		{
			if (this.Painted != null)
			{
				this.Painted(this, e);
			}
		}

		// Token: 0x1400008A RID: 138
		// (add) Token: 0x06001581 RID: 5505 RVA: 0x000589DC File Offset: 0x00056BDC
		// (remove) Token: 0x06001582 RID: 5506 RVA: 0x00058A14 File Offset: 0x00056C14
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public event EventHandler FocusSetted;

		// Token: 0x06001583 RID: 5507 RVA: 0x00058A49 File Offset: 0x00056C49
		private void OnFocusSetted(EventArgs e)
		{
			if (this.FocusSetted != null)
			{
				this.FocusSetted(this, e);
			}
		}

		// Token: 0x1400008B RID: 139
		// (add) Token: 0x06001584 RID: 5508 RVA: 0x00058A60 File Offset: 0x00056C60
		// (remove) Token: 0x06001585 RID: 5509 RVA: 0x00058A98 File Offset: 0x00056C98
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler FocusKilled;

		// Token: 0x06001586 RID: 5510 RVA: 0x00058ACD File Offset: 0x00056CCD
		private void OnFocusKilled(EventArgs e)
		{
			if (this.FocusKilled != null)
			{
				this.FocusKilled(this, e);
			}
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x00058AE4 File Offset: 0x00056CE4
		protected override void OnSelectionChangeCommitted(EventArgs e)
		{
			base.OnSelectionChangeCommitted(e);
			this.OnUserModified(EventArgs.Empty);
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001588 RID: 5512 RVA: 0x00058AF8 File Offset: 0x00056CF8
		BulkEditorAdapter IBulkEditor.BulkEditorAdapter
		{
			get
			{
				if (this.bulkEditorAdapter == null)
				{
					this.bulkEditorAdapter = new ComboBoxBulkEditorAdapter(this);
				}
				return this.bulkEditorAdapter;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001589 RID: 5513 RVA: 0x00058B14 File Offset: 0x00056D14
		// (set) Token: 0x0600158A RID: 5514 RVA: 0x00058B1C File Offset: 0x00056D1C
		[DefaultValue(0)]
		public DisplayFormatMode FormatMode
		{
			get
			{
				return this.formatMode;
			}
			set
			{
				if (this.formatMode != value)
				{
					this.formatMode = value;
					this.OnFormatChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x00058B3C File Offset: 0x00056D3C
		protected virtual void OnFormatChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ExchangeComboBox.EventFormatModeChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x1400008C RID: 140
		// (add) Token: 0x0600158C RID: 5516 RVA: 0x00058B6A File Offset: 0x00056D6A
		// (remove) Token: 0x0600158D RID: 5517 RVA: 0x00058B7D File Offset: 0x00056D7D
		public event EventHandler FormatModeChanged
		{
			add
			{
				base.Events.AddHandler(ExchangeComboBox.EventFormatModeChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ExchangeComboBox.EventFormatModeChanged, value);
			}
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x00058B9C File Offset: 0x00056D9C
		void IFormatModeProvider.add_BindingContextChanged(EventHandler A_1)
		{
			base.BindingContextChanged += A_1;
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x00058BA5 File Offset: 0x00056DA5
		void IFormatModeProvider.remove_BindingContextChanged(EventHandler A_1)
		{
			base.BindingContextChanged -= A_1;
		}

		// Token: 0x040007C5 RID: 1989
		private TextBoxConstraintProvider constraintProvider;

		// Token: 0x040007C6 RID: 1990
		private static readonly object EventFormatModeChanged = new object();

		// Token: 0x040007C7 RID: 1991
		private DisplayFormatMode formatMode;

		// Token: 0x040007CC RID: 1996
		private ComboBoxBulkEditorAdapter bulkEditorAdapter;
	}
}
