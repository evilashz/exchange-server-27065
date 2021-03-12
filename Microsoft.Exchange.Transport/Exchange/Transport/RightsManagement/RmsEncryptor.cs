using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.Exchange.Security.RightsManagement.Protectors;

namespace Microsoft.Exchange.Transport.RightsManagement
{
	// Token: 0x020003EB RID: 1003
	internal class RmsEncryptor : IDisposeTrackable, IDisposable
	{
		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x06002D9B RID: 11675 RVA: 0x000B70D8 File Offset: 0x000B52D8
		public string UseLicense
		{
			get
			{
				return this.useLicense;
			}
		}

		// Token: 0x06002D9C RID: 11676 RVA: 0x000B70E0 File Offset: 0x000B52E0
		public RmsEncryptor(OrganizationId orgId, IReadOnlyMailItem mailItem, Guid templateId, Breadcrumbs<string> breadcrumbs, Guid? externalDirectoryOrgId = null)
		{
			ArgumentValidator.ThrowIfNull("orgId", orgId);
			ArgumentValidator.ThrowIfNull("mailItem", mailItem);
			ArgumentValidator.ThrowIfNull("templateId", templateId);
			this.templateId = templateId;
			this.orgId = orgId;
			this.mailItem = mailItem;
			this.messageId = mailItem.Message.MessageId;
			this.breadcrumbs = breadcrumbs;
			this.objHashCode = this.GetHashCode();
			this.isReEncryption = false;
			this.licenseUri = RmsClientManager.GetFirstLicensingLocation(orgId);
			this.rmsContext = ((externalDirectoryOrgId != null) ? this.CreateRmsClientContext(externalDirectoryOrgId.Value) : this.CreateRmsClientContext());
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x06002D9D RID: 11677 RVA: 0x000B7198 File Offset: 0x000B5398
		public RmsEncryptor(OrganizationId orgId, IReadOnlyMailItem mailItem, string publishingLicense, string useLicense, Uri licenseUri, Breadcrumbs<string> breadcrumbs)
		{
			ArgumentValidator.ThrowIfNull("orgId", orgId);
			ArgumentValidator.ThrowIfNull("mailItem", mailItem);
			ArgumentValidator.ThrowIfNullOrEmpty("publishingLicense", publishingLicense);
			ArgumentValidator.ThrowIfNullOrEmpty("useLicense", useLicense);
			ArgumentValidator.ThrowIfNull("licenseUri", licenseUri);
			this.orgId = orgId;
			this.mailItem = mailItem;
			this.messageId = mailItem.Message.MessageId;
			this.publishingLicense = publishingLicense;
			this.useLicense = useLicense;
			this.breadcrumbs = breadcrumbs;
			this.objHashCode = this.GetHashCode();
			this.licenseUri = licenseUri;
			this.isReEncryption = true;
			this.rmsContext = this.CreateRmsClientContext();
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x06002D9E RID: 11678 RVA: 0x000B724D File Offset: 0x000B544D
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<RmsEncryptor>(this);
		}

		// Token: 0x06002D9F RID: 11679 RVA: 0x000B7255 File Offset: 0x000B5455
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06002DA0 RID: 11680 RVA: 0x000B726C File Offset: 0x000B546C
		public void Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
				this.disposeTracker = null;
			}
			if (this.tenantLicenses != null)
			{
				this.tenantLicenses.Dispose();
				this.tenantLicenses = null;
			}
			this.disposed = true;
		}

		// Token: 0x06002DA1 RID: 11681 RVA: 0x000B72C0 File Offset: 0x000B54C0
		public bool Encrypt(out EmailMessage encryptedEmailMessage, out Exception exception)
		{
			encryptedEmailMessage = null;
			exception = null;
			AsyncOperationResult<EmailMessage> asyncOperationResult = this.EndEncrypt(this.BeginEncrypt(null, null));
			if (asyncOperationResult.IsSucceeded)
			{
				encryptedEmailMessage = asyncOperationResult.Data;
				return true;
			}
			exception = asyncOperationResult.Exception;
			return false;
		}

