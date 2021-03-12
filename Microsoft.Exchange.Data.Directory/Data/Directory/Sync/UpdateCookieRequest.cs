using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000978 RID: 2424
	[DebuggerStepThrough]
	[MessageContract(WrapperName = "UpdateCookie", WrapperNamespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", IsWrapped = true)]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public class UpdateCookieRequest
	{
		// Token: 0x060070EE RID: 28910 RVA: 0x0017796B File Offset: 0x00175B6B
		public UpdateCookieRequest()
		{
		}

		// Token: 0x060070EF RID: 28911 RVA: 0x00177973 File Offset: 0x00175B73
		public UpdateCookieRequest(byte[] getChangesCookie, int? schemaRevision, SyncOptions? options, string[] objectClassesOfInterest, string[] propertiesOfInterest, string[] linkClassesOfInterest, string[] alwaysReturnProperties)
		{
			this.getChangesCookie = getChangesCookie;
			this.schemaRevision = schemaRevision;
			this.options = options;
			this.objectClassesOfInterest = objectClassesOfInterest;
			this.propertiesOfInterest = propertiesOfInterest;
			this.linkClassesOfInterest = linkClassesOfInterest;
			this.alwaysReturnProperties = alwaysReturnProperties;
		}

		// Token: 0x0400496F RID: 18799
		[XmlElement(DataType = "base64Binary", IsNullable = true)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 0)]
		public byte[] getChangesCookie;

		// Token: 0x04004970 RID: 18800
		[XmlElement(IsNullable = true)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 1)]
		public int? schemaRevision;

		// Token: 0x04004971 RID: 18801
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 2)]
		[XmlElement(IsNullable = true)]
		public SyncOptions? options;

		// Token: 0x04004972 RID: 18802
		[XmlArray(IsNullable = true)]
		[XmlArrayItem(Namespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays")]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 3)]
		public string[] objectClassesOfInterest;

		// Token: 0x04004973 RID: 18803
		[XmlArray(IsNullable = true)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 4)]
		[XmlArrayItem(Namespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays")]
		public string[] propertiesOfInterest;

		// Token: 0x04004974 RID: 18804
		[XmlArrayItem(Namespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays")]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 5)]
		[XmlArray(IsNullable = true)]
		public string[] linkClassesOfInterest;

		// Token: 0x04004975 RID: 18805
		[XmlArrayItem(Namespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays")]
		[XmlArray(IsNullable = true)]
		[MessageBodyMember(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", Order = 6)]
		public string[] alwaysReturnProperties;
	}
}
