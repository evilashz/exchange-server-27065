using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200096F RID: 2415
	[MessageContract(WrapperName = "GetContextResponse", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", IsWrapped = true)]
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public class GetContextResponse
	{
		// Token: 0x060070E0 RID: 28896 RVA: 0x0017789F File Offset: 0x00175A9F
		public GetContextResponse()
		{
		}

		// Token: 0x060070E1 RID: 28897 RVA: 0x001778A7 File Offset: 0x00175AA7
		public GetContextResponse(DirectoryObjectsAndLinks GetContextResult)
		{
			this.GetContextResult = GetContextResult;
		}

		// Token: 0x04004955 RID: 18773
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 0)]
		[XmlElement(IsNullable = true)]
		public DirectoryObjectsAndLinks GetContextResult;
	}
}
