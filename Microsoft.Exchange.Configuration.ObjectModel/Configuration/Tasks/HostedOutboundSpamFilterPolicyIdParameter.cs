using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000114 RID: 276
	[Serializable]
	public class HostedOutboundSpamFilterPolicyIdParameter : ADIdParameter
	{
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x000215AE File Offset: 0x0001F7AE
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Dehydrateable;
			}
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x000215B1 File Offset: 0x0001F7B1
		public HostedOutboundSpamFilterPolicyIdParameter()
		{
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x000215B9 File Offset: 0x0001F7B9
		public HostedOutboundSpamFilterPolicyIdParameter(ADObjectId adobjectid) : base(adobjectid)
		{
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x000215C2 File Offset: 0x0001F7C2
		public HostedOutboundSpamFilterPolicyIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x000215CB File Offset: 0x0001F7CB
		public HostedOutboundSpamFilterPolicyIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x000215D4 File Offset: 0x0001F7D4
		public static HostedOutboundSpamFilterPolicyIdParameter Parse(string identity)
		{
			return new HostedOutboundSpamFilterPolicyIdParameter(identity);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x000215DC File Offset: 0x0001F7DC
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			if (typeof(T) != typeof(HostedOutboundSpamFilterPolicy))
			{
				throw new ArgumentException(Strings.ErrorInvalidType(typeof(T).Name), "type");
			}
			return base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason);
		}
	}
}
