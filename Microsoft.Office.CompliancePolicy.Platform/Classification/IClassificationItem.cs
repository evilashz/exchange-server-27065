using System;
using System.IO;

namespace Microsoft.Office.CompliancePolicy.Classification
{
	// Token: 0x0200000C RID: 12
	public interface IClassificationItem
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000040 RID: 64
		Stream Content { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000041 RID: 65
		string ItemId { get; }

		// Token: 0x06000042 RID: 66
		void SetClassificationResults(ICAClassificationResultCollection results);
	}
}
