using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Net.Protocols.DeltaSync.Email;
using Microsoft.Exchange.Net.Protocols.DeltaSync.HMFolder;
using Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SyncResponse
{
	// Token: 0x020001BF RID: 447
	[XmlType(TypeName = "ApplicationData", Namespace = "AirSync:")]
	[Serializable]
	public class ApplicationData
	{
		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x0001EBB5 File Offset: 0x0001CDB5
		// (set) Token: 0x06000C83 RID: 3203 RVA: 0x0001EBBD File Offset: 0x0001CDBD
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

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x0001EBCD File Offset: 0x0001CDCD
		// (set) Token: 0x06000C85 RID: 3205 RVA: 0x0001EBD5 File Offset: 0x0001CDD5
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

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x0001EBE5 File Offset: 0x0001CDE5
		// (set) Token: 0x06000C87 RID: 3207 RVA: 0x0001EBED File Offset: 0x0001CDED
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

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x0001EBFD File Offset: 0x0001CDFD
		// (set) Token: 0x06000C89 RID: 3209 RVA: 0x0001EC18 File Offset: 0x0001CE18
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

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x0001EC21 File Offset: 0x0001CE21
		// (set) Token: 0x06000C8B RID: 3211 RVA: 0x0001EC29 File Offset: 0x0001CE29
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

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x0001EC39 File Offset: 0x0001CE39
		// (set) Token: 0x06000C8D RID: 3213 RVA: 0x0001EC54 File Offset: 0x0001CE54
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

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x0001EC5D File Offset: 0x0001CE5D
		// (set) Token: 0x06000C8F RID: 3215 RVA: 0x0001EC78 File Offset: 0x0001CE78
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

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x0001EC81 File Offset: 0x0001CE81
		// (set) Token: 0x06000C91 RID: 3217 RVA: 0x0001EC9C File Offset: 0x0001CE9C
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

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x0001ECA5 File Offset: 0x0001CEA5
		// (set) Token: 0x06000C93 RID: 3219 RVA: 0x0001ECAD File Offset: 0x0001CEAD
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

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x0001ECBD File Offset: 0x0001CEBD
		// (set) Token: 0x06000C95 RID: 3221 RVA: 0x0001ECD8 File Offset: 0x0001CED8
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

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x0001ECE1 File Offset: 0x0001CEE1
		// (set) Token: 0x06000C97 RID: 3223 RVA: 0x0001ECFC File Offset: 0x0001CEFC
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

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x0001ED05 File Offset: 0x0001CF05
		// (set) Token: 0x06000C99 RID: 3225 RVA: 0x0001ED20 File Offset: 0x0001CF20
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

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x0001ED29 File Offset: 0x0001CF29
		// (set) Token: 0x06000C9B RID: 3227 RVA: 0x0001ED44 File Offset: 0x0001CF44
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

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x0001ED4D File Offset: 0x0001CF4D
		// (set) Token: 0x06000C9D RID: 3229 RVA: 0x0001ED68 File Offset: 0x0001CF68
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

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x0001ED71 File Offset: 0x0001CF71
		// (set) Token: 0x06000C9F RID: 3231 RVA: 0x0001ED79 File Offset: 0x0001CF79
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

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x0001ED82 File Offset: 0x0001CF82
		// (set) Token: 0x06000CA1 RID: 3233 RVA: 0x0001ED8A File Offset: 0x0001CF8A
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

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x0001ED9A File Offset: 0x0001CF9A
		// (set) Token: 0x06000CA3 RID: 3235 RVA: 0x0001EDA2 File Offset: 0x0001CFA2
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

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x0001EDB2 File Offset: 0x0001CFB2
		// (set) Token: 0x06000CA5 RID: 3237 RVA: 0x0001EDBA File Offset: 0x0001CFBA
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

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x0001EDCA File Offset: 0x0001CFCA
		// (set) Token: 0x06000CA7 RID: 3239 RVA: 0x0001EDD2 File Offset: 0x0001CFD2
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

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x0001EDDB File Offset: 0x0001CFDB
		// (set) Token: 0x06000CA9 RID: 3241 RVA: 0x0001EDE3 File Offset: 0x0001CFE3
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

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x0001EDEC File Offset: 0x0001CFEC
		// (set) Token: 0x06000CAB RID: 3243 RVA: 0x0001EDF4 File Offset: 0x0001CFF4
		[XmlIgnore]
		public string Message
		{
			get
			{
				return this.internalMessage;
			}
			set
			{
				this.internalMessage = value;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x0001EDFD File Offset: 0x0001CFFD
		// (set) Token: 0x06000CAD RID: 3245 RVA: 0x0001EE05 File Offset: 0x0001D005
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

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x0001EE15 File Offset: 0x0001D015
		// (set) Token: 0x06000CAF RID: 3247 RVA: 0x0001EE1D File Offset: 0x0001D01D
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

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x0001EE2D File Offset: 0x0001D02D
		// (set) Token: 0x06000CB1 RID: 3249 RVA: 0x0001EE35 File Offset: 0x0001D035
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

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x0001EE45 File Offset: 0x0001D045
		// (set) Token: 0x06000CB3 RID: 3251 RVA: 0x0001EE4D File Offset: 0x0001D04D
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

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x0001EE5D File Offset: 0x0001D05D
		// (set) Token: 0x06000CB5 RID: 3253 RVA: 0x0001EE65 File Offset: 0x0001D065
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

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x0001EE75 File Offset: 0x0001D075
		// (set) Token: 0x06000CB7 RID: 3255 RVA: 0x0001EE7D File Offset: 0x0001D07D
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

		// Token: 0x040006FD RID: 1789
		[XmlElement(ElementName = "Read", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "unsignedByte", Namespace = "EMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public byte internalRead;

		// Token: 0x040006FE RID: 1790
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalReadSpecified;

		// Token: 0x040006FF RID: 1791
		[XmlElement(ElementName = "ReplyToOrForwardState", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "unsignedByte", Namespace = "HMMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public byte internalReplyToOrForwardState;

		// Token: 0x04000700 RID: 1792
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalReplyToOrForwardStateSpecified;

		// Token: 0x04000701 RID: 1793
		[XmlElement(ElementName = "Importance", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "unsignedByte", Namespace = "EMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public byte internalImportance;

		// Token: 0x04000702 RID: 1794
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalImportanceSpecified;

		// Token: 0x04000703 RID: 1795
		[XmlElement(Type = typeof(Categories), ElementName = "Categories", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Categories internalCategories;

		// Token: 0x04000704 RID: 1796
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "ConfirmedJunk", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMMAIL:")]
		public Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail.bitType internalConfirmedJunk;

		// Token: 0x04000705 RID: 1797
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalConfirmedJunkSpecified;

		// Token: 0x04000706 RID: 1798
		[XmlElement(Type = typeof(Flag), ElementName = "Flag", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Flag internalFlag;

		// Token: 0x04000707 RID: 1799
		[XmlElement(Type = typeof(FolderId), ElementName = "FolderId", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public FolderId internalFolderId;

		// Token: 0x04000708 RID: 1800
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(DisplayName), ElementName = "DisplayName", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMFOLDER:")]
		public DisplayName internalDisplayName;

		// Token: 0x04000709 RID: 1801
		[XmlElement(ElementName = "Version", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMFOLDER:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalVersion;

		// Token: 0x0400070A RID: 1802
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalVersionSpecified;

		// Token: 0x0400070B RID: 1803
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(ParentId), ElementName = "ParentId", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMFOLDER:")]
		public ParentId internalParentId;

		// Token: 0x0400070C RID: 1804
		[XmlElement(Type = typeof(ConversationTopic), ElementName = "ConversationTopic", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ConversationTopic internalConversationTopic;

		// Token: 0x0400070D RID: 1805
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(ConversationIndex), ElementName = "ConversationIndex", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMMAIL:")]
		public ConversationIndex internalConversationIndex;

		// Token: 0x0400070E RID: 1806
		[XmlElement(Type = typeof(From), ElementName = "From", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "EMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public From internalFrom;

		// Token: 0x0400070F RID: 1807
		[XmlElement(Type = typeof(Subject), ElementName = "Subject", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "EMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Subject internalSubject;

		// Token: 0x04000710 RID: 1808
		[XmlElement(ElementName = "DateReceived", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "EMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalDateReceived;

		// Token: 0x04000711 RID: 1809
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "HasAttachments", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMMAIL:")]
		public Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail.bitType internalHasAttachments;

		// Token: 0x04000712 RID: 1810
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalHasAttachmentsSpecified;

		// Token: 0x04000713 RID: 1811
		[XmlElement(ElementName = "IsFromSomeoneAddressBook", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail.bitType internalIsFromSomeoneAddressBook;

		// Token: 0x04000714 RID: 1812
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalIsFromSomeoneAddressBookSpecified;

		// Token: 0x04000715 RID: 1813
		[XmlElement(ElementName = "IsToAllowList", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail.bitType internalIsToAllowList;

		// Token: 0x04000716 RID: 1814
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalIsToAllowListSpecified;

		// Token: 0x04000717 RID: 1815
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "LegacyId", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMMAIL:")]
		public string internalLegacyId;

		// Token: 0x04000718 RID: 1816
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "MessageClass", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "EMAIL:")]
		public string internalMessageClass;

		// Token: 0x04000719 RID: 1817
		[XmlElement(ElementName = "Message", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalMessage;

		// Token: 0x0400071A RID: 1818
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Sensitivity", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "unsignedByte", Namespace = "HMMAIL:")]
		public byte internalSensitivity;

		// Token: 0x0400071B RID: 1819
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalSensitivitySpecified;

		// Token: 0x0400071C RID: 1820
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Size", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMMAIL:")]
		public int internalSize;

		// Token: 0x0400071D RID: 1821
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalSizeSpecified;

		// Token: 0x0400071E RID: 1822
		[XmlElement(ElementName = "TrustedSource", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail.bitType internalTrustedSource;

		// Token: 0x0400071F RID: 1823
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalTrustedSourceSpecified;

		// Token: 0x04000720 RID: 1824
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Version", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMMAIL:")]
		public int internalVersion2;

		// Token: 0x04000721 RID: 1825
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalVersion2Specified;

		// Token: 0x04000722 RID: 1826
		[XmlElement(ElementName = "IsBondedSender", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMMAIL:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail.bitType internalIsBondedSender;

		// Token: 0x04000723 RID: 1827
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalIsBondedSenderSpecified;

		// Token: 0x04000724 RID: 1828
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "TypeData", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "unsignedByte", Namespace = "HMMAIL:")]
		public byte internalTypeData;

		// Token: 0x04000725 RID: 1829
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalTypeDataSpecified;
	}
}
