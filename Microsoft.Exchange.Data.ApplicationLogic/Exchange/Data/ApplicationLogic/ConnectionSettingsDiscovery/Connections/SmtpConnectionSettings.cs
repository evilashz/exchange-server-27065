using System;
using System.Security;
using System.Text;
using Microsoft.Exchange.Connections.Common;

namespace Microsoft.Exchange.Data.ApplicationLogic.ConnectionSettingsDiscovery.Connections
{
	// Token: 0x02000199 RID: 409
	internal class SmtpConnectionSettings : ProtocolSpecificConnectionSettings
	{
		// Token: 0x06000F8D RID: 3981 RVA: 0x0003F56D File Offset: 0x0003D76D
		public SmtpConnectionSettings(Fqdn serverName, int portNumber) : base(ConnectionSettingsType.Smtp)
		{
			if (serverName == null)
			{
				throw new ArgumentNullException("serverName", "The serverName parameter cannot be null.");
			}
			if (portNumber < 0)
			{
				throw new ArgumentException("serverName", "The portNumber parameter must have a value greater than 0.");
			}
			this.ServerName = serverName;
			this.Port = portNumber;
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x0003F5AC File Offset: 0x0003D7AC
		// (set) Token: 0x06000F8F RID: 3983 RVA: 0x0003F5B4 File Offset: 0x0003D7B4
		public Fqdn ServerName { get; private set; }

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x0003F5BD File Offset: 0x0003D7BD
		// (set) Token: 0x06000F91 RID: 3985 RVA: 0x0003F5C5 File Offset: 0x0003D7C5
		public int Port { get; private set; }

		// Token: 0x06000F92 RID: 3986 RVA: 0x0003F5D0 File Offset: 0x0003D7D0
		public override string ToMultiLineString(string lineSeparator)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Send {0}", base.ToMultiLineString(lineSeparator));
			stringBuilder.AppendFormat("Server name={0},{1}", this.ServerName, lineSeparator);
			stringBuilder.AppendFormat("Port={0}", this.Port);
			return stringBuilder.ToString();
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x0003F626 File Offset: 0x0003D826
		protected override OperationStatusCode TestUserCanLogonWithCurrentSettings(SmtpAddress email, string userName, SecureString password)
		{
			return OperationStatusCode.Success;
		}
	}
}
