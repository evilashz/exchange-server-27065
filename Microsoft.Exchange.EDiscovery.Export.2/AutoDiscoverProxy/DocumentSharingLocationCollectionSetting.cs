using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x02000088 RID: 136
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class DocumentSharingLocationCollectionSetting : UserSetting
	{
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x0001F72A File Offset: 0x0001D92A
		// (set) Token: 0x06000874 RID: 2164 RVA: 0x0001F732 File Offset: 0x0001D932
		[XmlArrayItem(IsNullable = false)]
		public DocumentSharingLocation[] DocumentSharingLocations
		{
			get
			{
				return this.documentSharingLocationsField;
			}
			set
			{
				this.documentSharingLocationsField = value;
			}
		}

		// Token: 0x04000330 RID: 816
		private DocumentSharingLocation[] documentSharingLocationsField;
	}
}
