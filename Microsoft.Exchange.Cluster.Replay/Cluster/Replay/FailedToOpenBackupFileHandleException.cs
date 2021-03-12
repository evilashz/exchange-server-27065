using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004A1 RID: 1185
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToOpenBackupFileHandleException : TransientException
	{
		// Token: 0x06002CDC RID: 11484 RVA: 0x000C02C1 File Offset: 0x000BE4C1
		public FailedToOpenBackupFileHandleException(string databaseSource, string serverSrc, int ec, string errorMessage) : base(ReplayStrings.FailedToOpenBackupFileHandle(databaseSource, serverSrc, ec, errorMessage))
		{
			this.databaseSource = databaseSource;
			this.serverSrc = serverSrc;
			this.ec = ec;
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x000C02F0 File Offset: 0x000BE4F0
		public FailedToOpenBackupFileHandleException(string databaseSource, string serverSrc, int ec, string errorMessage, Exception innerException) : base(ReplayStrings.FailedToOpenBackupFileHandle(databaseSource, serverSrc, ec, errorMessage), innerException)
		{
			this.databaseSource = databaseSource;
			this.serverSrc = serverSrc;
			this.ec = ec;
			this.errorMessage = errorMessage;
		}

		// Token: 0x06002CDE RID: 11486 RVA: 0x000C0324 File Offset: 0x000BE524
		protected FailedToOpenBackupFileHandleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.databaseSource = (string)info.GetValue("databaseSource", typeof(string));
			this.serverSrc = (string)info.GetValue("serverSrc", typeof(string));
			this.ec = (int)info.GetValue("ec", typeof(int));
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x06002CDF RID: 11487 RVA: 0x000C03BC File Offset: 0x000BE5BC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("databaseSource", this.databaseSource);
			info.AddValue("serverSrc", this.serverSrc);
			info.AddValue("ec", this.ec);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x06002CE0 RID: 11488 RVA: 0x000C0415 File Offset: 0x000BE615
		public string DatabaseSource
		{
			get
			{
				return this.databaseSource;
			}
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06002CE1 RID: 11489 RVA: 0x000C041D File Offset: 0x000BE61D
		public string ServerSrc
		{
			get
			{
				return this.serverSrc;
			}
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06002CE2 RID: 11490 RVA: 0x000C0425 File Offset: 0x000BE625
		public int Ec
		{
			get
			{
				return this.ec;
			}
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06002CE3 RID: 11491 RVA: 0x000C042D File Offset: 0x000BE62D
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x040014FF RID: 5375
		private readonly string databaseSource;

		// Token: 0x04001500 RID: 5376
		private readonly string serverSrc;

		// Token: 0x04001501 RID: 5377
		private readonly int ec;

		// Token: 0x04001502 RID: 5378
		private readonly string errorMessage;
	}
}
