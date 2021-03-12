using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F95 RID: 3989
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SendConnectorComputerNotFoundException : LocalizedException
	{
		// Token: 0x0600ACBF RID: 44223 RVA: 0x002909DB File Offset: 0x0028EBDB
		public SendConnectorComputerNotFoundException(string serverName) : base(Strings.SendConnectorComputerNotFound(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x0600ACC0 RID: 44224 RVA: 0x002909F0 File Offset: 0x0028EBF0
		public SendConnectorComputerNotFoundException(string serverName, Exception innerException) : base(Strings.SendConnectorComputerNotFound(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x0600ACC1 RID: 44225 RVA: 0x00290A06 File Offset: 0x0028EC06
		protected SendConnectorComputerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600ACC2 RID: 44226 RVA: 0x00290A30 File Offset: 0x0028EC30
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17003788 RID: 14216
		// (get) Token: 0x0600ACC3 RID: 44227 RVA: 0x00290A4B File Offset: 0x0028EC4B
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x040060EE RID: 24814
		private readonly string serverName;
	}
}
