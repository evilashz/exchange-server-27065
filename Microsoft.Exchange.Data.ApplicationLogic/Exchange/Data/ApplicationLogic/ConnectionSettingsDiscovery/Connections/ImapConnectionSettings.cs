using System;
using System.Security;
using System.Text;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Connections.Imap;

namespace Microsoft.Exchange.Data.ApplicationLogic.ConnectionSettingsDiscovery.Connections
{
	// Token: 0x02000197 RID: 407
	internal class ImapConnectionSettings : ProtocolSpecificConnectionSettings
	{
		// Token: 0x06000F76 RID: 3958 RVA: 0x0003F25C File Offset: 0x0003D45C
		public ImapConnectionSettings(Fqdn serverName, int portNumber, ImapAuthenticationMechanism authentication, ImapSecurityMechanism security) : base(ConnectionSettingsType.Imap)
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

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x0003F2EE File Offset: 0x0003D4EE
		// (set) Token: 0x06000F78 RID: 3960 RVA: 0x0003F2F6 File Offset: 0x0003D4F6
		public Fqdn ServerName { get; private set; }

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x0003F2FF File Offset: 0x0003D4FF
		// (set) Token: 0x06000F7A RID: 3962 RVA: 0x0003F307 File Offset: 0x0003D507
		public int Port { get; private set; }

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000F7B RID: 3963 RVA: 0x0003F310 File Offset: 0x0003D510
		// (set) Token: 0x06000F7C RID: 3964 RVA: 0x0003F318 File Offset: 0x0003D518
		public ImapAuthenticationMechanism Authentication { get; private set; }

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000F7D RID: 3965 RVA: 0x0003F321 File Offset: 0x0003D521
		// (set) Token: 0x06000F7E RID: 3966 RVA: 0x0003F329 File Offset: 0x0003D529
		public ImapSecurityMechanism Security { get; private set; }

		// Token: 0x06000F7F RID: 3967 RVA: 0x0003F334 File Offset: 0x0003D534
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

		// Token: 0x06000F80 RID: 3968 RVA: 0x0003F3BC File Offset: 0x0003D5BC
		protected override OperationStatusCode TestUserCanLogonWithCurrentSettings(SmtpAddress email, string userName, SecureString password)
		{
			OperationStatusCode result;
			using (ImapConnection imapConnection = ImapConnection.CreateInstance(this.connectionParameters))
			{
				ImapServerParameters serverParameters = new ImapServerParameters(this.ServerName, this.Port);
				result = imapConnection.TestLogon(serverParameters, new ImapAuthenticationParameters(userName, password, this.Authentication, this.Security), ImapConnectionSettings.requiredCapabilities);
			}
			return result;
		}

		// Token: 0x04000849 RID: 2121
		private static readonly IServerCapabilities requiredCapabilities = new ImapServerCapabilities().Add("IMAP4REV1");

		// Token: 0x0400084A RID: 2122
		private readonly ConnectionParameters connectionParameters = new ConnectionParameters(new UniquelyNamedObject(), new NullLog(), long.MaxValue, Convert.ToInt32(TimeSpan.FromSeconds(20.0).TotalMilliseconds));
	}
}
