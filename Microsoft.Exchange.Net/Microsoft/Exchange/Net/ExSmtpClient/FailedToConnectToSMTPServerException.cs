using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net.ExSmtpClient
{
	// Token: 0x020000E6 RID: 230
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FailedToConnectToSMTPServerException : LocalizedException
	{
		// Token: 0x0600060E RID: 1550 RVA: 0x00015DE8 File Offset: 0x00013FE8
		public FailedToConnectToSMTPServerException(string server) : base(NetException.FailedToConnectToSMTPServerException(server))
		{
			this.server = server;
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00015DFD File Offset: 0x00013FFD
		public FailedToConnectToSMTPServerException(string server, Exception innerException) : base(NetException.FailedToConnectToSMTPServerException(server), innerException)
		{
			this.server = server;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00015E13 File Offset: 0x00014013
		protected FailedToConnectToSMTPServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.server = (string)info.GetValue("server", typeof(string));
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00015E3D File Offset: 0x0001403D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("server", this.server);
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x00015E58 File Offset: 0x00014058
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x040004F7 RID: 1271
		private readonly string server;
	}
}
