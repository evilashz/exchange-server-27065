using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000157 RID: 343
	[XmlType(TypeName = "PropertiesType", Namespace = "HMSETTINGS:")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class PropertiesType
	{
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x0001CDE9 File Offset: 0x0001AFE9
		// (set) Token: 0x060009E3 RID: 2531 RVA: 0x0001CDF1 File Offset: 0x0001AFF1
		[XmlIgnore]
		public AccountStatusType AccountStatus
		{
			get
			{
				return this.internalAccountStatus;
			}
			set
			{
				this.internalAccountStatus = value;
				this.internalAccountStatusSpecified = true;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x0001CE01 File Offset: 0x0001B001
		// (set) Token: 0x060009E5 RID: 2533 RVA: 0x0001CE09 File Offset: 0x0001B009
		[XmlIgnore]
		public ParentalControlStatusType ParentalControlStatus
		{
			get
			{
				return this.internalParentalControlStatus;
			}
			set
			{
				this.internalParentalControlStatus = value;
				this.internalParentalControlStatusSpecified = true;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0001CE19 File Offset: 0x0001B019
		// (set) Token: 0x060009E7 RID: 2535 RVA: 0x0001CE21 File Offset: 0x0001B021
		[XmlIgnore]
		public long MailBoxSize
		{
			get
			{
				return this.internalMailBoxSize;
			}
			set
			{
				this.internalMailBoxSize = value;
				this.internalMailBoxSizeSpecified = true;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x0001CE31 File Offset: 0x0001B031
		// (set) Token: 0x060009E9 RID: 2537 RVA: 0x0001CE39 File Offset: 0x0001B039
		[XmlIgnore]
		public long MaxMailBoxSize
		{
			get
			{
				return this.internalMaxMailBoxSize;
			}
			set
			{
				this.internalMaxMailBoxSize = value;
				this.internalMaxMailBoxSizeSpecified = true;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x0001CE49 File Offset: 0x0001B049
		// (set) Token: 0x060009EB RID: 2539 RVA: 0x0001CE51 File Offset: 0x0001B051
		[XmlIgnore]
		public int MaxAttachments
		{
			get
			{
				return this.internalMaxAttachments;
			}
			set
			{
				this.internalMaxAttachments = value;
				this.internalMaxAttachmentsSpecified = true;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x0001CE61 File Offset: 0x0001B061
		// (set) Token: 0x060009ED RID: 2541 RVA: 0x0001CE69 File Offset: 0x0001B069
		[XmlIgnore]
		public long MaxMessageSize
		{
			get
			{
				return this.internalMaxMessageSize;
			}
			set
			{
				this.internalMaxMessageSize = value;
				this.internalMaxMessageSizeSpecified = true;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x0001CE79 File Offset: 0x0001B079
		// (set) Token: 0x060009EF RID: 2543 RVA: 0x0001CE81 File Offset: 0x0001B081
		[XmlIgnore]
		public long MaxUnencodedMessageSize
		{
			get
			{
				return this.internalMaxUnencodedMessageSize;
			}
			set
			{
				this.internalMaxUnencodedMessageSize = value;
				this.internalMaxUnencodedMessageSizeSpecified = true;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x0001CE91 File Offset: 0x0001B091
		// (set) Token: 0x060009F1 RID: 2545 RVA: 0x0001CE99 File Offset: 0x0001B099
		[XmlIgnore]
		public int MaxFilters
		{
			get
			{
				return this.internalMaxFilters;
			}
			set
			{
				this.internalMaxFilters = value;
				this.internalMaxFiltersSpecified = true;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x0001CEA9 File Offset: 0x0001B0A9
		// (set) Token: 0x060009F3 RID: 2547 RVA: 0x0001CEB1 File Offset: 0x0001B0B1
		[XmlIgnore]
		public int MaxFilterClauseValueLength
		{
			get
			{
				return this.internalMaxFilterClauseValueLength;
			}
			set
			{
				this.internalMaxFilterClauseValueLength = value;
				this.internalMaxFilterClauseValueLengthSpecified = true;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x0001CEC1 File Offset: 0x0001B0C1
		// (set) Token: 0x060009F5 RID: 2549 RVA: 0x0001CEC9 File Offset: 0x0001B0C9
		[XmlIgnore]
		public int MaxRecipients
		{
			get
			{
				return this.internalMaxRecipients;
			}
			set
			{
				this.internalMaxRecipients = value;
				this.internalMaxRecipientsSpecified = true;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x0001CED9 File Offset: 0x0001B0D9
		// (set) Token: 0x060009F7 RID: 2551 RVA: 0x0001CEE1 File Offset: 0x0001B0E1
		[XmlIgnore]
		public int MaxUserSignatureLength
		{
			get
			{
				return this.internalMaxUserSignatureLength;
			}
			set
			{
				this.internalMaxUserSignatureLength = value;
				this.internalMaxUserSignatureLengthSpecified = true;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x0001CEF1 File Offset: 0x0001B0F1
		// (set) Token: 0x060009F9 RID: 2553 RVA: 0x0001CEF9 File Offset: 0x0001B0F9
		[XmlIgnore]
		public int BlockListAddressMax
		{
			get
			{
				return this.internalBlockListAddressMax;
			}
			set
			{
				this.internalBlockListAddressMax = value;
				this.internalBlockListAddressMaxSpecified = true;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x0001CF09 File Offset: 0x0001B109
		// (set) Token: 0x060009FB RID: 2555 RVA: 0x0001CF11 File Offset: 0x0001B111
		[XmlIgnore]
		public int BlockListDomainMax
		{
			get
			{
				return this.internalBlockListDomainMax;
			}
			set
			{
				this.internalBlockListDomainMax = value;
				this.internalBlockListDomainMaxSpecified = true;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x0001CF21 File Offset: 0x0001B121
		// (set) Token: 0x060009FD RID: 2557 RVA: 0x0001CF29 File Offset: 0x0001B129
		[XmlIgnore]
		public int WhiteListAddressMax
		{
			get
			{
				return this.internalWhiteListAddressMax;
			}
			set
			{
				this.internalWhiteListAddressMax = value;
				this.internalWhiteListAddressMaxSpecified = true;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x0001CF39 File Offset: 0x0001B139
		// (set) Token: 0x060009FF RID: 2559 RVA: 0x0001CF41 File Offset: 0x0001B141
		[XmlIgnore]
		public int WhiteListDomainMax
		{
			get
			{
				return this.internalWhiteListDomainMax;
			}
			set
			{
				this.internalWhiteListDomainMax = value;
				this.internalWhiteListDomainMaxSpecified = true;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x0001CF51 File Offset: 0x0001B151
		// (set) Token: 0x06000A01 RID: 2561 RVA: 0x0001CF59 File Offset: 0x0001B159
		[XmlIgnore]
		public int WhiteToListMax
		{
			get
			{
				return this.internalWhiteToListMax;
			}
			set
			{
				this.internalWhiteToListMax = value;
				this.internalWhiteToListMaxSpecified = true;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x0001CF69 File Offset: 0x0001B169
		// (set) Token: 0x06000A03 RID: 2563 RVA: 0x0001CF71 File Offset: 0x0001B171
		[XmlIgnore]
		public int AlternateFromListMax
		{
			get
			{
				return this.internalAlternateFromListMax;
			}
			set
			{
				this.internalAlternateFromListMax = value;
				this.internalAlternateFromListMaxSpecified = true;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x0001CF81 File Offset: 0x0001B181
		// (set) Token: 0x06000A05 RID: 2565 RVA: 0x0001CF89 File Offset: 0x0001B189
		[XmlIgnore]
		public int MaxDailySendMessages
		{
			get
			{
				return this.internalMaxDailySendMessages;
			}
			set
			{
				this.internalMaxDailySendMessages = value;
				this.internalMaxDailySendMessagesSpecified = true;
			}
		}

		// Token: 0x0400056C RID: 1388
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "AccountStatus", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public AccountStatusType internalAccountStatus;

		// Token: 0x0400056D RID: 1389
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalAccountStatusSpecified;

		// Token: 0x0400056E RID: 1390
		[XmlElement(ElementName = "ParentalControlStatus", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ParentalControlStatusType internalParentalControlStatus;

		// Token: 0x0400056F RID: 1391
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalParentalControlStatusSpecified;

		// Token: 0x04000570 RID: 1392
		[XmlElement(ElementName = "MailBoxSize", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "long", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public long internalMailBoxSize;

		// Token: 0x04000571 RID: 1393
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalMailBoxSizeSpecified;

		// Token: 0x04000572 RID: 1394
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "MaxMailBoxSize", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "long", Namespace = "HMSETTINGS:")]
		public long internalMaxMailBoxSize;

		// Token: 0x04000573 RID: 1395
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalMaxMailBoxSizeSpecified;

		// Token: 0x04000574 RID: 1396
		[XmlElement(ElementName = "MaxAttachments", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalMaxAttachments;

		// Token: 0x04000575 RID: 1397
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalMaxAttachmentsSpecified;

		// Token: 0x04000576 RID: 1398
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "MaxMessageSize", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "long", Namespace = "HMSETTINGS:")]
		public long internalMaxMessageSize;

		// Token: 0x04000577 RID: 1399
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalMaxMessageSizeSpecified;

		// Token: 0x04000578 RID: 1400
		[XmlElement(ElementName = "MaxUnencodedMessageSize", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "long", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public long internalMaxUnencodedMessageSize;

		// Token: 0x04000579 RID: 1401
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalMaxUnencodedMessageSizeSpecified;

		// Token: 0x0400057A RID: 1402
		[XmlElement(ElementName = "MaxFilters", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalMaxFilters;

		// Token: 0x0400057B RID: 1403
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalMaxFiltersSpecified;

		// Token: 0x0400057C RID: 1404
		[XmlElement(ElementName = "MaxFilterClauseValueLength", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalMaxFilterClauseValueLength;

		// Token: 0x0400057D RID: 1405
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalMaxFilterClauseValueLengthSpecified;

		// Token: 0x0400057E RID: 1406
		[XmlElement(ElementName = "MaxRecipients", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalMaxRecipients;

		// Token: 0x0400057F RID: 1407
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalMaxRecipientsSpecified;

		// Token: 0x04000580 RID: 1408
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "MaxUserSignatureLength", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		public int internalMaxUserSignatureLength;

		// Token: 0x04000581 RID: 1409
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalMaxUserSignatureLengthSpecified;

		// Token: 0x04000582 RID: 1410
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "BlockListAddressMax", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		public int internalBlockListAddressMax;

		// Token: 0x04000583 RID: 1411
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalBlockListAddressMaxSpecified;

		// Token: 0x04000584 RID: 1412
		[XmlElement(ElementName = "BlockListDomainMax", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalBlockListDomainMax;

		// Token: 0x04000585 RID: 1413
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalBlockListDomainMaxSpecified;

		// Token: 0x04000586 RID: 1414
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "WhiteListAddressMax", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		public int internalWhiteListAddressMax;

		// Token: 0x04000587 RID: 1415
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalWhiteListAddressMaxSpecified;

		// Token: 0x04000588 RID: 1416
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "WhiteListDomainMax", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		public int internalWhiteListDomainMax;

		// Token: 0x04000589 RID: 1417
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalWhiteListDomainMaxSpecified;

		// Token: 0x0400058A RID: 1418
		[XmlElement(ElementName = "WhiteToListMax", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalWhiteToListMax;

		// Token: 0x0400058B RID: 1419
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalWhiteToListMaxSpecified;

		// Token: 0x0400058C RID: 1420
		[XmlElement(ElementName = "AlternateFromListMax", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalAlternateFromListMax;

		// Token: 0x0400058D RID: 1421
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalAlternateFromListMaxSpecified;

		// Token: 0x0400058E RID: 1422
		[XmlElement(ElementName = "MaxDailySendMessages", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalMaxDailySendMessages;

		// Token: 0x0400058F RID: 1423
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalMaxDailySendMessagesSpecified;
	}
}
