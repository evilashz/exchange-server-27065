using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000040 RID: 64
	public abstract class SystemConfigurationObjectActionTask<TIdentity, TDataObject> : ObjectActionTenantADTask<TIdentity, TDataObject> where TIdentity : IIdentityParameter, new() where TDataObject : ADObject, new()
	{
		// Token: 0x06000302 RID: 770 RVA: 0x0000BE46 File Offset: 0x0000A046
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new ADObjectTaskModuleFactory();
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000BE50 File Offset: 0x0000A050
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, null, base.SessionSettings, 186, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\ADObjectActionTask.cs");
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000BE8C File Offset: 0x0000A08C
		protected override IConfigurable ResolveDataObject()
		{
			ADObject adobject = (ADObject)base.ResolveDataObject();
			if (TaskHelper.ShouldUnderscopeDataSessionToOrganization((IDirectorySession)base.DataSession, adobject))
			{
				base.UnderscopeDataSession(adobject.OrganizationId);
			}
			return adobject;
		}
	}
}
