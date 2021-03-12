using System;
using Microsoft.Exchange.Configuration.ObjectModel;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200009C RID: 156
	public abstract class SetSingletonObjectTask<TDataObject> : SetSingletonObjectTaskBase<TDataObject> where TDataObject : ConfigObject, new()
	{
		// Token: 0x060005FE RID: 1534 RVA: 0x000169B2 File Offset: 0x00014BB2
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			this.Instance = this.DataObject;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x000169D0 File Offset: 0x00014BD0
		protected override IConfigDataProvider CreateSession()
		{
			DataSourceManager[] dataSourceManagers = DataSourceManager.GetDataSourceManagers(typeof(TDataObject), "Identity");
			return dataSourceManagers[0];
		}
	}
}
