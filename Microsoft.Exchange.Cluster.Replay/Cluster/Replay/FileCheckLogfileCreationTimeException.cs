using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003C3 RID: 963
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FileCheckLogfileCreationTimeException : FileCheckException
	{
		// Token: 0x06002830 RID: 10288 RVA: 0x000B768A File Offset: 0x000B588A
		public FileCheckLogfileCreationTimeException(string logfile, DateTime previousGenerationCreationTime, DateTime previousGenerationCreationTimeActual) : base(ReplayStrings.FileCheckLogfileCreationTime(logfile, previousGenerationCreationTime, previousGenerationCreationTimeActual))
		{
			this.logfile = logfile;
			this.previousGenerationCreationTime = previousGenerationCreationTime;
			this.previousGenerationCreationTimeActual = previousGenerationCreationTimeActual;
		}

		// Token: 0x06002831 RID: 10289 RVA: 0x000B76B4 File Offset: 0x000B58B4
		public FileCheckLogfileCreationTimeException(string logfile, DateTime previousGenerationCreationTime, DateTime previousGenerationCreationTimeActual, Exception innerException) : base(ReplayStrings.FileCheckLogfileCreationTime(logfile, previousGenerationCreationTime, previousGenerationCreationTimeActual), innerException)
		{
			this.logfile = logfile;
			this.previousGenerationCreationTime = previousGenerationCreationTime;
			this.previousGenerationCreationTimeActual = previousGenerationCreationTimeActual;
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x000B76E0 File Offset: 0x000B58E0
		protected FileCheckLogfileCreationTimeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.logfile = (string)info.GetValue("logfile", typeof(string));
			this.previousGenerationCreationTime = (DateTime)info.GetValue("previousGenerationCreationTime", typeof(DateTime));
			this.previousGenerationCreationTimeActual = (DateTime)info.GetValue("previousGenerationCreationTimeActual", typeof(DateTime));
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x000B7755 File Offset: 0x000B5955
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("logfile", this.logfile);
			info.AddValue("previousGenerationCreationTime", this.previousGenerationCreationTime);
			info.AddValue("previousGenerationCreationTimeActual", this.previousGenerationCreationTimeActual);
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06002834 RID: 10292 RVA: 0x000B7792 File Offset: 0x000B5992
		public string Logfile
		{
			get
			{
				return this.logfile;
			}
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06002835 RID: 10293 RVA: 0x000B779A File Offset: 0x000B599A
		public DateTime PreviousGenerationCreationTime
		{
			get
			{
				return this.previousGenerationCreationTime;
			}
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06002836 RID: 10294 RVA: 0x000B77A2 File Offset: 0x000B59A2
		public DateTime PreviousGenerationCreationTimeActual
		{
			get
			{
				return this.previousGenerationCreationTimeActual;
			}
		}

		// Token: 0x040013CB RID: 5067
		private readonly string logfile;

		// Token: 0x040013CC RID: 5068
		private readonly DateTime previousGenerationCreationTime;

		// Token: 0x040013CD RID: 5069
		private readonly DateTime previousGenerationCreationTimeActual;
	}
}
