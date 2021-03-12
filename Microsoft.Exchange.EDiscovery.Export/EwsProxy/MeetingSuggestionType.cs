using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000183 RID: 387
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class MeetingSuggestionType : EntityType
	{
		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x00023D0F File Offset: 0x00021F0F
		// (set) Token: 0x060010BE RID: 4286 RVA: 0x00023D17 File Offset: 0x00021F17
		[XmlArrayItem("EmailUser", IsNullable = false)]
		public EmailUserType[] Attendees
		{
			get
			{
				return this.attendeesField;
			}
			set
			{
				this.attendeesField = value;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x00023D20 File Offset: 0x00021F20
		// (set) Token: 0x060010C0 RID: 4288 RVA: 0x00023D28 File Offset: 0x00021F28
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

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x060010C1 RID: 4289 RVA: 0x00023D31 File Offset: 0x00021F31
		// (set) Token: 0x060010C2 RID: 4290 RVA: 0x00023D39 File Offset: 0x00021F39
		public string Subject
		{
			get
			{
				return this.subjectField;
			}
			set
			{
				this.subjectField = value;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x00023D42 File Offset: 0x00021F42
		// (set) Token: 0x060010C4 RID: 4292 RVA: 0x00023D4A File Offset: 0x00021F4A
		public string MeetingString
		{
			get
			{
				return this.meetingStringField;
			}
			set
			{
				this.meetingStringField = value;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x060010C5 RID: 4293 RVA: 0x00023D53 File Offset: 0x00021F53
		// (set) Token: 0x060010C6 RID: 4294 RVA: 0x00023D5B File Offset: 0x00021F5B
		public DateTime StartTime
		{
			get
			{
				return this.startTimeField;
			}
			set
			{
				this.startTimeField = value;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x060010C7 RID: 4295 RVA: 0x00023D64 File Offset: 0x00021F64
		// (set) Token: 0x060010C8 RID: 4296 RVA: 0x00023D6C File Offset: 0x00021F6C
		[XmlIgnore]
		public bool StartTimeSpecified
		{
			get
			{
				return this.startTimeFieldSpecified;
			}
			set
			{
				this.startTimeFieldSpecified = value;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x060010C9 RID: 4297 RVA: 0x00023D75 File Offset: 0x00021F75
		// (set) Token: 0x060010CA RID: 4298 RVA: 0x00023D7D File Offset: 0x00021F7D
		public DateTime EndTime
		{
			get
			{
				return this.endTimeField;
			}
			set
			{
				this.endTimeField = value;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x060010CB RID: 4299 RVA: 0x00023D86 File Offset: 0x00021F86
		// (set) Token: 0x060010CC RID: 4300 RVA: 0x00023D8E File Offset: 0x00021F8E
		[XmlIgnore]
		public bool EndTimeSpecified
		{
			get
			{
				return this.endTimeFieldSpecified;
			}
			set
			{
				this.endTimeFieldSpecified = value;
			}
		}

		// Token: 0x04000B65 RID: 2917
		private EmailUserType[] attendeesField;

		// Token: 0x04000B66 RID: 2918
		private string locationField;

		// Token: 0x04000B67 RID: 2919
		private string subjectField;

		// Token: 0x04000B68 RID: 2920
		private string meetingStringField;

		// Token: 0x04000B69 RID: 2921
		private DateTime startTimeField;

		// Token: 0x04000B6A RID: 2922
		private bool startTimeFieldSpecified;

		// Token: 0x04000B6B RID: 2923
		private DateTime endTimeField;

		// Token: 0x04000B6C RID: 2924
		private bool endTimeFieldSpecified;
	}
}
