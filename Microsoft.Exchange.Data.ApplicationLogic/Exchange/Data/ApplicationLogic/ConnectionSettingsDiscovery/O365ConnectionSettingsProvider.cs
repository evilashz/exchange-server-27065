using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.ApplicationLogic.ConnectionSettingsDiscovery.Connections;
using Microsoft.Exchange.Data.ApplicationLogic.Diagnostics;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.ApplicationLogic.ConnectionSettingsDiscovery
{
	// Token: 0x0200019E RID: 414
	internal class O365ConnectionSettingsProvider : IConnectionSettingsReadProvider
	{
		// Token: 0x06000FB3 RID: 4019 RVA: 0x00040024 File Offset: 0x0003E224
		public O365ConnectionSettingsProvider(ILogAdapter logAdapter)
		{
			if (logAdapter == null)
			{
				throw new ArgumentNullException("logAdapter", "The logAdapter argument cannot be null.");
			}
			this.log = logAdapter;
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x00040046 File Offset: 0x0003E246
		public string SourceId
		{
			get
			{
				return "Exchange.O365ConnectionSettingsProvider";
			}
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x00040414 File Offset: 0x0003E614
		public IEnumerable<ConnectionSettings> GetConnectionSettingsMatchingEmail(SmtpAddress email)
		{
			Office365ConnectionSettings connectionSettings = null;
			this.log.ExecuteMonitoredOperation(ConnectionSettingsDiscoveryMetadata.GetOffice365ConnectionSettings, delegate
			{
				try
				{
					ADSessionSettings sessionSettings = ADSessionSettings.FromTenantAcceptedDomain(email.Domain);
					ITenantRecipientSession recipientSession = DirectorySessionFactory.Default.CreateTenantRecipientSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 78, "GetConnectionSettingsMatchingEmail", "f:\\15.00.1497\\sources\\dev\\data\\src\\ApplicationLogic\\ConnectionSettingsDiscovery\\ConnectionSettingsProviders\\O365ConnectionSettingsProvider.cs");
					MiniRecipient adUser = null;
					Action action = delegate()
					{
						adUser = recipientSession.FindByProxyAddress<MiniRecipient>(new SmtpProxyAddress(email.ToString(), true));
					};
					bool flag = false;
					int i = 0;
					while (i < 1)
					{
						i++;
						try
						{
							action();
							flag = true;
							break;
						}
						catch (DataSourceTransientException ex)
						{
							this.log.Trace("Caught an exception from directory while trying to find an AD user object for email address {0}. Exception: {1}", new object[]
							{
								email,
								ex
							});
						}
						catch (DataSourceOperationException ex2)
						{
							this.log.Trace("Caught an exception from directory while trying to find an AD user object for email address {0}. Exception: {1}", new object[]
							{
								email,
								ex2
							});
						}
					}
					if (!flag)
					{
						i++;
						action();
					}
					if (adUser != null)
					{
						connectionSettings = new Office365ConnectionSettings(adUser);
					}
					else
					{
						this.log.Trace("Found Office365 connection settings for email address {0} when AD user object does not exist", new object[]
						{
							email
						});
						connectionSettings = new Office365ConnectionSettings();
					}
				}
				catch (CannotResolveTenantNameException exception)
				{
					this.log.LogException(exception, "Failed to find Office365 connection settings for email address {0}. No tenant exists with domain name: ", new object[]
					{
						email,
						email.Domain
					});
				}
			});
			if (connectionSettings != null)
			{
				this.log.LogOperationResult(ConnectionSettingsDiscoveryMetadata.Office365ConnectionSettingsFound, email.Domain, true);
				yield return new ConnectionSettings(this, connectionSettings, null);
			}
			this.log.LogOperationResult(ConnectionSettingsDiscoveryMetadata.Office365ConnectionSettingsFound, email.Domain, false);
			yield break;
		}

		// Token: 0x04000864 RID: 2148
		private const int MaxRetryForADTransient = 2;

		// Token: 0x04000865 RID: 2149
		private readonly ILogAdapter log;
	}
}
