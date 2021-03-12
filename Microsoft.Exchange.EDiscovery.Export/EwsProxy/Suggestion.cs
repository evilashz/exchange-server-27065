using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000275 RID: 629
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class Suggestion
	{
		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x0600175F RID: 5983 RVA: 0x0002750A File Offset: 0x0002570A
		// (set) Token: 0x06001760 RID: 5984 RVA: 0x00027512 File Offset: 0x00025712
		public DateTime MeetingTime
		{
			get
			{
				return this.meetingTimeField;
			}
			set
			{
				this.meetingTimeField = value;
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06001761 RID: 5985 RVA: 0x0002751B File Offset: 0x0002571B
		// (set) Token: 0x06001762 RID: 5986 RVA: 0x00027523 File Offset: 0x00025723
		public bool IsWorkTime
		{
			get
			{
				return this.isWorkTimeField;
			}
			set
			{
				this.isWorkTimeField = value;
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06001763 RID: 5987 RVA: 0x0002752C File Offset: 0x0002572C
		// (set) Token: 0x06001764 RID: 5988 RVA: 0x00027534 File Offset: 0x00025734
		public SuggestionQuality SuggestionQuality
		{
			get
			{
				return this.suggestionQualityField;
			}
			set
			{
				this.suggestionQualityField = value;
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06001765 RID: 5989 RVA: 0x0002753D File Offset: 0x0002573D
		// (set) Token: 0x06001766 RID: 5990 RVA: 0x00027545 File Offset: 0x00025745
		[XmlArrayItem(typeof(TooBigGroupAttendeeConflictData))]
		[XmlArrayItem(typeof(GroupAttendeeConflictData))]
		[XmlArrayItem(typeof(UnknownAttendeeConflictData))]
		[XmlArrayItem(typeof(IndividualAttendeeConflictData))]
		public AttendeeConflictData[] AttendeeConflictDataArray
		{
			get
			{
				return this.attendeeConflictDataArrayField;
			}
			set
			{
				this.attendeeConflictDataArrayField = value;
			}
		}

		// Token: 0x04000FCA RID: 4042
		private DateTime meetingTimeField;

		// Token: 0x04000FCB RID: 4043
		private bool isWorkTimeField;

		// Token: 0x04000FCC RID: 4044
		private SuggestionQuality suggestionQualityField;

		// Token: 0x04000FCD RID: 4045
		private AttendeeConflictData[] attendeeConflictDataArrayField;
	}
}
