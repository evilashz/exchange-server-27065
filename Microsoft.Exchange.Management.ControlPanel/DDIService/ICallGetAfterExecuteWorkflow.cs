using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200018B RID: 395
	public interface ICallGetAfterExecuteWorkflow
	{
		// Token: 0x17001AA6 RID: 6822
		// (get) Token: 0x06002298 RID: 8856
		// (set) Token: 0x06002299 RID: 8857
		[DefaultValue(false)]
		bool IgnoreGetObject { get; set; }
	}
}
