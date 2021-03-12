using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000979 RID: 2425
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName = "UpdateCookieResponse", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", IsWrapped = true)]
	[DebuggerStepThrough]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public class UpdateCookieResponse
	{
		// Token: 0x060070F0 RID: 28912 RVA: 0x001779B0 File Offset: 0x00175BB0
		public UpdateCookieResponse()
		{
		}

		// Token: 0x060070F1 RID: 28913 RVA: 0x001779B8 File Offset: 0x00175BB8
		public UpdateCookieResponse(byte[] UpdateCookieResult)
		{
			this.UpdateCookieResult = UpdateCookieResult;
		}

		// Token: 0x04004976 RID: 18806
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 0)]
		[XmlElement(DataType = "base64Binary", IsNullable = true)]
		public byte[] UpdateCookieResult;
	}
}
