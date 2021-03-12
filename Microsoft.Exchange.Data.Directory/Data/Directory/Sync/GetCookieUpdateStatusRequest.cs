using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200097A RID: 2426
	[DebuggerStepThrough]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[MessageContract(WrapperName = "GetCookieUpdateStatus", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", IsWrapped = true)]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public class GetCookieUpdateStatusRequest
	{
		// Token: 0x060070F2 RID: 28914 RVA: 0x001779C7 File Offset: 0x00175BC7
		public GetCookieUpdateStatusRequest()
		{
		}

		// Token: 0x060070F3 RID: 28915 RVA: 0x001779CF File Offset: 0x00175BCF
		public GetCookieUpdateStatusRequest(byte[] getChangesCookie)
		{
			this.getChangesCookie = getChangesCookie;
		}

		// Token: 0x04004977 RID: 18807
		[XmlElement(DataType = "base64Binary", IsNullable = true)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 0)]
		public byte[] getChangesCookie;
	}
}
