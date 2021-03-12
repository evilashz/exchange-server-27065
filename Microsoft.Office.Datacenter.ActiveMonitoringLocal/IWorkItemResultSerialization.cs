using System;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200000B RID: 11
	public interface IWorkItemResultSerialization
	{
		// Token: 0x0600002A RID: 42
		string Serialize();

		// Token: 0x0600002B RID: 43
		void Deserialize(string result);
	}
}
