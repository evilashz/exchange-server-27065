using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005B5 RID: 1461
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "Conversation")]
	[Serializable]
	public class ConversationType : ServiceObject
	{
		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06002BDA RID: 11226 RVA: 0x000AFF91 File Offset: 0x000AE191
		// (set) Token: 0x06002BDB RID: 11227 RVA: 0x000AFF99 File Offset: 0x000AE199
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public BaseFolderId FolderId { get; set; }

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06002BDC RID: 11228 RVA: 0x000AFFA2 File Offset: 0x000AE1A2
		// (set) Token: 0x06002BDD RID: 11229 RVA: 0x000AFFB4 File Offset: 0x000AE1B4
		[DataMember(Order = 2)]
		public ItemId ConversationId
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<ItemId>(ConversationSchema.ConversationId);
			}
			set
			{
				base.PropertyBag[ConversationSchema.ConversationId] = value;
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06002BDE RID: 11230 RVA: 0x000AFFC7 File Offset: 0x000AE1C7
		// (set) Token: 0x06002BDF RID: 11231 RVA: 0x000AFFD9 File Offset: 0x000AE1D9
		[DataMember(Order = 4)]
		public string ConversationTopic
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ConversationSchema.ConversationTopic);
			}
			set
			{
				base.PropertyBag[ConversationSchema.ConversationTopic] = value;
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06002BE0 RID: 11232 RVA: 0x000AFFEC File Offset: 0x000AE1EC
		// (set) Token: 0x06002BE1 RID: 11233 RVA: 0x000AFFFE File Offset: 0x000AE1FE
		[DataMember(EmitDefaultValue = false, Order = 5)]
		[XmlArrayItem("String", IsNullable = false)]
		public string[] UniqueRecipients
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string[]>(ConversationSchema.UniqueRecipients);
			}
			set
			{
				base.PropertyBag[ConversationSchema.UniqueRecipients] = value;
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06002BE2 RID: 11234 RVA: 0x000B0011 File Offset: 0x000AE211
		// (set) Token: 0x06002BE3 RID: 11235 RVA: 0x000B0023 File Offset: 0x000AE223
		[DataMember(EmitDefaultValue = false, Order = 6)]
		[XmlArrayItem("String", IsNullable = false)]
		public string[] GlobalUniqueRecipients
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string[]>(ConversationSchema.GlobalUniqueRecipients);
			}
			set
			{
				base.PropertyBag[ConversationSchema.GlobalUniqueRecipients] = value;
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06002BE4 RID: 11236 RVA: 0x000B0036 File Offset: 0x000AE236
		// (set) Token: 0x06002BE5 RID: 11237 RVA: 0x000B0048 File Offset: 0x000AE248
		[XmlArrayItem("String", IsNullable = false)]
		[DataMember(EmitDefaultValue = false, Order = 7)]
		public string[] UniqueUnreadSenders
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string[]>(ConversationSchema.UniqueUnreadSenders);
			}
			set
			{
				base.PropertyBag[ConversationSchema.UniqueUnreadSenders] = value;
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06002BE6 RID: 11238 RVA: 0x000B005B File Offset: 0x000AE25B
		// (set) Token: 0x06002BE7 RID: 11239 RVA: 0x000B006D File Offset: 0x000AE26D
		[XmlArrayItem("String", IsNullable = false)]
		[DataMember(EmitDefaultValue = false, Order = 8)]
		public string[] GlobalUniqueUnreadSenders
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string[]>(ConversationSchema.GlobalUniqueUnreadSenders);
			}
			set
			{
				base.PropertyBag[ConversationSchema.GlobalUniqueUnreadSenders] = value;
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06002BE8 RID: 11240 RVA: 0x000B0080 File Offset: 0x000AE280
		// (set) Token: 0x06002BE9 RID: 11241 RVA: 0x000B0092 File Offset: 0x000AE292
		[DataMember(EmitDefaultValue = false, Order = 9)]
		[XmlArrayItem("String", IsNullable = false)]
		public string[] UniqueSenders
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string[]>(ConversationSchema.UniqueSenders);
			}
			set
			{
				base.PropertyBag[ConversationSchema.UniqueSenders] = value;
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06002BEA RID: 11242 RVA: 0x000B00A5 File Offset: 0x000AE2A5
		// (set) Token: 0x06002BEB RID: 11243 RVA: 0x000B00B7 File Offset: 0x000AE2B7
		[XmlArrayItem("String", IsNullable = false)]
		[DataMember(EmitDefaultValue = false, Order = 10)]
		public string[] GlobalUniqueSenders
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string[]>(ConversationSchema.GlobalUniqueSenders);
			}
			set
			{
				base.PropertyBag[ConversationSchema.GlobalUniqueSenders] = value;
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06002BEC RID: 11244 RVA: 0x000B00CA File Offset: 0x000AE2CA
		// (set) Token: 0x06002BED RID: 11245 RVA: 0x000B00D7 File Offset: 0x000AE2D7
		[DataMember(EmitDefaultValue = false, Order = 11)]
		[DateTimeString]
		public string LastDeliveryTime
		{
			get
			{
				return base.GetValueOrDefault<string>(ConversationSchema.LastDeliveryTime);
			}
			set
			{
				this[ConversationSchema.LastDeliveryTime] = value;
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06002BEE RID: 11246 RVA: 0x000B00E5 File Offset: 0x000AE2E5
		// (set) Token: 0x06002BEF RID: 11247 RVA: 0x000B00F2 File Offset: 0x000AE2F2
		[DateTimeString]
		[DataMember(EmitDefaultValue = false, Order = 12)]
		public string GlobalLastDeliveryTime
		{
			get
			{
				return base.GetValueOrDefault<string>(ConversationSchema.GlobalLastDeliveryTime);
			}
			set
			{
				this[ConversationSchema.GlobalLastDeliveryTime] = value;
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06002BF0 RID: 11248 RVA: 0x000B0100 File Offset: 0x000AE300
		// (set) Token: 0x06002BF1 RID: 11249 RVA: 0x000B0112 File Offset: 0x000AE312
		[DataMember(EmitDefaultValue = false, Order = 13)]
		[XmlArrayItem("String", IsNullable = false)]
		public string[] Categories
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string[]>(ConversationSchema.Categories);
			}
			set
			{
				base.PropertyBag[ConversationSchema.Categories] = value;
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06002BF2 RID: 11250 RVA: 0x000B0125 File Offset: 0x000AE325
		// (set) Token: 0x06002BF3 RID: 11251 RVA: 0x000B0137 File Offset: 0x000AE337
		[XmlArrayItem("String", IsNullable = false)]
		[DataMember(EmitDefaultValue = false, Order = 14)]
		public string[] GlobalCategories
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string[]>(ConversationSchema.GlobalCategories);
			}
			set
			{
				base.PropertyBag[ConversationSchema.GlobalCategories] = value;
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06002BF4 RID: 11252 RVA: 0x000B014A File Offset: 0x000AE34A
		// (set) Token: 0x06002BF5 RID: 11253 RVA: 0x000B015C File Offset: 0x000AE35C
		[DataMember(EmitDefaultValue = false, Name = "FlagStatus", Order = 15)]
		[XmlIgnore]
		public string FlagStatusString
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ConversationSchema.FlagStatus);
			}
			set
			{
				base.PropertyBag[ConversationSchema.FlagStatus] = value;
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06002BF6 RID: 11254 RVA: 0x000B016F File Offset: 0x000AE36F
		// (set) Token: 0x06002BF7 RID: 11255 RVA: 0x000B0186 File Offset: 0x000AE386
		[XmlElement("FlagStatus")]
		[IgnoreDataMember]
		public FlagStatus FlagStatus
		{
			get
			{
				if (!this.FlagStatusSpecified)
				{
					return FlagStatus.NotFlagged;
				}
				return EnumUtilities.Parse<FlagStatus>(this.FlagStatusString);
			}
			set
			{
				this.FlagStatusString = EnumUtilities.ToString<FlagStatus>(value);
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06002BF8 RID: 11256 RVA: 0x000B0194 File Offset: 0x000AE394
		// (set) Token: 0x06002BF9 RID: 11257 RVA: 0x000B01A1 File Offset: 0x000AE3A1
		[IgnoreDataMember]
		[XmlIgnore]
		public bool FlagStatusSpecified
		{
			get
			{
				return base.IsSet(ConversationSchema.FlagStatus);
			}
			set
			{
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06002BFA RID: 11258 RVA: 0x000B01A3 File Offset: 0x000AE3A3
		// (set) Token: 0x06002BFB RID: 11259 RVA: 0x000B01B5 File Offset: 0x000AE3B5
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, Name = "GlobalFlagStatus", Order = 16)]
		public string GlobalFlagStatusString
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ConversationSchema.GlobalFlagStatus);
			}
			set
			{
				base.PropertyBag[ConversationSchema.GlobalFlagStatus] = value;
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06002BFC RID: 11260 RVA: 0x000B01C8 File Offset: 0x000AE3C8
		// (set) Token: 0x06002BFD RID: 11261 RVA: 0x000B01DF File Offset: 0x000AE3DF
		[IgnoreDataMember]
		[XmlElement("GlobalFlagStatus")]
		public FlagStatus GlobalFlagStatus
		{
			get
			{
				if (!this.GlobalFlagStatusSpecified)
				{
					return FlagStatus.NotFlagged;
				}
				return EnumUtilities.Parse<FlagStatus>(this.GlobalFlagStatusString);
			}
			set
			{
				this.GlobalFlagStatusString = EnumUtilities.ToString<FlagStatus>(value);
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06002BFE RID: 11262 RVA: 0x000B01ED File Offset: 0x000AE3ED
		// (set) Token: 0x06002BFF RID: 11263 RVA: 0x000B01FA File Offset: 0x000AE3FA
		[XmlIgnore]
		[IgnoreDataMember]
		public bool GlobalFlagStatusSpecified
		{
			get
			{
				return base.IsSet(ConversationSchema.GlobalFlagStatus);
			}
			set
			{
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06002C00 RID: 11264 RVA: 0x000B01FC File Offset: 0x000AE3FC
		// (set) Token: 0x06002C01 RID: 11265 RVA: 0x000B020E File Offset: 0x000AE40E
		[DataMember(EmitDefaultValue = false, Order = 17)]
		public bool? HasAttachments
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(ConversationSchema.HasAttachments);
			}
			set
			{
				base.PropertyBag.SetNullableValue<bool>(ConversationSchema.HasAttachments, value);
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06002C02 RID: 11266 RVA: 0x000B0221 File Offset: 0x000AE421
		// (set) Token: 0x06002C03 RID: 11267 RVA: 0x000B022E File Offset: 0x000AE42E
		[XmlIgnore]
		[IgnoreDataMember]
		public bool HasAttachmentsSpecified
		{
			get
			{
				return base.IsSet(ConversationSchema.HasAttachments);
			}
			set
			{
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06002C04 RID: 11268 RVA: 0x000B0230 File Offset: 0x000AE430
		// (set) Token: 0x06002C05 RID: 11269 RVA: 0x000B0242 File Offset: 0x000AE442
		[DataMember(EmitDefaultValue = false, Order = 18)]
		public bool? GlobalHasAttachments
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(ConversationSchema.GlobalHasAttachments);
			}
			set
			{
				base.PropertyBag.SetNullableValue<bool>(ConversationSchema.GlobalHasAttachments, value);
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06002C06 RID: 11270 RVA: 0x000B0255 File Offset: 0x000AE455
		// (set) Token: 0x06002C07 RID: 11271 RVA: 0x000B0262 File Offset: 0x000AE462
		[IgnoreDataMember]
		[XmlIgnore]
		public bool GlobalHasAttachmentsSpecified
		{
			get
			{
				return base.IsSet(ConversationSchema.GlobalHasAttachments);
			}
			set
			{
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06002C08 RID: 11272 RVA: 0x000B0264 File Offset: 0x000AE464
		// (set) Token: 0x06002C09 RID: 11273 RVA: 0x000B0276 File Offset: 0x000AE476
		[DataMember(EmitDefaultValue = false, Order = 19)]
		public int? MessageCount
		{
			get
			{
				return base.PropertyBag.GetNullableValue<int>(ConversationSchema.MessageCount);
			}
			set
			{
				base.PropertyBag.SetNullableValue<int>(ConversationSchema.MessageCount, value);
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06002C0A RID: 11274 RVA: 0x000B0289 File Offset: 0x000AE489
		// (set) Token: 0x06002C0B RID: 11275 RVA: 0x000B0296 File Offset: 0x000AE496
		[XmlIgnore]
		[IgnoreDataMember]
		public bool MessageCountSpecified
		{
			get
			{
				return base.IsSet(ConversationSchema.MessageCount);
			}
			set
			{
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06002C0C RID: 11276 RVA: 0x000B0298 File Offset: 0x000AE498
		// (set) Token: 0x06002C0D RID: 11277 RVA: 0x000B02AA File Offset: 0x000AE4AA
		[DataMember(EmitDefaultValue = false, Order = 20)]
		public int? GlobalMessageCount
		{
			get
			{
				return base.PropertyBag.GetNullableValue<int>(ConversationSchema.GlobalMessageCount);
			}
			set
			{
				base.PropertyBag.SetNullableValue<int>(ConversationSchema.GlobalMessageCount, value);
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06002C0E RID: 11278 RVA: 0x000B02BD File Offset: 0x000AE4BD
		// (set) Token: 0x06002C0F RID: 11279 RVA: 0x000B02CA File Offset: 0x000AE4CA
		[XmlIgnore]
		[IgnoreDataMember]
		public bool GlobalMessageCountSpecified
		{
			get
			{
				return base.IsSet(ConversationSchema.GlobalMessageCount);
			}
			set
			{
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06002C10 RID: 11280 RVA: 0x000B02CC File Offset: 0x000AE4CC
		// (set) Token: 0x06002C11 RID: 11281 RVA: 0x000B02DE File Offset: 0x000AE4DE
		[DataMember(EmitDefaultValue = false, Order = 21)]
		public int? UnreadCount
		{
			get
			{
				return base.PropertyBag.GetNullableValue<int>(ConversationSchema.UnreadCount);
			}
			set
			{
				base.PropertyBag.SetNullableValue<int>(ConversationSchema.UnreadCount, value);
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06002C12 RID: 11282 RVA: 0x000B02F1 File Offset: 0x000AE4F1
		// (set) Token: 0x06002C13 RID: 11283 RVA: 0x000B02FE File Offset: 0x000AE4FE
		[IgnoreDataMember]
		[XmlIgnore]
		public bool UnreadCountSpecified
		{
			get
			{
				return base.IsSet(ConversationSchema.UnreadCount);
			}
			set
			{
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06002C14 RID: 11284 RVA: 0x000B0300 File Offset: 0x000AE500
		// (set) Token: 0x06002C15 RID: 11285 RVA: 0x000B0312 File Offset: 0x000AE512
		[DataMember(EmitDefaultValue = false, Order = 22)]
		public int? GlobalUnreadCount
		{
			get
			{
				return base.PropertyBag.GetNullableValue<int>(ConversationSchema.GlobalUnreadCount);
			}
			set
			{
				base.PropertyBag.SetNullableValue<int>(ConversationSchema.GlobalUnreadCount, value);
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06002C16 RID: 11286 RVA: 0x000B0325 File Offset: 0x000AE525
		// (set) Token: 0x06002C17 RID: 11287 RVA: 0x000B0332 File Offset: 0x000AE532
		[IgnoreDataMember]
		[XmlIgnore]
		public bool GlobalUnreadCountSpecified
		{
			get
			{
				return base.IsSet(ConversationSchema.GlobalUnreadCount);
			}
			set
			{
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06002C18 RID: 11288 RVA: 0x000B0334 File Offset: 0x000AE534
		// (set) Token: 0x06002C19 RID: 11289 RVA: 0x000B0346 File Offset: 0x000AE546
		[DataMember(EmitDefaultValue = false, Order = 23)]
		public int? Size
		{
			get
			{
				return base.PropertyBag.GetNullableValue<int>(ConversationSchema.Size);
			}
			set
			{
				base.PropertyBag.SetNullableValue<int>(ConversationSchema.Size, value);
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06002C1A RID: 11290 RVA: 0x000B0359 File Offset: 0x000AE559
		// (set) Token: 0x06002C1B RID: 11291 RVA: 0x000B0366 File Offset: 0x000AE566
		[IgnoreDataMember]
		[XmlIgnore]
		public bool SizeSpecified
		{
			get
			{
				return base.IsSet(ConversationSchema.Size);
			}
			set
			{
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06002C1C RID: 11292 RVA: 0x000B0368 File Offset: 0x000AE568
		// (set) Token: 0x06002C1D RID: 11293 RVA: 0x000B037A File Offset: 0x000AE57A
		[DataMember(EmitDefaultValue = false, Order = 24)]
		public int? GlobalSize
		{
			get
			{
				return base.PropertyBag.GetNullableValue<int>(ConversationSchema.GlobalSize);
			}
			set
			{
				base.PropertyBag.SetNullableValue<int>(ConversationSchema.GlobalSize, value);
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06002C1E RID: 11294 RVA: 0x000B038D File Offset: 0x000AE58D
		// (set) Token: 0x06002C1F RID: 11295 RVA: 0x000B039A File Offset: 0x000AE59A
		[XmlIgnore]
		[IgnoreDataMember]
		public bool GlobalSizeSpecified
		{
			get
			{
				return base.IsSet(ConversationSchema.GlobalSize);
			}
			set
			{
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06002C20 RID: 11296 RVA: 0x000B039C File Offset: 0x000AE59C
		// (set) Token: 0x06002C21 RID: 11297 RVA: 0x000B03AE File Offset: 0x000AE5AE
		[XmlArrayItem("ItemClass", IsNullable = false)]
		[DataMember(EmitDefaultValue = false, Order = 25)]
		public string[] ItemClasses
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string[]>(ConversationSchema.ItemClasses);
			}
			set
			{
				base.PropertyBag[ConversationSchema.ItemClasses] = value;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06002C22 RID: 11298 RVA: 0x000B03C1 File Offset: 0x000AE5C1
		// (set) Token: 0x06002C23 RID: 11299 RVA: 0x000B03D3 File Offset: 0x000AE5D3
		[DataMember(EmitDefaultValue = false, Order = 26)]
		[XmlArrayItem("ItemClass", IsNullable = false)]
		public string[] GlobalItemClasses
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string[]>(ConversationSchema.GlobalItemClasses);
			}
			set
			{
				base.PropertyBag[ConversationSchema.GlobalItemClasses] = value;
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06002C24 RID: 11300 RVA: 0x000B03E6 File Offset: 0x000AE5E6
		// (set) Token: 0x06002C25 RID: 11301 RVA: 0x000B03FD File Offset: 0x000AE5FD
		[IgnoreDataMember]
		public ImportanceType Importance
		{
			get
			{
				if (!this.ImportanceSpecified)
				{
					return ImportanceType.Normal;
				}
				return EnumUtilities.Parse<ImportanceType>(this.ImportanceString);
			}
			set
			{
				this.ImportanceString = EnumUtilities.ToString<ImportanceType>(value);
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06002C26 RID: 11302 RVA: 0x000B040B File Offset: 0x000AE60B
		// (set) Token: 0x06002C27 RID: 11303 RVA: 0x000B041D File Offset: 0x000AE61D
		[DataMember(EmitDefaultValue = false, Name = "Importance", Order = 27)]
		[XmlIgnore]
		public string ImportanceString
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ConversationSchema.Importance);
			}
			set
			{
				base.PropertyBag[ConversationSchema.Importance] = value;
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06002C28 RID: 11304 RVA: 0x000B0430 File Offset: 0x000AE630
		// (set) Token: 0x06002C29 RID: 11305 RVA: 0x000B043D File Offset: 0x000AE63D
		[XmlIgnore]
		[IgnoreDataMember]
		public bool ImportanceSpecified
		{
			get
			{
				return base.IsSet(ConversationSchema.Importance);
			}
			set
			{
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06002C2A RID: 11306 RVA: 0x000B043F File Offset: 0x000AE63F
		// (set) Token: 0x06002C2B RID: 11307 RVA: 0x000B0456 File Offset: 0x000AE656
		[IgnoreDataMember]
		public ImportanceType GlobalImportance
		{
			get
			{
				if (!this.GlobalImportanceSpecified)
				{
					return ImportanceType.Normal;
				}
				return EnumUtilities.Parse<ImportanceType>(this.GlobalImportanceString);
			}
			set
			{
				this.GlobalImportanceString = EnumUtilities.ToString<ImportanceType>(value);
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06002C2C RID: 11308 RVA: 0x000B0464 File Offset: 0x000AE664
		// (set) Token: 0x06002C2D RID: 11309 RVA: 0x000B0476 File Offset: 0x000AE676
		[DataMember(EmitDefaultValue = false, Name = "GlobalImportance", Order = 28)]
		[XmlIgnore]
		public string GlobalImportanceString
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ConversationSchema.GlobalImportance);
			}
			set
			{
				base.PropertyBag[ConversationSchema.GlobalImportance] = value;
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06002C2E RID: 11310 RVA: 0x000B0489 File Offset: 0x000AE689
		// (set) Token: 0x06002C2F RID: 11311 RVA: 0x000B0496 File Offset: 0x000AE696
		[IgnoreDataMember]
		[XmlIgnore]
		public bool GlobalImportanceSpecified
		{
			get
			{
				return base.IsSet(ConversationSchema.GlobalImportance);
			}
			set
			{
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06002C30 RID: 11312 RVA: 0x000B0498 File Offset: 0x000AE698
		// (set) Token: 0x06002C31 RID: 11313 RVA: 0x000B04AA File Offset: 0x000AE6AA
		[XmlArrayItem("OccurrenceItemId", typeof(OccurrenceItemId), IsNullable = false)]
		[DataMember(EmitDefaultValue = false, Order = 29)]
		[XmlArrayItem("ItemId", typeof(ItemId), IsNullable = false)]
		[XmlArrayItem("RecurringMasterItemId", typeof(RecurringMasterItemId), IsNullable = false)]
		public BaseItemId[] ItemIds
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<BaseItemId[]>(ConversationSchema.ItemIds);
			}
			set
			{
				base.PropertyBag[ConversationSchema.ItemIds] = value;
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06002C32 RID: 11314 RVA: 0x000B04BD File Offset: 0x000AE6BD
		// (set) Token: 0x06002C33 RID: 11315 RVA: 0x000B04CF File Offset: 0x000AE6CF
		[XmlArrayItem("ItemId", typeof(ItemId), IsNullable = false)]
		[XmlArrayItem("RecurringMasterItemId", typeof(RecurringMasterItemId), IsNullable = false)]
		[DataMember(EmitDefaultValue = false, Order = 30)]
		[XmlArrayItem("OccurrenceItemId", typeof(OccurrenceItemId), IsNullable = false)]
		public BaseItemId[] GlobalItemIds
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<BaseItemId[]>(ConversationSchema.GlobalItemIds);
			}
			set
			{
				base.PropertyBag[ConversationSchema.GlobalItemIds] = value;
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06002C34 RID: 11316 RVA: 0x000B04E2 File Offset: 0x000AE6E2
		// (set) Token: 0x06002C35 RID: 11317 RVA: 0x000B04F4 File Offset: 0x000AE6F4
		[DateTimeString]
		[DataMember(EmitDefaultValue = false, Order = 31)]
		public string LastModifiedTime
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ConversationSchema.LastModifiedTime);
			}
			set
			{
				base.PropertyBag[ConversationSchema.LastModifiedTime] = value;
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06002C36 RID: 11318 RVA: 0x000B0507 File Offset: 0x000AE707
		// (set) Token: 0x06002C37 RID: 11319 RVA: 0x000B0519 File Offset: 0x000AE719
		[XmlElement(DataType = "base64Binary")]
		[IgnoreDataMember]
		public byte[] InstanceKey
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<byte[]>(ConversationSchema.InstanceKey);
			}
			set
			{
				base.PropertyBag[ConversationSchema.InstanceKey] = value;
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06002C38 RID: 11320 RVA: 0x000B052C File Offset: 0x000AE72C
		// (set) Token: 0x06002C39 RID: 11321 RVA: 0x000B054B File Offset: 0x000AE74B
		[DataMember(Name = "InstanceKey", EmitDefaultValue = false, Order = 32)]
		[XmlIgnore]
		public string InstanceKeyString
		{
			get
			{
				byte[] instanceKey = this.InstanceKey;
				if (instanceKey == null)
				{
					return null;
				}
				return Convert.ToBase64String(instanceKey);
			}
			set
			{
				this.InstanceKey = ((value != null) ? Convert.FromBase64String(value) : null);
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06002C3A RID: 11322 RVA: 0x000B055F File Offset: 0x000AE75F
		// (set) Token: 0x06002C3B RID: 11323 RVA: 0x000B0571 File Offset: 0x000AE771
		[DataMember(EmitDefaultValue = false, Order = 33)]
		public string Preview
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ConversationSchema.Preview);
			}
			set
			{
				base.PropertyBag[ConversationSchema.Preview] = value;
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06002C3C RID: 11324 RVA: 0x000B0584 File Offset: 0x000AE784
		// (set) Token: 0x06002C3D RID: 11325 RVA: 0x000B058C File Offset: 0x000AE78C
		[IgnoreDataMember]
		[XmlIgnore]
		public bool MailboxScopeSpecified { get; set; }

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06002C3E RID: 11326 RVA: 0x000B0595 File Offset: 0x000AE795
		// (set) Token: 0x06002C3F RID: 11327 RVA: 0x000B059D File Offset: 0x000AE79D
		[XmlElement]
		[IgnoreDataMember]
		public MailboxSearchLocation MailboxScope
		{
			get
			{
				return this.mailboxScope;
			}
			set
			{
				this.mailboxScope = value;
				this.MailboxScopeSpecified = true;
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06002C40 RID: 11328 RVA: 0x000B05AD File Offset: 0x000AE7AD
		// (set) Token: 0x06002C41 RID: 11329 RVA: 0x000B05C4 File Offset: 0x000AE7C4
		[XmlIgnore]
		[DataMember(Name = "MailboxScope", IsRequired = false, Order = 36)]
		public string MailboxScopeString
		{
			get
			{
				if (!this.MailboxScopeSpecified)
				{
					return null;
				}
				return EnumUtilities.ToString<MailboxSearchLocation>(this.mailboxScope);
			}
			set
			{
				this.MailboxScope = EnumUtilities.Parse<MailboxSearchLocation>(value);
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06002C42 RID: 11330 RVA: 0x000B05D2 File Offset: 0x000AE7D2
		// (set) Token: 0x06002C43 RID: 11331 RVA: 0x000B05E9 File Offset: 0x000AE7E9
		[IgnoreDataMember]
		[XmlElement]
		public IconIndexType IconIndex
		{
			get
			{
				if (!this.IconIndexSpecified)
				{
					return IconIndexType.Default;
				}
				return EnumUtilities.Parse<IconIndexType>(this.IconIndexString);
			}
			set
			{
				this.IconIndexString = EnumUtilities.ToString<IconIndexType>(value);
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06002C44 RID: 11332 RVA: 0x000B05F7 File Offset: 0x000AE7F7
		// (set) Token: 0x06002C45 RID: 11333 RVA: 0x000B0609 File Offset: 0x000AE809
		[DataMember(Name = "IconIndex", EmitDefaultValue = false, Order = 37)]
		[XmlIgnore]
		public string IconIndexString
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ConversationSchema.IconIndex);
			}
			set
			{
				base.PropertyBag[ConversationSchema.IconIndex] = value;
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06002C46 RID: 11334 RVA: 0x000B061C File Offset: 0x000AE81C
		// (set) Token: 0x06002C47 RID: 11335 RVA: 0x000B0629 File Offset: 0x000AE829
		[XmlIgnore]
		[IgnoreDataMember]
		public bool IconIndexSpecified
		{
			get
			{
				return base.IsSet(ConversationSchema.IconIndex);
			}
			set
			{
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06002C48 RID: 11336 RVA: 0x000B062B File Offset: 0x000AE82B
		// (set) Token: 0x06002C49 RID: 11337 RVA: 0x000B0642 File Offset: 0x000AE842
		[IgnoreDataMember]
		[XmlElement]
		public IconIndexType GlobalIconIndex
		{
			get
			{
				if (!this.GlobalIconIndexSpecified)
				{
					return IconIndexType.Default;
				}
				return EnumUtilities.Parse<IconIndexType>(this.GlobalIconIndexString);
			}
			set
			{
				this.GlobalIconIndexString = EnumUtilities.ToString<IconIndexType>(value);
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06002C4A RID: 11338 RVA: 0x000B0650 File Offset: 0x000AE850
		// (set) Token: 0x06002C4B RID: 11339 RVA: 0x000B0662 File Offset: 0x000AE862
		[XmlIgnore]
		[DataMember(Name = "GlobalIconIndex", EmitDefaultValue = false, Order = 38)]
		public string GlobalIconIndexString
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ConversationSchema.GlobalIconIndex);
			}
			set
			{
				base.PropertyBag[ConversationSchema.GlobalIconIndex] = value;
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06002C4C RID: 11340 RVA: 0x000B0675 File Offset: 0x000AE875
		// (set) Token: 0x06002C4D RID: 11341 RVA: 0x000B0682 File Offset: 0x000AE882
		[XmlIgnore]
		[IgnoreDataMember]
		public bool GlobalIconIndexSpecified
		{
			get
			{
				return base.IsSet(ConversationSchema.GlobalIconIndex);
			}
			set
			{
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06002C4E RID: 11342 RVA: 0x000B0684 File Offset: 0x000AE884
		// (set) Token: 0x06002C4F RID: 11343 RVA: 0x000B0696 File Offset: 0x000AE896
		[XmlArrayItem("ItemId", typeof(ItemId), IsNullable = false)]
		[DataMember(EmitDefaultValue = false, Order = 39)]
		public BaseItemId[] DraftItemIds
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<BaseItemId[]>(ConversationSchema.DraftItemIds);
			}
			set
			{
				base.PropertyBag[ConversationSchema.DraftItemIds] = value;
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06002C50 RID: 11344 RVA: 0x000B06A9 File Offset: 0x000AE8A9
		// (set) Token: 0x06002C51 RID: 11345 RVA: 0x000B06BB File Offset: 0x000AE8BB
		[DataMember(EmitDefaultValue = false, Order = 40)]
		public bool? HasIrm
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(ConversationSchema.HasIrm);
			}
			set
			{
				base.PropertyBag.SetNullableValue<bool>(ConversationSchema.HasIrm, value);
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06002C52 RID: 11346 RVA: 0x000B06CE File Offset: 0x000AE8CE
		// (set) Token: 0x06002C53 RID: 11347 RVA: 0x000B06DB File Offset: 0x000AE8DB
		[IgnoreDataMember]
		[XmlIgnore]
		public bool HasIrmSpecified
		{
			get
			{
				return base.IsSet(ConversationSchema.HasIrm);
			}
			set
			{
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06002C54 RID: 11348 RVA: 0x000B06DD File Offset: 0x000AE8DD
		// (set) Token: 0x06002C55 RID: 11349 RVA: 0x000B06EF File Offset: 0x000AE8EF
		[DataMember(EmitDefaultValue = false, Order = 41)]
		public bool? GlobalHasIrm
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(ConversationSchema.GlobalHasIrm);
			}
			set
			{
				base.PropertyBag.SetNullableValue<bool>(ConversationSchema.GlobalHasIrm, value);
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06002C56 RID: 11350 RVA: 0x000B0702 File Offset: 0x000AE902
		// (set) Token: 0x06002C57 RID: 11351 RVA: 0x000B070F File Offset: 0x000AE90F
		[IgnoreDataMember]
		[XmlIgnore]
		public bool GlobalHasIrmSpecified
		{
			get
			{
				return base.IsSet(ConversationSchema.GlobalHasIrm);
			}
			set
			{
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06002C58 RID: 11352 RVA: 0x000B0711 File Offset: 0x000AE911
		// (set) Token: 0x06002C59 RID: 11353 RVA: 0x000B0723 File Offset: 0x000AE923
		[DataMember(EmitDefaultValue = false, Order = 42)]
		[XmlIgnore]
		public bool? HasClutter
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(ConversationSchema.HasClutter);
			}
			set
			{
				base.PropertyBag.SetNullableValue<bool>(ConversationSchema.HasClutter, value);
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06002C5A RID: 11354 RVA: 0x000B0736 File Offset: 0x000AE936
		// (set) Token: 0x06002C5B RID: 11355 RVA: 0x000B073E File Offset: 0x000AE93E
		internal IEnumerable<StoreId> DraftStoreIds { get; set; }

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06002C5C RID: 11356 RVA: 0x000B0747 File Offset: 0x000AE947
		// (set) Token: 0x06002C5D RID: 11357 RVA: 0x000B074F File Offset: 0x000AE94F
		internal IList<BaseItemId> DraftItemIdsList { get; set; }

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06002C5E RID: 11358 RVA: 0x000B0758 File Offset: 0x000AE958
		internal override StoreObjectType StoreObjectType
		{
			get
			{
				return StoreObjectType.ConversationActionItem;
			}
		}

		// Token: 0x06002C5F RID: 11359 RVA: 0x000B075C File Offset: 0x000AE95C
		internal static ConversationType MergeConversations(ConversationType x, ConversationType y)
		{
			ConversationType conversationType = new ConversationType();
			conversationType.Categories = ConversationHelper.MergeArray<string>(x.Categories, y.Categories);
			conversationType.ConversationId = x.ConversationId;
			conversationType.ConversationTopic = x.ConversationTopic;
			conversationType.DraftItemIds = ConversationHelper.MergeArray<BaseItemId>(x.DraftItemIds, y.DraftItemIds);
			conversationType.FlagStatus = x.FlagStatus;
			conversationType.FolderId = x.FolderId;
			conversationType.GlobalCategories = ConversationHelper.MergeArray<string>(x.GlobalCategories, y.GlobalCategories);
			conversationType.GlobalFlagStatus = x.GlobalFlagStatus;
			conversationType.GlobalHasAttachments = ConversationHelper.MergeBoolNullable(x.GlobalHasAttachments, y.GlobalHasAttachments);
			conversationType.GlobalImportance = x.GlobalImportance;
			conversationType.GlobalIconIndex = ((x.GlobalIconIndex == IconIndexType.Default) ? y.GlobalIconIndex : x.GlobalIconIndex);
			conversationType.GlobalItemClasses = ConversationHelper.MergeArray<string>(x.GlobalItemClasses, y.GlobalItemClasses);
			conversationType.GlobalItemIds = ConversationHelper.MergeArray<BaseItemId>(x.GlobalItemIds, y.GlobalItemIds);
			conversationType.GlobalLastDeliveryTime = ConversationHelper.MergeDates(x.GlobalLastDeliveryTime, y.GlobalLastDeliveryTime);
			conversationType.GlobalLastDeliveryOrRenewTime = ConversationHelper.MergeDates(x.GlobalLastDeliveryOrRenewTime, y.GlobalLastDeliveryOrRenewTime);
			conversationType.GlobalMessageCount = ConversationHelper.MergeInts(x.GlobalMessageCount, y.GlobalMessageCount);
			conversationType.GlobalSize = ConversationHelper.MergeInts(x.GlobalSize, y.GlobalSize);
			conversationType.GlobalRichContent = ConversationHelper.MergeArray<short>(x.GlobalRichContent, y.GlobalRichContent);
			conversationType.GlobalUniqueRecipients = ConversationHelper.MergeArray<string>(x.GlobalUniqueRecipients, y.GlobalUniqueRecipients);
			conversationType.GlobalUniqueSenders = ConversationHelper.MergeArray<string>(x.GlobalUniqueSenders, y.GlobalUniqueSenders);
			conversationType.GlobalUniqueUnreadSenders = ConversationHelper.MergeArray<string>(x.GlobalUniqueUnreadSenders, y.GlobalUniqueUnreadSenders);
			conversationType.GlobalUnreadCount = ConversationHelper.MergeInts(x.GlobalUnreadCount, y.GlobalUnreadCount);
			conversationType.HasAttachments = ConversationHelper.MergeBoolNullable(x.HasAttachments, y.HasAttachments);
			conversationType.IconIndex = ((x.IconIndex == IconIndexType.Default) ? y.IconIndex : x.IconIndex);
			conversationType.Importance = x.Importance;
			conversationType.SetInstanceKey(x.InstanceKey);
			conversationType.ItemClasses = ConversationHelper.MergeArray<string>(x.ItemClasses, y.ItemClasses);
			conversationType.ItemIds = ConversationHelper.MergeArray<BaseItemId>(x.ItemIds, y.ItemIds);
			conversationType.LastDeliveryTime = ConversationHelper.MergeDates(x.LastDeliveryTime, y.LastDeliveryTime);
			conversationType.LastDeliveryOrRenewTime = ConversationHelper.MergeDates(x.LastDeliveryOrRenewTime, y.LastDeliveryOrRenewTime);
			conversationType.LastModifiedTime = ConversationHelper.MergeDates(x.LastModifiedTime, y.LastModifiedTime);
			conversationType.MailboxScope = MailboxSearchLocation.All;
			conversationType.MessageCount = ConversationHelper.MergeInts(x.MessageCount, y.MessageCount);
			conversationType.Preview = x.Preview;
			conversationType.Size = ConversationHelper.MergeInts(x.Size, y.Size);
			conversationType.UniqueRecipients = ConversationHelper.MergeArray<string>(x.UniqueRecipients, y.UniqueRecipients);
			conversationType.UniqueSenders = ConversationHelper.MergeArray<string>(x.UniqueSenders, y.UniqueSenders);
			conversationType.UniqueUnreadSenders = ConversationHelper.MergeArray<string>(x.UniqueUnreadSenders, y.UniqueUnreadSenders);
			conversationType.UnreadCount = ConversationHelper.MergeInts(x.UnreadCount, y.UnreadCount);
			conversationType.HasClutter = x.HasClutter;
			return conversationType;
		}

		// Token: 0x06002C60 RID: 11360 RVA: 0x000B0A98 File Offset: 0x000AEC98
		internal static ConversationType LoadFromAggregatedConversation(IStorePropertyBag aggregatedProperties, MailboxSession session, PropertyUriEnum[] properties)
		{
			ConversationType conversationType = new ConversationType();
			AggregatedConversationLoader.LoadProperties(conversationType, aggregatedProperties, session, properties);
			return conversationType;
		}

		// Token: 0x06002C61 RID: 11361 RVA: 0x000B0AB5 File Offset: 0x000AECB5
		internal override void AddExtendedPropertyValue(ExtendedPropertyType extendedProperty)
		{
			throw new InvalidOperationException("Conversations don't have extended properties. This method should not be called.");
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x000B0AC1 File Offset: 0x000AECC1
		private void SetInstanceKey(byte[] value)
		{
			base.PropertyBag[ConversationSchema.InstanceKey] = value;
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06002C63 RID: 11363 RVA: 0x000B0AD4 File Offset: 0x000AECD4
		// (set) Token: 0x06002C64 RID: 11364 RVA: 0x000B0ADC File Offset: 0x000AECDC
		[XmlArrayItem("String", IsNullable = false)]
		[DataMember(EmitDefaultValue = false, Order = 40)]
		public string[] GlobalItemChangeKeys { get; set; }

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06002C65 RID: 11365 RVA: 0x000B0AE5 File Offset: 0x000AECE5
		// (set) Token: 0x06002C66 RID: 11366 RVA: 0x000B0AED File Offset: 0x000AECED
		[XmlArrayItem("Boolean", IsNullable = false)]
		[DataMember(EmitDefaultValue = false, Order = 41)]
		public bool[] GlobalItemReadFlags { get; set; }

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06002C67 RID: 11367 RVA: 0x000B0AF6 File Offset: 0x000AECF6
		// (set) Token: 0x06002C68 RID: 11368 RVA: 0x000B0B08 File Offset: 0x000AED08
		[IgnoreDataMember]
		[XmlIgnore]
		public object InternalInitialPost
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<object>(ConversationSchema.InitialPost);
			}
			set
			{
				base.PropertyBag[ConversationSchema.InitialPost] = value;
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06002C69 RID: 11369 RVA: 0x000B0B1B File Offset: 0x000AED1B
		// (set) Token: 0x06002C6A RID: 11370 RVA: 0x000B0B2D File Offset: 0x000AED2D
		[XmlIgnore]
		[IgnoreDataMember]
		public object InternalRecentReplys
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<object>(ConversationSchema.RecentReplys);
			}
			set
			{
				base.PropertyBag[ConversationSchema.RecentReplys] = value;
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06002C6B RID: 11371 RVA: 0x000B0B40 File Offset: 0x000AED40
		// (set) Token: 0x06002C6C RID: 11372 RVA: 0x000B0B4D File Offset: 0x000AED4D
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 42)]
		[XmlIgnore]
		[XmlElement("MeetingResponse", typeof(MeetingResponseMessageType))]
		[XmlElement("Message", typeof(MessageType))]
		[XmlElement("MeetingRequest", typeof(MeetingRequestMessageType))]
		[XmlElement("MeetingCancellation", typeof(MeetingCancellationMessageType))]
		[XmlElement("MeetingMessage", typeof(MeetingMessageType))]
		public MessageType InitialPost
		{
			get
			{
				return (MessageType)this.InternalInitialPost;
			}
			set
			{
				this.InternalInitialPost = value;
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06002C6D RID: 11373 RVA: 0x000B0B56 File Offset: 0x000AED56
		// (set) Token: 0x06002C6E RID: 11374 RVA: 0x000B0B63 File Offset: 0x000AED63
		[XmlIgnore]
		[DataMember(Name = "RecentReplys", EmitDefaultValue = false, IsRequired = false, Order = 43)]
		public MessageType[] RecentReplys
		{
			get
			{
				return (MessageType[])this.InternalRecentReplys;
			}
			set
			{
				this.InternalRecentReplys = value;
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06002C6F RID: 11375 RVA: 0x000B0B6C File Offset: 0x000AED6C
		// (set) Token: 0x06002C70 RID: 11376 RVA: 0x000B0B7E File Offset: 0x000AED7E
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, IsRequired = false, Order = 44)]
		public ItemId FamilyId
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<ItemId>(ConversationSchema.FamilyId);
			}
			set
			{
				base.PropertyBag[ConversationSchema.FamilyId] = value;
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06002C71 RID: 11377 RVA: 0x000B0B91 File Offset: 0x000AED91
		// (set) Token: 0x06002C72 RID: 11378 RVA: 0x000B0BA3 File Offset: 0x000AEDA3
		[DataMember(EmitDefaultValue = false, Order = 45)]
		[XmlArrayItem("Int16", IsNullable = false)]
		public short[] GlobalRichContent
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<short[]>(ConversationSchema.GlobalRichContent);
			}
			set
			{
				base.PropertyBag[ConversationSchema.GlobalRichContent] = value;
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06002C73 RID: 11379 RVA: 0x000B0BB6 File Offset: 0x000AEDB6
		// (set) Token: 0x06002C74 RID: 11380 RVA: 0x000B0BC3 File Offset: 0x000AEDC3
		[XmlIgnore]
		[DateTimeString]
		[DataMember(EmitDefaultValue = false, Order = 46)]
		public string LastDeliveryOrRenewTime
		{
			get
			{
				return base.GetValueOrDefault<string>(ConversationSchema.LastDeliveryOrRenewTime);
			}
			set
			{
				this[ConversationSchema.LastDeliveryOrRenewTime] = value;
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06002C75 RID: 11381 RVA: 0x000B0BD1 File Offset: 0x000AEDD1
		// (set) Token: 0x06002C76 RID: 11382 RVA: 0x000B0BDE File Offset: 0x000AEDDE
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, Order = 47)]
		[DateTimeString]
		public string GlobalLastDeliveryOrRenewTime
		{
			get
			{
				return base.GetValueOrDefault<string>(ConversationSchema.GlobalLastDeliveryOrRenewTime);
			}
			set
			{
				this[ConversationSchema.GlobalLastDeliveryOrRenewTime] = value;
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06002C77 RID: 11383 RVA: 0x000B0BEC File Offset: 0x000AEDEC
		// (set) Token: 0x06002C78 RID: 11384 RVA: 0x000B0BFE File Offset: 0x000AEDFE
		[DataMember(EmitDefaultValue = false, Order = 48)]
		[XmlIgnore]
		public string WorkingSetSourcePartition
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(ConversationSchema.WorkingSetSourcePartition);
			}
			set
			{
				base.PropertyBag[ConversationSchema.WorkingSetSourcePartition] = value;
			}
		}

		// Token: 0x06002C79 RID: 11385 RVA: 0x000B0C14 File Offset: 0x000AEE14
		internal void BulkAssignProperties(PropertyDefinition[] propertyDefinitions, object[] propertyValues, Guid mailboxGuid, ExTimeZone timeZone = null)
		{
			this.ConversationId = new ItemId(IdConverter.ConversationIdToEwsId(mailboxGuid, this.GetItemProperty<ConversationId>(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationId)), null);
			this.ConversationTopic = this.GetItemProperty<string>(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationTopic);
			this.UniqueRecipients = this.GetItemProperty<string[]>(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationMVTo);
			this.GlobalUniqueRecipients = this.GetItemProperty<string[]>(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationGlobalMVTo);
			this.UniqueSenders = this.GetItemProperty<string[]>(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationMVFrom);
			this.GlobalUniqueSenders = this.GetItemProperty<string[]>(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationGlobalMVFrom);
			this.UniqueUnreadSenders = this.GetItemProperty<string[]>(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationMVUnreadFrom);
			this.GlobalUniqueUnreadSenders = this.GetItemProperty<string[]>(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationGlobalMVUnreadFrom);
			this.LastDeliveryTime = this.GetDateTimeProperty(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationLastDeliveryTime, timeZone);
			this.GlobalLastDeliveryTime = this.GetDateTimeProperty(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationGlobalLastDeliveryTime, timeZone);
			this.LastDeliveryOrRenewTime = this.GetDateTimeProperty(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationLastDeliveryOrRenewTime, timeZone);
			this.GlobalLastDeliveryOrRenewTime = this.GetDateTimeProperty(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationGlobalLastDeliveryOrRenewTime, timeZone);
			this.Categories = this.GetItemProperty<string[]>(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationCategories);
			FlagType flagType = new FlagType();
			if (this.IsPropertyDefined(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationFlagStatus))
			{
				flagType.FlagStatus = (FlagStatus)this.GetItemProperty<int>(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationFlagStatus, 0);
				this.FlagStatus = flagType.FlagStatus;
			}
			flagType = new FlagType();
			if (this.IsPropertyDefined(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationGlobalFlagStatus))
			{
				flagType.FlagStatus = (FlagStatus)this.GetItemProperty<int>(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationGlobalFlagStatus, 0);
				this.GlobalFlagStatus = flagType.FlagStatus;
			}
			this.HasAttachments = new bool?(this.GetItemProperty<bool>(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationHasAttach));
			this.GlobalHasAttachments = new bool?(this.GetItemProperty<bool>(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationGlobalHasAttach));
			this.HasIrm = new bool?(this.GetItemProperty<bool>(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationHasIrm));
			this.GlobalHasIrm = new bool?(this.GetItemProperty<bool>(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationGlobalHasIrm));
			this.MessageCount = new int?(this.GetItemProperty<int>(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationMessageCount));
			this.GlobalMessageCount = new int?(this.GetItemProperty<int>(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationGlobalMessageCount));
			this.UnreadCount = new int?(this.GetItemProperty<int>(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationUnreadMessageCount));
			this.GlobalUnreadCount = new int?(this.GetItemProperty<int>(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationGlobalUnreadMessageCount));
			this.Size = new int?(this.GetItemProperty<int>(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationMessageSize));
			this.GlobalSize = new int?(this.GetItemProperty<int>(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationGlobalMessageSize));
			this.ItemClasses = this.GetItemProperty<string[]>(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationMessageClasses);
			this.GlobalItemClasses = this.GetItemProperty<string[]>(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationGlobalMessageClasses);
			this.ImportanceString = ((ImportanceType)this.GetItemProperty<int>(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationImportance, 1)).ToString();
			this.GlobalImportanceString = ((ImportanceType)this.GetItemProperty<int>(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationGlobalImportance, 1)).ToString();
			this.GlobalRichContent = this.GetItemProperty<short[]>(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationGlobalRichContent);
			StoreId[] itemProperty = this.GetItemProperty<StoreId[]>(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationItemIds, new StoreId[0]);
			this.ItemIds = new ItemId[itemProperty.Length];
			for (int i = 0; i < itemProperty.Length; i++)
			{
				this.ItemIds[i] = new ItemId(this.GetEwsId(itemProperty[i], mailboxGuid), null);
			}
			StoreId[] itemProperty2 = this.GetItemProperty<StoreId[]>(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationGlobalItemIds, new StoreId[0]);
			this.GlobalItemIds = new ItemId[itemProperty2.Length];
			for (int j = 0; j < itemProperty2.Length; j++)
			{
				this.GlobalItemIds[j] = new ItemId(this.GetEwsId(itemProperty2[j], mailboxGuid), null);
			}
			this.LastModifiedTime = this.GetDateTimeProperty(propertyDefinitions, propertyValues, StoreObjectSchema.LastModifiedTime, timeZone);
			this.Preview = this.GetItemProperty<string>(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationPreview);
			this.MailboxScopeString = MailboxSearchLocation.PrimaryOnly.ToString();
			IconIndex itemProperty3 = (IconIndex)this.GetItemProperty<int>(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationReplyForwardState);
			if (itemProperty3 > (IconIndex)0)
			{
				this.IconIndexString = itemProperty3.ToString();
			}
			itemProperty3 = (IconIndex)this.GetItemProperty<int>(propertyDefinitions, propertyValues, ConversationItemSchema.ConversationGlobalReplyForwardState);
			if (itemProperty3 > (IconIndex)0)
			{
				this.GlobalIconIndexString = itemProperty3.ToString();
			}
		}

		// Token: 0x06002C7A RID: 11386 RVA: 0x000B1018 File Offset: 0x000AF218
		private T GetItemProperty<T>(PropertyDefinition[] propertyDefinitions, object[] propertyValues, PropertyDefinition propertyWanted)
		{
			return this.GetItemProperty<T>(propertyDefinitions, propertyValues, propertyWanted, default(T));
		}

		// Token: 0x06002C7B RID: 11387 RVA: 0x000B1038 File Offset: 0x000AF238
		private T GetItemProperty<T>(PropertyDefinition[] propertyDefinitions, object[] propertyValues, PropertyDefinition propertyWanted, T defaultValue)
		{
			if (!this.IsPropertyDefined(propertyDefinitions, propertyValues, propertyWanted))
			{
				return defaultValue;
			}
			object obj = propertyValues[Array.IndexOf<PropertyDefinition>(propertyDefinitions, propertyWanted)];
			if (!(obj is T))
			{
				return defaultValue;
			}
			return (T)((object)obj);
		}

		// Token: 0x06002C7C RID: 11388 RVA: 0x000B1070 File Offset: 0x000AF270
		private bool IsPropertyDefined(PropertyDefinition[] propertyDefinitions, object[] propertyValues, PropertyDefinition propertyWanted)
		{
			int num = Array.IndexOf<PropertyDefinition>(propertyDefinitions, propertyWanted);
			return num >= 0 && num < propertyDefinitions.Length && propertyValues[num] != null && !(propertyValues[num] is PropertyError);
		}

		// Token: 0x06002C7D RID: 11389 RVA: 0x000B10A8 File Offset: 0x000AF2A8
		private string GetDateTimeProperty(PropertyDefinition[] propertyDefinitions, object[] propertyValues, PropertyDefinition propertyWanted, ExTimeZone timeZone)
		{
			ExDateTime itemProperty = this.GetItemProperty<ExDateTime>(propertyDefinitions, propertyValues, propertyWanted, ExDateTime.MinValue);
			if (ExDateTime.MinValue.Equals(itemProperty))
			{
				return null;
			}
			ExTimeZone exTimeZone = (timeZone == null || timeZone == ExTimeZone.UnspecifiedTimeZone) ? itemProperty.TimeZone : timeZone;
			if (exTimeZone == ExTimeZone.UtcTimeZone)
			{
				return ExDateTimeConverter.ToUtcXsdDateTime(itemProperty);
			}
			return ExDateTimeConverter.ToOffsetXsdDateTime(itemProperty, exTimeZone);
		}

		// Token: 0x06002C7E RID: 11390 RVA: 0x000B1105 File Offset: 0x000AF305
		private string GetEwsId(StoreId storeId, Guid mailboxGuid)
		{
			if (storeId == null)
			{
				return null;
			}
			return StoreId.StoreIdToEwsId(mailboxGuid, storeId);
		}

		// Token: 0x04001A67 RID: 6759
		private MailboxSearchLocation mailboxScope;
	}
}
