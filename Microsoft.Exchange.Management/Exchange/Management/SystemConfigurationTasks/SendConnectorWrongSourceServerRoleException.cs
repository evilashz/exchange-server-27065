using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F88 RID: 3976
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SendConnectorWrongSourceServerRoleException : LocalizedException
	{
		// Token: 0x0600AC84 RID: 44164 RVA: 0x00290579 File Offset: 0x0028E779
		public SendConnectorWrongSourceServerRoleException(string serverName) : base(Strings.SendConnectorWrongSourceServerRole(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x0600AC85 RID: 44165 RVA: 0x0029058E File Offset: 0x0028E78E
		public SendConnectorWrongSourceServerRoleException(string serverName, Exception innerException) : base(Strings.SendConnectorWrongSourceServerRole(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x0600AC86 RID: 44166 RVA: 0x002905A4 File Offset: 0x0028E7A4
		protected SendConnectorWrongSourceServerRoleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600AC87 RID: 44167 RVA: 0x002905CE File Offset: 0x0028E7CE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17003781 RID: 14209
		// (get) Token: 0x0600AC88 RID: 44168 RVA: 0x002905E9 File Offset: 0x0028E7E9
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x040060E7 RID: 24807
		private readonly string serverName;
	}
}
