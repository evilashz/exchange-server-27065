using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Principal;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Storage.Authentication;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.Exchange.Transport.RightsManagement;

namespace Microsoft.Exchange.MessagingPolicies.RmSvcAgent
{
	// Token: 0x02000013 RID: 19
	internal sealed class PrelicenseAgent : RoutingAgent
	{
		// Token: 0x06000055 RID: 85 RVA: 0x0000536C File Offset: 0x0000356C
		public PrelicenseAgent(PrelicenseAgentFactory factory, SmtpServer server)
		{
			this.agentFactory = factory;
			this.addressbook = server.AddressBook;
			base.OnRoutedMessage += this.PrelicenseOnRoutedHandler;
			this.breadcrumbs.Drop(PrelicenseAgent.AgentState.Initialized);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000053BC File Offset: 0x000035BC
		private void PrelicenseOnRoutedHandler(RoutedMessageEventSource eventSource, QueuedMessageEventArgs args)
		{
			this.breadcrumbs.Drop(PrelicenseAgent.AgentState.Started);
			this.mailId = base.MailItem.Message.MessageId;
			this.source = eventSource;
			this.tenantId = base.MailItem.TenantId;
			OrganizationId organizationId = Utils.OrgIdFromMailItem(base.MailItem);
			if (!this.IsPrelicensableMailItem())
			{
				return;
			}
			string publishLicense;
			if (!this.TryGetPublishLicense(out publishLicense))
			{
				this.TraceFail("Failed to extract the issuance license from the protected stream", new object[0]);
				return;
			}
			XmlNode[] publishLicenseAsXmlNodeArray = null;
			Uri uri = null;
			bool flag = false;
			Exception ex = null;
			try
			{
				RmsClientManager.GetLicensingUri(organizationId, publishLicense, out uri, out publishLicenseAsXmlNodeArray, out flag);
			}
			catch (ExchangeConfigurationException ex2)
			{
				ex = ex2;
			}
			catch (RightsManagementException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				this.TraceFail("Message: {0}. Hit a transient exception while reading configuration for org {1}. Error : {2}", new object[]
				{
					this.mailId,
					organizationId,
					ex
				});
				if (this.IncrementDeferralCountAndCheckCap())
				{
					this.TracePass("Message: {0}.  Being deferred.", new object[]
					{
						this.mailId
					});
					string[] additionalInfo = new string[]
					{
						string.Format(CultureInfo.InvariantCulture, "A transient error occurred during prelicensing when communicating with RMS server {0}.", new object[]
						{
							(uri != null) ? uri.OriginalString : string.Empty
						})
					};
					this.source.Defer(RmsClientManager.AppSettings.TransientErrorDeferInterval, Utils.GetResponseForExceptionDeferral(ex, additionalInfo));
					PrelicenseAgentPerfCounters.TotalDeferralsToPreLicense.Increment();
					return;
				}
				this.TraceFail("Message: {0}.  Deferral threshold has been reached.  Letting message through without pre-license.", new object[]
				{
					this.mailId
				});
				PrelicenseAgentPerfCounters.TotalMessagesFailedToPreLicense.Increment();
				this.agentFactory.UpdatePercentileCounters(false);
				return;
			}
			else if (flag)
			{
				if (!this.IsInternalEndUserPreLicensingEnabled(organizationId) && !this.IsInternalServerPreLicensingEnabled(organizationId))
				{
					this.TracePass("Message: {0}. Skipping processing of message because Prelicensing and Server Licensing are disabled for organization {1}.", new object[]
					{
						this.mailId,
						organizationId
					});
					return;
				}
				if (!this.TryPromoteActiveAgent(true))
				{
					return;
				}
				this.TracePass("Message: {0}. Acquiring pre license / server license from an internal RMS server {1} for organization {2}", new object[]
				{
					this.mailId,
					uri,
					organizationId
				});
				this.AcquireServerUseLicense(organizationId, uri, publishLicense, publishLicenseAsXmlNodeArray);
				return;
			}
			else
			{
				if (!this.IsExternalServerPreLicensingEnabled(organizationId))
				{
					this.TracePass("Message: {0}. Skipping processing of message because the message is protected against an external RMS server {1} and  ExternalLicensing is disabled for organization {2}.", new object[]
					{
						this.mailId,
						uri,
						organizationId
					});
					return;
				}
				if (!this.TryPromoteActiveAgent(false))
				{
					return;
				}
				this.TracePass("Message: {0}. Acquiring pre license for recipients from an external RMS server {1} for organization {2}", new object[]
				{
					this.mailId,
					uri,
					organizationId
				});
				this.AcquirePrelicenseFromExternalRms(organizationId, uri, publishLicense, publishLicenseAsXmlNodeArray);
				return;
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00005664 File Offset: 0x00003864
		private bool IsPrelicensableMailItem()
		{
			this.TracePass("Message: {0}. PL Agent will verify whether this mail item should be processed.", new object[]
			{
				this.mailId
			});
			if (!Utils.IsProtectedEmail(base.MailItem.Message))
			{
				this.TracePass("Message: {0}. This mail item is not RMS protected. PL Agent will not process this mail item.", new object[]
				{
					this.mailId
				});
				return false;
			}
			if (base.MailItem.Recipients.Count == 0)
			{
				this.TracePass("Message: {0}. This mail item has zero recipient. PL Agent will not process this mail item.", new object[]
				{
					this.mailId
				});
				return false;
			}
			bool flag = this.UpdateRecipientList();
			this.TracePass(flag ? "Message: {0}. PL Agent will process this mail item." : "Message: {0}. PL Agent will not process this mail item.", new object[]
			{
				this.mailId
			});
			return flag;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00005720 File Offset: 0x00003920
		private bool UpdateRecipientList()
		{
			EnvelopeRecipientCollection recipients = base.MailItem.Recipients;
			this.TracePass("Message: {0}. PL Agent will check all ({1}) recipients of this mail item.", new object[]
			{
				this.mailId,
				recipients.Count
			});
			List<EnvelopeRecipient> list = new List<EnvelopeRecipient>();
			this.recipientsToProcess = new List<EnvelopeRecipient>();
			this.recipientsWithTransientFailure = new List<EnvelopeRecipient>();
			foreach (EnvelopeRecipient envelopeRecipient in recipients)
			{
				bool flag;
				if (this.IsPrelicensableRecipient(envelopeRecipient, out flag))
				{
					if (flag)
					{
						this.recipientsWithTransientFailure.Add(envelopeRecipient);
					}
					else
					{
						this.recipientsToProcess.Add(envelopeRecipient);
					}
				}
				else
				{
					list.Add(envelopeRecipient);
				}
			}
			if (list.Count > 0)
			{
				if (recipients.Count > list.Count)
				{
					this.TracePass("Message: {0}. PL Agent will fork this mail item as {1} out of {2} recipients are non-prelicensable.", new object[]
					{
						this.mailId,
						list.Count,
						recipients.Count
					});
					this.source.Fork(list);
				}
				else
				{
					this.TracePass("Message: {0}. PL Agent will not process this mail item as all ({1}) recipients are non-prelicensable.", new object[]
					{
						this.mailId,
						list.Count
					});
				}
				return false;
			}
			if (this.recipientsToProcess.Count == recipients.Count)
			{
				this.TracePass("Message: {0}. PL Agent will process all {1} recipients of this mail item.", new object[]
				{
					this.mailId,
					recipients.Count
				});
				return true;
			}
			if (this.recipientsWithTransientFailure.Count == recipients.Count)
			{
				this.TracePass("Message: {0}. PL Agent will defer this mail item as all {1} recipients are with transient failure.", new object[]
				{
					this.mailId,
					recipients.Count
				});
				if (this.IncrementDeferralCountAndCheckCap())
				{
					this.TracePass("Message: {0}.  Being deferred.", new object[]
					{
						this.mailId
					});
					this.source.Defer(RmsClientManager.AppSettings.TransientErrorDeferInterval, Utils.GetResponseForDeferral(new string[]
					{
						"A transient error occurred when reading user configuration from AD."
					}));
					PrelicenseAgentPerfCounters.TotalDeferralsToPreLicense.Increment();
				}
				else
				{
					this.TraceFail("Message: {0}.  Deferral threshold has been reached.  Letting message through without pre-license.", new object[]
					{
						this.mailId
					});
					PrelicenseAgentPerfCounters.TotalMessagesFailedToPreLicense.Increment();
					this.agentFactory.UpdatePercentileCounters(false);
				}
				return false;
			}
			this.TracePass("Message: {0}. PL Agent will process {1} out of {2} recipients of this mail item. Rest ({3}) of recipients are with transient failure.", new object[]
			{
				this.mailId,
				this.recipientsToProcess.Count,
				recipients.Count,
				this.recipientsWithTransientFailure.Count
			});
			return true;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000059FC File Offset: 0x00003BFC
		private bool IsPrelicensableRecipient(EnvelopeRecipient recipient, out bool transientFailure)
		{
			transientFailure = false;
			this.TracePass("Message: {0}. PL Agent will verify whether recipient ({1}) should be processed.", new object[]
			{
				this.mailId,
				recipient.Address.ToString()
			});
			if (recipient.OutboundDeliveryMethod != DeliveryMethod.Mailbox)
			{
				this.TracePass("Message: {0}. This recipient is not last-hop recipient. PL Agent will not process this recipient.", new object[]
				{
					this.mailId
				});
				return false;
			}
			if (recipient.Properties.ContainsKey("Microsoft.Exchange.RightsManagement.DRMLicense"))
			{
				this.TracePass("Message: {0}. This recipient already has use-license. PL Agent will not process this recipient.", new object[]
				{
					this.mailId
				});
				return false;
			}
			if (recipient.Properties.ContainsKey("Microsoft.Exchange.RightsManagement.DRMFailure"))
			{
				this.TracePass("Message: {0}. This recipient has permanent error. PL Agent will not process this recipient.", new object[]
				{
					this.mailId
				});
				return false;
			}
			try
			{
				AddressBookEntry addressBookEntry = this.addressbook.Find(recipient.Address);
				if (addressBookEntry != null && addressBookEntry.RecipientType != RecipientType.User)
				{
					this.TracePass("Message: {0}. This recipient in not a User type. PL Agent will not process this recipient.", new object[]
					{
						this.mailId
					});
					return false;
				}
				if (addressBookEntry != null && addressBookEntry.UserAccountSid != null)
				{
					recipient.Properties["Microsoft.Exchange.PrelicenseAgent.RecipientSid"] = addressBookEntry.UserAccountSid.Value;
				}
			}
			catch (ADTransientException)
			{
				this.TracePass("Message: {0}. This recipient has transient failure.", new object[]
				{
					this.mailId
				});
				transientFailure = true;
			}
			return true;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00005B78 File Offset: 0x00003D78
		private bool IsInternalEndUserPreLicensingEnabled(OrganizationId orgId)
		{
			return this.IsConfigEnabledForTenant(orgId, PrelicenseAgent.ConfigurationType.InternalEndUserPreLicensing);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00005B82 File Offset: 0x00003D82
		private bool IsInternalServerPreLicensingEnabled(OrganizationId orgId)
		{
			return this.IsConfigEnabledForTenant(orgId, PrelicenseAgent.ConfigurationType.InternalServerPreLicensing);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00005B8C File Offset: 0x00003D8C
		private bool IsExternalServerPreLicensingEnabled(OrganizationId orgId)
		{
			if (!ExternalAuthentication.GetCurrent().Enabled)
			{
				this.TraceFail("Message: {0}. The organization is not federated. Not querying the external RMS server", new object[]
				{
					this.mailId
				});
				return false;
			}
			return this.IsConfigEnabledForTenant(orgId, PrelicenseAgent.ConfigurationType.ExternalServerPreLicensing);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00005BCC File Offset: 0x00003DCC
		private bool IsConfigEnabledForTenant(OrganizationId orgId, PrelicenseAgent.ConfigurationType configurationType)
		{
			Exception ex2;
			try
			{
				switch (configurationType)
				{
				case PrelicenseAgent.ConfigurationType.InternalEndUserPreLicensing:
					return RmsClientManager.IRMConfig.IsInternalLicensingEnabledForTenant(orgId);
				case PrelicenseAgent.ConfigurationType.InternalServerPreLicensing:
					return RmsClientManager.IRMConfig.IsInternalServerPreLicensingEnabledForTenant(orgId);
				case PrelicenseAgent.ConfigurationType.ExternalServerPreLicensing:
					return RmsClientManager.IRMConfig.IsExternalServerPreLicensingEnabledForTenant(orgId);
				default:
					throw new ArgumentException("configurationType");
				}
			}
			catch (ExchangeConfigurationException ex)
			{
				ex2 = ex;
			}
			catch (RightsManagementException ex3)
			{
				ex2 = ex3;
			}
			this.TraceFail("Message: {0}. Hit a transient exception while reading configuration for org {1}. Error : {2}", new object[]
			{
				this.mailId,
				orgId,
				ex2
			});
			if (this.IncrementDeferralCountAndCheckCap())
			{
				this.TracePass("Message: {0}.  Being deferred.", new object[]
				{
					this.mailId
				});
				string[] additionalInfo = new string[]
				{
					string.Format(CultureInfo.InvariantCulture, "A transient error occurred when reading configuration for {0} from AD.", new object[]
					{
						Utils.GetTenantString(base.MailItem.TenantId)
					})
				};
				this.source.Defer(RmsClientManager.AppSettings.TransientErrorDeferInterval, Utils.GetResponseForExceptionDeferral(ex2, additionalInfo));
				if (configurationType == PrelicenseAgent.ConfigurationType.InternalEndUserPreLicensing)
				{
					PrelicenseAgentPerfCounters.TotalDeferralsToPreLicense.Increment();
				}
				else if (PrelicenseAgent.ConfigurationType.ExternalServerPreLicensing == configurationType)
				{
					PrelicenseAgentPerfCounters.TotalDeferralsToPreLicenseForExternalMessages.Increment();
				}
				else
				{
					PrelicenseAgentPerfCounters.TotalDeferralsToLicense.Increment();
				}
			}
			else
			{
				this.TraceFail("Message: {0}.  Deferral threshold has been reached.  Letting message through without pre-license.", new object[]
				{
					this.mailId
				});
				if (configurationType == PrelicenseAgent.ConfigurationType.InternalEndUserPreLicensing)
				{
					PrelicenseAgentPerfCounters.TotalMessagesFailedToPreLicense.Increment();
					this.agentFactory.UpdatePercentileCounters(false);
				}
				else if (PrelicenseAgent.ConfigurationType.ExternalServerPreLicensing == configurationType)
				{
					PrelicenseAgentPerfCounters.TotalExternalMessagesFailedToPreLicense.Increment();
					this.agentFactory.UpdatePercentileCounters(false);
				}
				else
				{
					PrelicenseAgentPerfCounters.TotalMessagesFailedToLicense.Increment();
					this.agentFactory.UpdatePercentileCounters(false);
				}
			}
			return false;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00005DA8 File Offset: 0x00003FA8
		private bool TryPromoteActiveAgent(bool isInternalUri)
		{
			this.TracePass("Message: {0}. PL Agent is trying to promote itself as Active Agent ...", new object[]
			{
				this.mailId
			});
			this.active = this.agentFactory.InstanceController.TryMakeActive(this.tenantId);
			if (!this.active)
			{
				if (this.IncrementDeferralCountAndCheckCap())
				{
					this.TracePass("Message: {0}. Unable to promote as Active PL agent - will defer message.", new object[]
					{
						this.mailId
					});
					this.source.Defer(RmsClientManager.AppSettings.ActiveAgentCapDeferInterval, Utils.GetResponseForDeferral(new string[]
					{
						"Already processing maximum number of messages."
					}));
					if (isInternalUri)
					{
						PrelicenseAgentPerfCounters.TotalDeferralsToPreLicense.Increment();
					}
					else
					{
						PrelicenseAgentPerfCounters.TotalDeferralsToPreLicenseForExternalMessages.Increment();
					}
				}
				else
				{
					this.TraceFail("Message: {0}.  Deferral threshold has been reached.  Letting message through without pre-license.", new object[]
					{
						this.mailId
					});
					if (isInternalUri)
					{
						PrelicenseAgentPerfCounters.TotalMessagesFailedToPreLicense.Increment();
						this.agentFactory.UpdatePercentileCounters(false);
					}
					else
					{
						PrelicenseAgentPerfCounters.TotalExternalMessagesFailedToPreLicense.Increment();
						this.agentFactory.UpdatePercentileCounters(false);
					}
				}
			}
			else
			{
				this.TracePass("Message: {0}. PL Agent is promoted as Active Agent.", new object[]
				{
					this.mailId
				});
			}
			return this.active;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00005EDC File Offset: 0x000040DC
		private void AcquirePreLicense(PrelicenseAgent.PreLicenseAgentAsyncState agentAsyncContext)
		{
			ArgumentValidator.ThrowIfNull("agentAsyncContext", agentAsyncContext);
			if (!this.IsInternalEndUserPreLicensingEnabled(agentAsyncContext.OrganizationId))
			{
				this.CleanUp(agentAsyncContext);
				return;
			}
			this.TracePass("Message: {0}. PL Agent will get list of identities.", new object[]
			{
				this.mailId
			});
			int count = this.recipientsToProcess.Count;
			string[] array = new string[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = this.recipientsToProcess[i].Address.ToString();
			}
			this.TracePass("Message: {0}. PL Agent will invokes Async web method.", new object[]
			{
				this.mailId
			});
			this.breadcrumbs.Drop(PrelicenseAgent.AgentState.BeginAcquirePreLicense);
			RmsClientManager.SaveContextCallback = new RmsClientManager.SaveContextOnAsyncQueryCallback(this.SaveContext);
			RmsClientManager.BeginAcquirePreLicense(agentAsyncContext.Context, agentAsyncContext.LicensingUri, true, agentAsyncContext.PublishLicenseAsXmlNodeArray, array, new AsyncCallback(this.AcquirePreLicenseCallback), agentAsyncContext);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00005FCC File Offset: 0x000041CC
		private void AcquirePreLicenseCallback(IAsyncResult asyncResult)
		{
			this.breadcrumbs.Drop(PrelicenseAgent.AgentState.AcquirePreLicenseCallback);
			this.TracePass("Message: {0}. PL Agent will invoke End Async web method.", new object[]
			{
				this.mailId
			});
			ArgumentValidator.ThrowIfNull("asyncResult", asyncResult);
			ArgumentValidator.ThrowIfNull("asyncResult.AsyncState", asyncResult.AsyncState);
			PrelicenseAgent.PreLicenseAgentAsyncState preLicenseAgentAsyncState = asyncResult.AsyncState as PrelicenseAgent.PreLicenseAgentAsyncState;
			if (preLicenseAgentAsyncState == null)
			{
				throw new InvalidOperationException("asyncResult.AsyncState not of type PreLicenseAgentAsyncState");
			}
			this.SaveContext(preLicenseAgentAsyncState);
			try
			{
				LicenseResponse[] array = null;
				Exception ex = null;
				try
				{
					array = RmsClientManager.EndAcquirePreLicense(asyncResult);
					object obj;
					if (preLicenseAgentAsyncState.OrganizationId == OrganizationId.ForestWideOrgId && preLicenseAgentAsyncState.IsInternalRmsUri && base.MailItem.Properties.TryGetValue("Microsoft.Exchange.RightsManagement.TransportDecryptionUL", out obj) && obj != null)
					{
						this.CreateSignatureBuilder((string)obj, preLicenseAgentAsyncState.PublishLicense, preLicenseAgentAsyncState.Context, preLicenseAgentAsyncState.LicensingUri);
					}
				}
				catch (RightsManagementException ex2)
				{
					this.TraceFail("Message: {0}. Exception: '{1}'.", new object[]
					{
						this.mailId,
						ex2
					});
					RmsEventLogHandler.LogException(PrelicenseAgentFactory.Logger, base.MailItem, RmsComponent.PrelicensingAgent, ex2, ex2.IsPermanent);
					ex = ex2;
				}
				catch (ExchangeConfigurationException ex3)
				{
					this.TraceFail("Message: {0}. Failed to read configuration. Exception: {1}.", new object[]
					{
						this.mailId,
						ex3
					});
					ex = ex3;
				}
				int count = this.recipientsToProcess.Count;
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				bool flag = true;
				RightsManagementFailureCode rightsManagementFailureCode = RightsManagementFailureCode.Success;
				string text = null;
				if (ex != null)
				{
					if (ex is RightsManagementException)
					{
						RightsManagementException ex4 = (RightsManagementException)ex;
						if (!ex4.IsPermanent)
						{
							this.recipientsWithTransientFailure.AddRange(this.recipientsToProcess);
							num3 = count;
						}
						else
						{
							foreach (EnvelopeRecipient envelopeRecipient in this.recipientsToProcess)
							{
								envelopeRecipient.Properties["Microsoft.Exchange.RightsManagement.DRMFailure"] = (int)ex4.FailureCode;
							}
							num2 = count;
						}
					}
					else
					{
						this.recipientsWithTransientFailure.AddRange(this.recipientsToProcess);
						num3 = count;
					}
				}
				else
				{
					for (int i = 0; i < count; i++)
					{
						this.TracePass("Message: {0}. PL Agent got {1} status code for recipient {2}.", new object[]
						{
							this.mailId,
							(array[i].Exception == null) ? RightsManagementFailureCode.Success : array[i].Exception.FailureCode,
							this.recipientsToProcess[i].Address
						});
						if (array[i].Exception == null)
						{
							if (preLicenseAgentAsyncState.IsInternalRmsUri)
							{
								if (preLicenseAgentAsyncState.OrganizationId != OrganizationId.ForestWideOrgId)
								{
									if (!DrmClientUtils.IsCachingOfLicenseDisabled(array[i].License))
									{
										this.recipientsToProcess[i].Properties["Microsoft.Exchange.RightsManagement.B2BDRMLicense"] = array[i].License;
									}
									if (array[i].UsageRights != null)
									{
										this.recipientsToProcess[i].Properties["Microsoft.Exchange.RightsManagement.DRMRights"] = (int)array[i].UsageRights.Value;
										this.recipientsToProcess[i].Properties["Microsoft.Exchange.RightsManagement.DRMExpiryTime"] = DateTime.MaxValue;
										this.recipientsToProcess[i].Properties["Microsoft.Exchange.RightsManagement.DRMPropsSignature"] = PrelicenseAgent.EmptyByteArray;
									}
									else
									{
										this.TraceFail("Message: {0}. OfflineRMS: Failed to get the rights for user {1} from the use license {2}.", new object[]
										{
											this.mailId,
											this.recipientsToProcess[i],
											array[i].License
										});
									}
								}
								else
								{
									if (!DrmClientUtils.IsCachingOfLicenseDisabled(array[i].License))
									{
										this.recipientsToProcess[i].Properties["Microsoft.Exchange.RightsManagement.DRMLicense"] = DrmEmailCompression.CompressString(array[i].License);
									}
									this.AddSignature(array[i], this.recipientsToProcess[i]);
								}
							}
							else
							{
								if (!DrmClientUtils.IsCachingOfLicenseDisabled(array[i].License))
								{
									this.recipientsToProcess[i].Properties["Microsoft.Exchange.RightsManagement.B2BDRMLicense"] = array[i].License;
								}
								if (array[i].UsageRights != null)
								{
									this.recipientsToProcess[i].Properties["Microsoft.Exchange.RightsManagement.DRMRights"] = (int)array[i].UsageRights.Value;
									this.recipientsToProcess[i].Properties["Microsoft.Exchange.RightsManagement.DRMExpiryTime"] = DateTime.MaxValue;
									this.recipientsToProcess[i].Properties["Microsoft.Exchange.RightsManagement.DRMPropsSignature"] = PrelicenseAgent.EmptyByteArray;
								}
								else
								{
									this.TraceFail("Message: {0}. B2B: Failed to get the rights from the use license {1}.", new object[]
									{
										this.mailId,
										array[i].License
									});
								}
							}
							num++;
						}
						else if (array[i].Exception.IsPermanent)
						{
							this.recipientsToProcess[i].Properties["Microsoft.Exchange.RightsManagement.DRMFailure"] = (int)array[i].Exception.FailureCode;
							num2++;
						}
						else
						{
							this.recipientsWithTransientFailure.Add(this.recipientsToProcess[i]);
							if (rightsManagementFailureCode == RightsManagementFailureCode.Success)
							{
								rightsManagementFailureCode = array[i].Exception.FailureCode;
								text = (string)this.recipientsToProcess[i].Address;
							}
							else if (flag && array[i].Exception.FailureCode != rightsManagementFailureCode)
							{
								flag = false;
							}
							num3++;
						}
					}
				}
				this.TracePass("Message: {0}. PL Agent completed processing with {1} success, {2} permanent failure and {3} transient failure.", new object[]
				{
					this.mailId,
					num,
					num2,
					num3
				});
				int count2 = base.MailItem.Recipients.Count;
				num3 = this.recipientsWithTransientFailure.Count;
				if (num3 != 0)
				{
					if (num3 < count2)
					{
						this.TracePass("Message: {0}. PL Agent will fork this mail item as {1} out of {2} recipients have transient failure.", new object[]
						{
							this.mailId,
							num3,
							count2
						});
						this.source.Fork(this.recipientsWithTransientFailure);
					}
					if (this.IncrementDeferralCountAndCheckCap())
					{
						this.TracePass("Message: {0}. PL Agent will defer this mail item as ({1}) recipient(s) have transient failure.", new object[]
						{
							this.mailId,
							num3
						});
						SmtpResponse deferReason;
						if (ex != null)
						{
							deferReason = Utils.GetResponseForExceptionDeferral(ex, new string[]
							{
								string.Format(CultureInfo.InvariantCulture, "A transient error occurred during prelicensing when communicating with RMS server {0}.", new object[]
								{
									preLicenseAgentAsyncState.LicensingUri.OriginalString
								})
							});
						}
						else if (flag)
						{
							deferReason = Utils.GetResponseForDeferral(new string[]
							{
								string.Format(CultureInfo.InvariantCulture, "A transient error occurred during prelicensing when communicating with RMS server {0}. Failure Code {1}", new object[]
								{
									preLicenseAgentAsyncState.LicensingUri.OriginalString,
									rightsManagementFailureCode
								})
							});
						}
						else
						{
							deferReason = Utils.GetResponseForDeferral(new string[]
							{
								string.Format(CultureInfo.InvariantCulture, "A transient error occurred during prelicensing when communicating with RMS server {0}. First recipient {1} failure code is {2}", new object[]
								{
									preLicenseAgentAsyncState.LicensingUri.OriginalString,
									text,
									rightsManagementFailureCode
								})
							});
						}
						this.source.Defer(RmsClientManager.AppSettings.TransientErrorDeferInterval, deferReason);
						PrelicenseAgentPerfCounters.TotalDeferralsToPreLicense.Increment();
					}
					else
					{
						this.TracePass("Message: {0}.  Deferral threshold has been reached.  Letting message through without pre-license.", new object[]
						{
							this.mailId
						});
						num2 += num3;
					}
				}
				if (num2 > 0)
				{
					PrelicenseAgentPerfCounters.TotalRecipientsFailedToPreLicense.IncrementBy((long)num2);
					if (num2 == count2)
					{
						PrelicenseAgentPerfCounters.TotalMessagesFailedToPreLicense.Increment();
						this.agentFactory.UpdatePercentileCounters(false);
					}
				}
				if (num > 0)
				{
					PrelicenseAgentPerfCounters.TotalRecipientsPreLicensed.IncrementBy((long)num);
					PrelicenseAgentPerfCounters.TotalMessagesPreLicensed.Increment();
					this.agentFactory.UpdatePercentileCounters(true);
				}
			}
			finally
			{
				this.CleanUp(preLicenseAgentAsyncState);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00006868 File Offset: 0x00004A68
		private void CreateSignatureBuilder(string serverUseLicense, string publishLicense, RmsClientManagerContext context, Uri licensingUri)
		{
			if (this.signatureBuilder != null)
			{
				return;
			}
			using (DisposableTenantLicensePair disposableTenantLicensePair = RmsClientManager.AcquireTenantLicenses(context, licensingUri))
			{
				this.signatureBuilder = new RightsSignatureBuilder(serverUseLicense, publishLicense, RmsClientManager.EnvironmentHandle, disposableTenantLicensePair);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000068B8 File Offset: 0x00004AB8
		private void AddSignature(LicenseResponse response, EnvelopeRecipient recipient)
		{
			ContentRight? usageRights = response.UsageRights;
			object obj;
			if (recipient.Properties.TryGetValue("Microsoft.Exchange.PrelicenseAgent.RecipientSid", out obj) && obj != null && this.signatureBuilder != null && usageRights != null && usageRights.Value != ContentRight.None)
			{
				recipient.Properties["Microsoft.Exchange.RightsManagement.DRMRights"] = (int)usageRights.Value;
				try
				{
					ExDateTime useLicenseExpiryTime = RmsClientManagerUtils.GetUseLicenseExpiryTime(response);
					recipient.Properties["Microsoft.Exchange.RightsManagement.DRMExpiryTime"] = (DateTime)useLicenseExpiryTime;
					recipient.Properties["Microsoft.Exchange.RightsManagement.DRMPropsSignature"] = this.signatureBuilder.Sign(usageRights.Value, useLicenseExpiryTime, new SecurityIdentifier((string)obj));
					return;
				}
				catch (RightsManagementException ex)
				{
					this.TraceFail("Message: {0}. Prelicensing agent is not stamping DRMPropsSignature property for recipient {1}.  Exception: {2}", new object[]
					{
						this.mailId,
						recipient.Address,
						ex.ToString()
					});
					return;
				}
				catch (ExchangeConfigurationException ex2)
				{
					this.TraceFail("Message: {0}. Prelicensing agent is not stamping DRMPropsSignature property for recipient {1}.  Exception: {2}", new object[]
					{
						this.mailId,
						recipient.Address,
						ex2.ToString()
					});
					return;
				}
			}
			this.TracePass("Message: {0}. Prelicensing agent is not stamping DRMRights/DRMExpiryTime property for recipient {1} because of incomplete information.  Usage rights available? {2}; Recipient SID? {3}; Signature builder? {4}", new object[]
			{
				this.mailId,
				recipient.Address,
				usageRights != null,
				obj != null,
				this.signatureBuilder != null
			});
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00006A78 File Offset: 0x00004C78
		private void CleanUp(PrelicenseAgent.PreLicenseAgentAsyncState asyncState)
		{
			this.TracePass("Message: {0}. PL Agent will do clean up now.", new object[]
			{
				this.mailId
			});
			if (this.signatureBuilder != null)
			{
				this.signatureBuilder.Dispose();
				this.signatureBuilder = null;
			}
			if (this.active)
			{
				this.agentFactory.InstanceController.MakeInactive(this.tenantId);
				this.active = false;
			}
			this.recipientsWithTransientFailure = null;
			this.recipientsToProcess = null;
			this.mailId = null;
			this.source = null;
			if (asyncState != null && asyncState.AgentAsyncContext != null)
			{
				asyncState.AgentAsyncContext.Complete();
			}
			this.breadcrumbs.Drop(PrelicenseAgent.AgentState.CleanedUp);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00006B20 File Offset: 0x00004D20
		private bool IncrementDeferralCountAndCheckCap()
		{
			int num = Utils.IncrementDeferralCount(base.MailItem, "Microsoft.Exchange.PrelicenseAgent.DeferralCount");
			if (num == -1)
			{
				this.TraceFail("Deferral count of message {0} is broken.", new object[]
				{
					this.mailId
				});
				return false;
			}
			if (num > 1)
			{
				this.TraceFail("Message {0} has been deferred {1} time(s).", new object[]
				{
					this.mailId,
					num - 1
				});
			}
			return num <= 2;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00006B94 File Offset: 0x00004D94
		private void AcquireServerUseLicense(OrganizationId organizationId, Uri licensingUri, string publishLicense, XmlNode[] publishLicenseAsXmlNodeArray)
		{
			this.breadcrumbs.Drop(PrelicenseAgent.AgentState.AcquireServerUseLicense);
			PrelicenseAgent.PreLicenseAgentAsyncState preLicenseAgentAsyncState = new PrelicenseAgent.PreLicenseAgentAsyncState(organizationId, licensingUri, true, publishLicense, publishLicenseAsXmlNodeArray, base.MailItem, this.mailId, base.GetAgentAsyncContext());
			bool flag = true;
			try
			{
				try
				{
					object obj;
					bool flag2 = base.MailItem.Properties.TryGetValue("Microsoft.Exchange.RightsManagement.TransportDecryptionUL", out obj) && !string.IsNullOrEmpty((string)obj);
					if (organizationId == OrganizationId.ForestWideOrgId && !flag2 && this.IsInternalServerPreLicensingEnabled(organizationId))
					{
						this.breadcrumbs.Drop(PrelicenseAgent.AgentState.BeginAcquireUseLicense);
						RmsClientManager.SaveContextCallback = new RmsClientManager.SaveContextOnAsyncQueryCallback(this.SaveContext);
						RmsClientManager.BeginAcquireUseLicense(preLicenseAgentAsyncState.Context, licensingUri, true, publishLicenseAsXmlNodeArray, null, new AsyncCallback(this.AcquireServerUseLicenseCallback), preLicenseAgentAsyncState);
						flag = false;
						return;
					}
					this.TracePass("Message: {0}.  No need to acquire server use license.", new object[]
					{
						this.mailId
					});
				}
				catch (RightsManagementException e)
				{
					if (this.HandleRightsManagementExceptionForServerLicensing(e, licensingUri))
					{
						flag = true;
						return;
					}
				}
				catch (ExchangeConfigurationException e2)
				{
					if (this.HandleTransientExceptionForServerLicensing(e2, licensingUri))
					{
						flag = true;
						return;
					}
				}
				this.TracePass("Message: {0}.  Acquiring pre-licenses for recipients...", new object[]
				{
					this.mailId
				});
				this.AcquirePreLicense(preLicenseAgentAsyncState);
				flag = false;
			}
			finally
			{
				if (flag)
				{
					this.CleanUp(preLicenseAgentAsyncState);
				}
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00006CF4 File Offset: 0x00004EF4
		private void AcquireServerUseLicenseCallback(IAsyncResult asyncResult)
		{
			this.breadcrumbs.Drop(PrelicenseAgent.AgentState.AcquireServerUseLicenseCallback);
			ArgumentValidator.ThrowIfNull("asyncResult", asyncResult);
			ArgumentValidator.ThrowIfNull("asyncResult.AsyncState", asyncResult.AsyncState);
			PrelicenseAgent.PreLicenseAgentAsyncState preLicenseAgentAsyncState = asyncResult.AsyncState as PrelicenseAgent.PreLicenseAgentAsyncState;
			if (preLicenseAgentAsyncState == null)
			{
				throw new InvalidOperationException("asyncResult.AsyncState not of type PreLicenseAgentAsyncState");
			}
			this.SaveContext(preLicenseAgentAsyncState);
			this.TracePass("Message: {0}.  Got call-back for AcquireServerUseLicense.", new object[]
			{
				this.mailId
			});
			bool flag = true;
			try
			{
				try
				{
					string license = RmsClientManager.EndAcquireUseLicense(asyncResult).License;
					base.MailItem.Properties["Microsoft.Exchange.RightsManagement.TransportDecryptionUL"] = license;
					PrelicenseAgentPerfCounters.TotalMessagesLicensed.Increment();
					this.agentFactory.UpdatePercentileCounters(true);
				}
				catch (RightsManagementException e)
				{
					if (this.HandleRightsManagementExceptionForServerLicensing(e, preLicenseAgentAsyncState.LicensingUri))
					{
						flag = true;
						return;
					}
				}
				catch (ExchangeConfigurationException e2)
				{
					if (this.HandleTransientExceptionForServerLicensing(e2, preLicenseAgentAsyncState.LicensingUri))
					{
						flag = true;
						return;
					}
				}
				this.TracePass("Message: {0}.  Acquiring pre-licenses for recipients...", new object[]
				{
					this.mailId
				});
				this.AcquirePreLicense(preLicenseAgentAsyncState);
				flag = false;
			}
			finally
			{
				if (flag)
				{
					this.CleanUp(preLicenseAgentAsyncState);
				}
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00006E30 File Offset: 0x00005030
		private void AcquirePrelicenseFromExternalRms(OrganizationId organizationId, Uri licensingUri, string publishLicense, XmlNode[] publishLicenseAsXmlNodeArray)
		{
			if (this.recipientsToProcess.Count == 0)
			{
				this.TracePass("Message: {0}. No recipients to process for B2B prelicensing", new object[]
				{
					this.mailId
				});
				return;
			}
			this.TracePass("Message: {0}. PL Agent will get list of identities.", new object[]
			{
				this.mailId
			});
			int count = this.recipientsToProcess.Count;
			string[] array = new string[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = this.recipientsToProcess[i].Address.ToString();
			}
			this.TracePass("Message: {0}. PL Agent will switch to Async mode for B2B prelicensing.", new object[]
			{
				this.mailId
			});
			this.breadcrumbs.Drop(PrelicenseAgent.AgentState.BeginAcquirePreLicenseFromExternalRms);
			PrelicenseAgent.PreLicenseAgentAsyncState preLicenseAgentAsyncState = new PrelicenseAgent.PreLicenseAgentAsyncState(organizationId, licensingUri, false, publishLicense, publishLicenseAsXmlNodeArray, base.MailItem, this.mailId, base.GetAgentAsyncContext());
			RmsClientManager.SaveContextCallback = new RmsClientManager.SaveContextOnAsyncQueryCallback(this.SaveContext);
			RmsClientManager.BeginAcquirePreLicense(preLicenseAgentAsyncState.Context, licensingUri, false, publishLicenseAsXmlNodeArray, array, new AsyncCallback(this.AcquirePreLicenseCallback), preLicenseAgentAsyncState);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00006F44 File Offset: 0x00005144
		private bool HandleRightsManagementExceptionForServerLicensing(RightsManagementException e, Uri licenseUri)
		{
			RmsEventLogHandler.LogException(PrelicenseAgentFactory.Logger, base.MailItem, RmsComponent.PrelicensingAgent, e, e.IsPermanent);
			if (!e.IsPermanent)
			{
				return this.HandleTransientExceptionForServerLicensing(e, licenseUri);
			}
			this.TraceFail("Message: {0}.  Prelicensing agent caught a permanent exception while acquiring a server use license.  Exception: {1}", new object[]
			{
				this.mailId,
				e
			});
			PrelicenseAgentPerfCounters.TotalMessagesFailedToLicense.Increment();
			this.agentFactory.UpdatePercentileCounters(false);
			return false;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00006FB4 File Offset: 0x000051B4
		private bool HandleTransientExceptionForServerLicensing(Exception e, Uri licenseUri)
		{
			if (this.IncrementDeferralCountAndCheckCap())
			{
				this.TracePass("Message: {0}. Prelicensing agent is deferring this message because of a transient exception caught while acquiring a server use license.  Exception: {1}", new object[]
				{
					this.mailId,
					e
				});
				this.source.Defer(RmsClientManager.AppSettings.TransientErrorDeferInterval, Utils.GetResponseForExceptionDeferral(e, new string[]
				{
					string.Format(CultureInfo.InvariantCulture, "A transient error occurred during prelicensing when communicating with RMS server {0}.", new object[]
					{
						licenseUri.OriginalString
					})
				}));
				PrelicenseAgentPerfCounters.TotalDeferralsToLicense.Increment();
				return true;
			}
			this.TraceFail("Message: {0}.  Prelicensing agent caught a transient exception while acquiring a server use license but the deferral threshold has been reached.  Exception: {1}", new object[]
			{
				this.mailId,
				e
			});
			PrelicenseAgentPerfCounters.TotalMessagesFailedToLicense.Increment();
			this.agentFactory.UpdatePercentileCounters(false);
			return false;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000707C File Offset: 0x0000527C
		private bool TryGetPublishLicense(out string publishLicense)
		{
			publishLicense = null;
			Attachment attachment = base.MailItem.Message.Attachments[0];
			Stream stream;
			if (attachment.TryGetContentReadStream(out stream))
			{
				using (stream)
				{
					using (DrmEmailMessageContainer drmEmailMessageContainer = new DrmEmailMessageContainer())
					{
						try
						{
							drmEmailMessageContainer.Load(stream, (object param0) => Streams.CreateTemporaryStorageStream());
							publishLicense = drmEmailMessageContainer.PublishLicense;
							return true;
						}
						catch (InvalidRpmsgFormatException ex)
						{
							this.TraceFail("Exception thrown while trying to load drm email message out of protectedStream. Error is {0}", new object[]
							{
								ex
							});
							return false;
						}
					}
				}
			}
			this.TraceFail("Failed to read protected stream, InvalidRpmsgFormatException was not thrown", new object[0]);
			return false;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000715C File Offset: 0x0000535C
		private void SaveContext(object state)
		{
			PrelicenseAgent.PreLicenseAgentAsyncState preLicenseAgentAsyncState = state as PrelicenseAgent.PreLicenseAgentAsyncState;
			if (preLicenseAgentAsyncState != null && preLicenseAgentAsyncState.AgentAsyncContext != null)
			{
				preLicenseAgentAsyncState.AgentAsyncContext.Resume();
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00007186 File Offset: 0x00005386
		private void TracePass(string formatString, params object[] args)
		{
			if (base.MailItem != null)
			{
				RmsClientManager.TracePass(this, base.MailItem.SystemProbeId, formatString, args);
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000071A3 File Offset: 0x000053A3
		private void TraceFail(string formatString, params object[] args)
		{
			if (base.MailItem != null)
			{
				RmsClientManager.TraceFail(this, base.MailItem.SystemProbeId, formatString, args);
			}
		}

		// Token: 0x0400008A RID: 138
		private const int MaxDeferrals = 2;

		// Token: 0x0400008B RID: 139
		private const string DeferralCountProperty = "Microsoft.Exchange.PrelicenseAgent.DeferralCount";

		// Token: 0x0400008C RID: 140
		private const string RecipientSidPropertyName = "Microsoft.Exchange.PrelicenseAgent.RecipientSid";

		// Token: 0x0400008D RID: 141
		private static readonly byte[] EmptyByteArray = new byte[0];

		// Token: 0x0400008E RID: 142
		private readonly PrelicenseAgentFactory agentFactory;

		// Token: 0x0400008F RID: 143
		private readonly AddressBook addressbook;

		// Token: 0x04000090 RID: 144
		private readonly Breadcrumbs<PrelicenseAgent.AgentState> breadcrumbs = new Breadcrumbs<PrelicenseAgent.AgentState>(8);

		// Token: 0x04000091 RID: 145
		private RoutedMessageEventSource source;

		// Token: 0x04000092 RID: 146
		private bool active;

		// Token: 0x04000093 RID: 147
		private string mailId;

		// Token: 0x04000094 RID: 148
		private List<EnvelopeRecipient> recipientsToProcess;

		// Token: 0x04000095 RID: 149
		private List<EnvelopeRecipient> recipientsWithTransientFailure;

		// Token: 0x04000096 RID: 150
		private Guid tenantId;

		// Token: 0x04000097 RID: 151
		private RightsSignatureBuilder signatureBuilder;

		// Token: 0x02000014 RID: 20
		private enum AgentState
		{
			// Token: 0x0400009A RID: 154
			Initialized,
			// Token: 0x0400009B RID: 155
			Started,
			// Token: 0x0400009C RID: 156
			BeginAcquirePreLicense,
			// Token: 0x0400009D RID: 157
			AcquirePreLicenseCallback,
			// Token: 0x0400009E RID: 158
			CleanedUp,
			// Token: 0x0400009F RID: 159
			AcquireServerUseLicense,
			// Token: 0x040000A0 RID: 160
			AcquireServerUseLicenseCallback,
			// Token: 0x040000A1 RID: 161
			BeginAcquireUseLicense,
			// Token: 0x040000A2 RID: 162
			BeginAcquirePreLicenseFromExternalRms
		}

		// Token: 0x02000015 RID: 21
		private enum ConfigurationType
		{
			// Token: 0x040000A4 RID: 164
			InternalEndUserPreLicensing,
			// Token: 0x040000A5 RID: 165
			InternalServerPreLicensing,
			// Token: 0x040000A6 RID: 166
			ExternalServerPreLicensing
		}

		// Token: 0x02000016 RID: 22
		private class PreLicenseAgentAsyncState
		{
			// Token: 0x06000070 RID: 112 RVA: 0x000071D0 File Offset: 0x000053D0
			public PreLicenseAgentAsyncState(OrganizationId organizationId, Uri licensingUri, bool isInternalRmsUri, string publishLicense, XmlNode[] publishLicenseAsXmlNodeArray, MailItem mailItem, string messageId, AgentAsyncContext agentAsyncContext)
			{
				ArgumentValidator.ThrowIfNull("organizationId", organizationId);
				ArgumentValidator.ThrowIfNull("licensingUri", licensingUri);
				ArgumentValidator.ThrowIfNullOrEmpty("publishLicense", publishLicense);
				ArgumentValidator.ThrowIfNull("publishLicenseAsXmlNodeArray", publishLicenseAsXmlNodeArray);
				ArgumentValidator.ThrowIfNull("agentAsyncContext", agentAsyncContext);
				this.OrganizationId = organizationId;
				this.LicensingUri = licensingUri;
				this.IsInternalRmsUri = isInternalRmsUri;
				this.PublishLicense = publishLicense;
				this.PublishLicenseAsXmlNodeArray = publishLicenseAsXmlNodeArray;
				this.AgentAsyncContext = agentAsyncContext;
				this.Context = Utils.CreateRmsContext(organizationId, mailItem, messageId, publishLicense);
			}

			// Token: 0x040000A7 RID: 167
			public readonly OrganizationId OrganizationId;

			// Token: 0x040000A8 RID: 168
			public readonly Uri LicensingUri;

			// Token: 0x040000A9 RID: 169
			public readonly bool IsInternalRmsUri;

			// Token: 0x040000AA RID: 170
			public readonly string PublishLicense;

			// Token: 0x040000AB RID: 171
			public readonly XmlNode[] PublishLicenseAsXmlNodeArray;

			// Token: 0x040000AC RID: 172
			public readonly AgentAsyncContext AgentAsyncContext;

			// Token: 0x040000AD RID: 173
			public readonly RmsClientManagerContext Context;
		}
	}
}
