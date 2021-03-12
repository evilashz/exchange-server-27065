using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008A6 RID: 2214
	[MessageContract(WrapperName = "NewCookie2", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", IsWrapped = true)]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[DebuggerStepThrough]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public class NewCookie2Request
	{
		// Token: 0x06006E08 RID: 28168 RVA: 0x00175FCC File Offset: 0x001741CC
		public NewCookie2Request()
		{
		}

		// Token: 0x06006E09 RID: 28169 RVA: 0x00175FD4 File Offset: 0x001741D4
		public NewCookie2Request(int schemaRevision, string serviceInstance, SyncOptions options, string[] objectClassesOfInterest, string[] propertiesOfInterest, string[] linkClassesOfInterest, string[] alwaysReturnProperties)
		{
			this.schemaRevision = schemaRevision;
			this.serviceInstance = serviceInstance;
			this.options = options;
			this.objectClassesOfInterest = objectClassesOfInterest;
			this.propertiesOfInterest = propertiesOfInterest;
			this.linkClassesOfInterest = linkClassesOfInterest;
			this.alwaysReturnProperties = alwaysReturnProperties;
		}

		// Token: 0x0400479C RID: 18332
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 0)]
		public int schemaRevision;

		// Token: 0x0400479D RID: 18333
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 1)]
		[XmlElement(IsNullable = true)]
		public string serviceInstance;

		// Token: 0x0400479E RID: 18334
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 2)]
		public SyncOptions options;

		// Token: 0x0400479F RID: 18335
		[XmlArray(IsNullable = true)]
		[XmlArrayItem(Namespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays")]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 3)]
		public string[] objectClassesOfInterest;

		// Token: 0x040047A0 RID: 18336
		[XmlArray(IsNullable = true)]
		[XmlArrayItem(Namespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays")]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 4)]
		public string[] propertiesOfInterest;

		// Token: 0x040047A1 RID: 18337
		[XmlArrayItem(Namespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays")]
		[XmlArray(IsNullable = true)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 5)]
		public string[] linkClassesOfInterest;

		// Token: 0x040047A2 RID: 18338
		[XmlArray(IsNullable = true)]
		[XmlArrayItem(Namespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays")]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 6)]
		public string[] alwaysReturnProperties;
	}
}
