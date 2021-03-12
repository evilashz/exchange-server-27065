using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005D7 RID: 1495
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Name = "ModifiedEvent", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class ModifiedEventType : BaseObjectChangedEventType
	{
		// Token: 0x06002D04 RID: 11524 RVA: 0x000B1B9E File Offset: 0x000AFD9E
		public ModifiedEventType() : base(NotificationTypeEnum.ModifiedEvent)
		{
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06002D05 RID: 11525 RVA: 0x000B1BA7 File Offset: 0x000AFDA7
		// (set) Token: 0x06002D06 RID: 11526 RVA: 0x000B1BAF File Offset: 0x000AFDAF
		[DataMember(EmitDefaultValue = false)]
		public int UnreadCount
		{
			get
			{
				return this.unreadCount;
			}
			set
			{
				this.UnreadCountSpecified = true;
				this.unreadCount = value;
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06002D07 RID: 11527 RVA: 0x000B1BBF File Offset: 0x000AFDBF
		// (set) Token: 0x06002D08 RID: 11528 RVA: 0x000B1BC7 File Offset: 0x000AFDC7
		[XmlIgnore]
		[IgnoreDataMember]
		public bool UnreadCountSpecified { get; set; }

		// Token: 0x04001B0E RID: 6926
		private int unreadCount;
	}
}
