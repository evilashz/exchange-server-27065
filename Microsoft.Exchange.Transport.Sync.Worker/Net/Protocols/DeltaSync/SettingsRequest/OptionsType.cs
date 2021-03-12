using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Net.Protocols.DeltaSync.HMTypes;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x02000122 RID: 290
	[XmlType(TypeName = "OptionsType", Namespace = "HMSETTINGS:")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Serializable]
	public class OptionsType
	{
		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x0001BD7D File Offset: 0x00019F7D
		// (set) Token: 0x060008AB RID: 2219 RVA: 0x0001BD85 File Offset: 0x00019F85
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

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x0001BD95 File Offset: 0x00019F95
		// (set) Token: 0x060008AD RID: 2221 RVA: 0x0001BD9D File Offset: 0x00019F9D
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

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x0001BDAD File Offset: 0x00019FAD
		// (set) Token: 0x060008AF RID: 2223 RVA: 0x0001BDB5 File Offset: 0x00019FB5
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

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x0001BDC5 File Offset: 0x00019FC5
		// (set) Token: 0x060008B1 RID: 2225 RVA: 0x0001BDCD File Offset: 0x00019FCD
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

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x0001BDDD File Offset: 0x00019FDD
		// (set) Token: 0x060008B3 RID: 2227 RVA: 0x0001BDE5 File Offset: 0x00019FE5
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

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x0001BDF5 File Offset: 0x00019FF5
		// (set) Token: 0x060008B5 RID: 2229 RVA: 0x0001BDFD File Offset: 0x00019FFD
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

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x0001BE0D File Offset: 0x0001A00D
		// (set) Token: 0x060008B7 RID: 2231 RVA: 0x0001BE15 File Offset: 0x0001A015
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

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x0001BE25 File Offset: 0x0001A025
		// (set) Token: 0x060008B9 RID: 2233 RVA: 0x0001BE2D File Offset: 0x0001A02D
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

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x0001BE36 File Offset: 0x0001A036
		// (set) Token: 0x060008BB RID: 2235 RVA: 0x0001BE3E File Offset: 0x0001A03E
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

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x0001BE4E File Offset: 0x0001A04E
		// (set) Token: 0x060008BD RID: 2237 RVA: 0x0001BE56 File Offset: 0x0001A056
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

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x0001BE66 File Offset: 0x0001A066
		// (set) Token: 0x060008BF RID: 2239 RVA: 0x0001BE81 File Offset: 0x0001A081
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

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x0001BE8A File Offset: 0x0001A08A
		// (set) Token: 0x060008C1 RID: 2241 RVA: 0x0001BEA5 File Offset: 0x0001A0A5
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

		// Token: 0x040004B7 RID: 1207
		[XmlElement(ElementName = "AlertState", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public AlertStateType internalAlertState;

		// Token: 0x040004B8 RID: 1208
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalAlertStateSpecified;

		// Token: 0x040004B9 RID: 1209
		[XmlElement(ElementName = "ConfirmSent", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public BitType internalConfirmSent;

		// Token: 0x040004BA RID: 1210
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalConfirmSentSpecified;

		// Token: 0x040004BB RID: 1211
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "HeaderDisplay", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public HeaderDisplayType internalHeaderDisplay;

		// Token: 0x040004BC RID: 1212
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalHeaderDisplaySpecified;

		// Token: 0x040004BD RID: 1213
		[XmlElement(ElementName = "IncludeOriginalInReply", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IncludeOriginalInReplyType internalIncludeOriginalInReply;

		// Token: 0x040004BE RID: 1214
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalIncludeOriginalInReplySpecified;

		// Token: 0x040004BF RID: 1215
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "JunkLevel", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public JunkLevelType internalJunkLevel;

		// Token: 0x040004C0 RID: 1216
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalJunkLevelSpecified;

		// Token: 0x040004C1 RID: 1217
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "JunkMailDestination", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public JunkMailDestinationType internalJunkMailDestination;

		// Token: 0x040004C2 RID: 1218
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalJunkMailDestinationSpecified;

		// Token: 0x040004C3 RID: 1219
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "ReplyIndicator", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public ReplyIndicatorType internalReplyIndicator;

		// Token: 0x040004C4 RID: 1220
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalReplyIndicatorSpecified;

		// Token: 0x040004C5 RID: 1221
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "ReplyToAddress", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "HMSETTINGS:")]
		public string internalReplyToAddress;

		// Token: 0x040004C6 RID: 1222
		[XmlElement(ElementName = "SaveSentMail", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public BitType internalSaveSentMail;

		// Token: 0x040004C7 RID: 1223
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalSaveSentMailSpecified;

		// Token: 0x040004C8 RID: 1224
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "UseReplyToAddress", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public BitType internalUseReplyToAddress;

		// Token: 0x040004C9 RID: 1225
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalUseReplyToAddressSpecified;

		// Token: 0x040004CA RID: 1226
		[XmlElement(Type = typeof(VacationResponse), ElementName = "VacationResponse", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public VacationResponse internalVacationResponse;

		// Token: 0x040004CB RID: 1227
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(MailForwarding), ElementName = "MailForwarding", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public MailForwarding internalMailForwarding;
	}
}
