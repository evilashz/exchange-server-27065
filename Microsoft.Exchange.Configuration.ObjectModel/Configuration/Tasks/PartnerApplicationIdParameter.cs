using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000137 RID: 311
	[Serializable]
	public class PartnerApplicationIdParameter : ADIdParameter
	{
		// Token: 0x06000B17 RID: 2839 RVA: 0x00023B62 File Offset: 0x00021D62
		public PartnerApplicationIdParameter()
		{
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00023B6A File Offset: 0x00021D6A
		public PartnerApplicationIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00023B73 File Offset: 0x00021D73
		public PartnerApplicationIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00023B7C File Offset: 0x00021D7C
		public PartnerApplicationIdParameter(PartnerApplication app) : base(app.Id)
		{
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00023B8A File Offset: 0x00021D8A
		public PartnerApplicationIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00023B93 File Offset: 0x00021D93
		public static PartnerApplicationIdParameter Parse(string identity)
		{
			return new PartnerApplicationIdParameter(identity);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00023B9C File Offset: 0x00021D9C
		internal override IEnumerable<T> GetObjectsInOrganization<T>(string identityString, ADObjectId rootId, IDirectorySession session, OptionalIdentityData optionalData)
		{
			bool isRedirectedToSharedConfig = session.SessionSettings.IsRedirectedToSharedConfig;
			IEnumerable<T> objectsInOrganization;
			try
			{
				session.SessionSettings.IsRedirectedToSharedConfig = false;
				objectsInOrganization = base.GetObjectsInOrganization<T>(identityString, rootId, session, optionalData);
			}
			finally
			{
				session.SessionSettings.IsRedirectedToSharedConfig = isRedirectedToSharedConfig;
			}
			return objectsInOrganization;
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x00023BF0 File Offset: 0x00021DF0
		internal override ADPropertyDefinition[] AdditionalMatchingProperties
		{
			get
			{
				return new ADPropertyDefinition[]
				{
					PartnerApplicationSchema.ApplicationIdentifier
				};
			}
		}
	}
}
