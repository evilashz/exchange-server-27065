using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000149 RID: 329
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class IrmLicensingManager : IPendingRequestNotifier
	{
		// Token: 0x06000B39 RID: 2873 RVA: 0x0004F604 File Offset: 0x0004D804
		internal IrmLicensingManager(UserContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (userContext.PendingRequestManager == null)
			{
				throw new ArgumentNullException("userContext.PendingRequestManager");
			}
			userContext.PendingRequestManager.AddPendingRequestNotifier(this);
			this.userContext = userContext;
			this.licensingResponseQueue = new IrmLicensingManager.LicensingResponseQueue();
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000B3A RID: 2874 RVA: 0x0004F658 File Offset: 0x0004D858
		// (remove) Token: 0x06000B3B RID: 2875 RVA: 0x0004F690 File Offset: 0x0004D890
		public event DataAvailableEventHandler DataAvailable;

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x0004F6C5 File Offset: 0x0004D8C5
		public bool ShouldThrottle
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0004F6C8 File Offset: 0x0004D8C8
		public string ReadDataAndResetState()
		{
			return this.licensingResponseQueue.DequeueAllAndGetPendingRequestPayload();
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0004F6D5 File Offset: 0x0004D8D5
		public void ConnectionAliveTimer()
		{
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0004F6D8 File Offset: 0x0004D8D8
		internal void AsyncAcquireLicenses(OrganizationId organizationId, OwaStoreObjectId messageId, string publishLicense, string userSmtpAddress, SecurityIdentifier userSid, RecipientTypeDetails userType, string requestCorrelator)
		{
			if (organizationId == null)
			{
				throw new ArgumentNullException("organizationId");
			}
			if (messageId == null)
			{
				throw new ArgumentNullException("messageId");
			}
			if (string.IsNullOrEmpty(publishLicense))
			{
				throw new ArgumentNullException("publishLicense");
			}
			if (string.IsNullOrEmpty(userSmtpAddress))
			{
				throw new ArgumentNullException("userSmtpAddress");
			}
			if (userSid == null)
			{
				throw new ArgumentNullException("userSid");
			}
			if (string.IsNullOrEmpty("requestCorrelator"))
			{
				throw new ArgumentNullException("requestCorrelator");
			}
			this.licensingResponseQueue.InitializeErrorIconUrl(this.userContext.GetThemeFileUrl(ThemeFileId.Error));
			this.userCulture = this.userContext.UserCulture;
			this.licensingResponseQueue.InitializeUserCulture(this.userContext.UserCulture);
			try
			{
				IrmLicensingManager.Tracer.TraceDebug((long)this.GetHashCode(), "AsyncAcquireLicenses: issuing async license req. orgId: {0}; msgId: {1}; userId: {2}; correlator: {3}", new object[]
				{
					organizationId,
					messageId,
					userSmtpAddress,
					requestCorrelator
				});
				RmsClientManager.BeginAcquireUseLicenseAndUsageRights(new RmsClientManagerContext(organizationId, RmsClientManagerContext.ContextId.MessageId, messageId.ToString(), publishLicense), publishLicense, userSmtpAddress, userSid, userType, new AsyncCallback(this.AcquireUseLicenseAndUsageRightsCallback), new IrmLicensingManager.AsyncState(messageId, requestCorrelator));
			}
			catch (ExchangeConfigurationException arg)
			{
				IrmLicensingManager.Tracer.TraceError<string, ExchangeConfigurationException>((long)this.GetHashCode(), "AsyncAcquireLicenses: exception at BeginAcquireUseLicenseAndUsageRights.  Correlator: {0}; Exception: {1}", requestCorrelator, arg);
				this.HandleExchangeConfigurationException(messageId, requestCorrelator);
			}
			catch (RightsManagementException ex)
			{
				IrmLicensingManager.Tracer.TraceError<string, RightsManagementException>((long)this.GetHashCode(), "AsyncAcquireLicenses: exception at BeginAcquireUseLicenseAndUsageRights.  Correlator: {0}; Exception: {1}", requestCorrelator, ex);
				this.HandleRightsManagementException(ex, messageId, requestCorrelator);
			}
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0004F864 File Offset: 0x0004DA64
		private void AcquireUseLicenseAndUsageRightsCallback(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (asyncResult.AsyncState == null)
			{
				IrmLicensingManager.Tracer.TraceError((long)this.GetHashCode(), "AcquireLicenseAndRightsCB: asyncResult.AsyncState is null.");
				throw new InvalidOperationException("asyncResult.AsyncState must NOT be null.");
			}
			IrmLicensingManager.AsyncState asyncState = (IrmLicensingManager.AsyncState)asyncResult.AsyncState;
			try
			{
				IrmLicensingManager.Tracer.TraceDebug<OwaStoreObjectId>((long)this.GetHashCode(), "AcquireLicenseAndRightsCB: calling EndAcquireUseLicenseAndUsageRights to get results for message {0}", asyncState.MessageId);
				UseLicenseAndUsageRights useLicenseAndUsageRights = RmsClientManager.EndAcquireUseLicenseAndUsageRights(asyncResult);
				if (string.IsNullOrEmpty(useLicenseAndUsageRights.UseLicense))
				{
					IrmLicensingManager.Tracer.TraceError((long)this.GetHashCode(), "AcquireLicenseAndRightsCB: Use license is null/empty");
					throw new InvalidOperationException("Use license must NOT be null/empty");
				}
				if (this.userContext.State != UserContextState.Active)
				{
					IrmLicensingManager.Tracer.TraceDebug((long)this.GetHashCode(), "AcquireLicenseAndRightsCB: User context is no longer active.  Bailing out.");
				}
				else
				{
					SafeRightsManagementHandle safeRightsManagementHandle = null;
					try
					{
						RmsClientManager.BindUseLicenseForDecryption(new RmsClientManagerContext(useLicenseAndUsageRights.OrganizationId, RmsClientManagerContext.ContextId.MessageId, asyncState.MessageId.ToString(), null), useLicenseAndUsageRights.LicensingUri, useLicenseAndUsageRights.UseLicense, useLicenseAndUsageRights.PublishingLicense, out safeRightsManagementHandle);
					}
					finally
					{
						if (safeRightsManagementHandle != null)
						{
							safeRightsManagementHandle.Close();
						}
					}
					bool flag = false;
					try
					{
						this.userContext.Lock();
						flag = true;
						using (Item item = Item.Bind(asyncState.MessageId.GetSession(this.userContext), asyncState.MessageId.StoreObjectId, ItemBindOption.None))
						{
							MessageItem messageItem = item as MessageItem;
							if (messageItem == null)
							{
								IrmLicensingManager.Tracer.TraceError((long)this.GetHashCode(), "AcquireLicenseAndRightsCB: bound item is not a message.  Ignoring it.");
							}
							else
							{
								messageItem.OpenAsReadWrite();
								messageItem[MessageItemSchema.DRMRights] = useLicenseAndUsageRights.UsageRights;
								messageItem[MessageItemSchema.DRMExpiryTime] = useLicenseAndUsageRights.ExpiryTime;
								if (!DrmClientUtils.IsCachingOfLicenseDisabled(useLicenseAndUsageRights.UseLicense))
								{
									using (Stream stream = messageItem.OpenPropertyStream(MessageItemSchema.DRMServerLicenseCompressed, PropertyOpenMode.Create))
									{
										DrmEmailCompression.CompressUseLicense(useLicenseAndUsageRights.UseLicense, stream);
									}
								}
								messageItem[MessageItemSchema.DRMPropsSignature] = useLicenseAndUsageRights.DRMPropsSignature;
								RightsManagedMessageItem rightsManagedMessageItem = messageItem as RightsManagedMessageItem;
								if (rightsManagedMessageItem != null && rightsManagedMessageItem.IsRestricted && !rightsManagedMessageItem.TryDecode(Utilities.CreateOutboundConversionOptions(this.userContext), false).Failed)
								{
									bool flag2 = false;
									foreach (AttachmentHandle attachmentHandle in rightsManagedMessageItem.ProtectedAttachmentCollection)
									{
										if (!attachmentHandle.IsInline)
										{
											flag2 = true;
											break;
										}
									}
									messageItem[MessageItemSchema.AllAttachmentsHidden] = !flag2;
									rightsManagedMessageItem.AbandonChangesOnProtectedData();
								}
								messageItem.Save(SaveMode.ResolveConflicts);
								if (DrmClientUtils.IsCachingOfLicenseDisabled(useLicenseAndUsageRights.UseLicense))
								{
									this.EnqueueLicensingError(asyncState.MessageId, SanitizedHtmlString.FromStringId(-1616549110, this.userCulture), asyncState.RequestCorrelator);
								}
								else
								{
									this.EnqueueLicenseAcquired(asyncState.MessageId, asyncState.RequestCorrelator);
								}
							}
						}
					}
					finally
					{
						if (this.userContext.LockedByCurrentThread() && flag)
						{
							this.userContext.Unlock();
						}
					}
				}
			}
			catch (OwaLockTimeoutException arg)
			{
				IrmLicensingManager.Tracer.TraceError<OwaLockTimeoutException>((long)this.GetHashCode(), "AcquireLicenseAndRightsCB: timed-out at acquiring user context lock.  Exception: {0}", arg);
				this.EnqueueLicensingError(asyncState.MessageId, new SanitizedHtmlString(LocalizedStrings.GetNonEncoded(858913858)), asyncState.RequestCorrelator);
			}
			catch (ExchangeConfigurationException arg2)
			{
				IrmLicensingManager.Tracer.TraceError<ExchangeConfigurationException>((long)this.GetHashCode(), "AcquireLicenseAndRightsCB: caught exception.  Exception: {0}", arg2);
				this.HandleExchangeConfigurationException(asyncState.MessageId, asyncState.RequestCorrelator);
			}
			catch (RightsManagementException ex)
			{
				IrmLicensingManager.Tracer.TraceError<RightsManagementException>((long)this.GetHashCode(), "AcquireLicenseAndRightsCB: caught exception.  Exception: {0}", ex);
				this.HandleRightsManagementException(ex, asyncState.MessageId, asyncState.RequestCorrelator);
			}
			catch (StoragePermanentException ex2)
			{
				IrmLicensingManager.Tracer.TraceError<StoragePermanentException>((long)this.GetHashCode(), "AcquireLicenseAndRightsCB: caught exception.  Exception: {0}", ex2);
				this.EnqueueLicensingError(asyncState.MessageId, SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(994351595, this.userCulture), new object[]
				{
					ex2.Message
				}), asyncState.RequestCorrelator);
			}
			catch (TransientException arg3)
			{
				IrmLicensingManager.Tracer.TraceError<TransientException>((long)this.GetHashCode(), "AcquireLicenseAndRightsCB: caught exception.  Exception: {0}", arg3);
				this.EnqueueLicensingError(asyncState.MessageId, new SanitizedHtmlString(LocalizedStrings.GetNonEncoded(858913858)), asyncState.RequestCorrelator);
			}
			catch (Exception ex3)
			{
				IrmLicensingManager.Tracer.TraceError<Exception>((long)this.GetHashCode(), "AcquireLicenseAndRightsCB: caught exception.  Exception: {0}", ex3);
				if (Globals.SendWatsonReports)
				{
					IrmLicensingManager.Tracer.TraceError((long)this.GetHashCode(), "AcquireLicenseAndRightsCB: sending watson report...");
					string data = string.Format("OWA version: {0}; Message Id: {1}", Globals.ApplicationVersion, asyncState.MessageId.ToBase64String());
					ExWatson.AddExtraData(data);
					ExWatson.SendReport(ex3, ReportOptions.None, null);
					IrmLicensingManager.Tracer.TraceError((long)this.GetHashCode(), "AcquireLicenseAndRightsCB: watson report has been sent.");
				}
			}
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0004FE1C File Offset: 0x0004E01C
		private void EnqueueLicenseAcquired(OwaStoreObjectId messageId, string requestCorrelator)
		{
			if (this.licensingResponseQueue.EnqueueLicenseAcquired(messageId, requestCorrelator))
			{
				this.DataAvailable(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0004FE3E File Offset: 0x0004E03E
		private void EnqueueLicensingError(OwaStoreObjectId messageId, SanitizedHtmlString requestStatusMessage, string requestCorrelator)
		{
			if (this.licensingResponseQueue.EnqueueLicensingError(messageId, requestStatusMessage, requestCorrelator))
			{
				this.DataAvailable(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0004FE61 File Offset: 0x0004E061
		private void HandleExchangeConfigurationException(OwaStoreObjectId messageId, string correlator)
		{
			this.EnqueueLicensingError(messageId, new SanitizedHtmlString(LocalizedStrings.GetNonEncoded(858913858)), correlator);
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0004FE7C File Offset: 0x0004E07C
		private void HandleRightsManagementException(RightsManagementException e, OwaStoreObjectId messageId, string correlator)
		{
			RightsManagementFailureCode failureCode = e.FailureCode;
			if (failureCode <= RightsManagementFailureCode.PreLicenseAcquisitionFailed)
			{
				if (failureCode == RightsManagementFailureCode.UserRightNotGranted)
				{
					this.EnqueueLicensingError(messageId, SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(-1796455575, this.userCulture), new object[]
					{
						string.Empty
					}), correlator);
					return;
				}
				if (failureCode != RightsManagementFailureCode.PreLicenseAcquisitionFailed)
				{
					goto IL_15E;
				}
			}
			else
			{
				switch (failureCode)
				{
				case RightsManagementFailureCode.FailedToExtractTargetUriFromMex:
				case RightsManagementFailureCode.FailedToDownloadMexData:
					this.EnqueueLicensingError(messageId, SanitizedHtmlString.FromStringId(1314141112, this.userCulture), correlator);
					return;
				case RightsManagementFailureCode.GetServerInfoFailed:
					goto IL_15E;
				case RightsManagementFailureCode.InternalLicensingDisabled:
					this.EnqueueLicensingError(messageId, SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(1049269714, this.userCulture), new object[]
					{
						Utilities.GetOfficeDownloadAnchor(Microsoft.Exchange.Data.Storage.BodyFormat.TextHtml, this.userCulture)
					}), correlator);
					return;
				case RightsManagementFailureCode.ExternalLicensingDisabled:
					this.EnqueueLicensingError(messageId, SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(1397740097, this.userCulture), new object[]
					{
						Utilities.GetOfficeDownloadAnchor(Microsoft.Exchange.Data.Storage.BodyFormat.TextHtml, this.userCulture)
					}), correlator);
					return;
				default:
					switch (failureCode)
					{
					case RightsManagementFailureCode.ServerRightNotGranted:
						this.EnqueueLicensingError(messageId, SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(784482022, this.userCulture), new object[]
						{
							string.Empty
						}), correlator);
						return;
					case RightsManagementFailureCode.InvalidLicensee:
						break;
					default:
						goto IL_15E;
					}
					break;
				}
			}
			this.EnqueueLicensingError(messageId, SanitizedHtmlString.FromStringId(-1489754529, this.userCulture), correlator);
			return;
			IL_15E:
			SanitizingStringBuilder<OwaHtml> sanitizingStringBuilder = new SanitizingStringBuilder<OwaHtml>(LocalizedStrings.GetNonEncoded(360598592));
			sanitizingStringBuilder.Append("<br>");
			sanitizingStringBuilder.AppendFormat(LocalizedStrings.GetHtmlEncoded(1633606253, this.userCulture), new object[]
			{
				e.Message
			});
			this.EnqueueLicensingError(messageId, sanitizingStringBuilder.ToSanitizedString<SanitizedHtmlString>(), correlator);
		}

		// Token: 0x040007F6 RID: 2038
		private static readonly Trace Tracer = ExTraceGlobals.IrmTracer;

		// Token: 0x040007F7 RID: 2039
		private readonly UserContext userContext;

		// Token: 0x040007F8 RID: 2040
		private readonly IrmLicensingManager.LicensingResponseQueue licensingResponseQueue;

		// Token: 0x040007F9 RID: 2041
		private CultureInfo userCulture;

		// Token: 0x0200014A RID: 330
		private sealed class AsyncState
		{
			// Token: 0x06000B46 RID: 2886 RVA: 0x00050046 File Offset: 0x0004E246
			internal AsyncState(OwaStoreObjectId messageId, string correlator)
			{
				this.MessageId = messageId;
				this.RequestCorrelator = correlator;
			}

			// Token: 0x040007FB RID: 2043
			internal readonly OwaStoreObjectId MessageId;

			// Token: 0x040007FC RID: 2044
			internal readonly string RequestCorrelator;
		}

		// Token: 0x0200014B RID: 331
		private sealed class LicensingResponseQueue
		{
			// Token: 0x06000B47 RID: 2887 RVA: 0x0005005C File Offset: 0x0004E25C
			internal LicensingResponseQueue()
			{
				this.queue = new Queue<IrmLicensingManager.LicensingResponseQueue.LicensingResponse>(10);
			}

			// Token: 0x06000B48 RID: 2888 RVA: 0x0005007C File Offset: 0x0004E27C
			internal void InitializeErrorIconUrl(string errorIconUrl)
			{
				if (string.IsNullOrEmpty(errorIconUrl))
				{
					throw new ArgumentNullException("errorIconUrl");
				}
				this.errorIconUrl = errorIconUrl;
			}

			// Token: 0x06000B49 RID: 2889 RVA: 0x00050098 File Offset: 0x0004E298
			internal void InitializeUserCulture(CultureInfo userCulture)
			{
				if (userCulture == null)
				{
					throw new ArgumentNullException("userCulture");
				}
				this.userCulture = userCulture;
			}

			// Token: 0x06000B4A RID: 2890 RVA: 0x000500AF File Offset: 0x0004E2AF
			internal bool EnqueueLicenseAcquired(OwaStoreObjectId messageId, string requestCorrelator)
			{
				return this.Enqueue(new IrmLicensingManager.LicensingResponseQueue.LicensingResponse(messageId, false, SanitizedHtmlString.Empty, requestCorrelator));
			}

			// Token: 0x06000B4B RID: 2891 RVA: 0x000500C4 File Offset: 0x0004E2C4
			internal bool EnqueueLicensingError(OwaStoreObjectId messageId, SanitizedHtmlString requestStatusMessage, string requestCorrelator)
			{
				return this.Enqueue(new IrmLicensingManager.LicensingResponseQueue.LicensingResponse(messageId, true, requestStatusMessage, requestCorrelator));
			}

			// Token: 0x06000B4C RID: 2892 RVA: 0x000500D8 File Offset: 0x0004E2D8
			internal string DequeueAllAndGetPendingRequestPayload()
			{
				Queue<IrmLicensingManager.LicensingResponseQueue.LicensingResponse> queue;
				lock (this.syncRoot)
				{
					queue = this.queue;
					this.queue = new Queue<IrmLicensingManager.LicensingResponseQueue.LicensingResponse>();
				}
				if (queue.Count <= 0)
				{
					return string.Empty;
				}
				StringBuilder stringBuilder = new StringBuilder(128 * queue.Count);
				stringBuilder.Append("processIrmLicenseResponses([");
				bool flag2 = true;
				while (queue.Count > 0)
				{
					IrmLicensingManager.LicensingResponseQueue.LicensingResponse licensingResponse = queue.Dequeue();
					if (!flag2)
					{
						stringBuilder.Append(',');
					}
					stringBuilder.Append(licensingResponse.GetJavascriptEncoded(this.errorIconUrl, this.userCulture));
					flag2 = false;
				}
				stringBuilder.Append("]);");
				return stringBuilder.ToString();
			}

			// Token: 0x06000B4D RID: 2893 RVA: 0x000501A4 File Offset: 0x0004E3A4
			private bool Enqueue(IrmLicensingManager.LicensingResponseQueue.LicensingResponse response)
			{
				bool result;
				lock (this.syncRoot)
				{
					if (this.queue.Count >= 10)
					{
						this.queue.Dequeue();
					}
					this.queue.Enqueue(response);
					result = (this.queue.Count == 1);
				}
				return result;
			}

			// Token: 0x040007FD RID: 2045
			private const int MaximumQueuedResponses = 10;

			// Token: 0x040007FE RID: 2046
			private readonly object syncRoot = new object();

			// Token: 0x040007FF RID: 2047
			private Queue<IrmLicensingManager.LicensingResponseQueue.LicensingResponse> queue;

			// Token: 0x04000800 RID: 2048
			private string errorIconUrl;

			// Token: 0x04000801 RID: 2049
			private CultureInfo userCulture;

			// Token: 0x0200014C RID: 332
			private struct LicensingResponse
			{
				// Token: 0x06000B4E RID: 2894 RVA: 0x00050218 File Offset: 0x0004E418
				internal LicensingResponse(OwaStoreObjectId messageId, bool failure, SanitizedHtmlString requestStatusMessage, string requestCorrelator)
				{
					this.messageId = messageId;
					this.failure = failure;
					this.requestStatusMessage = requestStatusMessage;
					this.requestCorrelator = requestCorrelator;
				}

				// Token: 0x06000B4F RID: 2895 RVA: 0x00050238 File Offset: 0x0004E438
				internal string GetJavascriptEncoded(string errorIconUrl, CultureInfo userCulture)
				{
					return string.Format(CultureInfo.InvariantCulture, "{{ 'sMessageId' : '{0}', 'sReqCorrelator' : '{1}', 'fSuccess' : {2}, 'sErrorBody' : '{3}' }}", new object[]
					{
						Utilities.JavascriptEncode(this.messageId.ToString()),
						Utilities.JavascriptEncode(this.requestCorrelator),
						this.failure ? 0 : 1,
						Utilities.JavascriptEncode(this.GetErrorHtml(errorIconUrl, userCulture))
					});
				}

				// Token: 0x06000B50 RID: 2896 RVA: 0x000502A4 File Offset: 0x0004E4A4
				private SanitizedHtmlString GetErrorHtml(string errorIconUrl, CultureInfo userCulture)
				{
					if (userCulture == null)
					{
						throw new ArgumentNullException("userCulture");
					}
					if (!this.failure)
					{
						return SanitizedHtmlString.Empty;
					}
					SanitizingStringBuilder<OwaHtml> sanitizingStringBuilder = new SanitizingStringBuilder<OwaHtml>();
					sanitizingStringBuilder.AppendFormat(CultureInfo.InvariantCulture, "<font face=\"{0}\" size=\"2\">", new object[]
					{
						Utilities.GetDefaultFontName(userCulture)
					});
					sanitizingStringBuilder.AppendFormat(CultureInfo.InvariantCulture, "<img src=\"{0}\">&nbsp;{1}", new object[]
					{
						errorIconUrl,
						this.requestStatusMessage
					});
					sanitizingStringBuilder.Append("</font>");
					return sanitizingStringBuilder.ToSanitizedString<SanitizedHtmlString>();
				}

				// Token: 0x04000802 RID: 2050
				private readonly OwaStoreObjectId messageId;

				// Token: 0x04000803 RID: 2051
				private readonly bool failure;

				// Token: 0x04000804 RID: 2052
				private readonly SanitizedHtmlString requestStatusMessage;

				// Token: 0x04000805 RID: 2053
				private readonly string requestCorrelator;
			}
		}
	}
}
