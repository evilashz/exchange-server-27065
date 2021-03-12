using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000DB RID: 219
	[Serializable]
	public class ActiveSyncDeviceAutoblockThresholdIdParameter : ADIdParameter
	{
		// Token: 0x06000807 RID: 2055 RVA: 0x0001D78D File Offset: 0x0001B98D
		public ActiveSyncDeviceAutoblockThresholdIdParameter()
		{
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001D795 File Offset: 0x0001B995
		public ActiveSyncDeviceAutoblockThresholdIdParameter(string rawString) : base(rawString)
		{
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0001D79E File Offset: 0x0001B99E
		public ActiveSyncDeviceAutoblockThresholdIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0001D7A7 File Offset: 0x0001B9A7
		public ActiveSyncDeviceAutoblockThresholdIdParameter(ActiveSyncDeviceAutoblockThreshold settings) : base(settings.Id)
		{
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0001D7B5 File Offset: 0x0001B9B5
		public ActiveSyncDeviceAutoblockThresholdIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0001D7BE File Offset: 0x0001B9BE
		public static ActiveSyncDeviceAutoblockThresholdIdParameter Parse(string rawString)
		{
			return new ActiveSyncDeviceAutoblockThresholdIdParameter(rawString);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0001D7C8 File Offset: 0x0001B9C8
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (typeof(T) != typeof(ActiveSyncDeviceAutoblockThreshold))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			return base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason);
		}
	}
}
