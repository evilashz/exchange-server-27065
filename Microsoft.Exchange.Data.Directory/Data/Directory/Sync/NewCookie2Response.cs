using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008A7 RID: 2215
	[MessageContract(WrapperName = "NewCookie2Response", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", IsWrapped = true)]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[DebuggerStepThrough]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public class NewCookie2Response
	{
		// Token: 0x06006E0A RID: 28170 RVA: 0x00176011 File Offset: 0x00174211
		public NewCookie2Response()
		{
		}

		// Token: 0x06006E0B RID: 28171 RVA: 0x00176019 File Offset: 0x00174219
		public NewCookie2Response(byte[] NewCookie2Result)
		{
			this.NewCookie2Result = NewCookie2Result;
		}

		// Token: 0x040047A3 RID: 18339
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 0)]
		[XmlElement(DataType = "base64Binary", IsNullable = true)]
		public byte[] NewCookie2Result;
	}
}
