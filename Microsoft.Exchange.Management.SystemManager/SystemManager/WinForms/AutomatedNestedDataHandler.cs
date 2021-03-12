using System;
using System.Data;
using System.Reflection;
using Microsoft.Exchange.Configuration.MonadDataProvider;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000056 RID: 86
	internal class AutomatedNestedDataHandler : AutomatedDataHandlerBase
	{
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000386 RID: 902 RVA: 0x0000CEE8 File Offset: 0x0000B0E8
		internal DataContext ParentContext
		{
			get
			{
				return this.parentContext;
			}
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000CEF0 File Offset: 0x0000B0F0
		public static AutomatedNestedDataHandler CreateDataHandlerWithParentSchema(DataContext parentContext)
		{
			return new AutomatedNestedDataHandler(parentContext, (parentContext.DataHandler as AutomatedDataHandlerBase).Assembly, (parentContext.DataHandler as AutomatedDataHandlerBase).SchemaName);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000CF18 File Offset: 0x0000B118
		public AutomatedNestedDataHandler(DataContext parentContext, Assembly assembly, string schema) : base(assembly, schema)
		{
			this.parentContext = parentContext;
			base.EnableBulkEdit = (parentContext.DataHandler as AutomatedDataHandlerBase).EnableBulkEdit;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000CF40 File Offset: 0x0000B140
		internal override void OnReadData(CommandInteractionHandler interactionHandler, string pageName)
		{
			DataTable table = (DataTable)this.parentContext.DataHandler.DataSource;
			base.Table.Merge(table);
			base.DataObjectStore = ((AutomatedDataHandlerBase)this.parentContext.DataHandler).DataObjectStore.Clone();
			base.RefreshDataObjectStore();
			base.DataSource = base.Table;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000CFA4 File Offset: 0x0000B1A4
		internal override void OnSaveData(CommandInteractionHandler interactionHandler)
		{
			AutomatedDataHandlerBase automatedDataHandlerBase = this.parentContext.DataHandler as AutomatedDataHandlerBase;
			automatedDataHandlerBase.DataObjectStore = base.DataObjectStore;
			automatedDataHandlerBase.RefreshDataObjectStoreWithNewTable();
			this.parentContext.IsDirty = true;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000CFE0 File Offset: 0x0000B1E0
		private AutomatedDataHandler GetRootDataHandler(DataContext context)
		{
			if (context.DataHandler is AutomatedDataHandler)
			{
				return context.DataHandler as AutomatedDataHandler;
			}
			return this.GetRootDataHandler((context.DataHandler as AutomatedNestedDataHandler).ParentContext);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000D011 File Offset: 0x0000B211
		internal override bool HasViewPermissionForPage(string pageName)
		{
			return this.GetRootDataHandler(this.ParentContext).HasViewPermissionForPage(pageName);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000D025 File Offset: 0x0000B225
		internal override bool HasPermissionForProperty(string propertyName, bool canUpdate)
		{
			return this.GetRootDataHandler(this.ParentContext).HasPermissionForProperty(propertyName, canUpdate);
		}

		// Token: 0x040000EF RID: 239
		private DataContext parentContext;
	}
}
