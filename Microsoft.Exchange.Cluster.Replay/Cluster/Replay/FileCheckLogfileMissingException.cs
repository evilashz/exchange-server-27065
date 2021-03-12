using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003C4 RID: 964
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FileCheckLogfileMissingException : FileCheckException
	{
		// Token: 0x06002837 RID: 10295 RVA: 0x000B77AA File Offset: 0x000B59AA
		public FileCheckLogfileMissingException(string logfile) : base(ReplayStrings.FileCheckLogfileMissing(logfile))
		{
			this.logfile = logfile;
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x000B77C4 File Offset: 0x000B59C4
		public FileCheckLogfileMissingException(string logfile, Exception innerException) : base(ReplayStrings.FileCheckLogfileMissing(logfile), innerException)
		{
			this.logfile = logfile;
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x000B77DF File Offset: 0x000B59DF
		protected FileCheckLogfileMissingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.logfile = (string)info.GetValue("logfile", typeof(string));
		}

		// Token: 0x0600283A RID: 10298 RVA: 0x000B7809 File Offset: 0x000B5A09
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("logfile", this.logfile);
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x0600283B RID: 10299 RVA: 0x000B7824 File Offset: 0x000B5A24
		public string Logfile
		{
			get
			{
				return this.logfile;
			}
		}

		// Token: 0x040013CE RID: 5070
		private readonly string logfile;
	}
}
