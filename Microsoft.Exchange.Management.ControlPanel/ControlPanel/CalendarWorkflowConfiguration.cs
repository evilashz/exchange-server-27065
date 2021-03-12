using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000077 RID: 119
	[DataContract]
	public class CalendarWorkflowConfiguration : BaseRow
	{
		// Token: 0x06001B22 RID: 6946 RVA: 0x000568BC File Offset: 0x00054ABC
		public CalendarWorkflowConfiguration(CalendarConfiguration calendarConfiguration) : base(calendarConfiguration)
		{
			this.CalendarConfiguration = calendarConfiguration;
		}

		// Token: 0x17001881 RID: 6273
		// (get) Token: 0x06001B23 RID: 6947 RVA: 0x000568CC File Offset: 0x00054ACC
		// (set) Token: 0x06001B24 RID: 6948 RVA: 0x000568D4 File Offset: 0x00054AD4
		public CalendarConfiguration CalendarConfiguration { get; private set; }

		// Token: 0x17001882 RID: 6274
		// (get) Token: 0x06001B25 RID: 6949 RVA: 0x000568DD File Offset: 0x00054ADD
		// (set) Token: 0x06001B26 RID: 6950 RVA: 0x000568F4 File Offset: 0x00054AF4
		[DataMember]
		public string AutomateProcessing
		{
			get
			{
				return this.CalendarConfiguration.AutomateProcessing.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001883 RID: 6275
		// (get) Token: 0x06001B27 RID: 6951 RVA: 0x000568FB File Offset: 0x00054AFB
		// (set) Token: 0x06001B28 RID: 6952 RVA: 0x00056908 File Offset: 0x00054B08
		[DataMember]
		public bool RemoveOldMeetingMessages
		{
			get
			{
				return this.CalendarConfiguration.RemoveOldMeetingMessages;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001884 RID: 6276
		// (get) Token: 0x06001B29 RID: 6953 RVA: 0x0005690F File Offset: 0x00054B0F
		// (set) Token: 0x06001B2A RID: 6954 RVA: 0x0005691C File Offset: 0x00054B1C
		[DataMember]
		public bool AddNewRequestsTentatively
		{
			get
			{
				return this.CalendarConfiguration.AddNewRequestsTentatively;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001885 RID: 6277
		// (get) Token: 0x06001B2B RID: 6955 RVA: 0x00056923 File Offset: 0x00054B23
		// (set) Token: 0x06001B2C RID: 6956 RVA: 0x00056930 File Offset: 0x00054B30
		[DataMember]
		public bool ProcessExternalMeetingMessages
		{
			get
			{
				return this.CalendarConfiguration.ProcessExternalMeetingMessages;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001886 RID: 6278
		// (get) Token: 0x06001B2D RID: 6957 RVA: 0x00056937 File Offset: 0x00054B37
		// (set) Token: 0x06001B2E RID: 6958 RVA: 0x00056944 File Offset: 0x00054B44
		[DataMember]
		public bool RemoveForwardedMeetingNotifications
		{
			get
			{
				return this.CalendarConfiguration.RemoveForwardedMeetingNotifications;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}
	}
}
