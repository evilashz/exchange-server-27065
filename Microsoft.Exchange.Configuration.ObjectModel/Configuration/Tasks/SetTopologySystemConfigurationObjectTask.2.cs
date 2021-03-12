using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000094 RID: 148
	public abstract class SetTopologySystemConfigurationObjectTask<TIdentity, TDataObject> : SetTopologySystemConfigurationObjectTask<TIdentity, TDataObject, TDataObject> where TIdentity : IIdentityParameter, new() where TDataObject : ADObject, new()
	{
	}
}
