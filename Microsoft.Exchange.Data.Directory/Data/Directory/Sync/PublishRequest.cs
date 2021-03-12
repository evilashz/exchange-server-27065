using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200096C RID: 2412
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[MessageContract(WrapperName = "Publish", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", IsWrapped = true)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public class PublishRequest
	{
		// Token: 0x060070DA RID: 28890 RVA: 0x0017784C File Offset: 0x00175A4C
		public PublishRequest()
		{
		}

		// Token: 0x060070DB RID: 28891 RVA: 0x00177854 File Offset: 0x00175A54
		public PublishRequest(ServicePublication[] publications)
		{
			this.publications = publications;
		}

		// Token: 0x04004950 RID: 18768
		[XmlArrayItem(IsNullable = false)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 0)]
		[XmlArray(IsNullable = true)]
		public ServicePublication[] publications;
	}
}
