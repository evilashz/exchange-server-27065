using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008A4 RID: 2212
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DebuggerStepThrough]
	[MessageContract(WrapperName = "NewCookie", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", IsWrapped = true)]
	public class NewCookieRequest
	{
		// Token: 0x06006E04 RID: 28164 RVA: 0x00175F90 File Offset: 0x00174190
		public NewCookieRequest()
		{
		}

		// Token: 0x06006E05 RID: 28165 RVA: 0x00175F98 File Offset: 0x00174198
		public NewCookieRequest(string serviceInstance, SyncOptions options, string[] alwaysReturnProperties)
		{
			this.serviceInstance = serviceInstance;
			this.options = options;
			this.alwaysReturnProperties = alwaysReturnProperties;
		}

		// Token: 0x04004798 RID: 18328
		[XmlElement(IsNullable = true)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 0)]
		public string serviceInstance;

		// Token: 0x04004799 RID: 18329
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 1)]
		public SyncOptions options;

		// Token: 0x0400479A RID: 18330
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 2)]
		[XmlArrayItem(Namespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays")]
		[XmlArray(IsNullable = true)]
		public string[] alwaysReturnProperties;
	}
}
