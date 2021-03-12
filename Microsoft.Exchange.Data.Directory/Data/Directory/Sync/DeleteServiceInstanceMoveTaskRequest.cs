using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200099A RID: 2458
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DebuggerStepThrough]
	[MessageContract(WrapperName = "DeleteServiceInstanceMoveTask", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11", IsWrapped = true)]
	public class DeleteServiceInstanceMoveTaskRequest
	{
		// Token: 0x0600718A RID: 29066 RVA: 0x0017857B File Offset: 0x0017677B
		public DeleteServiceInstanceMoveTaskRequest()
		{
		}

		// Token: 0x0600718B RID: 29067 RVA: 0x00178583 File Offset: 0x00176783
		public DeleteServiceInstanceMoveTaskRequest(ServiceInstanceMoveTask serviceInstanceMoveTask)
		{
			this.serviceInstanceMoveTask = serviceInstanceMoveTask;
		}

		// Token: 0x040049A2 RID: 18850
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11", Order = 0)]
		[XmlElement(IsNullable = true)]
		public ServiceInstanceMoveTask serviceInstanceMoveTask;
	}
}
