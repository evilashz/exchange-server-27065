using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200099B RID: 2459
	[DebuggerStepThrough]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(WrapperName = "DeleteServiceInstanceMoveTaskResponse", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11", IsWrapped = true)]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public class DeleteServiceInstanceMoveTaskResponse
	{
		// Token: 0x0600718C RID: 29068 RVA: 0x00178592 File Offset: 0x00176792
		public DeleteServiceInstanceMoveTaskResponse()
		{
		}

		// Token: 0x0600718D RID: 29069 RVA: 0x0017859A File Offset: 0x0017679A
		public DeleteServiceInstanceMoveTaskResponse(ServiceInstanceMoveOperationResult DeleteServiceInstanceMoveTaskResult)
		{
			this.DeleteServiceInstanceMoveTaskResult = DeleteServiceInstanceMoveTaskResult;
		}

		// Token: 0x040049A3 RID: 18851
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11", Order = 0)]
		[XmlElement(IsNullable = true)]
		public ServiceInstanceMoveOperationResult DeleteServiceInstanceMoveTaskResult;
	}
}
