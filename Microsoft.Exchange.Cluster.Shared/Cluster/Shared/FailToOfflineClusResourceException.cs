using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000D3 RID: 211
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailToOfflineClusResourceException : ClusCommonFailException
	{
		// Token: 0x06000752 RID: 1874 RVA: 0x0001BE79 File Offset: 0x0001A079
		public FailToOfflineClusResourceException(string groupName, string resourceId, string reason) : base(Strings.FailToOfflineClusResourceException(groupName, resourceId, reason))
		{
			this.groupName = groupName;
			this.resourceId = resourceId;
			this.reason = reason;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0001BEA3 File Offset: 0x0001A0A3
		public FailToOfflineClusResourceException(string groupName, string resourceId, string reason, Exception innerException) : base(Strings.FailToOfflineClusResourceException(groupName, resourceId, reason), innerException)
		{
			this.groupName = groupName;
			this.resourceId = resourceId;
			this.reason = reason;
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0001BED0 File Offset: 0x0001A0D0
		protected FailToOfflineClusResourceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.groupName = (string)info.GetValue("groupName", typeof(string));
			this.resourceId = (string)info.GetValue("resourceId", typeof(string));
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0001BF45 File Offset: 0x0001A145
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("groupName", this.groupName);
			info.AddValue("resourceId", this.resourceId);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x0001BF82 File Offset: 0x0001A182
		public string GroupName
		{
			get
			{
				return this.groupName;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x0001BF8A File Offset: 0x0001A18A
		public string ResourceId
		{
			get
			{
				return this.resourceId;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x0001BF92 File Offset: 0x0001A192
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x04000726 RID: 1830
		private readonly string groupName;

		// Token: 0x04000727 RID: 1831
		private readonly string resourceId;

		// Token: 0x04000728 RID: 1832
		private readonly string reason;
	}
}
