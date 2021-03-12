using System;
using System.Collections.Generic;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200000A RID: 10
	public interface IPersistence
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000026 RID: 38
		LocalDataAccessMetaData LocalDataAccessMetaData { get; }

		// Token: 0x06000027 RID: 39
		void Initialize(Dictionary<string, string> propertyBag, LocalDataAccessMetaData metaData);

		// Token: 0x06000028 RID: 40
		void SetProperties(Dictionary<string, string> propertyBag);

		// Token: 0x06000029 RID: 41
		void Write(Action<IPersistence> preWriteHandler = null);
	}
}
