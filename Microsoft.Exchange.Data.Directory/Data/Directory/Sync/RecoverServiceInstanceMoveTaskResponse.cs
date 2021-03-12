using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200099D RID: 2461
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName = "RecoverServiceInstanceMoveTaskResponse", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11", IsWrapped = true)]
	public class RecoverServiceInstanceMoveTaskResponse
	{
		// Token: 0x06007190 RID: 29072 RVA: 0x001785C0 File Offset: 0x001767C0
		public RecoverServiceInstanceMoveTaskResponse()
		{
		}

		// Token: 0x06007191 RID: 29073 RVA: 0x001785C8 File Offset: 0x001767C8
		public RecoverServiceInstanceMoveTaskResponse(ServiceInstanceMoveOperationResult RecoverServiceInstanceMoveTaskResult)
		{
			this.RecoverServiceInstanceMoveTaskResult = RecoverServiceInstanceMoveTaskResult;
		}

		// Token: 0x040049A5 RID: 18853
		[XmlElement(IsNullable = true)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11", Order = 0)]
		public ServiceInstanceMoveOperationResult RecoverServiceInstanceMoveTaskResult;
	}
}
