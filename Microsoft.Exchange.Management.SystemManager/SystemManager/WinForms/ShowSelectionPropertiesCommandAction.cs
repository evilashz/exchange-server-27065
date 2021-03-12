using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000208 RID: 520
	public abstract class ShowSelectionPropertiesCommandAction : ResultsCommandAction
	{
		// Token: 0x06001795 RID: 6037 RVA: 0x00063123 File Offset: 0x00061323
		public ShowSelectionPropertiesCommandAction()
		{
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001796 RID: 6038 RVA: 0x00063136 File Offset: 0x00061336
		// (set) Token: 0x06001797 RID: 6039 RVA: 0x0006313E File Offset: 0x0006133E
		public Assembly Assembly { get; set; }

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001798 RID: 6040 RVA: 0x00063147 File Offset: 0x00061347
		// (set) Token: 0x06001799 RID: 6041 RVA: 0x0006314F File Offset: 0x0006134F
		public string Schema { get; set; }

		// Token: 0x0600179A RID: 6042 RVA: 0x00063158 File Offset: 0x00061358
		public override bool HasPermission()
		{
			if (!string.IsNullOrEmpty(this.Schema) && null != this.Assembly)
			{
				PageConfigurableProfile pageConfigurableProfile = AutomatedDataHandlerBase.BuildProfile(this.Assembly, this.Schema) as PageConfigurableProfile;
				return pageConfigurableProfile.HasPermission();
			}
			return base.HasPermission();
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x000631A4 File Offset: 0x000613A4
		protected override void OnExecute()
		{
			base.OnExecute();
			bool flag = base.ResultPane.SelectedObjects.Count > 1;
			string objectName = flag ? this.GetBulkSelectionPropertySheetDisplayName() : this.GetSingleSelectionPropertySheetDisplayName();
			string dialogNamePrefix = flag ? "BulkEditingPropertySheet_" : "PropertySheet_";
			this.ShowPropertySheetDailog(Strings.SingleSelectionProperties(objectName), dialogNamePrefix, flag);
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x0600179C RID: 6044 RVA: 0x00063200 File Offset: 0x00061400
		protected virtual bool SharePropertySheetDialog
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x0600179D RID: 6045 RVA: 0x00063203 File Offset: 0x00061403
		private Dictionary<SelectedObjects, Guid> SelectedObjectsDictionary
		{
			get
			{
				if (!this.SharePropertySheetDialog)
				{
					return this.PrivateSelectedObjectsDictionary;
				}
				return ShowSelectionPropertiesCommandAction.SharedSelectedObjectsDictionary;
			}
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x00063219 File Offset: 0x00061419
		protected virtual string GetSingleSelectionPropertySheetDisplayName()
		{
			return base.ResultPane.SelectedName;
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x00063226 File Offset: 0x00061426
		protected virtual string GetBulkSelectionPropertySheetDisplayName()
		{
			if (!string.IsNullOrEmpty(base.ResultPane.SelectedObjectDetailsType))
			{
				return base.ResultPane.SelectedObjectDetailsType;
			}
			return base.ResultPane.SelectedName;
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x0006328C File Offset: 0x0006148C
		private void ShowPropertySheetDailog(string propertySheetName, string dialogNamePrefix, bool bulkEditing)
		{
			SelectedObjects selectedComponents = new SelectedObjects(base.ResultPane.SelectedObjects);
			Guid value = Guid.Empty;
			if (this.SelectedObjectsDictionary.ContainsKey(selectedComponents))
			{
				value = this.SelectedObjectsDictionary[selectedComponents];
			}
			else
			{
				value = Guid.NewGuid();
				this.SelectedObjectsDictionary[selectedComponents] = value;
			}
			string text = dialogNamePrefix + value.ToString();
			if (!ExchangeForm.ActivateSingleInstanceForm(text))
			{
				ExchangePropertyPageControl[] array = bulkEditing ? this.OnGetBulkSelectionPropertyPageControls() : this.OnGetSingleSelectionPropertyPageControls();
				if (!bulkEditing)
				{
					List<ExchangePropertyPageControl> list = new List<ExchangePropertyPageControl>();
					foreach (ExchangePropertyPageControl exchangePropertyPageControl in array)
					{
						if (exchangePropertyPageControl.HasPermission())
						{
							list.Add(exchangePropertyPageControl);
						}
					}
					array = list.ToArray();
				}
				this.ApplyOptionsOnPage(array, bulkEditing);
				PropertySheetDialog propertySheetDialog = new PropertySheetDialog(propertySheetName, array);
				propertySheetDialog.Name = text;
				propertySheetDialog.HelpTopic = base.ResultPane.SelectionHelpTopic + "Property";
				propertySheetDialog.Closed += delegate(object param0, EventArgs param1)
				{
					if (this.SelectedObjectsDictionary.ContainsKey(selectedComponents))
					{
						this.SelectedObjectsDictionary.Remove(selectedComponents);
					}
				};
				propertySheetDialog.ShowModeless(base.ResultPane, null);
			}
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x000633F4 File Offset: 0x000615F4
		private void ApplyOptionsOnPage(ExchangePropertyPageControl[] pages, bool bulkEditing)
		{
			if (pages != null && pages.Length > 0)
			{
				ArrayList arrayList = new ArrayList();
				DataContextFlags dataContextFlags = new DataContextFlags();
				dataContextFlags.SelectedObjectsCount = base.ResultPane.SelectedObjects.Count;
				dataContextFlags.SelectedObjectDetailsType = base.ResultPane.SelectedObjectDetailsType;
				for (int i = 0; i < pages.Length; i++)
				{
					DataContext context = pages[i].Context;
					if (context != null && !arrayList.Contains(context))
					{
						arrayList.Add(context);
						dataContextFlags.Pages.Add(pages[i]);
						context.DataSaved += delegate(object param0, EventArgs param1)
						{
							this.RefreshResultsThreadSafely(context);
						};
						if (context.RefreshOnSave == null)
						{
							context.RefreshOnSave = base.GetDefaultRefreshObject();
						}
					}
				}
				AutomatedDataHandler automatedDataHandler = pages[0].DataHandler as AutomatedDataHandler;
				if (automatedDataHandler != null)
				{
					automatedDataHandler.ReaderExecutionContextFactory = new MonadCommandExecutionContextForPropertyPageFactory();
					automatedDataHandler.SaverExecutionContextFactory = new MonadCommandExecutionContextForPropertyPageFactory();
				}
			}
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x00063514 File Offset: 0x00061714
		protected virtual ExchangePropertyPageControl[] OnGetBulkSelectionPropertyPageControls()
		{
			return new ExchangePropertyPageControl[0];
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x0006351C File Offset: 0x0006171C
		protected virtual ExchangePropertyPageControl[] OnGetSingleSelectionPropertyPageControls()
		{
			return new ExchangePropertyPageControl[0];
		}

		// Token: 0x040008D7 RID: 2263
		private static Dictionary<SelectedObjects, Guid> SharedSelectedObjectsDictionary = new Dictionary<SelectedObjects, Guid>();

		// Token: 0x040008D8 RID: 2264
		private Dictionary<SelectedObjects, Guid> PrivateSelectedObjectsDictionary = new Dictionary<SelectedObjects, Guid>();
	}
}
