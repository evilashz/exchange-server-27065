using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000125 RID: 293
	public class ExchangePropertyPageControl : ExchangePage, IHasPermission
	{
		// Token: 0x06000B97 RID: 2967 RVA: 0x000296FC File Offset: 0x000278FC
		public ExchangePropertyPageControl()
		{
			base.Name = "ExchangePropertyPageControl";
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x0002970F File Offset: 0x0002790F
		protected override Size DefaultSize
		{
			get
			{
				return new Size(418, 396);
			}
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00029720 File Offset: 0x00027920
		public void Apply(CancelEventArgs e)
		{
			this.OnApplying(e);
			if (base.Context != null)
			{
				if (!base.Context.IsDirty)
				{
					base.Context.AllowNextRead();
				}
				else if (this.TryApply())
				{
					base.DataHandler.UpdateWorkUnits();
					if (base.DataHandler.TimeConsuming)
					{
						e.Cancel = !this.SaveDataContextByProgressDialog();
					}
					else
					{
						e.Cancel = !this.SaveDataContextByInvisibleForm();
					}
				}
				else
				{
					e.Cancel = true;
				}
				if (!e.Cancel)
				{
					base.IsDirty = false;
				}
			}
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x000297C8 File Offset: 0x000279C8
		private bool SaveDataContextByInvisibleForm()
		{
			bool result;
			using (InvisibleForm invisibleForm = new InvisibleForm())
			{
				invisibleForm.BackgroundWorker.DoWork += delegate(object sender, DoWorkEventArgs e2)
				{
					base.Context.SaveData(new WinFormsCommandInteractionHandler(base.ShellUI));
				};
				invisibleForm.ShowDialog(this);
				bool flag = !invisibleForm.ShowErrors(Strings.PropertyPageWriteError, Strings.PropertyPageWriteWarning, base.DataHandler.WorkUnits, base.ShellUI);
				result = flag;
			}
			return result;
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00029854 File Offset: 0x00027A54
		private bool ConfirmedToSaveDataContext()
		{
			bool result = false;
			if (base.Context.IsDirty)
			{
				using (BulkEditSummaryPage bulkEditSummaryPage = new BulkEditSummaryPage())
				{
					bulkEditSummaryPage.BindingSource.DataSource = base.Context;
					if (base.ShowDialog(bulkEditSummaryPage) == DialogResult.OK)
					{
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00029A88 File Offset: 0x00027C88
		private bool SaveDataContextByProgressDialog()
		{
			bool succeeded = false;
			if (this.ConfirmedToSaveDataContext())
			{
				using (BackgroundWorkerProgressDialog progressDialog = new BackgroundWorkerProgressDialog())
				{
					progressDialog.Text = Strings.BulkEditingProgressDialogTitle;
					progressDialog.OkEnabled = false;
					progressDialog.CancelEnabled = base.Context.DataHandler.CanCancel;
					base.Context.DataHandler.UpdateWorkUnits();
					WorkUnitCollection workUnits = base.Context.DataHandler.WorkUnits;
					progressDialog.UseMarquee = true;
					progressDialog.StatusText = workUnits.Description;
					int timeIntervalForReport = 100;
					Stopwatch reportStatus = new Stopwatch();
					reportStatus.Start();
					base.Context.DataHandler.ProgressReport += delegate(object sender, ProgressReportEventArgs progressReportEventArgs)
					{
						if (reportStatus.ElapsedMilliseconds > (long)timeIntervalForReport)
						{
							int percentProgress = workUnits.ProgressValue * 100 / workUnits.MaxProgressValue;
							progressDialog.ReportProgress(percentProgress, workUnits.Description);
							reportStatus.Reset();
							reportStatus.Start();
						}
					};
					progressDialog.DoWork += delegate(object sender, DoWorkEventArgs e2)
					{
						base.Context.SaveData(new WinFormsCommandInteractionHandler(base.ShellUI));
					};
					progressDialog.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e2)
					{
						progressDialog.ReportProgress(100, workUnits.Description);
						succeeded = !progressDialog.ShowErrors(Strings.PropertyPageWriteError, Strings.PropertyPageWriteWarning, workUnits, this.ShellUI);
						if (succeeded && workUnits.FindByStatus(WorkUnitStatus.NotStarted).Count > 0)
						{
							succeeded = false;
						}
					};
					progressDialog.FormClosing += delegate(object sender, FormClosingEventArgs e)
					{
						if (progressDialog.IsBusy && progressDialog.CancelEnabled)
						{
							progressDialog.CancelEnabled = false;
							WinformsHelper.InvokeAsync(delegate
							{
								this.Context.DataHandler.Cancel();
							}, progressDialog);
							e.Cancel = progressDialog.IsBusy;
						}
					};
					progressDialog.FormClosed += delegate(object param0, FormClosedEventArgs param1)
					{
						base.Context.DataHandler.ResetCancel();
					};
					progressDialog.ShowDialog(this);
				}
			}
			return succeeded;
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x00029C64 File Offset: 0x00027E64
		internal bool TryApply()
		{
			ValidationError[] array = this.ValidateOnApply();
			if (array.Length != 0)
			{
				base.ShowError(base.CreateValidateContextErrorMessageFor(new List<ValidationError>(array)));
				return false;
			}
			return true;
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x00029C97 File Offset: 0x00027E97
		protected virtual ValidationError[] ValidateOnApply()
		{
			return base.Context.Validate();
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00029CA4 File Offset: 0x00027EA4
		protected virtual void OnApplying(CancelEventArgs e)
		{
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x00029CA8 File Offset: 0x00027EA8
		protected override void OnSetActive(EventArgs e)
		{
			base.OnSetActive(e);
			EventHandler test_SetActive = ExchangePropertyPageControl.Test_SetActive;
			if (test_SetActive != null)
			{
				test_SetActive(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x00029CD4 File Offset: 0x00027ED4
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessDialogKey(Keys keyData)
		{
			Button button = base.FocusedControl as Button;
			if (keyData == Keys.Return && button != null)
			{
				button.PerformClick();
				return true;
			}
			return base.ProcessDialogKey(keyData);
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x00029D04 File Offset: 0x00027F04
		protected override void VerifyCorruptedObject()
		{
			if (base.Context.IsDataSourceCorrupted)
			{
				if (DialogResult.OK == base.ShowMessage(this.CreateErrorMessageForCorruptedObject(), MessageBoxButtons.OKCancel))
				{
					base.Context.OverrideCorruptedValuesWithDefault();
					if (base.Context.IsDirty)
					{
						AutomatedDataHandler automatedDataHandler = base.DataHandler as AutomatedDataHandler;
						if (automatedDataHandler != null)
						{
							automatedDataHandler.RefreshDataObjectStore();
						}
						base.ForceIsDirty(true);
						return;
					}
				}
				else
				{
					base.DisableRelatedPages(false);
				}
			}
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x00029D6C File Offset: 0x00027F6C
		private string CreateErrorMessageForCorruptedObject()
		{
			ADObject adobject = base.Context.DataHandler.DataSource as ADObject;
			string str;
			if (adobject != null && !string.IsNullOrEmpty(adobject.Name))
			{
				str = Strings.CorruptedObjectErrorMessageObjectName(string.Format("'{0}'", adobject.Name));
			}
			else
			{
				str = Strings.CorruptedObjectErrorMessageNoName;
			}
			LocalizedString localizedString = LocalizedString.Empty;
			IConfigurable configurable = base.Context.DataHandler.DataSource as IConfigurable;
			if (configurable != null)
			{
				ValidationError[] array = configurable.Validate();
				List<string> list = new List<string>(array.Length);
				foreach (ValidationError validationError in array)
				{
					PropertyValidationError propertyValidationError = validationError as PropertyValidationError;
					if (propertyValidationError != null)
					{
						string name = propertyValidationError.PropertyDefinition.Name;
						if (!list.Contains(name))
						{
							list.Add(name);
						}
					}
				}
				if (list.Count > 0)
				{
					localizedString = Strings.CorruptedObjectErrorPropertyNames(string.Join(", ", list.ToArray()));
				}
			}
			return str + Strings.CorruptedObjectErrorMessageBody.ToString() + localizedString.ToString();
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00029E90 File Offset: 0x00028090
		protected override ValidationError[] ValidateContextOnPageTransition()
		{
			return base.Context.ValidateOnly(base.BindingSource.DataSource);
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00029EA8 File Offset: 0x000280A8
		protected override bool BlockPageSwitchWithError(PropertyValidationError propertyError)
		{
			return propertyError != null && propertyError.PropertyDefinition != null && base.InputValidationProvider.IsBoundToProperty(propertyError.PropertyDefinition.Name);
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00029ED0 File Offset: 0x000280D0
		public bool HasPermission()
		{
			AutomatedDataHandler automatedDataHandler = base.DataHandler as AutomatedDataHandler;
			return automatedDataHandler == null || automatedDataHandler.HasViewPermissionForPage(base.Name);
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x00029EFC File Offset: 0x000280FC
		public bool HasLockedControls
		{
			get
			{
				AutomatedDataHandlerBase automatedDataHandlerBase = base.DataHandler as AutomatedDataHandlerBase;
				if (automatedDataHandlerBase != null)
				{
					BindingManagerBase bindingManagerBase = this.BindingContext[base.BindingSource];
					foreach (object obj in bindingManagerBase.Bindings)
					{
						Binding binding = (Binding)obj;
						IBulkEditor bulkEditor = binding.Control as IBulkEditor;
						if (bulkEditor != null && bulkEditor.BulkEditorAdapter[binding.PropertyName] == 3)
						{
							return true;
						}
					}
					return false;
				}
				return false;
			}
		}

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x06000BA8 RID: 2984 RVA: 0x00029FA4 File Offset: 0x000281A4
		// (remove) Token: 0x06000BA9 RID: 2985 RVA: 0x00029FD8 File Offset: 0x000281D8
		public static event EventHandler Test_SetActive;
	}
}
