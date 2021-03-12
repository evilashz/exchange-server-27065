using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200004D RID: 77
	internal class PSRemoteServerSchema : ObjectSchema
	{
		// Token: 0x040000CD RID: 205
		public static readonly AdminPropertyDefinition RemotePSServer = new AdminPropertyDefinition("RemotePSServer", ExchangeObjectVersion.Exchange2003, typeof(Fqdn), null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
