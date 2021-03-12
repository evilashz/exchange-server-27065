using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000DC RID: 220
	[Serializable]
	public class ActiveSyncDeviceClassIdParameter : ADIdParameter
	{
		// Token: 0x0600080E RID: 2062 RVA: 0x0001D821 File Offset: 0x0001BA21
		public ActiveSyncDeviceClassIdParameter()
		{
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0001D829 File Offset: 0x0001BA29
		public ActiveSyncDeviceClassIdParameter(string rawString) : base(rawString)
		{
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0001D832 File Offset: 0x0001BA32
		public ActiveSyncDeviceClassIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0001D83B File Offset: 0x0001BA3B
		public ActiveSyncDeviceClassIdParameter(ActiveSyncDeviceClass settings) : base(settings.Id)
		{
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0001D849 File Offset: 0x0001BA49
		public ActiveSyncDeviceClassIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0001D852 File Offset: 0x0001BA52
		public static ActiveSyncDeviceClassIdParameter Parse(string rawString)
		{
			return new ActiveSyncDeviceClassIdParameter(rawString);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0001D85C File Offset: 0x0001BA5C
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (typeof(T) != typeof(ActiveSyncDeviceClass))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			return base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason);
		}
	}
}
