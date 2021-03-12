using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200047D RID: 1149
	[XmlType(TypeName = "SetHoldOnMailboxesType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Name = "SetHoldOnMailboxesRequest", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class SetHoldOnMailboxesRequest : BaseRequest
	{
		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x0600220A RID: 8714 RVA: 0x000A2BC5 File Offset: 0x000A0DC5
		// (set) Token: 0x0600220B RID: 8715 RVA: 0x000A2BCD File Offset: 0x000A0DCD
		[XmlElement("ActionType")]
		[IgnoreDataMember]
		public HoldAction ActionType
		{
			get
			{
				return this.actionType;
			}
			set
			{
				this.actionType = value;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x0600220C RID: 8716 RVA: 0x000A2BD6 File Offset: 0x000A0DD6
		// (set) Token: 0x0600220D RID: 8717 RVA: 0x000A2BE3 File Offset: 0x000A0DE3
		[XmlIgnore]
		[DataMember(Name = "ActionType", IsRequired = true)]
		public string ActionTypeString
		{
			get
			{
				return EnumUtilities.ToString<HoldAction>(this.actionType);
			}
			set
			{
				this.actionType = EnumUtilities.Parse<HoldAction>(value);
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x0600220E RID: 8718 RVA: 0x000A2BF1 File Offset: 0x000A0DF1
		// (set) Token: 0x0600220F RID: 8719 RVA: 0x000A2BF9 File Offset: 0x000A0DF9
		[XmlElement("HoldId")]
		[DataMember(Name = "HoldId", IsRequired = true)]
		public string HoldId
		{
			get
			{
				return this.holdId;
			}
			set
			{
				this.holdId = value;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06002210 RID: 8720 RVA: 0x000A2C02 File Offset: 0x000A0E02
		// (set) Token: 0x06002211 RID: 8721 RVA: 0x000A2C0A File Offset: 0x000A0E0A
		[XmlElement("Query")]
		[DataMember(Name = "Query", IsRequired = true)]
		public string Query
		{
			get
			{
				return this.query;
			}
			set
			{
				this.query = value;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06002212 RID: 8722 RVA: 0x000A2C13 File Offset: 0x000A0E13
		// (set) Token: 0x06002213 RID: 8723 RVA: 0x000A2C1B File Offset: 0x000A0E1B
		[XmlArray(ElementName = "Mailboxes", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[XmlArrayItem(ElementName = "String", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(string))]
		[DataMember(Name = "Mailboxes", IsRequired = true)]
		public string[] Mailboxes
		{
			get
			{
				return this.mailboxes;
			}
			set
			{
				this.mailboxes = value;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06002214 RID: 8724 RVA: 0x000A2C24 File Offset: 0x000A0E24
		// (set) Token: 0x06002215 RID: 8725 RVA: 0x000A2C2C File Offset: 0x000A0E2C
		[XmlElement("Language")]
		[DataMember(Name = "Language", IsRequired = false)]
		public string Language
		{
			get
			{
				return this.language;
			}
			set
			{
				this.language = value;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06002216 RID: 8726 RVA: 0x000A2C35 File Offset: 0x000A0E35
		// (set) Token: 0x06002217 RID: 8727 RVA: 0x000A2C3D File Offset: 0x000A0E3D
		[DataMember(Name = "IncludeNonIndexableItems", IsRequired = false)]
		[XmlElement("IncludeNonIndexableItems")]
		public bool IncludeNonIndexableItems
		{
			get
			{
				return this.includeNonIndexableItems;
			}
			set
			{
				this.includeNonIndexableItems = value;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06002218 RID: 8728 RVA: 0x000A2C46 File Offset: 0x000A0E46
		// (set) Token: 0x06002219 RID: 8729 RVA: 0x000A2C4E File Offset: 0x000A0E4E
		[DataMember(Name = "Deduplication", IsRequired = false)]
		[XmlElement("Deduplication")]
		public bool Deduplication
		{
			get
			{
				return this.deduplication;
			}
			set
			{
				this.deduplication = value;
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x0600221A RID: 8730 RVA: 0x000A2C57 File Offset: 0x000A0E57
		// (set) Token: 0x0600221B RID: 8731 RVA: 0x000A2C5F File Offset: 0x000A0E5F
		[DataMember(Name = "InPlaceHoldIdentity", IsRequired = false)]
		[XmlElement("InPlaceHoldIdentity")]
		public string InPlaceHoldIdentity
		{
			get
			{
				return this.inPlaceHoldIdentity;
			}
			set
			{
				this.inPlaceHoldIdentity = value;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x0600221C RID: 8732 RVA: 0x000A2C68 File Offset: 0x000A0E68
		// (set) Token: 0x0600221D RID: 8733 RVA: 0x000A2C70 File Offset: 0x000A0E70
		[DataMember(Name = "ItemHoldPeriod", IsRequired = false)]
		[XmlElement("ItemHoldPeriod")]
		public string ItemHoldPeriod
		{
			get
			{
				return this.itemHoldPeriod;
			}
			set
			{
				this.itemHoldPeriod = value;
			}
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x000A2C79 File Offset: 0x000A0E79
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new SetHoldOnMailboxes(callContext, this);
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x000A2C82 File Offset: 0x000A0E82
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06002220 RID: 8736 RVA: 0x000A2C85 File Offset: 0x000A0E85
		internal override ResourceKey[] GetResources(CallContext callContext, int currentStep)
		{
			return null;
		}

		// Token: 0x040014CE RID: 5326
		private HoldAction actionType;

		// Token: 0x040014CF RID: 5327
		private string holdId;

		// Token: 0x040014D0 RID: 5328
		private string query;

		// Token: 0x040014D1 RID: 5329
		private string[] mailboxes;

		// Token: 0x040014D2 RID: 5330
		private string language;

		// Token: 0x040014D3 RID: 5331
		private bool includeNonIndexableItems;

		// Token: 0x040014D4 RID: 5332
		private bool deduplication;

		// Token: 0x040014D5 RID: 5333
		private string inPlaceHoldIdentity;

		// Token: 0x040014D6 RID: 5334
		private string itemHoldPeriod;
	}
}
