using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000D9 RID: 217
	[Serializable]
	public class ActiveSyncDeviceIdParameter : MobileDeviceIdParameter
	{
		// Token: 0x060007FA RID: 2042 RVA: 0x0001D6C4 File Offset: 0x0001B8C4
		public ActiveSyncDeviceIdParameter()
		{
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0001D6CC File Offset: 0x0001B8CC
		public ActiveSyncDeviceIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0001D6D5 File Offset: 0x0001B8D5
		public ActiveSyncDeviceIdParameter(ADObjectId objectId) : base(objectId)
		{
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001D6DE File Offset: 0x0001B8DE
		public ActiveSyncDeviceIdParameter(ActiveSyncDevice device) : base(device)
		{
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0001D6E7 File Offset: 0x0001B8E7
		public ActiveSyncDeviceIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0001D6F0 File Offset: 0x0001B8F0
		public new static ActiveSyncDeviceIdParameter Parse(string identity)
		{
			return new ActiveSyncDeviceIdParameter(identity);
		}
	}
}
