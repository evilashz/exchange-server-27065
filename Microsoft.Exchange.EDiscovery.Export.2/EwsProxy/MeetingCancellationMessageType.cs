using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200015B RID: 347
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[Serializable]
	public class MeetingCancellationMessageType : MeetingMessageType
	{
		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06000F17 RID: 3863 RVA: 0x00022F1D File Offset: 0x0002111D
		// (set) Token: 0x06000F18 RID: 3864 RVA: 0x00022F25 File Offset: 0x00021125
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

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06000F19 RID: 3865 RVA: 0x00022F2E File Offset: 0x0002112E
		// (set) Token: 0x06000F1A RID: 3866 RVA: 0x00022F36 File Offset: 0x00021136
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

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06000F1B RID: 3867 RVA: 0x00022F3F File Offset: 0x0002113F
		// (set) Token: 0x06000F1C RID: 3868 RVA: 0x00022F47 File Offset: 0x00021147
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

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x00022F50 File Offset: 0x00021150
		// (set) Token: 0x06000F1E RID: 3870 RVA: 0x00022F58 File Offset: 0x00021158
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

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x00022F61 File Offset: 0x00021161
		// (set) Token: 0x06000F20 RID: 3872 RVA: 0x00022F69 File Offset: 0x00021169
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

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x00022F72 File Offset: 0x00021172
		// (set) Token: 0x06000F22 RID: 3874 RVA: 0x00022F7A File Offset: 0x0002117A
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

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x00022F83 File Offset: 0x00021183
		// (set) Token: 0x06000F24 RID: 3876 RVA: 0x00022F8B File Offset: 0x0002118B
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

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x00022F94 File Offset: 0x00021194
		// (set) Token: 0x06000F26 RID: 3878 RVA: 0x00022F9C File Offset: 0x0002119C
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

		// Token: 0x04000A81 RID: 2689
		private DateTime startField;

		// Token: 0x04000A82 RID: 2690
		private bool startFieldSpecified;

		// Token: 0x04000A83 RID: 2691
		private DateTime endField;

		// Token: 0x04000A84 RID: 2692
		private bool endFieldSpecified;

		// Token: 0x04000A85 RID: 2693
		private string locationField;

		// Token: 0x04000A86 RID: 2694
		private RecurrenceType recurrenceField;

		// Token: 0x04000A87 RID: 2695
		private string calendarItemTypeField;

		// Token: 0x04000A88 RID: 2696
		private EnhancedLocationType enhancedLocationField;
	}
}
