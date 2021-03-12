using System;
using Microsoft.Exchange.Configuration.ObjectModel;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000099 RID: 153
	public abstract class SetObjectTask<TIdentity, TPublicObject, TDataObject> : SetObjectWithIdentityTaskBase<TIdentity, TPublicObject, TDataObject> where TIdentity : IIdentityParameter, new() where TPublicObject : IConfigurable, new() where TDataObject : ConfigObject, new()
	{
		// Token: 0x060005F9 RID: 1529 RVA: 0x00016958 File Offset: 0x00014B58
		protected override IConfigDataProvider CreateSession()
		{
			DataSourceManager[] dataSourceManagers = DataSourceManager.GetDataSourceManagers(typeof(TDataObject), "Identity");
			return dataSourceManagers[0];
		}
	}
}