		// Token: 0x06002DA2 RID: 11682 RVA: 0x000B7300 File Offset: 0x000B5500
		public IAsyncResult BeginEncrypt(AsyncCallback asyncCallback, object state)
		{
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(null, state, asyncCallback);
			this.BreadCrumbsDrop(State.BeginAcquireTenantLicenses);
			ExTraceGlobals.RightsManagementTracer.TraceDebug<string, OrganizationId>((long)this.objHashCode, "BeginAcquireTenantLicenses for MessageId {0}, OrgId {1}", this.messageId, this.orgId);
			bool isInternal;
			try
			{
				isInternal = RmsClientManager.IsInternalRMSLicensingUri(this.orgId, this.licenseUri);
			}
			catch (ExchangeConfigurationException ex)
			{
				ExTraceGlobals.RightsManagementTracer.TraceError<Uri, OrganizationId, ExchangeConfigurationException>(0L, "IsInternalRMSLicenseUri threw an exception for {0}, Org {1}. Error {2}", this.licenseUri, this.orgId, ex);
				lazyAsyncResult.InvokeCallback(new AsyncOperationResult<EmailMessage>(null, ex));
				return lazyAsyncResult;
			}
			catch (RightsManagementException ex2)
			{
				ExTraceGlobals.RightsManagementTracer.TraceError<Uri, OrganizationId, RightsManagementException>(0L, "IsInternalRMSLicenseUri threw an exception for {0}, Org {1}. Error {2}", this.licenseUri, this.orgId, ex2);
				lazyAsyncResult.InvokeCallback(new AsyncOperationResult<EmailMessage>(null, ex2));
				return lazyAsyncResult;
			}
			this.BeginAcquireTenantLicenses(lazyAsyncResult, isInternal);
			return lazyAsyncResult;
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x000B73E4 File Offset: 0x000B55E4
		public AsyncOperationResult<EmailMessage> EndEncrypt(IAsyncResult result)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)result;
			return (AsyncOperationResult<EmailMessage>)lazyAsyncResult.InternalWaitForCompletion();
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x000B7403 File Offset: 0x000B5603
		protected virtual void BeginAcquireTenantLicenses(object state, bool isInternal)
		{
			RmsClientManager.SaveContextCallback = new RmsClientManager.SaveContextOnAsyncQueryCallback(this.SaveContext);
			RmsClientManager.BeginAcquireTenantLicenses(this.rmsContext, this.licenseUri, isInternal, new AsyncCallback(this.AcquireTenantLicensesCallback), state);
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x000B7436 File Offset: 0x000B5636
		protected virtual DisposableTenantLicensePair EndAcquireTenantLicenses(IAsyncResult asyncResult)
		{
			return RmsClientManager.EndAcquireTenantLicenses(asyncResult);
		}

		// Token: 0x06002DA6 RID: 11686 RVA: 0x000B743E File Offset: 0x000B563E
		protected virtual void BeginAcquireRMSTemplate(object state)
		{
			RmsClientManager.SaveContextCallback = new RmsClientManager.SaveContextOnAsyncQueryCallback(this.SaveContext);
			RmsClientManager.BeginAcquireRMSTemplate(this.rmsContext, this.templateId, new AsyncCallback(this.AcquireRmsTemplateCallback), state);
		}

		// Token: 0x06002DA7 RID: 11687 RVA: 0x000B7470 File Offset: 0x000B5670
		protected virtual RmsTemplate EndAcquireRMSTemplate(IAsyncResult asyncResult)
		{
			return RmsClientManager.EndAcquireRMSTemplate(asyncResult);
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x000B7478 File Offset: 0x000B5678
		protected virtual Stream GenerateRightsProtectedStream(Encoding encoding)
		{
			return MessageConverter.GenerateRightsProtectedStream(this.mailItem, encoding, this.tenantLicenses, this.publishingLicense, this.useLicense);
		}

		// Token: 0x06002DA9 RID: 11689 RVA: 0x000B7498 File Offset: 0x000B5698
		protected virtual Stream GenerateRightsProtectedStream(RmsTemplate template, ReadOnlyCollection<string> rcptAddresses, Encoding encoding)
		{
			return MessageConverter.GenerateRightsProtectedStream(this.mailItem, template, rcptAddresses, encoding, this.tenantLicenses);
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x000B74AE File Offset: 0x000B56AE
		protected virtual bool VerifyTenantLicensesValid()
		{
			return this.tenantLicenses != null && this.tenantLicenses.EnablingPrincipalRac != null;
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x000B74C8 File Offset: 0x000B56C8
		protected void AcquireTenantLicensesCallback(IAsyncResult asyncResult)
		{
			this.BreadCrumbsDrop(State.AcquireTenantLicensesCallback);
			ExTraceGlobals.RightsManagementTracer.TraceDebug<string, OrganizationId>((long)this.objHashCode, "Entered callback of BeginAcquireTenantLicenses for messageId {0}, orgId {1}", this.messageId, this.orgId);
			ArgumentValidator.ThrowIfNull("asyncResult", asyncResult);
			ArgumentValidator.ThrowIfNull("asyncResult.AsyncState", asyncResult.AsyncState);
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)asyncResult.AsyncState;
			Exception ex = null;
			try
			{
				this.tenantLicenses = this.EndAcquireTenantLicenses(asyncResult);
				if (!this.VerifyTenantLicensesValid())
				{
					ExTraceGlobals.RightsManagementTracer.TraceError<OrganizationId, string>((long)this.objHashCode, "The license pair we got for org {0}, Message {1} is invalid.", this.orgId, this.messageId);
					ex = new RightsManagementException(RightsManagementFailureCode.InvalidTenantLicense, Strings.InvalidTenantLicensePair);
				}
				else if (!this.isReEncryption)
				{
					this.BreadCrumbsDrop(State.BeginAcquireRMSTemplate);
					this.BeginAcquireRMSTemplate(lazyAsyncResult);
				}
				else
				{
					this.EncryptMessage(null, lazyAsyncResult);
				}
			}
			catch (RightsManagementException ex2)
			{
				ex = ex2;
			}
			catch (ExchangeConfigurationException ex3)
			{
				ex = ex3;
			}
			finally
			{
				if (ex != null)
				{
					this.BreadCrumbsDrop(State.AcquireTenantLicensesFailed);
					ExTraceGlobals.RightsManagementTracer.TraceError<string, OrganizationId, Exception>((long)this.objHashCode, "AcquireTenantLicenses failed for message {0}, orgId {1}, error {2}", this.messageId, this.orgId, ex);
					lazyAsyncResult.InvokeCallback(new AsyncOperationResult<EmailMessage>(null, ex));
				}
			}
		}

		// Token: 0x06002DAC RID: 11692 RVA: 0x000B7614 File Offset: 0x000B5814
		protected void AcquireRmsTemplateCallback(IAsyncResult asyncResult)
		{
			this.BreadCrumbsDrop(State.AcquireRMSTemplateCallback);
			ExTraceGlobals.RightsManagementTracer.TraceDebug<string, OrganizationId>((long)this.objHashCode, "Entered callback of BeginAcquireRmsTemplate for message {0}, orgId {1}", this.messageId, this.orgId);
			ArgumentValidator.ThrowIfNull("asyncResult", asyncResult);
			ArgumentValidator.ThrowIfNull("asyncResult.AsyncState", asyncResult.AsyncState);
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)asyncResult.AsyncState;
			Exception ex = null;
			RmsTemplate rmsTemplate = null;
			try
			{
				rmsTemplate = this.EndAcquireRMSTemplate(asyncResult);
				if (rmsTemplate == null)
				{
					ExTraceGlobals.RightsManagementTracer.TraceDebug<Guid, string>((long)this.objHashCode, "Template {0} does not exist. Message {1} is being NDR'd.", this.templateId, this.messageId);
					ex = new RightsManagementException(RightsManagementFailureCode.TemplateDoesNotExist, Strings.RMSTemplateNotFound(this.templateId));
				}
			}
			catch (RightsManagementException ex2)
			{
				ExTraceGlobals.RightsManagementTracer.TraceError<Guid, string, RightsManagementException>((long)this.objHashCode, "Failed to find RMS template {0} for message {1}. Details: {2}", this.templateId, this.messageId, ex2);
				ex = ex2;
			}
			catch (ExchangeConfigurationException ex3)
			{
				ExTraceGlobals.RightsManagementTracer.TraceError<Guid, string, ExchangeConfigurationException>((long)this.objHashCode, "Failed to find RMS template {0} for message {1}. Details: {2}", this.templateId, this.messageId, ex3);
				ex = ex3;
			}
			finally
			{
				if (ex != null)
				{
					this.BreadCrumbsDrop(State.AcquireRMSTemplateFailed);
					lazyAsyncResult.InvokeCallback(new AsyncOperationResult<EmailMessage>(null, ex));
				}
			}
			if (ex == null)
			{
				this.EncryptMessage(rmsTemplate, lazyAsyncResult);
			}
		}

		// Token: 0x06002DAD RID: 11693 RVA: 0x000B7768 File Offset: 0x000B5968
		private void EncryptMessage(RmsTemplate template, LazyAsyncResult originalAsyncResult)
		{
			Exception ex = null;
			EmailMessage data = null;
			if (!this.isReEncryption)
			{
				ExTraceGlobals.RightsManagementTracer.TraceDebug<string, string, Guid>((long)this.objHashCode, "ControlPoint Encryption: Rights-protecting message {0} with template {1} (ID={2})", this.messageId, template.Name, template.Id);
			}
			else
			{
				ExTraceGlobals.RightsManagementTracer.TraceDebug<string>((long)this.objHashCode, "Re-Encryption: Rights-protecting message {0}", this.messageId);
			}
			try
			{
				data = this.RightsProtectMessage(template);
				this.BreadCrumbsDrop(State.Encrypted);
			}
			catch (AttachmentProtectionException ex2)
			{
				ExTraceGlobals.RightsManagementTracer.TraceError<string, AttachmentProtectionException>((long)this.objHashCode, "Failed to protect message '{0}'.  AttachmentProtectionException: {1}", this.messageId, ex2);
				ex = new RightsManagementException(RightsManagementFailureCode.AttachmentProtectionFailed, Strings.AttachmentProtectionFailed, ex2);
			}
			catch (RightsManagementException ex3)
			{
				ExTraceGlobals.RightsManagementTracer.TraceError<string, RightsManagementException>((long)this.objHashCode, "Failed to protect message '{0}'. RightsManagementException: {1}", this.messageId, ex3);
				ex = ex3;
			}
			catch (ExchangeConfigurationException ex4)
			{
				ExTraceGlobals.RightsManagementTracer.TraceError<string, ExchangeConfigurationException>((long)this.objHashCode, "Failed to protect message '{0}'. ExchangeConfigurationException Exception: {1}", this.messageId, ex4);
				ex = ex4;
			}
			catch (TextConvertersException ex5)
			{
				ExTraceGlobals.RightsManagementTracer.TraceError<string, TextConvertersException>((long)this.objHashCode, "Failed to protect message '{0}'. TextConvertersException: {1}", this.messageId, ex5);
				ex = new MessageConversionException(Strings.TextConvertersFailed, ex5, false);
			}
			catch (StorageTransientException ex6)
			{
				ExTraceGlobals.RightsManagementTracer.TraceError<string, StorageTransientException>((long)this.objHashCode, "Failed to protect message '{0}'. Storage transient exception in processing embedded message: {1}", this.messageId, ex6);
				ex = new MessageConversionException(ex6.LocalizedString, ex6, true);
			}
			catch (StoragePermanentException ex7)
			{
				ExTraceGlobals.RightsManagementTracer.TraceError<string, StoragePermanentException>((long)this.objHashCode, "Failed to protect message '{0}'. Storage permanent exception in processing embedded message: {1}", this.messageId, ex7);
				ex = new MessageConversionException(ex7.LocalizedString, ex7, false);
			}
			catch (MessageConversionException ex8)
			{
				ExTraceGlobals.RightsManagementTracer.TraceError<string, MessageConversionException>((long)this.objHashCode, "Failed to protect message '{0}'. MessageConversionException: {1}", this.messageId, ex8);
				ex = ex8;
			}
			finally
			{
				if (ex != null)
				{
					this.BreadCrumbsDrop(State.EncryptionFailed);
				}
			}
			originalAsyncResult.InvokeCallback(new AsyncOperationResult<EmailMessage>(data, ex));
		}

		// Token: 0x06002DAE RID: 11694 RVA: 0x000B7A04 File Offset: 0x000B5C04
		private EmailMessage RightsProtectMessage(RmsTemplate template)
		{
			string text;
			CultureInfo defaultCulture;
			Encoding encoding;
			if (!Utils.TryGetCultureInfoAndEncoding(this.mailItem.Message, out text, out defaultCulture, out encoding) || defaultCulture == null)
			{
				ExTraceGlobals.RightsManagementTracer.TraceError<string>((long)this.objHashCode, "RightsProtectMessage failed <== Body.CharsetName is unsupported: {0}", this.mailItem.Message.Body.CharsetName);
				throw new MessageConversionException(Strings.InvalidCharset(this.mailItem.Message.Body.CharsetName), false);
			}
			defaultCulture = CultureProcessor.Instance.DefaultCulture;
			EmailMessage result;
			this.CreateProtectedMessage(defaultCulture, encoding, template, out result);
			return result;
		}

		// Token: 0x06002DAF RID: 11695 RVA: 0x000B7A90 File Offset: 0x000B5C90
		private void CreateProtectedMessage(CultureInfo cultureInfo, Encoding encoding, RmsTemplate template, out EmailMessage protectedMessage)
		{
			protectedMessage = null;
			ReadOnlyCollection<string> readOnlyCollection = null;
			if (!this.isReEncryption && template != null)
			{
				readOnlyCollection = Utils.GetRecipientEmailAddresses(this.mailItem);
				if (readOnlyCollection == null || readOnlyCollection.Count == 0)
				{
					ExTraceGlobals.RightsManagementTracer.TraceDebug<string>((long)this.objHashCode, "Message {0} has no recipient", this.messageId);
					return;
				}
			}
			protectedMessage = EmailMessage.Create(Microsoft.Exchange.Data.Transport.Email.BodyFormat.Text, false, Charset.UTF8.Name);
			Attachment attachment = protectedMessage.Attachments.Add("message.rpmsg", "application/x-microsoft-rpmsg-message");
			Stream stream = null;
			try
			{
				stream = (this.isReEncryption ? this.GenerateRightsProtectedStream(encoding) : this.GenerateRightsProtectedStream(template, readOnlyCollection, encoding));
				MimePart mimePart = attachment.MimePart;
				mimePart.SetContentStream(ContentTransferEncoding.Base64, stream, CachingMode.SourceTakeOwnership);
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
				}
			}
			Utils.SetProtectedContentClass(protectedMessage);
			Utils.SetBodyContent(cultureInfo, protectedMessage.Body);
			Utils.CopyHeadersDuringEncryption(this.mailItem.Message, protectedMessage);
		}

		// Token: 0x06002DB0 RID: 11696 RVA: 0x000B7B80 File Offset: 0x000B5D80
		private void BreadCrumbsDrop(object state)
		{
			if (this.breadcrumbs != null)
			{
				this.breadcrumbs.Drop(state.ToString());
			}
		}

		// Token: 0x06002DB1 RID: 11697 RVA: 0x000B7B9C File Offset: 0x000B5D9C
		private void SaveContext(object state)
		{
			LazyAsyncResult lazyAsyncResult = state as LazyAsyncResult;
			if (lazyAsyncResult != null)
			{
				AgentAsyncState agentAsyncState = lazyAsyncResult.AsyncState as AgentAsyncState;
				if (agentAsyncState != null)
				{
					agentAsyncState.Resume();
				}
			}
		}

		// Token: 0x06002DB2 RID: 11698 RVA: 0x000B7BC8 File Offset: 0x000B5DC8
		private RmsClientManagerContext CreateRmsClientContext()
		{
			return new RmsClientManagerContext(this.orgId, RmsClientManagerContext.ContextId.MessageId, this.messageId, this.mailItem.ADRecipientCache, new RmsLatencyTracker(this.mailItem.LatencyTracker), this.publishingLicense)
			{
				SystemProbeId = this.mailItem.SystemProbeId
			};
		}

		// Token: 0x06002DB3 RID: 11699 RVA: 0x000B7C1C File Offset: 0x000B5E1C
		private RmsClientManagerContext CreateRmsClientContext(Guid externalDirectoryOrgId)
		{
			return new RmsClientManagerContext(this.orgId, RmsClientManagerContext.ContextId.MessageId, this.messageId, this.mailItem.ADRecipientCache, new RmsLatencyTracker(this.mailItem.LatencyTracker), this.publishingLicense, externalDirectoryOrgId)
			{
				SystemProbeId = this.mailItem.SystemProbeId
			};
		}

		// Token: 0x040016BF RID: 5823
		private readonly OrganizationId orgId;

		// Token: 0x040016C0 RID: 5824
		private readonly IReadOnlyMailItem mailItem;

		// Token: 0x040016C1 RID: 5825
		private readonly string messageId;

		// Token: 0x040016C2 RID: 5826
		private readonly Guid templateId;

		// Token: 0x040016C3 RID: 5827
		private readonly Breadcrumbs<string> breadcrumbs;

		// Token: 0x040016C4 RID: 5828
		private readonly string publishingLicense;

		// Token: 0x040016C5 RID: 5829
		private readonly string useLicense;

		// Token: 0x040016C6 RID: 5830
		private readonly int objHashCode;

		// Token: 0x040016C7 RID: 5831
		private readonly bool isReEncryption;

		// Token: 0x040016C8 RID: 5832
		private readonly Uri licenseUri;

		// Token: 0x040016C9 RID: 5833
		private readonly RmsClientManagerContext rmsContext;

		// Token: 0x040016CA RID: 5834
		private DisposableTenantLicensePair tenantLicenses;

		// Token: 0x040016CB RID: 5835
		private DisposeTracker disposeTracker;

		// Token: 0x040016CC RID: 5836
		private bool disposed;
	}
}
