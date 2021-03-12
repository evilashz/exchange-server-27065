using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000078 RID: 120
	[DataContract]
	public class SetCalendarWorkflowConfiguration : SetObjectProperties
	{
		// Token: 0x17001887 RID: 6279
		// (get) Token: 0x06001B2F RID: 6959 RVA: 0x0005694B File Offset: 0x00054B4B
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-CalendarProcessing";
			}
		}

		// Token: 0x17001888 RID: 6280
		// (get) Token: 0x06001B30 RID: 6960 RVA: 0x00056952 File Offset: 0x00054B52
		public override string RbacScope
		{
			get
			{
				return "@W:Self";
			}
		}

		// Token: 0x17001889 RID: 6281
		// (get) Token: 0x06001B31 RID: 6961 RVA: 0x00056959 File Offset: 0x00054B59
		// (set) Token: 0x06001B32 RID: 6962 RVA: 0x0005696B File Offset: 0x00054B6B
		[DataMember]
		public string AutomateProcessing
		{
			get
			{
				return (string)base["AutomateProcessing"];
			}
			set
			{
				base["AutomateProcessing"] = value;
			}
		}

		// Token: 0x1700188A RID: 6282
		// (get) Token: 0x06001B33 RID: 6963 RVA: 0x00056979 File Offset: 0x00054B79
		// (set) Token: 0x06001B34 RID: 6964 RVA: 0x00056995 File Offset: 0x00054B95
		[DataMember]
		public bool RemoveOldMeetingMessages
		{
			get
			{
				return (bool)(base["RemoveOldMeetingMessages"] ?? false);
			}
			set
			{
				base["RemoveOldMeetingMessages"] = value;
			}
		}

		// Token: 0x1700188B RID: 6283
		// (get) Token: 0x06001B35 RID: 6965 RVA: 0x000569A8 File Offset: 0x00054BA8
		// (set) Token: 0x06001B36 RID: 6966 RVA: 0x000569C4 File Offset: 0x00054BC4
		[DataMember]
		public bool AddNewRequestsTentatively
		{
			get
			{
				return (bool)(base["AddNewRequestsTentatively"] ?? false);
			}
			set
			{
				base["AddNewRequestsTentatively"] = value;
			}
		}

		// Token: 0x1700188C RID: 6284
		// (get) Token: 0x06001B37 RID: 6967 RVA: 0x000569D7 File Offset: 0x00054BD7
		// (set) Token: 0x06001B38 RID: 6968 RVA: 0x000569F3 File Offset: 0x00054BF3
		[DataMember]
		public bool ProcessExternalMeetingMessages
		{
			get
			{
				return (bool)(base["ProcessExternalMeetingMessages"] ?? false);
			}
			set
			{
				base["ProcessExternalMeetingMessages"] = value;
			}
		}

		// Token: 0x1700188D RID: 6285
		// (get) Token: 0x06001B39 RID: 6969 RVA: 0x00056A06 File Offset: 0x00054C06
		// (set) Token: 0x06001B3A RID: 6970 RVA: 0x00056A22 File Offset: 0x00054C22
		[DataMember]
		public bool RemoveForwardedMeetingNotifications
		{
			get
			{
				return (bool)(base["RemoveForwardedMeetingNotifications"] ?? false);
			}
			set
			{
				base["RemoveForwardedMeetingNotifications"] = value;
			}
		}
	}
}
