using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001EF RID: 495
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ClientExtensionType
	{
		// Token: 0x04000CC4 RID: 3268
		[XmlArrayItem("String", IsNullable = false)]
		public string[] SpecificUsers;

		// Token: 0x04000CC5 RID: 3269
		[XmlElement(DataType = "base64Binary")]
		public byte[] Manifest;

		// Token: 0x04000CC6 RID: 3270
		[XmlAttribute]
		public bool IsAvailable;

		// Token: 0x04000CC7 RID: 3271
		[XmlIgnore]
		public bool IsAvailableSpecified;

		// Token: 0x04000CC8 RID: 3272
		[XmlAttribute]
		public bool IsMandatory;

		// Token: 0x04000CC9 RID: 3273
		[XmlIgnore]
		public bool IsMandatorySpecified;

		// Token: 0x04000CCA RID: 3274
		[XmlAttribute]
		public bool IsEnabledByDefault;

		// Token: 0x04000CCB RID: 3275
		[XmlIgnore]
		public bool IsEnabledByDefaultSpecified;

		// Token: 0x04000CCC RID: 3276
		[XmlAttribute]
		public ClientExtensionProvidedToType ProvidedTo;

		// Token: 0x04000CCD RID: 3277
		[XmlIgnore]
		public bool ProvidedToSpecified;

		// Token: 0x04000CCE RID: 3278
		[XmlAttribute]
		public ClientExtensionTypeType Type;

		// Token: 0x04000CCF RID: 3279
		[XmlIgnore]
		public bool TypeSpecified;

		// Token: 0x04000CD0 RID: 3280
		[XmlAttribute]
		public ClientExtensionScopeType Scope;

		// Token: 0x04000CD1 RID: 3281
		[XmlIgnore]
		public bool ScopeSpecified;

		// Token: 0x04000CD2 RID: 3282
		[XmlAttribute]
		public string MarketplaceAssetId;

		// Token: 0x04000CD3 RID: 3283
		[XmlAttribute]
		public string MarketplaceContentMarket;

		// Token: 0x04000CD4 RID: 3284
		[XmlAttribute]
		public string AppStatus;

		// Token: 0x04000CD5 RID: 3285
		[XmlAttribute]
		public string Etoken;
	}
}
