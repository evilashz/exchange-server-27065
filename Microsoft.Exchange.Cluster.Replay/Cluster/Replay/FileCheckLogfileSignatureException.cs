using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003A4 RID: 932
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FileCheckLogfileSignatureException : FileCheckException
	{
		// Token: 0x06002782 RID: 10114 RVA: 0x000B60F9 File Offset: 0x000B42F9
		public FileCheckLogfileSignatureException(string logfile, string logfileSignature, string expectedSignature) : base(ReplayStrings.FileCheckLogfileSignature(logfile, logfileSignature, expectedSignature))
		{
			this.logfile = logfile;
			this.logfileSignature = logfileSignature;
			this.expectedSignature = expectedSignature;
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x000B6123 File Offset: 0x000B4323
		public FileCheckLogfileSignatureException(string logfile, string logfileSignature, string expectedSignature, Exception innerException) : base(ReplayStrings.FileCheckLogfileSignature(logfile, logfileSignature, expectedSignature), innerException)
		{
			this.logfile = logfile;
			this.logfileSignature = logfileSignature;
			this.expectedSignature = expectedSignature;
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x000B6150 File Offset: 0x000B4350
		protected FileCheckLogfileSignatureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.logfile = (string)info.GetValue("logfile", typeof(string));
			this.logfileSignature = (string)info.GetValue("logfileSignature", typeof(string));
			this.expectedSignature = (string)info.GetValue("expectedSignature", typeof(string));
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x000B61C5 File Offset: 0x000B43C5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("logfile", this.logfile);
			info.AddValue("logfileSignature", this.logfileSignature);
			info.AddValue("expectedSignature", this.expectedSignature);
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06002786 RID: 10118 RVA: 0x000B6202 File Offset: 0x000B4402
		public string Logfile
		{
			get
			{
				return this.logfile;
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06002787 RID: 10119 RVA: 0x000B620A File Offset: 0x000B440A
		public string LogfileSignature
		{
			get
			{
				return this.logfileSignature;
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06002788 RID: 10120 RVA: 0x000B6212 File Offset: 0x000B4412
		public string ExpectedSignature
		{
			get
			{
				return this.expectedSignature;
			}
		}

		// Token: 0x04001399 RID: 5017
		private readonly string logfile;

		// Token: 0x0400139A RID: 5018
		private readonly string logfileSignature;

		// Token: 0x0400139B RID: 5019
		private readonly string expectedSignature;
	}
}
