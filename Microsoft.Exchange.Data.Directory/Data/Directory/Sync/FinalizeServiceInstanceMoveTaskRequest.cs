using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000998 RID: 2456
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DebuggerStepThrough]
	[MessageContract(WrapperName = "FinalizeServiceInstanceMoveTask", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11", IsWrapped = true)]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public class FinalizeServiceInstanceMoveTaskRequest
	{
		// Token: 0x06007186 RID: 29062 RVA: 0x00178546 File Offset: 0x00176746
		public FinalizeServiceInstanceMoveTaskRequest()
		{
		}

		// Token: 0x06007187 RID: 29063 RVA: 0x0017854E File Offset: 0x0017674E
		public FinalizeServiceInstanceMoveTaskRequest(ServiceInstanceMoveTask serviceInstanceMoveTask, byte[] lastCookie)
		{
			this.serviceInstanceMoveTask = serviceInstanceMoveTask;
			this.lastCookie = lastCookie;
		}

		// Token: 0x0400499F RID: 18847
		[XmlElement(IsNullable = true)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11", Order = 0)]
		public ServiceInstanceMoveTask serviceInstanceMoveTask;

		// Token: 0x040049A0 RID: 18848
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11", Order = 1)]
		[XmlElement(DataType = "base64Binary", IsNullable = true)]
		public byte[] lastCookie;
	}
}
