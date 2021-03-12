using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001F7 RID: 503
	[XmlInclude(typeof(AlternatePublicFolderItemIdType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class AlternatePublicFolderIdType : AlternateIdBaseType
	{
		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x00025AE3 File Offset: 0x00023CE3
		// (set) Token: 0x06001446 RID: 5190 RVA: 0x00025AEB File Offset: 0x00023CEB
		[XmlAttribute]
		public string FolderId
		{
			get
			{
				return this.folderIdField;
			}
			set
			{
				this.folderIdField = value;
			}
		}

		// Token: 0x04000E01 RID: 3585
		private string folderIdField;
	}
}
