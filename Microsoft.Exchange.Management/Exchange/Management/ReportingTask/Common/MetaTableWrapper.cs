using System;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace Microsoft.Exchange.Management.ReportingTask.Common
{
	// Token: 0x020006A2 RID: 1698
	internal class MetaTableWrapper : MetaTable
	{
		// Token: 0x06003C21 RID: 15393 RVA: 0x00100BDB File Offset: 0x000FEDDB
		public MetaTableWrapper(MetaModelWrapper metaModelWrapper, MetaTable metaTable, string tableName)
		{
			this.metaModelWrapper = metaModelWrapper;
			this.metaTable = metaTable;
			this.tableName = tableName;
			this.metaTypeWrapper = new MetaTypeWrapper(this.metaModelWrapper, this.metaTable.RowType, this);
		}

		// Token: 0x170011EE RID: 4590
		// (get) Token: 0x06003C22 RID: 15394 RVA: 0x00100C15 File Offset: 0x000FEE15
		public override MetaModel Model
		{
			get
			{
				return this.metaModelWrapper;
			}
		}

		// Token: 0x170011EF RID: 4591
		// (get) Token: 0x06003C23 RID: 15395 RVA: 0x00100C1D File Offset: 0x000FEE1D
		public override string TableName
		{
			get
			{
				return this.tableName;
			}
		}

		// Token: 0x170011F0 RID: 4592
		// (get) Token: 0x06003C24 RID: 15396 RVA: 0x00100C25 File Offset: 0x000FEE25
		public override MetaType RowType
		{
			get
			{
				return this.metaTypeWrapper;
			}
		}

		// Token: 0x170011F1 RID: 4593
		// (get) Token: 0x06003C25 RID: 15397 RVA: 0x00100C2D File Offset: 0x000FEE2D
		public override MethodInfo InsertMethod
		{
			get
			{
				return this.metaTable.InsertMethod;
			}
		}

		// Token: 0x170011F2 RID: 4594
		// (get) Token: 0x06003C26 RID: 15398 RVA: 0x00100C3A File Offset: 0x000FEE3A
		public override MethodInfo UpdateMethod
		{
			get
			{
				return this.metaTable.UpdateMethod;
			}
		}

		// Token: 0x170011F3 RID: 4595
		// (get) Token: 0x06003C27 RID: 15399 RVA: 0x00100C47 File Offset: 0x000FEE47
		public override MethodInfo DeleteMethod
		{
			get
			{
				return this.metaTable.DeleteMethod;
			}
		}

		// Token: 0x04002724 RID: 10020
		private MetaTable metaTable;

		// Token: 0x04002725 RID: 10021
		private MetaModelWrapper metaModelWrapper;

		// Token: 0x04002726 RID: 10022
		private readonly string tableName;

		// Token: 0x04002727 RID: 10023
		private MetaTypeWrapper metaTypeWrapper;
	}
}
