using System;
using System.Management.Automation;
using System.Net.Sockets;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Migration.DataAccessLayer
{
	// Token: 0x02000091 RID: 145
	internal class ImapEndpoint : MigrationEndpointBase
	{
		// Token: 0x06000879 RID: 2169 RVA: 0x000255A2 File Offset: 0x000237A2
		public ImapEndpoint(MigrationEndpoint presentationObject) : base(presentationObject)
		{
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x000255AB File Offset: 0x000237AB
		public ImapEndpoint() : base(MigrationType.IMAP)
		{
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x000255B4 File Offset: 0x000237B4
		// (set) Token: 0x0600087C RID: 2172 RVA: 0x000255DB File Offset: 0x000237DB
		public int Port
		{
			get
			{
				return base.ExtendedProperties.Get<int>("Port", (this.Security == IMAPSecurityMechanism.Ssl) ? 993 : 143);
			}
			set
			{
				base.ExtendedProperties.Set<int>("Port", value);
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x000255F0 File Offset: 0x000237F0
		// (set) Token: 0x0600087E RID: 2174 RVA: 0x00025658 File Offset: 0x00023858
		public IMAPAuthenticationMechanism Authentication
		{
			get
			{
				switch (base.AuthenticationMethod)
				{
				case AuthenticationMethod.Basic:
					return IMAPAuthenticationMechanism.Basic;
				case AuthenticationMethod.Ntlm:
					return IMAPAuthenticationMechanism.Ntlm;
				}
				throw new AuthenticationMethodNotSupportedException(base.AuthenticationMethod.ToString(), this.PreferredMigrationType.ToString(), string.Join<AuthenticationMethod>(",", this.SupportedAuthenticationMethods));
			}
			set
			{
				if (value == IMAPAuthenticationMechanism.Basic)
				{
					base.AuthenticationMethod = AuthenticationMethod.Basic;
					return;
				}
				if (value != IMAPAuthenticationMechanism.Ntlm)
				{
					throw new AuthenticationMethodNotSupportedException(base.AuthenticationMethod.ToString(), this.PreferredMigrationType.ToString(), string.Join<AuthenticationMethod>(",", this.SupportedAuthenticationMethods));
				}
				base.AuthenticationMethod = AuthenticationMethod.Ntlm;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x000256BA File Offset: 0x000238BA
		// (set) Token: 0x06000880 RID: 2176 RVA: 0x000256CD File Offset: 0x000238CD
		public IMAPSecurityMechanism Security
		{
			get
			{
				return base.ExtendedProperties.Get<IMAPSecurityMechanism>("Security", IMAPSecurityMechanism.Ssl);
			}
			set
			{
				base.ExtendedProperties.Set<IMAPSecurityMechanism>("Security", value);
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x000256E0 File Offset: 0x000238E0
		public override ConnectionSettingsBase ConnectionSettings
		{
			get
			{
				return new IMAPConnectionSettings
				{
					Server = this.RemoteServer,
					Port = this.Port,
					Authentication = this.Authentication,
					Security = this.Security
				};
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x00025729 File Offset: 0x00023929
		public override MigrationType PreferredMigrationType
		{
			get
			{
				return MigrationType.IMAP;
			}
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0002572C File Offset: 0x0002392C
		public override void InitializeFromAutoDiscover(SmtpAddress emailAddress, PSCredential credentials)
		{
			throw new AutoDiscoverNotSupportedException(base.EndpointType);
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0002573C File Offset: 0x0002393C
		public override void VerifyConnectivity()
		{
			using (TcpClient tcpClient = new TcpClient())
			{
				IAsyncResult asyncResult = tcpClient.BeginConnect(this.RemoteServer, this.Port, null, null);
				if (!asyncResult.AsyncWaitHandle.WaitOne(ImapEndpoint.ConnectionTimeout))
				{
					throw new MigrationServerConnectionTimeoutException(this.RemoteServer, ImapEndpoint.ConnectionTimeout);
				}
				if (!asyncResult.IsCompleted)
				{
					throw new MigrationServerConnectionFailedException(this.RemoteServer);
				}
				try
				{
					tcpClient.EndConnect(asyncResult);
				}
				catch (SocketException innerException)
				{
					throw new MigrationServerConnectionFailedException(this.RemoteServer, innerException);
				}
			}
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x000257F0 File Offset: 0x000239F0
		protected override void ApplyAutodiscoverSettings(AutodiscoverClientResponse response)
		{
			throw new AutoDiscoverNotSupportedException(base.EndpointType);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x000257FD File Offset: 0x000239FD
		protected override void ApplyAdditionalProperties(MigrationEndpoint presentationObject)
		{
			presentationObject.Authentication = new AuthenticationMethod?(base.AuthenticationMethod);
			presentationObject.Security = new IMAPSecurityMechanism?(this.Security);
			presentationObject.Port = new int?(this.Port);
			base.ApplyAdditionalProperties(presentationObject);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0002583C File Offset: 0x00023A3C
		protected override void InitializeFromPresentationObject(MigrationEndpoint endpoint)
		{
			base.InitializeFromPresentationObject(endpoint);
			if (endpoint.Port != null)
			{
				this.Port = endpoint.Port.Value;
				this.Security = endpoint.Security.Value;
			}
		}

		// Token: 0x04000358 RID: 856
		private static readonly TimeSpan ConnectionTimeout = TimeSpan.FromSeconds(15.0);
	}
}
