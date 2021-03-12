using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F87 RID: 3975
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SendConnectorWrongSourceServerRgException : LocalizedException
	{
		// Token: 0x0600AC7F RID: 44159 RVA: 0x00290501 File Offset: 0x0028E701
		public SendConnectorWrongSourceServerRgException(string serverName) : base(Strings.SendConnectorWrongSourceServerRg(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x0600AC80 RID: 44160 RVA: 0x00290516 File Offset: 0x0028E716
		public SendConnectorWrongSourceServerRgException(string serverName, Exception innerException) : base(Strings.SendConnectorWrongSourceServerRg(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x0600AC81 RID: 44161 RVA: 0x0029052C File Offset: 0x0028E72C
		protected SendConnectorWrongSourceServerRgException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600AC82 RID: 44162 RVA: 0x00290556 File Offset: 0x0028E756
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17003780 RID: 14208
		// (get) Token: 0x0600AC83 RID: 44163 RVA: 0x00290571 File Offset: 0x0028E771
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x040060E6 RID: 24806
		private readonly string serverName;
	}
}
