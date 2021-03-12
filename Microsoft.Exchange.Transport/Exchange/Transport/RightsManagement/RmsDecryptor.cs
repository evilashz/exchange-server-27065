using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Transport.RightsManagement
{
	// Token: 0x020003E8 RID: 1000
	internal class RmsDecryptor
	{
		// Token: 0x06002D8E RID: 11662 RVA: 0x000B68BC File Offset: 0x000B4ABC
		public static DrmEmailMessageContainer DrmEmailMessageContainerFromMessage(EmailMessage rmsMessage)
		{
			ArgumentValidator.ThrowIfNull("rmsMessage", rmsMessage);
			ArgumentValidator.ThrowIfNull("rmsMessage.Attachments", rmsMessage.Attachments);
			Attachment attachment = rmsMessage.Attachments[0];
			Stream stream = null;
			DrmEmailMessageContainer result;
			try
			{
				if (!attachment.TryGetContentReadStream(out stream) || stream == null)
				{
					throw new InvalidOperationException("Failed To read content from the protected attachment");
				}
				DrmEmailMessageContainer drmEmailMessageContainer = new DrmEmailMessageContainer();
				drmEmailMessageContainer.Load(stream, (object param0) => Streams.CreateTemporaryStorageStream());
				result = drmEmailMessageContainer;
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
				}
			}
			return result;
		}

		// Token: 0x06002D8F RID: 11663 RVA: 0x000B6954 File Offset: 0x000B4B54
		public RmsDecryptor(RmsClientManagerContext context, EmailMessage rmsMessage, DrmEmailMessageContainer drmMsgContainer, string recipientAddress, string useLicense, OutboundConversionOptions contentConversionOption, Breadcrumbs<string> breadcrumbs, bool verifyExtractRights, bool verifyRightsForReEncryption, bool copyHeaders = true, bool convertToTNEF = true)
		{
			ArgumentValidator.ThrowIfNull("context", context);
			ArgumentValidator.ThrowIfNull("rmsMessage", rmsMessage);
			ArgumentValidator.ThrowIfNull("drmMsgContainer", drmMsgContainer);
			ArgumentValidator.ThrowIfNull("contentConversionOption", contentConversionOption);
			ArgumentValidator.ThrowIfNull("rmsMessage.Attachments", rmsMessage.Attachments);
			if (rmsMessage.Attachments.Count == 0)
			{
				throw new ArgumentException("rmsMessage.Attachments.Count");
			}
			this.recipientAddress = recipientAddress;
			this.breadcrumbs = breadcrumbs;
			this.orgId = context.OrgId;
			this.contentConversionOption = contentConversionOption;
			this.rmsMessage = rmsMessage;
			this.messageId = rmsMessage.MessageId;
			this.useLicense = useLicense;
			this.verifyExtractRights = verifyExtractRights;
			this.verifyRightsForReEncryption = verifyRightsForReEncryption;
			this.objHashCode = this.GetHashCode();
			this.rmsContext = context;
			this.drmMsgContainer = drmMsgContainer;
			this.copyHeaders = copyHeaders;
			this.convertToTNEF = convertToTNEF;
			RmsClientManager.GetLicensingUri(this.orgId, this.drmMsgContainer.PublishLicense, out this.licenseUri, out this.publishLicenseAsXmlNodes, out this.isInternalRmsLicensingServer);
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x000B6A5D File Offset: 0x000B4C5D
		public Task<AsyncOperationResult<DecryptionResultData>> DecryptAsync()
		{
			return Task.Factory.FromAsync<AsyncOperationResult<DecryptionResultData>>(new Func<AsyncCallback, object, IAsyncResult>(this.BeginDecrypt), new Func<IAsyncResult, AsyncOperationResult<DecryptionResultData>>(this.EndDecrypt), null);
		}

		// Token: 0x06002D91 RID: 11665 RVA: 0x000B6A84 File Offset: 0x000B4C84
		public IAsyncResult BeginDecrypt(AsyncCallback asyncCallback, object state)
		{
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(null, state, asyncCallback);
			bool flag = false;
			try
			{
				if (this.breadcrumbs != null)
				{
					this.breadcrumbs.Drop(RmsDecryptor.State.BeginDecrypt.ToString());
				}
				this.BeginAcquireUseLicense(lazyAsyncResult);
				flag = true;
			}
			finally
			{
				if (!flag && this.drmMsgContainer != null)
				{
					this.drmMsgContainer.Dispose();
					this.drmMsgContainer = null;
				}
			}
			return lazyAsyncResult;
		}

		// Token: 0x06002D92 RID: 11666 RVA: 0x000B6AF4 File Offset: 0x000B4CF4
		public AsyncOperationResult<DecryptionResultData> EndDecrypt(IAsyncResult result)
		{
			if (this.breadcrumbs != null)
			{
				this.breadcrumbs.Drop(RmsDecryptor.State.EndDecrypt.ToString());
			}
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)result;
			return (AsyncOperationResult<DecryptionResultData>)lazyAsyncResult.InternalWaitForCompletion();
		}

		// Token: 0x06002D93 RID: 11667 RVA: 0x000B6B34 File Offset: 0x000B4D34
		private void BeginAcquireUseLicense(object state)
		{
			if (!string.IsNullOrEmpty(this.useLicense))
			{
				AsyncDecryptionOperationResult<DecryptionResultData> value = this.DecryptMessage();
				LazyAsyncResult lazyAsyncResult = state as LazyAsyncResult;
				lazyAsyncResult.InvokeCallback(value);
				return;
			}
			if (this.breadcrumbs != null)
			{
				this.breadcrumbs.Drop(RmsDecryptor.State.BeginAcquireUseLicense.ToString());
			}
			RmsClientManager.SaveContextCallback = new RmsClientManager.SaveContextOnAsyncQueryCallback(this.SaveContext);
			RmsClientManager.BeginAcquireUseLicense(this.rmsContext, this.licenseUri, this.isInternalRmsLicensingServer, this.publishLicenseAsXmlNodes, this.recipientAddress, new AsyncCallback(this.OnAcquireUseLicenseComplete), state);
		}

		// Token: 0x06002D94 RID: 11668 RVA: 0x000B6BC5 File Offset: 0x000B4DC5
		private string EndAcquireUseLicense(IAsyncResult result)
		{
			if (this.breadcrumbs != null)
			{
				this.breadcrumbs.Drop(RmsDecryptor.State.EndAcquireUseLicense.ToString());
			}
			return RmsClientManager.EndAcquireUseLicense(result).License;
		}

		// Token: 0x06002D95 RID: 11669 RVA: 0x000B6BF0 File Offset: 0x000B4DF0
		private void OnAcquireUseLicenseComplete(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (asyncResult.AsyncState == null)
			{
				throw new InvalidOperationException("asyncResult.AsyncState cannot be null here");
			}
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)asyncResult.AsyncState;
			Exception ex = null;
			try
			{
				this.useLicense = this.EndAcquireUseLicense(asyncResult);
			}
			catch (RightsManagementException ex2)
			{
				ex = ex2;
			}
			catch (ExchangeConfigurationException ex3)
			{
				ex = ex3;
			}
			AsyncDecryptionOperationResult<DecryptionResultData> value;
			if (ex != null)
			{
				ExTraceGlobals.RightsManagementTracer.TraceError<string, OrganizationId, Exception>((long)this.objHashCode, "Failed to acquire use license {0}, orgId {1}, error {2}", this.messageId, this.orgId, ex);
				if (this.drmMsgContainer != null)
				{
					this.drmMsgContainer.Dispose();
					this.drmMsgContainer = null;
				}
				DecryptionResultData data = new DecryptionResultData(null, this.useLicense, this.licenseUri);
				value = new AsyncDecryptionOperationResult<DecryptionResultData>(data, ex);
			}
			else
			{
				value = this.DecryptMessage();
			}
			lazyAsyncResult.InvokeCallback(value);
		}

		// Token: 0x06002D96 RID: 11670 RVA: 0x000B6CD4 File Offset: 0x000B4ED4
		private AsyncDecryptionOperationResult<DecryptionResultData> DecryptMessage()
		{
			Exception ex = null;
			EmailMessage emailMessage = null;
			try
			{
				if (this.breadcrumbs != null)
				{
					this.breadcrumbs.Drop(RmsDecryptor.State.DecryptMessage.ToString());
				}
				if (string.IsNullOrEmpty(this.useLicense))
				{
					throw new ArgumentNullException("this.useLicense");
				}
				if (this.verifyExtractRights || this.verifyRightsForReEncryption)
				{
					SafeRightsManagementQueryHandle safeRightsManagementQueryHandle = null;
					try
					{
						int hr = SafeNativeMethods.DRMParseUnboundLicense(this.useLicense, out safeRightsManagementQueryHandle);
						Errors.ThrowOnErrorCode(hr);
						this.VerifyHasSufficientRightToReEncrypt(safeRightsManagementQueryHandle);
						this.VerifyExtractRightsBeforeDecryptingMessage(safeRightsManagementQueryHandle);
					}
					finally
					{
						if (safeRightsManagementQueryHandle != null)
						{
							safeRightsManagementQueryHandle.Close();
						}
					}
				}
				RpMsgToMsgConverter rpMsgToMsgConverter = new RpMsgToMsgConverter(this.drmMsgContainer, this.orgId, true);
				using (DisposableTenantLicensePair disposableTenantLicensePair = RmsClientManager.AcquireTenantLicenses(this.rmsContext, this.licenseUri))
				{
					using (MessageItem messageItem = rpMsgToMsgConverter.ConvertRpmsgToMsg(null, this.useLicense, disposableTenantLicensePair.EnablingPrincipalRac))
					{
						emailMessage = Utils.ConvertMessageItemToEmailMessage(messageItem, this.contentConversionOption, this.convertToTNEF);
					}
				}
				if (this.copyHeaders)
				{
					Utils.CopyHeadersDuringDecryption(this.rmsMessage, emailMessage);
				}
			}
			catch (RightsManagementException ex2)
			{
				ex = ex2;
			}
			catch (InvalidRpmsgFormatException ex3)
			{
				ex = ex3;
			}
			catch (ExchangeConfigurationException ex4)
			{
				ex = ex4;
			}
			catch (StorageTransientException ex5)
			{
				ex = ex5;
			}
			catch (StoragePermanentException ex6)
			{
				ex = ex6;
			}
			finally
			{
				if (this.drmMsgContainer != null)
				{
					this.drmMsgContainer.Dispose();
					this.drmMsgContainer = null;
				}
				if (ex != null)
				{
					if (this.breadcrumbs != null)
					{
						this.breadcrumbs.Drop(RmsDecryptor.State.AcquireUseLicenseFailed.ToString());
					}
					ExTraceGlobals.RightsManagementTracer.TraceError<string, OrganizationId, Exception>((long)this.objHashCode, "DecryptMessage failed for message {0}, orgId {1}, error {2}", this.messageId, this.orgId, ex);
				}
			}
			DecryptionResultData data = new DecryptionResultData(emailMessage, this.useLicense, this.licenseUri);
			return new AsyncDecryptionOperationResult<DecryptionResultData>(data, ex);
		}

		// Token: 0x06002D97 RID: 11671 RVA: 0x000B6F5C File Offset: 0x000B515C
		private void VerifyHasSufficientRightToReEncrypt(SafeRightsManagementQueryHandle queryRootHandle)
		{
			if (!this.verifyRightsForReEncryption)
			{
				ExTraceGlobals.RightsManagementTracer.TraceDebug((long)this.objHashCode, "No need to VerifyHasSufficientRightToReEncrypt.");
				return;
			}
			ContentRight usageRightsFromLicense = DrmClientUtils.GetUsageRightsFromLicense(queryRootHandle);
			if ((usageRightsFromLicense & ContentRight.Owner) == ContentRight.Owner)
			{
				ExTraceGlobals.RightsManagementTracer.TraceDebug((long)this.objHashCode, "VerifyHasSufficientRightToReEncrypt verified owner right");
				return;
			}
			if ((usageRightsFromLicense & ContentRight.Edit) == ContentRight.Edit)
			{
				ExTraceGlobals.RightsManagementTracer.TraceDebug((long)this.objHashCode, "VerifyHasSufficientRightToReEncrypt verified Edit and ViewRightsData rights");
				return;
			}
			ExTraceGlobals.RightsManagementTracer.TraceError((long)this.objHashCode, "VerifyHasSufficientRightToReEncrypt doesn't detect enough rights to re-encrypt");
			throw new RightsManagementException(RightsManagementFailureCode.NotEnoughRightsToReEncrypt, Strings.NotEnoughRightsToReEncrypt((int)usageRightsFromLicense));
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x000B6FF4 File Offset: 0x000B51F4
		private void VerifyExtractRightsBeforeDecryptingMessage(SafeRightsManagementQueryHandle queryRootHandle)
		{
			if (!this.verifyExtractRights)
			{
				ExTraceGlobals.RightsManagementTracer.TraceDebug((long)this.objHashCode, "No need to VerifyExtractRightsBeforeDecryptingMessage.");
				return;
			}
			if (this.isInternalRmsLicensingServer)
			{
				ExTraceGlobals.RightsManagementTracer.TraceDebug((long)this.objHashCode, "No need to VerifyExtractRightsBeforeDecryptingMessage for internal RMS messages.");
				return;
			}
			ExTraceGlobals.RightsManagementTracer.TraceDebug<OrganizationId, Uri>((long)this.objHashCode, "Verifying if the recipient org has rights to decrypt the message to archive in clear. Org Id {0}, License Uri {1}", this.orgId, this.licenseUri);
			if (!DrmClientUtils.IsExchangeRecipientOrganizationExtractAllowed(queryRootHandle))
			{
				ExTraceGlobals.RightsManagementTracer.TraceError<OrganizationId, Uri>((long)this.objHashCode, "Extract not allowed for message protected against an external RMS server. Org Id {0}, License Uri {1}", this.orgId, this.licenseUri);
				throw new RightsManagementException(RightsManagementFailureCode.ExtractNotAllowed, Strings.ExtractNotAllowed(this.licenseUri, this.orgId.ToString()));
			}
		}

		// Token: 0x06002D99 RID: 11673 RVA: 0x000B70AC File Offset: 0x000B52AC
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

		// Token: 0x04001699 RID: 5785
		private readonly EmailMessage rmsMessage;

		// Token: 0x0400169A RID: 5786
		private readonly OutboundConversionOptions contentConversionOption;

		// Token: 0x0400169B RID: 5787
		private readonly bool convertToTNEF;

		// Token: 0x0400169C RID: 5788
		private readonly OrganizationId orgId;

		// Token: 0x0400169D RID: 5789
		private readonly Breadcrumbs<string> breadcrumbs;

		// Token: 0x0400169E RID: 5790
		private readonly string recipientAddress;

		// Token: 0x0400169F RID: 5791
		private readonly bool verifyExtractRights;

		// Token: 0x040016A0 RID: 5792
		private readonly bool verifyRightsForReEncryption;

		// Token: 0x040016A1 RID: 5793
		private readonly Uri licenseUri;

		// Token: 0x040016A2 RID: 5794
		private readonly bool isInternalRmsLicensingServer;

		// Token: 0x040016A3 RID: 5795
		private readonly string messageId;

		// Token: 0x040016A4 RID: 5796
		private readonly XmlNode[] publishLicenseAsXmlNodes;

		// Token: 0x040016A5 RID: 5797
		private readonly int objHashCode;

		// Token: 0x040016A6 RID: 5798
		private readonly RmsClientManagerContext rmsContext;

		// Token: 0x040016A7 RID: 5799
		private readonly bool copyHeaders;

		// Token: 0x040016A8 RID: 5800
		private DrmEmailMessageContainer drmMsgContainer;

		// Token: 0x040016A9 RID: 5801
		private string useLicense;

		// Token: 0x020003E9 RID: 1001
		private enum State
		{
			// Token: 0x040016AC RID: 5804
			BeginDecrypt,
			// Token: 0x040016AD RID: 5805
			BeginAcquireUseLicense,
			// Token: 0x040016AE RID: 5806
			AcquireUseLicenseFailed,
			// Token: 0x040016AF RID: 5807
			EndAcquireUseLicense,
			// Token: 0x040016B0 RID: 5808
			EndDecrypt,
			// Token: 0x040016B1 RID: 5809
			DecryptMessage,
			// Token: 0x040016B2 RID: 5810
			OnAcquireUseLicenseComplete
		}
	}
}
