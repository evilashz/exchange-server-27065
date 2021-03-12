using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200096E RID: 2414
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[DebuggerStepThrough]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(WrapperName = "GetContext", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", IsWrapped = true)]
	public class GetContextRequest
	{
		// Token: 0x060070DE RID: 28894 RVA: 0x0017787A File Offset: 0x00175A7A
		public GetContextRequest()
		{
		}

		// Token: 0x060070DF RID: 28895 RVA: 0x00177882 File Offset: 0x00175A82
		public GetContextRequest(byte[] lastCookie, string contextId, byte[] lastPageToken)
		{
			this.lastCookie = lastCookie;
			this.contextId = contextId;
			this.lastPageToken = lastPageToken;
		}

		// Token: 0x04004952 RID: 18770
		[XmlElement(DataType = "base64Binary", IsNullable = true)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 0)]
		public byte[] lastCookie;

		// Token: 0x04004953 RID: 18771
		[XmlElement(IsNullable = true)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 1)]
		public string contextId;

		// Token: 0x04004954 RID: 18772
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 2)]
		[XmlElement(DataType = "base64Binary", IsNullable = true)]
		public byte[] lastPageToken;
	}
}
