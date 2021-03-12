using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005E1 RID: 1505
	[XmlType("ImGroupType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "ImGroup")]
	[Serializable]
	public class ImGroup
	{
		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06002D4C RID: 11596 RVA: 0x000B1FA8 File Offset: 0x000B01A8
		// (set) Token: 0x06002D4D RID: 11597 RVA: 0x000B1FB0 File Offset: 0x000B01B0
		[DataMember(Order = 1)]
		[XmlElement]
		public string DisplayName { get; set; }

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06002D4E RID: 11598 RVA: 0x000B1FB9 File Offset: 0x000B01B9
		// (set) Token: 0x06002D4F RID: 11599 RVA: 0x000B1FC1 File Offset: 0x000B01C1
		[XmlElement]
		[DataMember(Order = 2)]
		public string GroupType { get; set; }

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06002D50 RID: 11600 RVA: 0x000B1FCA File Offset: 0x000B01CA
		// (set) Token: 0x06002D51 RID: 11601 RVA: 0x000B1FD2 File Offset: 0x000B01D2
		[XmlElement]
		[DataMember(Order = 3)]
		public ItemId ExchangeStoreId { get; set; }

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06002D52 RID: 11602 RVA: 0x000B1FDB File Offset: 0x000B01DB
		// (set) Token: 0x06002D53 RID: 11603 RVA: 0x000B1FE3 File Offset: 0x000B01E3
		[DataMember(Order = 4)]
		[XmlArray]
		[XmlArrayItem("ItemId", typeof(ItemId))]
		public ItemId[] MemberCorrelationKey { get; set; }

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06002D54 RID: 11604 RVA: 0x000B1FEC File Offset: 0x000B01EC
		// (set) Token: 0x06002D55 RID: 11605 RVA: 0x000B1FF4 File Offset: 0x000B01F4
		[XmlArrayItem("ExtendedProperty", typeof(ExtendedPropertyType))]
		[XmlArray]
		[DataMember(Order = 5)]
		public ExtendedPropertyType[] ExtendedProperties { get; set; }

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06002D56 RID: 11606 RVA: 0x000B1FFD File Offset: 0x000B01FD
		// (set) Token: 0x06002D57 RID: 11607 RVA: 0x000B2005 File Offset: 0x000B0205
		[DataMember(Order = 6)]
		[XmlElement]
		public string SmtpAddress { get; set; }

		// Token: 0x06002D58 RID: 11608 RVA: 0x000B2010 File Offset: 0x000B0210
		internal static ImGroup LoadFromRawImGroup(RawImGroup rawImGroup, MailboxSession session)
		{
			ImGroup imGroup = new ImGroup();
			imGroup.DisplayName = rawImGroup.DisplayName;
			imGroup.GroupType = rawImGroup.GroupType;
			imGroup.ExchangeStoreId = IdConverter.ConvertStoreItemIdToItemId(rawImGroup.ExchangeStoreId, session);
			if (rawImGroup.MemberCorrelationKey.Length > 0)
			{
				List<ItemId> list = new List<ItemId>(rawImGroup.MemberCorrelationKey.Length);
				foreach (StoreObjectId storeItemId in rawImGroup.MemberCorrelationKey)
				{
					list.Add(IdConverter.ConvertStoreItemIdToItemId(storeItemId, session));
				}
				imGroup.MemberCorrelationKey = list.ToArray();
			}
			imGroup.ExtendedProperties = rawImGroup.ExtendedProperties;
			imGroup.SmtpAddress = rawImGroup.SmtpAddress;
			return imGroup;
		}
	}
}
