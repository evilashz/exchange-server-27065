using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003C5 RID: 965
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FileCheckRequiredLogfileGapException : FileCheckException
	{
		// Token: 0x0600283C RID: 10300 RVA: 0x000B782C File Offset: 0x000B5A2C
		public FileCheckRequiredLogfileGapException(string logfile) : base(ReplayStrings.FileCheckRequiredLogfileGapException(logfile))
		{
			this.logfile = logfile;
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x000B7846 File Offset: 0x000B5A46
		public FileCheckRequiredLogfileGapException(string logfile, Exception innerException) : base(ReplayStrings.FileCheckRequiredLogfileGapException(logfile), innerException)
		{
			this.logfile = logfile;
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x000B7861 File Offset: 0x000B5A61
		protected FileCheckRequiredLogfileGapException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.logfile = (string)info.GetValue("logfile", typeof(string));
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x000B788B File Offset: 0x000B5A8B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("logfile", this.logfile);
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06002840 RID: 10304 RVA: 0x000B78A6 File Offset: 0x000B5AA6
		public string Logfile
		{
			get
			{
				return this.logfile;
			}
		}

		// Token: 0x040013CF RID: 5071
		private readonly string logfile;
	}
}
