using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.AirSync.SchemaConverter.Entity;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.AirSync.SchemaConverter
{
	// Token: 0x020001AF RID: 431
	internal class AirSyncEntitySchemaState : AirSyncSchemaState, IDataObjectGenerator, IClassFilter
	{
		// Token: 0x06001236 RID: 4662 RVA: 0x00062ECC File Offset: 0x000610CC
		public AirSyncEntitySchemaState(QueryFilter supportedClassFilter)
		{
			this.supportedClassFilter = supportedClassFilter;
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06001237 RID: 4663 RVA: 0x00062EDB File Offset: 0x000610DB
		public QueryFilter SupportedClassFilter
		{
			get
			{
				return this.supportedClassFilter;
			}
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x00062EE3 File Offset: 0x000610E3
		public EntityDataObject GetEntityDataObject()
		{
			return new EntityDataObject(base.GetSchema(1), this);
		}

		// Token: 0x04000B55 RID: 2901
		private static readonly QueryFilter falseFilterInstance = new FalseFilter();

		// Token: 0x04000B56 RID: 2902
		private readonly QueryFilter supportedClassFilter;
	}
}
