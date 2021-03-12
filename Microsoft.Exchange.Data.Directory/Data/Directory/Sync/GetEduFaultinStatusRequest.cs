using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200098E RID: 2446
	[MessageContract(WrapperName = "GetEduFaultinStatus", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11", IsWrapped = true)]
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public class GetEduFaultinStatusRequest
	{
		// Token: 0x06007153 RID: 29011 RVA: 0x001781CF File Offset: 0x001763CF
		public GetEduFaultinStatusRequest()
		{
		}

		// Token: 0x06007154 RID: 29012 RVA: 0x001781D7 File Offset: 0x001763D7
		public GetEduFaultinStatusRequest(string[] contextIds)
		{
			this.contextIds = contextIds;
		}

		// Token: 0x04004991 RID: 18833
		[XmlArrayItem("guid", Namespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays")]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11", Order = 0)]
		[XmlArray(IsNullable = true)]
		public string[] contextIds;
	}
}
