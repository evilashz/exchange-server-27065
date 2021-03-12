using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005D4 RID: 1492
	[KnownType(typeof(BaseObjectChangedEventType))]
	[KnownType(typeof(ModifiedEventType))]
	[XmlInclude(typeof(MovedCopiedEventType))]
	[XmlInclude(typeof(ModifiedEventType))]
	[KnownType(typeof(MovedCopiedEventType))]
	[XmlInclude(typeof(BaseObjectChangedEventType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Name = "BaseNotificationEvent", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class BaseNotificationEventType
	{
		// Token: 0x06002CE7 RID: 11495 RVA: 0x000B1A7D File Offset: 0x000AFC7D
		public BaseNotificationEventType()
		{
			this.notificationType = NotificationTypeEnum.StatusEvent;
		}

		// Token: 0x06002CE8 RID: 11496 RVA: 0x000B1A8C File Offset: 0x000AFC8C
		public BaseNotificationEventType(NotificationTypeEnum notificationType)
		{
			this.notificationType = notificationType;
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06002CE9 RID: 11497 RVA: 0x000B1A9B File Offset: 0x000AFC9B
		// (set) Token: 0x06002CEA RID: 11498 RVA: 0x000B1AA3 File Offset: 0x000AFCA3
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public string Watermark { get; set; }

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06002CEB RID: 11499 RVA: 0x000B1AAC File Offset: 0x000AFCAC
		[IgnoreDataMember]
		[XmlIgnore]
		public NotificationTypeEnum NotificationType
		{
			get
			{
				return this.notificationType;
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06002CEC RID: 11500 RVA: 0x000B1AB4 File Offset: 0x000AFCB4
		// (set) Token: 0x06002CED RID: 11501 RVA: 0x000B1AC1 File Offset: 0x000AFCC1
		[DataMember(Name = "NotificationType", IsRequired = true, Order = 1)]
		[XmlIgnore]
		public string NotificationTypeString
		{
			get
			{
				return EnumUtilities.ToString<NotificationTypeEnum>(this.notificationType);
			}
			set
			{
				this.notificationType = EnumUtilities.Parse<NotificationTypeEnum>(value);
			}
		}

		// Token: 0x04001B07 RID: 6919
		private NotificationTypeEnum notificationType;
	}
}
