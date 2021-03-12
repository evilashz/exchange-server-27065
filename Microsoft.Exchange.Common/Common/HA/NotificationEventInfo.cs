using System;
using System.Text;

namespace Microsoft.Exchange.Common.HA
{
	// Token: 0x0200003E RID: 62
	internal class NotificationEventInfo
	{
		// Token: 0x0600011D RID: 285 RVA: 0x00006A68 File Offset: 0x00004C68
		internal NotificationEventInfo()
		{
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006A70 File Offset: 0x00004C70
		internal NotificationEventInfo(int eventId)
		{
			this.EventId = eventId;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00006A7F File Offset: 0x00004C7F
		internal NotificationEventInfo(int eventId, string[] parameters)
		{
			this.EventId = eventId;
			this.Parameters = parameters;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006A98 File Offset: 0x00004C98
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (base.GetType() != obj.GetType())
			{
				return false;
			}
			NotificationEventInfo notificationEventInfo = obj as NotificationEventInfo;
			if (!this.EventId.Equals(notificationEventInfo.EventId))
			{
				return false;
			}
			if ((this.Parameters == null || notificationEventInfo.Parameters == null) && (this.Parameters != null || notificationEventInfo.Parameters != null))
			{
				return false;
			}
			if (this.Parameters != null && notificationEventInfo.Parameters != null)
			{
				if (this.Parameters.Length != notificationEventInfo.Parameters.Length)
				{
					return false;
				}
				for (int i = 0; i < this.Parameters.Length; i++)
				{
					if (!object.Equals(this.Parameters[i], notificationEventInfo.Parameters[i]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00006B54 File Offset: 0x00004D54
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			stringBuilder.AppendFormat("EventId={0} ", this.EventId);
			stringBuilder.AppendLine();
			if (this.Parameters != null && this.Parameters.Length > 0)
			{
				stringBuilder.Append("Parameters={");
				foreach (string arg in this.Parameters)
				{
					stringBuilder.AppendFormat("{0},", arg);
				}
				stringBuilder.Append("} ");
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00006BE8 File Offset: 0x00004DE8
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00006BF0 File Offset: 0x00004DF0
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00006BF8 File Offset: 0x00004DF8
		internal int EventId
		{
			get
			{
				return this.eventId;
			}
			set
			{
				this.eventId = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00006C01 File Offset: 0x00004E01
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00006C09 File Offset: 0x00004E09
		internal string[] Parameters
		{
			get
			{
				return this.parameters;
			}
			set
			{
				this.parameters = value;
			}
		}

		// Token: 0x04000154 RID: 340
		internal const int InvalidEventId = -1;

		// Token: 0x04000155 RID: 341
		private int eventId;

		// Token: 0x04000156 RID: 342
		private string[] parameters;
	}
}
