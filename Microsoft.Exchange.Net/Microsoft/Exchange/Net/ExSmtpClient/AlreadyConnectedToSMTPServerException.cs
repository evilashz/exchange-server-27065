using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net.ExSmtpClient
{
	// Token: 0x020000E3 RID: 227
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class AlreadyConnectedToSMTPServerException : LocalizedException
	{
		// Token: 0x06000600 RID: 1536 RVA: 0x00015CC9 File Offset: 0x00013EC9
		public AlreadyConnectedToSMTPServerException(string server) : base(NetException.AlreadyConnectedToSMTPServerException(server))
		{
			this.server = server;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00015CDE File Offset: 0x00013EDE
		public AlreadyConnectedToSMTPServerException(string server, Exception innerException) : base(NetException.AlreadyConnectedToSMTPServerException(server), innerException)
		{
			this.server = server;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00015CF4 File Offset: 0x00013EF4
		protected AlreadyConnectedToSMTPServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.server = (string)info.GetValue("server", typeof(string));
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00015D1E File Offset: 0x00013F1E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("server", this.server);
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x00015D39 File Offset: 0x00013F39
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x040004F5 RID: 1269
		private readonly string server;
	}
}
