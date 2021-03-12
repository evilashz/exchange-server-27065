using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000434 RID: 1076
	public abstract class GetDeepSearchMailboxPolicyBase<TIdentity, TDataObject> : GetMultitenancySystemConfigurationObjectTask<TIdentity, TDataObject> where TIdentity : IIdentityParameter where TDataObject : ADObject, new()
	{
		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06002609 RID: 9737 RVA: 0x00097E6F File Offset: 0x0009606F
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
