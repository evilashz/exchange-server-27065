using System;
using Microsoft.Exchange.Configuration.ObjectModel;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000089 RID: 137
	public abstract class RemoveObjectTask<TIdentity, TDataObject> : RemoveTaskBase<TIdentity, TDataObject> where TIdentity : IIdentityParameter where TDataObject : ConfigObject, new()
	{
		// Token: 0x0600058F RID: 1423 RVA: 0x00015414 File Offset: 0x00013614
		protected override IConfigDataProvider CreateSession()
		{
			DataSourceManager[] dataSourceManagers = DataSourceManager.GetDataSourceManagers(typeof(TDataObject), "Identity");
			return dataSourceManagers[0];
		}
	}
}
