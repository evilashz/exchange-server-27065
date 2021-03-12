using System;
using System.Collections.Generic;
using System.Data;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000058 RID: 88
	public abstract class Reader : RunnerBase
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600039A RID: 922
		public abstract object DataObject { get; }

		// Token: 0x0600039B RID: 923 RVA: 0x0000D13C File Offset: 0x0000B33C
		public sealed override void Cancel()
		{
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000D13E File Offset: 0x0000B33E
		public virtual Reader CreateBulkReader(string dataObjectName, DataObjectStore store, DataTable table)
		{
			return new BulkEditReaderTask(store.GetDataObjectType(dataObjectName), store.GetDataObjectCreator(dataObjectName), dataObjectName, table);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000D155 File Offset: 0x0000B355
		public virtual bool HasPermission(IList<ParameterProfile> paramInfos)
		{
			return true;
		}
	}
}
