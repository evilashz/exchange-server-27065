using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Eas.Model.Common.Email;
using Microsoft.Exchange.Connections.Eas.Model.Request.AirSyncBase;
using Microsoft.Exchange.Connections.Eas.Model.Request.Calendar;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.AirSync
{
	// Token: 0x02000092 RID: 146
	[XmlType(Namespace = "AirSync", TypeName = "ApplicationData")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class ApplicationData : ICalendarData
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000976D File Offset: 0x0000796D
		// (set) Token: 0x060002CB RID: 715 RVA: 0x00009775 File Offset: 0x00007975
		[XmlElement(ElementName = "Body", Namespace = "AirSyncBase")]
		public Body Body { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000977E File Offset: 0x0000797E
		// (set) Token: 0x060002CD RID: 717 RVA: 0x00009786 File Offset: 0x00007986
		[XmlElement(ElementName = "NativeBodyType", Namespace = "AirSyncBase")]
		public byte? NativeBodyType { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000978F File Offset: 0x0000798F
		// (set) Token: 0x060002CF RID: 719 RVA: 0x00009797 File Offset: 0x00007997
		[XmlElement(ElementName = "DateReceived", Namespace = "Email")]
		public string DateReceived { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x000097A0 File Offset: 0x000079A0
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x000097A8 File Offset: 0x000079A8
		[XmlElement(ElementName = "Flag", Namespace = "Email")]
		public Flag Flag { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x000097B1 File Offset: 0x000079B1
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x000097B9 File Offset: 0x000079B9
		[XmlElement(ElementName = "From", Namespace = "Email")]
		public string From { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x000097C2 File Offset: 0x000079C2
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x000097CA File Offset: 0x000079CA
		[XmlElement(ElementName = "Importance", Namespace = "Email")]
		public byte? Importance { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x000097D3 File Offset: 0x000079D3
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x000097DB File Offset: 0x000079DB
		[XmlElement(ElementName = "InternetCpid", Namespace = "Email")]
		public int? InternetCpid { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x000097E4 File Offset: 0x000079E4
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x000097EC File Offset: 0x000079EC
		[XmlElement(ElementName = "Read", Namespace = "Email")]
		public byte? Read { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002DA RID: 730 RVA: 0x000097F5 File Offset: 0x000079F5
		// (set) Token: 0x060002DB RID: 731 RVA: 0x000097FD File Offset: 0x000079FD
		[XmlElement(ElementName = "To", Namespace = "Email")]
		public string To { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060002DC RID: 732 RVA: 0x00009806 File Offset: 0x00007A06
		// (set) Token: 0x060002DD RID: 733 RVA: 0x0000980E File Offset: 0x00007A0E
		[XmlElement(ElementName = "TimeZone", Namespace = "Calendar")]
		public string TimeZone { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00009817 File Offset: 0x00007A17
		// (set) Token: 0x060002DF RID: 735 RVA: 0x0000981F File Offset: 0x00007A1F
		[XmlElement(ElementName = "AllDayEvent", Namespace = "Calendar")]
		public byte? AllDayEvent { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x00009828 File Offset: 0x00007A28
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x00009830 File Offset: 0x00007A30
		[XmlElement(ElementName = "BusyStatus", Namespace = "Calendar")]
		public byte? BusyStatus { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00009839 File Offset: 0x00007A39
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x00009841 File Offset: 0x00007A41
		[XmlElement(ElementName = "OrganizerName", Namespace = "Calendar")]
		public string OrganizerName { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000984A File Offset: 0x00007A4A
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x00009852 File Offset: 0x00007A52
		[XmlElement(ElementName = "OrganizerEmail", Namespace = "Calendar")]
		public string OrganizerEmail { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000985B File Offset: 0x00007A5B
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x00009863 File Offset: 0x00007A63
		[XmlElement(ElementName = "DtStamp", Namespace = "Calendar")]
		public string DtStamp { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000986C File Offset: 0x00007A6C
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x00009874 File Offset: 0x00007A74
		[XmlElement(ElementName = "EndTime", Namespace = "Calendar")]
		public string EndTime { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000987D File Offset: 0x00007A7D
		// (set) Token: 0x060002EB RID: 747 RVA: 0x00009885 File Offset: 0x00007A85
		[XmlElement(ElementName = "Location", Namespace = "Calendar")]
		public string Location { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000988E File Offset: 0x00007A8E
		// (set) Token: 0x060002ED RID: 749 RVA: 0x00009896 File Offset: 0x00007A96
		[XmlElement(ElementName = "Reminder", Namespace = "Calendar")]
		public uint? Reminder { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000989F File Offset: 0x00007A9F
		// (set) Token: 0x060002EF RID: 751 RVA: 0x000098A7 File Offset: 0x00007AA7
		[XmlElement(ElementName = "Sensitivity", Namespace = "Calendar")]
		public byte? Sensitivity { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x000098B0 File Offset: 0x00007AB0
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x000098B8 File Offset: 0x00007AB8
		[XmlElement(ElementName = "Subject", Namespace = "Calendar")]
		public string CalendarSubject { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x000098C1 File Offset: 0x00007AC1
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x000098C9 File Offset: 0x00007AC9
		[XmlElement(ElementName = "StartTime", Namespace = "Calendar")]
		public string StartTime { get; set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x000098D2 File Offset: 0x00007AD2
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x000098DA File Offset: 0x00007ADA
		[XmlElement(ElementName = "UID", Namespace = "Calendar")]
		public string Uid { get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x000098E3 File Offset: 0x00007AE3
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x000098EB File Offset: 0x00007AEB
		[XmlElement(ElementName = "MeetingStatus", Namespace = "Calendar")]
		public byte? MeetingStatus { get; set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x000098F4 File Offset: 0x00007AF4
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x000098FC File Offset: 0x00007AFC
		[XmlArray(ElementName = "Attendees", Namespace = "Calendar")]
		public List<Attendee> Attendees { get; set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00009905 File Offset: 0x00007B05
		// (set) Token: 0x060002FB RID: 763 RVA: 0x0000990D File Offset: 0x00007B0D
		[XmlArray(ElementName = "Categories", Namespace = "Calendar")]
		public List<Category> CalendarCategories { get; set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060002FC RID: 764 RVA: 0x00009916 File Offset: 0x00007B16
		// (set) Token: 0x060002FD RID: 765 RVA: 0x0000991E File Offset: 0x00007B1E
		[XmlElement(ElementName = "Recurrence", Namespace = "Calendar")]
		public Recurrence Recurrence { get; set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060002FE RID: 766 RVA: 0x00009927 File Offset: 0x00007B27
		// (set) Token: 0x060002FF RID: 767 RVA: 0x0000992F File Offset: 0x00007B2F
		[XmlArray(ElementName = "Exceptions", Namespace = "Calendar")]
		public List<Microsoft.Exchange.Connections.Eas.Model.Request.Calendar.Exception> Exceptions { get; set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00009938 File Offset: 0x00007B38
		// (set) Token: 0x06000301 RID: 769 RVA: 0x00009940 File Offset: 0x00007B40
		[XmlElement(ElementName = "ResponseRequested", Namespace = "Calendar")]
		public byte? ResponseRequested { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00009949 File Offset: 0x00007B49
		// (set) Token: 0x06000303 RID: 771 RVA: 0x00009951 File Offset: 0x00007B51
		[XmlElement(ElementName = "DisallowNewTimeProposal", Namespace = "Calendar")]
		public byte? DisallowNewTimeProposal { get; set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000995A File Offset: 0x00007B5A
		// (set) Token: 0x06000305 RID: 773 RVA: 0x00009962 File Offset: 0x00007B62
		[XmlElement(ElementName = "OnlineMeetingConfLink", Namespace = "Calendar")]
		public string OnlineMeetingConferenceLink { get; set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0000996B File Offset: 0x00007B6B
		// (set) Token: 0x06000307 RID: 775 RVA: 0x00009973 File Offset: 0x00007B73
		[XmlElement(ElementName = "OnlineMeetingExternalLink", Namespace = "Calendar")]
		public string OnlineMeetingExternalLink { get; set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000997C File Offset: 0x00007B7C
		[XmlIgnore]
		public bool NativeBodyTypeSpecified
		{
			get
			{
				return this.NativeBodyType != null;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00009998 File Offset: 0x00007B98
		[XmlIgnore]
		public bool ImportanceSpecified
		{
			get
			{
				return this.Importance != null;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600030A RID: 778 RVA: 0x000099B4 File Offset: 0x00007BB4
		[XmlIgnore]
		public bool InternetCpidSpecified
		{
			get
			{
				return this.InternetCpid != null;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600030B RID: 779 RVA: 0x000099D0 File Offset: 0x00007BD0
		[XmlIgnore]
		public bool ReadSpecified
		{
			get
			{
				return this.Read != null;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600030C RID: 780 RVA: 0x000099EC File Offset: 0x00007BEC
		[XmlIgnore]
		public bool AllDayEventSpecified
		{
			get
			{
				return this.AllDayEvent != null;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00009A08 File Offset: 0x00007C08
		[XmlIgnore]
		public bool BusyStatusSpecified
		{
			get
			{
				return this.BusyStatus != null;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00009A24 File Offset: 0x00007C24
		[XmlIgnore]
		public bool ReminderSpecified
		{
			get
			{
				return this.Reminder != null;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00009A40 File Offset: 0x00007C40
		[XmlIgnore]
		public bool SensitivitySpecified
		{
			get
			{
				return this.Sensitivity != null;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00009A5C File Offset: 0x00007C5C
		[XmlIgnore]
		public bool MeetingStatusSpecified
		{
			get
			{
				return this.MeetingStatus != null;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00009A77 File Offset: 0x00007C77
		[XmlIgnore]
		public bool AttendeesSpecified
		{
			get
			{
				return this.Attendees != null;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000312 RID: 786 RVA: 0x00009A85 File Offset: 0x00007C85
		[XmlIgnore]
		public bool CalendarCategoriesSpecified
		{
			get
			{
				return this.CalendarCategories != null;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00009A93 File Offset: 0x00007C93
		[XmlIgnore]
		public bool ExceptionsSpecified
		{
			get
			{
				return this.Exceptions != null;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000314 RID: 788 RVA: 0x00009AA4 File Offset: 0x00007CA4
		[XmlIgnore]
		public bool ResponseRequestedSpecified
		{
			get
			{
				return this.ResponseRequested != null;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00009AC0 File Offset: 0x00007CC0
		[XmlIgnore]
		public bool DisallowNewTimeProposalSpecified
		{
			get
			{
				return this.DisallowNewTimeProposal != null;
			}
		}
	}
}
