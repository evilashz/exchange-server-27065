using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000983 RID: 2435
	[MessageContract(WrapperName = "GetServiceInstanceMap", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11", IsWrapped = true)]
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public class GetServiceInstanceMapRequest
	{
		// Token: 0x0600713F RID: 28991 RVA: 0x001780D4 File Offset: 0x001762D4
		public GetServiceInstanceMapRequest()
		{
		}

		// Token: 0x06007140 RID: 28992 RVA: 0x001780DC File Offset: 0x001762DC
		public GetServiceInstanceMapRequest(string serviceType)
		{
			this.serviceType = serviceType;
		}

		// Token: 0x0400497F RID: 18815
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11", Order = 0)]
		[XmlElement(IsNullable = true)]
		public string serviceType;
	}
}
