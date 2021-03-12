using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000984 RID: 2436
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DebuggerStepThrough]
	[MessageContract(WrapperName = "GetServiceInstanceMapResponse", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11", IsWrapped = true)]
	public class GetServiceInstanceMapResponse
	{
		// Token: 0x06007141 RID: 28993 RVA: 0x001780EB File Offset: 0x001762EB
		public GetServiceInstanceMapResponse()
		{
		}

		// Token: 0x06007142 RID: 28994 RVA: 0x001780F3 File Offset: 0x001762F3
		public GetServiceInstanceMapResponse(ServiceInstanceMapValue GetServiceInstanceMapResult)
		{
			this.GetServiceInstanceMapResult = GetServiceInstanceMapResult;
		}

		// Token: 0x04004980 RID: 18816
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11", Order = 0)]
		[XmlElement(IsNullable = true)]
		public ServiceInstanceMapValue GetServiceInstanceMapResult;
	}
}
