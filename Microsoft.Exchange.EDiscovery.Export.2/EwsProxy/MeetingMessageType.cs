using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200015A RID: 346
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(MeetingRequestMessageType))]
	[XmlInclude(typeof(MeetingCancellationMessageType))]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlInclude(typeof(MeetingResponseMessageType))]
	[DebuggerStepThrough]
	[Serializable]
	public class MeetingMessageType : MessageType
	{
		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x00022E05 File Offset: 0x00021005
		// (set) Token: 0x06000EF7 RID: 3831 RVA: 0x00022E0D File Offset: 0x0002100D
		public ItemIdType AssociatedCalendarItemId
		{
			get
			{
				return this.associatedCalendarItemIdField;
			}
			set
			{
				this.associatedCalendarItemIdField = value;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x00022E16 File Offset: 0x00021016
		// (set) Token: 0x06000EF9 RID: 3833 RVA: 0x00022E1E File Offset: 0x0002101E
		public bool IsDelegated
		{
			get
			{
				return this.isDelegatedField;
			}
			set
			{
				this.isDelegatedField = value;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x00022E27 File Offset: 0x00021027
		// (set) Token: 0x06000EFB RID: 3835 RVA: 0x00022E2F File Offset: 0x0002102F
		[XmlIgnore]
		public bool IsDelegatedSpecified
		{
			get
			{
				return this.isDelegatedFieldSpecified;
			}
			set
			{
				this.isDelegatedFieldSpecified = value;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x00022E38 File Offset: 0x00021038
		// (set) Token: 0x06000EFD RID: 3837 RVA: 0x00022E40 File Offset: 0x00021040
		public bool IsOutOfDate
		{
			get
			{
				return this.isOutOfDateField;
			}
			set
			{
				this.isOutOfDateField = value;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x00022E49 File Offset: 0x00021049
		// (set) Token: 0x06000EFF RID: 3839 RVA: 0x00022E51 File Offset: 0x00021051
		[XmlIgnore]
		public bool IsOutOfDateSpecified
		{
			get
			{
				return this.isOutOfDateFieldSpecified;
			}
			set
			{
				this.isOutOfDateFieldSpecified = value;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x00022E5A File Offset: 0x0002105A
		// (set) Token: 0x06000F01 RID: 3841 RVA: 0x00022E62 File Offset: 0x00021062
		public bool HasBeenProcessed
		{
			get
			{
				return this.hasBeenProcessedField;
			}
			set
			{
				this.hasBeenProcessedField = value;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x00022E6B File Offset: 0x0002106B
		// (set) Token: 0x06000F03 RID: 3843 RVA: 0x00022E73 File Offset: 0x00021073
		[XmlIgnore]
		public bool HasBeenProcessedSpecified
		{
			get
			{
				return this.hasBeenProcessedFieldSpecified;
			}
			set
			{
				this.hasBeenProcessedFieldSpecified = value;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x00022E7C File Offset: 0x0002107C
		// (set) Token: 0x06000F05 RID: 3845 RVA: 0x00022E84 File Offset: 0x00021084
		public ResponseTypeType ResponseType
		{
			get
			{
				return this.responseTypeField;
			}
			set
			{
				this.responseTypeField = value;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x00022E8D File Offset: 0x0002108D
		// (set) Token: 0x06000F07 RID: 3847 RVA: 0x00022E95 File Offset: 0x00021095
		[XmlIgnore]
		public bool ResponseTypeSpecified
		{
			get
			{
				return this.responseTypeFieldSpecified;
			}
			set
			{
				this.responseTypeFieldSpecified = value;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x00022E9E File Offset: 0x0002109E
		// (set) Token: 0x06000F09 RID: 3849 RVA: 0x00022EA6 File Offset: 0x000210A6
		public string UID
		{
			get
			{
				return this.uIDField;
			}
			set
			{
				this.uIDField = value;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x00022EAF File Offset: 0x000210AF
		// (set) Token: 0x06000F0B RID: 3851 RVA: 0x00022EB7 File Offset: 0x000210B7
		public DateTime RecurrenceId
		{
			get
			{
				return this.recurrenceIdField;
			}
			set
			{
				this.recurrenceIdField = value;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x00022EC0 File Offset: 0x000210C0
		// (set) Token: 0x06000F0D RID: 3853 RVA: 0x00022EC8 File Offset: 0x000210C8
		[XmlIgnore]
		public bool RecurrenceIdSpecified
		{
			get
			{
				return this.recurrenceIdFieldSpecified;
			}
			set
			{
				this.recurrenceIdFieldSpecified = value;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x00022ED1 File Offset: 0x000210D1
		// (set) Token: 0x06000F0F RID: 3855 RVA: 0x00022ED9 File Offset: 0x000210D9
		public DateTime DateTimeStamp
		{
			get
			{
				return this.dateTimeStampField;
			}
			set
			{
				this.dateTimeStampField = value;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x00022EE2 File Offset: 0x000210E2
		// (set) Token: 0x06000F11 RID: 3857 RVA: 0x00022EEA File Offset: 0x000210EA
		[XmlIgnore]
		public bool DateTimeStampSpecified
		{
			get
			{
				return this.dateTimeStampFieldSpecified;
			}
			set
			{
				this.dateTimeStampFieldSpecified = value;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x00022EF3 File Offset: 0x000210F3
		// (set) Token: 0x06000F13 RID: 3859 RVA: 0x00022EFB File Offset: 0x000210FB
		public bool IsOrganizer
		{
			get
			{
				return this.isOrganizerField;
			}
			set
			{
				this.isOrganizerField = value;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x00022F04 File Offset: 0x00021104
		// (set) Token: 0x06000F15 RID: 3861 RVA: 0x00022F0C File Offset: 0x0002110C
		[XmlIgnore]
		public bool IsOrganizerSpecified
		{
			get
			{
				return this.isOrganizerFieldSpecified;
			}
			set
			{
				this.isOrganizerFieldSpecified = value;
			}
		}

		// Token: 0x04000A71 RID: 2673
		private ItemIdType associatedCalendarItemIdField;

		// Token: 0x04000A72 RID: 2674
		private bool isDelegatedField;

		// Token: 0x04000A73 RID: 2675
		private bool isDelegatedFieldSpecified;

		// Token: 0x04000A74 RID: 2676
		private bool isOutOfDateField;

		// Token: 0x04000A75 RID: 2677
		private bool isOutOfDateFieldSpecified;

		// Token: 0x04000A76 RID: 2678
		private bool hasBeenProcessedField;

		// Token: 0x04000A77 RID: 2679
		private bool hasBeenProcessedFieldSpecified;

		// Token: 0x04000A78 RID: 2680
		private ResponseTypeType responseTypeField;

		// Token: 0x04000A79 RID: 2681
		private bool responseTypeFieldSpecified;

		// Token: 0x04000A7A RID: 2682
		private string uIDField;

		// Token: 0x04000A7B RID: 2683
		private DateTime recurrenceIdField;

		// Token: 0x04000A7C RID: 2684
		private bool recurrenceIdFieldSpecified;

		// Token: 0x04000A7D RID: 2685
		private DateTime dateTimeStampField;

		// Token: 0x04000A7E RID: 2686
		private bool dateTimeStampFieldSpecified;

		// Token: 0x04000A7F RID: 2687
		private bool isOrganizerField;

		// Token: 0x04000A80 RID: 2688
		private bool isOrganizerFieldSpecified;
	}
}
