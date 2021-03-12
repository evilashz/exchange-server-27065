using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.InfoWorker.Common.ELC
{
	// Token: 0x0200018D RID: 397
	internal class AdPolicyReader : AdReader
	{
		// Token: 0x06000AA4 RID: 2724 RVA: 0x0002D18C File Offset: 0x0002B38C
		internal static List<RetentionPolicy> GetAllRetentionPolicies(IConfigurationSession session, OrganizationId organizationId)
		{
			ADPagedReader<RetentionPolicy> source = session.FindPaged<RetentionPolicy>(null, QueryScope.SubTree, null, null, 0);
			List<RetentionPolicy> list = source.ToList<RetentionPolicy>();
			string arg = (organizationId.ConfigurationUnit == null) ? "First Organization" : organizationId.ConfigurationUnit.ToString();
			AdReader.Tracer.TraceDebug<int, string>(0L, "Found {0} retention policies for {1} in AD.", list.Count, arg);
			return list;
		}
	}
}
