using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200099C RID: 2460
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName = "RecoverServiceInstanceMoveTask", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11", IsWrapped = true)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DebuggerStepThrough]
	public class RecoverServiceInstanceMoveTaskRequest
	{
		// Token: 0x0600718E RID: 29070 RVA: 0x001785A9 File Offset: 0x001767A9
		public RecoverServiceInstanceMoveTaskRequest()
		{
		}

		// Token: 0x0600718F RID: 29071 RVA: 0x001785B1 File Offset: 0x001767B1
		public RecoverServiceInstanceMoveTaskRequest(ServiceInstanceMoveTask serviceInstanceMoveTask)
		{
			this.serviceInstanceMoveTask = serviceInstanceMoveTask;
		}

		// Token: 0x040049A4 RID: 18852
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11", Order = 0)]
		[XmlElement(IsNullable = true)]
		public ServiceInstanceMoveTask serviceInstanceMoveTask;
	}
}
