using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000996 RID: 2454
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(WrapperName = "GetServiceInstanceMoveTaskStatus", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11", IsWrapped = true)]
	[DebuggerStepThrough]
	public class GetServiceInstanceMoveTaskStatusRequest
	{
		// Token: 0x06007182 RID: 29058 RVA: 0x00178511 File Offset: 0x00176711
		public GetServiceInstanceMoveTaskStatusRequest()
		{
		}

		// Token: 0x06007183 RID: 29059 RVA: 0x00178519 File Offset: 0x00176719
		public GetServiceInstanceMoveTaskStatusRequest(ServiceInstanceMoveTask serviceInstanceMoveTask, byte[] lastCookie)
		{
			this.serviceInstanceMoveTask = serviceInstanceMoveTask;
			this.lastCookie = lastCookie;
		}

		// Token: 0x0400499C RID: 18844
		[XmlElement(IsNullable = true)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11", Order = 0)]
		public ServiceInstanceMoveTask serviceInstanceMoveTask;

		// Token: 0x0400499D RID: 18845
		[XmlElement(DataType = "base64Binary", IsNullable = true)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11", Order = 1)]
		public byte[] lastCookie;
	}
}
