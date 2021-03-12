using System;
using System.Globalization;
using System.Security;

namespace Microsoft.Exchange.Data.ApplicationLogic.ConnectionSettingsDiscovery.Connections
{
	// Token: 0x02000193 RID: 403
	internal class ConnectionSettings
	{
		// Token: 0x06000F5A RID: 3930 RVA: 0x0003ECA9 File Offset: 0x0003CEA9
		public ConnectionSettings(IConnectionSettingsReadProvider provider, ProtocolSpecificConnectionSettings incomingSettings, SmtpConnectionSettings outgoingSettings)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider", "The provider parameter cannot be null.");
			}
			this.Initialize(provider, incomingSettings, outgoingSettings);
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0003ECCD File Offset: 0x0003CECD
		public ConnectionSettings(ProtocolSpecificConnectionSettings incomingSettings, SmtpConnectionSettings outgoingSettings)
		{
			this.Initialize(null, incomingSettings, outgoingSettings);
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x0003ECDE File Offset: 0x0003CEDE
		// (set) Token: 0x06000F5D RID: 3933 RVA: 0x0003ECE6 File Offset: 0x0003CEE6
		public string SourceId { get; private set; }

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x0003ECEF File Offset: 0x0003CEEF
		// (set) Token: 0x06000F5F RID: 3935 RVA: 0x0003ECF7 File Offset: 0x0003CEF7
		public SmtpConnectionSettings OutgoingConnectionSettings { get; private set; }

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000F60 RID: 3936 RVA: 0x0003ED00 File Offset: 0x0003CF00
		// (set) Token: 0x06000F61 RID: 3937 RVA: 0x0003ED08 File Offset: 0x0003CF08
		public ProtocolSpecificConnectionSettings IncomingConnectionSettings { get; private set; }

		// Token: 0x06000F62 RID: 3938 RVA: 0x0003ED14 File Offset: 0x0003CF14
		public bool TestUserCanLogon(SmtpAddress email, ref string userName, SecureString password)
		{
			bool flag = this.IncomingConnectionSettings.TestUserCanLogon(email, ref userName, password);
			if (this.OutgoingConnectionSettings != null)
			{
				string text = userName;
				flag &= this.OutgoingConnectionSettings.TestUserCanLogon(email, ref text, password);
			}
			return flag;
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0003ED4E File Offset: 0x0003CF4E
		public string ToMultiLineString(string lineSeparator)
		{
			return this.IncomingConnectionSettings.ToMultiLineString(lineSeparator) + ((this.OutgoingConnectionSettings != null) ? (lineSeparator + this.OutgoingConnectionSettings.ToMultiLineString(lineSeparator)) : string.Empty);
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0003ED82 File Offset: 0x0003CF82
		public override string ToString()
		{
			return this.ToMultiLineString(" ");
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0003ED90 File Offset: 0x0003CF90
		private void Initialize(IConnectionSettingsReadProvider provider, ProtocolSpecificConnectionSettings incomingSettings, SmtpConnectionSettings outgoingSettings)
		{
			if (incomingSettings == null)
			{
				throw new ArgumentNullException("incomingSettings", "The incomingSettings parameter cannot be null.");
			}
			ConnectionSettingsType connectionType = incomingSettings.ConnectionType;
			if (connectionType <= ConnectionSettingsType.ExchangeActiveSync)
			{
				if (connectionType == ConnectionSettingsType.Office365 || connectionType == ConnectionSettingsType.ExchangeActiveSync)
				{
					if (outgoingSettings != null)
					{
						throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The outgoingSettings parameter is invalid (ConnectionType: {0}). It should be set to null for {1} settings.", new object[]
						{
							outgoingSettings.ConnectionType,
							incomingSettings.ConnectionType
						}));
					}
					goto IL_10B;
				}
			}
			else if (connectionType != ConnectionSettingsType.Imap && connectionType != ConnectionSettingsType.Pop)
			{
				if (connectionType == ConnectionSettingsType.Smtp)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The incomingSettings parameter cannot be {0}. That is an outgoing protocol.", new object[]
					{
						incomingSettings.ConnectionType
					}));
				}
			}
			else
			{
				if (outgoingSettings == null)
				{
					throw new ArgumentNullException(string.Format(CultureInfo.InvariantCulture, "The outgoingSettings parameter cannot be null for {0} settings. It must be Smtp.", new object[]
					{
						incomingSettings.ConnectionType
					}));
				}
				goto IL_10B;
			}
			throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "The incommingSettings argument has an unexpected ConnectionType: {0}.", new object[]
			{
				incomingSettings.ConnectionType
			}));
			IL_10B:
			this.SourceId = ((provider == null) ? ConnectionSettings.UserSpecified : provider.SourceId);
			this.OutgoingConnectionSettings = outgoingSettings;
			this.IncomingConnectionSettings = incomingSettings;
		}

		// Token: 0x04000837 RID: 2103
		public static string UserSpecified = "UserSpecified";
	}
}
