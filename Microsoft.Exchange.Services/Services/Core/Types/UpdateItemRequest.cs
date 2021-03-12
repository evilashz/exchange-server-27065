using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000499 RID: 1177
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("UpdateItemType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class UpdateItemRequest : BaseRequest
	{
		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x0600232C RID: 9004 RVA: 0x000A39CE File Offset: 0x000A1BCE
		// (set) Token: 0x0600232D RID: 9005 RVA: 0x000A39D6 File Offset: 0x000A1BD6
		[DataMember(Name = "SavedItemFolderId", IsRequired = false)]
		[XmlElement("SavedItemFolderId")]
		public TargetFolderId SavedItemFolderId { get; set; }

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x0600232E RID: 9006 RVA: 0x000A39DF File Offset: 0x000A1BDF
		// (set) Token: 0x0600232F RID: 9007 RVA: 0x000A39E7 File Offset: 0x000A1BE7
		[XmlArrayItem("ItemChange", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(ItemChange))]
		[DataMember(Name = "ItemChanges", IsRequired = true)]
		[XmlArray(ElementName = "ItemChanges", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public ItemChange[] ItemChanges { get; set; }

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06002330 RID: 9008 RVA: 0x000A39F0 File Offset: 0x000A1BF0
		// (set) Token: 0x06002331 RID: 9009 RVA: 0x000A39F8 File Offset: 0x000A1BF8
		[XmlAttribute(AttributeName = "ConflictResolution")]
		[IgnoreDataMember]
		public ConflictResolutionType ConflictResolution { get; set; }

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06002332 RID: 9010 RVA: 0x000A3A01 File Offset: 0x000A1C01
		// (set) Token: 0x06002333 RID: 9011 RVA: 0x000A3A0E File Offset: 0x000A1C0E
		[DataMember(Name = "ConflictResolution", IsRequired = true)]
		[XmlIgnore]
		public string ConflictResolutionString
		{
			get
			{
				return EnumUtilities.ToString<ConflictResolutionType>(this.ConflictResolution);
			}
			set
			{
				this.ConflictResolution = EnumUtilities.Parse<ConflictResolutionType>(value);
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06002334 RID: 9012 RVA: 0x000A3A1C File Offset: 0x000A1C1C
		// (set) Token: 0x06002335 RID: 9013 RVA: 0x000A3A24 File Offset: 0x000A1C24
		[DataMember(Name = "MessageDisposition", IsRequired = false)]
		[XmlAttribute("MessageDisposition", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string MessageDisposition { get; set; }

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06002336 RID: 9014 RVA: 0x000A3A2D File Offset: 0x000A1C2D
		// (set) Token: 0x06002337 RID: 9015 RVA: 0x000A3A35 File Offset: 0x000A1C35
		[DataMember(Name = "ItemShape", IsRequired = false)]
		[XmlIgnore]
		public ItemResponseShape ItemShape { get; set; }

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06002338 RID: 9016 RVA: 0x000A3A3E File Offset: 0x000A1C3E
		// (set) Token: 0x06002339 RID: 9017 RVA: 0x000A3A46 File Offset: 0x000A1C46
		[XmlIgnore]
		[DataMember(Name = "ShapeName", IsRequired = false)]
		public string ShapeName { get; set; }

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x0600233A RID: 9018 RVA: 0x000A3A4F File Offset: 0x000A1C4F
		// (set) Token: 0x0600233B RID: 9019 RVA: 0x000A3A57 File Offset: 0x000A1C57
		[DataMember(Name = "SendCalendarInvitationsOrCancellations", IsRequired = false)]
		[XmlAttribute("SendMeetingInvitationsOrCancellations", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string SendCalendarInvitationsOrCancellations { get; set; }

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x0600233C RID: 9020 RVA: 0x000A3A60 File Offset: 0x000A1C60
		// (set) Token: 0x0600233D RID: 9021 RVA: 0x000A3A68 File Offset: 0x000A1C68
		[XmlIgnore]
		[DataMember(Name = "ComplianceId", IsRequired = false)]
		public string ComplianceId { get; set; }

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x0600233E RID: 9022 RVA: 0x000A3A71 File Offset: 0x000A1C71
		// (set) Token: 0x0600233F RID: 9023 RVA: 0x000A3A79 File Offset: 0x000A1C79
		[DataMember(Name = "ClientSupportsIrm", IsRequired = false)]
		[XmlIgnore]
		public bool ClientSupportsIrm { get; set; }

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06002340 RID: 9024 RVA: 0x000A3A82 File Offset: 0x000A1C82
		// (set) Token: 0x06002341 RID: 9025 RVA: 0x000A3A8A File Offset: 0x000A1C8A
		[XmlIgnore]
		[DataMember(Name = "PromoteInlineAttachments", IsRequired = false)]
		public bool PromoteInlineAttachments { get; set; }

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06002342 RID: 9026 RVA: 0x000A3A93 File Offset: 0x000A1C93
		// (set) Token: 0x06002343 RID: 9027 RVA: 0x000A3A9B File Offset: 0x000A1C9B
		[DataMember(Name = "MarkRefAttachAsInline", IsRequired = false)]
		[XmlIgnore]
		public bool MarkRefAttachAsInline { get; set; }

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06002344 RID: 9028 RVA: 0x000A3AA4 File Offset: 0x000A1CA4
		// (set) Token: 0x06002345 RID: 9029 RVA: 0x000A3AAC File Offset: 0x000A1CAC
		[DataMember(Name = "SendOnNotFoundError", IsRequired = false)]
		[XmlIgnore]
		public bool SendOnNotFoundError { get; set; }

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06002346 RID: 9030 RVA: 0x000A3AB5 File Offset: 0x000A1CB5
		// (set) Token: 0x06002347 RID: 9031 RVA: 0x000A3ABD File Offset: 0x000A1CBD
		[XmlAttribute(AttributeName = "SuppressReadReceipts")]
		[DataMember(Name = "SuppressReadReceipts", IsRequired = false)]
		public bool SuppressReadReceipts { get; set; }

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06002348 RID: 9032 RVA: 0x000A3AC6 File Offset: 0x000A1CC6
		// (set) Token: 0x06002349 RID: 9033 RVA: 0x000A3ACE File Offset: 0x000A1CCE
		[DataMember(Name = "ComposeOperation", IsRequired = false)]
		[XmlAttribute("ComposeOperation", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string ComposeOperation { get; set; }

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x0600234A RID: 9034 RVA: 0x000A3AD7 File Offset: 0x000A1CD7
		// (set) Token: 0x0600234B RID: 9035 RVA: 0x000A3AE9 File Offset: 0x000A1CE9
		[XmlIgnore]
		[DataMember(Name = "OutboundCharset", IsRequired = false)]
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

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x0600234C RID: 9036 RVA: 0x000A3B06 File Offset: 0x000A1D06
		// (set) Token: 0x0600234D RID: 9037 RVA: 0x000A3B0E File Offset: 0x000A1D0E
		[XmlIgnore]
		[DataMember(Name = "UseGB18030", IsRequired = false)]
		public bool UseGB18030 { get; set; }

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x0600234E RID: 9038 RVA: 0x000A3B17 File Offset: 0x000A1D17
		// (set) Token: 0x0600234F RID: 9039 RVA: 0x000A3B1F File Offset: 0x000A1D1F
		[DataMember(Name = "UseISO885915", IsRequired = false)]
		[XmlIgnore]
		public bool UseISO885915 { get; set; }

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06002350 RID: 9040 RVA: 0x000A3B28 File Offset: 0x000A1D28
		public OutboundCharsetOptions OutboundCharsetOptions
		{
			get
			{
				return this.outboundCharset;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06002351 RID: 9041 RVA: 0x000A3B30 File Offset: 0x000A1D30
		// (set) Token: 0x06002352 RID: 9042 RVA: 0x000A3B38 File Offset: 0x000A1D38
		[DataMember(Name = "InternetMessageId", IsRequired = false)]
		[XmlIgnore]
		public string InternetMessageId { get; set; }

		// Token: 0x06002353 RID: 9043 RVA: 0x000A3B44 File Offset: 0x000A1D44
		protected override List<ServiceObjectId> GetAllIds()
		{
			List<ServiceObjectId> list = new List<ServiceObjectId>();
			foreach (ItemChange itemChange in this.ItemChanges)
			{
				list.Add(itemChange.ItemId);
			}
			return list;
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06002354 RID: 9044 RVA: 0x000A3B7D File Offset: 0x000A1D7D
		// (set) Token: 0x06002355 RID: 9045 RVA: 0x000A3B85 File Offset: 0x000A1D85
		[IgnoreDataMember]
		[XmlIgnore]
		internal List<KeyValuePair<ItemChange, StoreId>> MarkAsReadItemChanges { get; private set; }

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06002356 RID: 9046 RVA: 0x000A3B8E File Offset: 0x000A1D8E
		// (set) Token: 0x06002357 RID: 9047 RVA: 0x000A3B96 File Offset: 0x000A1D96
		[XmlIgnore]
		[IgnoreDataMember]
		internal List<KeyValuePair<ItemChange, StoreId>> MarkAsUnreadItemChanges { get; private set; }

		// Token: 0x06002358 RID: 9048 RVA: 0x000A3B9F File Offset: 0x000A1D9F
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			if (this.CanOptimizeCommandExecution(callContext))
			{
				return new UpdateItemIsReadBatch(callContext, this);
			}
			return new UpdateItem(callContext, this);
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x000A3BBC File Offset: 0x000A1DBC
		internal override bool CanOptimizeCommandExecution(CallContext callContext)
		{
			if (!base.AllowCommandOptimization("updateitem"))
			{
				return false;
			}
			if (ExchangeVersion.Current == ExchangeVersion.Exchange2007)
			{
				return false;
			}
			IdConverter idConverter = new IdConverter(callContext);
			List<KeyValuePair<ItemChange, StoreId>> list = new List<KeyValuePair<ItemChange, StoreId>>();
			List<KeyValuePair<ItemChange, StoreId>> list2 = new List<KeyValuePair<ItemChange, StoreId>>();
			Guid? guid = null;
			new List<ItemChange>();
			ItemChange[] itemChanges = this.ItemChanges;
			int i = 0;
			while (i < itemChanges.Length)
			{
				ItemChange itemChange = itemChanges[i];
				BaseItemId itemId = itemChange.ItemId;
				StoreId value;
				Guid value2;
				if (idConverter.TryGetStoreIdAndMailboxGuidFromItemId(itemId, out value, out value2))
				{
					if (guid == null)
					{
						guid = new Guid?(value2);
					}
					else if (!value2.Equals(guid.Value))
					{
						return false;
					}
					if (itemChange.PropertyUpdates.Length == 1)
					{
						PropertyUpdate propertyUpdate = itemChange.PropertyUpdates[0];
						SetItemPropertyUpdate setItemPropertyUpdate = propertyUpdate as SetItemPropertyUpdate;
						if (setItemPropertyUpdate != null)
						{
							PropertyUri propertyUri = setItemPropertyUpdate.PropertyPath as PropertyUri;
							if (propertyUri != null && propertyUri.Uri == PropertyUriEnum.IsRead)
							{
								KeyValuePair<ItemChange, StoreId> item = new KeyValuePair<ItemChange, StoreId>(itemChange, value);
								bool valueOrDefault = setItemPropertyUpdate.ServiceObject.GetValueOrDefault<bool>(MessageSchema.IsRead);
								if (valueOrDefault)
								{
									list.Add(item);
								}
								else
								{
									list2.Add(item);
								}
							}
						}
					}
					i++;
					continue;
				}
				return false;
			}
			this.MarkAsUnreadItemChanges = list2;
			this.MarkAsReadItemChanges = list;
			return this.MarkAsReadItemChanges.Count > 0 || this.MarkAsUnreadItemChanges.Count > 0;
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x000A3D22 File Offset: 0x000A1F22
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.ItemChanges == null || this.ItemChanges.Length == 0)
			{
				return null;
			}
			return BaseRequest.GetServerInfoForItemId(callContext, this.ItemChanges[0].ItemId);
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x000A3D4C File Offset: 0x000A1F4C
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			BaseServerIdInfo baseServerIdInfo = (this.SavedItemFolderId == null || this.SavedItemFolderId.BaseFolderId == null) ? null : BaseRequest.GetServerInfoForFolderId(callContext, this.SavedItemFolderId.BaseFolderId);
			BaseServerIdInfo baseServerIdInfo2 = (this.ItemChanges == null || this.ItemChanges.Length == 0) ? null : BaseRequest.GetServerInfoForItemId(callContext, this.ItemChanges[taskStep].ItemId);
			return BaseRequest.ServerInfosToResourceKeys(true, new BaseServerIdInfo[]
			{
				baseServerIdInfo,
				baseServerIdInfo2
			});
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x000A3DC2 File Offset: 0x000A1FC2
		internal override void Validate()
		{
			base.Validate();
			if (this.ItemChanges == null || this.ItemChanges.Length == 0)
			{
				throw FaultExceptionUtilities.CreateFault(new ServiceInvalidOperationException(ResponseCodeType.ErrorInvalidArgument), FaultParty.Sender);
			}
		}

		// Token: 0x04001533 RID: 5427
		internal const string ElementName = "UpdateItem";

		// Token: 0x04001534 RID: 5428
		internal const string SavedItemFolderIdElementName = "SavedItemFolderId";

		// Token: 0x04001535 RID: 5429
		internal const string ItemChangesElementName = "ItemChanges";

		// Token: 0x04001536 RID: 5430
		private OutboundCharsetOptions outboundCharset;
	}
}
