using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Net.Protocols.DeltaSync.HMTypes;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000158 RID: 344
	[XmlType(TypeName = "OptionsType", Namespace = "HMSETTINGS:")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class OptionsType
	{
		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x0001CFA1 File Offset: 0x0001B1A1
		// (set) Token: 0x06000A08 RID: 2568 RVA: 0x0001CFA9 File Offset: 0x0001B1A9
		[XmlIgnore]
		public AlertStateType AlertState
		{
			get
			{
				return this.internalAlertState;
			}
			set
			{
				this.internalAlertState = value;
				this.internalAlertStateSpecified = true;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x0001CFB9 File Offset: 0x0001B1B9
		// (set) Token: 0x06000A0A RID: 2570 RVA: 0x0001CFC1 File Offset: 0x0001B1C1
		[XmlIgnore]
		public BitType ConfirmSent
		{
			get
			{
				return this.internalConfirmSent;
			}
			set
			{
				this.internalConfirmSent = value;
				this.internalConfirmSentSpecified = true;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0001CFD1 File Offset: 0x0001B1D1
		// (set) Token: 0x06000A0C RID: 2572 RVA: 0x0001CFD9 File Offset: 0x0001B1D9
		[XmlIgnore]
		public HeaderDisplayType HeaderDisplay
		{
			get
			{
				return this.internalHeaderDisplay;
			}
			set
			{
				this.internalHeaderDisplay = value;
				this.internalHeaderDisplaySpecified = true;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x0001CFE9 File Offset: 0x0001B1E9
		// (set) Token: 0x06000A0E RID: 2574 RVA: 0x0001CFF1 File Offset: 0x0001B1F1
		[XmlIgnore]
		public IncludeOriginalInReplyType IncludeOriginalInReply
		{
			get
			{
				return this.internalIncludeOriginalInReply;
			}
			set
			{
				this.internalIncludeOriginalInReply = value;
				this.internalIncludeOriginalInReplySpecified = true;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x0001D001 File Offset: 0x0001B201
		// (set) Token: 0x06000A10 RID: 2576 RVA: 0x0001D009 File Offset: 0x0001B209
		[XmlIgnore]
		public JunkLevelType JunkLevel
		{
			get
			{
				return this.internalJunkLevel;
			}
			set
			{
				this.internalJunkLevel = value;
				this.internalJunkLevelSpecified = true;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x0001D019 File Offset: 0x0001B219
		// (set) Token: 0x06000A12 RID: 2578 RVA: 0x0001D021 File Offset: 0x0001B221
		[XmlIgnore]
		public JunkMailDestinationType JunkMailDestination
		{
			get
			{
				return this.internalJunkMailDestination;
			}
			set
			{
				this.internalJunkMailDestination = value;
				this.internalJunkMailDestinationSpecified = true;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x0001D031 File Offset: 0x0001B231
		// (set) Token: 0x06000A14 RID: 2580 RVA: 0x0001D039 File Offset: 0x0001B239
		[XmlIgnore]
		public ReplyIndicatorType ReplyIndicator
		{
			get
			{
				return this.internalReplyIndicator;
			}
			set
			{
				this.internalReplyIndicator = value;
				this.internalReplyIndicatorSpecified = true;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x0001D049 File Offset: 0x0001B249
		// (set) Token: 0x06000A16 RID: 2582 RVA: 0x0001D051 File Offset: 0x0001B251
		[XmlIgnore]
		public string ReplyToAddress
		{
			get
			{
				return this.internalReplyToAddress;
			}
			set
			{
				this.internalReplyToAddress = value;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x0001D05A File Offset: 0x0001B25A
		// (set) Token: 0x06000A18 RID: 2584 RVA: 0x0001D062 File Offset: 0x0001B262
		[XmlIgnore]
		public BitType SaveSentMail
		{
			get
			{
				return this.internalSaveSentMail;
			}
			set
			{
				this.internalSaveSentMail = value;
				this.internalSaveSentMailSpecified = true;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x0001D072 File Offset: 0x0001B272
		// (set) Token: 0x06000A1A RID: 2586 RVA: 0x0001D07A File Offset: 0x0001B27A
		[XmlIgnore]
		public BitType UseReplyToAddress
		{
			get
			{
				return this.internalUseReplyToAddress;
			}
			set
			{
				this.internalUseReplyToAddress = value;
				this.internalUseReplyToAddressSpecified = true;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x0001D08A File Offset: 0x0001B28A
		// (set) Token: 0x06000A1C RID: 2588 RVA: 0x0001D0A5 File Offset: 0x0001B2A5
		[XmlIgnore]
		public VacationResponse VacationResponse
		{
			get
			{
				if (this.internalVacationResponse == null)
				{
					this.internalVacationResponse = new VacationResponse();
				}
				return this.internalVacationResponse;
			}
			set
			{
				this.internalVacationResponse = value;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x0001D0AE File Offset: 0x0001B2AE
		// (set) Token: 0x06000A1E RID: 2590 RVA: 0x0001D0C9 File Offset: 0x0001B2C9
		[XmlIgnore]
		public MailForwarding MailForwarding
		{
			get
			{
				if (this.internalMailForwarding == null)
				{
					this.internalMailForwarding = new MailForwarding();
				}
				return this.internalMailForwarding;
			}
			set
			{
				this.internalMailForwarding = value;
			}
		}

		// Token: 0x04000590 RID: 1424
		[XmlElement(ElementName = "AlertState", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public AlertStateType internalAlertState;

		// Token: 0x04000591 RID: 1425
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalAlertStateSpecified;

		// Token: 0x04000592 RID: 1426
		[XmlElement(ElementName = "ConfirmSent", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public BitType internalConfirmSent;

		// Token: 0x04000593 RID: 1427
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalConfirmSentSpecified;

		// Token: 0x04000594 RID: 1428
		[XmlElement(ElementName = "HeaderDisplay", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public HeaderDisplayType internalHeaderDisplay;

		// Token: 0x04000595 RID: 1429
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalHeaderDisplaySpecified;

		// Token: 0x04000596 RID: 1430
		[XmlElement(ElementName = "IncludeOriginalInReply", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IncludeOriginalInReplyType internalIncludeOriginalInReply;

		// Token: 0x04000597 RID: 1431
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalIncludeOriginalInReplySpecified;

		// Token: 0x04000598 RID: 1432
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "JunkLevel", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public JunkLevelType internalJunkLevel;

		// Token: 0x04000599 RID: 1433
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalJunkLevelSpecified;

		// Token: 0x0400059A RID: 1434
		[XmlElement(ElementName = "JunkMailDestination", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public JunkMailDestinationType internalJunkMailDestination;

		// Token: 0x0400059B RID: 1435
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalJunkMailDestinationSpecified;

		// Token: 0x0400059C RID: 1436
		[XmlElement(ElementName = "ReplyIndicator", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ReplyIndicatorType internalReplyIndicator;

		// Token: 0x0400059D RID: 1437
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalReplyIndicatorSpecified;

		// Token: 0x0400059E RID: 1438
		[XmlElement(ElementName = "ReplyToAddress", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalReplyToAddress;

		// Token: 0x0400059F RID: 1439
		[XmlElement(ElementName = "SaveSentMail", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public BitType internalSaveSentMail;

		// Token: 0x040005A0 RID: 1440
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalSaveSentMailSpecified;

		// Token: 0x040005A1 RID: 1441
		[XmlElement(ElementName = "UseReplyToAddress", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public BitType internalUseReplyToAddress;

		// Token: 0x040005A2 RID: 1442
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalUseReplyToAddressSpecified;

		// Token: 0x040005A3 RID: 1443
		[XmlElement(Type = typeof(VacationResponse), ElementName = "VacationResponse", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public VacationResponse internalVacationResponse;

		// Token: 0x040005A4 RID: 1444
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(MailForwarding), ElementName = "MailForwarding", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public MailForwarding internalMailForwarding;
	}
}
