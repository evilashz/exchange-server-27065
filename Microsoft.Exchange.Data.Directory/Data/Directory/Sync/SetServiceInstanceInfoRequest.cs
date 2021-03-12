using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200098A RID: 2442
	[MessageContract(WrapperName = "SetServiceInstanceInfo", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11", IsWrapped = true)]
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public class SetServiceInstanceInfoRequest
	{
		// Token: 0x0600714B RID: 29003 RVA: 0x00178165 File Offset: 0x00176365
		public SetServiceInstanceInfoRequest()
		{
		}

		// Token: 0x0600714C RID: 29004 RVA: 0x0017816D File Offset: 0x0017636D
		public SetServiceInstanceInfoRequest(string serviceInstance, ServiceInstanceInfoValue serviceInstanceInfo)
		{
			this.serviceInstance = serviceInstance;
			this.serviceInstanceInfo = serviceInstanceInfo;
		}

		// Token: 0x0400498B RID: 18827
		[XmlElement(IsNullable = true)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11", Order = 0)]
		public string serviceInstance;

		// Token: 0x0400498C RID: 18828
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11", Order = 1)]
		[XmlElement(IsNullable = true)]
		public ServiceInstanceInfoValue serviceInstanceInfo;
	}
}
