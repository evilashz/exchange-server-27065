using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F9B RID: 3995
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SendConnectorDuplicateSourceServerException : LocalizedException
	{
		// Token: 0x0600ACD9 RID: 44249 RVA: 0x00290B87 File Offset: 0x0028ED87
		public SendConnectorDuplicateSourceServerException(string server) : base(Strings.SendConnectorDuplicateSourceServerException(server))
		{
			this.server = server;
		}

		// Token: 0x0600ACDA RID: 44250 RVA: 0x00290B9C File Offset: 0x0028ED9C
		public SendConnectorDuplicateSourceServerException(string server, Exception innerException) : base(Strings.SendConnectorDuplicateSourceServerException(server), innerException)
		{
			this.server = server;
		}

		// Token: 0x0600ACDB RID: 44251 RVA: 0x00290BB2 File Offset: 0x0028EDB2
		protected SendConnectorDuplicateSourceServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.server = (string)info.GetValue("server", typeof(string));
		}

		// Token: 0x0600ACDC RID: 44252 RVA: 0x00290BDC File Offset: 0x0028EDDC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("server", this.server);
		}

		// Token: 0x1700378A RID: 14218
		// (get) Token: 0x0600ACDD RID: 44253 RVA: 0x00290BF7 File Offset: 0x0028EDF7
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x040060F0 RID: 24816
		private readonly string server;
	}
}
