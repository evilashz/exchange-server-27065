using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200098D RID: 2445
	[DebuggerStepThrough]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName = "QueueEduFaultinResponse", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11", IsWrapped = true)]
	public class QueueEduFaultinResponse
	{
		// Token: 0x06007151 RID: 29009 RVA: 0x001781B8 File Offset: 0x001763B8
		public QueueEduFaultinResponse()
		{
		}

		// Token: 0x06007152 RID: 29010 RVA: 0x001781C0 File Offset: 0x001763C0
		public QueueEduFaultinResponse(ExchangeFaultinStatus QueueEduFaultinResult)
		{
			this.QueueEduFaultinResult = QueueEduFaultinResult;
		}

		// Token: 0x04004990 RID: 18832
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11", Order = 0)]
		public ExchangeFaultinStatus QueueEduFaultinResult;
	}
}
