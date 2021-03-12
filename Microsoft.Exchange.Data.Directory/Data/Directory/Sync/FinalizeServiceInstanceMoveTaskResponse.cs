using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000999 RID: 2457
	[MessageContract(WrapperName = "FinalizeServiceInstanceMoveTaskResponse", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11", IsWrapped = true)]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DebuggerStepThrough]
	public class FinalizeServiceInstanceMoveTaskResponse
	{
		// Token: 0x06007188 RID: 29064 RVA: 0x00178564 File Offset: 0x00176764
		public FinalizeServiceInstanceMoveTaskResponse()
		{
		}

		// Token: 0x06007189 RID: 29065 RVA: 0x0017856C File Offset: 0x0017676C
		public FinalizeServiceInstanceMoveTaskResponse(ServiceInstanceMoveOperationResult FinalizeServiceInstanceMoveTaskResult)
		{
			this.FinalizeServiceInstanceMoveTaskResult = FinalizeServiceInstanceMoveTaskResult;
		}

		// Token: 0x040049A1 RID: 18849
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11", Order = 0)]
		[XmlElement(IsNullable = true)]
		public ServiceInstanceMoveOperationResult FinalizeServiceInstanceMoveTaskResult;
	}
}
