using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000158 RID: 344
	[Serializable]
	public class SystemAttendantIdParameter : ServerIdParameter
	{
		// Token: 0x06000C6B RID: 3179 RVA: 0x000270F5 File Offset: 0x000252F5
		public SystemAttendantIdParameter(string identity) : base(identity)
		{
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x000270FE File Offset: 0x000252FE
		public SystemAttendantIdParameter()
		{
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x00027106 File Offset: 0x00025306
		public SystemAttendantIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0002710F File Offset: 0x0002530F
		public SystemAttendantIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x00027118 File Offset: 0x00025318
		public new static SystemAttendantIdParameter Parse(string identity)
		{
			return new SystemAttendantIdParameter(identity);
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00027120 File Offset: 0x00025320
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(session.DomainController, true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 104, "GetObjects", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\IdentityParameter\\SystemAttendantIdParameter.cs");
			IEnumerable<Server> objects = base.GetObjects<Server>(rootId, topologyConfigurationSession, topologyConfigurationSession, null, out notFoundReason);
			List<T> list = new List<T>();
			foreach (Server server in objects)
			{
				ADObjectId childId = server.Id.GetChildId("Microsoft System Attendant");
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, "exchangeAdminService");
				IEnumerable<ADRecipient> enumerable = base.PerformPrimarySearch<ADRecipient>(filter, childId, session, true, optionalData);
				int num = 0;
				foreach (ADRecipient adrecipient in enumerable)
				{
					list.Add((T)((object)adrecipient));
					num++;
				}
			}
			return list;
		}
	}
}
