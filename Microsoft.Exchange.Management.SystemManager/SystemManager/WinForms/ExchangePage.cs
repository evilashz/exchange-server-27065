using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.Exchange.Sqm;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000122 RID: 290
	[DefaultEvent("KillActive")]
	[DefaultProperty("BindingSource")]
	public class ExchangePage : BindableUserControlBase
	{
		// Token: 0x14000033 RID: 51
		// (add) Token: 0x06000B31 RID: 2865 RVA: 0x00027FA0 File Offset: 0x000261A0
		// (remove) Token: 0x06000B32 RID: 2866 RVA: 0x00027FD8 File Offset: 0x000261D8
		public event EventHandler ReadDataFailed;

		// Token: 0x06000B33 RID: 2867 RVA: 0x0002800D File Offset: 0x0002620D
		private void RegisterBindableControl(BindableUserControl control)
		{
			if (!this.bindableUnpagedControlList.Contains(control))
			{
				this.bindableUnpagedControlList.Add(control);
				this.InputValidationProvider.SetEnabled(control.BindingSource, true);
				this.inputValidationProvider.SetUIValidationEnabled(control, true);
			}
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00028048 File Offset: 0x00026248
		public ExchangePage()
		{
			this.BindingContext = new BindingContext();
			this.InitializeComponent();
			this.makeDirty = new EventHandler(this.MakeDirtyHandler);
			this.DoubleBuffered = true;
			this.bindableUnpagedControlList = new List<BindableUserControl>();
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x00028097 File Offset: 0x00026297
		// (set) Token: 0x06000B36 RID: 2870 RVA: 0x0002809F File Offset: 0x0002629F
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override AutoValidate AutoValidate
		{
			get
			{
				return base.AutoValidate;
			}
			set
			{
				base.AutoValidate = value;
			}
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x000280A8 File Offset: 0x000262A8
		private void InitializeComponent()
		{
			this.components = new Container();
			this.inputValidationProvider = new InputValidationProvider(this.components);
			((ISupportInitialize)base.BindingSource).BeginInit();
			base.SuspendLayout();
			this.inputValidationProvider.SetEnabled(base.BindingSource, true);
			this.inputValidationProvider.ContainerControl = this;
			this.inputValidationProvider.DataBoundPropertyChanged += this.MakeDirtyHandler;
			base.Name = "ExchangePage";
			((ISupportInitialize)base.BindingSource).EndInit();
			base.ResumeLayout(false);
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x00028134 File Offset: 0x00026334
		public InputValidationProvider InputValidationProvider
		{
			get
			{
				return this.inputValidationProvider;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x0002813C File Offset: 0x0002633C
		// (set) Token: 0x06000B3A RID: 2874 RVA: 0x00028144 File Offset: 0x00026344
		[DefaultValue(null)]
		public DataContext Context
		{
			get
			{
				return this.context;
			}
			set
			{
				if (value != this.Context)
				{
					if (this.Context != null)
					{
						this.Context.Pages.Remove(this);
					}
					this.context = value;
					if (this.Context != null)
					{
						this.Context.Pages.Add(this);
						AutomatedDataHandlerBase automatedDataHandlerBase = this.context.DataHandler as AutomatedDataHandlerBase;
						if (automatedDataHandlerBase != null)
						{
							base.BindingSource.DataSource = automatedDataHandlerBase.GetDataTableSchema();
						}
					}
					this.OnContextChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x000281C4 File Offset: 0x000263C4
		protected virtual void OnContextChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ExchangePage.EventContextChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06000B3C RID: 2876 RVA: 0x000281F2 File Offset: 0x000263F2
		// (remove) Token: 0x06000B3D RID: 2877 RVA: 0x00028205 File Offset: 0x00026405
		public event EventHandler ContextChanged
		{
			add
			{
				base.Events.AddHandler(ExchangePage.EventContextChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ExchangePage.EventContextChanged, value);
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x00028218 File Offset: 0x00026418
		public DataHandler DataHandler
		{
			get
			{
				if (this.Context == null)
				{
					return null;
				}
				return this.Context.DataHandler;
			}
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x00028230 File Offset: 0x00026430
		public override DialogResult ShowDialog(Form form)
		{
			Control control = base.FocusedControl ?? this;
			if (!this.OnKillActive())
			{
				return DialogResult.Cancel;
			}
			DialogResult result;
			try
			{
				DialogResult dialogResult = base.ShowDialog(form);
				if (dialogResult == DialogResult.OK && (this.Context == null || this.Context.IsDirty))
				{
					this.IsDirty = true;
				}
				result = dialogResult;
			}
			finally
			{
				this.OnSetActive();
				control.Focus();
			}
			return result;
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x000282A0 File Offset: 0x000264A0
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected ErrorProvider ErrorProvider
		{
			get
			{
				return this.InputValidationProvider.GetErrorProvider(base.BindingSource);
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x000282B3 File Offset: 0x000264B3
		// (set) Token: 0x06000B42 RID: 2882 RVA: 0x000282CF File Offset: 0x000264CF
		public string HelpTopic
		{
			get
			{
				if (this.helpTopic == null)
				{
					this.helpTopic = this.DefaultHelpTopic;
				}
				return this.helpTopic;
			}
			set
			{
				value = (value ?? "");
				if (this.HelpTopic != value)
				{
					this.helpTopic = value;
					this.OnHelpTopicChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x000282FD File Offset: 0x000264FD
		private bool ShouldSerializeHelpTopic()
		{
			return this.HelpTopic != this.DefaultHelpTopic;
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00028310 File Offset: 0x00026510
		private void ResetHelpTopic()
		{
			this.HelpTopic = this.DefaultHelpTopic;
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x00028320 File Offset: 0x00026520
		private bool InDesignMode
		{
			get
			{
				bool designMode = base.DesignMode;
				if (!designMode)
				{
					Form form = base.FindForm() as WizardForm;
					if (form != null && form.Site != null)
					{
						designMode = form.Site.DesignMode;
					}
				}
				return designMode;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x0002835B File Offset: 0x0002655B
		protected virtual string DefaultHelpTopic
		{
			get
			{
				return base.GetType().FullName;
			}
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00028368 File Offset: 0x00026568
		protected virtual void OnHelpTopicChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ExchangePage.EventHelpTopicChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x06000B48 RID: 2888 RVA: 0x00028396 File Offset: 0x00026596
		// (remove) Token: 0x06000B49 RID: 2889 RVA: 0x000283A9 File Offset: 0x000265A9
		public event EventHandler HelpTopicChanged
		{
			add
			{
				base.Events.AddHandler(ExchangePage.EventHelpTopicChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ExchangePage.EventHelpTopicChanged, value);
			}
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x000283BC File Offset: 0x000265BC
		protected override void OnHelpRequested(HelpEventArgs hevent)
		{
			if (!hevent.Handled)
			{
				ExchangeHelpService.ShowHelpFromPage(this);
				hevent.Handled = true;
			}
			base.OnHelpRequested(hevent);
		}

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x06000B4B RID: 2891 RVA: 0x000283DC File Offset: 0x000265DC
		// (remove) Token: 0x06000B4C RID: 2892 RVA: 0x00028414 File Offset: 0x00026614
		public event CancelEventHandler Cancel;

		// Token: 0x06000B4D RID: 2893 RVA: 0x00028449 File Offset: 0x00026649
		protected virtual void OnCancel(CancelEventArgs e)
		{
			if (this.Cancel != null)
			{
				this.Cancel(this, e);
			}
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00028460 File Offset: 0x00026660
		public bool NotifyCancel()
		{
			if (this.CanCancel)
			{
				CancelEventArgs cancelEventArgs = new CancelEventArgs(false);
				this.OnCancel(cancelEventArgs);
				return !cancelEventArgs.Cancel;
			}
			return false;
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x0002848E File Offset: 0x0002668E
		// (set) Token: 0x06000B50 RID: 2896 RVA: 0x00028496 File Offset: 0x00026696
		[DefaultValue(true)]
		public bool CanCancel
		{
			get
			{
				return this.canCancel;
			}
			set
			{
				if (this.CanCancel != value)
				{
					this.canCancel = value;
					this.OnCanCancelChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x000284B3 File Offset: 0x000266B3
		protected virtual void OnCanCancelChanged(EventArgs e)
		{
			if (this.CanCancelChanged != null)
			{
				this.CanCancelChanged(this, e);
			}
		}

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x06000B52 RID: 2898 RVA: 0x000284CC File Offset: 0x000266CC
		// (remove) Token: 0x06000B53 RID: 2899 RVA: 0x00028504 File Offset: 0x00026704
		public event EventHandler CanCancelChanged;

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x00028539 File Offset: 0x00026739
		// (set) Token: 0x06000B55 RID: 2901 RVA: 0x00028544 File Offset: 0x00026744
		[DefaultValue(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsDirty
		{
			get
			{
				return this.isDirty;
			}
			set
			{
				if (!this.suppressIsDirtyChanges && this.IsDirty != value && (this.Context == null || !this.Context.IsSaving))
				{
					this.isDirty = value;
					if (this.IsDirty && this.Context != null)
					{
						this.Context.IsDirty = true;
					}
					this.OnIsDirtyChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x000285A8 File Offset: 0x000267A8
		protected virtual void OnIsDirtyChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ExchangePage.EventIsDirtyChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06000B57 RID: 2903 RVA: 0x000285D6 File Offset: 0x000267D6
		// (remove) Token: 0x06000B58 RID: 2904 RVA: 0x000285E9 File Offset: 0x000267E9
		public event EventHandler IsDirtyChanged
		{
			add
			{
				SynchronizedDelegate.Combine(base.Events, ExchangePage.EventIsDirtyChanged, value);
			}
			remove
			{
				SynchronizedDelegate.Remove(base.Events, ExchangePage.EventIsDirtyChanged, value);
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x000285FC File Offset: 0x000267FC
		public EventHandler MakeDirty
		{
			get
			{
				return this.makeDirty;
			}
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00028604 File Offset: 0x00026804
		private void MakeDirtyHandler(object sender, EventArgs e)
		{
			this.IsDirty = true;
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x0002860D File Offset: 0x0002680D
		// (set) Token: 0x06000B5C RID: 2908 RVA: 0x00028615 File Offset: 0x00026815
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x0002861E File Offset: 0x0002681E
		// (set) Token: 0x06000B5E RID: 2910 RVA: 0x00028626 File Offset: 0x00026826
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[DefaultValue(false)]
		public bool IgnorePageValidation
		{
			get
			{
				return this.ignorePageValidation;
			}
			set
			{
				this.ignorePageValidation = value;
			}
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0002862F File Offset: 0x0002682F
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Context = null;
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0002864D File Offset: 0x0002684D
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public void OnSetActive()
		{
			this.OnSetActive(true);
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0002867C File Offset: 0x0002687C
		internal void OnSetActive(bool focusOnFirstChild)
		{
			this.suppressIsDirtyChanges = true;
			try
			{
				this.isActivePage = true;
				this.InputValidationProvider.SuspendRecordingModifiedBindingMembers = true;
				if (base.IsHandleCreated)
				{
					if (this.Context != null && !this.InDesignMode)
					{
						using (InvisibleForm invisibleForm = new InvisibleForm())
						{
							invisibleForm.BackgroundWorker.DoWork += delegate(object sender, DoWorkEventArgs e)
							{
								e.Result = this.Context.ReadData(new WinFormsCommandInteractionHandler(base.ShellUI), base.Name);
							};
							invisibleForm.ShowDialog(this);
							if (base.IsDisposed)
							{
								return;
							}
							bool flag = invisibleForm.ShowErrors(Strings.PropertyPageReadError, Strings.PropertyPageReadWarning, new WorkUnitCollection(), base.ShellUI);
							if (invisibleForm.AsyncResults != null)
							{
								base.BindingSource.DataSource = invisibleForm.AsyncResults;
								if (!this.CheckReadOnlyAndDisablePage())
								{
									this.VerifyCorruptedObject();
								}
							}
							else
							{
								this.DisableRelatedPages(true);
							}
							if (flag)
							{
								this.OnReadDataFailed(EventArgs.Empty);
							}
						}
					}
					this.OnSetActive(EventArgs.Empty);
					this.EnableBulkEditingOnDemand();
					EventHandler setActived = this.SetActived;
					if (setActived != null)
					{
						setActived(this, EventArgs.Empty);
					}
				}
			}
			finally
			{
				this.InputValidationProvider.SuspendRecordingModifiedBindingMembers = false;
				this.suppressIsDirtyChanges = false;
				if (focusOnFirstChild)
				{
					base.Focus();
					base.SelectNextControl(this, true, true, true, false);
				}
			}
		}

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06000B62 RID: 2914 RVA: 0x000287F4 File Offset: 0x000269F4
		// (remove) Token: 0x06000B63 RID: 2915 RVA: 0x0002882C File Offset: 0x00026A2C
		public event EventHandler SetActived;

		// Token: 0x06000B64 RID: 2916 RVA: 0x00028864 File Offset: 0x00026A64
		private void EnableBulkEditingOnDemand()
		{
			if (!this.enabledBulkEditors)
			{
				AutomatedDataHandlerBase automatedDataHandlerBase = this.DataHandler as AutomatedDataHandlerBase;
				if (automatedDataHandlerBase != null)
				{
					if (automatedDataHandlerBase.EnableBulkEdit)
					{
						this.EnableBulkEditingBindingSource(automatedDataHandlerBase);
					}
					else if (!(automatedDataHandlerBase is AutomatedDataHandler) || (automatedDataHandlerBase as AutomatedDataHandler).SaverExecutionContextFactory is MonadCommandExecutionContextForPropertyPageFactory)
					{
						this.EnableRbacBindingSource(automatedDataHandlerBase);
					}
				}
				this.enabledBulkEditors = true;
			}
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x000288C4 File Offset: 0x00026AC4
		private void EnableBulkEditingBindingSource(AutomatedDataHandlerBase dataHandler)
		{
			BindingManagerBase bindingManagerBase = this.BindingContext[base.BindingSource];
			object dataSource = base.BindingSource.DataSource;
			foreach (object obj in bindingManagerBase.Bindings)
			{
				Binding binding = (Binding)obj;
				IBulkEditor bulkEditor = binding.Control as IBulkEditor;
				if (bulkEditor != null)
				{
					BulkEditorState bulkEditorState = 0;
					if (!dataHandler.IsBulkEditingModifiedParameterName(dataSource, binding.BindingMemberInfo.BindingMember))
					{
						bulkEditorState = (dataHandler.IsBulkEditingSupportedParameterName(dataSource, binding.BindingMemberInfo.BindingMember) ? 1 : 2);
					}
					bulkEditor.BulkEditorAdapter[binding.PropertyName] = bulkEditorState;
				}
			}
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x00028998 File Offset: 0x00026B98
		private void EnableRbacBindingSource(AutomatedDataHandlerBase dataHandler)
		{
			if (dataHandler != null)
			{
				BindingManagerBase bindingManagerBase = this.BindingContext[base.BindingSource];
				foreach (object obj in bindingManagerBase.Bindings)
				{
					Binding binding = (Binding)obj;
					IBulkEditor bulkEditor = binding.Control as IBulkEditor;
					if (bulkEditor != null && !dataHandler.HasPermissionForProperty(binding.BindingMemberInfo.BindingMember, binding.DataSourceUpdateMode != DataSourceUpdateMode.Never))
					{
						bulkEditor.BulkEditorAdapter[binding.PropertyName] = 3;
					}
				}
			}
			EventHandler setActived = this.SetActived;
			if (setActived != null)
			{
				setActived(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x00028A64 File Offset: 0x00026C64
		protected void ForceIsDirty(bool value)
		{
			bool flag = this.suppressIsDirtyChanges;
			this.suppressIsDirtyChanges = false;
			this.IsDirty = value;
			this.suppressIsDirtyChanges = flag;
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x00028A8D File Offset: 0x00026C8D
		protected virtual void VerifyCorruptedObject()
		{
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00028A90 File Offset: 0x00026C90
		protected void DisableRelatedPages(bool removeContext)
		{
			List<ExchangePage> list = new List<ExchangePage>(this.Context.Pages);
			foreach (ExchangePage exchangePage in list)
			{
				exchangePage.Enabled = false;
				if (removeContext)
				{
					exchangePage.Context = null;
				}
			}
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x00028AFC File Offset: 0x00026CFC
		protected virtual void OnReadDataFailed(EventArgs e)
		{
			if (this.ReadDataFailed != null)
			{
				this.ReadDataFailed(this, e);
			}
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x00028B14 File Offset: 0x00026D14
		protected bool CheckReadOnlyAndDisablePage()
		{
			if (this.Context != null && this.Context.DataHandler != null && this.Context.DataHandler.IsObjectReadOnly)
			{
				base.Enabled = false;
				if (this.Context.Flags.NeedToShowVersionWarning)
				{
					base.ShellUI.ShowMessage(this.Context.DataHandler.ObjectReadOnlyReason);
					this.Context.Flags.NeedToShowVersionWarning = false;
				}
			}
			return !base.Enabled;
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x00028B98 File Offset: 0x00026D98
		protected virtual void OnSetActive(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ExchangePage.EventSetActive];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			if (ManagementGuiSqmSession.Instance.Enabled)
			{
				ManagementGuiSqmSession.Instance.AddToStreamDataPoint(SqmDataID.DATAID_EMC_GUI_ACTION, new object[]
				{
					2U,
					base.Name
				});
			}
		}

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x06000B6D RID: 2925 RVA: 0x00028BFB File Offset: 0x00026DFB
		// (remove) Token: 0x06000B6E RID: 2926 RVA: 0x00028C0E File Offset: 0x00026E0E
		public event EventHandler SetActive
		{
			add
			{
				base.Events.AddHandler(ExchangePage.EventSetActive, value);
			}
			remove
			{
				base.Events.RemoveHandler(ExchangePage.EventSetActive, value);
			}
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x00028C24 File Offset: 0x00026E24
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public bool OnKillActive()
		{
			CancelEventArgs cancelEventArgs = new CancelEventArgs();
			if (base.IsHandleCreated && base.Enabled)
			{
				cancelEventArgs.Cancel = !this.ValidateChildren(ValidationConstraints.Enabled | ValidationConstraints.Visible);
				if (!cancelEventArgs.Cancel && !this.IgnorePageValidation)
				{
					List<ValidationError> list = new List<ValidationError>();
					foreach (BindableUserControl control in this.bindableUnpagedControlList)
					{
						this.DealWithErrorForBindableUserControl(list, control);
					}
					this.OnValidating(cancelEventArgs);
					if (!cancelEventArgs.Cancel)
					{
						this.OnValidated(EventArgs.Empty);
						StringBuilder stringBuilder = new StringBuilder();
						foreach (ValidationError validationError in list)
						{
							stringBuilder.Append(" - ").AppendLine(validationError.Description);
						}
						try
						{
							this.OnKillActive(cancelEventArgs);
						}
						catch (Exception ex)
						{
							base.ShowError(ex.Message);
							cancelEventArgs.Cancel = true;
						}
						if (!cancelEventArgs.Cancel && base.Enabled)
						{
							Control control2 = null;
							string text = this.InputValidationProvider.GetErrorProviderMessages(this, ref control2);
							text += stringBuilder;
							if (!string.IsNullOrEmpty(text))
							{
								this.DisplayValidationError(text);
								if (control2 != null)
								{
									control2.Focus();
								}
								cancelEventArgs.Cancel = true;
							}
						}
					}
					if (!cancelEventArgs.Cancel)
					{
						cancelEventArgs.Cancel = !this.ValidateContext();
					}
				}
			}
			this.isActivePage = cancelEventArgs.Cancel;
			if (!cancelEventArgs.Cancel && this.DataHandler != null)
			{
				this.DataHandler.SpecifyParameterNames(this.InputValidationProvider.ModifiedBindingMembers);
				this.InputValidationProvider.ModifiedBindingMembers.Clear();
			}
			return !cancelEventArgs.Cancel;
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00028E18 File Offset: 0x00027018
		private void DealWithErrorForBindableUserControl(List<ValidationError> unhandledErrors, BindableUserControl control)
		{
			if (!control.Visible || !control.Enabled || !string.IsNullOrEmpty(this.GetErrorMessage(control)))
			{
				return;
			}
			foreach (ValidationError validationError in control.Validator.Validate())
			{
				StrongTypeValidationError strongTypeValidationError = validationError as StrongTypeValidationError;
				if (strongTypeValidationError != null && (string.IsNullOrEmpty(strongTypeValidationError.PropertyName) || !this.InputValidationProvider.UpdateErrorProviderTextForProperty(strongTypeValidationError.Description, strongTypeValidationError.PropertyName)))
				{
					unhandledErrors.Add(validationError);
				}
			}
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00028EA0 File Offset: 0x000270A0
		protected virtual void OnKillActive(CancelEventArgs e)
		{
			CancelEventHandler cancelEventHandler = (CancelEventHandler)base.Events[ExchangePage.EventKillActive];
			if (cancelEventHandler != null)
			{
				cancelEventHandler(this, e);
			}
		}

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x06000B72 RID: 2930 RVA: 0x00028ECE File Offset: 0x000270CE
		// (remove) Token: 0x06000B73 RID: 2931 RVA: 0x00028EE1 File Offset: 0x000270E1
		public event CancelEventHandler KillActive
		{
			add
			{
				base.Events.AddHandler(ExchangePage.EventKillActive, value);
			}
			remove
			{
				base.Events.RemoveHandler(ExchangePage.EventKillActive, value);
			}
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x00028EF4 File Offset: 0x000270F4
		private bool ValidateContext()
		{
			bool result = true;
			if (!this.IgnorePageValidation && this.Context != null)
			{
				string text = null;
				try
				{
					ValidationError[] errors = this.ValidateContextOnPageTransition();
					List<ValidationError> list = this.FilterErrorsFromCurrentPage(errors);
					if (list.Count > 0)
					{
						text = this.CreateValidateContextErrorMessageFor(list);
					}
				}
				catch (LocalizedException ex)
				{
					text = ex.Message;
				}
				catch (ArgumentException ex2)
				{
					text = ex2.Message;
				}
				if (!string.IsNullOrEmpty(text))
				{
					result = false;
					this.DisplayValidationError(text);
				}
			}
			return result;
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x00028F84 File Offset: 0x00027184
		private void DisplayValidationError(string message)
		{
			string[] array = message.Split(new string[]
			{
				Environment.NewLine
			}, StringSplitOptions.RemoveEmptyEntries);
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string str in array)
			{
				stringBuilder.AppendLine(" - " + str);
			}
			base.ShowError(Strings.InvalidControls + stringBuilder.ToString());
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x00028FF8 File Offset: 0x000271F8
		protected virtual ValidationError[] ValidateContextOnPageTransition()
		{
			return this.Context.Validate();
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x00029008 File Offset: 0x00027208
		protected LocalizedString CreateValidateContextErrorMessageFor(List<ValidationError> errors)
		{
			object[] array = new object[errors.Count];
			for (int i = 0; i < errors.Count; i++)
			{
				ValidationError validationError = errors[i];
				array[i] = validationError.Description;
				PropertyValidationError propertyValidationError = validationError as PropertyValidationError;
				if (propertyValidationError != null && propertyValidationError.PropertyDefinition != null)
				{
					this.InputValidationProvider.UpdateErrorProviderTextForProperty(propertyValidationError.Description, propertyValidationError.PropertyDefinition.Name);
				}
			}
			return LocalizedString.Join(Environment.NewLine, array);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x00029088 File Offset: 0x00027288
		private List<ValidationError> FilterErrorsFromCurrentPage(ValidationError[] errors)
		{
			List<ValidationError> list = new List<ValidationError>(errors.Length);
			foreach (ValidationError validationError in errors)
			{
				PropertyValidationError propertyError = validationError as PropertyValidationError;
				if (this.BlockPageSwitchWithError(propertyError))
				{
					list.Add(validationError);
				}
			}
			return list;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x000290CF File Offset: 0x000272CF
		protected virtual bool BlockPageSwitchWithError(PropertyValidationError propertyError)
		{
			return propertyError == null || this.InputValidationProvider.IsBoundToProperty(propertyError.PropertyDefinition.Name);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x000290EC File Offset: 0x000272EC
		protected override void OnLoad(EventArgs e)
		{
			this.RegisterAllBinableUserControl(this);
			base.OnLoad(e);
			if (this.isActivePage)
			{
				this.OnSetActive();
			}
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0002910C File Offset: 0x0002730C
		private void RegisterAllBinableUserControl(Control parent)
		{
			if (parent == null)
			{
				return;
			}
			if (parent != this && parent is BindableUserControl)
			{
				this.RegisterBindableControl(parent as BindableUserControl);
				return;
			}
			foreach (object obj in parent.Controls)
			{
				Control parent2 = (Control)obj;
				this.RegisterAllBinableUserControl(parent2);
			}
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x00029184 File Offset: 0x00027384
		protected override IUIService CreateUIService()
		{
			return new ExchangePage.ExchangePageUIService(this);
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000B7D RID: 2941 RVA: 0x0002918C File Offset: 0x0002738C
		protected bool HasErrorsProviders
		{
			get
			{
				return !string.IsNullOrEmpty(this.GetErrorMessage(this));
			}
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x000291A0 File Offset: 0x000273A0
		private string GetErrorMessage(Control topControl)
		{
			Control control = null;
			return this.InputValidationProvider.GetErrorProviderMessages(topControl, ref control);
		}

		// Token: 0x040004AF RID: 1199
		private static readonly object EventIsDirtyChanged = new object();

		// Token: 0x040004B0 RID: 1200
		private static readonly object EventSetActive = new object();

		// Token: 0x040004B1 RID: 1201
		private static readonly object EventKillActive = new object();

		// Token: 0x040004B2 RID: 1202
		[AccessedThroughProperty("InputValidationProvider")]
		private InputValidationProvider inputValidationProvider;

		// Token: 0x040004B3 RID: 1203
		private DataContext context;

		// Token: 0x040004B4 RID: 1204
		private bool isDirty;

		// Token: 0x040004B5 RID: 1205
		private bool suppressIsDirtyChanges;

		// Token: 0x040004B6 RID: 1206
		private readonly EventHandler makeDirty;

		// Token: 0x040004B7 RID: 1207
		private bool isActivePage;

		// Token: 0x040004B8 RID: 1208
		private IContainer components;

		// Token: 0x040004B9 RID: 1209
		private bool ignorePageValidation;

		// Token: 0x040004BA RID: 1210
		protected IList<BindableUserControl> bindableUnpagedControlList;

		// Token: 0x040004BB RID: 1211
		private static readonly object EventContextChanged = new object();

		// Token: 0x040004BC RID: 1212
		private string helpTopic;

		// Token: 0x040004BD RID: 1213
		private static readonly object EventHelpTopicChanged = new object();

		// Token: 0x040004BF RID: 1215
		private bool canCancel = true;

		// Token: 0x040004C2 RID: 1218
		private bool enabledBulkEditors;

		// Token: 0x02000124 RID: 292
		private class ExchangePageUIService : UIService
		{
			// Token: 0x06000B95 RID: 2965 RVA: 0x000296DE File Offset: 0x000278DE
			public ExchangePageUIService(ExchangePage page) : base(page)
			{
				this.page = page;
			}

			// Token: 0x06000B96 RID: 2966 RVA: 0x000296EE File Offset: 0x000278EE
			public override void SetUIDirty()
			{
				this.page.IsDirty = true;
			}

			// Token: 0x040004C5 RID: 1221
			private ExchangePage page;
		}
	}
}
