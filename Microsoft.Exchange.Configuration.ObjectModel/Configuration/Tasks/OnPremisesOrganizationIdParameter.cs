using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000130 RID: 304
	[Serializable]
	public class OnPremisesOrganizationIdParameter : ADIdParameter
	{
		// Token: 0x06000AE1 RID: 2785 RVA: 0x00023457 File Offset: 0x00021657
		public OnPremisesOrganizationIdParameter()
		{
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0002345F File Offset: 0x0002165F
		public OnPremisesOrganizationIdParameter(ADObjectId adobjectid) : base(adobjectid)
		{
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00023468 File Offset: 0x00021668
		protected OnPremisesOrganizationIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00023471 File Offset: 0x00021671
		public OnPremisesOrganizationIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0002347A File Offset: 0x0002167A
		public static OnPremisesOrganizationIdParameter Parse(string identity)
		{
			return new OnPremisesOrganizationIdParameter(identity);
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00023484 File Offset: 0x00021684
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (typeof(T) != typeof(OnPremisesOrganization))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			return base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason);
		}
	}
}
