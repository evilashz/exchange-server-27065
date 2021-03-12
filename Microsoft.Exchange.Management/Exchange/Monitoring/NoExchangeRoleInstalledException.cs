using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000EFC RID: 3836
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoExchangeRoleInstalledException : LocalizedException
	{
		// Token: 0x0600A9D5 RID: 43477 RVA: 0x0028C635 File Offset: 0x0028A835
		public NoExchangeRoleInstalledException(string serverName) : base(Strings.NoExchangeRoleInstalled(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x0600A9D6 RID: 43478 RVA: 0x0028C64A File Offset: 0x0028A84A
		public NoExchangeRoleInstalledException(string serverName, Exception innerException) : base(Strings.NoExchangeRoleInstalled(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x0600A9D7 RID: 43479 RVA: 0x0028C660 File Offset: 0x0028A860
		protected NoExchangeRoleInstalledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600A9D8 RID: 43480 RVA: 0x0028C68A File Offset: 0x0028A88A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17003702 RID: 14082
		// (get) Token: 0x0600A9D9 RID: 43481 RVA: 0x0028C6A5 File Offset: 0x0028A8A5
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x04006068 RID: 24680
		private readonly string serverName;
	}
}
