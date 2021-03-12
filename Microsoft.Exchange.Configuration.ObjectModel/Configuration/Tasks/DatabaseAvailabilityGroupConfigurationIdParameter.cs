using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000FE RID: 254
	[Serializable]
	public class DatabaseAvailabilityGroupConfigurationIdParameter : ADIdParameter
	{
		// Token: 0x0600092A RID: 2346 RVA: 0x0001FC89 File Offset: 0x0001DE89
		public DatabaseAvailabilityGroupConfigurationIdParameter(DatabaseAvailabilityGroupConfiguration dagConfig) : base(dagConfig.Id)
		{
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0001FC97 File Offset: 0x0001DE97
		public DatabaseAvailabilityGroupConfigurationIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0001FCA0 File Offset: 0x0001DEA0
		public DatabaseAvailabilityGroupConfigurationIdParameter()
		{
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0001FCA8 File Offset: 0x0001DEA8
		public DatabaseAvailabilityGroupConfigurationIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0001FCB1 File Offset: 0x0001DEB1
		protected DatabaseAvailabilityGroupConfigurationIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0001FCBA File Offset: 0x0001DEBA
		public static DatabaseAvailabilityGroupConfigurationIdParameter Parse(string identity)
		{
			return new DatabaseAvailabilityGroupConfigurationIdParameter(identity);
		}
	}
}
