using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.ApplicationLogic.ConnectionSettingsDiscovery.Connections;
using Microsoft.Exchange.Data.ApplicationLogic.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.ConnectionSettingsDiscovery
{
	// Token: 0x0200019F RID: 415
	internal class ConnectionSettingsManager
	{
		// Token: 0x06000FB6 RID: 4022 RVA: 0x00040438 File Offset: 0x0003E638
		protected ConnectionSettingsManager(IConnectionSettingsWriteProvider[] writableProviders, IConnectionSettingsReadProvider[] readOnlyProviders)
		{
			this.readOnlyProviders = readOnlyProviders;
			this.writableProviders = writableProviders;
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x00040450 File Offset: 0x0003E650
		public static ConnectionSettingsManager GetInstanceForModernOutlook(ILogAdapter logAdapter)
		{
			if (logAdapter == null)
			{
				throw new ArgumentNullException("logAdapter", "The logAdapter parameter cannot be null.");
			}
			logAdapter.RegisterLogMetaData("ConnectionSettingsDiscovery", typeof(ConnectionSettingsDiscoveryMetadata));
			GlobalConnectionSettingsProvider globalConnectionSettingsProvider = new GlobalConnectionSettingsProvider(logAdapter);
			return new ConnectionSettingsManager(new IConnectionSettingsWriteProvider[]
			{
				globalConnectionSettingsProvider
			}, new IConnectionSettingsReadProvider[]
			{
				new O365ConnectionSettingsProvider(logAdapter),
				globalConnectionSettingsProvider
			});
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x000406D4 File Offset: 0x0003E8D4
		public IEnumerable<ConnectionSettings> GetConnectionSettingsMatchingEmail(SmtpAddress email)
		{
			foreach (IConnectionSettingsReadProvider provider in this.readOnlyProviders)
			{
				foreach (ConnectionSettings settings in provider.GetConnectionSettingsMatchingEmail(email))
				{
					yield return settings;
				}
			}
			yield break;
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x000406F8 File Offset: 0x0003E8F8
		public void SetConnectionSettings(SmtpAddress email, ConnectionSettings connectionSettings)
		{
			foreach (IConnectionSettingsWriteProvider connectionSettingsWriteProvider in this.writableProviders)
			{
				if (connectionSettingsWriteProvider.SourceId != connectionSettings.SourceId)
				{
					connectionSettingsWriteProvider.SetConnectionSettingsMatchingEmail(email, connectionSettings);
				}
			}
		}

		// Token: 0x04000866 RID: 2150
		private const string ConnectionSettingsDiscoveryLogName = "ConnectionSettingsDiscovery";

		// Token: 0x04000867 RID: 2151
		private readonly IConnectionSettingsWriteProvider[] writableProviders;

		// Token: 0x04000868 RID: 2152
		private readonly IConnectionSettingsReadProvider[] readOnlyProviders;
	}
}
