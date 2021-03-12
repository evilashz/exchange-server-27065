using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200097B RID: 2427
	[DebuggerStepThrough]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(WrapperName = "GetCookieUpdateStatusResponse", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", IsWrapped = true)]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public class GetCookieUpdateStatusResponse
	{
		// Token: 0x060070F4 RID: 28916 RVA: 0x001779DE File Offset: 0x00175BDE
		public GetCookieUpdateStatusResponse()
		{
		}

		// Token: 0x060070F5 RID: 28917 RVA: 0x001779E6 File Offset: 0x00175BE6
		public GetCookieUpdateStatusResponse(CookieUpdateStatus GetCookieUpdateStatusResult)
		{
			this.GetCookieUpdateStatusResult = GetCookieUpdateStatusResult;
		}

		// Token: 0x04004978 RID: 18808
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 0)]
		public CookieUpdateStatus GetCookieUpdateStatusResult;
	}
}
