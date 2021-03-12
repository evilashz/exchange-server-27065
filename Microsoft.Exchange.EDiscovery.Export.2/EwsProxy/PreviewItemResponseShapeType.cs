using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002C3 RID: 707
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class PreviewItemResponseShapeType
	{
		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06001825 RID: 6181 RVA: 0x00027B78 File Offset: 0x00025D78
		// (set) Token: 0x06001826 RID: 6182 RVA: 0x00027B80 File Offset: 0x00025D80
		public PreviewItemBaseShapeType BaseShape
		{
			get
			{
				return this.baseShapeField;
			}
			set
			{
				this.baseShapeField = value;
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06001827 RID: 6183 RVA: 0x00027B89 File Offset: 0x00025D89
		// (set) Token: 0x06001828 RID: 6184 RVA: 0x00027B91 File Offset: 0x00025D91
		[XmlArrayItem("ExtendedFieldURI", IsNullable = false)]
		public PathToExtendedFieldType[] AdditionalProperties
		{
			get
			{
				return this.additionalPropertiesField;
			}
			set
			{
				this.additionalPropertiesField = value;
			}
		}

		// Token: 0x04001062 RID: 4194
		private PreviewItemBaseShapeType baseShapeField;

		// Token: 0x04001063 RID: 4195
		private PathToExtendedFieldType[] additionalPropertiesField;
	}
}
