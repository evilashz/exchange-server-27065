using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003C2 RID: 962
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FileCheckLogfileGenerationException : FileCheckException
	{
		// Token: 0x06002829 RID: 10281 RVA: 0x000B756A File Offset: 0x000B576A
		public FileCheckLogfileGenerationException(string logfile, long logfileGeneration, long expectedGeneration) : base(ReplayStrings.FileCheckLogfileGeneration(logfile, logfileGeneration, expectedGeneration))
		{
			this.logfile = logfile;
			this.logfileGeneration = logfileGeneration;
			this.expectedGeneration = expectedGeneration;
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x000B7594 File Offset: 0x000B5794
		public FileCheckLogfileGenerationException(string logfile, long logfileGeneration, long expectedGeneration, Exception innerException) : base(ReplayStrings.FileCheckLogfileGeneration(logfile, logfileGeneration, expectedGeneration), innerException)
		{
			this.logfile = logfile;
			this.logfileGeneration = logfileGeneration;
			this.expectedGeneration = expectedGeneration;
		}

		// Token: 0x0600282B RID: 10283 RVA: 0x000B75C0 File Offset: 0x000B57C0
		protected FileCheckLogfileGenerationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.logfile = (string)info.GetValue("logfile", typeof(string));
			this.logfileGeneration = (long)info.GetValue("logfileGeneration", typeof(long));
			this.expectedGeneration = (long)info.GetValue("expectedGeneration", typeof(long));
		}

		// Token: 0x0600282C RID: 10284 RVA: 0x000B7635 File Offset: 0x000B5835
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("logfile", this.logfile);
			info.AddValue("logfileGeneration", this.logfileGeneration);
			info.AddValue("expectedGeneration", this.expectedGeneration);
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x0600282D RID: 10285 RVA: 0x000B7672 File Offset: 0x000B5872
		public string Logfile
		{
			get
			{
				return this.logfile;
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x0600282E RID: 10286 RVA: 0x000B767A File Offset: 0x000B587A
		public long LogfileGeneration
		{
			get
			{
				return this.logfileGeneration;
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x0600282F RID: 10287 RVA: 0x000B7682 File Offset: 0x000B5882
		public long ExpectedGeneration
		{
			get
			{
				return this.expectedGeneration;
			}
		}

		// Token: 0x040013C8 RID: 5064
		private readonly string logfile;

		// Token: 0x040013C9 RID: 5065
		private readonly long logfileGeneration;

		// Token: 0x040013CA RID: 5066
		private readonly long expectedGeneration;
	}
}
