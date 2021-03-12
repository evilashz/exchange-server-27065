using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000969 RID: 2409
	[MessageContract(WrapperName = "GetChanges", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", IsWrapped = true)]
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public class GetChangesRequest
	{
		// Token: 0x060070D6 RID: 28886 RVA: 0x0017781E File Offset: 0x00175A1E
		public GetChangesRequest()
		{
		}

		// Token: 0x060070D7 RID: 28887 RVA: 0x00177826 File Offset: 0x00175A26
		public GetChangesRequest(byte[] lastCookie)
		{
			this.lastCookie = lastCookie;
		}

		// Token: 0x04004944 RID: 18756
		[XmlElement(DataType = "base64Binary", IsNullable = true)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 0)]
		public byte[] lastCookie;
	}
}
