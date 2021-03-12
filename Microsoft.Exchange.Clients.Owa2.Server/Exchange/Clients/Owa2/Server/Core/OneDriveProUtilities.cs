using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.SharePoint.Client;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200004B RID: 75
	internal static class OneDriveProUtilities
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000803C File Offset: 0x0000623C
		internal static string UserAgentString
		{
			get
			{
				if (string.IsNullOrEmpty(OneDriveProUtilities.userAgentString))
				{
					OneDriveProUtilities.userAgentString = "Exchange/" + Globals.ApplicationVersion + "/Outlook Web App";
				}
				return OneDriveProUtilities.userAgentString;
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00008068 File Offset: 0x00006268
		internal static WebHeaderCollection GetOAuthRequestHeaders()
		{
			WebHeaderCollection webHeaderCollection = new WebHeaderCollection();
			webHeaderCollection.Add(HttpRequestHeader.Authorization, "Bearer");
			webHeaderCollection["X-RequestForceAuthentication"] = "true";
			webHeaderCollection["client-request-id"] = Guid.NewGuid().ToString();
			webHeaderCollection["return-client-request-id"] = "true";
			return webHeaderCollection;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x000080C7 File Offset: 0x000062C7
		internal static CamlQuery CreatePagedCamlPageQuery(string location, AttachmentItemsSort sort, ListItemCollectionPosition listItemCollectionPosition, int numberOfItems)
		{
			return OneDriveProUtilities.CreatePagedCamlQuery(location, sort, listItemCollectionPosition, numberOfItems, "<View>\r\n                                                    <Query>\r\n                                                        <OrderBy>\r\n                                                            <FieldRef Name='FSObjType' Ascending='FALSE' />\r\n                                                            <FieldRef Name='{0}' Ascending='{1}' />\r\n                                                        </OrderBy>\r\n                                                    </Query>\r\n                                                    <ViewFields>\r\n                                                        <FieldRef Name='ID' />\r\n                                                        <FieldRef Name='FileLeafRef' />\r\n                                                        <FieldRef Name='FSObjType' />\r\n                                                        <FieldRef Name='SortBehavior' />\r\n                                                    </ViewFields>\r\n                                                    <RowLimit>{2}</RowLimit>\r\n                                                </View>");
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000080D7 File Offset: 0x000062D7
		internal static CamlQuery CreatePagedCamlDataQuery(string location, AttachmentItemsSort sort, ListItemCollectionPosition listItemCollectionPosition, int numberOfItems)
		{
			return OneDriveProUtilities.CreatePagedCamlQuery(location, sort, listItemCollectionPosition, numberOfItems, "<View>\r\n                                                <Query>\r\n                                                    <OrderBy>\r\n                                                        <FieldRef Name='FSObjType' Ascending='FALSE' />\r\n                                                        <FieldRef Name='{0}' Ascending='{1}' />\r\n                                                    </OrderBy>\r\n                                                </Query>\r\n                                                <ViewFields>\r\n                                                    <FieldRef Name='ID' />\r\n                                                    <FieldRef Name='FileLeafRef' />\r\n                                                    <FieldRef Name='FSObjType' />\r\n                                                    <FieldRef Name='FileRef' />\r\n                                                    <FieldRef Name='File_x0020_Size' />\r\n                                                    <FieldRef Name='Modified' />\r\n                                                    <FieldRef Name='Editor' />\r\n                                                    <FieldRef Name='ItemChildCount' />\r\n                                                    <FieldRef Name='FolderChildCount' />\r\n                                                    <FieldRef Name='ProgId' />\r\n                                                </ViewFields><RowLimit>{2}</RowLimit></View>");
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000080E7 File Offset: 0x000062E7
		internal static CamlQuery CreateCamlDataQuery(string location, AttachmentItemsSort sort)
		{
			return OneDriveProUtilities.CreateCamlQuery(location, null, string.Format("<View>\r\n                                                <Query>\r\n                                                    <OrderBy>\r\n                                                        <FieldRef Name='FSObjType' Ascending='FALSE' />\r\n                                                        <FieldRef Name='{0}' Ascending='{1}' />\r\n                                                    </OrderBy>\r\n                                                </Query>\r\n                                                <ViewFields>\r\n                                                    <FieldRef Name='ID' />\r\n                                                    <FieldRef Name='FileLeafRef' />\r\n                                                    <FieldRef Name='FSObjType' />\r\n                                                    <FieldRef Name='FileRef' />\r\n                                                    <FieldRef Name='File_x0020_Size' />\r\n                                                    <FieldRef Name='Modified' />\r\n                                                    <FieldRef Name='Editor' />\r\n                                                    <FieldRef Name='ItemChildCount' />\r\n                                                    <FieldRef Name='FolderChildCount' />\r\n                                                    <FieldRef Name='ProgId' />\r\n                                                </ViewFields></View>", OneDriveProUtilities.GetSortColumn(sort.SortColumn), OneDriveProUtilities.GetSortOrder(sort.SortOrder)));
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00008150 File Offset: 0x00006350
		internal static IClientContext CreateAndConfigureClientContext(OwaIdentity identity, string url)
		{
			ICredentials oneDriveProCredentials = OneDriveProUtilities.GetOneDriveProCredentials(identity);
			IClientContext clientContext = ClientContextFactory.Create(url);
			clientContext.Credentials = oneDriveProCredentials;
			clientContext.FormDigestHandlingEnabled = false;
			clientContext.ExecutingWebRequest += delegate(object sender, WebRequestEventArgs args)
			{
				if (args != null)
				{
					args.WebRequestExecutor.RequestHeaders.Add(OneDriveProUtilities.GetOAuthRequestHeaders());
					args.WebRequestExecutor.WebRequest.PreAuthenticate = true;
					args.WebRequestExecutor.WebRequest.UserAgent = OneDriveProUtilities.UserAgentString;
				}
			};
			return clientContext;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000819D File Offset: 0x0000639D
		internal static ICredentials GetOneDriveProCredentials(OwaIdentity identity)
		{
			return OauthUtils.GetOauthCredential(identity.GetOWAMiniRecipient());
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000081AC File Offset: 0x000063AC
		internal static DownloadResult SendRestRequest(string requestMethod, string requestUri, OwaIdentity identity, Stream requestStream, DataProviderCallLogEvent logEvent, string spCallName)
		{
			DownloadResult result;
			using (HttpClient httpClient = new HttpClient())
			{
				HttpSessionConfig httpSessionConfig = new HttpSessionConfig
				{
					Method = requestMethod,
					Credentials = OauthUtils.GetOauthCredential(identity.GetOWAMiniRecipient()),
					UserAgent = OneDriveProUtilities.UserAgentString,
					RequestStream = requestStream,
					ContentType = "application/json;odata=verbose",
					PreAuthenticate = true
				};
				httpSessionConfig.Headers = OneDriveProUtilities.GetOAuthRequestHeaders();
				if (logEvent != null)
				{
					logEvent.TrackSPCallBegin();
				}
				ICancelableAsyncResult cancelableAsyncResult = httpClient.BeginDownload(new Uri(requestUri), httpSessionConfig, null, null);
				cancelableAsyncResult.AsyncWaitHandle.WaitOne();
				DownloadResult downloadResult = httpClient.EndDownload(cancelableAsyncResult);
				if (logEvent != null)
				{
					string correlationId = (downloadResult.ResponseHeaders == null) ? null : downloadResult.ResponseHeaders["SPRequestGuid"];
					logEvent.TrackSPCallEnd(spCallName, correlationId);
				}
				result = downloadResult;
			}
			return result;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00008294 File Offset: 0x00006494
		internal static bool IsDurableUrlFormat(string documentUrl)
		{
			return !string.IsNullOrEmpty(documentUrl) && documentUrl.LastIndexOf(".", StringComparison.InvariantCulture) < documentUrl.LastIndexOf("?d=", StringComparison.InvariantCulture);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000082BC File Offset: 0x000064BC
		internal static string GetWacUrl(OwaIdentity identity, string endPointUrl, string documentUrl, bool isEdit)
		{
			string arg = isEdit ? "2" : "4";
			string text = string.Format("{0}/_api/Microsoft.SharePoint.Yammer.WACAPI.GetWacToken(fileUrl=@p, wopiAction={2})?@p='{1}'", endPointUrl, documentUrl, arg);
			string result;
			using (HttpClient httpClient = new HttpClient())
			{
				OWAMiniRecipient owaminiRecipient = identity.GetOWAMiniRecipient();
				ICredentials oauthCredential = OauthUtils.GetOauthCredential(owaminiRecipient);
				WebHeaderCollection oauthRequestHeaders = OneDriveProUtilities.GetOAuthRequestHeaders();
				HttpSessionConfig sessionConfig = new HttpSessionConfig
				{
					Method = "GET",
					Credentials = oauthCredential,
					UserAgent = OneDriveProUtilities.UserAgentString,
					ContentType = "application/json;odata=verbose",
					PreAuthenticate = true,
					Headers = oauthRequestHeaders
				};
				DownloadResult downloadResult;
				try
				{
					downloadResult = OneDriveProUtilities.TryTwice(httpClient, sessionConfig, text);
				}
				catch (WebException ex)
				{
					if (!OneDriveProUtilities.IsDurableUrlFormat(documentUrl))
					{
						throw ex;
					}
					ExTraceGlobals.AttachmentHandlingTracer.TraceWarning<string>(0L, "OneDriveProUtilities.GetWacUrl Exception while trying to get wac token using durable url. : {0}", ex.StackTrace);
					documentUrl = documentUrl.Substring(0, documentUrl.LastIndexOf("?", StringComparison.InvariantCulture));
					text = string.Format("{0}/_api/Microsoft.SharePoint.Yammer.WACAPI.GetWacToken(fileUrl=@p, wopiAction={2})?@p='{1}'", endPointUrl, documentUrl, arg);
					ExTraceGlobals.AttachmentHandlingTracer.TraceWarning<string>(0L, "OneDriveProUtilities.GetWacUrl Fallback to canonical url format: {0}", text);
					OwaServerTraceLogger.AppendToLog(new TraceLogEvent("SP.GWT", null, "GetWacToken", string.Format("Error getting WAC Token fallback to canonical format:{0}", text)));
					downloadResult = OneDriveProUtilities.TryTwice(httpClient, sessionConfig, text);
				}
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(downloadResult.ResponseStream);
				string namespaceURI = "http://schemas.microsoft.com/ado/2007/08/dataservices";
				string text2 = null;
				string text3 = null;
				string text4 = null;
				foreach (object obj in xmlDocument.GetElementsByTagName("*", namespaceURI))
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (xmlNode is XmlElement)
					{
						if (text2 != null && text3 != null && text4 != null)
						{
							break;
						}
						if (string.CompareOrdinal(xmlNode.LocalName, "AppUrl") == 0)
						{
							text2 = xmlNode.InnerText;
						}
						else if (string.CompareOrdinal(xmlNode.LocalName, "AccessToken") == 0)
						{
							text3 = xmlNode.InnerText;
						}
						else if (string.CompareOrdinal(xmlNode.LocalName, "AccessTokenTtl") == 0)
						{
							text4 = xmlNode.InnerText;
						}
					}
				}
				if (text2 == null || text3 == null || text4 == null)
				{
					throw new OwaException("SharePoint's GetWacToken response is not usable.");
				}
				string text5 = isEdit ? "OwaEdit" : "OwaView";
				result = string.Format("{0}&access_token={1}&access_token_ttl={2}&sc={3}", new object[]
				{
					text2,
					text3,
					text4,
					text5
				});
			}
			return result;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00008570 File Offset: 0x00006770
		internal static void ExecuteQueryWithTraces(UserContext userContext, IClientContext context, DataProviderCallLogEvent logEvent, string spCallName)
		{
			try
			{
				if (logEvent != null)
				{
					logEvent.TrackSPCallBegin();
				}
				context.ExecuteQuery();
			}
			finally
			{
				if (logEvent != null)
				{
					logEvent.TrackSPCallEnd(spCallName, context.TraceCorrelationId);
				}
				OneDriveProUtilities.SendPendingGetNotification(userContext, new AttachmentOperationCorrelationIdNotificationPayload
				{
					CorrelationId = context.TraceCorrelationId,
					SharePointCallName = spCallName
				});
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000085D0 File Offset: 0x000067D0
		internal static IList GetDocumentsLibrary(IClientContext context, string documentLibraryName)
		{
			string text = new Uri(context.Url).LocalPath;
			if (!text.EndsWith("/"))
			{
				text += "/";
			}
			return context.Web.GetList(text + documentLibraryName);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000861C File Offset: 0x0000681C
		private static void SendPendingGetNotification(UserContext userContext, AttachmentOperationCorrelationIdNotificationPayload payload)
		{
			if (userContext.IsGroupUserContext)
			{
				return;
			}
			if (!userContext.IsDisposed)
			{
				AttachmentOperationCorrelationIdNotifier attachmentOperationCorrelationIdNotifier = new AttachmentOperationCorrelationIdNotifier(userContext, payload.SubscriptionId);
				try
				{
					attachmentOperationCorrelationIdNotifier.RegisterWithPendingRequestNotifier();
					attachmentOperationCorrelationIdNotifier.Payload = payload;
					attachmentOperationCorrelationIdNotifier.PickupData();
				}
				finally
				{
					attachmentOperationCorrelationIdNotifier.UnregisterWithPendingRequestNotifier();
				}
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00008674 File Offset: 0x00006874
		private static DownloadResult TryTwice(HttpClient httpClient, HttpSessionConfig sessionConfig, string url)
		{
			ICancelableAsyncResult cancelableAsyncResult = httpClient.BeginDownload(new Uri(url), sessionConfig, null, null);
			cancelableAsyncResult.AsyncWaitHandle.WaitOne();
			DownloadResult result = httpClient.EndDownload(cancelableAsyncResult);
			if (result.Exception != null)
			{
				if (!result.IsRetryable)
				{
					throw result.Exception;
				}
				cancelableAsyncResult = httpClient.BeginDownload(new Uri(url), sessionConfig, null, null);
				cancelableAsyncResult.AsyncWaitHandle.WaitOne();
				result = httpClient.EndDownload(cancelableAsyncResult);
				if (result.Exception != null)
				{
					throw result.Exception;
				}
			}
			return result;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000086F5 File Offset: 0x000068F5
		private static CamlQuery CreatePagedCamlQuery(string location, AttachmentItemsSort sort, ListItemCollectionPosition listItemCollectionPosition, int numberOfItems, string viewXmlFormat)
		{
			return OneDriveProUtilities.CreateCamlQuery(location, listItemCollectionPosition, string.Format(viewXmlFormat, OneDriveProUtilities.GetSortColumn(sort.SortColumn), OneDriveProUtilities.GetSortOrder(sort.SortOrder), numberOfItems));
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00008724 File Offset: 0x00006924
		private static CamlQuery CreateCamlQuery(string location, ListItemCollectionPosition listItemCollectionPosition, string viewXml)
		{
			CamlQuery camlQuery = new CamlQuery
			{
				ViewXml = viewXml
			};
			if (!string.IsNullOrEmpty(location))
			{
				camlQuery.FolderServerRelativeUrl = location;
			}
			camlQuery.ListItemCollectionPosition = listItemCollectionPosition;
			return camlQuery;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00008757 File Offset: 0x00006957
		private static string GetSortColumn(AttachmentItemsSortColumn column)
		{
			OneDriveProUtilities.EnsureColumnMap();
			return OneDriveProUtilities.columnToOneDriveProColumn[column];
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00008769 File Offset: 0x00006969
		private static string GetSortOrder(AttachmentItemsSortOrder sortOrder)
		{
			if (sortOrder != AttachmentItemsSortOrder.Ascending)
			{
				return "false";
			}
			return "true";
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000877C File Offset: 0x0000697C
		private static void EnsureColumnMap()
		{
			if (OneDriveProUtilities.columnToOneDriveProColumn == null)
			{
				lock (OneDriveProUtilities.syncRoot)
				{
					if (OneDriveProUtilities.columnToOneDriveProColumn == null)
					{
						OneDriveProUtilities.columnToOneDriveProColumn = new Dictionary<AttachmentItemsSortColumn, string>();
						OneDriveProUtilities.columnToOneDriveProColumn[AttachmentItemsSortColumn.LastModified] = "Modified";
						OneDriveProUtilities.columnToOneDriveProColumn[AttachmentItemsSortColumn.Name] = "FileLeafRef";
						OneDriveProUtilities.columnToOneDriveProColumn[AttachmentItemsSortColumn.Size] = "File_x0020_Size";
					}
				}
			}
		}

		// Token: 0x040000CF RID: 207
		internal const string Post = "POST";

		// Token: 0x040000D0 RID: 208
		internal const string Get = "GET";

		// Token: 0x040000D1 RID: 209
		internal const string SPRequestIdHeader = "SPRequestGuid";

		// Token: 0x040000D2 RID: 210
		private const string Bearer = "Bearer";

		// Token: 0x040000D3 RID: 211
		private const string XRequestForceAuthenticationHeader = "X-RequestForceAuthentication";

		// Token: 0x040000D4 RID: 212
		private const string True = "true";

		// Token: 0x040000D5 RID: 213
		private const string False = "false";

		// Token: 0x040000D6 RID: 214
		private const string ReturnClientRequestIdHeader = "return-client-request-id";

		// Token: 0x040000D7 RID: 215
		private const string CamlDataQueryStart = "<View>\r\n                                                <Query>\r\n                                                    <OrderBy>\r\n                                                        <FieldRef Name='FSObjType' Ascending='FALSE' />\r\n                                                        <FieldRef Name='{0}' Ascending='{1}' />\r\n                                                    </OrderBy>\r\n                                                </Query>\r\n                                                <ViewFields>\r\n                                                    <FieldRef Name='ID' />\r\n                                                    <FieldRef Name='FileLeafRef' />\r\n                                                    <FieldRef Name='FSObjType' />\r\n                                                    <FieldRef Name='FileRef' />\r\n                                                    <FieldRef Name='File_x0020_Size' />\r\n                                                    <FieldRef Name='Modified' />\r\n                                                    <FieldRef Name='Editor' />\r\n                                                    <FieldRef Name='ItemChildCount' />\r\n                                                    <FieldRef Name='FolderChildCount' />\r\n                                                    <FieldRef Name='ProgId' />\r\n                                                </ViewFields>";

		// Token: 0x040000D8 RID: 216
		private const string CamlDataQueryEnd = "</View>";

		// Token: 0x040000D9 RID: 217
		private const string RowLimit = "<RowLimit>{2}</RowLimit>";

		// Token: 0x040000DA RID: 218
		private const string CamlDataQuery = "<View>\r\n                                                <Query>\r\n                                                    <OrderBy>\r\n                                                        <FieldRef Name='FSObjType' Ascending='FALSE' />\r\n                                                        <FieldRef Name='{0}' Ascending='{1}' />\r\n                                                    </OrderBy>\r\n                                                </Query>\r\n                                                <ViewFields>\r\n                                                    <FieldRef Name='ID' />\r\n                                                    <FieldRef Name='FileLeafRef' />\r\n                                                    <FieldRef Name='FSObjType' />\r\n                                                    <FieldRef Name='FileRef' />\r\n                                                    <FieldRef Name='File_x0020_Size' />\r\n                                                    <FieldRef Name='Modified' />\r\n                                                    <FieldRef Name='Editor' />\r\n                                                    <FieldRef Name='ItemChildCount' />\r\n                                                    <FieldRef Name='FolderChildCount' />\r\n                                                    <FieldRef Name='ProgId' />\r\n                                                </ViewFields></View>";

		// Token: 0x040000DB RID: 219
		private const string PagedCamlDataQuery = "<View>\r\n                                                <Query>\r\n                                                    <OrderBy>\r\n                                                        <FieldRef Name='FSObjType' Ascending='FALSE' />\r\n                                                        <FieldRef Name='{0}' Ascending='{1}' />\r\n                                                    </OrderBy>\r\n                                                </Query>\r\n                                                <ViewFields>\r\n                                                    <FieldRef Name='ID' />\r\n                                                    <FieldRef Name='FileLeafRef' />\r\n                                                    <FieldRef Name='FSObjType' />\r\n                                                    <FieldRef Name='FileRef' />\r\n                                                    <FieldRef Name='File_x0020_Size' />\r\n                                                    <FieldRef Name='Modified' />\r\n                                                    <FieldRef Name='Editor' />\r\n                                                    <FieldRef Name='ItemChildCount' />\r\n                                                    <FieldRef Name='FolderChildCount' />\r\n                                                    <FieldRef Name='ProgId' />\r\n                                                </ViewFields><RowLimit>{2}</RowLimit></View>";

		// Token: 0x040000DC RID: 220
		private const string PagedCamlPagingQuery = "<View>\r\n                                                    <Query>\r\n                                                        <OrderBy>\r\n                                                            <FieldRef Name='FSObjType' Ascending='FALSE' />\r\n                                                            <FieldRef Name='{0}' Ascending='{1}' />\r\n                                                        </OrderBy>\r\n                                                    </Query>\r\n                                                    <ViewFields>\r\n                                                        <FieldRef Name='ID' />\r\n                                                        <FieldRef Name='FileLeafRef' />\r\n                                                        <FieldRef Name='FSObjType' />\r\n                                                        <FieldRef Name='SortBehavior' />\r\n                                                    </ViewFields>\r\n                                                    <RowLimit>{2}</RowLimit>\r\n                                                </View>";

		// Token: 0x040000DD RID: 221
		private const string RestContentType = "application/json;odata=verbose";

		// Token: 0x040000DE RID: 222
		private static Dictionary<AttachmentItemsSortColumn, string> columnToOneDriveProColumn;

		// Token: 0x040000DF RID: 223
		private static object syncRoot = new object();

		// Token: 0x040000E0 RID: 224
		private static string userAgentString;
	}
}
