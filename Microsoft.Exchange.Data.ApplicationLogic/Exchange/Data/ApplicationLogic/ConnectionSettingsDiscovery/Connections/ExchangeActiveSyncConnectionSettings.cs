using System;
using System.Net;
using System.Security;
using System.Text;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Connections.Eas;

namespace Microsoft.Exchange.Data.ApplicationLogic.ConnectionSettingsDiscovery.Connections
{
	// Token: 0x02000196 RID: 406
	internal class ExchangeActiveSyncConnectionSettings : ProtocolSpecificConnectionSettings
	{
		// Token: 0x06000F71 RID: 3953 RVA: 0x0003F174 File Offset: 0x0003D374
		public ExchangeActiveSyncConnectionSettings() : base(ConnectionSettingsType.ExchangeActiveSync)
		{
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x0003F1C5 File Offset: 0x0003D3C5
		// (set) Token: 0x06000F73 RID: 3955 RVA: 0x0003F1CD File Offset: 0x0003D3CD
		public string EndpointAddressOverride { get; set; }

		// Token: 0x06000F74 RID: 3956 RVA: 0x0003F1D8 File Offset: 0x0003D3D8
		public override string ToMultiLineString(string lineSeparator)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Get {0}", base.ToMultiLineString(lineSeparator));
			return stringBuilder.ToString();
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x0003F204 File Offset: 0x0003D404
		protected override OperationStatusCode TestUserCanLogonWithCurrentSettings(SmtpAddress email, string userName, SecureString password)
		{
			EasAuthenticationParameters authenticationParameters = new EasAuthenticationParameters(new NetworkCredential(userName, password), email.Local, email.Domain, string.IsNullOrEmpty(this.EndpointAddressOverride) ? null : this.EndpointAddressOverride);
			IEasConnection easConnection = EasConnection.CreateInstance(this.connectionParameters, authenticationParameters, this.deviceParameters);
			return easConnection.TestLogon();
		}

		// Token: 0x04000843 RID: 2115
		private const string DeviceId = "REALM_DISCOVERY0";

		// Token: 0x04000844 RID: 2116
		private const string DeviceType = "RealmDiscoveryEasDeviceType";

		// Token: 0x04000845 RID: 2117
		private const string UserAgent = "ExchangeTestConnectionSettingsAgent";

		// Token: 0x04000846 RID: 2118
		private readonly EasConnectionParameters connectionParameters = new EasConnectionParameters(new UniquelyNamedObject(), new NullLog(), EasProtocolVersion.Version140, false, false, null);

		// Token: 0x04000847 RID: 2119
		private readonly EasDeviceParameters deviceParameters = new EasDeviceParameters("REALM_DISCOVERY0", "RealmDiscoveryEasDeviceType", "ExchangeTestConnectionSettingsAgent", "");
	}
}
