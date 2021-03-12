using System;
using System.Net;
using System.Net.Security;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.RequestDispatch;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.SoapWebClient;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000B0 RID: 176
	internal sealed class GetFolderRequest : AsyncWebRequest, IDisposable
	{
		// Token: 0x060003EC RID: 1004 RVA: 0x000104DC File Offset: 0x0000E6DC
		public GetFolderRequest(Application application, InternalClientContext clientContext, RequestType requestType, RequestLogger requestLogger, BaseQuery query, Uri url) : base(application, clientContext, requestLogger, "GetFolderRequest")
		{
			if (query.RecipientData == null || query.RecipientData.AssociatedFolderId == null)
			{
				throw new InvalidOperationException("Unable to get associated folder id");
			}
			this.query = query;
			this.url = url.OriginalString;
			this.binding = new ExchangeServiceBinding(Globals.CertificateValidationComponentId, new RemoteCertificateValidationCallback(CertificateErrorHandler.CertValidationCallback));
			this.binding.Url = url.OriginalString;
			this.binding.RequestServerVersionValue = new RequestServerVersion();
			this.binding.RequestServerVersionValue.Version = ExchangeVersionType.Exchange2007_SP1;
			Server localServer = LocalServerCache.LocalServer;
			if (localServer != null && localServer.InternetWebProxy != null)
			{
				GetFolderRequest.GetFolderRequestTracer.TraceDebug<GetFolderRequest, Uri>((long)this.GetHashCode(), "{0}: Using custom InternetWebProxy {1}", this, localServer.InternetWebProxy);
				this.binding.Proxy = new WebProxy(localServer.InternetWebProxy);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x000105C8 File Offset: 0x0000E7C8
		public BaseQuery Query
		{
			get
			{
				return this.query;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x000105D0 File Offset: 0x0000E7D0
		// (set) Token: 0x060003EF RID: 1007 RVA: 0x000105D8 File Offset: 0x0000E7D8
		public string ResultFolderId { get; private set; }

		// Token: 0x060003F0 RID: 1008 RVA: 0x000105E1 File Offset: 0x0000E7E1
		public void Dispose()
		{
			if (this.binding != null)
			{
				this.binding.Dispose();
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x000105F6 File Offset: 0x0000E7F6
		public override void Abort()
		{
			base.Abort();
			if (this.binding != null)
			{
				this.binding.Abort();
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00010611 File Offset: 0x0000E811
		protected override bool IsImpersonating
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00010614 File Offset: 0x0000E814
		protected override IAsyncResult BeginInvoke()
		{
			GetFolderType getFolder = new GetFolderType
			{
				FolderShape = GetFolderRequest.GetFolderShape,
				FolderIds = new BaseFolderIdType[]
				{
					new DistinguishedFolderIdType
					{
						Id = DistinguishedFolderIdNameType.calendar
					}
				}
			};
			this.binding.Authenticator = SoapHttpClientAuthenticator.CreateNetworkService();
			this.binding.Authenticator.AdditionalSoapHeaders.Add(new SerializedSecurityContextType
			{
				UserSid = (this.query.RecipientData.Sid ?? this.query.RecipientData.MasterAccountSid).ToString(),
				GroupSids = GetFolderRequest.SidStringAndAttributesConverter(ClientSecurityContext.DisabledEveryoneOnlySidStringAndAttributesArray),
				RestrictedGroupSids = null,
				PrimarySmtpAddress = this.query.RecipientData.PrimarySmtpAddress.ToString()
			});
			return this.binding.BeginGetFolder(getFolder, new AsyncCallback(base.Complete), null);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00010708 File Offset: 0x0000E908
		protected override void EndInvoke(IAsyncResult asyncResult)
		{
			GetFolderResponseType getFolderResponseType = this.binding.EndGetFolder(asyncResult);
			if (getFolderResponseType.ResponseMessages == null || getFolderResponseType.ResponseMessages.Items == null)
			{
				GetFolderRequest.GetFolderRequestTracer.TraceDebug((long)this.GetHashCode(), "{0}: GetFolder web request returned NULL ResponseMessages.", new object[]
				{
					TraceContext.Get()
				});
				this.SetErrorResultOnUnexpectedResponse();
				return;
			}
			FolderInfoResponseMessageType folderInfoResponseMessageType = getFolderResponseType.ResponseMessages.Items[0] as FolderInfoResponseMessageType;
			if (folderInfoResponseMessageType == null)
			{
				GetFolderRequest.GetFolderRequestTracer.TraceDebug((long)this.GetHashCode(), "{0}: GetFolder web request returned NULL FolderInfoResponseMessageType.", new object[]
				{
					TraceContext.Get()
				});
				this.SetErrorResultOnUnexpectedResponse();
				return;
			}
			if (folderInfoResponseMessageType.ResponseCode != ResponseCodeType.NoError)
			{
				GetFolderRequest.GetFolderRequestTracer.TraceDebug<object, ResponseCodeType>((long)this.GetHashCode(), "{0}: GetFolder web request returned ResponseCodeType {1}.", TraceContext.Get(), folderInfoResponseMessageType.ResponseCode);
				this.SetErrorResultOnUnexpectedResponse();
				return;
			}
			if (folderInfoResponseMessageType.Folders == null)
			{
				GetFolderRequest.GetFolderRequestTracer.TraceDebug((long)this.GetHashCode(), "{0}: GetFolder web request returned NULL Folders.", new object[]
				{
					TraceContext.Get()
				});
				this.SetErrorResultOnUnexpectedResponse();
				return;
			}
			BaseFolderType baseFolderType = folderInfoResponseMessageType.Folders[0];
			if (baseFolderType == null)
			{
				GetFolderRequest.GetFolderRequestTracer.TraceDebug<object, EmailAddress>((long)this.GetHashCode(), "{0}: GetFolder web request returned NULL FolderResponse for mailbox {1}.", TraceContext.Get(), this.query.Email);
				this.SetErrorResultOnUnexpectedResponse();
				return;
			}
			this.ResultFolderId = baseFolderType.FolderId.Id;
			GetFolderRequest.GetFolderRequestTracer.TraceDebug<object, EmailAddress, string>((long)this.GetHashCode(), "{0}: GetFolder web request returned folder id {2} for mailbox {1}.", TraceContext.Get(), this.query.Email, this.ResultFolderId);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00010888 File Offset: 0x0000EA88
		protected override void HandleException(Exception exception)
		{
			if (GetFolderRequest.GetFolderRequestTracer.IsTraceEnabled(TraceType.ErrorTrace))
			{
				GetFolderRequest.GetFolderRequestTracer.TraceError<object, Exception>((long)this.GetHashCode(), "{0}: Exception occurred while completing GetFolder web request. Exception info is {1}. ", TraceContext.Get(), exception);
			}
			GetFolderRequestProcessingException exception2 = this.GenerateException(HttpWebRequestExceptionHandler.TranslateExceptionString(exception));
			BaseQueryResult baseQueryResult = base.Application.CreateQueryResult(exception2);
			if (this.query.SetResultOnFirstCall(baseQueryResult))
			{
				GetFolderRequest.GetFolderRequestTracer.TraceError<object, EmailAddress, BaseQueryResult>((long)this.GetHashCode(), "{0}: the following result was set for query {1}: {2}", TraceContext.Get(), this.query.Email, baseQueryResult);
			}
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00010910 File Offset: 0x0000EB10
		private static SidAndAttributesType[] SidStringAndAttributesConverter(SidStringAndAttributes[] sidStringAndAttributesArray)
		{
			if (sidStringAndAttributesArray == null)
			{
				return null;
			}
			SidAndAttributesType[] array = new SidAndAttributesType[sidStringAndAttributesArray.Length];
			for (int i = 0; i < sidStringAndAttributesArray.Length; i++)
			{
				array[i] = new SidAndAttributesType
				{
					SecurityIdentifier = sidStringAndAttributesArray[i].SecurityIdentifier,
					Attributes = sidStringAndAttributesArray[i].Attributes
				};
			}
			return array;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00010960 File Offset: 0x0000EB60
		private void SetErrorResultOnUnexpectedResponse()
		{
			this.query.SetResultOnFirstCall(base.Application.CreateQueryResult(this.GenerateException(string.Empty)));
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00010984 File Offset: 0x0000EB84
		private GetFolderRequestProcessingException GenerateException(string error)
		{
			return new GetFolderRequestProcessingException(Strings.descProxyRequestProcessingError(error, this.GetHttpRequestString()));
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00010997 File Offset: 0x0000EB97
		private string GetHttpRequestString()
		{
			return string.Format("GetFolderRequest url = {0}, mailbox = {1}\n", this.url, this.query.Email);
		}

		// Token: 0x04000263 RID: 611
		private readonly ExchangeServiceBinding binding;

		// Token: 0x04000264 RID: 612
		private readonly BaseQuery query;

		// Token: 0x04000265 RID: 613
		private readonly string url;

		// Token: 0x04000266 RID: 614
		private static readonly FolderResponseShapeType GetFolderShape = new FolderResponseShapeType
		{
			BaseShape = DefaultShapeNamesType.IdOnly
		};

		// Token: 0x04000267 RID: 615
		private static readonly Trace GetFolderRequestTracer = ExTraceGlobals.GetFolderRequestTracer;
	}
}
