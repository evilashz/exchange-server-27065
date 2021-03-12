using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.ExchangeCertificate;
using Microsoft.Exchange.ServiceHost;
using Microsoft.Exchange.Servicelets.CertificateNotification.Messages;

namespace Microsoft.Exchange.Servicelets
{
	// Token: 0x02000002 RID: 2
	public class CertificateNotificationServicelet : Servicelet
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public CertificateNotificationServicelet()
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020ED File Offset: 0x000002ED
		internal CertificateNotificationServicelet(ITopologyConfigurationSession adSession, OrganizationId orgId, Server serverObj)
		{
			this.adSession = adSession;
			this.orgId = orgId;
			this.serverObj = serverObj;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002120 File Offset: 0x00000320
		public override void Work()
		{
			Thread.Sleep(15000);
			int num = 1440;
			for (;;)
			{
				try
				{
					if (!int.TryParse(ConfigurationManager.AppSettings["CertificateNotificationPollIntervalInMinutes"], out num))
					{
						num = 1440;
					}
				}
				catch (ConfigurationException)
				{
					num = 1440;
				}
				if (num <= 0 || num > 14400)
				{
					num = 1440;
				}
				try
				{
					if (!int.TryParse(ConfigurationManager.AppSettings["CertificateNotificationWarningDays"], out this.warningDays))
					{
						this.warningDays = 30;
					}
				}
				catch (ConfigurationException)
				{
					this.warningDays = 30;
				}
				if (this.warningDays <= 0 || this.warningDays > 400)
				{
					this.warningDays = 30;
				}
				this.adSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 185, "Work", "f:\\15.00.1497\\sources\\dev\\Management\\src\\ServiceHost\\Servicelets\\CertificateNotification\\Program\\CertificateNotificationServicelet.cs");
				this.orgId = ADSessionSettings.FromRootOrgScopeSet().CurrentOrganizationId;
				ExchangeCertificateRpc exchangeCertificateRpc = new ExchangeCertificateRpc();
				byte[] outputBlob = null;
				ExchangeCertificateRpcClient exchangeCertificateRpcClient = null;
				ExchangeCertificateRpcClient2 exchangeCertificateRpcClient2 = null;
				try
				{
					IEnumerable<Server> objects = new ServerIdParameter().GetObjects<Server>(this.orgId.ConfigurationUnit, this.adSession);
					if (objects == null || objects.Count<Server>() == 0)
					{
						goto IL_28B;
					}
					this.serverObj = objects.First<Server>();
					ExchangeCertificateRpcVersion exchangeCertificateRpcVersion = ExchangeCertificateRpcVersion.Version1;
					try
					{
						byte[] inBlob = exchangeCertificateRpc.SerializeInputParameters(ExchangeCertificateRpcVersion.Version2);
						exchangeCertificateRpcClient2 = new ExchangeCertificateRpcClient2(this.serverObj.Name);
						outputBlob = exchangeCertificateRpcClient2.GetCertificate2(0, inBlob);
						exchangeCertificateRpcVersion = ExchangeCertificateRpcVersion.Version2;
					}
					catch (RpcException)
					{
						exchangeCertificateRpcVersion = ExchangeCertificateRpcVersion.Version1;
					}
					if (exchangeCertificateRpcVersion == ExchangeCertificateRpcVersion.Version1)
					{
						byte[] inBlob2 = exchangeCertificateRpc.SerializeInputParameters(exchangeCertificateRpcVersion);
						exchangeCertificateRpcClient = new ExchangeCertificateRpcClient(this.serverObj.Name);
						outputBlob = exchangeCertificateRpcClient.GetCertificate(0, inBlob2);
					}
					ExchangeCertificateRpc exchangeCertificateRpc2 = new ExchangeCertificateRpc(exchangeCertificateRpcVersion, null, outputBlob);
					this.UpdateDataInMbx(exchangeCertificateRpc2.ReturnCertList);
				}
				catch (RpcClientException ex)
				{
					this.EventLog.LogEvent(CertificateNotificationEventLogConstants.Tuple_TransientException, string.Empty, new object[]
					{
						ex.ToString()
					});
				}
				catch (LocalizedException ex2)
				{
					this.EventLog.LogEvent(CertificateNotificationEventLogConstants.Tuple_TransientException, string.Empty, new object[]
					{
						ex2.ToString()
					});
				}
				catch (RpcException ex3)
				{
					this.EventLog.LogEvent(CertificateNotificationEventLogConstants.Tuple_TransientException, string.Empty, new object[]
					{
						ex3.ToString()
					});
				}
				finally
				{
					if (exchangeCertificateRpcClient2 != null)
					{
						exchangeCertificateRpcClient2.Dispose();
					}
					if (exchangeCertificateRpcClient != null)
					{
						exchangeCertificateRpcClient.Dispose();
					}
					this.adSession = null;
					this.asyncDataProvider = null;
					this.serverObj = null;
					this.orgId = null;
				}
				goto IL_252;
				IL_28B:
				if (base.StopEvent.WaitOne(TimeSpan.FromMinutes((double)num), false))
				{
					break;
				}
				continue;
				IL_252:
				this.EventLog.LogEvent(CertificateNotificationEventLogConstants.Tuple_OneRoundCompleted, string.Empty, new object[]
				{
					ExDateTime.Now.AddMinutes((double)num)
				});
				goto IL_28B;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000024FC File Offset: 0x000006FC
		public void UpdateDataInMbx(List<ExchangeCertificate> certificates)
		{
			this.asyncDataProvider = new AsyncOperationNotificationDataProvider(this.orgId);
			this.RemoveAllNotification();
			IEnumerable<ExchangeCertificate> certificates2 = from cert in certificates
			where cert.NotAfter <= (DateTime)ExDateTime.UtcNow.AddDays((double)this.warningDays) && cert.NotAfter >= (DateTime)ExDateTime.UtcNow && cert.Status != CertificateStatus.PendingRequest
			select cert;
			IEnumerable<ExchangeCertificate> certificates3 = from cert in certificates
			where cert.NotAfter < (DateTime)ExDateTime.UtcNow && cert.Status != CertificateStatus.PendingRequest
			select cert;
			this.CreateNotification(certificates2, false);
			this.CreateNotification(certificates3, true);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000025CC File Offset: 0x000007CC
		private void RemoveAllNotification()
		{
			IEnumerable<AsyncOperationNotification> enumerable = from notification in this.asyncDataProvider.GetNotificationDetails(new AsyncOperationType?(AsyncOperationType.CertExpiry), null, null, new ProviderPropertyDefinition[0])
			where notification.ExtendedAttributes == null || notification.ExtendedAttributes.Any((KeyValuePair<string, LocalizedString> item) => item.Key.Equals("ServerFqdn") && item.Value.Equals(new LocalizedString(this.serverObj.Fqdn)))
			select notification;
			foreach (AsyncOperationNotification instance in enumerable)
			{
				this.asyncDataProvider.Delete(instance);
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000265C File Offset: 0x0000085C
		private void CreateNotification(IEnumerable<ExchangeCertificate> certificates, bool isExpired)
		{
			ADRecipientOrAddress owner = null;
			MicrosoftExchangeRecipient microsoftExchangeRecipient = this.adSession.FindMicrosoftExchangeRecipient();
			if (microsoftExchangeRecipient == null)
			{
				this.EventLog.LogEvent(CertificateNotificationEventLogConstants.Tuple_MicrosoftExchangeRecipientNotFoundException, string.Empty, new object[]
				{
					string.Empty
				});
			}
			else
			{
				owner = new ADRecipientOrAddress(new Participant(microsoftExchangeRecipient));
			}
			foreach (ExchangeCertificate exchangeCertificate in certificates)
			{
				KeyValuePair<string, LocalizedString>[] extendedAttributes = new KeyValuePair<string, LocalizedString>[]
				{
					new KeyValuePair<string, LocalizedString>("ServerName", new LocalizedString(this.serverObj.Name)),
					new KeyValuePair<string, LocalizedString>("ServerFqdn", new LocalizedString(this.serverObj.Fqdn)),
					new KeyValuePair<string, LocalizedString>("ThumbPrint", new LocalizedString(exchangeCertificate.Thumbprint)),
					new KeyValuePair<string, LocalizedString>("FriendlyName", new LocalizedString(exchangeCertificate.FriendlyName)),
					new KeyValuePair<string, LocalizedString>("ExpireDate", new LocalizedString(exchangeCertificate.NotAfter.ToFileTimeUtc().ToString()))
				};
				string id = this.serverObj.Fqdn + "\\" + exchangeCertificate.Thumbprint;
				AsyncOperationNotificationDataProvider.CreateNotification(this.orgId, id, AsyncOperationType.CertExpiry, isExpired ? AsyncOperationStatus.CertExpired : AsyncOperationStatus.CertExpiring, new LocalizedString(exchangeCertificate.FriendlyName), owner, extendedAttributes, false);
				AsyncOperationNotificationDataProvider.SendNotificationEmail(this.orgId, id, false, null, false);
			}
		}

		// Token: 0x04000001 RID: 1
		private const int DefaultPoolMinutes = 1440;

		// Token: 0x04000002 RID: 2
		private const int MaxPoolMinutes = 14400;

		// Token: 0x04000003 RID: 3
		private const int DefaultWarningDaysBeforeExpire = 30;

		// Token: 0x04000004 RID: 4
		private const int MaxWarningDaysBeforeExpire = 400;

		// Token: 0x04000005 RID: 5
		private const int SleepSecondsBeforeStart = 15;

		// Token: 0x04000006 RID: 6
		private const string ServerName = "ServerName";

		// Token: 0x04000007 RID: 7
		private const string ServerFqdn = "ServerFqdn";

		// Token: 0x04000008 RID: 8
		private const string ThumbPrint = "ThumbPrint";

		// Token: 0x04000009 RID: 9
		private const string ExpireDate = "ExpireDate";

		// Token: 0x0400000A RID: 10
		private const string FriendlyName = "FriendlyName";

		// Token: 0x0400000B RID: 11
		private static readonly Guid ComponentGuid = new Guid("DEA08CF2-8501-480F-A7AE-A96443FCD4FD");

		// Token: 0x0400000C RID: 12
		private readonly ExEventLog EventLog = new ExEventLog(CertificateNotificationServicelet.ComponentGuid, "MSExchange Certificate Notification");

		// Token: 0x0400000D RID: 13
		private AsyncOperationNotificationDataProvider asyncDataProvider;

		// Token: 0x0400000E RID: 14
		private ITopologyConfigurationSession adSession;

		// Token: 0x0400000F RID: 15
		private OrganizationId orgId;

		// Token: 0x04000010 RID: 16
		private Server serverObj;

		// Token: 0x04000011 RID: 17
		private int warningDays;
	}
}
