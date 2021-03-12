using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi
{
	// Token: 0x02000010 RID: 16
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ClientBehaviorOverrides : IClientBehaviorOverrides
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002BCD File Offset: 0x00000DCD
		internal static IClientBehaviorOverrides Instance
		{
			get
			{
				return ClientBehaviorOverrides.instance;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002BD4 File Offset: 0x00000DD4
		public bool TryGetClientSpecificOverrides(string clientId, CrossServerBehavior crossServerBehaviorDefaults, out CrossServerBehavior crossServerBehaviorOverrides)
		{
			return CrossServerConnectionRegistryParameters.TryGetClientSpecificOverrides(clientId, crossServerBehaviorDefaults, out crossServerBehaviorOverrides);
		}

		// Token: 0x04000069 RID: 105
		private static IClientBehaviorOverrides instance = new ClientBehaviorOverrides();
	}
}
