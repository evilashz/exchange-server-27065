using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net.ExSmtpClient
{
	// Token: 0x020000E9 RID: 233
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class NotConnectedToSMTPServerException : LocalizedException
	{
		// Token: 0x0600061C RID: 1564 RVA: 0x00015F07 File Offset: 0x00014107
		public NotConnectedToSMTPServerException(string server) : base(NetException.NotConnectedToSMTPServerException(server))
		{
			this.server = server;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00015F1C File Offset: 0x0001411C
		public NotConnectedToSMTPServerException(string server, Exception innerException) : base(NetException.NotConnectedToSMTPServerException(server), innerException)
		{
			this.server = server;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00015F32 File Offset: 0x00014132
		protected NotConnectedToSMTPServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.server = (string)info.GetValue("server", typeof(string));
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00015F5C File Offset: 0x0001415C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("server", this.server);
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x00015F77 File Offset: 0x00014177
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x040004F9 RID: 1273
		private readonly string server;
	}
}
