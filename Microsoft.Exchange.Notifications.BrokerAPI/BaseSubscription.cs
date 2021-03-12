using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200001D RID: 29
	[KnownType(typeof(NewMailSubscription))]
	[XmlInclude(typeof(RowSubscription))]
	[KnownType(typeof(RowSubscription))]
	[XmlInclude(typeof(UnseenCountSubscription))]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlInclude(typeof(NewMailSubscription))]
	[KnownType(typeof(UnseenCountSubscription))]
	public abstract class BaseSubscription
	{
		// Token: 0x06000095 RID: 149 RVA: 0x000033E7 File Offset: 0x000015E7
		protected BaseSubscription(NotificationType notificationType)
		{
			this.NotificationType = notificationType;
			this.CultureInfo = CultureInfo.InvariantCulture;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003401 File Offset: 0x00001601
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00003409 File Offset: 0x00001609
		[DataMember(EmitDefaultValue = false)]
		public NotificationType NotificationType { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003412 File Offset: 0x00001612
		// (set) Token: 0x06000099 RID: 153 RVA: 0x0000341A File Offset: 0x0000161A
		[DataMember(EmitDefaultValue = false)]
		public string ConsumerSubscriptionId { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003423 File Offset: 0x00001623
		// (set) Token: 0x0600009B RID: 155 RVA: 0x0000342B File Offset: 0x0000162B
		[IgnoreDataMember]
		[XmlIgnore]
		public CultureInfo CultureInfo { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00003434 File Offset: 0x00001634
		// (set) Token: 0x0600009D RID: 157 RVA: 0x0000344B File Offset: 0x0000164B
		[DataMember(EmitDefaultValue = false, Name = "Culture")]
		[XmlElement("Culture")]
		public string CultureInfoForSerialization
		{
			get
			{
				if (this.CultureInfo != null)
				{
					return this.CultureInfo.Name;
				}
				return null;
			}
			set
			{
				this.CultureInfo = (string.IsNullOrEmpty(value) ? CultureInfo.InvariantCulture : new CultureInfo(value));
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00003468 File Offset: 0x00001668
		[IgnoreDataMember]
		[XmlIgnore]
		public bool IsValid
		{
			get
			{
				return this.Validate();
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003470 File Offset: 0x00001670
		[IgnoreDataMember]
		[XmlIgnore]
		public virtual IEnumerable<Tuple<string, object>> Differentiators
		{
			get
			{
				return new Tuple<string, object>[]
				{
					new Tuple<string, object>("NT", this.NotificationType)
				};
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000349D File Offset: 0x0000169D
		protected virtual bool Validate()
		{
			return Enum.IsDefined(typeof(NotificationType), this.NotificationType) && this.CultureInfo != null;
		}
	}
}
