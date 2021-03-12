using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000D5 RID: 213
	[Serializable]
	public class AcceptedDomainIdParameter : ADIdParameter
	{
		// Token: 0x060007DD RID: 2013 RVA: 0x0001CFEB File Offset: 0x0001B1EB
		public AcceptedDomainIdParameter()
		{
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0001CFF3 File Offset: 0x0001B1F3
		public AcceptedDomainIdParameter(ADObjectId adobjectid) : base(adobjectid)
		{
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0001CFFC File Offset: 0x0001B1FC
		protected AcceptedDomainIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0001D005 File Offset: 0x0001B205
		public AcceptedDomainIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0001D00E File Offset: 0x0001B20E
		public static AcceptedDomainIdParameter Parse(string identity)
		{
			return new AcceptedDomainIdParameter(identity);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0001D018 File Offset: 0x0001B218
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (typeof(T) != typeof(AcceptedDomain))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			return base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason);
		}
	}
}
