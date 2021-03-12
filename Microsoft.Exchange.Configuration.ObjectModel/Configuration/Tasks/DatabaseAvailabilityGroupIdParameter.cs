using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000FD RID: 253
	[Serializable]
	public class DatabaseAvailabilityGroupIdParameter : ADIdParameter
	{
		// Token: 0x06000924 RID: 2340 RVA: 0x0001FC50 File Offset: 0x0001DE50
		public DatabaseAvailabilityGroupIdParameter(DatabaseAvailabilityGroup dag) : base(dag.Id)
		{
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0001FC5E File Offset: 0x0001DE5E
		public DatabaseAvailabilityGroupIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0001FC67 File Offset: 0x0001DE67
		public DatabaseAvailabilityGroupIdParameter()
		{
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0001FC6F File Offset: 0x0001DE6F
		public DatabaseAvailabilityGroupIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0001FC78 File Offset: 0x0001DE78
		protected DatabaseAvailabilityGroupIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0001FC81 File Offset: 0x0001DE81
		public static DatabaseAvailabilityGroupIdParameter Parse(string identity)
		{
			return new DatabaseAvailabilityGroupIdParameter(identity);
		}
	}
}
