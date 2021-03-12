using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;

namespace Microsoft.Exchange.Management.ReportingTask.Common
{
	// Token: 0x020006A1 RID: 1697
	internal class MetaModelWrapper : MetaModel
	{
		// Token: 0x06003C14 RID: 15380 RVA: 0x00100AE7 File Offset: 0x000FECE7
		public MetaModelWrapper(MappingSourceWrapper mappingSourceWrapper, MetaModel metaModel)
		{
			this.MetaModel = metaModel;
			this.mappingSourceWrapper = mappingSourceWrapper;
		}

		// Token: 0x170011E9 RID: 4585
		// (get) Token: 0x06003C15 RID: 15381 RVA: 0x00100AFD File Offset: 0x000FECFD
		// (set) Token: 0x06003C16 RID: 15382 RVA: 0x00100B05 File Offset: 0x000FED05
		public MetaModel MetaModel { get; private set; }

		// Token: 0x06003C17 RID: 15383 RVA: 0x00100B10 File Offset: 0x000FED10
		public MetaTable Wrap(MetaTable metaTable)
		{
			string text = this.mappingSourceWrapper.FindView(metaTable.RowType.Type);
			if (text != null)
			{
				return new MetaTableWrapper(this, metaTable, text);
			}
			return metaTable;
		}

		// Token: 0x06003C18 RID: 15384 RVA: 0x00100B44 File Offset: 0x000FED44
		public override MetaTable GetTable(Type rowType)
		{
			MetaTable table = this.MetaModel.GetTable(rowType);
			return this.Wrap(table);
		}

		// Token: 0x06003C19 RID: 15385 RVA: 0x00100B65 File Offset: 0x000FED65
		public override IEnumerable<MetaTable> GetTables()
		{
			return this.MetaModel.GetTables().Select(new Func<MetaTable, MetaTable>(this.Wrap));
		}

		// Token: 0x06003C1A RID: 15386 RVA: 0x00100B83 File Offset: 0x000FED83
		public override MetaFunction GetFunction(MethodInfo method)
		{
			return this.MetaModel.GetFunction(method);
		}

		// Token: 0x06003C1B RID: 15387 RVA: 0x00100B91 File Offset: 0x000FED91
		public override IEnumerable<MetaFunction> GetFunctions()
		{
			return this.MetaModel.GetFunctions();
		}

		// Token: 0x06003C1C RID: 15388 RVA: 0x00100B9E File Offset: 0x000FED9E
		public override MetaType GetMetaType(Type type)
		{
			return this.MetaModel.GetMetaType(type);
		}

		// Token: 0x170011EA RID: 4586
		// (get) Token: 0x06003C1D RID: 15389 RVA: 0x00100BAC File Offset: 0x000FEDAC
		public override MappingSource MappingSource
		{
			get
			{
				return this.mappingSourceWrapper;
			}
		}

		// Token: 0x170011EB RID: 4587
		// (get) Token: 0x06003C1E RID: 15390 RVA: 0x00100BB4 File Offset: 0x000FEDB4
		public override Type ContextType
		{
			get
			{
				return this.MetaModel.ContextType;
			}
		}

		// Token: 0x170011EC RID: 4588
		// (get) Token: 0x06003C1F RID: 15391 RVA: 0x00100BC1 File Offset: 0x000FEDC1
		public override string DatabaseName
		{
			get
			{
				return this.MetaModel.DatabaseName;
			}
		}

		// Token: 0x170011ED RID: 4589
		// (get) Token: 0x06003C20 RID: 15392 RVA: 0x00100BCE File Offset: 0x000FEDCE
		public override Type ProviderType
		{
			get
			{
				return this.MetaModel.ProviderType;
			}
		}

		// Token: 0x04002722 RID: 10018
		private readonly MappingSourceWrapper mappingSourceWrapper;
	}
}
