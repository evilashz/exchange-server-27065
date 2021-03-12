using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000482 RID: 1154
	[XmlInclude(typeof(PullSubscriptionRequest))]
	[KnownType(typeof(PullSubscriptionRequest))]
	[KnownType(typeof(PushSubscriptionRequest))]
	[KnownType(typeof(StreamingSubscriptionRequest))]
	[XmlInclude(typeof(PushSubscriptionRequest))]
	[XmlInclude(typeof(StreamingSubscriptionRequest))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("BaseSubscriptionRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public abstract class SubscriptionRequestBase
	{
		// Token: 0x06002240 RID: 8768 RVA: 0x000A2D93 File Offset: 0x000A0F93
		public SubscriptionRequestBase()
		{
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06002241 RID: 8769 RVA: 0x000A2D9B File Offset: 0x000A0F9B
		// (set) Token: 0x06002242 RID: 8770 RVA: 0x000A2DA3 File Offset: 0x000A0FA3
		[DataMember(Name = "FolderIds", IsRequired = false, Order = 1)]
		[XmlArrayItem("DistinguishedFolderId", typeof(DistinguishedFolderId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("FolderId", typeof(FolderId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArray("FolderIds")]
		public BaseFolderId[] FolderIds { get; set; }

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06002243 RID: 8771 RVA: 0x000A2DAC File Offset: 0x000A0FAC
		// (set) Token: 0x06002244 RID: 8772 RVA: 0x000A2DB4 File Offset: 0x000A0FB4
		[DataMember(Name = "SubscribeToAllFolders", IsRequired = false, Order = 2)]
		[XmlAttribute("SubscribeToAllFolders", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public bool SubscribeToAllFolders { get; set; }

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06002245 RID: 8773 RVA: 0x000A2DBD File Offset: 0x000A0FBD
		// (set) Token: 0x06002246 RID: 8774 RVA: 0x000A2DC5 File Offset: 0x000A0FC5
		[XmlArrayItem("EventType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(EventType))]
		[IgnoreDataMember]
		[XmlArray("EventTypes", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public EventType[] EventTypes { get; set; }

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06002247 RID: 8775 RVA: 0x000A2DD0 File Offset: 0x000A0FD0
		// (set) Token: 0x06002248 RID: 8776 RVA: 0x000A2E1C File Offset: 0x000A101C
		[DataMember(Name = "EventTypes", IsRequired = true, Order = 3)]
		[XmlIgnore]
		public string[] EventTypesString
		{
			get
			{
				if (this.EventTypes == null)
				{
					return null;
				}
				string[] array = new string[this.EventTypes.Length];
				for (int i = 0; i < this.EventTypes.Length; i++)
				{
					array[i] = EnumUtilities.ToString<EventType>(this.EventTypes[i]);
				}
				return array;
			}
			set
			{
				if (value == null)
				{
					this.EventTypes = null;
					return;
				}
				this.EventTypes = new EventType[value.Length];
				for (int i = 0; i < this.EventTypes.Length; i++)
				{
					this.EventTypes[i] = EnumUtilities.Parse<EventType>(value[i]);
				}
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06002249 RID: 8777 RVA: 0x000A2E65 File Offset: 0x000A1065
		// (set) Token: 0x0600224A RID: 8778 RVA: 0x000A2E6D File Offset: 0x000A106D
		[DataMember(Name = "Watermark", IsRequired = false, Order = 4)]
		[XmlElement("Watermark", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public string Watermark { get; set; }
	}
}
