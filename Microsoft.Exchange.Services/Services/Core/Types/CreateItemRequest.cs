using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000407 RID: 1031
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("CreateItemType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class CreateItemRequest : BaseRequest
	{
		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06001D48 RID: 7496 RVA: 0x0009F166 File Offset: 0x0009D366
		// (set) Token: 0x06001D49 RID: 7497 RVA: 0x0009F16E File Offset: 0x0009D36E
		[DataMember(Name = "SavedItemFolderId", IsRequired = false, Order = 1)]
		[XmlElement("SavedItemFolderId")]
		public TargetFolderId SavedItemFolderId { get; set; }

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06001D4A RID: 7498 RVA: 0x0009F177 File Offset: 0x0009D377
		// (set) Token: 0x06001D4B RID: 7499 RVA: 0x0009F17F File Offset: 0x0009D37F
		[IgnoreDataMember]
		[XmlElement("Items", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public NonEmptyArrayOfAllItemsType Items { get; set; }

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06001D4C RID: 7500 RVA: 0x0009F188 File Offset: 0x0009D388
		// (set) Token: 0x06001D4D RID: 7501 RVA: 0x0009F1A0 File Offset: 0x0009D3A0
		[XmlIgnore]
		[DataMember(Name = "Items", IsRequired = true, Order = 2)]
		public ItemType[] ItemsArray
		{
			get
			{
				if (this.Items == null)
				{
					return null;
				}
				return this.Items.Items;
			}
			set
			{
				this.Items = new NonEmptyArrayOfAllItemsType
				{
					Items = value
				};
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06001D4E RID: 7502 RVA: 0x0009F1C1 File Offset: 0x0009D3C1
		// (set) Token: 0x06001D4F RID: 7503 RVA: 0x0009F1C9 File Offset: 0x0009D3C9
		[DataMember(IsRequired = false, Order = 3)]
		[XmlAttribute("MessageDisposition", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string MessageDisposition { get; set; }

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06001D50 RID: 7504 RVA: 0x0009F1D2 File Offset: 0x0009D3D2
		// (set) Token: 0x06001D51 RID: 7505 RVA: 0x0009F1DA File Offset: 0x0009D3DA
		[DataMember(IsRequired = false, Order = 4)]
		[XmlAttribute("SendMeetingInvitations", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string SendMeetingInvitations { get; set; }

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001D52 RID: 7506 RVA: 0x0009F1E3 File Offset: 0x0009D3E3
		// (set) Token: 0x06001D53 RID: 7507 RVA: 0x0009F1EB File Offset: 0x0009D3EB
		[XmlIgnore]
		[DataMember(Name = "ItemShape", IsRequired = false, Order = 5)]
		public ItemResponseShape ItemShape { get; set; }

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001D54 RID: 7508 RVA: 0x0009F1F4 File Offset: 0x0009D3F4
		// (set) Token: 0x06001D55 RID: 7509 RVA: 0x0009F1FC File Offset: 0x0009D3FC
		[DataMember(Name = "ShapeName", IsRequired = false, Order = 6)]
		[XmlIgnore]
		public string ShapeName { get; set; }

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001D56 RID: 7510 RVA: 0x0009F205 File Offset: 0x0009D405
		// (set) Token: 0x06001D57 RID: 7511 RVA: 0x0009F20D File Offset: 0x0009D40D
		[DataMember(Name = "ComplianceId", IsRequired = false, Order = 7)]
		[XmlIgnore]
		public string ComplianceId { get; set; }

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06001D58 RID: 7512 RVA: 0x0009F216 File Offset: 0x0009D416
		// (set) Token: 0x06001D59 RID: 7513 RVA: 0x0009F21E File Offset: 0x0009D41E
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, Order = 8)]
		public bool GenerateResponseMessageOnFailure { get; set; }

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06001D5A RID: 7514 RVA: 0x0009F227 File Offset: 0x0009D427
		// (set) Token: 0x06001D5B RID: 7515 RVA: 0x0009F22F File Offset: 0x0009D42F
		[DataMember(EmitDefaultValue = false, Order = 9)]
		[XmlIgnore]
		public bool FailResponseOnImportantUpdate { get; set; }

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06001D5C RID: 7516 RVA: 0x0009F238 File Offset: 0x0009D438
		// (set) Token: 0x06001D5D RID: 7517 RVA: 0x0009F240 File Offset: 0x0009D440
		[DataMember(EmitDefaultValue = false, Order = 10)]
		[XmlIgnore]
		public bool GenerateMeetingResponseWithOldLocationIfChanged { get; set; }

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06001D5E RID: 7518 RVA: 0x0009F249 File Offset: 0x0009D449
		// (set) Token: 0x06001D5F RID: 7519 RVA: 0x0009F251 File Offset: 0x0009D451
		[DataMember(Name = "ClientSupportsIrm", IsRequired = false, Order = 11)]
		[XmlIgnore]
		public bool ClientSupportsIrm { get; set; }

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001D60 RID: 7520 RVA: 0x0009F25A File Offset: 0x0009D45A
		// (set) Token: 0x06001D61 RID: 7521 RVA: 0x0009F262 File Offset: 0x0009D462
		[DataMember(Name = "SendOnNotFoundError", IsRequired = false)]
		[XmlIgnore]
		public bool SendOnNotFoundError { get; set; }

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001D62 RID: 7522 RVA: 0x0009F26B File Offset: 0x0009D46B
		// (set) Token: 0x06001D63 RID: 7523 RVA: 0x0009F273 File Offset: 0x0009D473
		[XmlIgnore]
		[DataMember(Name = "TimeFormat", IsRequired = false, Order = 12)]
		public string TimeFormat { get; set; }

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001D64 RID: 7524 RVA: 0x0009F27C File Offset: 0x0009D47C
		// (set) Token: 0x06001D65 RID: 7525 RVA: 0x0009F284 File Offset: 0x0009D484
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, Order = 13)]
		public bool ShouldSuppressReadReceipt { get; set; }

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001D66 RID: 7526 RVA: 0x0009F28D File Offset: 0x0009D48D
		// (set) Token: 0x06001D67 RID: 7527 RVA: 0x0009F295 File Offset: 0x0009D495
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, Order = 14)]
		public string SubjectPrefix { get; set; }

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001D68 RID: 7528 RVA: 0x0009F29E File Offset: 0x0009D49E
		// (set) Token: 0x06001D69 RID: 7529 RVA: 0x0009F2A6 File Offset: 0x0009D4A6
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, Order = 15)]
		public bool IsNonDraft { get; set; }

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06001D6A RID: 7530 RVA: 0x0009F2AF File Offset: 0x0009D4AF
		// (set) Token: 0x06001D6B RID: 7531 RVA: 0x0009F2B7 File Offset: 0x0009D4B7
		[DataMember(IsRequired = false, Order = 16)]
		[XmlAttribute("ComposeOperation", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string ComposeOperation { get; set; }

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06001D6C RID: 7532 RVA: 0x0009F2C0 File Offset: 0x0009D4C0
		// (set) Token: 0x06001D6D RID: 7533 RVA: 0x0009F2D2 File Offset: 0x0009D4D2
		[DataMember(Name = "OutboundCharset", IsRequired = false, Order = 17)]
		[XmlIgnore]
		public string OutboundCharset
		{
			get
			{
				return this.outboundCharset.ToString();
			}
			set
			{
				this.outboundCharset = (OutboundCharsetOptions)Enum.Parse(typeof(OutboundCharsetOptions), value);
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001D6E RID: 7534 RVA: 0x0009F2EF File Offset: 0x0009D4EF
		// (set) Token: 0x06001D6F RID: 7535 RVA: 0x0009F2F7 File Offset: 0x0009D4F7
		[XmlIgnore]
		[DataMember(Name = "UseGB18030", IsRequired = false, Order = 18)]
		public bool UseGB18030 { get; set; }

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06001D70 RID: 7536 RVA: 0x0009F300 File Offset: 0x0009D500
		// (set) Token: 0x06001D71 RID: 7537 RVA: 0x0009F308 File Offset: 0x0009D508
		[XmlIgnore]
		[DataMember(Name = "UseISO885915", IsRequired = false, Order = 19)]
		public bool UseISO885915 { get; set; }

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06001D72 RID: 7538 RVA: 0x0009F311 File Offset: 0x0009D511
		// (set) Token: 0x06001D73 RID: 7539 RVA: 0x0009F319 File Offset: 0x0009D519
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, Order = 20)]
		public bool IsDraftEvent { get; set; }

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06001D74 RID: 7540 RVA: 0x0009F322 File Offset: 0x0009D522
		public OutboundCharsetOptions OutboundCharsetOptions
		{
			get
			{
				return this.outboundCharset;
			}
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x0009F32A File Offset: 0x0009D52A
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new CreateItem(callContext, this);
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x0009F333 File Offset: 0x0009D533
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return this.GetServerInfoForStep(callContext, 0);
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x0009F340 File Offset: 0x0009D540
		internal BaseServerIdInfo GetServerInfoForStep(CallContext callContext, int step)
		{
			BaseServerIdInfo baseServerIdInfo = null;
			if (this.SavedItemFolderId == null)
			{
				ItemType itemType = this.Items.Items[step];
				ResponseObjectCoreType responseObjectCoreType = itemType as ResponseObjectCoreType;
				if (responseObjectCoreType != null)
				{
					BaseItemId referenceItemId = responseObjectCoreType.ReferenceItemId;
					if (referenceItemId != null)
					{
						baseServerIdInfo = BaseRequest.GetServerInfoForItemId(callContext, referenceItemId);
					}
				}
				if (baseServerIdInfo == null)
				{
					baseServerIdInfo = callContext.GetServerInfoForEffectiveCaller();
				}
			}
			else
			{
				baseServerIdInfo = BaseRequest.GetServerInfoForFolderId(callContext, this.SavedItemFolderId.BaseFolderId);
			}
			return baseServerIdInfo;
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x0009F3A0 File Offset: 0x0009D5A0
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			BaseServerIdInfo baseServerIdInfo = (this.SavedItemFolderId == null || this.SavedItemFolderId.BaseFolderId == null) ? null : BaseRequest.GetServerInfoForFolderId(callContext, this.SavedItemFolderId.BaseFolderId);
			BaseServerIdInfo serverInfoForStep = this.GetServerInfoForStep(callContext, taskStep);
			return BaseRequest.ServerInfosToResourceKeys(true, new BaseServerIdInfo[]
			{
				baseServerIdInfo,
				serverInfoForStep
			});
		}

		// Token: 0x0400131B RID: 4891
		internal const string ElementName = "CreateItem";

		// Token: 0x0400131C RID: 4892
		internal const string SavedItemFolderIdElementName = "SavedItemFolderId";

		// Token: 0x0400131D RID: 4893
		internal const string ItemsElementName = "Items";

		// Token: 0x0400131E RID: 4894
		private OutboundCharsetOptions outboundCharset;
	}
}
