using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002D7 RID: 727
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class AttachmentResponseShapeType
	{
		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x0600189B RID: 6299 RVA: 0x00027F5B File Offset: 0x0002615B
		// (set) Token: 0x0600189C RID: 6300 RVA: 0x00027F63 File Offset: 0x00026163
		public bool IncludeMimeContent
		{
			get
			{
				return this.includeMimeContentField;
			}
			set
			{
				this.includeMimeContentField = value;
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x0600189D RID: 6301 RVA: 0x00027F6C File Offset: 0x0002616C
		// (set) Token: 0x0600189E RID: 6302 RVA: 0x00027F74 File Offset: 0x00026174
		[XmlIgnore]
		public bool IncludeMimeContentSpecified
		{
			get
			{
				return this.includeMimeContentFieldSpecified;
			}
			set
			{
				this.includeMimeContentFieldSpecified = value;
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x0600189F RID: 6303 RVA: 0x00027F7D File Offset: 0x0002617D
		// (set) Token: 0x060018A0 RID: 6304 RVA: 0x00027F85 File Offset: 0x00026185
		public BodyTypeResponseType BodyType
		{
			get
			{
				return this.bodyTypeField;
			}
			set
			{
				this.bodyTypeField = value;
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x060018A1 RID: 6305 RVA: 0x00027F8E File Offset: 0x0002618E
		// (set) Token: 0x060018A2 RID: 6306 RVA: 0x00027F96 File Offset: 0x00026196
		[XmlIgnore]
		public bool BodyTypeSpecified
		{
			get
			{
				return this.bodyTypeFieldSpecified;
			}
			set
			{
				this.bodyTypeFieldSpecified = value;
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x060018A3 RID: 6307 RVA: 0x00027F9F File Offset: 0x0002619F
		// (set) Token: 0x060018A4 RID: 6308 RVA: 0x00027FA7 File Offset: 0x000261A7
		public bool FilterHtmlContent
		{
			get
			{
				return this.filterHtmlContentField;
			}
			set
			{
				this.filterHtmlContentField = value;
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x060018A5 RID: 6309 RVA: 0x00027FB0 File Offset: 0x000261B0
		// (set) Token: 0x060018A6 RID: 6310 RVA: 0x00027FB8 File Offset: 0x000261B8
		[XmlIgnore]
		public bool FilterHtmlContentSpecified
		{
			get
			{
				return this.filterHtmlContentFieldSpecified;
			}
			set
			{
				this.filterHtmlContentFieldSpecified = value;
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x060018A7 RID: 6311 RVA: 0x00027FC1 File Offset: 0x000261C1
		// (set) Token: 0x060018A8 RID: 6312 RVA: 0x00027FC9 File Offset: 0x000261C9
		[XmlArrayItem("FieldURI", typeof(PathToUnindexedFieldType), IsNullable = false)]
		[XmlArrayItem("IndexedFieldURI", typeof(PathToIndexedFieldType), IsNullable = false)]
		[XmlArrayItem("ExtendedFieldURI", typeof(PathToExtendedFieldType), IsNullable = false)]
		[XmlArrayItem("Path", IsNullable = false)]
		public BasePathToElementType[] AdditionalProperties
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

		// Token: 0x040010A9 RID: 4265
		private bool includeMimeContentField;

		// Token: 0x040010AA RID: 4266
		private bool includeMimeContentFieldSpecified;

		// Token: 0x040010AB RID: 4267
		private BodyTypeResponseType bodyTypeField;

		// Token: 0x040010AC RID: 4268
		private bool bodyTypeFieldSpecified;

		// Token: 0x040010AD RID: 4269
		private bool filterHtmlContentField;

		// Token: 0x040010AE RID: 4270
		private bool filterHtmlContentFieldSpecified;

		// Token: 0x040010AF RID: 4271
		private BasePathToElementType[] additionalPropertiesField;
	}
}
