using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000DD RID: 221
	[Serializable]
	public class ActiveSyncOrganizationSettingsIdParameter : ADIdParameter
	{
		// Token: 0x06000815 RID: 2069 RVA: 0x0001D8B5 File Offset: 0x0001BAB5
		public ActiveSyncOrganizationSettingsIdParameter()
		{
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0001D8BD File Offset: 0x0001BABD
		public ActiveSyncOrganizationSettingsIdParameter(string rawString) : base(rawString)
		{
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0001D8C6 File Offset: 0x0001BAC6
		public ActiveSyncOrganizationSettingsIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0001D8CF File Offset: 0x0001BACF
		public ActiveSyncOrganizationSettingsIdParameter(ActiveSyncOrganizationSettings settings) : base(settings.Id)
		{
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0001D8DD File Offset: 0x0001BADD
		public ActiveSyncOrganizationSettingsIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0001D8E6 File Offset: 0x0001BAE6
		public static ActiveSyncOrganizationSettingsIdParameter Parse(string rawString)
		{
			return new ActiveSyncOrganizationSettingsIdParameter(rawString);
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0001D8F0 File Offset: 0x0001BAF0
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (typeof(T) != typeof(ActiveSyncOrganizationSettings))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			return base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason);
		}
	}
}
