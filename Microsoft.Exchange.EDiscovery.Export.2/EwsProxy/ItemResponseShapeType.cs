using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002F9 RID: 761
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class ItemResponseShapeType
	{
		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x0600194E RID: 6478 RVA: 0x00028540 File Offset: 0x00026740
		// (set) Token: 0x0600194F RID: 6479 RVA: 0x00028548 File Offset: 0x00026748
		public DefaultShapeNamesType BaseShape
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

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06001950 RID: 6480 RVA: 0x00028551 File Offset: 0x00026751
		// (set) Token: 0x06001951 RID: 6481 RVA: 0x00028559 File Offset: 0x00026759
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

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06001952 RID: 6482 RVA: 0x00028562 File Offset: 0x00026762
		// (set) Token: 0x06001953 RID: 6483 RVA: 0x0002856A File Offset: 0x0002676A
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

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06001954 RID: 6484 RVA: 0x00028573 File Offset: 0x00026773
		// (set) Token: 0x06001955 RID: 6485 RVA: 0x0002857B File Offset: 0x0002677B
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

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06001956 RID: 6486 RVA: 0x00028584 File Offset: 0x00026784
		// (set) Token: 0x06001957 RID: 6487 RVA: 0x0002858C File Offset: 0x0002678C
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

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06001958 RID: 6488 RVA: 0x00028595 File Offset: 0x00026795
		// (set) Token: 0x06001959 RID: 6489 RVA: 0x0002859D File Offset: 0x0002679D
		public BodyTypeResponseType UniqueBodyType
		{
			get
			{
				return this.uniqueBodyTypeField;
			}
			set
			{
				this.uniqueBodyTypeField = value;
			}
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x0600195A RID: 6490 RVA: 0x000285A6 File Offset: 0x000267A6
		// (set) Token: 0x0600195B RID: 6491 RVA: 0x000285AE File Offset: 0x000267AE
		[XmlIgnore]
		public bool UniqueBodyTypeSpecified
		{
			get
			{
				return this.uniqueBodyTypeFieldSpecified;
			}
			set
			{
				this.uniqueBodyTypeFieldSpecified = value;
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x0600195C RID: 6492 RVA: 0x000285B7 File Offset: 0x000267B7
		// (set) Token: 0x0600195D RID: 6493 RVA: 0x000285BF File Offset: 0x000267BF
		public BodyTypeResponseType NormalizedBodyType
		{
			get
			{
				return this.normalizedBodyTypeField;
			}
			set
			{
				this.normalizedBodyTypeField = value;
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x0600195E RID: 6494 RVA: 0x000285C8 File Offset: 0x000267C8
		// (set) Token: 0x0600195F RID: 6495 RVA: 0x000285D0 File Offset: 0x000267D0
		[XmlIgnore]
		public bool NormalizedBodyTypeSpecified
		{
			get
			{
				return this.normalizedBodyTypeFieldSpecified;
			}
			set
			{
				this.normalizedBodyTypeFieldSpecified = value;
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06001960 RID: 6496 RVA: 0x000285D9 File Offset: 0x000267D9
		// (set) Token: 0x06001961 RID: 6497 RVA: 0x000285E1 File Offset: 0x000267E1
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

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06001962 RID: 6498 RVA: 0x000285EA File Offset: 0x000267EA
		// (set) Token: 0x06001963 RID: 6499 RVA: 0x000285F2 File Offset: 0x000267F2
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

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06001964 RID: 6500 RVA: 0x000285FB File Offset: 0x000267FB
		// (set) Token: 0x06001965 RID: 6501 RVA: 0x00028603 File Offset: 0x00026803
		public bool ConvertHtmlCodePageToUTF8
		{
			get
			{
				return this.convertHtmlCodePageToUTF8Field;
			}
			set
			{
				this.convertHtmlCodePageToUTF8Field = value;
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06001966 RID: 6502 RVA: 0x0002860C File Offset: 0x0002680C
		// (set) Token: 0x06001967 RID: 6503 RVA: 0x00028614 File Offset: 0x00026814
		[XmlIgnore]
		public bool ConvertHtmlCodePageToUTF8Specified
		{
			get
			{
				return this.convertHtmlCodePageToUTF8FieldSpecified;
			}
			set
			{
				this.convertHtmlCodePageToUTF8FieldSpecified = value;
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06001968 RID: 6504 RVA: 0x0002861D File Offset: 0x0002681D
		// (set) Token: 0x06001969 RID: 6505 RVA: 0x00028625 File Offset: 0x00026825
		public string InlineImageUrlTemplate
		{
			get
			{
				return this.inlineImageUrlTemplateField;
			}
			set
			{
				this.inlineImageUrlTemplateField = value;
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x0600196A RID: 6506 RVA: 0x0002862E File Offset: 0x0002682E
		// (set) Token: 0x0600196B RID: 6507 RVA: 0x00028636 File Offset: 0x00026836
		public bool BlockExternalImages
		{
			get
			{
				return this.blockExternalImagesField;
			}
			set
			{
				this.blockExternalImagesField = value;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x0600196C RID: 6508 RVA: 0x0002863F File Offset: 0x0002683F
		// (set) Token: 0x0600196D RID: 6509 RVA: 0x00028647 File Offset: 0x00026847
		[XmlIgnore]
		public bool BlockExternalImagesSpecified
		{
			get
			{
				return this.blockExternalImagesFieldSpecified;
			}
			set
			{
				this.blockExternalImagesFieldSpecified = value;
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x0600196E RID: 6510 RVA: 0x00028650 File Offset: 0x00026850
		// (set) Token: 0x0600196F RID: 6511 RVA: 0x00028658 File Offset: 0x00026858
		public bool AddBlankTargetToLinks
		{
			get
			{
				return this.addBlankTargetToLinksField;
			}
			set
			{
				this.addBlankTargetToLinksField = value;
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06001970 RID: 6512 RVA: 0x00028661 File Offset: 0x00026861
		// (set) Token: 0x06001971 RID: 6513 RVA: 0x00028669 File Offset: 0x00026869
		[XmlIgnore]
		public bool AddBlankTargetToLinksSpecified
		{
			get
			{
				return this.addBlankTargetToLinksFieldSpecified;
			}
			set
			{
				this.addBlankTargetToLinksFieldSpecified = value;
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06001972 RID: 6514 RVA: 0x00028672 File Offset: 0x00026872
		// (set) Token: 0x06001973 RID: 6515 RVA: 0x0002867A File Offset: 0x0002687A
		public int MaximumBodySize
		{
			get
			{
				return this.maximumBodySizeField;
			}
			set
			{
				this.maximumBodySizeField = value;
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06001974 RID: 6516 RVA: 0x00028683 File Offset: 0x00026883
		// (set) Token: 0x06001975 RID: 6517 RVA: 0x0002868B File Offset: 0x0002688B
		[XmlIgnore]
		public bool MaximumBodySizeSpecified
		{
			get
			{
				return this.maximumBodySizeFieldSpecified;
			}
			set
			{
				this.maximumBodySizeFieldSpecified = value;
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06001976 RID: 6518 RVA: 0x00028694 File Offset: 0x00026894
		// (set) Token: 0x06001977 RID: 6519 RVA: 0x0002869C File Offset: 0x0002689C
		[XmlArrayItem("ExtendedFieldURI", typeof(PathToExtendedFieldType), IsNullable = false)]
		[XmlArrayItem("IndexedFieldURI", typeof(PathToIndexedFieldType), IsNullable = false)]
		[XmlArrayItem("FieldURI", typeof(PathToUnindexedFieldType), IsNullable = false)]
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

		// Token: 0x0400111C RID: 4380
		private DefaultShapeNamesType baseShapeField;

		// Token: 0x0400111D RID: 4381
		private bool includeMimeContentField;

		// Token: 0x0400111E RID: 4382
		private bool includeMimeContentFieldSpecified;

		// Token: 0x0400111F RID: 4383
		private BodyTypeResponseType bodyTypeField;

		// Token: 0x04001120 RID: 4384
		private bool bodyTypeFieldSpecified;

		// Token: 0x04001121 RID: 4385
		private BodyTypeResponseType uniqueBodyTypeField;

		// Token: 0x04001122 RID: 4386
		private bool uniqueBodyTypeFieldSpecified;

		// Token: 0x04001123 RID: 4387
		private BodyTypeResponseType normalizedBodyTypeField;

		// Token: 0x04001124 RID: 4388
		private bool normalizedBodyTypeFieldSpecified;

		// Token: 0x04001125 RID: 4389
		private bool filterHtmlContentField;

		// Token: 0x04001126 RID: 4390
		private bool filterHtmlContentFieldSpecified;

		// Token: 0x04001127 RID: 4391
		private bool convertHtmlCodePageToUTF8Field;

		// Token: 0x04001128 RID: 4392
		private bool convertHtmlCodePageToUTF8FieldSpecified;

		// Token: 0x04001129 RID: 4393
		private string inlineImageUrlTemplateField;

		// Token: 0x0400112A RID: 4394
		private bool blockExternalImagesField;

		// Token: 0x0400112B RID: 4395
		private bool blockExternalImagesFieldSpecified;

		// Token: 0x0400112C RID: 4396
		private bool addBlankTargetToLinksField;

		// Token: 0x0400112D RID: 4397
		private bool addBlankTargetToLinksFieldSpecified;

		// Token: 0x0400112E RID: 4398
		private int maximumBodySizeField;

		// Token: 0x0400112F RID: 4399
		private bool maximumBodySizeFieldSpecified;

		// Token: 0x04001130 RID: 4400
		private BasePathToElementType[] additionalPropertiesField;
	}
}
