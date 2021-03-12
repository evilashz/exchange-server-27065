using System;
using Microsoft.Exchange.Configuration.ObjectModel;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000064 RID: 100
	public abstract class GetSingletonObjectTask<TDataObject> : GetTaskBase<TDataObject> where TDataObject : ConfigObject, new()
	{
		// Token: 0x06000419 RID: 1049 RVA: 0x0000F1F8 File Offset: 0x0000D3F8
		protected override IConfigDataProvider CreateSession()
		{
			DataSourceManager[] dataSourceManagers = DataSourceManager.GetDataSourceManagers(typeof(TDataObject), "Identity");
			return dataSourceManagers[0];
		}
	}
}
