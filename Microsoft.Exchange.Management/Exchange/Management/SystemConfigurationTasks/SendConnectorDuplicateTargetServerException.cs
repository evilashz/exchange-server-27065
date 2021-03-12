using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F9C RID: 3996
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SendConnectorDuplicateTargetServerException : LocalizedException
	{
		// Token: 0x0600ACDE RID: 44254 RVA: 0x00290BFF File Offset: 0x0028EDFF
		public SendConnectorDuplicateTargetServerException(string server) : base(Strings.SendConnectorDuplicateTargetServerException(server))
		{
			this.server = server;
		}

		// Token: 0x0600ACDF RID: 44255 RVA: 0x00290C14 File Offset: 0x0028EE14
		public SendConnectorDuplicateTargetServerException(string server, Exception innerException) : base(Strings.SendConnectorDuplicateTargetServerException(server), innerException)
		{
			this.server = server;
		}

		// Token: 0x0600ACE0 RID: 44256 RVA: 0x00290C2A File Offset: 0x0028EE2A
		protected SendConnectorDuplicateTargetServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.server = (string)info.GetValue("server", typeof(string));
		}

		// Token: 0x0600ACE1 RID: 44257 RVA: 0x00290C54 File Offset: 0x0028EE54
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("server", this.server);
		}

		// Token: 0x1700378B RID: 14219
		// (get) Token: 0x0600ACE2 RID: 44258 RVA: 0x00290C6F File Offset: 0x0028EE6F
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x040060F1 RID: 24817
		private readonly string server;
	}
}
