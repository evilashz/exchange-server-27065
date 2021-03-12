using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000994 RID: 2452
	[MessageContract(WrapperName = "StartServiceInstanceMoveTask", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11", IsWrapped = true)]
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public class StartServiceInstanceMoveTaskRequest
	{
		// Token: 0x0600717E RID: 29054 RVA: 0x001784CD File Offset: 0x001766CD
		public StartServiceInstanceMoveTaskRequest()
		{
		}

		// Token: 0x0600717F RID: 29055 RVA: 0x001784D5 File Offset: 0x001766D5
		public StartServiceInstanceMoveTaskRequest(string contextId, string oldServiceInstance, string newServiceInstance, ServiceInstanceMoveOptions options)
		{
			this.contextId = contextId;
			this.oldServiceInstance = oldServiceInstance;
			this.newServiceInstance = newServiceInstance;
			this.options = options;
		}

		// Token: 0x04004997 RID: 18839
		[XmlElement(IsNullable = true)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11", Order = 0)]
		public string contextId;

		// Token: 0x04004998 RID: 18840
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11", Order = 1)]
		[XmlElement(IsNullable = true)]
		public string oldServiceInstance;

		// Token: 0x04004999 RID: 18841
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11", Order = 2)]
		[XmlElement(IsNullable = true)]
		public string newServiceInstance;

		// Token: 0x0400499A RID: 18842
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11", Order = 3)]
		public ServiceInstanceMoveOptions options;
	}
}
