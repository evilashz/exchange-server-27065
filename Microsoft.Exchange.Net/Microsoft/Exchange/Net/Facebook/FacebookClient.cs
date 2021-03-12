using System;
using System.IO;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Facebook
{
	// Token: 0x02000718 RID: 1816
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FacebookClient : DisposeTrackableBase, IFacebookClient, IDisposable
	{
		// Token: 0x06002263 RID: 8803 RVA: 0x00046D64 File Offset: 0x00044F64
		public FacebookClient(Uri endpoint)
		{
			ArgumentValidator.ThrowIfNull("Endpoint", endpoint);
			WebChannelFactory<IFacebookService> webChannelFactory = new WebChannelFactory<IFacebookService>(endpoint);
			WebHttpBinding webHttpBinding = (WebHttpBinding)webChannelFactory.Endpoint.Binding;
			webHttpBinding.MaxReceivedMessageSize = 5242880L;
			webHttpBinding.ContentTypeMapper = new FacebookClient.JsonContentMapper();
			FacebookClientMessageInspector facebookClientMessageInspector = new FacebookClientMessageInspector();
			facebookClientMessageInspector.MessageDownloaded += this.OnMessageDownload;
			webChannelFactory.Endpoint.Behaviors.Add(new FacebookClientMessageBehavior(facebookClientMessageInspector));
			this.channelFactory = webChannelFactory;
			this.service = webChannelFactory.CreateChannel();
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x00046DF2 File Offset: 0x00044FF2
		public IAsyncResult BeginGetFriends(string accessToken, string fields, string limit, string offset, AsyncCallback callback, object state)
		{
			base.CheckDisposed();
			ArgumentValidator.ThrowIfNullOrEmpty("accessToken", accessToken);
			ArgumentValidator.ThrowIfNullOrEmpty("fields", fields);
			return this.service.BeginGetFriends(accessToken, fields, limit, offset, callback, state);
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x00046E24 File Offset: 0x00045024
		public FacebookUsersList EndGetFriends(IAsyncResult asyncResult)
		{
			base.CheckDisposed();
			ArgumentValidator.ThrowIfNull("AsyncResult", asyncResult);
			return this.service.EndGetFriends(asyncResult);
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x00046E43 File Offset: 0x00045043
		public IAsyncResult BeginGetUsers(string accessToken, string userIds, string fields, AsyncCallback callback, object state)
		{
			base.CheckDisposed();
			ArgumentValidator.ThrowIfNullOrEmpty("accessToken", accessToken);
			ArgumentValidator.ThrowIfNullOrEmpty("userIds", userIds);
			ArgumentValidator.ThrowIfNullOrEmpty("fields", fields);
			return this.service.BeginGetUsers(accessToken, userIds, fields, callback, state);
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x00046E7E File Offset: 0x0004507E
		public FacebookUsersList EndGetUsers(IAsyncResult asyncResult)
		{
			base.CheckDisposed();
			ArgumentValidator.ThrowIfNull("AsyncResult", asyncResult);
			return this.service.EndGetUsers(asyncResult);
		}

		// Token: 0x06002268 RID: 8808 RVA: 0x00046EA0 File Offset: 0x000450A0
		public FacebookUser GetProfile(string accessToken, string fields)
		{
			base.CheckDisposed();
			ArgumentValidator.ThrowIfNullOrEmpty("accessToken", accessToken);
			ArgumentValidator.ThrowIfNullOrEmpty("fields", fields);
			FacebookUser profile;
			using (new OperationContextScope((IContextChannel)this.service))
			{
				profile = this.service.GetProfile(accessToken, fields);
			}
			return profile;
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x00046F08 File Offset: 0x00045108
		public void RemoveApplication(string accessToken)
		{
			base.CheckDisposed();
			ArgumentValidator.ThrowIfNullOrEmpty("accessToken", accessToken);
			using (new OperationContextScope((IContextChannel)this.service))
			{
				this.service.RemoveApplication(accessToken);
			}
		}

		// Token: 0x0600226A RID: 8810 RVA: 0x00046F60 File Offset: 0x00045160
		public FacebookImportContactsResult UploadContacts(string accessToken, bool continuous, bool async, string source, string contactsFormat, string contactsStreamContentType, Stream contacts)
		{
			base.CheckDisposed();
			ArgumentValidator.ThrowIfNullOrEmpty("accessToken", accessToken);
			ArgumentValidator.ThrowIfNullOrEmpty("source", source);
			ArgumentValidator.ThrowIfNullOrEmpty("format", contactsFormat);
			ArgumentValidator.ThrowIfNull("contacts", contacts);
			FacebookImportContactsResult result;
			using (new OperationContextScope((IContextChannel)this.service))
			{
				if (!string.IsNullOrEmpty(contactsStreamContentType))
				{
					WebOperationContext.Current.OutgoingRequest.ContentType = contactsStreamContentType;
				}
				result = this.service.ImportContacts(accessToken, contactsFormat, string.Empty, continuous, async, source, contacts);
			}
			return result;
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x00047004 File Offset: 0x00045204
		public static void AppendDiagnoseDataToException(CommunicationException exception)
		{
			ArgumentValidator.ThrowIfNull("exception", exception);
			if (exception.Data != null)
			{
				using (MemoryStream responseStreamFromException = FacebookClient.GetResponseStreamFromException(exception))
				{
					if (responseStreamFromException != null && responseStreamFromException.Length > 0L)
					{
						using (StreamReader streamReader = new StreamReader(responseStreamFromException))
						{
							exception.Data.Add("FBError.ResponseText", streamReader.ReadToEnd());
						}
					}
				}
			}
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x0004708C File Offset: 0x0004528C
		private static MemoryStream GetResponseStreamFromException(CommunicationException e)
		{
			MemoryStream memoryStream = new MemoryStream();
			WebException ex = e.InnerException as WebException;
			if (ex != null)
			{
				HttpWebResponse httpWebResponse = ex.Response as HttpWebResponse;
				if (httpWebResponse != null)
				{
					Stream responseStream = httpWebResponse.GetResponseStream();
					if (responseStream != null)
					{
						responseStream.CopyTo(memoryStream);
						memoryStream.Position = 0L;
					}
				}
			}
			return memoryStream;
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x000470D7 File Offset: 0x000452D7
		public void Cancel()
		{
			this.channelFactory.Abort();
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x000470E4 File Offset: 0x000452E4
		public void SubscribeDownloadCompletedEvent(EventHandler<DownloadCompleteEventArgs> eventHandler)
		{
			base.CheckDisposed();
			this.downloadCompleted = (EventHandler<DownloadCompleteEventArgs>)Delegate.Combine(this.downloadCompleted, eventHandler);
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x00047104 File Offset: 0x00045304
		protected virtual HttpStatusCode GetHttpWebResponseStatusCode(HttpWebRequest httpWebRequest)
		{
			HttpStatusCode statusCode;
			using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
			{
				statusCode = httpWebResponse.StatusCode;
			}
			return statusCode;
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x00047144 File Offset: 0x00045344
		internal void OnMessageDownload(object sender, FacebookMessageEventArgs eventArgs)
		{
			object obj;
			if (eventArgs != null && eventArgs.MessageTransferred != null && !eventArgs.MessageTransferred.IsEmpty && eventArgs.MessageTransferred.Properties.TryGetValue(HttpResponseMessageProperty.Name, out obj) && obj is HttpResponseMessageProperty)
			{
				string s = ((HttpResponseMessageProperty)obj).Headers[HttpResponseHeader.ContentLength];
				long num;
				if (long.TryParse(s, out num))
				{
					this.bytesDownloaded = num;
					if (this.downloadCompleted != null)
					{
						this.downloadCompleted(this, new DownloadCompleteEventArgs(this.bytesDownloaded, 0L));
					}
				}
			}
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x000471CF File Offset: 0x000453CF
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<FacebookClient>(this);
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x000471D7 File Offset: 0x000453D7
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.channelFactory.Close();
			}
		}

		// Token: 0x040020D0 RID: 8400
		private const int DefaultMaxReceivedMessageSize = 5242880;

		// Token: 0x040020D1 RID: 8401
		private const string ErrorResponseTextPropertyName = "FBError.ResponseText";

		// Token: 0x040020D2 RID: 8402
		private readonly ChannelFactory<IFacebookService> channelFactory;

		// Token: 0x040020D3 RID: 8403
		private readonly IFacebookService service;

		// Token: 0x040020D4 RID: 8404
		private long bytesDownloaded;

		// Token: 0x040020D5 RID: 8405
		private EventHandler<DownloadCompleteEventArgs> downloadCompleted;

		// Token: 0x02000719 RID: 1817
		private class JsonContentMapper : WebContentTypeMapper
		{
			// Token: 0x06002273 RID: 8819 RVA: 0x000471E7 File Offset: 0x000453E7
			public override WebContentFormat GetMessageFormatForContentType(string contentType)
			{
				return WebContentFormat.Json;
			}
		}
	}
}
