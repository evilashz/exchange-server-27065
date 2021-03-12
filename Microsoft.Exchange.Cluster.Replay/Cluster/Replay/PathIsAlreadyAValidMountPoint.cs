using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200050F RID: 1295
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PathIsAlreadyAValidMountPoint : DatabaseVolumeInfoException
	{
		// Token: 0x06002F60 RID: 12128 RVA: 0x000C5775 File Offset: 0x000C3975
		public PathIsAlreadyAValidMountPoint(string path) : base(ReplayStrings.PathIsAlreadyAValidMountPoint(path))
		{
			this.path = path;
		}

		// Token: 0x06002F61 RID: 12129 RVA: 0x000C578F File Offset: 0x000C398F
		public PathIsAlreadyAValidMountPoint(string path, Exception innerException) : base(ReplayStrings.PathIsAlreadyAValidMountPoint(path), innerException)
		{
			this.path = path;
		}

		// Token: 0x06002F62 RID: 12130 RVA: 0x000C57AA File Offset: 0x000C39AA
		protected PathIsAlreadyAValidMountPoint(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.path = (string)info.GetValue("path", typeof(string));
		}

		// Token: 0x06002F63 RID: 12131 RVA: 0x000C57D4 File Offset: 0x000C39D4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("path", this.path);
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x06002F64 RID: 12132 RVA: 0x000C57EF File Offset: 0x000C39EF
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x040015CB RID: 5579
		private readonly string path;
	}
}
