using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataProviders;

namespace Microsoft.Exchange.Entities.Calendaring
{
	// Token: 0x02000010 RID: 16
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CalendarGroupIsNotEmptyException : InvalidRequestException
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00002BD5 File Offset: 0x00000DD5
		public CalendarGroupIsNotEmptyException(StoreId groupId, Guid groupClassId, string groupName, int calendarsCount) : base(CalendaringStrings.CalendarGroupIsNotEmpty(groupId, groupClassId, groupName, calendarsCount))
		{
			this.groupId = groupId;
			this.groupClassId = groupClassId;
			this.groupName = groupName;
			this.calendarsCount = calendarsCount;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002C04 File Offset: 0x00000E04
		public CalendarGroupIsNotEmptyException(StoreId groupId, Guid groupClassId, string groupName, int calendarsCount, Exception innerException) : base(CalendaringStrings.CalendarGroupIsNotEmpty(groupId, groupClassId, groupName, calendarsCount), innerException)
		{
			this.groupId = groupId;
			this.groupClassId = groupClassId;
			this.groupName = groupName;
			this.calendarsCount = calendarsCount;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002C38 File Offset: 0x00000E38
		protected CalendarGroupIsNotEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.groupId = (StoreId)info.GetValue("groupId", typeof(StoreId));
			this.groupClassId = (Guid)info.GetValue("groupClassId", typeof(Guid));
			this.groupName = (string)info.GetValue("groupName", typeof(string));
			this.calendarsCount = (int)info.GetValue("calendarsCount", typeof(int));
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002CD0 File Offset: 0x00000ED0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("groupId", this.groupId);
			info.AddValue("groupClassId", this.groupClassId);
			info.AddValue("groupName", this.groupName);
			info.AddValue("calendarsCount", this.calendarsCount);
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002D2E File Offset: 0x00000F2E
		public StoreId GroupId
		{
			get
			{
				return this.groupId;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002D36 File Offset: 0x00000F36
		public Guid GroupClassId
		{
			get
			{
				return this.groupClassId;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002D3E File Offset: 0x00000F3E
		public string GroupName
		{
			get
			{
				return this.groupName;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002D46 File Offset: 0x00000F46
		public int CalendarsCount
		{
			get
			{
				return this.calendarsCount;
			}
		}

		// Token: 0x04000033 RID: 51
		private readonly StoreId groupId;

		// Token: 0x04000034 RID: 52
		private readonly Guid groupClassId;

		// Token: 0x04000035 RID: 53
		private readonly string groupName;

		// Token: 0x04000036 RID: 54
		private readonly int calendarsCount;
	}
}
