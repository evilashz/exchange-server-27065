using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000971 RID: 2417
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(WrapperName = "GetDirectoryObjects", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", IsWrapped = true)]
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public class GetDirectoryObjectsRequest
	{
		// Token: 0x060070E2 RID: 28898 RVA: 0x001778B6 File Offset: 0x00175AB6
		public GetDirectoryObjectsRequest()
		{
		}

		// Token: 0x060070E3 RID: 28899 RVA: 0x001778BE File Offset: 0x00175ABE
		public GetDirectoryObjectsRequest(byte[] lastGetChangesCookieOrGetContextPageToken, DirectoryObjectIdentity[] objectIdentities, GetDirectoryObjectsOptions? options, byte[] lastPageToken)
		{
			this.lastGetChangesCookieOrGetContextPageToken = lastGetChangesCookieOrGetContextPageToken;
			this.objectIdentities = objectIdentities;
			this.options = options;
			this.lastPageToken = lastPageToken;
		}

		// Token: 0x0400495A RID: 18778
		[XmlElement(DataType = "base64Binary", IsNullable = true)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 0)]
		public byte[] lastGetChangesCookieOrGetContextPageToken;

		// Token: 0x0400495B RID: 18779
		[XmlArrayItem(IsNullable = false)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 1)]
		[XmlArray(IsNullable = true)]
		public DirectoryObjectIdentity[] objectIdentities;

		// Token: 0x0400495C RID: 18780
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 2)]
		[XmlElement(IsNullable = true)]
		public GetDirectoryObjectsOptions? options;

		// Token: 0x0400495D RID: 18781
		[XmlElement(DataType = "base64Binary", IsNullable = true)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 3)]
		public byte[] lastPageToken;
	}
}
