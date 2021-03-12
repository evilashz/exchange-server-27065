using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200096D RID: 2413
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName = "PublishResponse", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", IsWrapped = true)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DebuggerStepThrough]
	public class PublishResponse
	{
		// Token: 0x060070DC RID: 28892 RVA: 0x00177863 File Offset: 0x00175A63
		public PublishResponse()
		{
		}

		// Token: 0x060070DD RID: 28893 RVA: 0x0017786B File Offset: 0x00175A6B
		public PublishResponse(UpdateResultCode[] PublishResult)
		{
			this.PublishResult = PublishResult;
		}

		// Token: 0x04004951 RID: 18769
		[XmlArray(IsNullable = true)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 0)]
		public UpdateResultCode[] PublishResult;
	}
}
