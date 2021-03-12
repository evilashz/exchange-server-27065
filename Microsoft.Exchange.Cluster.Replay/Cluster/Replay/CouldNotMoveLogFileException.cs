using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004D4 RID: 1236
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CouldNotMoveLogFileException : TransientException
	{
		// Token: 0x06002E07 RID: 11783 RVA: 0x000C29C2 File Offset: 0x000C0BC2
		public CouldNotMoveLogFileException(string oldpath, string newpath) : base(ReplayStrings.CouldNotMoveLogFile(oldpath, newpath))
		{
			this.oldpath = oldpath;
			this.newpath = newpath;
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x000C29DF File Offset: 0x000C0BDF
		public CouldNotMoveLogFileException(string oldpath, string newpath, Exception innerException) : base(ReplayStrings.CouldNotMoveLogFile(oldpath, newpath), innerException)
		{
			this.oldpath = oldpath;
			this.newpath = newpath;
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x000C2A00 File Offset: 0x000C0C00
		protected CouldNotMoveLogFileException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.oldpath = (string)info.GetValue("oldpath", typeof(string));
			this.newpath = (string)info.GetValue("newpath", typeof(string));
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x000C2A55 File Offset: 0x000C0C55
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("oldpath", this.oldpath);
			info.AddValue("newpath", this.newpath);
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06002E0B RID: 11787 RVA: 0x000C2A81 File Offset: 0x000C0C81
		public string Oldpath
		{
			get
			{
				return this.oldpath;
			}
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06002E0C RID: 11788 RVA: 0x000C2A89 File Offset: 0x000C0C89
		public string Newpath
		{
			get
			{
				return this.newpath;
			}
		}

		// Token: 0x0400155E RID: 5470
		private readonly string oldpath;

		// Token: 0x0400155F RID: 5471
		private readonly string newpath;
	}
}
