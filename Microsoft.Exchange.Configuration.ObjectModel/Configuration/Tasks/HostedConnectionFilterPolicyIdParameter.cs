using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000112 RID: 274
	[Serializable]
	public class HostedConnectionFilterPolicyIdParameter : ADIdParameter
	{
		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x000214C1 File Offset: 0x0001F6C1
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Dehydrateable;
			}
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x000214C4 File Offset: 0x0001F6C4
		public HostedConnectionFilterPolicyIdParameter()
		{
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x000214CC File Offset: 0x0001F6CC
		public HostedConnectionFilterPolicyIdParameter(ADObjectId adobjectid) : base(adobjectid)
		{
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x000214D5 File Offset: 0x0001F6D5
		public HostedConnectionFilterPolicyIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x000214DE File Offset: 0x0001F6DE
		public HostedConnectionFilterPolicyIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x000214E7 File Offset: 0x0001F6E7
		public static HostedConnectionFilterPolicyIdParameter Parse(string identity)
		{
			return new HostedConnectionFilterPolicyIdParameter(identity);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x000214F0 File Offset: 0x0001F6F0
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (typeof(T) != typeof(HostedConnectionFilterPolicy))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			return base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason);
		}
	}
}
