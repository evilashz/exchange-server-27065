using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000085 RID: 133
	public abstract class RemoveADTaskBase<TIdentity, TDataObject> : RemoveTenantADTaskBase<TIdentity, TDataObject> where TIdentity : IIdentityParameter where TDataObject : ADObject, new()
	{
		// Token: 0x0600056A RID: 1386 RVA: 0x00014C6C File Offset: 0x00012E6C
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new ADObjectTaskModuleFactory();
		}
	}
}
