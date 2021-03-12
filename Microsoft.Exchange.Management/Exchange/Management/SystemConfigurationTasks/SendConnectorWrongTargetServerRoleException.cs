using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F92 RID: 3986
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SendConnectorWrongTargetServerRoleException : LocalizedException
	{
		// Token: 0x0600ACB1 RID: 44209 RVA: 0x002908BC File Offset: 0x0028EABC
		public SendConnectorWrongTargetServerRoleException(string serverName) : base(Strings.SendConnectorWrongTargetServerRole(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x0600ACB2 RID: 44210 RVA: 0x002908D1 File Offset: 0x0028EAD1
		public SendConnectorWrongTargetServerRoleException(string serverName, Exception innerException) : base(Strings.SendConnectorWrongTargetServerRole(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x0600ACB3 RID: 44211 RVA: 0x002908E7 File Offset: 0x0028EAE7
		protected SendConnectorWrongTargetServerRoleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600ACB4 RID: 44212 RVA: 0x00290911 File Offset: 0x0028EB11
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17003786 RID: 14214
		// (get) Token: 0x0600ACB5 RID: 44213 RVA: 0x0029092C File Offset: 0x0028EB2C
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x040060EC RID: 24812
		private readonly string serverName;
	}
}
