using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200005C RID: 92
	public abstract class GetSystemConfigurationObjectTask<TIdentity, TDataObject> : GetTenantADObjectWithIdentityTaskBase<TIdentity, TDataObject> where TIdentity : IIdentityParameter where TDataObject : ADObject, new()
	{
		// Token: 0x060003E3 RID: 995 RVA: 0x0000E9F5 File Offset: 0x0000CBF5
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 531, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\GetAdObjectTask.cs");
		}
	}
}
