using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000169 RID: 361
	internal class MessageTrace : MessageTraceEntityBase, IExtendedPropertyStore<MessageProperty>, IComparable<MessageTrace>
	{
		// Token: 0x06000E50 RID: 3664 RVA: 0x0002A438 File Offset: 0x00028638
		public MessageTrace()
		{
			this.extendedProperties = new ExtendedPropertyStore<MessageProperty>();
			this.ExMessageId = IdGenerator.GenerateIdentifier(IdScope.MessageTrace);
			this.ClientMessageId = Guid.NewGuid().ToString();
			this.SetReceivedTime(this.ExMessageId);
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x0002A487 File Offset: 0x00028687
		public override ObjectId Identity
		{
			get
			{
				return new MessageTraceObjectId(this.OrganizationalUnitRoot, this.ExMessageId);
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000E52 RID: 3666 RVA: 0x0002A49A File Offset: 0x0002869A
		// (set) Token: 0x06000E53 RID: 3667 RVA: 0x0002A4AC File Offset: 0x000286AC
		public Guid ExMessageId
		{
			get
			{
				return (Guid)this[MessageTraceSchema.ExMessageIdProperty];
			}
			set
			{
				this[MessageTraceSchema.ExMessageIdProperty] = value;
				this.SetReceivedTime(value);
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000E54 RID: 3668 RVA: 0x0002A4C6 File Offset: 0x000286C6
		// (set) Token: 0x06000E55 RID: 3669 RVA: 0x0002A4D8 File Offset: 0x000286D8
		public Guid OrganizationalUnitRoot
		{
			get
			{
				return (Guid)this[MessageTraceSchema.OrganizationalUnitRootProperty];
			}
			set
			{
				this[MessageTraceSchema.OrganizationalUnitRootProperty] = value;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x0002A4EB File Offset: 0x000286EB
		// (set) Token: 0x06000E57 RID: 3671 RVA: 0x0002A4FD File Offset: 0x000286FD
		public string ClientMessageId
		{
			get
			{
				return (string)this[MessageTraceSchema.ClientMessageIdProperty];
			}
			set
			{
				this[MessageTraceSchema.ClientMessageIdProperty] = value;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x0002A50B File Offset: 0x0002870B
		// (set) Token: 0x06000E59 RID: 3673 RVA: 0x0002A51D File Offset: 0x0002871D
		public MailDirection Direction
		{
			get
			{
				return (MailDirection)this[MessageTraceSchema.DirectionProperty];
			}
			set
			{
				this[MessageTraceSchema.DirectionProperty] = value;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000E5A RID: 3674 RVA: 0x0002A530 File Offset: 0x00028730
		// (set) Token: 0x06000E5B RID: 3675 RVA: 0x0002A542 File Offset: 0x00028742
		public string FromEmailPrefix
		{
			get
			{
				return (string)this[MessageTraceSchema.FromEmailPrefixProperty];
			}
			set
			{
				this[MessageTraceSchema.FromEmailPrefixProperty] = value;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000E5C RID: 3676 RVA: 0x0002A550 File Offset: 0x00028750
		// (set) Token: 0x06000E5D RID: 3677 RVA: 0x0002A562 File Offset: 0x00028762
		public string FromEmailDomain
		{
			get
			{
				return (string)this[MessageTraceSchema.FromEmailDomainProperty];
			}
			set
			{
				this[MessageTraceSchema.FromEmailDomainProperty] = value;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000E5E RID: 3678 RVA: 0x0002A570 File Offset: 0x00028770
		// (set) Token: 0x06000E5F RID: 3679 RVA: 0x0002A582 File Offset: 0x00028782
		public IPAddress IPAddress
		{
			get
			{
				return (IPAddress)this[MessageTraceSchema.IPAddressProperty];
			}
			set
			{
				this[MessageTraceSchema.IPAddressProperty] = value;
				this.SetIPOctetProperties();
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000E60 RID: 3680 RVA: 0x0002A596 File Offset: 0x00028796
		// (set) Token: 0x06000E61 RID: 3681 RVA: 0x0002A5A8 File Offset: 0x000287A8
		public byte? IP1
		{
			get
			{
				return (byte?)this[MessageTraceSchema.IP1Property];
			}
			private set
			{
				this[MessageTraceSchema.IP1Property] = value;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000E62 RID: 3682 RVA: 0x0002A5BB File Offset: 0x000287BB
		// (set) Token: 0x06000E63 RID: 3683 RVA: 0x0002A5CD File Offset: 0x000287CD
		public byte? IP2
		{
			get
			{
				return (byte?)this[MessageTraceSchema.IP2Property];
			}
			private set
			{
				this[MessageTraceSchema.IP2Property] = value;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x0002A5E0 File Offset: 0x000287E0
		// (set) Token: 0x06000E65 RID: 3685 RVA: 0x0002A5F2 File Offset: 0x000287F2
		public byte? IP3
		{
			get
			{
				return (byte?)this[MessageTraceSchema.IP3Property];
			}
			private set
			{
				this[MessageTraceSchema.IP3Property] = value;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x0002A605 File Offset: 0x00028805
		// (set) Token: 0x06000E67 RID: 3687 RVA: 0x0002A617 File Offset: 0x00028817
		public byte? IP4
		{
			get
			{
				return (byte?)this[MessageTraceSchema.IP4Property];
			}
			private set
			{
				this[MessageTraceSchema.IP4Property] = value;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x0002A62A File Offset: 0x0002882A
		// (set) Token: 0x06000E69 RID: 3689 RVA: 0x0002A63C File Offset: 0x0002883C
		public byte? IP5
		{
			get
			{
				return (byte?)this[MessageTraceSchema.IP5Property];
			}
			private set
			{
				this[MessageTraceSchema.IP5Property] = value;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x0002A64F File Offset: 0x0002884F
		// (set) Token: 0x06000E6B RID: 3691 RVA: 0x0002A661 File Offset: 0x00028861
		public byte? IP6
		{
			get
			{
				return (byte?)this[MessageTraceSchema.IP6Property];
			}
			private set
			{
				this[MessageTraceSchema.IP6Property] = value;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x0002A674 File Offset: 0x00028874
		// (set) Token: 0x06000E6D RID: 3693 RVA: 0x0002A686 File Offset: 0x00028886
		public byte? IP7
		{
			get
			{
				return (byte?)this[MessageTraceSchema.IP7Property];
			}
			private set
			{
				this[MessageTraceSchema.IP7Property] = value;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x0002A699 File Offset: 0x00028899
		// (set) Token: 0x06000E6F RID: 3695 RVA: 0x0002A6AB File Offset: 0x000288AB
		public byte? IP8
		{
			get
			{
				return (byte?)this[MessageTraceSchema.IP8Property];
			}
			private set
			{
				this[MessageTraceSchema.IP8Property] = value;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x0002A6BE File Offset: 0x000288BE
		// (set) Token: 0x06000E71 RID: 3697 RVA: 0x0002A6D0 File Offset: 0x000288D0
		public byte? IP9
		{
			get
			{
				return (byte?)this[MessageTraceSchema.IP9Property];
			}
			private set
			{
				this[MessageTraceSchema.IP9Property] = value;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x0002A6E3 File Offset: 0x000288E3
		// (set) Token: 0x06000E73 RID: 3699 RVA: 0x0002A6F5 File Offset: 0x000288F5
		public byte? IP10
		{
			get
			{
				return (byte?)this[MessageTraceSchema.IP10Property];
			}
			private set
			{
				this[MessageTraceSchema.IP10Property] = value;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x0002A708 File Offset: 0x00028908
		// (set) Token: 0x06000E75 RID: 3701 RVA: 0x0002A71A File Offset: 0x0002891A
		public byte? IP11
		{
			get
			{
				return (byte?)this[MessageTraceSchema.IP11Property];
			}
			private set
			{
				this[MessageTraceSchema.IP11Property] = value;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000E76 RID: 3702 RVA: 0x0002A72D File Offset: 0x0002892D
		// (set) Token: 0x06000E77 RID: 3703 RVA: 0x0002A73F File Offset: 0x0002893F
		public byte? IP12
		{
			get
			{
				return (byte?)this[MessageTraceSchema.IP12Property];
			}
			private set
			{
				this[MessageTraceSchema.IP12Property] = value;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000E78 RID: 3704 RVA: 0x0002A752 File Offset: 0x00028952
		// (set) Token: 0x06000E79 RID: 3705 RVA: 0x0002A764 File Offset: 0x00028964
		public byte? IP13
		{
			get
			{
				return (byte?)this[MessageTraceSchema.IP13Property];
			}
			private set
			{
				this[MessageTraceSchema.IP13Property] = value;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x0002A777 File Offset: 0x00028977
		// (set) Token: 0x06000E7B RID: 3707 RVA: 0x0002A789 File Offset: 0x00028989
		public byte? IP14
		{
			get
			{
				return (byte?)this[MessageTraceSchema.IP14Property];
			}
			private set
			{
				this[MessageTraceSchema.IP14Property] = value;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000E7C RID: 3708 RVA: 0x0002A79C File Offset: 0x0002899C
		// (set) Token: 0x06000E7D RID: 3709 RVA: 0x0002A7AE File Offset: 0x000289AE
		public byte? IP15
		{
			get
			{
				return (byte?)this[MessageTraceSchema.IP15Property];
			}
			private set
			{
				this[MessageTraceSchema.IP15Property] = value;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x0002A7C1 File Offset: 0x000289C1
		// (set) Token: 0x06000E7F RID: 3711 RVA: 0x0002A7D3 File Offset: 0x000289D3
		public byte? IP16
		{
			get
			{
				return (byte?)this[MessageTraceSchema.IP16Property];
			}
			private set
			{
				this[MessageTraceSchema.IP16Property] = value;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x0002A7E8 File Offset: 0x000289E8
		public List<MessageRecipient> Recipients
		{
			get
			{
				List<MessageRecipient> result;
				if ((result = this.msgRecipients) == null)
				{
					result = (this.msgRecipients = new List<MessageRecipient>());
				}
				return result;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000E81 RID: 3713 RVA: 0x0002A810 File Offset: 0x00028A10
		public List<MessageEvent> Events
		{
			get
			{
				List<MessageEvent> result;
				if ((result = this.msgEvents) == null)
				{
					result = (this.msgEvents = new List<MessageEvent>());
				}
				return result;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000E82 RID: 3714 RVA: 0x0002A838 File Offset: 0x00028A38
		public List<MessageClassification> Classifications
		{
			get
			{
				List<MessageClassification> result;
				if ((result = this.msgClassifications) == null)
				{
					result = (this.msgClassifications = new List<MessageClassification>());
				}
				return result;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000E83 RID: 3715 RVA: 0x0002A860 File Offset: 0x00028A60
		public List<MessageClientInformation> ClientInformation
		{
			get
			{
				List<MessageClientInformation> result;
				if ((result = this.msgClientInformation) == null)
				{
					result = (this.msgClientInformation = new List<MessageClientInformation>());
				}
				return result;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000E84 RID: 3716 RVA: 0x0002A885 File Offset: 0x00028A85
		public int ExtendedPropertiesCount
		{
			get
			{
				return this.extendedProperties.ExtendedPropertiesCount;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x0002A892 File Offset: 0x00028A92
		// (set) Token: 0x06000E86 RID: 3718 RVA: 0x0002A8A4 File Offset: 0x00028AA4
		public DateTime ReceivedTime
		{
			get
			{
				return (DateTime)this[MessageTraceSchema.ReceivedTimeProperty];
			}
			private set
			{
				this[MessageTraceSchema.ReceivedTimeProperty] = value;
			}
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x0002A8B7 File Offset: 0x00028AB7
		public bool TryGetExtendedProperty(string nameSpace, string name, out MessageProperty extendedProperty)
		{
			return this.extendedProperties.TryGetExtendedProperty(nameSpace, name, out extendedProperty);
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0002A8C7 File Offset: 0x00028AC7
		public MessageProperty GetExtendedProperty(string nameSpace, string name)
		{
			return this.extendedProperties.GetExtendedProperty(nameSpace, name);
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x0002A8D6 File Offset: 0x00028AD6
		public IEnumerable<MessageProperty> GetExtendedPropertiesEnumerable()
		{
			return this.extendedProperties.GetExtendedPropertiesEnumerable();
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0002A8E3 File Offset: 0x00028AE3
		public void AddExtendedProperty(MessageProperty extendedProperty)
		{
			this.extendedProperties.AddExtendedProperty(extendedProperty);
			extendedProperty.ExMessageId = this.ExMessageId;
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x0002A900 File Offset: 0x00028B00
		public override void Accept(IMessageTraceVisitor visitor)
		{
			visitor.Visit(this);
			foreach (MessageProperty messageProperty in this.GetExtendedPropertiesEnumerable())
			{
				messageProperty.Accept(visitor);
			}
			if (this.msgRecipients != null)
			{
				foreach (MessageRecipient messageRecipient in this.msgRecipients)
				{
					messageRecipient.Accept(visitor);
				}
			}
			if (this.msgEvents != null)
			{
				foreach (MessageEvent messageEvent in this.msgEvents)
				{
					messageEvent.Accept(visitor);
				}
			}
			if (this.msgClassifications != null)
			{
				foreach (MessageClassification messageClassification in this.msgClassifications)
				{
					messageClassification.Accept(visitor);
				}
			}
			if (this.msgClientInformation != null)
			{
				foreach (MessageClientInformation messageClientInformation in this.msgClientInformation)
				{
					messageClientInformation.Accept(visitor);
				}
			}
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x0002AA88 File Offset: 0x00028C88
		public void Add(MessageRecipient recipient)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			recipient.ExMessageId = this.ExMessageId;
			this.Recipients.Add(recipient);
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x0002AAB0 File Offset: 0x00028CB0
		public void Add(MessageEvent msgEvent)
		{
			if (msgEvent == null)
			{
				throw new ArgumentNullException("msgEvent");
			}
			msgEvent.ExMessageId = this.ExMessageId;
			this.Events.Add(msgEvent);
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0002AAD8 File Offset: 0x00028CD8
		public void Add(MessageClassification msgClassification)
		{
			if (msgClassification == null)
			{
				throw new ArgumentNullException("msgClassification");
			}
			msgClassification.ExMessageId = this.ExMessageId;
			this.Classifications.Add(msgClassification);
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0002AB00 File Offset: 0x00028D00
		public void Add(MessageClientInformation msgClientInformation)
		{
			if (msgClientInformation == null)
			{
				throw new ArgumentNullException("msgClientInformation");
			}
			msgClientInformation.ExMessageId = this.ExMessageId;
			this.ClientInformation.Add(msgClientInformation);
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0002AB28 File Offset: 0x00028D28
		public override Type GetSchemaType()
		{
			return typeof(MessageTraceSchema);
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0002AB34 File Offset: 0x00028D34
		public override HygienePropertyDefinition[] GetAllProperties()
		{
			return MessageTrace.Properties;
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x0002AB3C File Offset: 0x00028D3C
		public int CompareTo(MessageTrace other)
		{
			if (other == null)
			{
				return 1;
			}
			int num = 0;
			byte[] array = this.ExMessageId.ToByteArray();
			byte[] array2 = other.ExMessageId.ToByteArray();
			int num2 = 10;
			while (num == 0 && num2 < 16)
			{
				num = array[num2].CompareTo(array2[num2]);
				num2++;
			}
			return num;
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0002AB94 File Offset: 0x00028D94
		private void SetIPOctetProperties()
		{
			if (this.IPAddress == null)
			{
				this.IP1 = (this.IP2 = (this.IP3 = (this.IP4 = (this.IP5 = (this.IP6 = (this.IP7 = (this.IP8 = (this.IP9 = (this.IP10 = (this.IP11 = (this.IP12 = (this.IP13 = (this.IP14 = (this.IP15 = (this.IP16 = null)))))))))))))));
				return;
			}
			byte[] addressBytes = this.IPAddress.GetAddressBytes();
			if (this.IPAddress.AddressFamily == AddressFamily.InterNetwork)
			{
				this.IP1 = new byte?(addressBytes[0]);
				this.IP2 = new byte?(addressBytes[1]);
				this.IP3 = new byte?(addressBytes[2]);
				this.IP4 = new byte?(addressBytes[3]);
				this.IP5 = (this.IP6 = (this.IP7 = (this.IP8 = (this.IP9 = (this.IP10 = (this.IP11 = (this.IP12 = (this.IP13 = (this.IP14 = (this.IP15 = (this.IP16 = null)))))))))));
				return;
			}
			this.IP1 = new byte?(addressBytes[0]);
			this.IP2 = new byte?(addressBytes[1]);
			this.IP3 = new byte?(addressBytes[2]);
			this.IP4 = new byte?(addressBytes[3]);
			this.IP5 = new byte?(addressBytes[4]);
			this.IP6 = new byte?(addressBytes[5]);
			this.IP7 = new byte?(addressBytes[6]);
			this.IP8 = new byte?(addressBytes[7]);
			this.IP9 = new byte?(addressBytes[8]);
			this.IP10 = new byte?(addressBytes[9]);
			this.IP11 = new byte?(addressBytes[10]);
			this.IP12 = new byte?(addressBytes[11]);
			this.IP13 = new byte?(addressBytes[12]);
			this.IP14 = new byte?(addressBytes[13]);
			this.IP15 = new byte?(addressBytes[14]);
			this.IP16 = new byte?(addressBytes[15]);
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0002AE23 File Offset: 0x00029023
		private void SetReceivedTime(Guid guid)
		{
			if (CombGuidGenerator.IsCombGuid(guid))
			{
				this.ReceivedTime = CombGuidGenerator.ExtractDateTimeFromCombGuid(guid);
				return;
			}
			this.ReceivedTime = DateTime.UtcNow;
		}

		// Token: 0x040006B6 RID: 1718
		internal static readonly HygienePropertyDefinition[] Properties = new HygienePropertyDefinition[]
		{
			MessageTraceSchema.OrganizationalUnitRootProperty,
			MessageTraceSchema.ClientMessageIdProperty,
			MessageTraceSchema.ExMessageIdProperty,
			MessageTraceSchema.DirectionProperty,
			MessageTraceSchema.FromEmailPrefixProperty,
			MessageTraceSchema.FromEmailDomainProperty,
			MessageTraceSchema.IPAddressProperty,
			MessageTraceSchema.IP1Property,
			MessageTraceSchema.IP2Property,
			MessageTraceSchema.IP3Property,
			MessageTraceSchema.IP4Property,
			MessageTraceSchema.IP5Property,
			MessageTraceSchema.IP6Property,
			MessageTraceSchema.IP7Property,
			MessageTraceSchema.IP8Property,
			MessageTraceSchema.IP9Property,
			MessageTraceSchema.IP10Property,
			MessageTraceSchema.IP11Property,
			MessageTraceSchema.IP12Property,
			MessageTraceSchema.IP13Property,
			MessageTraceSchema.IP14Property,
			MessageTraceSchema.IP15Property,
			MessageTraceSchema.IP16Property,
			CommonMessageTraceSchema.EmailHashKeyProperty,
			CommonMessageTraceSchema.EmailDomainHashKeyProperty,
			CommonMessageTraceSchema.IPHashKeyProperty,
			CommonMessageTraceSchema.HashBucketProperty,
			MessageTraceSchema.ReceivedTimeProperty
		};

		// Token: 0x040006B7 RID: 1719
		private ExtendedPropertyStore<MessageProperty> extendedProperties;

		// Token: 0x040006B8 RID: 1720
		private List<MessageRecipient> msgRecipients;

		// Token: 0x040006B9 RID: 1721
		private List<MessageEvent> msgEvents;

		// Token: 0x040006BA RID: 1722
		private List<MessageClassification> msgClassifications;

		// Token: 0x040006BB RID: 1723
		private List<MessageClientInformation> msgClientInformation;
	}
}
