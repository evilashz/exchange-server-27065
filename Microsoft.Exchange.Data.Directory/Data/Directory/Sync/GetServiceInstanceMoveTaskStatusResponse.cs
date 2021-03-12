using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000997 RID: 2455
	[MessageContract(WrapperName = "GetServiceInstanceMoveTaskStatusResponse", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11", IsWrapped = true)]
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public class GetServiceInstanceMoveTaskStatusResponse
	{
		// Token: 0x06007184 RID: 29060 RVA: 0x0017852F File Offset: 0x0017672F
		public GetServiceInstanceMoveTaskStatusResponse()
		{
		}

		// Token: 0x06007185 RID: 29061 RVA: 0x00178537 File Offset: 0x00176737
		public GetServiceInstanceMoveTaskStatusResponse(ServiceInstanceMoveOperationResult GetServiceInstanceMoveTaskStatusResult)
		{
			this.GetServiceInstanceMoveTaskStatusResult = GetServiceInstanceMoveTaskStatusResult;
		}

		// Token: 0x0400499E RID: 18846
		[XmlElement(IsNullable = true)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11", Order = 0)]
		public ServiceInstanceMoveOperationResult GetServiceInstanceMoveTaskStatusResult;
	}
}
