using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataProviders;

namespace Microsoft.Exchange.Entities.Calendaring
{
	// Token: 0x0200000F RID: 15
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CannotDeleteSpecialCalendarGroupException : InvalidRequestException
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00002AAA File Offset: 0x00000CAA
		public CannotDeleteSpecialCalendarGroupException(StoreId groupId, Guid groupClassId, string groupName) : base(CalendaringStrings.CannotDeleteWellKnownCalendarGroup(groupId, groupClassId, groupName))
		{
			this.groupId = groupId;
			this.groupClassId = groupClassId;
			this.groupName = groupName;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002ACF File Offset: 0x00000CCF
		public CannotDeleteSpecialCalendarGroupException(StoreId groupId, Guid groupClassId, string groupName, Exception innerException) : base(CalendaringStrings.CannotDeleteWellKnownCalendarGroup(groupId, groupClassId, groupName), innerException)
		{
			this.groupId = groupId;
			this.groupClassId = groupClassId;
			this.groupName = groupName;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002AF8 File Offset: 0x00000CF8
		protected CannotDeleteSpecialCalendarGroupException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.groupId = (StoreId)info.GetValue("groupId", typeof(StoreId));
			this.groupClassId = (Guid)info.GetValue("groupClassId", typeof(Guid));
			this.groupName = (string)info.GetValue("groupName", typeof(string));
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002B70 File Offset: 0x00000D70
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("groupId", this.groupId);
			info.AddValue("groupClassId", this.groupClassId);
			info.AddValue("groupName", this.groupName);
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002BBD File Offset: 0x00000DBD
		public StoreId GroupId
		{
			get
			{
				return this.groupId;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002BC5 File Offset: 0x00000DC5
		public Guid GroupClassId
		{
			get
			{
				return this.groupClassId;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002BCD File Offset: 0x00000DCD
		public string GroupName
		{
			get
			{
				return this.groupName;
			}
		}

		// Token: 0x04000030 RID: 48
		private readonly StoreId groupId;

		// Token: 0x04000031 RID: 49
		private readonly Guid groupClassId;

		// Token: 0x04000032 RID: 50
		private readonly string groupName;
	}
}
