using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.Services;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000DC RID: 220
	public class ExchangeUserControl : CustomUserControl, IServiceProvider, IBulkEditor, IBulkEditSupport
	{
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x00019C78 File Offset: 0x00017E78
		// (set) Token: 0x060007BC RID: 1980 RVA: 0x00019C80 File Offset: 0x00017E80
		public ICollection<UIValidationError> ValidationErrors
		{
			get
			{
				return this.validationErrors;
			}
			protected set
			{
				if (this.validationErrors != value)
				{
					if (this.ValidationErrorsChanging != null)
					{
						this.ValidationErrorsChanging(this, new UIValidationEventArgs(value));
					}
					this.validationErrors = value;
				}
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x00019CAC File Offset: 0x00017EAC
		public virtual Control ErrorProviderAnchor
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060007BE RID: 1982 RVA: 0x00019CB0 File Offset: 0x00017EB0
		// (remove) Token: 0x060007BF RID: 1983 RVA: 0x00019CE8 File Offset: 0x00017EE8
		public event ExchangeUserControl.UIValidationEventHandler ValidationErrorsChanging;

		// Token: 0x060007C0 RID: 1984 RVA: 0x00019D1D File Offset: 0x00017F1D
		public ExchangeUserControl()
		{
			base.SetStyle(Theme.UserPaintStyle | ControlStyles.CacheText, true);
			base.Name = "ExchangeUserControl";
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x00019D42 File Offset: 0x00017F42
		protected bool HasComponents
		{
			get
			{
				return this.components != null && this.components.Count != 0;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x00019D5F File Offset: 0x00017F5F
		protected IContainer Components
		{
			get
			{
				if (this.components == null)
				{
					this.components = new ServicedContainer(base.DesignMode ? null : this);
				}
				return this.components;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x00019D88 File Offset: 0x00017F88
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public Control FocusedControl
		{
			get
			{
				Control focusedControl = ExchangeUserControl.GetFocusedControl();
				Control control = focusedControl;
				while (control != null && control != this)
				{
					control = control.Parent;
				}
				if (control != this)
				{
					return null;
				}
				return focusedControl;
			}
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00019DB4 File Offset: 0x00017FB4
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00019DD3 File Offset: 0x00017FD3
		object IServiceProvider.GetService(Type serviceType)
		{
			if (serviceType == typeof(IUIService))
			{
				return this.ShellUI;
			}
			return this.GetService(serviceType);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00019DF5 File Offset: 0x00017FF5
		protected ISelectionService GetSelectionService()
		{
			return (ISelectionService)this.GetService(typeof(ISelectionService));
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00019E0C File Offset: 0x0001800C
		protected IServiceContainer GetServiceContainer()
		{
			return (IServiceContainer)this.GetService(typeof(IServiceContainer));
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x00019E24 File Offset: 0x00018024
		public IUIService ShellUI
		{
			get
			{
				IUIService iuiservice = (IUIService)this.GetService(typeof(IUIService));
				if (iuiservice == null)
				{
					iuiservice = this.CreateUIService();
				}
				return iuiservice;
			}
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00019E52 File Offset: 0x00018052
		protected virtual IUIService CreateUIService()
		{
			return new UIService(this);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00019E5A File Offset: 0x0001805A
		public void ShowError(string message)
		{
			this.ShellUI.ShowError(message);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00019E68 File Offset: 0x00018068
		public void ShowMessage(string message)
		{
			this.ShellUI.ShowMessage(message);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00019E76 File Offset: 0x00018076
		public DialogResult ShowMessage(string message, MessageBoxButtons buttons)
		{
			return this.ShellUI.ShowMessage(message, UIService.DefaultCaption, buttons);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00019E8A File Offset: 0x0001808A
		public virtual DialogResult ShowDialog(Form form)
		{
			return this.ShellUI.ShowDialog(form);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00019E98 File Offset: 0x00018098
		public DialogResult ShowDialog(ExchangePropertyPageControl propertyPage)
		{
			return this.ShowDialog(ExchangeUserControl.WrapPageAsDialog(propertyPage));
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00019EA8 File Offset: 0x000180A8
		public static ExchangePropertyPageControl WrapUserControlAsPage(BindableUserControl control)
		{
			ExchangePropertyPageControl exchangePropertyPageControl = new ExchangePropertyPageControl();
			exchangePropertyPageControl.SuspendLayout();
			control.Dock = DockStyle.Fill;
			exchangePropertyPageControl.Padding = new Padding(13, 12, 0, 12);
			exchangePropertyPageControl.Controls.Add(control);
			exchangePropertyPageControl.Text = control.Text;
			exchangePropertyPageControl.AutoSize = true;
			exchangePropertyPageControl.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			exchangePropertyPageControl.AutoScaleDimensions = ExchangeUserControl.DefaultAutoScaleDimension;
			exchangePropertyPageControl.AutoScaleMode = AutoScaleMode.Font;
			exchangePropertyPageControl.ResumeLayout(false);
			exchangePropertyPageControl.PerformLayout();
			exchangePropertyPageControl.HelpTopic = control.GetType().FullName;
			return exchangePropertyPageControl;
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00019F31 File Offset: 0x00018131
		public static PropertyPageDialog WrapUserControlAsDialog(BindableUserControl control)
		{
			return ExchangeUserControl.WrapPageAsDialog(ExchangeUserControl.WrapUserControlAsPage(control));
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00019F3E File Offset: 0x0001813E
		public static PropertyPageDialog WrapPageAsDialog(ExchangePropertyPageControl page)
		{
			return new PropertyPageDialog(page);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00019F46 File Offset: 0x00018146
		public DialogResult ShowDialog(BindableUserControl control)
		{
			return this.ShowDialog(ExchangeUserControl.WrapUserControlAsDialog(control));
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00019F54 File Offset: 0x00018154
		public DialogResult ShowDialog(string caption, string dialogHelpTopic, ExchangePropertyPageControl[] pages)
		{
			foreach (ExchangePropertyPageControl exchangePropertyPageControl in pages)
			{
				exchangePropertyPageControl.AutoScaleDimensions = ExchangeUserControl.DefaultAutoScaleDimension;
				exchangePropertyPageControl.AutoScaleMode = AutoScaleMode.Font;
			}
			DialogResult result;
			using (PropertySheetDialog propertySheetDialog = new PropertySheetDialog(caption, pages))
			{
				propertySheetDialog.HelpTopic = dialogHelpTopic;
				result = this.ShowDialog(propertySheetDialog);
			}
			return result;
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00019FC0 File Offset: 0x000181C0
		public IProgress CreateProgress(string operationName)
		{
			IProgressProvider progressProvider = (IProgressProvider)this.GetService(typeof(IProgressProvider));
			if (progressProvider != null)
			{
				return progressProvider.CreateProgress(operationName);
			}
			return NullProgress.Value;
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00019FF3 File Offset: 0x000181F3
		public static string RemoveAccelerator(string value)
		{
			if (ExchangeUserControl.removeDBCSAcceleratorRegEx.IsMatch(value))
			{
				return ExchangeUserControl.removeDBCSAcceleratorRegEx.Replace(value, "");
			}
			return ExchangeUserControl.removeAcceleratorRegEx.Replace(value, "$1");
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0001A024 File Offset: 0x00018224
		public static Control GetFocusedControl()
		{
			IntPtr focus = UnsafeNativeMethods.GetFocus();
			if (!(focus == IntPtr.Zero))
			{
				return Control.FromChildHandle(focus);
			}
			return null;
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0001A04C File Offset: 0x0001824C
		protected override void OnValidating(CancelEventArgs e)
		{
			this.UpdateError(true);
			base.OnValidating(e);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0001A05C File Offset: 0x0001825C
		public void UpdateError()
		{
			this.UpdateError(false);
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0001A068 File Offset: 0x00018268
		private void UpdateError(bool force)
		{
			if (force || (this.ValidationErrors != null && this.ValidationErrors.Count > 0))
			{
				UIValidationError[] array = this.GetValidationErrors() ?? UIValidationError.None;
				if (force)
				{
					this.ValidationErrors = array;
					return;
				}
				List<UIValidationError> list = new List<UIValidationError>();
				foreach (UIValidationError uivalidationError in array)
				{
					foreach (UIValidationError uivalidationError2 in this.ValidationErrors)
					{
						if (uivalidationError.ErrorProviderAnchor == uivalidationError2.ErrorProviderAnchor)
						{
							list.Add(uivalidationError);
						}
					}
				}
				this.ValidationErrors = list;
			}
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0001A12C File Offset: 0x0001832C
		protected virtual UIValidationError[] GetValidationErrors()
		{
			return UIValidationError.None;
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0001A133 File Offset: 0x00018333
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			if (!base.Enabled)
			{
				this.ValidationErrors = UIValidationError.None;
			}
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0001A150 File Offset: 0x00018350
		protected override void OnLayout(LayoutEventArgs e)
		{
			bool vscroll = base.VScroll;
			base.OnLayout(e);
			if (!vscroll && base.VScroll)
			{
				this.AdjustFormScrollbars(this.AutoScroll);
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x0001A182 File Offset: 0x00018382
		// (set) Token: 0x060007DE RID: 2014 RVA: 0x0001A18A File Offset: 0x0001838A
		public new Padding Padding
		{
			get
			{
				return this.originPadding;
			}
			set
			{
				this.originPadding = value;
				base.Padding = LayoutHelper.RTLPadding(this.originPadding, this);
			}
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0001A1A5 File Offset: 0x000183A5
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.Padding = LayoutHelper.RTLPadding(this.originPadding, this);
			base.OnRightToLeftChanged(e);
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x0001A1C0 File Offset: 0x000183C0
		BulkEditorAdapter IBulkEditor.BulkEditorAdapter
		{
			get
			{
				if (this.bulkEditorAdapter == null)
				{
					this.bulkEditorAdapter = this.CreateBulkEditorAdapter();
				}
				return this.bulkEditorAdapter;
			}
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0001A1DC File Offset: 0x000183DC
		protected virtual BulkEditorAdapter CreateBulkEditorAdapter()
		{
			return new UserControlBulkEditorAdapter(this);
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x0001A1E4 File Offset: 0x000183E4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual Dictionary<string, HashSet<Control>> ExposedPropertyRelatedControls
		{
			get
			{
				Dictionary<string, HashSet<Control>> dictionary = new Dictionary<string, HashSet<Control>>();
				if (!string.IsNullOrEmpty(this.ExposedPropertyName))
				{
					dictionary.Add(this.ExposedPropertyName, this.GetChildControls());
				}
				return dictionary;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x0001A217 File Offset: 0x00018417
		protected virtual string ExposedPropertyName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0001A220 File Offset: 0x00018420
		protected HashSet<Control> GetChildControls()
		{
			HashSet<Control> directChildControls = this.GetDirectChildControls(this);
			foreach (object obj in base.Controls)
			{
				Control control = (Control)obj;
				if (typeof(TableLayoutPanel).IsAssignableFrom(control.GetType()))
				{
					directChildControls.UnionWith(this.GetDirectChildControls(control));
				}
			}
			return directChildControls;
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0001A2A0 File Offset: 0x000184A0
		private HashSet<Control> GetDirectChildControls(Control parentControl)
		{
			HashSet<Control> hashSet = new HashSet<Control>();
			foreach (object obj in parentControl.Controls)
			{
				Control control = (Control)obj;
				Control control2 = control;
				if (!typeof(TableLayoutPanel).IsAssignableFrom(control2.GetType()) && !typeof(Label).IsAssignableFrom(control2.GetType()))
				{
					if (control2 is AutoSizePanel && control2.Controls.Count == 1 && control2.Controls[0] is ExchangeTextBox)
					{
						control2 = control2.Controls[0];
					}
					hashSet.Add(control2);
				}
			}
			return hashSet;
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0001A370 File Offset: 0x00018570
		protected void NotifyExposedPropertyIsModified()
		{
			this.NotifyExposedPropertyIsModified(this.ExposedPropertyName);
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x060007E7 RID: 2023 RVA: 0x0001A380 File Offset: 0x00018580
		// (remove) Token: 0x060007E8 RID: 2024 RVA: 0x0001A3B8 File Offset: 0x000185B8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public event EventHandler<PropertyChangedEventArgs> UserModified;

		// Token: 0x060007E9 RID: 2025 RVA: 0x0001A3ED File Offset: 0x000185ED
		protected void NotifyExposedPropertyIsModified(string propertyName)
		{
			if (this.UserModified != null && !string.IsNullOrEmpty(propertyName))
			{
				this.UserModified(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x0400039C RID: 924
		internal static readonly SizeF DefaultAutoScaleDimension = new SizeF(6f, 13f);

		// Token: 0x0400039D RID: 925
		private ICollection<UIValidationError> validationErrors;

		// Token: 0x0400039F RID: 927
		private ServicedContainer components;

		// Token: 0x040003A0 RID: 928
		private static Regex removeAcceleratorRegEx = new Regex("&([^&])", RegexOptions.Compiled);

		// Token: 0x040003A1 RID: 929
		private static Regex removeDBCSAcceleratorRegEx = new Regex("\\(&([^&])\\)", RegexOptions.Compiled);

		// Token: 0x040003A2 RID: 930
		private Padding originPadding;

		// Token: 0x040003A3 RID: 931
		protected BulkEditorAdapter bulkEditorAdapter;

		// Token: 0x020000DD RID: 221
		// (Invoke) Token: 0x060007EC RID: 2028
		public delegate void UIValidationEventHandler(object sender, UIValidationEventArgs e);
	}
}
