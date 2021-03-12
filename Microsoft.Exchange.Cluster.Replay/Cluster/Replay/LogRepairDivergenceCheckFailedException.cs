using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004DB RID: 1243
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LogRepairDivergenceCheckFailedException : LocalizedException
	{
		// Token: 0x06002E30 RID: 11824 RVA: 0x000C2F0A File Offset: 0x000C110A
		public LogRepairDivergenceCheckFailedException(string localEndOfLogFilename, string remoteDataInTempFilename, string exceptionText) : base(ReplayStrings.LogRepairDivergenceCheckFailedError(localEndOfLogFilename, remoteDataInTempFilename, exceptionText))
		{
			this.localEndOfLogFilename = localEndOfLogFilename;
			this.remoteDataInTempFilename = remoteDataInTempFilename;
			this.exceptionText = exceptionText;
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x000C2F2F File Offset: 0x000C112F
		public LogRepairDivergenceCheckFailedException(string localEndOfLogFilename, string remoteDataInTempFilename, string exceptionText, Exception innerException) : base(ReplayStrings.LogRepairDivergenceCheckFailedError(localEndOfLogFilename, remoteDataInTempFilename, exceptionText), innerException)
		{
			this.localEndOfLogFilename = localEndOfLogFilename;
			this.remoteDataInTempFilename = remoteDataInTempFilename;
			this.exceptionText = exceptionText;
		}

		// Token: 0x06002E32 RID: 11826 RVA: 0x000C2F58 File Offset: 0x000C1158
		protected LogRepairDivergenceCheckFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.localEndOfLogFilename = (string)info.GetValue("localEndOfLogFilename", typeof(string));
			this.remoteDataInTempFilename = (string)info.GetValue("remoteDataInTempFilename", typeof(string));
			this.exceptionText = (string)info.GetValue("exceptionText", typeof(string));
		}

		// Token: 0x06002E33 RID: 11827 RVA: 0x000C2FCD File Offset: 0x000C11CD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("localEndOfLogFilename", this.localEndOfLogFilename);
			info.AddValue("remoteDataInTempFilename", this.remoteDataInTempFilename);
			info.AddValue("exceptionText", this.exceptionText);
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06002E34 RID: 11828 RVA: 0x000C300A File Offset: 0x000C120A
		public string LocalEndOfLogFilename
		{
			get
			{
				return this.localEndOfLogFilename;
			}
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06002E35 RID: 11829 RVA: 0x000C3012 File Offset: 0x000C1212
		public string RemoteDataInTempFilename
		{
			get
			{
				return this.remoteDataInTempFilename;
			}
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06002E36 RID: 11830 RVA: 0x000C301A File Offset: 0x000C121A
		public string ExceptionText
		{
			get
			{
				return this.exceptionText;
			}
		}

		// Token: 0x0400156B RID: 5483
		private readonly string localEndOfLogFilename;

		// Token: 0x0400156C RID: 5484
		private readonly string remoteDataInTempFilename;

		// Token: 0x0400156D RID: 5485
		private readonly string exceptionText;
	}
}
