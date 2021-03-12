using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F91 RID: 3985
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SendConnectorWrongTargetServerRgException : LocalizedException
	{
		// Token: 0x0600ACAC RID: 44204 RVA: 0x00290844 File Offset: 0x0028EA44
		public SendConnectorWrongTargetServerRgException(string serverName) : base(Strings.SendConnectorWrongTargetServerRg(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x0600ACAD RID: 44205 RVA: 0x00290859 File Offset: 0x0028EA59
		public SendConnectorWrongTargetServerRgException(string serverName, Exception innerException) : base(Strings.SendConnectorWrongTargetServerRg(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x0600ACAE RID: 44206 RVA: 0x0029086F File Offset: 0x0028EA6F
		protected SendConnectorWrongTargetServerRgException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600ACAF RID: 44207 RVA: 0x00290899 File Offset: 0x0028EA99
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17003785 RID: 14213
		// (get) Token: 0x0600ACB0 RID: 44208 RVA: 0x002908B4 File Offset: 0x0028EAB4
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x040060EB RID: 24811
		private readonly string serverName;
	}
}
