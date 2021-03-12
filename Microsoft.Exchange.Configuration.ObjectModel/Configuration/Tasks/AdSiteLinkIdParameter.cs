using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000E9 RID: 233
	[Serializable]
	public class AdSiteLinkIdParameter : ADIdParameter
	{
		// Token: 0x06000872 RID: 2162 RVA: 0x0001E5F8 File Offset: 0x0001C7F8
		public AdSiteLinkIdParameter()
		{
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0001E600 File Offset: 0x0001C800
		public AdSiteLinkIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0001E609 File Offset: 0x0001C809
		public AdSiteLinkIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0001E612 File Offset: 0x0001C812
		public AdSiteLinkIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0001E61B File Offset: 0x0001C81B
		public static AdSiteLinkIdParameter Parse(string identity)
		{
			return new AdSiteLinkIdParameter(identity);
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0001E624 File Offset: 0x0001C824
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (typeof(T) != typeof(ADSiteLink))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			return base.GetObjects<T>(session.GetConfigurationNamingContext().GetChildId("Sites"), session, subTreeSession, optionalData, out notFoundReason);
		}
	}
}
