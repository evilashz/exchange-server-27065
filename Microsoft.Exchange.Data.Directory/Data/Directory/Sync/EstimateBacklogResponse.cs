using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200097F RID: 2431
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(WrapperName = "EstimateBacklogResponse", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", IsWrapped = true)]
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public class EstimateBacklogResponse
	{
		// Token: 0x060070FC RID: 28924 RVA: 0x00177A48 File Offset: 0x00175C48
		public EstimateBacklogResponse()
		{
		}

		// Token: 0x060070FD RID: 28925 RVA: 0x00177A50 File Offset: 0x00175C50
		public EstimateBacklogResponse(BacklogEstimateBatch EstimateBacklogResult)
		{
			this.EstimateBacklogResult = EstimateBacklogResult;
		}

		// Token: 0x0400497E RID: 18814
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 0)]
		public BacklogEstimateBatch EstimateBacklogResult;
	}
}
