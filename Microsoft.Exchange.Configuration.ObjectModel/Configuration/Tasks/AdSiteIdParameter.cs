using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000E8 RID: 232
	[Serializable]
	public class AdSiteIdParameter : ADIdParameter
	{
		// Token: 0x0600086B RID: 2155 RVA: 0x0001E557 File Offset: 0x0001C757
		public AdSiteIdParameter()
		{
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0001E55F File Offset: 0x0001C75F
		public AdSiteIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0001E568 File Offset: 0x0001C768
		public AdSiteIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0001E571 File Offset: 0x0001C771
		public AdSiteIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0001E57A File Offset: 0x0001C77A
		public AdSiteIdParameter(ADSite site) : base(site.Id)
		{
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0001E588 File Offset: 0x0001C788
		public static AdSiteIdParameter Parse(string identity)
		{
			return new AdSiteIdParameter(identity);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0001E590 File Offset: 0x0001C790
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (typeof(T) != typeof(ADSite))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			return base.GetObjects<T>(session.GetConfigurationNamingContext().GetChildId("Sites"), session, subTreeSession, optionalData, out notFoundReason);
		}
	}
}
