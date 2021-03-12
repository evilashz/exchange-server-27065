using System;
using System.Data;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x020000C6 RID: 198
	public sealed class AutomatedObjectPicker : ObjectPicker
	{
		// Token: 0x06000690 RID: 1680 RVA: 0x00017905 File Offset: 0x00015B05
		public AutomatedObjectPicker()
		{
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001790D File Offset: 0x00015B0D
		public AutomatedObjectPicker(IResultsLoaderConfiguration configurable) : base(configurable)
		{
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00017916 File Offset: 0x00015B16
		public AutomatedObjectPicker(string profileName) : base(profileName)
		{
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001791F File Offset: 0x00015B1F
		public override ExchangeColumnHeader[] CreateColumnHeaders()
		{
			return base.ObjectPickerProfile.CreateColumnHeaders();
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0001792C File Offset: 0x00015B2C
		public override DataTable CreateResultsDataTable()
		{
			return base.ObjectPickerProfile.CreateResultsDataTable();
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00017939 File Offset: 0x00015B39
		public void InputValue(string columnName, object value)
		{
			base.ObjectPickerProfile.InputValue(columnName, value);
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00017948 File Offset: 0x00015B48
		public object GetValue(string columnName)
		{
			return base.ObjectPickerProfile.GetValue(columnName);
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x00017956 File Offset: 0x00015B56
		public override bool ShowListItemIcon
		{
			get
			{
				return !base.ObjectPickerProfile.HideIcon;
			}
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00017968 File Offset: 0x00015B68
		protected override DataTableLoader CreateDataTableLoader()
		{
			return new DataTableLoader(base.ObjectPickerProfile)
			{
				EnforeViewEntireForest = true
			};
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001798C File Offset: 0x00015B8C
		public override void PerformQuery(object rootId, string searchText)
		{
			(base.DataTableLoader.RefreshArgument as ResultsLoaderProfile).SearchText = searchText;
			(base.DataTableLoader.RefreshArgument as ResultsLoaderProfile).Scope = ((base.ShouldScopingWithinDefaultDomainScope && rootId == null) ? base.ScopeSettings.OrganizationalUnit : rootId);
			(base.DataTableLoader.RefreshArgument as ResultsLoaderProfile).IsResolving = false;
			(base.DataTableLoader.RefreshArgument as ResultsLoaderProfile).PipelineObjects = null;
			base.DataTableLoader.Refresh(NullProgress.Value);
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x00017A19 File Offset: 0x00015C19
		public override string ObjectClassDisplayName
		{
			get
			{
				return base.ObjectPickerProfile.DisplayName;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x00017A26 File Offset: 0x00015C26
		public override string NameProperty
		{
			get
			{
				return base.ObjectPickerProfile.NameProperty;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x00017A33 File Offset: 0x00015C33
		public override string IdentityProperty
		{
			get
			{
				return base.ObjectPickerProfile.DistinguishIdentity;
			}
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00017A40 File Offset: 0x00015C40
		public void SetTitle(string displayName)
		{
			base.ObjectPickerProfile.DisplayName = displayName;
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x00017A4E File Offset: 0x00015C4E
		public override string ImageProperty
		{
			get
			{
				return base.ObjectPickerProfile.ImageProperty;
			}
		}
	}
}
