using System;
using System.Net;
using System.Net.Sockets;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Transport.RightsManagement;

namespace Microsoft.Exchange.MessagingPolicies.RmSvcAgent
{
	// Token: 0x02000022 RID: 34
	internal sealed class RmsDecryptionAgent : RoutingAgent
	{
		// Token: 0x060000AC RID: 172 RVA: 0x000095AC File Offset: 0x000077AC
		public RmsDecryptionAgent(AgentInstanceController agentInstanceController)
		{
			this.agentInstanceController = agentInstanceController;
			base.OnSubmittedMessage += this.OnSubmittedMessageEventHandler;
			lock (RmsDecryptionAgent.SyncObject)
			{
				if (RmsDecryptionAgent.localIp == null)
				{
					try
					{
						RmsDecryptionAgent.localIp = Dns.GetHostEntry(Dns.GetHostName());
					}
					catch (SocketException)
					{
					}
				}
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000962C File Offset: 0x0000782C
		private void OnSubmittedMessageEventHandler(SubmittedMessageEventSource eventSource, QueuedMessageEventArgs args)
		{
			this.source = eventSource;
			this.tenantId = base.MailItem.TenantId;
			this.messageId = base.MailItem.Message.MessageId;
			DecryptionBaseComponent decryptionBaseComponent = new DecryptionBaseComponent(base.MailItem, new OnProcessDecryption(this.OnProcessDecryption));
			if (!decryptionBaseComponent.ShouldProcess())
			{
				return;
			}
			if (!this.agentInstanceController.TryMakeActive(this.tenantId))
			{
				this.TracePass("Unable to activate RmsDecryptionAgent because reaching capacity limit - deferring message.", new object[0]);
				this.source.Defer(RmsClientManager.AppSettings.ActiveAgentCapDeferInterval, RmsDecryptionAgent.ActiveAgentsCapResponse);
				return;
			}
			this.isActive = true;
			decryptionBaseComponent.StartDecryption();
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000096D4 File Offset: 0x000078D4
		private object OnProcessDecryption(DecryptionStatus status, TransportDecryptionSetting settings, AgentAsyncState asyncState, Exception exception)
		{
			if (status == DecryptionStatus.StartAsync)
			{
				return base.GetAgentAsyncContext();
			}
			if (status == DecryptionStatus.ConfigurationLoadFailure)
			{
				this.OnConfigurationLoadFailure(settings, exception);
			}
			else if (status == DecryptionStatus.TransientFailure)
			{
				this.OnTransientFailure(settings, exception);
			}
			else if (status == DecryptionStatus.PermanentFailure)
			{
				this.OnPermanentFailure(settings);
			}
			else if (status == DecryptionStatus.Success && RmsDecryptionAgent.localIp != null && RmsDecryptionAgent.localIp.AddressList != null && RmsDecryptionAgent.localIp.AddressList.Length != 0)
			{
				string localTcpInfo = RmsDecryptionAgent.localIp.AddressList[0].ToString();
				HeaderList headers = base.MailItem.Message.RootPart.Headers;
				Utils.PatchReceiverHeader(headers, localTcpInfo, "Transport Decrypted");
			}
			if (asyncState != null)
			{
				asyncState.Complete();
			}
			if (this.isActive)
			{
				this.agentInstanceController.MakeInactive(this.tenantId);
				this.isActive = false;
			}
			return null;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000979C File Offset: 0x0000799C
		private void OnConfigurationLoadFailure(TransportDecryptionSetting setting, Exception exception)
		{
			if (this.IncrementDeferralCountAndCheckCap())
			{
				this.TracePass("Deferred Message {0}", new object[]
				{
					this.messageId
				});
				this.source.Defer(RmsClientManager.AppSettings.TransientErrorDeferInterval);
				return;
			}
			OrganizationId organizationId = Utils.OrgIdFromMailItem(base.MailItem);
			DecryptionBaseComponent.Logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_FailedToLoadIRMConfiguration, null, new object[]
			{
				RmsComponent.DecryptionAgent,
				organizationId.ToString(),
				exception
			});
			this.OnPermanentFailure(setting);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00009828 File Offset: 0x00007A28
		private void OnTransientFailure(TransportDecryptionSetting setting, Exception exception)
		{
			if (this.IncrementDeferralCountAndCheckCap())
			{
				this.TracePass("Deferred Message {0}", new object[]
				{
					this.messageId
				});
				this.source.Defer(RmsClientManager.AppSettings.TransientErrorDeferInterval);
				return;
			}
			DecryptionBaseComponent.Logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_TransportDecryptionPermanentException, base.MailItem.Message.MessageId, new object[]
			{
				base.MailItem.Message.MessageId,
				Utils.OrgIdFromMailItem(base.MailItem),
				exception
			});
			this.OnPermanentFailure(setting);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000098C4 File Offset: 0x00007AC4
		private void OnPermanentFailure(TransportDecryptionSetting setting)
		{
			RmsDecryptionAgentPerfCounters.MessageFailedToDecrypt.Increment();
			DecryptionBaseComponent.UpdatePercentileCounters(false);
			if (setting != TransportDecryptionSetting.Mandatory)
			{
				if (setting == TransportDecryptionSetting.Optional)
				{
					Utils.StampXHeader(base.MailItem.Message, "X-MS-Exchange-Forest-TransportDecryption-Action", "Skipped");
				}
				return;
			}
			this.TraceFail("Transport Decryption will NDR message {0}, Response {1} because of permanent decryption error", new object[]
			{
				this.messageId,
				"Microsoft Exchange Transport cannot RMS decrypt the message."
			});
			EnvelopeRecipientCollection recipients = base.MailItem.Recipients;
			if (recipients == null)
			{
				this.TraceFail("Transport Decryption has no recipients to NDR for message {0}", new object[]
				{
					this.messageId
				});
				return;
			}
			for (int i = recipients.Count - 1; i >= 0; i--)
			{
				base.MailItem.Recipients.Remove(recipients[i], DsnType.Failure, Utils.GetResponseForNDR(new string[]
				{
					"Microsoft Exchange Transport cannot RMS decrypt the message."
				}));
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000999C File Offset: 0x00007B9C
		private bool IncrementDeferralCountAndCheckCap()
		{
			int num = Utils.IncrementDeferralCount(base.MailItem, "Microsoft.Exchange.RmsDecryptionAgent.DeferralCount");
			if (num == -1)
			{
				this.TraceFail("Deferral count of message {0} is broken", new object[]
				{
					this.messageId
				});
				return false;
			}
			if (num > 1)
			{
				this.TracePass("Message {0} has been deferred {1} times", new object[]
				{
					this.messageId,
					num - 1
				});
			}
			return num <= 2;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00009A0E File Offset: 0x00007C0E
		private void TracePass(string formatString, params object[] args)
		{
			if (base.MailItem != null)
			{
				RmsClientManager.TracePass(this, base.MailItem.SystemProbeId, formatString, args);
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00009A2B File Offset: 0x00007C2B
		private void TraceFail(string formatString, params object[] args)
		{
			if (base.MailItem != null)
			{
				RmsClientManager.TraceFail(this, base.MailItem.SystemProbeId, formatString, args);
			}
		}

		// Token: 0x04000109 RID: 265
		private const int MaxDeferrals = 2;

		// Token: 0x0400010A RID: 266
		private const string DeferralCountProperty = "Microsoft.Exchange.RmsDecryptionAgent.DeferralCount";

		// Token: 0x0400010B RID: 267
		private static readonly SmtpResponse ActiveAgentsCapResponse = new SmtpResponse("452", "4.3.2", new string[]
		{
			"Already processing maximum number of RMS message for Transport Decryption"
		});

		// Token: 0x0400010C RID: 268
		private static readonly object SyncObject = new object();

		// Token: 0x0400010D RID: 269
		private static IPHostEntry localIp;

		// Token: 0x0400010E RID: 270
		private readonly AgentInstanceController agentInstanceController;

		// Token: 0x0400010F RID: 271
		private SubmittedMessageEventSource source;

		// Token: 0x04000110 RID: 272
		private Guid tenantId;

		// Token: 0x04000111 RID: 273
		private string messageId;

		// Token: 0x04000112 RID: 274
		private bool isActive;
	}
}
