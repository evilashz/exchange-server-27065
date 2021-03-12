using System;
using System.Windows.Forms;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000209 RID: 521
	public abstract class ShowWizardCommandAction : ResultsCommandAction
	{
		// Token: 0x060017A5 RID: 6053 RVA: 0x00063530 File Offset: 0x00061730
		public ShowWizardCommandAction()
		{
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x00063538 File Offset: 0x00061738
		protected override void OnExecute()
		{
			base.OnExecute();
			string sharedFormName = this.GetSharedFormName();
			if (!string.IsNullOrEmpty(sharedFormName))
			{
				if (!ExchangeForm.ActivateSingleInstanceForm(sharedFormName))
				{
					this.ShowWizardForm(this.InternalCreateWizardForm(), sharedFormName);
					return;
				}
			}
			else
			{
				this.ShowWizardForm(this.InternalCreateWizardForm());
			}
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x0006357C File Offset: 0x0006177C
		private void ShowWizardForm(WizardForm wizardForm, string formName)
		{
			wizardForm.Name = formName;
			this.ShowWizardForm(wizardForm);
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x0006358C File Offset: 0x0006178C
		private void ShowWizardForm(WizardForm wizardForm)
		{
			wizardForm.ShowModeless(base.ResultPane);
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x0006359A File Offset: 0x0006179A
		protected virtual string GetSharedFormName()
		{
			return string.Empty;
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x00063644 File Offset: 0x00061844
		private WizardForm InternalCreateWizardForm()
		{
			WizardForm wizardForm = this.CreateWizardForm();
			IRefreshable defaultRefreshObject = base.GetDefaultRefreshObject();
			wizardForm.OriginatingCommand = base.Command;
			wizardForm.FormClosed += delegate(object param0, FormClosedEventArgs param1)
			{
				if (wizardForm.Wizard.IsDataChanged)
				{
					this.RefreshResultsThreadSafely(this.GetDataContextForRefresh(wizardForm, defaultRefreshObject));
				}
			};
			wizardForm.Load += delegate(object param0, EventArgs param1)
			{
				string text = this.CheckPreCondition((wizardForm.Context != null) ? wizardForm.Context.DataHandler.DataSource : null);
				if (!string.IsNullOrEmpty(text))
				{
					wizardForm.Close();
					this.ResultPane.ShowError(text);
				}
			};
			return wizardForm;
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x000636BB File Offset: 0x000618BB
		protected virtual string CheckPreCondition(object dataSource)
		{
			return string.Empty;
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x000636C4 File Offset: 0x000618C4
		private DataContext GetDataContextForRefresh(WizardForm wizardForm, IRefreshable defaultRefreshObject)
		{
			WizardPage wizardPage = wizardForm.WizardPages[wizardForm.WizardPages.Count - 1];
			DataContext context = wizardPage.Context;
			if (wizardForm.RefreshOnFinish != null)
			{
				context.RefreshOnSave = wizardForm.RefreshOnFinish;
			}
			else
			{
				context.RefreshOnSave = (context.RefreshOnSave ?? defaultRefreshObject);
			}
			return context;
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x0006371C File Offset: 0x0006191C
		protected override void RefreshResults(DataContext context)
		{
			if (context != null && context.RefreshOnSave == null)
			{
				if (context.DataHandler != null && context.DataHandler.SavedResults != null && context.DataHandler.SavedResults.Count > 0)
				{
					DataListViewResultPane dataListViewResultPane = base.ResultPane as DataListViewResultPane;
					if (context.DataHandler.SavedResults.Count == 1)
					{
						dataListViewResultPane.RefreshObject(context.DataHandler.SavedResults[0]);
						return;
					}
					dataListViewResultPane.RefreshObjects(context.DataHandler.SavedResults);
					return;
				}
			}
			else
			{
				base.RefreshResults(context);
			}
		}

		// Token: 0x060017AE RID: 6062
		protected abstract WizardForm CreateWizardForm();
	}
}
