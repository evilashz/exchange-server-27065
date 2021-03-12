using System;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x02000002 RID: 2
	public interface IAmServerNameLookup
	{
		// Token: 0x06000001 RID: 1
		void Enable();

		// Token: 0x06000002 RID: 2
		string GetFqdn(string shortNodeName);

		// Token: 0x06000003 RID: 3
		string GetFqdn(string shortNodeName, bool throwException);

		// Token: 0x06000004 RID: 4
		void RemoveEntry(string serverName);

		// Token: 0x06000005 RID: 5
		void PopulateForDag();
	}
}
