using System;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Configuration;
using Microsoft.Exchange.Transport.RightsManagement;

namespace Microsoft.Exchange.MessagingPolicies.RmSvcAgent
{
	// Token: 0x02000025 RID: 37
	internal class DecryptionBaseComponent
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00009A84 File Offset: 0x00007C84
		public DecryptionBaseComponent(MailItem mailItem, OnProcessDecryption onDecryptionProcess)
		{
			ArgumentValidator.ThrowIfNull("mailItem", mailItem);
			ArgumentValidator.ThrowIfNull("onDecryptionProcess", onDecryptionProcess);
			this.objHashCode = this.GetHashCode();
			this.mailItem = mailItem;
			this.onDecryptionProcess = onDecryptionProcess;
			this.breadcrumbs = new Breadcrumbs<string>(8);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00009AD4 File Offset: 0x00007CD4
		public void StartDecryption()
		{
			bool flag = false;
			Exception ex = null;
			try
			{
				this.rmsDecryptor = this.CreateRmsDecryptor();
			}
			catch (InvalidRpmsgFormatException ex2)
			{
				ex = ex2;
				return;
			}
			catch (RightsManagementException ex3)
			{
				ex = ex3;
				return;
			}
			catch (ExchangeConfigurationException ex4)
			{
				ex = ex4;
				flag = true;
				return;
			}
			finally
			{
				if (ex != null)
				{
					if (!flag)
					{
						ExTraceGlobals.RmSvcAgentTracer.TraceError<string, Exception>((long)this.objHashCode, "Failed to create the RMSDecryptor to load the protected message {0}. Error {1}", this.mailItem.Message.MessageId, ex);
						DecryptionBaseComponent.Logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_TransportDecryptionPermanentException, this.mailItem.Message.MessageId, new object[]
						{
							this.mailItem.Message.MessageId,
							this.orgId,
							ex
						});
						this.onDecryptionProcess(DecryptionStatus.PermanentFailure, this.transportDecryptionSetting, null, ex);
					}
					else
					{
						ExTraceGlobals.RmSvcAgentTracer.TraceError<string, Exception>((long)this.objHashCode, "Failed to create the RMSDecryptor for message {0}. Error: {1}", this.mailItem.Message.MessageId, ex);
						DecryptionBaseComponent.Logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_TransportDecryptionTransientException, this.mailItem.Message.MessageId, new object[]
						{
							this.mailItem.Message.MessageId,
							this.orgId,
							ex
						});
						this.onDecryptionProcess(DecryptionStatus.TransientFailure, this.transportDecryptionSetting, null, ex);
					}
				}
			}
			ExTraceGlobals.RmSvcAgentTracer.TraceDebug<string, OrganizationId>((long)this.objHashCode, "Begin Transport Decrypting Message {0} for OrgId {1}", this.mailItem.Message.MessageId, this.orgId);
			this.rmsDecryptor.BeginDecrypt(new AsyncCallback(this.OnDecryptComplete), new AgentAsyncState((AgentAsyncContext)this.onDecryptionProcess(DecryptionStatus.StartAsync, this.transportDecryptionSetting, null, null)));
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00009D2C File Offset: 0x00007F2C
		public bool ShouldProcess()
		{
			if (!Utils.IsProtectedEmail(this.mailItem.Message))
			{
				ExTraceGlobals.RmSvcAgentTracer.TraceDebug<string>((long)this.objHashCode, "Skip Transport Decryption because message {0} is not RMS format.", this.mailItem.Message.MessageId);
				return false;
			}
			if (!Utils.IsSupportedMapiMessageClass(this.mailItem.Message))
			{
				ExTraceGlobals.RmSvcAgentTracer.TraceDebug<string>((long)this.objHashCode, "Skip Transport Decryption because message {0} is not IPM.Note.", this.mailItem.Message.MessageId);
				return false;
			}
			if (DecryptionBaseComponent.GetTransportDecryptionActionHeader(this.mailItem) != null)
			{
				ExTraceGlobals.RmSvcAgentTracer.TraceDebug<string>((long)this.objHashCode, "TransportDecryption already applies to message {0} in another hop. Skip decryption", this.mailItem.Message.MessageId);
				return false;
			}
			this.orgId = Utils.OrgIdFromMailItem(this.mailItem);
			Exception ex = null;
			try
			{
				this.transportDecryptionSetting = RmsClientManager.IRMConfig.GetTenantTransportDecryptionSetting(this.orgId);
			}
			catch (ExchangeConfigurationException ex2)
			{
				ex = ex2;
			}
			catch (RightsManagementException ex3)
			{
				ex = ex3;
			}
			if (ex == null && this.transportDecryptionSetting != TransportDecryptionSetting.Disabled)
			{
				ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
				{
					PerTenantAcceptedDomainTable acceptedDomainTable = Components.Configuration.GetAcceptedDomainTable(this.orgId);
					this.acceptedDomains = acceptedDomainTable.AcceptedDomainTable;
				}, 0);
				if (!adoperationResult.Succeeded)
				{
					ex = adoperationResult.Exception;
				}
			}
			if (ex != null)
			{
				ExTraceGlobals.RmSvcAgentTracer.TraceError<OrganizationId, string, Exception>((long)this.objHashCode, "Failed to load IRM config for org {0}. messageID {1}. Error : {2}", this.orgId, this.mailItem.Message.MessageId, ex);
				DecryptionBaseComponent.Logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_TransportDecryptionTransientException, this.mailItem.Message.MessageId, new object[]
				{
					this.mailItem.Message.MessageId,
					this.orgId,
					ex
				});
				this.onDecryptionProcess(DecryptionStatus.ConfigurationLoadFailure, TransportDecryptionSetting.Mandatory, null, ex);
				return false;
			}
			if (this.transportDecryptionSetting == TransportDecryptionSetting.Disabled)
			{
				ExTraceGlobals.RmSvcAgentTracer.TraceDebug<OrganizationId, string>((long)this.objHashCode, "TransportDecryption is disabled for OrgId {0}. Skip decryption for message {1}", this.orgId, this.mailItem.Message.MessageId);
				return false;
			}
			return true;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00009F34 File Offset: 0x00008134
		protected virtual RmsDecryptor CreateRmsDecryptor()
		{
			string decryptionTokenRecipient = Utils.GetDecryptionTokenRecipient(this.mailItem, this.acceptedDomains);
			DrmEmailMessageContainer drmEmailMessageContainer = RmsDecryptor.DrmEmailMessageContainerFromMessage(this.mailItem.Message);
			this.publishLicense = drmEmailMessageContainer.PublishLicense;
			RmsClientManagerContext context = Utils.CreateRmsContext(this.orgId, this.mailItem, this.mailItem.Message.MessageId, this.publishLicense);
			return new RmsDecryptor(context, this.mailItem.Message, drmEmailMessageContainer, decryptionTokenRecipient, null, Utils.GetOutboundConversionOptions(this.mailItem), this.breadcrumbs, false, true, true, true);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00009FC4 File Offset: 0x000081C4
		protected virtual void UpdateMimeDoc(EmailMessage emailMessage)
		{
			try
			{
				using (Stream mimeWriteStream = this.mailItem.GetMimeWriteStream())
				{
					emailMessage.MimeDocument.RootPart.WriteTo(mimeWriteStream);
				}
			}
			finally
			{
				if (emailMessage != null)
				{
					((IDisposable)emailMessage).Dispose();
				}
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000A028 File Offset: 0x00008228
		internal static void UpdatePercentileCounters(bool success)
		{
			if (success)
			{
				DecryptionBaseComponent.PercentileCounter.AddValue(0L);
			}
			else
			{
				DecryptionBaseComponent.PercentileCounter.AddValue(1L);
			}
			RmsDecryptionAgentPerfCounters.Percentile95FailedToDecrypt.RawValue = DecryptionBaseComponent.PercentileCounter.PercentileQuery(95.0);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000A064 File Offset: 0x00008264
		private static Header GetTransportDecryptionActionHeader(MailItem mailItem)
		{
			return Utils.GetXHeader(mailItem.Message, "X-MS-Exchange-Forest-TransportDecryption-Action");
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000A076 File Offset: 0x00008276
		private static void SetTransportDecryptionLicenseUri(MailItem mailItem, Uri licenseUri)
		{
			mailItem.Properties["Microsoft.Exchange.RightsManagement.TransportDecryptionLicenseUri"] = licenseUri.OriginalString;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000A090 File Offset: 0x00008290
		private void OnDecryptComplete(IAsyncResult result)
		{
			this.breadcrumbs.Drop("OnDecryptComplete");
			DecryptionStatus status = DecryptionStatus.Success;
			ArgumentValidator.ThrowIfNull("result", result);
			ArgumentValidator.ThrowIfNull("result.AsyncState", result.AsyncState);
			((AgentAsyncState)result.AsyncState).Resume();
			AsyncDecryptionOperationResult<DecryptionResultData> asyncDecryptionOperationResult = (AsyncDecryptionOperationResult<DecryptionResultData>)this.rmsDecryptor.EndDecrypt(result);
			if (asyncDecryptionOperationResult.IsSucceeded)
			{
				ExTraceGlobals.RmSvcAgentTracer.TraceDebug<string, OrganizationId>((long)this.objHashCode, "Message {0} for OrgId {1} successfully decrypted.", this.mailItem.Message.MessageId, this.orgId);
				DecryptionBaseComponent.Logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_TransportDecryptionSucceeded, null, new object[]
				{
					this.mailItem.Message.MessageId,
					this.orgId
				});
				LamHelper.PublishSuccessfulIrmDecryptionToLAM(this.mailItem);
				this.UpdateMimeDoc(asyncDecryptionOperationResult.Data.DecryptedMessage);
				Utils.SetTransportDecryptionPLAndUL(this.mailItem, this.publishLicense, asyncDecryptionOperationResult.Data.UseLicense);
				DecryptionBaseComponent.SetTransportDecryptionLicenseUri(this.mailItem, asyncDecryptionOperationResult.Data.LicenseUri);
				Utils.SetTransportDecryptionApplied(this.mailItem, false);
				RmsDecryptionAgentPerfCounters.MessageDecrypted.Increment();
				DecryptionBaseComponent.UpdatePercentileCounters(true);
			}
			else if (asyncDecryptionOperationResult.IsTransientException)
			{
				ExTraceGlobals.RmSvcAgentTracer.TraceError<string, OrganizationId, Exception>((long)this.objHashCode, "Transport Decryption failed to decrypt message {0}, orgId {1}, transient exception {2}", this.mailItem.Message.MessageId, this.orgId, asyncDecryptionOperationResult.Exception);
				DecryptionBaseComponent.Logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_TransportDecryptionTransientException, this.mailItem.Message.MessageId, new object[]
				{
					this.mailItem.Message.MessageId,
					this.orgId,
					asyncDecryptionOperationResult.Exception
				});
				status = DecryptionStatus.TransientFailure;
			}
			else
			{
				ExTraceGlobals.RmSvcAgentTracer.TraceError<string, OrganizationId, Exception>((long)this.objHashCode, "Transport Decryption failed to decrypt message {0}, orgId {1}, permanent exception {2}", this.mailItem.Message.MessageId, this.orgId, asyncDecryptionOperationResult.Exception);
				DecryptionBaseComponent.Logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_TransportDecryptionPermanentException, this.mailItem.Message.MessageId, new object[]
				{
					this.mailItem.Message.MessageId,
					this.orgId,
					asyncDecryptionOperationResult.Exception
				});
				status = DecryptionStatus.PermanentFailure;
			}
			this.onDecryptionProcess(status, this.transportDecryptionSetting, (AgentAsyncState)result.AsyncState, asyncDecryptionOperationResult.Exception);
		}

		// Token: 0x04000119 RID: 281
		internal static readonly ExEventLog Logger = new ExEventLog(new Guid("7D2A0005-2C75-42ac-B495-8FE62F3B4FCF"), "MSExchange Messaging Policies");

		// Token: 0x0400011A RID: 282
		internal static PercentileCounter PercentileCounter = new PercentileCounter(TimeSpan.FromMinutes(30.0), TimeSpan.FromMinutes(1.0), 1L, 2L);

		// Token: 0x0400011B RID: 283
		protected OrganizationId orgId;

		// Token: 0x0400011C RID: 284
		protected AcceptedDomainCollection acceptedDomains;

		// Token: 0x0400011D RID: 285
		protected TransportDecryptionSetting transportDecryptionSetting;

		// Token: 0x0400011E RID: 286
		private readonly int objHashCode;

		// Token: 0x0400011F RID: 287
		private readonly Breadcrumbs<string> breadcrumbs;

		// Token: 0x04000120 RID: 288
		private readonly MailItem mailItem;

		// Token: 0x04000121 RID: 289
		private readonly OnProcessDecryption onDecryptionProcess;

		// Token: 0x04000122 RID: 290
		private RmsDecryptor rmsDecryptor;

		// Token: 0x04000123 RID: 291
		private string publishLicense;
	}
}
