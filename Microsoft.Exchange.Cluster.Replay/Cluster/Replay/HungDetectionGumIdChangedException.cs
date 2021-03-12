using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000482 RID: 1154
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class HungDetectionGumIdChangedException : ClusterException
	{
		// Token: 0x06002C2F RID: 11311 RVA: 0x000BEDA6 File Offset: 0x000BCFA6
		public HungDetectionGumIdChangedException(int localGumId, int remoteGumId, string lockOwnerName, long hungNodesMask) : base(ReplayStrings.HungDetectionGumIdChanged(localGumId, remoteGumId, lockOwnerName, hungNodesMask))
		{
			this.localGumId = localGumId;
			this.remoteGumId = remoteGumId;
			this.lockOwnerName = lockOwnerName;
			this.hungNodesMask = hungNodesMask;
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x000BEDDA File Offset: 0x000BCFDA
		public HungDetectionGumIdChangedException(int localGumId, int remoteGumId, string lockOwnerName, long hungNodesMask, Exception innerException) : base(ReplayStrings.HungDetectionGumIdChanged(localGumId, remoteGumId, lockOwnerName, hungNodesMask), innerException)
		{
			this.localGumId = localGumId;
			this.remoteGumId = remoteGumId;
			this.lockOwnerName = lockOwnerName;
			this.hungNodesMask = hungNodesMask;
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x000BEE10 File Offset: 0x000BD010
		protected HungDetectionGumIdChangedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.localGumId = (int)info.GetValue("localGumId", typeof(int));
			this.remoteGumId = (int)info.GetValue("remoteGumId", typeof(int));
			this.lockOwnerName = (string)info.GetValue("lockOwnerName", typeof(string));
			this.hungNodesMask = (long)info.GetValue("hungNodesMask", typeof(long));
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x000BEEA8 File Offset: 0x000BD0A8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("localGumId", this.localGumId);
			info.AddValue("remoteGumId", this.remoteGumId);
			info.AddValue("lockOwnerName", this.lockOwnerName);
			info.AddValue("hungNodesMask", this.hungNodesMask);
		}

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06002C33 RID: 11315 RVA: 0x000BEF01 File Offset: 0x000BD101
		public int LocalGumId
		{
			get
			{
				return this.localGumId;
			}
		}

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06002C34 RID: 11316 RVA: 0x000BEF09 File Offset: 0x000BD109
		public int RemoteGumId
		{
			get
			{
				return this.remoteGumId;
			}
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x06002C35 RID: 11317 RVA: 0x000BEF11 File Offset: 0x000BD111
		public string LockOwnerName
		{
			get
			{
				return this.lockOwnerName;
			}
		}

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06002C36 RID: 11318 RVA: 0x000BEF19 File Offset: 0x000BD119
		public long HungNodesMask
		{
			get
			{
				return this.hungNodesMask;
			}
		}

		// Token: 0x040014CE RID: 5326
		private readonly int localGumId;

		// Token: 0x040014CF RID: 5327
		private readonly int remoteGumId;

		// Token: 0x040014D0 RID: 5328
		private readonly string lockOwnerName;

		// Token: 0x040014D1 RID: 5329
		private readonly long hungNodesMask;
	}
}
