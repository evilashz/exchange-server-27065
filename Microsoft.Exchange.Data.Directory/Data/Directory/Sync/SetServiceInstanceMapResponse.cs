using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000987 RID: 2439
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName = "SetServiceInstanceMapResponse", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11", IsWrapped = true)]
	[DebuggerStepThrough]
	public class SetServiceInstanceMapResponse
	{
		// Token: 0x06007145 RID: 28997 RVA: 0x00178120 File Offset: 0x00176320
		public SetServiceInstanceMapResponse()
		{
		}

		// Token: 0x06007146 RID: 28998 RVA: 0x00178128 File Offset: 0x00176328
		public SetServiceInstanceMapResponse(ResultCode SetServiceInstanceMapResult)
		{
			this.SetServiceInstanceMapResult = SetServiceInstanceMapResult;
		}

		// Token: 0x04004988 RID: 18824
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11", Order = 0)]
		public ResultCode SetServiceInstanceMapResult;
	}
}
