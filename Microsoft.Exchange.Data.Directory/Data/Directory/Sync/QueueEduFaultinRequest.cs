using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200098C RID: 2444
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName = "QueueEduFaultin", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11", IsWrapped = true)]
	[DebuggerStepThrough]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public class QueueEduFaultinRequest
	{
		// Token: 0x0600714F RID: 29007 RVA: 0x0017819A File Offset: 0x0017639A
		public QueueEduFaultinRequest()
		{
		}

		// Token: 0x06007150 RID: 29008 RVA: 0x001781A2 File Offset: 0x001763A2
		public QueueEduFaultinRequest(string serviceInstance, string contextId)
		{
			this.serviceInstance = serviceInstance;
			this.contextId = contextId;
		}

		// Token: 0x0400498E RID: 18830
		[XmlElement(IsNullable = true)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11", Order = 0)]
		public string serviceInstance;

		// Token: 0x0400498F RID: 18831
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11", Order = 1)]
		[XmlElement(IsNullable = true)]
		public string contextId;
	}
}
