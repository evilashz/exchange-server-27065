using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200097D RID: 2429
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(WrapperName = "FilterAndGetContextRecoveryInfoResponse", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", IsWrapped = true)]
	[DebuggerStepThrough]
	public class FilterAndGetContextRecoveryInfoResponse
	{
		// Token: 0x060070F8 RID: 28920 RVA: 0x00177A13 File Offset: 0x00175C13
		public FilterAndGetContextRecoveryInfoResponse()
		{
		}

		// Token: 0x060070F9 RID: 28921 RVA: 0x00177A1B File Offset: 0x00175C1B
		public FilterAndGetContextRecoveryInfoResponse(ContextRecoveryInfo FilterAndGetContextRecoveryInfoResult)
		{
			this.FilterAndGetContextRecoveryInfoResult = FilterAndGetContextRecoveryInfoResult;
		}

		// Token: 0x0400497B RID: 18811
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 0)]
		public ContextRecoveryInfo FilterAndGetContextRecoveryInfoResult;
	}
}
