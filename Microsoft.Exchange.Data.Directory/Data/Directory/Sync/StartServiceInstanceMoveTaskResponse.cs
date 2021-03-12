using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000995 RID: 2453
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(WrapperName = "StartServiceInstanceMoveTaskResponse", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11", IsWrapped = true)]
	[DebuggerStepThrough]
	public class StartServiceInstanceMoveTaskResponse
	{
		// Token: 0x06007180 RID: 29056 RVA: 0x001784FA File Offset: 0x001766FA
		public StartServiceInstanceMoveTaskResponse()
		{
		}

		// Token: 0x06007181 RID: 29057 RVA: 0x00178502 File Offset: 0x00176702
		public StartServiceInstanceMoveTaskResponse(ServiceInstanceMoveOperationResult StartServiceInstanceMoveTaskResult)
		{
			this.StartServiceInstanceMoveTaskResult = StartServiceInstanceMoveTaskResult;
		}

		// Token: 0x0400499B RID: 18843
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11", Order = 0)]
		[XmlElement(IsNullable = true)]
		public ServiceInstanceMoveOperationResult StartServiceInstanceMoveTaskResult;
	}
}
