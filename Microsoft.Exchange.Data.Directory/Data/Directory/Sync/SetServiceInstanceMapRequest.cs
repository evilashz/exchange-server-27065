using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000986 RID: 2438
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName = "SetServiceInstanceMap", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11", IsWrapped = true)]
	[DebuggerStepThrough]
	public class SetServiceInstanceMapRequest
	{
		// Token: 0x06007143 RID: 28995 RVA: 0x00178102 File Offset: 0x00176302
		public SetServiceInstanceMapRequest()
		{
		}

		// Token: 0x06007144 RID: 28996 RVA: 0x0017810A File Offset: 0x0017630A
		public SetServiceInstanceMapRequest(string serviceType, ServiceInstanceMapValue serviceInstanceMap)
		{
			this.serviceType = serviceType;
			this.serviceInstanceMap = serviceInstanceMap;
		}

		// Token: 0x04004986 RID: 18822
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11", Order = 0)]
		[XmlElement(IsNullable = true)]
		public string serviceType;

		// Token: 0x04004987 RID: 18823
		[XmlElement(IsNullable = true)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11", Order = 1)]
		public ServiceInstanceMapValue serviceInstanceMap;
	}
}
