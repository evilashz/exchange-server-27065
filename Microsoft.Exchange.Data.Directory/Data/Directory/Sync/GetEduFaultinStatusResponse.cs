using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200098F RID: 2447
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName = "GetEduFaultinStatusResponse", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11", IsWrapped = true)]
	[DebuggerStepThrough]
	public class GetEduFaultinStatusResponse
	{
		// Token: 0x06007155 RID: 29013 RVA: 0x001781E6 File Offset: 0x001763E6
		public GetEduFaultinStatusResponse()
		{
		}

		// Token: 0x06007156 RID: 29014 RVA: 0x001781EE File Offset: 0x001763EE
		public GetEduFaultinStatusResponse(ExchangeFaultinStatus[] GetEduFaultinStatusResult)
		{
			this.GetEduFaultinStatusResult = GetEduFaultinStatusResult;
		}

		// Token: 0x04004992 RID: 18834
		[XmlArrayItem(IsNullable = false)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11", Order = 0)]
		public ExchangeFaultinStatus[] GetEduFaultinStatusResult;
	}
}
