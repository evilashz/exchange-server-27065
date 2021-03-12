using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Common.Cache;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.RightsManagement;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.MessagingPolicies.RmSvcAgent
{
	// Token: 0x02000010 RID: 16
	internal sealed class AutomaticProtectionAgent : RoutingAgent
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00004048 File Offset: 0x00002248
		public AutomaticProtectionAgent(AutomaticProtectionAgentFactory factory)
		{
			this.agentFactory = factory;
			base.OnSubmittedMessage += this.OnSubmitEventHandler;
			base.OnRoutedMessage += this.OnRoutedEventHandler;
			this.breadcrumbs.Drop("AgentInitialized");
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000040A4 File Offset: 0x000022A4
		private static bool IsTransportDecryptionApplied(MailItem mailItem)
		{
			object obj;
			return mailItem.Properties.TryGetValue("Microsoft.Exchange.RightsManagement.TransportDecrypted", out obj);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000040C3 File Offset: 0x000022C3
		private static bool IsFirstHop(MailItem mailItem)
		{
			return mailItem.InboundDeliveryMethod == DeliveryMethod.Mailbox || mailItem.InboundDeliveryMethod == DeliveryMethod.File || Utils.CheckMuaSubmission(mailItem);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000040E0 File Offset: 0x000022E0
		private static bool IsProtectedByApa(EmailMessage message)
		{
			Header xheader = Utils.GetXHeader(message, "X-MS-Exchange-Organization-MessageProtectedByApa");
			bool flag;
			return xheader != null && bool.TryParse(xheader.Value, out flag) && flag;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00004110 File Offset: 0x00002310
		private static void GetTransportDecryptionLicenseUri(MailItem mailItem, out Uri licenseUri)
		{
			licenseUri = null;
			object obj;
			if (mailItem.Properties.TryGetValue("Microsoft.Exchange.RightsManagement.TransportDecryptionLicenseUri", out obj) && obj != null)
			{
				Uri.TryCreate((string)obj, UriKind.Absolute, out licenseUri);
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00004145 File Offset: 0x00002345
		private static SmtpResponse GetResponseForExceptionNDR(Exception exception, string[] additionalInfo)
		{
			return Utils.GetResponseForNDR(Utils.GetSmtpResponseTextsForException(exception, additionalInfo));
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00004154 File Offset: 0x00002354
		private static bool IsMessageProtectable(MailItem mailItem, object obj)
		{
			if (mailItem.Message.IsSystemMessage || mailItem.Message.IsOpaqueMessage || AutomaticProtectionAgent.IsProtectedByApa(mailItem.Message))
			{
				RmsClientManager.TracePass(obj, mailItem.SystemProbeId, "The message is not protectable because it's either already protected by APA or is a system/opaque message.", new object[0]);
				return false;
			}
			if (Utils.IsProtectedEmail(mailItem.Message))
			{
				RmsClientManager.TracePass(obj, mailItem.SystemProbeId, "The message is already protected", new object[0]);
				return false;
			}
			if (!Utils.IsSupportedMapiMessageClass(mailItem.Message))
			{
				RmsClientManager.TracePass(obj, mailItem.SystemProbeId, "The message is not protectable because the message class {0} is not supported.", new object[]
				{
					mailItem.Message.MapiMessageClass
				});
				return false;
			}
			return true;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00004200 File Offset: 0x00002400
		private static Guid NeedsProtection(MailItem mailItem, object obj)
		{
			EmailMessage message = mailItem.Message;
			if (!AutomaticProtectionAgent.IsMessageProtectable(mailItem, obj))
			{
				return Guid.Empty;
			}
			Header xheader = Utils.GetXHeader(message, "X-MS-Exchange-Organization-RightsProtectMessage");
			Guid guid;
			if (xheader != null && DrmClientUtils.TryParseGuid(xheader.Value, out guid))
			{
				RmsClientManager.TracePass(obj, mailItem.SystemProbeId, "Giving precedence to the message template {0} over InternetConfidential", new object[]
				{
					guid
				});
				return guid;
			}
			return Guid.Empty;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000426C File Offset: 0x0000246C
		private void OnSubmitEventHandler(SubmittedMessageEventSource source, QueuedMessageEventArgs args)
		{
			this.messageId = base.MailItem.Message.MessageId;
			if (!AutomaticProtectionAgent.IsMessageProtectable(base.MailItem, this))
			{
				this.TracePass("Message cannot be protected using APA. Not saving the recipient list.", new object[0]);
				return;
			}
			object obj;
			if (base.MailItem.Properties.TryGetValue("Microsoft.Exchange.RMSEncryptionAgent.RecipientListForPL", out obj))
			{
				this.TracePass("Message was already processed OnSubmitted, skipping: {0}", new object[]
				{
					this.messageId
				});
				return;
			}
			List<string> list = new List<string>();
			if (AutomaticProtectionAgent.IsFirstHop(base.MailItem))
			{
				if (base.MailItem.Recipients != null && base.MailItem.Recipients.Count != 0)
				{
					foreach (EnvelopeRecipient envelopeRecipient in base.MailItem.Recipients)
					{
						list.Add((string)envelopeRecipient.Address);
					}
					this.TracePass("Saving recipient list. Count = {0}", new object[]
					{
						list.Count
					});
					base.MailItem.Properties["Microsoft.Exchange.RMSEncryptionAgent.RecipientListForPL"] = list;
					return;
				}
			}
			else
			{
				this.TracePass("Not the first hop. Not saving recipient list", new object[0]);
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000043C4 File Offset: 0x000025C4
		private void OnRoutedEventHandler(RoutedMessageEventSource source, QueuedMessageEventArgs args)
		{
			this.breadcrumbs.Drop("OnRoutedEventHandler");
			this.messageId = base.MailItem.Message.MessageId;
			this.tenantId = base.MailItem.TenantId;
			this.orgId = Utils.OrgIdFromMailItem(base.MailItem);
			if (AutomaticProtectionAgent.IsTransportDecryptionApplied(base.MailItem))
			{
				this.TracePass("Message {0} was Transport Decrypted. It needs to be re-encrypted.", new object[]
				{
					this.messageId
				});
				string text;
				string text2;
				Utils.GetTransportDecryptionPLAndUL(base.MailItem, out text, out text2);
				if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text2))
				{
					this.TraceFail("Transport Decryption failed to re-encrypt the message {0} because either PL or UL is empty", new object[]
					{
						this.messageId
					});
					AutomaticProtectionAgentFactory.Logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_TransportReEncryptionFailedInvalidPLOrUL, null, new object[]
					{
						this.messageId,
						this.orgId
					});
					base.MailItem.DsnFormatRequested = DsnFormatRequested.Headers;
					Utils.NDRMessage(base.MailItem, this.messageId, Utils.GetResponseForNDR(new string[]
					{
						string.IsNullOrEmpty(text) ? "Microsoft Exchange Transport cannot RMS re-encrypt the message (ATTR1)." : "Microsoft Exchange Transport cannot RMS re-encrypt the message (ATTR2)."
					}));
					ApaAgentPerfCounters.TotalMessagesFailedToReencrypt.Increment();
					return;
				}
				this.orgId = RmsClientManagerUtils.OrgIdFromPublishingLicenseOrDefault(text, this.orgId);
				Uri licenseUri;
				AutomaticProtectionAgent.GetTransportDecryptionLicenseUri(base.MailItem, out licenseUri);
				this.isReEncryption = true;
				IReadOnlyMailItem mailItem = (IReadOnlyMailItem)((ITransportMailItemWrapperFacade)base.MailItem).TransportMailItem;
				this.rmsEncryptor = new RmsEncryptor(this.orgId, mailItem, text, text2, licenseUri, this.breadcrumbs);
				this.rmsEncryptor.BeginEncrypt(new AsyncCallback(this.OnEncryptionCompleted), new AutomaticProtectionAgent.ApaAgentAsyncState(base.GetAgentAsyncContext(), Guid.Empty, source));
				return;
			}
			else
			{
				Guid guid = AutomaticProtectionAgent.NeedsProtection(base.MailItem, this);
				if (guid == Guid.Empty)
				{
					this.TracePass("Message {0} doesn't need to be rights protected", new object[]
					{
						this.messageId
					});
					return;
				}
				this.TracePass("Message {0} needs to policy encrypted by template {1}", new object[]
				{
					this.messageId,
					guid.ToString()
				});
				try
				{
					if (!RmsClientManager.IRMConfig.IsInternalLicensingEnabledForTenant(this.orgId))
					{
						this.TracePass("NDRing message since APA Encryption agent is disabled for organization {0}", new object[]
						{
							this.orgId
						});
						AutomaticProtectionAgentFactory.Logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_ApplyPolicyOperationNDRedDueToEncryptionOff, base.MailItem.TenantId.ToString(), new object[]
						{
							this.messageId,
							this.orgId
						});
						Utils.NDRMessage(base.MailItem, this.messageId, Utils.GetResponseForNDR(new string[]
						{
							"Cannot RMS protect the message because Encryption is disabled in Microsoft Exchange Transport."
						}));
						ApaAgentPerfCounters.TotalMessagesFailed.Increment();
						this.agentFactory.UpdatePercentileCounters(false);
						return;
					}
					if (!this.IsTenantInfoInCache(this.orgId, guid) && !this.TryPromoteActiveAgent(source))
					{
						return;
					}
				}
				catch (ExchangeConfigurationException ex)
				{
					this.TraceFail("Hit a transient exception while reading configuration for org {0}. Deferring message. Error : {1}", new object[]
					{
						this.orgId,
						ex
					});
					if (this.IncrementDeferralCountAndCheckCap())
					{
						source.Defer(RmsClientManager.AppSettings.EncryptionTransientErrorDeferInterval, Utils.GetResponseForExceptionDeferral(ex, null));
						ApaAgentPerfCounters.TotalDeferrals.Increment();
					}
					else
					{
						this.LogApplyPolicyEvent(ex, true);
						Utils.NDRMessage(base.MailItem, this.messageId, AutomaticProtectionAgent.GetResponseForExceptionNDR(ex, null));
						ApaAgentPerfCounters.TotalMessagesFailed.Increment();
						this.agentFactory.UpdatePercentileCounters(false);
					}
					return;
				}
				catch (RightsManagementException ex2)
				{
					this.TraceFail("Hit a transient exception while reading configuration for org {0}. Deferring message. Error : {1}", new object[]
					{
						this.orgId,
						ex2
					});
					if (this.IncrementDeferralCountAndCheckCap())
					{
						source.Defer(RmsClientManager.AppSettings.EncryptionTransientErrorDeferInterval, Utils.GetResponseForExceptionDeferral(ex2, null));
						ApaAgentPerfCounters.TotalDeferrals.Increment();
					}
					else
					{
						this.LogApplyPolicyEvent(ex2, true);
						Utils.NDRMessage(base.MailItem, this.messageId, AutomaticProtectionAgent.GetResponseForExceptionNDR(ex2, null));
						ApaAgentPerfCounters.TotalMessagesFailed.Increment();
						this.agentFactory.UpdatePercentileCounters(false);
					}
					return;
				}
				this.TracePass("Create encryptor for OrgId {0}, MessageId {1}", new object[]
				{
					this.orgId,
					this.messageId
				});
				IReadOnlyMailItem mailItem2 = (IReadOnlyMailItem)((ITransportMailItemWrapperFacade)base.MailItem).TransportMailItem;
				try
				{
					this.rmsEncryptor = new RmsEncryptor(this.orgId, mailItem2, guid, this.breadcrumbs, null);
				}
				catch (ExchangeConfigurationException ex3)
				{
					this.TraceFail("Hit a transient exception while reading licensing location for org {0}. Deferring message. Error : {1}", new object[]
					{
						this.orgId,
						ex3
					});
					if (this.IncrementDeferralCountAndCheckCap())
					{
						source.Defer(RmsClientManager.AppSettings.EncryptionTransientErrorDeferInterval, Utils.GetResponseForExceptionDeferral(ex3, null));
						ApaAgentPerfCounters.TotalDeferrals.Increment();
					}
					else
					{
						this.LogApplyPolicyEvent(ex3, true);
						Utils.NDRMessage(base.MailItem, this.messageId, AutomaticProtectionAgent.GetResponseForExceptionNDR(ex3, null));
						ApaAgentPerfCounters.TotalMessagesFailed.Increment();
						this.agentFactory.UpdatePercentileCounters(false);
					}
					this.ReleaseActiveAgent();
					return;
				}
				catch (RightsManagementException ex4)
				{
					this.TraceFail("Hit a transient exception while reading licensing location for org {0}. Deferring message. Error : {1}", new object[]
					{
						this.orgId,
						ex4
					});
					if (this.IncrementDeferralCountAndCheckCap())
					{
						source.Defer(RmsClientManager.AppSettings.EncryptionTransientErrorDeferInterval, Utils.GetResponseForExceptionDeferral(ex4, null));
						ApaAgentPerfCounters.TotalDeferrals.Increment();
					}
					else
					{
						this.LogApplyPolicyEvent(ex4, true);
						Utils.NDRMessage(base.MailItem, this.messageId, AutomaticProtectionAgent.GetResponseForExceptionNDR(ex4, null));
						ApaAgentPerfCounters.TotalMessagesFailed.Increment();
						this.agentFactory.UpdatePercentileCounters(false);
					}
					this.ReleaseActiveAgent();
					return;
				}
				this.rmsEncryptor.BeginEncrypt(new AsyncCallback(this.OnEncryptionCompleted), new AutomaticProtectionAgent.ApaAgentAsyncState(base.GetAgentAsyncContext(), guid, source));
				return;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000049F0 File Offset: 0x00002BF0
		private void OnEncryptionCompleted(IAsyncResult asyncResult)
		{
			this.breadcrumbs.Drop("OnEncryptionCompleted");
			ArgumentValidator.ThrowIfNull("asyncResult", asyncResult);
			ArgumentValidator.ThrowIfNull("asyncResult.AsyncState", asyncResult.AsyncState);
			AutomaticProtectionAgent.ApaAgentAsyncState apaAgentAsyncState = (AutomaticProtectionAgent.ApaAgentAsyncState)asyncResult.AsyncState;
			apaAgentAsyncState.Resume();
			try
			{
				AsyncOperationResult<EmailMessage> asyncOperationResult = this.rmsEncryptor.EndEncrypt(asyncResult);
				if (!asyncOperationResult.IsSucceeded)
				{
					if (!this.isReEncryption)
					{
						this.TraceFail("Failed to rights protected message {0} with exception {1}", new object[]
						{
							this.messageId,
							asyncOperationResult.Exception
						});
						if (!this.HandleRightsManagementException(asyncOperationResult.Exception, apaAgentAsyncState) && !this.HandleExchangeConfigurationException(asyncOperationResult.Exception, apaAgentAsyncState) && !this.HandleMessageConversionException(asyncOperationResult.Exception, apaAgentAsyncState))
						{
							throw new InvalidOperationException("Unexpected exception from EndEncrypt {0}", asyncOperationResult.Exception);
						}
					}
					else
					{
						this.TraceFail("Failed to re-encrypt message {0} with exception {1}", new object[]
						{
							this.messageId,
							asyncOperationResult.Exception
						});
						AutomaticProtectionAgentFactory.Logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_TransportReEncryptionFailed, null, new object[]
						{
							this.messageId,
							this.orgId,
							asyncOperationResult.Exception
						});
						base.MailItem.DsnFormatRequested = DsnFormatRequested.Headers;
						Utils.NDRMessage(base.MailItem, this.messageId, Utils.GetResponseForNDR(new string[]
						{
							"Microsoft Exchange Transport cannot RMS re-encrypt the message (ATTR3)."
						}));
						ApaAgentPerfCounters.TotalMessagesFailedToReencrypt.Increment();
					}
				}
				else
				{
					EmailMessage data = asyncOperationResult.Data;
					using (data)
					{
						using (Stream mimeWriteStream = base.MailItem.GetMimeWriteStream())
						{
							data.MimeDocument.RootPart.WriteTo(mimeWriteStream);
						}
					}
					Utils.StampXHeader(base.MailItem.Message, "X-MS-Exchange-Forest-TransportDecryption-Action", "Scanned");
					if (!this.isReEncryption)
					{
						Utils.StampXHeader(base.MailItem.Message, "X-MS-Exchange-Organization-MessageProtectedByApa", "true");
						this.TracePass("Successfully policy rights protected message {0} with template {1}", new object[]
						{
							this.messageId,
							apaAgentAsyncState.TemplateId
						});
						ApaAgentPerfCounters.TotalMessagesEncrypted.Increment();
						this.agentFactory.UpdatePercentileCounters(true);
					}
					else
					{
						Utils.SetTransportDecryptionPLAndUL(base.MailItem, null, this.rmsEncryptor.UseLicense);
						Utils.SetTransportDecryptionApplied(base.MailItem, true);
						ApaAgentPerfCounters.TotalMessagesReencrypted.Increment();
						this.TracePass("Successfully RMS re-encrypted message {0}", new object[]
						{
							this.messageId
						});
					}
					LamHelper.PublishSuccessfulIrmEncryptionToLAM(base.MailItem);
				}
			}
			finally
			{
				apaAgentAsyncState.Complete();
				this.rmsEncryptor.Dispose();
				this.ReleaseActiveAgent();
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00004CF4 File Offset: 0x00002EF4
		private bool HandleMessageConversionException(Exception exception, AutomaticProtectionAgent.ApaAgentAsyncState state)
		{
			MessageConversionException ex = exception as MessageConversionException;
			if (ex == null)
			{
				return false;
			}
			if (ex.IsTransient && this.IncrementDeferralCountAndCheckCap())
			{
				this.LogApplyPolicyEvent(ex, false);
				state.EventSource.Defer(RmsClientManager.AppSettings.EncryptionTransientErrorDeferInterval, Utils.GetResponseForExceptionDeferral(ex, null));
				ApaAgentPerfCounters.TotalDeferrals.Increment();
			}
			else
			{
				this.LogApplyPolicyEvent(ex, true);
				Utils.NDRMessage(base.MailItem, this.messageId, AutomaticProtectionAgent.GetResponseForExceptionNDR(ex, null));
				ApaAgentPerfCounters.TotalMessagesFailed.Increment();
				this.agentFactory.UpdatePercentileCounters(false);
			}
			return true;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00004D88 File Offset: 0x00002F88
		private bool HandleExchangeConfigurationException(Exception exception, AutomaticProtectionAgent.ApaAgentAsyncState state)
		{
			ExchangeConfigurationException ex = exception as ExchangeConfigurationException;
			if (ex == null)
			{
				return false;
			}
			if (this.IncrementDeferralCountAndCheckCap())
			{
				this.LogApplyPolicyEvent(ex, false);
				state.EventSource.Defer(RmsClientManager.AppSettings.EncryptionTransientErrorDeferInterval, Utils.GetResponseForExceptionDeferral(ex, null));
				ApaAgentPerfCounters.TotalDeferrals.Increment();
			}
			else
			{
				this.LogApplyPolicyEvent(ex, true);
				Utils.NDRMessage(base.MailItem, this.messageId, AutomaticProtectionAgent.GetResponseForExceptionNDR(ex, null));
				ApaAgentPerfCounters.TotalMessagesFailed.Increment();
				this.agentFactory.UpdatePercentileCounters(false);
			}
			return true;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00004E14 File Offset: 0x00003014
		private bool HandleRightsManagementException(Exception exception, AutomaticProtectionAgent.ApaAgentAsyncState state)
		{
			RightsManagementException ex = exception as RightsManagementException;
			if (ex == null)
			{
				return false;
			}
			string[] additionalInfo = null;
			RightsManagementFailureCode failureCode = ex.FailureCode;
			if (failureCode == RightsManagementFailureCode.TemplateAcquisitionFailed || failureCode == RightsManagementFailureCode.TemplateDoesNotExist)
			{
				additionalInfo = new string[]
				{
					string.Format(CultureInfo.InvariantCulture, "A failure occurred when trying to look up Rights Management Server template '{0}'.", new object[]
					{
						state.TemplateId
					})
				};
			}
			if (this.orgId != OrganizationId.ForestWideOrgId && ex.FailureCode == RightsManagementFailureCode.TemplateDoesNotExist)
			{
				AutomaticProtectionAgentFactory.Logger.LogEvent(this.orgId, MessagingPoliciesEventLogConstants.Tuple_TemplateDoesNotExist, state.TemplateId.ToString(), state.TemplateId);
				EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "TemplateDoesNotExist", null, string.Format("Microsoft Exchange couldn't IRM-protect the message because the RMS template {0} used by the Transport Protection Rule can't be found.", state.TemplateId), ResultSeverityLevel.Error, false);
			}
			if (ex.IsPermanent || !this.IncrementDeferralCountAndCheckCap())
			{
				RmsEventLogHandler.LogException(AutomaticProtectionAgentFactory.Logger, base.MailItem, RmsComponent.EncryptionAgent, ex, true);
				Utils.NDRMessage(base.MailItem, this.messageId, AutomaticProtectionAgent.GetResponseForExceptionNDR(ex, additionalInfo));
				ApaAgentPerfCounters.TotalMessagesFailed.Increment();
				this.agentFactory.UpdatePercentileCounters(false);
			}
			else
			{
				RmsEventLogHandler.LogException(AutomaticProtectionAgentFactory.Logger, base.MailItem, RmsComponent.EncryptionAgent, ex, false);
				state.EventSource.Defer(RmsClientManager.AppSettings.EncryptionTransientErrorDeferInterval, Utils.GetResponseForExceptionDeferral(ex, additionalInfo));
				ApaAgentPerfCounters.TotalDeferrals.Increment();
			}
			return true;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00004F8C File Offset: 0x0000318C
		private bool TryPromoteActiveAgent(RoutedMessageEventSource source)
		{
			this.TracePass("Message: {0}. Encryption Agent is trying to promote itself as Active Agent ...", new object[]
			{
				this.messageId
			});
			this.active = this.agentFactory.InstanceController.TryMakeActive(this.tenantId);
			if (!this.active)
			{
				this.TracePass("Message: {0}. Unable to promote as Active Encryption agent. Defer message.", new object[]
				{
					this.messageId
				});
				source.Defer(RmsClientManager.AppSettings.ActiveAgentCapDeferInterval, Utils.GetResponseForDeferral(new string[]
				{
					"Already processing maximum number of messages."
				}));
				ApaAgentPerfCounters.TotalDeferrals.Increment();
			}
			else
			{
				this.TracePass("Message: {0}. Encryption Agent is promoted as Active Agent.", new object[]
				{
					this.messageId
				});
			}
			return this.active;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000504C File Offset: 0x0000324C
		private void ReleaseActiveAgent()
		{
			if (this.active)
			{
				this.TracePass("Message: {0}. Encryption Agent is demoted.", new object[]
				{
					this.messageId
				});
				this.agentFactory.InstanceController.MakeInactive(this.tenantId);
				this.active = false;
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000509C File Offset: 0x0000329C
		private bool IncrementDeferralCountAndCheckCap()
		{
			int num = Utils.IncrementDeferralCount(base.MailItem, "Microsoft.Exchange.RmsEncryptionAgent.DeferralCount");
			if (num == -1)
			{
				this.TraceFail("Message: {0}. Deferral count is broken.", new object[]
				{
					this.messageId
				});
				return false;
			}
			if (num > 1)
			{
				this.TracePass("Message: {0}. Deferred {1} time(s).", new object[]
				{
					this.messageId,
					num - 1
				});
			}
			if (num > 3)
			{
				this.TracePass("Message: {0}. Will be NDR.", new object[]
				{
					this.messageId
				});
				return false;
			}
			this.TracePass("Message: {0}. Will be deferred.", new object[]
			{
				this.messageId
			});
			return true;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000514C File Offset: 0x0000334C
		private void LogApplyPolicyEvent(Exception exception, bool ndr)
		{
			AutomaticProtectionAgentFactory.Logger.LogEvent(ndr ? MessagingPoliciesEventLogConstants.Tuple_ApplyPolicyOperationFailNDR : MessagingPoliciesEventLogConstants.Tuple_ApplyPolicyOperationFailDefer, null, new object[]
			{
				this.messageId,
				exception
			});
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000518C File Offset: 0x0000338C
		private bool IsTenantInfoInCache(OrganizationId organizationId, Guid templateId)
		{
			ArgumentValidator.ThrowIfNull("organizationId", organizationId);
			this.TracePass("Checking if RAC/CLC and template are in cache for organization ({0})", new object[]
			{
				organizationId
			});
			bool flag = RmsClientManager.IsTenantInfoInCache(organizationId);
			if (flag && !RmsClientManager.UseOfflineRms)
			{
				Cache<Guid, RmsTemplate> templateCacheForFirstOrg = RmsClientManager.TemplateCacheForFirstOrg;
				RmsTemplate rmsTemplate;
				flag &= (templateCacheForFirstOrg != null && templateCacheForFirstOrg.TryGetValue(templateId, out rmsTemplate));
			}
			this.TracePass("RAC/CLC and template are in cache for organization ({0})? - {1}.", new object[]
			{
				organizationId,
				flag
			});
			return flag;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00005209 File Offset: 0x00003409
		private void TracePass(string formatString, params object[] args)
		{
			if (base.MailItem != null)
			{
				RmsClientManager.TracePass(this, base.MailItem.SystemProbeId, formatString, args);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00005226 File Offset: 0x00003426
		private void TraceFail(string formatString, params object[] args)
		{
			if (base.MailItem != null)
			{
				RmsClientManager.TraceFail(this, base.MailItem.SystemProbeId, formatString, args);
			}
		}

		// Token: 0x0400007C RID: 124
		private const string DeferralCountProperty = "Microsoft.Exchange.RmsEncryptionAgent.DeferralCount";

		// Token: 0x0400007D RID: 125
		private const int MaxDeferrals = 3;

		// Token: 0x0400007E RID: 126
		private readonly Breadcrumbs<string> breadcrumbs = new Breadcrumbs<string>(8);

		// Token: 0x0400007F RID: 127
		private readonly AutomaticProtectionAgentFactory agentFactory;

		// Token: 0x04000080 RID: 128
		private string messageId;

		// Token: 0x04000081 RID: 129
		private RmsEncryptor rmsEncryptor;

		// Token: 0x04000082 RID: 130
		private bool isReEncryption;

		// Token: 0x04000083 RID: 131
		private OrganizationId orgId;

		// Token: 0x04000084 RID: 132
		private Guid tenantId;

		// Token: 0x04000085 RID: 133
		private bool active;

		// Token: 0x02000011 RID: 17
		private sealed class ApaAgentAsyncState : AgentAsyncState
		{
			// Token: 0x0600004F RID: 79 RVA: 0x00005243 File Offset: 0x00003443
			public ApaAgentAsyncState(AgentAsyncContext asyncContext, Guid templateId, RoutedMessageEventSource eventSource) : base(asyncContext)
			{
				ArgumentValidator.ThrowIfNull("asyncContext", asyncContext);
				ArgumentValidator.ThrowIfNull("eventSource", eventSource);
				this.TemplateId = templateId;
				this.EventSource = eventSource;
			}

			// Token: 0x04000086 RID: 134
			public readonly Guid TemplateId;

			// Token: 0x04000087 RID: 135
			public readonly RoutedMessageEventSource EventSource;
		}
	}
}
