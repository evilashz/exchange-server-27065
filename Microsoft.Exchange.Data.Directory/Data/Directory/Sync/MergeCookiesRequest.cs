using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000973 RID: 2419
	[MessageContract(WrapperName = "MergeCookies", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", IsWrapped = true)]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DebuggerStepThrough]
	public class MergeCookiesRequest
	{
		// Token: 0x060070E6 RID: 28902 RVA: 0x001778FA File Offset: 0x00175AFA
		public MergeCookiesRequest()
		{
		}

		// Token: 0x060070E7 RID: 28903 RVA: 0x00177902 File Offset: 0x00175B02
		public MergeCookiesRequest(byte[] lastGetChangesCookie, byte[] lastGetContextPageToken, byte[] lastMergeCookie)
		{
			this.lastGetChangesCookie = lastGetChangesCookie;
			this.lastGetContextPageToken = lastGetContextPageToken;
			this.lastMergeCookie = lastMergeCookie;
		}

		// Token: 0x0400495F RID: 18783
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 0)]
		[XmlElement(DataType = "base64Binary", IsNullable = true)]
		public byte[] lastGetChangesCookie;

		// Token: 0x04004960 RID: 18784
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 1)]
		[XmlElement(DataType = "base64Binary", IsNullable = true)]
		public byte[] lastGetContextPageToken;

		// Token: 0x04004961 RID: 18785
		[XmlElement(DataType = "base64Binary", IsNullable = true)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 2)]
		public byte[] lastMergeCookie;
	}
}
