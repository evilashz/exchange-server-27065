using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004D3 RID: 1235
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LogInspectorCouldNotMoveLogFileException : LogInspectorFailedException
	{
		// Token: 0x06002E00 RID: 11776 RVA: 0x000C28A1 File Offset: 0x000C0AA1
		public LogInspectorCouldNotMoveLogFileException(string oldpath, string newpath, string error) : base(ReplayStrings.LogInspectorCouldNotMoveLogFileException(oldpath, newpath, error))
		{
			this.oldpath = oldpath;
			this.newpath = newpath;
			this.error = error;
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x000C28CB File Offset: 0x000C0ACB
		public LogInspectorCouldNotMoveLogFileException(string oldpath, string newpath, string error, Exception innerException) : base(ReplayStrings.LogInspectorCouldNotMoveLogFileException(oldpath, newpath, error), innerException)
		{
			this.oldpath = oldpath;
			this.newpath = newpath;
			this.error = error;
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x000C28F8 File Offset: 0x000C0AF8
		protected LogInspectorCouldNotMoveLogFileException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.oldpath = (string)info.GetValue("oldpath", typeof(string));
			this.newpath = (string)info.GetValue("newpath", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x000C296D File Offset: 0x000C0B6D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("oldpath", this.oldpath);
			info.AddValue("newpath", this.newpath);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06002E04 RID: 11780 RVA: 0x000C29AA File Offset: 0x000C0BAA
		public string Oldpath
		{
			get
			{
				return this.oldpath;
			}
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06002E05 RID: 11781 RVA: 0x000C29B2 File Offset: 0x000C0BB2
		public string Newpath
		{
			get
			{
				return this.newpath;
			}
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06002E06 RID: 11782 RVA: 0x000C29BA File Offset: 0x000C0BBA
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x0400155B RID: 5467
		private readonly string oldpath;

		// Token: 0x0400155C RID: 5468
		private readonly string newpath;

		// Token: 0x0400155D RID: 5469
		private readonly string error;
	}
}
