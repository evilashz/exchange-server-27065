using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200098B RID: 2443
	[DebuggerStepThrough]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(WrapperName = "SetServiceInstanceInfoResponse", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11", IsWrapped = true)]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public class SetServiceInstanceInfoResponse
	{
		// Token: 0x0600714D RID: 29005 RVA: 0x00178183 File Offset: 0x00176383
		public SetServiceInstanceInfoResponse()
		{
		}

		// Token: 0x0600714E RID: 29006 RVA: 0x0017818B File Offset: 0x0017638B
		public SetServiceInstanceInfoResponse(ResultCode SetServiceInstanceInfoResult)
		{
			this.SetServiceInstanceInfoResult = SetServiceInstanceInfoResult;
		}

		// Token: 0x0400498D RID: 18829
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11", Order = 0)]
		public ResultCode SetServiceInstanceInfoResult;
	}
}
