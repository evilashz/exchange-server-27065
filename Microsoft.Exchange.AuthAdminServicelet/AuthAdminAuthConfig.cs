using System;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Servicelets.AuthAdmin.Messages;

namespace Microsoft.Exchange.Servicelets.AuthAdmin
{
	// Token: 0x02000007 RID: 7
	internal class AuthAdminAuthConfig
	{
		// Token: 0x06000012 RID: 18 RVA: 0x0000255F File Offset: 0x0000075F
		internal AuthAdminAuthConfig(AuthAdminContext context, WaitHandle stopEvent)
		{
			this.Context = context;
			this.StopEvent = stopEvent;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002575 File Offset: 0x00000775
		// (set) Token: 0x06000014 RID: 20 RVA: 0x0000257D File Offset: 0x0000077D
		private protected AuthAdminContext Context { protected get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002586 File Offset: 0x00000786
		// (set) Token: 0x06000016 RID: 22 RVA: 0x0000258E File Offset: 0x0000078E
		private protected WaitHandle StopEvent { protected get; private set; }

		// Token: 0x06000017 RID: 23 RVA: 0x000025B4 File Offset: 0x000007B4
		internal void DoScheduledWork(ITopologyConfigurationSession session)
		{
			if (this.StopEvent.WaitOne(0, false))
			{
				return;
			}
			this.Context.Logger.Log(MigrationEventType.Information, "Starting Authentication Configuration task", new object[0]);
			AuthConfig authConfig = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				authConfig = AuthConfig.Read(session);
			});
			if (adoperationResult != ADOperationResult.Success)
			{
				this.Context.Logger.Log(MigrationEventType.Warning, "Unable to read Auth Config, result = {0}", new object[]
				{
					adoperationResult.ErrorCode.ToString()
				});
				if (adoperationResult.Exception is TransientException)
				{
					throw adoperationResult.Exception;
				}
				return;
			}
			else
			{
				if (authConfig == null)
				{
					throw new InvalidAuthConfigurationException(DirectoryStrings.ErrorInvalidAuthSettings(string.Empty));
				}
				if (authConfig.NextCertificateEffectiveDate == null || authConfig.NextCertificateEffectiveDate == null)
				{
					this.Context.Logger.Log(MigrationEventType.Information, "Next effective date is not set. Task complete", new object[0]);
					return;
				}
				ExDateTime exDateTime = (ExDateTime)authConfig.NextCertificateEffectiveDate.Value;
				if (exDateTime.ToUtc() > ExDateTime.UtcNow)
				{
					this.Context.Logger.Log(MigrationEventType.Information, "Next effective date has not yet occurred: {0}.  Task complete", new object[]
					{
						exDateTime.ToUtc().ToString()
					});
					return;
				}
				this.Context.Logger.Log(MigrationEventType.Information, "Next effective date {0} has occured, performing automatic certificate publish", new object[]
				{
					exDateTime.ToUtc().ToString()
				});
				if (string.IsNullOrEmpty(authConfig.NextCertificateThumbprint) || string.Compare(authConfig.NextCertificateThumbprint, authConfig.CurrentCertificateThumbprint, StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.Context.Logger.Log(MigrationEventType.Warning, "Next effective certificate thumbprint not set or same as current thumbprint, ignoring", new object[0]);
					authConfig.NextCertificateThumbprint = null;
					authConfig.NextCertificateEffectiveDate = null;
				}
				else
				{
					authConfig.PreviousCertificateThumbprint = authConfig.CurrentCertificateThumbprint;
					authConfig.CurrentCertificateThumbprint = authConfig.NextCertificateThumbprint;
					authConfig.NextCertificateThumbprint = null;
					authConfig.NextCertificateEffectiveDate = null;
				}
				session.Save(authConfig);
				this.Context.Logger.LogTerseEvent(MigrationEventType.Information, MSExchangeAuthAdminEventLogConstants.Tuple_CurrentSigningUpdated, new string[]
				{
					authConfig.CurrentCertificateThumbprint
				});
				return;
			}
		}
	}
}
