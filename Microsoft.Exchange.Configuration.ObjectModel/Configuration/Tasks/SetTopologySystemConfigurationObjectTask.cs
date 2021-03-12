using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000093 RID: 147
	public abstract class SetTopologySystemConfigurationObjectTask<TIdentity, TPublicObject, TDataObject> : SetSystemConfigurationObjectTask<TIdentity, TPublicObject, TDataObject> where TIdentity : IIdentityParameter, new() where TPublicObject : IConfigurable, new() where TDataObject : ADObject, new()
	{
	}
}
