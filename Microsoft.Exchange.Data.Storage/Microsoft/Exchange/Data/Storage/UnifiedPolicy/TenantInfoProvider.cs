using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.CompliancePolicy;
using Microsoft.Office.CompliancePolicy.PolicySync;

namespace Microsoft.Exchange.Data.Storage.UnifiedPolicy
{
	// Token: 0x02000E93 RID: 3731
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class TenantInfoProvider : ITenantInfoProvider, IDisposable
	{
		// Token: 0x17002270 RID: 8816
		// (get) Token: 0x060081CE RID: 33230 RVA: 0x0023774F File Offset: 0x0023594F
		// (set) Token: 0x060081CF RID: 33231 RVA: 0x00237757 File Offset: 0x00235957
		public ExchangePrincipal SyncMailboxPrincipal { get; private set; }

		// Token: 0x060081D0 RID: 33232 RVA: 0x00237760 File Offset: 0x00235960
		public void Save(TenantInfo tenantInfo)
		{
			try
			{
				using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(this.SyncMailboxPrincipal, CultureInfo.InvariantCulture, "Client=UnifiedPolicy;Action=CommitChanges;Interactive=False"))
				{
					using (UserConfiguration mailboxConfiguration = UserConfigurationHelper.GetMailboxConfiguration(mailboxSession, "TenantInfoConfigurations", UserConfigurationTypes.Stream, true))
					{
						using (Stream stream = mailboxConfiguration.GetStream())
						{
							BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
							binaryFormatter.Serialize(stream, tenantInfo);
						}
						mailboxConfiguration.Save();
					}
				}
			}
			catch (StoragePermanentException innerException)
			{
				throw new SyncAgentPermanentException("TenantInfoProvider.Save failed with StoragePermanentException", innerException);
			}
			catch (StorageTransientException innerException2)
			{
				throw new SyncAgentTransientException("TenantInfoProvider.Save failed with StorageTransientException", innerException2);
			}
			catch (IOException innerException3)
			{
				throw new SyncAgentTransientException("TenantInfoProvider.Save failed with IOException", innerException3);
			}
		}

		// Token: 0x060081D1 RID: 33233 RVA: 0x0023784C File Offset: 0x00235A4C
		public TenantInfo Load()
		{
			TenantInfo result;
			try
			{
				using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(this.SyncMailboxPrincipal, CultureInfo.InvariantCulture, "Client=UnifiedPolicy;Action=CommitChanges;Interactive=False"))
				{
					using (UserConfiguration mailboxConfiguration = UserConfigurationHelper.GetMailboxConfiguration(mailboxSession, "TenantInfoConfigurations", UserConfigurationTypes.Stream, false))
					{
						if (mailboxConfiguration == null)
						{
							result = null;
						}
						else
						{
							using (Stream stream = mailboxConfiguration.GetStream())
							{
								if (stream == null || stream.Length == 0L)
								{
									result = null;
								}
								else
								{
									BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
									TenantInfo tenantInfo = (TenantInfo)binaryFormatter.Deserialize(stream);
									result = tenantInfo;
								}
							}
						}
					}
				}
			}
			catch (StoragePermanentException innerException)
			{
				throw new SyncAgentPermanentException("TenantInfoProvider.Load failed with StoragePermanentException", innerException);
			}
			catch (StorageTransientException innerException2)
			{
				throw new SyncAgentTransientException("TenantInfoProvider.Load failed with StorageTransientException", innerException2);
			}
			catch (IOException innerException3)
			{
				throw new SyncAgentTransientException("TenantInfoProvider.Load failed with IOException", innerException3);
			}
			return result;
		}

		// Token: 0x060081D2 RID: 33234 RVA: 0x00237954 File Offset: 0x00235B54
		public TenantInfoProvider(ExchangePrincipal syncMailboxPrincipal)
		{
			this.SyncMailboxPrincipal = syncMailboxPrincipal;
		}

		// Token: 0x060081D3 RID: 33235 RVA: 0x00237963 File Offset: 0x00235B63
		public void Dispose()
		{
		}
	}
}
