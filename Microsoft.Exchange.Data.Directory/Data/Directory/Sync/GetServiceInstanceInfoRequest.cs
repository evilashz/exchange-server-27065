using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000988 RID: 2440
	[MessageContract(WrapperName = "GetServiceInstanceInfo", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11", IsWrapped = true)]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DebuggerStepThrough]
	public class GetServiceInstanceInfoRequest
	{
		// Token: 0x06007147 RID: 28999 RVA: 0x00178137 File Offset: 0x00176337
		public GetServiceInstanceInfoRequest()
		{
		}

		// Token: 0x06007148 RID: 29000 RVA: 0x0017813F File Offset: 0x0017633F
		public GetServiceInstanceInfoRequest(string serviceInstance)
		{
			this.serviceInstance = serviceInstance;
		}

		// Token: 0x04004989 RID: 18825
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11", Order = 0)]
		[XmlElement(IsNullable = true)]
		public string serviceInstance;
	}
}
