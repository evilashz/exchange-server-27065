using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200015C RID: 348
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class MeetingResponseMessageType : MeetingMessageType
	{
		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x00022FAD File Offset: 0x000211AD
		// (set) Token: 0x06000F29 RID: 3881 RVA: 0x00022FB5 File Offset: 0x000211B5
		public DateTime Start
		{
			get
			{
				return this.startField;
			}
			set
			{
				this.startField = value;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06000F2A RID: 3882 RVA: 0x00022FBE File Offset: 0x000211BE
		// (set) Token: 0x06000F2B RID: 3883 RVA: 0x00022FC6 File Offset: 0x000211C6
		[XmlIgnore]
		public bool StartSpecified
		{
			get
			{
				return this.startFieldSpecified;
			}
			set
			{
				this.startFieldSpecified = value;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x00022FCF File Offset: 0x000211CF
		// (set) Token: 0x06000F2D RID: 3885 RVA: 0x00022FD7 File Offset: 0x000211D7
		public DateTime End
		{
			get
			{
				return this.endField;
			}
			set
			{
				this.endField = value;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x00022FE0 File Offset: 0x000211E0
		// (set) Token: 0x06000F2F RID: 3887 RVA: 0x00022FE8 File Offset: 0x000211E8
		[XmlIgnore]
		public bool EndSpecified
		{
			get
			{
				return this.endFieldSpecified;
			}
			set
			{
				this.endFieldSpecified = value;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x00022FF1 File Offset: 0x000211F1
		// (set) Token: 0x06000F31 RID: 3889 RVA: 0x00022FF9 File Offset: 0x000211F9
		public string Location
		{
			get
			{
				return this.locationField;
			}
			set
			{
				this.locationField = value;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06000F32 RID: 3890 RVA: 0x00023002 File Offset: 0x00021202
		// (set) Token: 0x06000F33 RID: 3891 RVA: 0x0002300A File Offset: 0x0002120A
		public RecurrenceType Recurrence
		{
			get
			{
				return this.recurrenceField;
			}
			set
			{
				this.recurrenceField = value;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06000F34 RID: 3892 RVA: 0x00023013 File Offset: 0x00021213
		// (set) Token: 0x06000F35 RID: 3893 RVA: 0x0002301B File Offset: 0x0002121B
		public string CalendarItemType
		{
			get
			{
				return this.calendarItemTypeField;
			}
			set
			{
				this.calendarItemTypeField = value;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x00023024 File Offset: 0x00021224
		// (set) Token: 0x06000F37 RID: 3895 RVA: 0x0002302C File Offset: 0x0002122C
		public DateTime ProposedStart
		{
			get
			{
				return this.proposedStartField;
			}
			set
			{
				this.proposedStartField = value;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06000F38 RID: 3896 RVA: 0x00023035 File Offset: 0x00021235
		// (set) Token: 0x06000F39 RID: 3897 RVA: 0x0002303D File Offset: 0x0002123D
		[XmlIgnore]
		public bool ProposedStartSpecified
		{
			get
			{
				return this.proposedStartFieldSpecified;
			}
			set
			{
				this.proposedStartFieldSpecified = value;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06000F3A RID: 3898 RVA: 0x00023046 File Offset: 0x00021246
		// (set) Token: 0x06000F3B RID: 3899 RVA: 0x0002304E File Offset: 0x0002124E
		public DateTime ProposedEnd
		{
			get
			{
				return this.proposedEndField;
			}
			set
			{
				this.proposedEndField = value;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x00023057 File Offset: 0x00021257
		// (set) Token: 0x06000F3D RID: 3901 RVA: 0x0002305F File Offset: 0x0002125F
		[XmlIgnore]
		public bool ProposedEndSpecified
		{
			get
			{
				return this.proposedEndFieldSpecified;
			}
			set
			{
				this.proposedEndFieldSpecified = value;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x00023068 File Offset: 0x00021268
		// (set) Token: 0x06000F3F RID: 3903 RVA: 0x00023070 File Offset: 0x00021270
		public EnhancedLocationType EnhancedLocation
		{
			get
			{
				return this.enhancedLocationField;
			}
			set
			{
				this.enhancedLocationField = value;
			}
		}

		// Token: 0x04000A89 RID: 2697
		private DateTime startField;

		// Token: 0x04000A8A RID: 2698
		private bool startFieldSpecified;

		// Token: 0x04000A8B RID: 2699
		private DateTime endField;

		// Token: 0x04000A8C RID: 2700
		private bool endFieldSpecified;

		// Token: 0x04000A8D RID: 2701
		private string locationField;

		// Token: 0x04000A8E RID: 2702
		private RecurrenceType recurrenceField;

		// Token: 0x04000A8F RID: 2703
		private string calendarItemTypeField;

		// Token: 0x04000A90 RID: 2704
		private DateTime proposedStartField;

		// Token: 0x04000A91 RID: 2705
		private bool proposedStartFieldSpecified;

		// Token: 0x04000A92 RID: 2706
		private DateTime proposedEndField;

		// Token: 0x04000A93 RID: 2707
		private bool proposedEndFieldSpecified;

		// Token: 0x04000A94 RID: 2708
		private EnhancedLocationType enhancedLocationField;
	}
}
