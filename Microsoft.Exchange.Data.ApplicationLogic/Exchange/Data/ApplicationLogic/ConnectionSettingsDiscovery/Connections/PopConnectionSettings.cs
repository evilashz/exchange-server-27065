using System;
using System.Security;
using System.Text;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Connections.Pop;

namespace Microsoft.Exchange.Data.ApplicationLogic.ConnectionSettingsDiscovery.Connections
{
	// Token: 0x02000198 RID: 408
	internal class PopConnectionSettings : ProtocolSpecificConnectionSettings
	{
		// Token: 0x06000F82 RID: 3970 RVA: 0x0003F444 File Offset: 0x0003D644
		public PopConnectionSettings(Fqdn serverName, int portNumber, Pop3AuthenticationMechanism authentication, Pop3SecurityMechanism security) : base(ConnectionSettingsType.Pop)
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
			this.Authentication = authentication;
			this.Security = security;
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x0003F49D File Offset: 0x0003D69D
		// (set) Token: 0x06000F84 RID: 3972 RVA: 0x0003F4A5 File Offset: 0x0003D6A5
		public Fqdn ServerName { get; private set; }

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x0003F4AE File Offset: 0x0003D6AE
		// (set) Token: 0x06000F86 RID: 3974 RVA: 0x0003F4B6 File Offset: 0x0003D6B6
		public int Port { get; private set; }

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x0003F4BF File Offset: 0x0003D6BF
		// (set) Token: 0x06000F88 RID: 3976 RVA: 0x0003F4C7 File Offset: 0x0003D6C7
		public Pop3AuthenticationMechanism Authentication { get; private set; }

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x0003F4D0 File Offset: 0x0003D6D0
		// (set) Token: 0x06000F8A RID: 3978 RVA: 0x0003F4D8 File Offset: 0x0003D6D8
		public Pop3SecurityMechanism Security { get; private set; }

		// Token: 0x06000F8B RID: 3979 RVA: 0x0003F4E4 File Offset: 0x0003D6E4
		public override string ToMultiLineString(string lineSeparator)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Get {0}", base.ToMultiLineString(lineSeparator));
			stringBuilder.AppendFormat("Server name={0},{1}", this.ServerName, lineSeparator);
			stringBuilder.AppendFormat("Port={0},{1}", this.Port, lineSeparator);
			stringBuilder.AppendFormat("Authentication={0},{1}", this.Authentication, lineSeparator);
			stringBuilder.AppendFormat("Security={0}", this.Security);
			return stringBuilder.ToString();
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0003F56A File Offset: 0x0003D76A
		protected override OperationStatusCode TestUserCanLogonWithCurrentSettings(SmtpAddress email, string userName, SecureString password)
		{
			return OperationStatusCode.Success;
		}
	}
}
