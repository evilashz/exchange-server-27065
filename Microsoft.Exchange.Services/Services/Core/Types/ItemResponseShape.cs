using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006C1 RID: 1729
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "ItemResponseShapeType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ItemResponseShape : ResponseShape
	{
		// Token: 0x0600352C RID: 13612 RVA: 0x000BF902 File Offset: 0x000BDB02
		public ItemResponseShape()
		{
			this.Init();
		}

		// Token: 0x0600352D RID: 13613 RVA: 0x000BF910 File Offset: 0x000BDB10
		public ItemResponseShape(ItemResponseShape source)
		{
			base.BaseShape = source.BaseShape;
			this.IncludeMimeContent = source.IncludeMimeContent;
			this.BodyType = source.bodyType;
			this.UniqueBodyType = source.UniqueBodyType;
			this.NormalizedBodyType = source.NormalizedBodyType;
			this.FilterHtmlContent = source.FilterHtmlContent;
			this.ConvertHtmlCodePageToUTF8 = source.ConvertHtmlCodePageToUTF8;
			this.InlineImageUrlTemplate = source.InlineImageUrlTemplate;
			this.InlineImageUrlOnLoadTemplate = source.InlineImageUrlOnLoadTemplate;
			this.InlineImageCustomDataTemplate = source.InlineImageCustomDataTemplate;
			this.BlockExternalImages = source.BlockExternalImages;
			this.BlockExternalImagesIfSenderUntrusted = source.BlockExternalImagesIfSenderUntrusted;
			this.AddBlankTargetToLinks = source.AddBlankTargetToLinks;
			this.CssScopeClassName = source.CssScopeClassName;
			this.ClientSupportsIrm = source.ClientSupportsIrm;
			this.MaximumBodySize = source.MaximumBodySize;
			this.InferenceEnabled = source.InferenceEnabled;
			this.ShouldUseNarrowGapForPTagHtmlToTextConversion = source.ShouldUseNarrowGapForPTagHtmlToTextConversion;
			this.MaximumRecipientsToReturn = source.MaximumRecipientsToReturn;
			this.CalculateAttachmentInlineProps = source.CalculateAttachmentInlineProps;
			this.SeparateQuotedTextFromBody = source.SeparateQuotedTextFromBody;
			this.UseSafeHtml = source.UseSafeHtml;
			if (source.AdditionalProperties != null && source.AdditionalProperties.Length > 0)
			{
				base.AdditionalProperties = new PropertyPath[source.AdditionalProperties.Length];
				Array.Copy(source.AdditionalProperties, base.AdditionalProperties, source.AdditionalProperties.Length);
			}
		}

		// Token: 0x0600352E RID: 13614 RVA: 0x000BFA6A File Offset: 0x000BDC6A
		internal ItemResponseShape(ShapeEnum baseShape, BodyResponseType bodyType, bool includeMimeContent, PropertyPath[] additionalProperties) : base(baseShape, additionalProperties)
		{
			this.convertHtmlCodePageToUTF8 = true;
			this.IncludeMimeContent = includeMimeContent;
			this.bodyType = bodyType;
		}

		// Token: 0x0600352F RID: 13615 RVA: 0x000BFA8A File Offset: 0x000BDC8A
		private void Init()
		{
			this.bodyType = BodyResponseType.Best;
			this.uniqueBodyType = null;
			this.normalizedBodyType = null;
			this.convertHtmlCodePageToUTF8 = true;
			this.CalculateAttachmentInlineProps = false;
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x06003530 RID: 13616 RVA: 0x000BFAB9 File Offset: 0x000BDCB9
		// (set) Token: 0x06003531 RID: 13617 RVA: 0x000BFAC1 File Offset: 0x000BDCC1
		[DataMember(IsRequired = false, Order = 1)]
		[DefaultValue(false)]
		[XmlElement]
		public bool IncludeMimeContent { get; set; }

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x06003532 RID: 13618 RVA: 0x000BFACA File Offset: 0x000BDCCA
		// (set) Token: 0x06003533 RID: 13619 RVA: 0x000BFAD2 File Offset: 0x000BDCD2
		[IgnoreDataMember]
		[DefaultValue(BodyResponseType.Best)]
		[XmlElement("BodyType")]
		public BodyResponseType BodyType
		{
			get
			{
				return this.bodyType;
			}
			set
			{
				this.bodyType = value;
			}
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x06003534 RID: 13620 RVA: 0x000BFADB File Offset: 0x000BDCDB
		// (set) Token: 0x06003535 RID: 13621 RVA: 0x000BFAE8 File Offset: 0x000BDCE8
		[XmlIgnore]
		[DataMember(Name = "BodyType", IsRequired = false)]
		public string BodyTypeString
		{
			get
			{
				return EnumUtilities.ToString<BodyResponseType>(this.bodyType);
			}
			set
			{
				this.bodyType = EnumUtilities.Parse<BodyResponseType>(value);
			}
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x06003536 RID: 13622 RVA: 0x000BFAF8 File Offset: 0x000BDCF8
		// (set) Token: 0x06003537 RID: 13623 RVA: 0x000BFB23 File Offset: 0x000BDD23
		[IgnoreDataMember]
		[XmlElement("UniqueBodyType")]
		public BodyResponseType UniqueBodyType
		{
			get
			{
				BodyResponseType? bodyResponseType = this.uniqueBodyType;
				if (bodyResponseType == null)
				{
					return this.BodyType;
				}
				return bodyResponseType.GetValueOrDefault();
			}
			set
			{
				this.uniqueBodyType = new BodyResponseType?(value);
			}
		}

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x06003538 RID: 13624 RVA: 0x000BFB31 File Offset: 0x000BDD31
		// (set) Token: 0x06003539 RID: 13625 RVA: 0x000BFB40 File Offset: 0x000BDD40
		[XmlIgnore]
		[DataMember(Name = "UniqueBodyType", IsRequired = false)]
		public string UniqueBodyTypeString
		{
			get
			{
				return EnumUtilities.ToString<BodyResponseType>(this.UniqueBodyType);
			}
			set
			{
				this.uniqueBodyType = ((!string.IsNullOrEmpty(value)) ? new BodyResponseType?(EnumUtilities.Parse<BodyResponseType>(value)) : null);
			}
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x0600353A RID: 13626 RVA: 0x000BFB74 File Offset: 0x000BDD74
		// (set) Token: 0x0600353B RID: 13627 RVA: 0x000BFB9F File Offset: 0x000BDD9F
		[IgnoreDataMember]
		[XmlElement("NormalizedBodyType")]
		public BodyResponseType NormalizedBodyType
		{
			get
			{
				BodyResponseType? bodyResponseType = this.normalizedBodyType;
				if (bodyResponseType == null)
				{
					return this.BodyType;
				}
				return bodyResponseType.GetValueOrDefault();
			}
			set
			{
				this.normalizedBodyType = new BodyResponseType?(value);
			}
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x0600353C RID: 13628 RVA: 0x000BFBAD File Offset: 0x000BDDAD
		// (set) Token: 0x0600353D RID: 13629 RVA: 0x000BFBBC File Offset: 0x000BDDBC
		[DataMember(Name = "NormalizedBodyType", IsRequired = false)]
		[XmlIgnore]
		public string NormalizedBodyTypeString
		{
			get
			{
				return EnumUtilities.ToString<BodyResponseType>(this.NormalizedBodyType);
			}
			set
			{
				this.normalizedBodyType = ((!string.IsNullOrEmpty(value)) ? new BodyResponseType?(EnumUtilities.Parse<BodyResponseType>(value)) : null);
			}
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x0600353E RID: 13630 RVA: 0x000BFBED File Offset: 0x000BDDED
		// (set) Token: 0x0600353F RID: 13631 RVA: 0x000BFBF5 File Offset: 0x000BDDF5
		[DefaultValue(false)]
		[DataMember(IsRequired = false)]
		[XmlElement]
		public bool FilterHtmlContent { get; set; }

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06003540 RID: 13632 RVA: 0x000BFBFE File Offset: 0x000BDDFE
		// (set) Token: 0x06003541 RID: 13633 RVA: 0x000BFC06 File Offset: 0x000BDE06
		[DefaultValue(true)]
		[DataMember(IsRequired = false)]
		[XmlElement]
		public bool ConvertHtmlCodePageToUTF8
		{
			get
			{
				return this.convertHtmlCodePageToUTF8;
			}
			set
			{
				this.convertHtmlCodePageToUTF8 = value;
			}
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x06003542 RID: 13634 RVA: 0x000BFC0F File Offset: 0x000BDE0F
		// (set) Token: 0x06003543 RID: 13635 RVA: 0x000BFC17 File Offset: 0x000BDE17
		[DataMember(IsRequired = false)]
		[XmlElement]
		public string InlineImageUrlTemplate { get; set; }

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06003544 RID: 13636 RVA: 0x000BFC20 File Offset: 0x000BDE20
		// (set) Token: 0x06003545 RID: 13637 RVA: 0x000BFC28 File Offset: 0x000BDE28
		[DataMember(IsRequired = false)]
		[XmlElement]
		public string InlineImageUrlOnLoadTemplate { get; set; }

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06003546 RID: 13638 RVA: 0x000BFC31 File Offset: 0x000BDE31
		// (set) Token: 0x06003547 RID: 13639 RVA: 0x000BFC39 File Offset: 0x000BDE39
		[XmlElement]
		[DataMember(IsRequired = false)]
		public string InlineImageCustomDataTemplate { get; set; }

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06003548 RID: 13640 RVA: 0x000BFC42 File Offset: 0x000BDE42
		// (set) Token: 0x06003549 RID: 13641 RVA: 0x000BFC4A File Offset: 0x000BDE4A
		[XmlElement]
		[DataMember(IsRequired = false)]
		public bool BlockExternalImages { get; set; }

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x0600354A RID: 13642 RVA: 0x000BFC53 File Offset: 0x000BDE53
		// (set) Token: 0x0600354B RID: 13643 RVA: 0x000BFC5B File Offset: 0x000BDE5B
		[XmlIgnore]
		[DataMember(IsRequired = false)]
		public bool BlockExternalImagesIfSenderUntrusted { get; set; }

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x0600354C RID: 13644 RVA: 0x000BFC64 File Offset: 0x000BDE64
		// (set) Token: 0x0600354D RID: 13645 RVA: 0x000BFC6C File Offset: 0x000BDE6C
		[XmlElement]
		[DataMember(IsRequired = false)]
		public bool AddBlankTargetToLinks { get; set; }

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x0600354E RID: 13646 RVA: 0x000BFC75 File Offset: 0x000BDE75
		// (set) Token: 0x0600354F RID: 13647 RVA: 0x000BFC7D File Offset: 0x000BDE7D
		[DataMember(IsRequired = false)]
		[XmlIgnore]
		public bool ClientSupportsIrm { get; set; }

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06003550 RID: 13648 RVA: 0x000BFC86 File Offset: 0x000BDE86
		// (set) Token: 0x06003551 RID: 13649 RVA: 0x000BFC8E File Offset: 0x000BDE8E
		[XmlElement]
		[DataMember(IsRequired = false)]
		public int MaximumBodySize { get; set; }

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06003552 RID: 13650 RVA: 0x000BFC97 File Offset: 0x000BDE97
		// (set) Token: 0x06003553 RID: 13651 RVA: 0x000BFC9F File Offset: 0x000BDE9F
		[XmlIgnore]
		[DataMember(IsRequired = false)]
		public bool InferenceEnabled { get; set; }

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06003554 RID: 13652 RVA: 0x000BFCA8 File Offset: 0x000BDEA8
		// (set) Token: 0x06003555 RID: 13653 RVA: 0x000BFCB0 File Offset: 0x000BDEB0
		[XmlIgnore]
		[DataMember(IsRequired = false)]
		public string ConversationShapeName { get; set; }

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06003556 RID: 13654 RVA: 0x000BFCB9 File Offset: 0x000BDEB9
		// (set) Token: 0x06003557 RID: 13655 RVA: 0x000BFCC1 File Offset: 0x000BDEC1
		[XmlIgnore]
		[DataMember(IsRequired = false)]
		public TargetFolderId ConversationFolderId { get; set; }

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x06003558 RID: 13656 RVA: 0x000BFCCA File Offset: 0x000BDECA
		// (set) Token: 0x06003559 RID: 13657 RVA: 0x000BFCD2 File Offset: 0x000BDED2
		[DataMember(IsRequired = false)]
		[XmlIgnore]
		public bool ShouldUseNarrowGapForPTagHtmlToTextConversion { get; set; }

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x0600355A RID: 13658 RVA: 0x000BFCDB File Offset: 0x000BDEDB
		// (set) Token: 0x0600355B RID: 13659 RVA: 0x000BFCE3 File Offset: 0x000BDEE3
		[DataMember(IsRequired = false)]
		[XmlIgnore]
		public int MaximumRecipientsToReturn { get; set; }

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x0600355C RID: 13660 RVA: 0x000BFCEC File Offset: 0x000BDEEC
		// (set) Token: 0x0600355D RID: 13661 RVA: 0x000BFCF4 File Offset: 0x000BDEF4
		[DataMember(IsRequired = false)]
		[XmlIgnore]
		public bool CalculateAttachmentInlineProps { get; set; }

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x0600355E RID: 13662 RVA: 0x000BFCFD File Offset: 0x000BDEFD
		// (set) Token: 0x0600355F RID: 13663 RVA: 0x000BFD05 File Offset: 0x000BDF05
		[DataMember(IsRequired = false)]
		[XmlIgnore]
		public string CssScopeClassName { get; set; }

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06003560 RID: 13664 RVA: 0x000BFD0E File Offset: 0x000BDF0E
		// (set) Token: 0x06003561 RID: 13665 RVA: 0x000BFD16 File Offset: 0x000BDF16
		[XmlIgnore]
		[DataMember(IsRequired = false)]
		public bool SeparateQuotedTextFromBody { get; set; }

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06003562 RID: 13666 RVA: 0x000BFD1F File Offset: 0x000BDF1F
		// (set) Token: 0x06003563 RID: 13667 RVA: 0x000BFD30 File Offset: 0x000BDF30
		[DataMember(IsRequired = false)]
		[XmlIgnore]
		public bool UseSafeHtml
		{
			get
			{
				return Global.SafeHtmlLoaded && this.useSafeHtml;
			}
			set
			{
				this.useSafeHtml = value;
			}
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x06003564 RID: 13668 RVA: 0x000BFD39 File Offset: 0x000BDF39
		public bool CanCreateNormalizedBodyServiceObject
		{
			get
			{
				return !this.UseSafeHtml && (string.IsNullOrEmpty(this.CssScopeClassName) || this.NormalizedBodyType == BodyResponseType.Text);
			}
		}

		// Token: 0x06003565 RID: 13669 RVA: 0x000BFD5D File Offset: 0x000BDF5D
		[OnDeserializing]
		private void Init(StreamingContext context)
		{
			this.Init();
		}

		// Token: 0x04001DD2 RID: 7634
		private BodyResponseType bodyType;

		// Token: 0x04001DD3 RID: 7635
		private BodyResponseType? uniqueBodyType;

		// Token: 0x04001DD4 RID: 7636
		private BodyResponseType? normalizedBodyType;

		// Token: 0x04001DD5 RID: 7637
		private bool convertHtmlCodePageToUTF8;

		// Token: 0x04001DD6 RID: 7638
		private bool useSafeHtml;
	}
}
