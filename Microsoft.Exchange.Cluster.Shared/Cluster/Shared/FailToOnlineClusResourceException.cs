using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000D4 RID: 212
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailToOnlineClusResourceException : ClusCommonFailException
	{
		// Token: 0x06000759 RID: 1881 RVA: 0x0001BF9A File Offset: 0x0001A19A
		public FailToOnlineClusResourceException(string groupName, string resourceId, string reason) : base(Strings.FailToOnlineClusResourceException(groupName, resourceId, reason))
		{
			this.groupName = groupName;
			this.resourceId = resourceId;
			this.reason = reason;
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0001BFC4 File Offset: 0x0001A1C4
		public FailToOnlineClusResourceException(string groupName, string resourceId, string reason, Exception innerException) : base(Strings.FailToOnlineClusResourceException(groupName, resourceId, reason), innerException)
		{
			this.groupName = groupName;
			this.resourceId = resourceId;
			this.reason = reason;
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001BFF0 File Offset: 0x0001A1F0
		protected FailToOnlineClusResourceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.groupName = (string)info.GetValue("groupName", typeof(string));
			this.resourceId = (string)info.GetValue("resourceId", typeof(string));
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0001C065 File Offset: 0x0001A265
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("groupName", this.groupName);
			info.AddValue("resourceId", this.resourceId);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x0001C0A2 File Offset: 0x0001A2A2
		public string GroupName
		{
			get
			{
				return this.groupName;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x0001C0AA File Offset: 0x0001A2AA
		public string ResourceId
		{
			get
			{
				return this.resourceId;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x0001C0B2 File Offset: 0x0001A2B2
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x04000729 RID: 1833
		private readonly string groupName;

		// Token: 0x0400072A RID: 1834
		private readonly string resourceId;

		// Token: 0x0400072B RID: 1835
		private readonly string reason;
	}
}
