using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Net.Protocols.DeltaSync.Email;
using Microsoft.Exchange.Net.Protocols.DeltaSync.HMFolder;
using Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SyncRequest
{
	// Token: 0x020001A8 RID: 424
	[XmlType(TypeName = "ApplicationDataType", Namespace = "AirSync:")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class ApplicationDataType
	{
		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x0001E137 File Offset: 0x0001C337
		// (set) Token: 0x06000BB7 RID: 2999 RVA: 0x0001E13F File Offset: 0x0001C33F
		[XmlIgnore]
		public byte Read
		{
			get
			{
				return this.internalRead;
			}
			set
			{
				this.internalRead = value;
				this.internalReadSpecified = true;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x0001E14F File Offset: 0x0001C34F
		// (set) Token: 0x06000BB9 RID: 3001 RVA: 0x0001E16A File Offset: 0x0001C36A
		[XmlIgnore]
		public From From
		{
			get
			{
				if (this.internalFrom == null)
				{
					this.internalFrom = new From();
				}
				return this.internalFrom;
			}
			set
			{
				this.internalFrom = value;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x0001E173 File Offset: 0x0001C373
		// (set) Token: 0x06000BBB RID: 3003 RVA: 0x0001E18E File Offset: 0x0001C38E
		[XmlIgnore]
		public Subject Subject
		{
			get
			{
				if (this.internalSubject == null)
				{
					this.internalSubject = new Subject();
				}
				return this.internalSubject;
			}
			set
			{
				this.internalSubject = value;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x0001E197 File Offset: 0x0001C397
		// (set) Token: 0x06000BBD RID: 3005 RVA: 0x0001E19F File Offset: 0x0001C39F
		[XmlIgnore]
		public string DateReceived
		{
			get
			{
				return this.internalDateReceived;
			}
			set
			{
				this.internalDateReceived = value;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x0001E1A8 File Offset: 0x0001C3A8
		// (set) Token: 0x06000BBF RID: 3007 RVA: 0x0001E1B0 File Offset: 0x0001C3B0
		[XmlIgnore]
		public byte Importance
		{
			get
			{
				return this.internalImportance;
			}
			set
			{
				this.internalImportance = value;
				this.internalImportanceSpecified = true;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x0001E1C0 File Offset: 0x0001C3C0
		// (set) Token: 0x06000BC1 RID: 3009 RVA: 0x0001E1C8 File Offset: 0x0001C3C8
		[XmlIgnore]
		public string MessageClass
		{
			get
			{
				return this.internalMessageClass;
			}
			set
			{
				this.internalMessageClass = value;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x0001E1D1 File Offset: 0x0001C3D1
		// (set) Token: 0x06000BC3 RID: 3011 RVA: 0x0001E1EC File Offset: 0x0001C3EC
		[XmlIgnore]
		public Message Message
		{
			get
			{
				if (this.internalMessage == null)
				{
					this.internalMessage = new Message();
				}
				return this.internalMessage;
			}
			set
			{
				this.internalMessage = value;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x0001E1F5 File Offset: 0x0001C3F5
		// (set) Token: 0x06000BC5 RID: 3013 RVA: 0x0001E1FD File Offset: 0x0001C3FD
		[XmlIgnore]
		public byte ReplyToOrForwardState
		{
			get
			{
				return this.internalReplyToOrForwardState;
			}
			set
			{
				this.internalReplyToOrForwardState = value;
				this.internalReplyToOrForwardStateSpecified = true;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x0001E20D File Offset: 0x0001C40D
		// (set) Token: 0x06000BC7 RID: 3015 RVA: 0x0001E228 File Offset: 0x0001C428
		[XmlIgnore]
		public Categories Categories
		{
			get
			{
				if (this.internalCategories == null)
				{
					this.internalCategories = new Categories();
				}
				return this.internalCategories;
			}
			set
			{
				this.internalCategories = value;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x0001E231 File Offset: 0x0001C431
		// (set) Token: 0x06000BC9 RID: 3017 RVA: 0x0001E239 File Offset: 0x0001C439
		[XmlIgnore]
		public Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail.bitType ConfirmedJunk
		{
			get
			{
				return this.internalConfirmedJunk;
			}
			set
			{
				this.internalConfirmedJunk = value;
				this.internalConfirmedJunkSpecified = true;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x0001E249 File Offset: 0x0001C449
		// (set) Token: 0x06000BCB RID: 3019 RVA: 0x0001E264 File Offset: 0x0001C464
		[XmlIgnore]
		public Flag Flag
		{
			get
			{
				if (this.internalFlag == null)
				{
					this.internalFlag = new Flag();
				}
				return this.internalFlag;
			}
			set
			{
				this.internalFlag = value;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x0001E26D File Offset: 0x0001C46D
		// (set) Token: 0x06000BCD RID: 3021 RVA: 0x0001E288 File Offset: 0x0001C488
		[XmlIgnore]
		public FolderId FolderId
		{
			get
			{
				if (this.internalFolderId == null)
				{
					this.internalFolderId = new FolderId();
				}
				return this.internalFolderId;
			}
			set
			{
				this.internalFolderId = value;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x0001E291 File Offset: 0x0001C491
		// (set) Token: 0x06000BCF RID: 3023 RVA: 0x0001E299 File Offset: 0x0001C499
		[XmlIgnore]
		public string LegacyId
		{
			get
			{
				return this.internalLegacyId;
			}
			set
			{
				this.internalLegacyId = value;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x0001E2A2 File Offset: 0x0001C4A2
		// (set) Token: 0x06000BD1 RID: 3025 RVA: 0x0001E2BD File Offset: 0x0001C4BD
		[XmlIgnore]
		public ConversationTopic ConversationTopic
		{
			get
			{
				if (this.internalConversationTopic == null)
				{
					this.internalConversationTopic = new ConversationTopic();
				}
				return this.internalConversationTopic;
			}
			set
			{
				this.internalConversationTopic = value;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0001E2C6 File Offset: 0x0001C4C6
		// (set) Token: 0x06000BD3 RID: 3027 RVA: 0x0001E2E1 File Offset: 0x0001C4E1
		[XmlIgnore]
		public ConversationIndex ConversationIndex
		{
			get
			{
				if (this.internalConversationIndex == null)
				{
					this.internalConversationIndex = new ConversationIndex();
				}
				return this.internalConversationIndex;
			}
			set
			{
				this.internalConversationIndex = value;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x0001E2EA File Offset: 0x0001C4EA
		// (set) Token: 0x06000BD5 RID: 3029 RVA: 0x0001E2F2 File Offset: 0x0001C4F2
		[XmlIgnore]
		public byte Sensitivity
		{
			get
			{
				return this.internalSensitivity;
			}
			set
			{
				this.internalSensitivity = value;
				this.internalSensitivitySpecified = true;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x0001E302 File Offset: 0x0001C502
		// (set) Token: 0x06000BD7 RID: 3031 RVA: 0x0001E30A File Offset: 0x0001C50A
		[XmlIgnore]
		public int Size
		{
			get
			{
				return this.internalSize;
			}
			set
			{
				this.internalSize = value;
				this.internalSizeSpecified = true;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x0001E31A File Offset: 0x0001C51A
		// (set) Token: 0x06000BD9 RID: 3033 RVA: 0x0001E322 File Offset: 0x0001C522
		[XmlIgnore]
		public Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail.bitType HasAttachments
		{
			get
			{
				return this.internalHasAttachments;
			}
			set
			{
				this.internalHasAttachments = value;
				this.internalHasAttachmentsSpecified = true;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x0001E332 File Offset: 0x0001C532
		// (set) Token: 0x06000BDB RID: 3035 RVA: 0x0001E33A File Offset: 0x0001C53A
		[XmlIgnore]
		public Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail.bitType TrustedSource
		{
			get
			{
				return this.internalTrustedSource;
			}
			set
			{
				this.internalTrustedSource = value;
				this.internalTrustedSourceSpecified = true;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x0001E34A File Offset: 0x0001C54A
		// (set) Token: 0x06000BDD RID: 3037 RVA: 0x0001E352 File Offset: 0x0001C552
		[XmlIgnore]
		public Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail.bitType IsFromSomeoneAddressBook
		{
			get
			{
				return this.internalIsFromSomeoneAddressBook;
			}
			set
			{
				this.internalIsFromSomeoneAddressBook = value;
				this.internalIsFromSomeoneAddressBookSpecified = true;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x0001E362 File Offset: 0x0001C562
		// (set) Token: 0x06000BDF RID: 3039 RVA: 0x0001E36A File Offset: 0x0001C56A
		[XmlIgnore]
		public Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail.bitType IsToAllowList
		{
			get
			{
				return this.internalIsToAllowList;
			}
			set
			{
				this.internalIsToAllowList = value;
				this.internalIsToAllowListSpecified = true;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x0001E37A File Offset: 0x0001C57A
		// (set) Token: 0x06000BE1 RID: 3041 RVA: 0x0001E382 File Offset: 0x0001C582
		[XmlIgnore]
		public int Version
		{
			get
			{
				return this.internalVersion;
			}
			set
			{
				this.internalVersion = value;
				this.internalVersionSpecified = true;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x0001E392 File Offset: 0x0001C592
		// (set) Token: 0x06000BE3 RID: 3043 RVA: 0x0001E39A File Offset: 0x0001C59A
		[XmlIgnore]
		public Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail.bitType IsBondedSender
		{
			get
			{
				return this.internalIsBondedSender;
			}
			set
			{
				this.internalIsBondedSender = value;
				this.internalIsBondedSenderSpecified = true;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x0001E3AA File Offset: 0x0001C5AA
		// (set) Token: 0x06000BE5 RID: 3045 RVA: 0x0001E3B2 File Offset: 0x0001C5B2
		[XmlIgnore]
		public byte TypeData
		{
			get
			{
				return this.internalTypeData;
			}
			set
			{
				this.internalTypeData = value;
				this.internalTypeDataSpecified = true;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x0001E3C2 File Offset: 0x0001C5C2
		// (set) Token: 0x06000BE7 RID: 3047 RVA: 0x0001E3DD File Offset: 0x0001C5DD
		[XmlIgnore]
		public DisplayName DisplayName
		{
			get
			{
				if (this.internalDisplayName == null)
				{
					this.internalDisplayName = new DisplayName();
				}
				return this.internalDisplayName;
			}
			set
			{
				this.internalDisplayName = value;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x0001E3E6 File Offset: 0x0001C5E6
		// (set) Token: 0x06000BE9 RID: 3049 RVA: 0x0001E3EE File Offset: 0x0001C5EE
		[XmlIgnore]
		public int Version2
		{
			get
			{
				return this.internalVersion2;
			}
			set
			{
				this.internalVersion2 = value;
				this.internalVersion2Specified = true;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x0001E3FE File Offset: 0x0001C5FE
		// (set) Token: 0x06000BEB RID: 3051 RVA: 0x0001E419 File Offset: 0x0001C619
		[XmlIgnore]
		public ParentId ParentId
		{
			get
			{
				if (this.internalParentId == null)
				{
					this.internalParentId = new ParentId();
				}
				return this.internalParentId;
			}
			set
			{
				this.internalParentId = value;
			}
		}

		// Token: 0x040006AC RID: 1708
		[XmlElement(ElementName = "Read", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "unsignedByte", Namespace = "EMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public byte internalRead;

		// Token: 0x040006AD RID: 1709
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalReadSpecified;

		// Token: 0x040006AE RID: 1710
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(From), ElementName = "From", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "EMAIL:")]
		public From internalFrom;

		// Token: 0x040006AF RID: 1711
		[XmlElement(Type = typeof(Subject), ElementName = "Subject", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "EMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Subject internalSubject;

		// Token: 0x040006B0 RID: 1712
		[XmlElement(ElementName = "DateReceived", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "EMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalDateReceived;

		// Token: 0x040006B1 RID: 1713
		[XmlElement(ElementName = "Importance", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "unsignedByte", Namespace = "EMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public byte internalImportance;

		// Token: 0x040006B2 RID: 1714
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalImportanceSpecified;

		// Token: 0x040006B3 RID: 1715
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "MessageClass", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "EMAIL:")]
		public string internalMessageClass;

		// Token: 0x040006B4 RID: 1716
		[XmlElement(Type = typeof(Message), ElementName = "Message", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Message internalMessage;

		// Token: 0x040006B5 RID: 1717
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "ReplyToOrForwardState", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "unsignedByte", Namespace = "HMMAIL:")]
		public byte internalReplyToOrForwardState;

		// Token: 0x040006B6 RID: 1718
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalReplyToOrForwardStateSpecified;

		// Token: 0x040006B7 RID: 1719
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(Categories), ElementName = "Categories", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMMAIL:")]
		public Categories internalCategories;

		// Token: 0x040006B8 RID: 1720
		[XmlElement(ElementName = "ConfirmedJunk", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail.bitType internalConfirmedJunk;

		// Token: 0x040006B9 RID: 1721
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalConfirmedJunkSpecified;

		// Token: 0x040006BA RID: 1722
		[XmlElement(Type = typeof(Flag), ElementName = "Flag", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Flag internalFlag;

		// Token: 0x040006BB RID: 1723
		[XmlElement(Type = typeof(FolderId), ElementName = "FolderId", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public FolderId internalFolderId;

		// Token: 0x040006BC RID: 1724
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "LegacyId", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMMAIL:")]
		public string internalLegacyId;

		// Token: 0x040006BD RID: 1725
		[XmlElement(Type = typeof(ConversationTopic), ElementName = "ConversationTopic", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ConversationTopic internalConversationTopic;

		// Token: 0x040006BE RID: 1726
		[XmlElement(Type = typeof(ConversationIndex), ElementName = "ConversationIndex", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ConversationIndex internalConversationIndex;

		// Token: 0x040006BF RID: 1727
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Sensitivity", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "unsignedByte", Namespace = "HMMAIL:")]
		public byte internalSensitivity;

		// Token: 0x040006C0 RID: 1728
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalSensitivitySpecified;

		// Token: 0x040006C1 RID: 1729
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Size", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMMAIL:")]
		public int internalSize;

		// Token: 0x040006C2 RID: 1730
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalSizeSpecified;

		// Token: 0x040006C3 RID: 1731
		[XmlElement(ElementName = "HasAttachments", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail.bitType internalHasAttachments;

		// Token: 0x040006C4 RID: 1732
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalHasAttachmentsSpecified;

		// Token: 0x040006C5 RID: 1733
		[XmlElement(ElementName = "TrustedSource", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail.bitType internalTrustedSource;

		// Token: 0x040006C6 RID: 1734
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalTrustedSourceSpecified;

		// Token: 0x040006C7 RID: 1735
		[XmlElement(ElementName = "IsFromSomeoneAddressBook", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail.bitType internalIsFromSomeoneAddressBook;

		// Token: 0x040006C8 RID: 1736
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalIsFromSomeoneAddressBookSpecified;

		// Token: 0x040006C9 RID: 1737
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "IsToAllowList", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMMAIL:")]
		public Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail.bitType internalIsToAllowList;

		// Token: 0x040006CA RID: 1738
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalIsToAllowListSpecified;

		// Token: 0x040006CB RID: 1739
		[XmlElement(ElementName = "Version", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalVersion;

		// Token: 0x040006CC RID: 1740
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalVersionSpecified;

		// Token: 0x040006CD RID: 1741
		[XmlElement(ElementName = "IsBondedSender", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail.bitType internalIsBondedSender;

		// Token: 0x040006CE RID: 1742
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalIsBondedSenderSpecified;

		// Token: 0x040006CF RID: 1743
		[XmlElement(ElementName = "TypeData", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "unsignedByte", Namespace = "HMMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public byte internalTypeData;

		// Token: 0x040006D0 RID: 1744
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalTypeDataSpecified;

		// Token: 0x040006D1 RID: 1745
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(DisplayName), ElementName = "DisplayName", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMFOLDER:")]
		public DisplayName internalDisplayName;

		// Token: 0x040006D2 RID: 1746
		[XmlElement(ElementName = "Version", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMFOLDER:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalVersion2;

		// Token: 0x040006D3 RID: 1747
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalVersion2Specified;

		// Token: 0x040006D4 RID: 1748
		[XmlElement(Type = typeof(ParentId), ElementName = "ParentId", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMFOLDER:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ParentId internalParentId;
	}
}
