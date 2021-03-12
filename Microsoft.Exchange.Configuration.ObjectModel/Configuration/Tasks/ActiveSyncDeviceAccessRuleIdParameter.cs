using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000DA RID: 218
	[Serializable]
	public class ActiveSyncDeviceAccessRuleIdParameter : ADIdParameter
	{
		// Token: 0x06000800 RID: 2048 RVA: 0x0001D6F8 File Offset: 0x0001B8F8
		public ActiveSyncDeviceAccessRuleIdParameter()
		{
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0001D700 File Offset: 0x0001B900
		public ActiveSyncDeviceAccessRuleIdParameter(string rawString) : base(rawString)
		{
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0001D709 File Offset: 0x0001B909
		public ActiveSyncDeviceAccessRuleIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0001D712 File Offset: 0x0001B912
		public ActiveSyncDeviceAccessRuleIdParameter(ActiveSyncDeviceAccessRule settings) : base(settings.Id)
		{
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0001D720 File Offset: 0x0001B920
		public ActiveSyncDeviceAccessRuleIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0001D729 File Offset: 0x0001B929
		public static ActiveSyncDeviceAccessRuleIdParameter Parse(string rawString)
		{
			return new ActiveSyncDeviceAccessRuleIdParameter(rawString);
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0001D734 File Offset: 0x0001B934
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (typeof(T) != typeof(ActiveSyncDeviceAccessRule))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			return base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason);
		}
	}
}
