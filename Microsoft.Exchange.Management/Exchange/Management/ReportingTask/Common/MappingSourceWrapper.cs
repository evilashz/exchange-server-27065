using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;

namespace Microsoft.Exchange.Management.ReportingTask.Common
{
	// Token: 0x020006A0 RID: 1696
	internal class MappingSourceWrapper : MappingSource
	{
		// Token: 0x06003C0E RID: 15374 RVA: 0x00100A6D File Offset: 0x000FEC6D
		public MappingSourceWrapper(MappingSource mappingSource)
		{
			this.MappingSource = mappingSource;
			this.viewTypeMapping = new Dictionary<Type, string>();
		}

		// Token: 0x170011E8 RID: 4584
		// (get) Token: 0x06003C0F RID: 15375 RVA: 0x00100A87 File Offset: 0x000FEC87
		// (set) Token: 0x06003C10 RID: 15376 RVA: 0x00100A8F File Offset: 0x000FEC8F
		public MappingSource MappingSource { get; private set; }

		// Token: 0x06003C11 RID: 15377 RVA: 0x00100A98 File Offset: 0x000FEC98
		public void AddMapping(Type type, string viewName)
		{
			this.viewTypeMapping[type] = viewName;
		}

		// Token: 0x06003C12 RID: 15378 RVA: 0x00100AA8 File Offset: 0x000FECA8
		protected override MetaModel CreateModel(Type dataContextType)
		{
			MetaModel model = this.MappingSource.GetModel(dataContextType);
			return new MetaModelWrapper(this, model);
		}

		// Token: 0x06003C13 RID: 15379 RVA: 0x00100AC9 File Offset: 0x000FECC9
		public string FindView(Type type)
		{
			if (this.viewTypeMapping.ContainsKey(type))
			{
				return this.viewTypeMapping[type];
			}
			return null;
		}

		// Token: 0x04002720 RID: 10016
		private readonly Dictionary<Type, string> viewTypeMapping;
	}
}
