﻿using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.RightsManagement;

namespace Microsoft.Exchange.MessagingPolicies.RmSvcAgent
{
	// Token: 0x0200002B RID: 43
	internal sealed class E4eDecryptionAgent : RoutingAgent
	{
		// Token: 0x060000DE RID: 222 RVA: 0x0000BB34 File Offset: 0x00009D34
		public E4eDecryptionAgent(E4eDecryptionAgentFactory factory)
		{
			this.agentFactory = factory;
			base.OnSubmittedMessage += this.OnSubmittedMessageEventHandler;
			this.breadcrumbs.Drop("AgentInitialized");
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000BE4C File Offset: 0x0000A04C
		public void OnSubmittedMessageEventHandler(SubmittedMessageEventSource source, QueuedMessageEventArgs args)
		{
			this.breadcrumbs.Drop("E4eDecryptionAgent Started");
			this.tenantId = args.MailItem.TenantId;
			this.source = source;
			this.args = args;
			EnvelopeRecipientCollection recipients = args.MailItem.Recipients;
			string messageIdInMetadata = string.Empty;
			string recipientInMetadata = string.Empty;
			EmailMessage rmsMessage = null;
			string htmlString = null;
			if (!E4eDecryptionHelper.VerifyMailItemProperties(args.MailItem.Message, recipients, out htmlString))
			{
				return;
			}
			OutboundConversionOptions outboundOptions = Utils.GetOutboundConversionOptions(args.MailItem);
			bool exit = false;
			bool flag = E4eAgentCommon.RunUnderExceptionHandler(args.MailItem, source, false, "Microsoft.Exchange.E4eDecryptionAgent.DeferralCount", delegate
			{
				OrganizationId organizationId = Utils.OrgIdFromMailItem(args.MailItem);
				string messageId = args.MailItem.Message.MessageId;
				E4eLog.Instance.LogInfo(args.MailItem.Message.MessageId, "[D]FileVersion: {0}", new object[]
				{
					"15.00.1497.010"
				});
				E4eLog.Instance.LogInfo(args.MailItem.Message.MessageId, "[D]P1 Sender: '{0}'", new object[]
				{
					args.MailItem.FromAddress.ToString()
				});
				E4eLog.Instance.LogInfo(args.MailItem.Message.MessageId, "[D]P2 Sender: '{0}'", new object[]
				{
					E4eHelper.GetP2From(args.MailItem.Message)
				});
				E4eHelper.LogAllE4eHeaders(args.MailItem.Message, "[D]");
				string version = E4eDecryptionHelper.GetVersion(ref htmlString);
				this.decryptionHelper = E4eHelper.GetE4eDecryptionHelper(version);
				if (!this.decryptionHelper.VerifyEncryptedAttachment(ref htmlString, organizationId, recipients, outboundOptions, messageId, null, out this.originalSender, out this.originalSenderOrgId, out messageIdInMetadata, out recipientInMetadata, out rmsMessage))
				{
					exit = true;
					return;
				}
				if (!this.originalSenderOrgId.Equals(organizationId))
				{
					if (this.decryptionHelper.IsE4eCrossTenantDecryptionEnabled())
					{
						E4eLog.Instance.LogInfo(args.MailItem.Message.MessageId, "Decryption will be allowed for cross tenant, IsE4eCrossTenantDecryptionEnabled == true. OriginalSenderOrgId != RecipientOrgId. OriginalSenderOrgId: {0}. RecipientOrgId: {1}.", new object[]
						{
							this.originalSenderOrgId.ToString(),
							organizationId.ToString()
						});
						return;
					}
					E4eLog.Instance.LogInfo(args.MailItem.Message.MessageId, "Decryption will be skipped. OriginalSenderOrgId != RecipientOrgId. OriginalSenderOrgId: {0}. RecipientOrgId: {1}.", new object[]
					{
						this.originalSenderOrgId.ToString(),
						organizationId.ToString()
					});
					exit = true;
				}
			}, new E4eHelper.CompleteProcessDelegate(this.CompleteProcess));
			if (!flag || exit)
			{
				return;
			}
			flag = E4eAgentCommon.RunUnderExceptionHandler(args.MailItem, source, false, "Microsoft.Exchange.E4eDecryptionAgent.DeferralCount", delegate
			{
				if (!this.TryPromoteActiveAgent())
				{
					return;
				}
				this.rmsDecryptor = this.decryptionHelper.CreateRmsDecryptor(this.originalSenderOrgId, messageIdInMetadata, recipientInMetadata, this.breadcrumbs, rmsMessage, outboundOptions, null, E4eLog.Instance, out this.publishingLicense, false);
			}, new E4eHelper.CompleteProcessDelegate(this.CompleteProcess));
			if (flag && this.rmsDecryptor != null)
			{
				try
				{
					this.decryptionHelper.DecryptMessage(this.rmsDecryptor, new AsyncCallback(this.OnDecryptionCompleted), new AgentAsyncState(base.GetAgentAsyncContext()));
					return;
				}
				catch (Exception ex)
				{
					this.IncrementFailureCount();
					this.CompleteProcess(null);
					E4eLog.Instance.LogError(args.MailItem.Message.MessageId, "Decryption will be skipped. Exception occurred while calling DecryptMessage. Exception: {0}", new object[]
					{
						ex.ToString()
					});
					return;
				}
			}
			this.IncrementFailureCount();
			this.CompleteProcess(null);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000C024 File Offset: 0x0000A224
		private void OnDecryptionCompleted(IAsyncResult asyncResult)
		{
			this.breadcrumbs.Drop("OnDecryptionCompleted");
			ArgumentValidator.ThrowIfNull("asyncResult", asyncResult);
			ArgumentValidator.ThrowIfNull("asyncResult.AsyncState", asyncResult.AsyncState);
			AgentAsyncState agentAsyncState = (AgentAsyncState)asyncResult.AsyncState;
			agentAsyncState.Resume();
			try
			{
				AsyncOperationResult<DecryptionResultData> asyncOperationResult = this.rmsDecryptor.EndDecrypt(asyncResult);
				if (asyncOperationResult.IsSucceeded)
				{
					E4eLog.Instance.LogInfo(this.args.MailItem.Message.MessageId, "Successfully decrypted the mail.", new object[0]);
					Utils.CopyHeadersDuringDecryption(this.args.MailItem.Message, asyncOperationResult.Data.DecryptedMessage);
					E4eHelper.OverrideMime(this.args.MailItem, asyncOperationResult.Data.DecryptedMessage);
					LamHelper.PublishSuccessfulE4eDecryptionToLAM(this.args.MailItem);
					this.IncrementSuccessCount();
					E4eHelper.RemoveHeader(this.args.MailItem.Message, "X-MS-Exchange-Organization-E4eEncryptMessage");
					E4eHelper.RemoveHeader(this.args.MailItem.Message, "X-MS-Exchange-Organization-E4eMessageEncrypted");
					E4eHelper.RemoveHeader(this.args.MailItem.Message, "X-MS-Exchange-Organization-E4eHtmlFileGenerated");
					if (Utils.GetXHeader(this.args.MailItem.Message, "X-MS-Exchange-Organization-E4eMessageOriginalSender") == null)
					{
						Utils.StampXHeader(this.args.MailItem.Message, "X-MS-Exchange-Organization-E4eMessageOriginalSender", this.originalSender);
					}
					if (Utils.GetXHeader(this.args.MailItem.Message, "X-MS-Exchange-Organization-E4eMessageOriginalSenderOrgId") == null)
					{
						Utils.StampXHeader(this.args.MailItem.Message, "X-MS-Exchange-Organization-E4eMessageOriginalSenderOrgId", E4eHelper.ToBase64String(this.originalSenderOrgId));
					}
					Utils.StampXHeader(this.args.MailItem.Message, "X-MS-Exchange-Organization-E4eMessageDecrypted", "true");
					Utils.StampXHeader(this.args.MailItem.Message, "X-MS-Exchange-Organization-E4eReEncryptMessage", "true");
					E4eHelper.RemoveHeader(this.args.MailItem.Message, "X-MS-Exchange-OMEMessageEncrypted");
					Utils.StampXHeader(this.args.MailItem.Message, "X-MS-Exchange-OMEMessageEncrypted", "false");
					E4eHelper.SetTransportPLAndULAndLicenseUri(this.args.MailItem, this.publishingLicense, asyncOperationResult.Data.UseLicense, asyncOperationResult.Data.LicenseUri);
				}
				else
				{
					this.IncrementFailureCount();
					this.HandleDecryptionException(base.MailItem, asyncOperationResult.Exception, asyncOperationResult.IsTransientException, this.source);
				}
			}
			finally
			{
				this.CompleteProcess(agentAsyncState);
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000C2BC File Offset: 0x0000A4BC
		private bool TryPromoteActiveAgent()
		{
			E4eLog.Instance.LogInfo(this.args.MailItem.Message.MessageId, "E4eDecryptionAgent is trying to promote itself as Active Agent ...", new object[0]);
			this.isActive = this.agentFactory.InstanceController.TryMakeActive(this.tenantId);
			if (!this.isActive)
			{
				E4eLog.Instance.LogInfo(this.args.MailItem.Message.MessageId, "Unable to make active E4eDecryptionAgent - deferring message.", new object[0]);
				this.source.Defer(RmsClientManager.AppSettings.ActiveAgentCapDeferInterval, Utils.GetResponseForDeferral(E4eAgentCommon.AgentCapReachedDeferralText));
			}
			else
			{
				E4eLog.Instance.LogInfo(this.args.MailItem.Message.MessageId, "E4eDecryptionAgent is promoted as Active Agent.", new object[0]);
			}
			return this.isActive;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000C392 File Offset: 0x0000A592
		private void CompleteProcess(AgentAsyncState agentAsyncState)
		{
			this.breadcrumbs.Drop("E4eDecryptionAgent CleanedUp");
			if (this.isActive)
			{
				this.agentFactory.InstanceController.MakeInactive(this.tenantId);
				this.isActive = false;
			}
			if (agentAsyncState != null)
			{
				agentAsyncState.Complete();
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000C3D2 File Offset: 0x0000A5D2
		private void HandleDecryptionException(MailItem mailItem, Exception exception, bool isTransientException, QueuedMessageEventSource source)
		{
			if (isTransientException)
			{
				E4eAgentCommon.HandleTransientException(mailItem, exception, null, source, false, "Microsoft.Exchange.E4eDecryptionAgent.DeferralCount");
				return;
			}
			E4eAgentCommon.HandlePermanentException(mailItem, exception, null, false);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000C3F1 File Offset: 0x0000A5F1
		private void IncrementSuccessCount()
		{
			if (!this.args.MailItem.IsProbeMessage)
			{
				E4eAgentPerfCounters.DecryptionSuccessCount.Increment();
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000C410 File Offset: 0x0000A610
		private void IncrementFailureCount()
		{
			if (!this.args.MailItem.IsProbeMessage)
			{
				E4eAgentPerfCounters.DecryptionFailureCount.Increment();
			}
		}

		// Token: 0x0400013A RID: 314
		private readonly Breadcrumbs<string> breadcrumbs = new Breadcrumbs<string>(8);

		// Token: 0x0400013B RID: 315
		private bool isActive;

		// Token: 0x0400013C RID: 316
		private SubmittedMessageEventSource source;

		// Token: 0x0400013D RID: 317
		private QueuedMessageEventArgs args;

		// Token: 0x0400013E RID: 318
		private E4eDecryptionHelper decryptionHelper;

		// Token: 0x0400013F RID: 319
		private Guid tenantId;

		// Token: 0x04000140 RID: 320
		private RmsDecryptor rmsDecryptor;

		// Token: 0x04000141 RID: 321
		private E4eDecryptionAgentFactory agentFactory;

		// Token: 0x04000142 RID: 322
		private string originalSender;

		// Token: 0x04000143 RID: 323
		private OrganizationId originalSenderOrgId;

		// Token: 0x04000144 RID: 324
		private string publishingLicense;
	}
}
