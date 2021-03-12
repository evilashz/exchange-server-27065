using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001D6 RID: 470
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class PersonaAttributionType
	{
		// Token: 0x04000C82 RID: 3202
		public string Id;

		// Token: 0x04000C83 RID: 3203
		public ItemIdType SourceId;

		// Token: 0x04000C84 RID: 3204
		public string DisplayName;

		// Token: 0x04000C85 RID: 3205
		public bool IsWritable;

		// Token: 0x04000C86 RID: 3206
		[XmlIgnore]
		public bool IsWritableSpecified;

		// Token: 0x04000C87 RID: 3207
		public bool IsQuickContact;

		// Token: 0x04000C88 RID: 3208
		[XmlIgnore]
		public bool IsQuickContactSpecified;

		// Token: 0x04000C89 RID: 3209
		public bool IsHidden;

		// Token: 0x04000C8A RID: 3210
		[XmlIgnore]
		public bool IsHiddenSpecified;

		// Token: 0x04000C8B RID: 3211
		public FolderIdType FolderId;
	}
}
