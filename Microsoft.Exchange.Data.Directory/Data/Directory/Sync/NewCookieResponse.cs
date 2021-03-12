using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008A5 RID: 2213
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName = "NewCookieResponse", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", IsWrapped = true)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DebuggerStepThrough]
	public class NewCookieResponse
	{
		// Token: 0x06006E06 RID: 28166 RVA: 0x00175FB5 File Offset: 0x001741B5
		public NewCookieResponse()
		{
		}

		// Token: 0x06006E07 RID: 28167 RVA: 0x00175FBD File Offset: 0x001741BD
		public NewCookieResponse(byte[] NewCookieResult)
		{
			this.NewCookieResult = NewCookieResult;
		}

		// Token: 0x0400479B RID: 18331
		[XmlElement(DataType = "base64Binary", IsNullable = true)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 0)]
		public byte[] NewCookieResult;
	}
}
