using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000989 RID: 2441
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName = "GetServiceInstanceInfoResponse", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11", IsWrapped = true)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public class GetServiceInstanceInfoResponse
	{
		// Token: 0x06007149 RID: 29001 RVA: 0x0017814E File Offset: 0x0017634E
		public GetServiceInstanceInfoResponse()
		{
		}

		// Token: 0x0600714A RID: 29002 RVA: 0x00178156 File Offset: 0x00176356
		public GetServiceInstanceInfoResponse(ServiceInstanceInfoValue GetServiceInstanceInfoResult)
		{
			this.GetServiceInstanceInfoResult = GetServiceInstanceInfoResult;
		}

		// Token: 0x0400498A RID: 18826
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11", Order = 0)]
		[XmlElement(IsNullable = true)]
		public ServiceInstanceInfoValue GetServiceInstanceInfoResult;
	}
}
