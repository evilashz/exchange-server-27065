using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using Microsoft.Exchange.Clients.Owa2.Server.Core.OneDrive;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.Search;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Online.BOX.UI.Shell;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Sharing;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000403 RID: 1027
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[SimpleConfiguration("OWA.AttachmentDataProvider", "OneDriveProAttachmentDataProvider")]
	public class OneDriveProAttachmentDataProvider : AttachmentDataProvider, IAttachmentDataProviderChanged
	{
		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060021E0 RID: 8672 RVA: 0x0007AD04 File Offset: 0x00078F04
		// (remove) Token: 0x060021E1 RID: 8673 RVA: 0x0007AD3C File Offset: 0x00078F3C
		public event AttachmentDataProviderChangedEventHandler AttachmentDataProviderChanged;

		// Token: 0x060021E2 RID: 8674 RVA: 0x0007AD71 File Offset: 0x00078F71
		static OneDriveProAttachmentDataProvider()
		{
			OwsLogRegistry.Register(OwaApplication.GetRequestDetailsLogger.Get(ExtensibleLoggerMetadata.EventId), typeof(OneDriveProAttachmentDataProvider.LogMetadata), new Type[0]);
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x0007ADA4 File Offset: 0x00078FA4
		internal static OneDriveProAttachmentDataProvider CreateFromBpos(UserContext userContext, CallContext callContext)
		{
			bool useMockAttachmentDataProvider = AttachmentDataProviderManager.IsMockDataProviderEnabled();
			return OneDriveProAttachmentDataProvider.CreateFromBpos(userContext, callContext, useMockAttachmentDataProvider);
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x0007ADC0 File Offset: 0x00078FC0
		internal static OneDriveProAttachmentDataProvider CreateFromBpos(UserContext userContext, CallContext callContext, bool useMockAttachmentDataProvider)
		{
			if (!useMockAttachmentDataProvider && !userContext.IsBposUser)
			{
				return null;
			}
			string bposUrl;
			string displayName;
			if (useMockAttachmentDataProvider)
			{
				bposUrl = "http://localhost:1234/";
				displayName = "OneDrive — Mock";
			}
			else if (!OneDriveProAttachmentDataProvider.TryGetBposDocumentsInfoFromBox(userContext, callContext, out bposUrl, out displayName))
			{
				return null;
			}
			string endPointUrl;
			string documentLibrary;
			if (!OneDriveProAttachmentDataProvider.TryGetEndPointUrlAndDocumentLibraryFromBposUrl(userContext, bposUrl, out endPointUrl, out documentLibrary))
			{
				return null;
			}
			return new OneDriveProAttachmentDataProvider
			{
				DisplayName = displayName,
				EndPointUrl = endPointUrl,
				DocumentLibrary = documentLibrary,
				Id = Guid.NewGuid().ToString(),
				IsSystemGenerated = new bool?(true),
				LastUpdateTime = new DateTime?(DateTime.UtcNow),
				Type = AttachmentDataProviderType.OneDrivePro
			};
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x060021E5 RID: 8677 RVA: 0x0007AE6B File Offset: 0x0007906B
		// (set) Token: 0x060021E6 RID: 8678 RVA: 0x0007AE73 File Offset: 0x00079073
		[SimpleConfigurationProperty("DocumentLibrary")]
		[DataMember]
		public string DocumentLibrary { get; set; }

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x060021E7 RID: 8679 RVA: 0x0007AE7C File Offset: 0x0007907C
		// (set) Token: 0x060021E8 RID: 8680 RVA: 0x0007AE84 File Offset: 0x00079084
		[SimpleConfigurationProperty("IsSystemGenerated")]
		internal bool? IsSystemGenerated { get; set; }

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x060021E9 RID: 8681 RVA: 0x0007AE8D File Offset: 0x0007908D
		// (set) Token: 0x060021EA RID: 8682 RVA: 0x0007AE95 File Offset: 0x00079095
		[SimpleConfigurationProperty("LastUpdateTime")]
		internal DateTime? LastUpdateTime { get; set; }

		// Token: 0x060021EB RID: 8683 RVA: 0x0007AE9E File Offset: 0x0007909E
		public override string ToString()
		{
			return "OneDrivePro, Library=" + this.DocumentLibrary + ", API=" + base.EndPointUrl;
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x0007AEBC File Offset: 0x000790BC
		public override bool FileExists(string fileUrl)
		{
			UserContext userContext = UserContextManager.GetUserContext(CallContext.Current.HttpContext, CallContext.Current.EffectiveCaller, true);
			DataProviderCallLogEvent dataProviderCallLogEvent = new DataProviderCallLogEvent("OneDrive.FileExists", userContext);
			bool result;
			try
			{
				dataProviderCallLogEvent.Reset();
				using (IClientContext clientContext = OneDriveProUtilities.CreateAndConfigureClientContext(userContext.LogonIdentity, base.EndPointUrl))
				{
					if (!OneDriveProAttachmentDataProvider.IsAbsoluteLocation(fileUrl))
					{
						result = OneDriveProAttachmentDataProvider.FileExists(userContext, clientContext, fileUrl, dataProviderCallLogEvent);
					}
					else
					{
						Uri uri = new Uri(fileUrl);
						try
						{
							Uri uri2 = clientContext.WebUrlFromPageUrlDirect(uri);
							if (uri2.AbsoluteUri.Equals(new Uri(base.EndPointUrl).AbsoluteUri))
							{
								result = OneDriveProAttachmentDataProvider.FileExists(userContext, clientContext, uri.AbsolutePath, dataProviderCallLogEvent);
							}
							else
							{
								using (IClientContext clientContext2 = OneDriveProUtilities.CreateAndConfigureClientContext(userContext.LogonIdentity, uri2.AbsoluteUri))
								{
									result = OneDriveProAttachmentDataProvider.FileExists(userContext, clientContext2, uri.AbsolutePath, dataProviderCallLogEvent);
								}
							}
						}
						catch (ServerException)
						{
							OwaServerTraceLogger.AppendToLog(new TraceLogEvent("SP.FE", null, "FileExists", string.Format("Error doing fileExist check for file:{0}", fileUrl)));
							result = false;
						}
					}
				}
			}
			finally
			{
				dataProviderCallLogEvent.SetFinish();
				OwaServerTraceLogger.AppendToLog(dataProviderCallLogEvent);
			}
			return result;
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x0007B008 File Offset: 0x00079208
		internal override string GetLinkingUrl(UserContext userContext, string fileUrl, string endpointUrl, string itemId = null, string parentItemId = null)
		{
			DataProviderCallLogEvent dataProviderCallLogEvent = new DataProviderCallLogEvent("OneDrive.GetLinkingUrl", userContext);
			if (string.IsNullOrEmpty(endpointUrl))
			{
				endpointUrl = base.EndPointUrl;
			}
			try
			{
				dataProviderCallLogEvent.Reset();
				using (IClientContext clientContext = OneDriveProUtilities.CreateAndConfigureClientContext(userContext.LogonIdentity, endpointUrl))
				{
					string text = null;
					IFile file;
					if (string.IsNullOrEmpty(fileUrl) && !string.IsNullOrEmpty(itemId) && !string.IsNullOrEmpty(parentItemId))
					{
						file = clientContext.Web.Lists.GetById(new Guid(parentItemId)).GetItemById(itemId).File;
					}
					else
					{
						if (string.IsNullOrEmpty(fileUrl))
						{
							return null;
						}
						string relativeLocation = fileUrl;
						if (OneDriveProAttachmentDataProvider.IsAbsoluteLocation(fileUrl))
						{
							text = fileUrl;
							relativeLocation = new Uri(fileUrl).AbsolutePath;
						}
						file = clientContext.Web.GetFileByServerRelativeUrl(relativeLocation);
					}
					file.Load(clientContext, new Expression<Func<File, object>>[]
					{
						(File x) => x.LinkingUrl,
						(File x) => x.ServerRelativeUrl
					});
					OneDriveProUtilities.ExecuteQueryWithTraces(userContext, clientContext, dataProviderCallLogEvent, "LinkingUrl");
					if (!string.IsNullOrEmpty(file.LinkingUrl))
					{
						return file.LinkingUrl;
					}
					if (string.IsNullOrEmpty(text))
					{
						Uri baseUri = new Uri(endpointUrl);
						Uri uri = new Uri(baseUri, file.ServerRelativeUrl);
						return uri.AbsoluteUri;
					}
					return text;
				}
			}
			catch (ServerException error)
			{
				dataProviderCallLogEvent.SetError(error);
			}
			finally
			{
				dataProviderCallLogEvent.SetFinish();
				OwaServerTraceLogger.AppendToLog(dataProviderCallLogEvent);
			}
			return null;
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x0007B388 File Offset: 0x00079588
		internal override GetAttachmentDataProviderItemsResponse GetItems(AttachmentItemsPagingDetails paging, AttachmentDataProviderScope scope, MailboxSession mailboxSession)
		{
			UserContext userContext = UserContextManager.GetUserContext(CallContext.Current.HttpContext, CallContext.Current.EffectiveCaller, true);
			Func<GetAttachmentDataProviderItemsResponse> func = null;
			DataProviderCallLogEvent logEvent = null;
			string eventId = "OneDrive.GetItems";
			OneDriveProScopeType oneDriveProScopeType = OneDriveProScopeType.MyDocuments;
			if (scope != null)
			{
				OneDriveProScope oneDriveProScope = scope as OneDriveProScope;
				if (oneDriveProScope == null)
				{
					throw new ArgumentException("The AttachmentDataProviderScope must be an object of type OneDriveProScope", "scope");
				}
				oneDriveProScopeType = oneDriveProScope.Type;
			}
			switch (oneDriveProScopeType)
			{
			case OneDriveProScopeType.SharedWithMe:
				eventId = "OneDrive.GetSharedWithMeItems";
				if (string.IsNullOrEmpty(paging.Location))
				{
					func = (() => this.GetSharedWithMeItems(userContext, logEvent));
				}
				break;
			case OneDriveProScopeType.Followed:
				eventId = "OneDrive.GetFollowedItems";
				func = (() => this.GetFollowedItems(userContext));
				break;
			case OneDriveProScopeType.Groups:
				if (string.IsNullOrEmpty(paging.ItemsEndpointUrl))
				{
					OneDriveProGroupsPagingMetadata oneDriveProGroupsPagingMetadata = paging.PagingMetadata as OneDriveProGroupsPagingMetadata;
					if (oneDriveProGroupsPagingMetadata == null || string.IsNullOrEmpty(oneDriveProGroupsPagingMetadata.GroupSmtpAddress))
					{
						eventId = "OneDrive.GetGroups";
						func = (() => this.GetGroups(mailboxSession));
					}
					else
					{
						eventId = "OneDrive.GetGroupItems";
						func = (() => this.GetGroupItems(paging, userContext, logEvent));
					}
				}
				break;
			case OneDriveProScopeType.Recent:
				eventId = "OneDrive.Recent";
				func = (() => this.GetRecentItemsResponse(userContext, logEvent));
				break;
			}
			if (func == null)
			{
				string endpointUrl = string.IsNullOrEmpty(paging.ItemsEndpointUrl) ? this.GetEndpointUrlFromItemLocation(paging.Location, userContext) : paging.ItemsEndpointUrl;
				if (!string.IsNullOrEmpty(paging.Location) && OneDriveProAttachmentDataProvider.IsAbsoluteLocation(paging.Location))
				{
					paging.Location = new Uri(paging.Location).AbsolutePath;
				}
				if (paging.RequestedData == null)
				{
					eventId = "OneDrive.GetItemsNonPaged";
					func = (() => this.GetItemsNonPaged(paging.Location, paging.Sort, userContext, endpointUrl, this.DocumentLibrary, logEvent));
				}
				else
				{
					if (paging.PagingMetadata != null && !(paging.PagingMetadata is OneDriveProItemsPagingMetadata))
					{
						throw new ArgumentException("PagingMetadata must either be null or of type OneDriveProItemsPagingMetadata", "paging");
					}
					eventId = "OneDrive.GetItemsPaged";
					func = (() => this.GetItemsPaged(paging, userContext, endpointUrl, this.DocumentLibrary, logEvent));
				}
			}
			logEvent = new DataProviderCallLogEvent(eventId, userContext);
			return this.ExecuteFunctionSafeWithRetry<GetAttachmentDataProviderItemsResponse>(userContext, func, (AttachmentResultCode resultCode) => new GetAttachmentDataProviderItemsResponse
			{
				ResultCode = resultCode
			}, OneDriveProAttachmentDataProvider.LogMetadata.GetItems, true, logEvent);
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x0007BA14 File Offset: 0x00079C14
		public override Task<DownloadItemAsyncResult> DownloadItemAsync(string location, string itemId, string parentId, string providerEndpointUrl, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			UserContext userContext = UserContextManager.GetUserContext(CallContext.Current.HttpContext, CallContext.Current.EffectiveCaller, true);
			DataProviderCallLogEvent logEvent = new DataProviderCallLogEvent("OneDrive.DownloadItem", userContext);
			return this.ExecuteAsyncFunctionSafeWithRetry<DownloadItemAsyncResult>(cancellationToken, RequestContext.Current, CallContext.Current, userContext, delegate
			{
				cancellationToken.ThrowIfCancellationRequested();
				if (string.IsNullOrEmpty(providerEndpointUrl))
				{
					providerEndpointUrl = this.GetEndpointUrlFromItemLocation(location, userContext);
				}
				string text = location;
				if (!string.IsNullOrEmpty(location) && OneDriveProAttachmentDataProvider.IsAbsoluteLocation(location))
				{
					text = new Uri(location).AbsolutePath;
				}
				cancellationToken.ThrowIfCancellationRequested();
				string text2 = string.IsNullOrEmpty(providerEndpointUrl) ? this.EndPointUrl : providerEndpointUrl;
				DownloadItemAsyncResult result;
				using (IClientContext clientContext = OneDriveProUtilities.CreateAndConfigureClientContext(userContext.LogonIdentity, text2))
				{
					IFile file;
					if (!string.IsNullOrEmpty(text))
					{
						file = clientContext.Web.GetFileByServerRelativeUrl(text);
					}
					else
					{
						file = clientContext.Web.Lists.GetById(new Guid(parentId)).GetItemById(itemId).File;
					}
					file.Load(clientContext, new Expression<Func<File, object>>[]
					{
						(File x) => x.Name,
						(File x) => (object)x.Length,
						(File x) => x.ServerRelativeUrl,
						(File x) => x.ListItemAllFields
					});
					IClientResult<Stream> clientResult = file.OpenBinaryStream();
					OneDriveProUtilities.ExecuteQueryWithTraces(userContext, clientContext, logEvent, "DownloadItem");
					cancellationToken.ThrowIfCancellationRequested();
					Stream value = clientResult.Value;
					byte[] array = null;
					if (value != null)
					{
						logEvent.SetFileSize((int)value.Length);
						array = new byte[value.Length];
						int num = 0;
						while ((long)num < value.Length)
						{
							cancellationToken.ThrowIfCancellationRequested();
							int count = Math.Min(1048576, (int)value.Length - num);
							int num2 = value.Read(array, num, count);
							if (num2 <= 0)
							{
								logEvent.SetError("StreamReadFailure");
								logEvent.ResultCode = AttachmentResultCode.GenericFailure;
								return new DownloadItemAsyncResult
								{
									ResultCode = AttachmentResultCode.GenericFailure
								};
							}
							num += num2;
						}
					}
					result = new DownloadItemAsyncResult
					{
						ResultCode = AttachmentResultCode.Success,
						Item = this.CreateFileItem(file, text2),
						Bytes = array
					};
				}
				return result;
			}, (AttachmentResultCode resultCode) => new DownloadItemAsyncResult
			{
				ResultCode = resultCode
			}, OneDriveProAttachmentDataProvider.LogMetadata.DownloadItemAsync, true, logEvent);
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x0007BAE4 File Offset: 0x00079CE4
		internal override string GetItemAbsoulteUrl(UserContext userContext, string location, string providerEndpointUrl, string itemId = null, string parentItemId = null)
		{
			if (OneDriveProAttachmentDataProvider.IsAbsoluteLocation(location))
			{
				return location;
			}
			DataProviderCallLogEvent dataProviderCallLogEvent = new DataProviderCallLogEvent("OneDrive.GetItemAbsoulteUrl", userContext);
			string absoluteUri;
			try
			{
				dataProviderCallLogEvent.Reset();
				string text = string.IsNullOrEmpty(providerEndpointUrl) ? base.EndPointUrl : providerEndpointUrl;
				if (string.IsNullOrEmpty(location) && !string.IsNullOrEmpty(itemId) && !string.IsNullOrEmpty(parentItemId))
				{
					using (IClientContext clientContext = OneDriveProUtilities.CreateAndConfigureClientContext(userContext.LogonIdentity, text))
					{
						IFile file = clientContext.Web.Lists.GetById(new Guid(parentItemId)).GetItemById(itemId).File;
						file.Load(clientContext, new Expression<Func<File, object>>[]
						{
							(File x) => x.ServerRelativeUrl
						});
						OneDriveProUtilities.ExecuteQueryWithTraces(userContext, clientContext, dataProviderCallLogEvent, "GetItemAbsoulteUrl");
						location = file.ServerRelativeUrl;
					}
				}
				Uri baseUri = new Uri(text);
				Uri uri = new Uri(baseUri, location);
				absoluteUri = uri.AbsoluteUri;
			}
			finally
			{
				dataProviderCallLogEvent.SetFinish();
				OwaServerTraceLogger.AppendToLog(dataProviderCallLogEvent);
			}
			return absoluteUri;
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x0007BC4C File Offset: 0x00079E4C
		public override UploadItemAsyncResult UploadItemSync(byte[] fileContent, string fileName, CancellationToken cancellationToken)
		{
			UserContext userContext = UserContextManager.GetUserContext(CallContext.Current.HttpContext, CallContext.Current.EffectiveCaller, true);
			DataProviderCallLogEvent dataProviderCallLogEvent = new DataProviderCallLogEvent("OneDrive.UploadItem", userContext);
			UploadItemAsyncResult result;
			try
			{
				dataProviderCallLogEvent.Reset();
				result = this.UploadItemInternal(fileContent, fileName, userContext, CallContext.Current, dataProviderCallLogEvent, cancellationToken);
			}
			finally
			{
				dataProviderCallLogEvent.SetFinish();
				OwaServerTraceLogger.AppendToLog(dataProviderCallLogEvent);
			}
			return result;
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x0007BD10 File Offset: 0x00079F10
		internal override Task<UploadItemAsyncResult> UploadItemAsync(byte[] fileContent, string fileName, CancellationToken cancellationToken, CallContext callContext)
		{
			UserContext userContext = UserContextManager.GetUserContext(CallContext.Current.HttpContext, CallContext.Current.EffectiveCaller, true);
			DataProviderCallLogEvent logEvent = new DataProviderCallLogEvent("OneDrive.UploadItem", userContext);
			return this.ExecuteAsyncFunctionSafeWithRetry<UploadItemAsyncResult>(cancellationToken, RequestContext.Current, callContext, userContext, () => this.UploadItemInternal(fileContent, fileName, userContext, callContext, logEvent, cancellationToken), (AttachmentResultCode resultCode) => new UploadItemAsyncResult
			{
				ResultCode = resultCode
			}, OneDriveProAttachmentDataProvider.LogMetadata.UploadItemAsync, false, logEvent);
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x0007BDCC File Offset: 0x00079FCC
		private static ADUser GetADUserFromGroupSmtpAddress(string groupSmtpAddress)
		{
			IRecipientSession adrecipientSession = CallContext.Current.ADRecipientSessionContext.GetADRecipientSession();
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.PrimarySmtpAddress, groupSmtpAddress);
			ADUser[] array = adrecipientSession.FindADUser(adrecipientSession.SearchRoot, QueryScope.SubTree, filter, null, 1);
			if (array == null || array.Length == 0)
			{
				return null;
			}
			return array[0];
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x0007BE14 File Offset: 0x0007A014
		private UploadItemAsyncResult UploadItemInternal(byte[] fileContent, string fileName, UserContext userContext, CallContext callContext, DataProviderCallLogEvent logEvent, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			logEvent.SetFileSize(fileContent.Length);
			string text = base.EndPointUrl;
			if (userContext.IsGroupUserContext)
			{
				text = this.GetEndPointUrlForGroup(userContext, callContext, logEvent);
				if (string.IsNullOrEmpty(text))
				{
					logEvent.SetError("Unable to get Sharepoint documents url for group: {0}." + callContext.OwaExplicitLogonUser);
					return new UploadItemAsyncResult
					{
						ResultCode = AttachmentResultCode.GroupDocumentsUrlNotFound
					};
				}
			}
			UploadItemAsyncResult result;
			using (IClientContext clientContext = OneDriveProUtilities.CreateAndConfigureClientContext(userContext.LogonIdentity, text))
			{
				try
				{
					IFolder attachmentsUploadFolder = this.GetAttachmentsUploadFolder(clientContext, userContext, logEvent);
					if (attachmentsUploadFolder == null)
					{
						result = new UploadItemAsyncResult
						{
							ResultCode = AttachmentResultCode.GetUploadFolderError
						};
					}
					else
					{
						string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
						string extension = Path.GetExtension(fileName);
						string text2 = fileName;
						string fileUrl = attachmentsUploadFolder.ServerRelativeUrl + Path.AltDirectorySeparatorChar + text2;
						int num = 1;
						while (OneDriveProAttachmentDataProvider.FileExists(userContext, clientContext, fileUrl, logEvent))
						{
							text2 = fileNameWithoutExtension + string.Format("({0})", num) + extension;
							fileUrl = attachmentsUploadFolder.ServerRelativeUrl + Path.AltDirectorySeparatorChar + text2;
							num++;
							if (num > 300)
							{
								logEvent.SetError("TooManyDuplicateFileNames");
								logEvent.ResultCode = AttachmentResultCode.UploadError;
								return new UploadItemAsyncResult
								{
									ResultCode = AttachmentResultCode.UploadError
								};
							}
						}
						cancellationToken.ThrowIfCancellationRequested();
						FileCreationInformation fileCreationInformation = new FileCreationInformation();
						fileCreationInformation.ContentStream = new MemoryStream(fileContent);
						fileCreationInformation.Url = text2;
						fileCreationInformation.Overwrite = false;
						IFile file = attachmentsUploadFolder.Files.Add(fileCreationInformation);
						file.Load(clientContext, new Expression<Func<File, object>>[]
						{
							(File x) => x.Name,
							(File x) => (object)x.Length,
							(File x) => x.ServerRelativeUrl,
							(File x) => x.ListItemAllFields
						});
						OneDriveProUtilities.ExecuteQueryWithTraces(userContext, clientContext, logEvent, "UploadItem");
						result = new UploadItemAsyncResult
						{
							ResultCode = AttachmentResultCode.Success,
							Item = this.CreateFileItem(file, text)
						};
					}
				}
				catch (ServerException error)
				{
					logEvent.SetError(error);
					logEvent.ResultCode = AttachmentResultCode.UploadError;
					result = new UploadItemAsyncResult
					{
						ResultCode = AttachmentResultCode.UploadError
					};
				}
			}
			return result;
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x0007C16C File Offset: 0x0007A36C
		private string GetEndPointUrlForGroup(UserContext userContext, CallContext callContext, DataProviderCallLogEvent logEvent)
		{
			if (AttachmentDataProviderManager.IsMockDataProviderEnabled())
			{
				return base.EndPointUrl;
			}
			string result = null;
			if (userContext.IsGroupUserContext && !string.IsNullOrEmpty(callContext.OwaExplicitLogonUser))
			{
				ADUser aduserFromGroupSmtpAddress = OneDriveProAttachmentDataProvider.GetADUserFromGroupSmtpAddress(callContext.OwaExplicitLogonUser);
				if (aduserFromGroupSmtpAddress != null && !string.IsNullOrEmpty(aduserFromGroupSmtpAddress.SharePointDocumentsUrl))
				{
					result = this.GetEndpointUrlFromItemLocation(aduserFromGroupSmtpAddress.SharePointDocumentsUrl, userContext);
				}
			}
			return result;
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x0007C1CC File Offset: 0x0007A3CC
		private IFolder GetAttachmentsUploadFolder(IClientContext clientContext, UserContext userContext, DataProviderCallLogEvent logEvent)
		{
			string text;
			return this.GetAttachmentsUploadFolder(clientContext, userContext, logEvent, true, out text);
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x0007C1E8 File Offset: 0x0007A3E8
		private IFolder GetAttachmentsUploadFolder(IClientContext clientContext, UserContext userContext, DataProviderCallLogEvent logEvent, bool createFolder, out string folderName)
		{
			UserOptionsType config = new UserOptionsType();
			folderName = null;
			IFolder folder;
			try
			{
				folder = this.LoadExistingFolder(clientContext, userContext, logEvent, config);
			}
			catch (Exception error)
			{
				logEvent.SetError(error);
				return null;
			}
			if (folder == null)
			{
				string key = userContext.PrimarySmtpAddress.ToString();
				lock (OneDriveProAttachmentDataProvider.attachmentFolderCreationLocks.GetOrAdd(key, new object()))
				{
					try
					{
						try
						{
							folder = this.LoadExistingFolder(clientContext, userContext, logEvent, config);
						}
						catch (Exception error2)
						{
							logEvent.SetError(error2);
							return null;
						}
						if (folder == null)
						{
							string text = this.defaultUploadFolderName;
							string text2 = text;
							int num = 1;
							IFolderCollection folders = OneDriveProUtilities.GetDocumentsLibrary(clientContext, this.DocumentLibrary).RootFolder.Folders;
							while (OneDriveProAttachmentDataProvider.FolderExists(userContext, clientContext, folders, text2, logEvent))
							{
								text2 = text + " " + num;
								num++;
								if (num > 300)
								{
									logEvent.SetError("TooManyDuplicateAttachmentsFolderNames");
									logEvent.ResultCode = AttachmentResultCode.UploadError;
									return null;
								}
							}
							folderName = text2;
							if (createFolder)
							{
								folder = folders.Add(text2);
								folder.Load(clientContext, new Expression<Func<Folder, object>>[]
								{
									(Folder x) => x.ServerRelativeUrl,
									(Folder x) => x.ListItemAllFields
								});
								if (!userContext.IsGroupUserContext)
								{
									folder.ListItemAllFields.BreakRoleInheritance(false, false);
								}
								OneDriveProUtilities.ExecuteQueryWithTraces(userContext, clientContext, logEvent, "Load/Create Attachments Folder by name");
								try
								{
									this.SaveDefaultAttachmentsFolderId(userContext, folder.ListItemAllFields.IdAsString, config);
								}
								catch (Exception error3)
								{
									try
									{
										folder.DeleteObject();
										OneDriveProUtilities.ExecuteQueryWithTraces(userContext, clientContext, logEvent, "DeleteFolder");
									}
									catch (Exception ex)
									{
										logEvent.ErrorMessage = "Error deleting newly created folder: " + ex.ToString();
									}
									logEvent.SetError(error3);
									return null;
								}
							}
						}
					}
					finally
					{
						object obj;
						OneDriveProAttachmentDataProvider.attachmentFolderCreationLocks.TryRemove(key, out obj);
					}
					return folder;
				}
			}
			folderName = (string)folder.ListItemAllFields["FileLeafRef"];
			return folder;
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x0007C4E8 File Offset: 0x0007A6E8
		private IFolder LoadExistingFolder(IClientContext clientContext, UserContext userContext, DataProviderCallLogEvent logEvent, UserOptionsType config)
		{
			IFolder folder = null;
			string defaultAttachmentsFolderId = this.GetDefaultAttachmentsFolderId(userContext, config);
			if (!defaultAttachmentsFolderId.IsNullOrEmpty())
			{
				try
				{
					folder = OneDriveProUtilities.GetDocumentsLibrary(clientContext, this.DocumentLibrary).GetItemById(defaultAttachmentsFolderId).Folder;
					folder.Load(clientContext, new Expression<Func<Folder, object>>[]
					{
						(Folder x) => x.ServerRelativeUrl,
						(Folder x) => x.ListItemAllFields
					});
					OneDriveProUtilities.ExecuteQueryWithTraces(userContext, clientContext, logEvent, "Load Attachments Folder by ID");
				}
				catch (ServerException error)
				{
					logEvent.ErrorMessage = "Unable to load folder from OneDrive with saved folderId = " + defaultAttachmentsFolderId;
					logEvent.SetError(error);
					folder = null;
				}
				catch (MockServerException)
				{
					folder = null;
				}
			}
			return folder;
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x0007C60C File Offset: 0x0007A80C
		private string GetAttachmentsUploadFolderName(IClientContext clientContext, UserContext userContext, CallContext callContext, DataProviderCallLogEvent logEvent)
		{
			string result;
			this.GetAttachmentsUploadFolder(clientContext, userContext, logEvent, false, out result);
			return result;
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x0007C89C File Offset: 0x0007AA9C
		public override Task<UpdatePermissionsAsyncResult> UpdateDocumentPermissionsAsync(string[] userIds, AttachmentPermissionAssignment[] attachmentPermissionAssignments, CancellationToken cancellationToken)
		{
			if (userIds == null)
			{
				throw new ArgumentNullException("userIds");
			}
			if (attachmentPermissionAssignments == null)
			{
				throw new ArgumentNullException("attachmentPermissionAssignments");
			}
			UserContext userContext = UserContextManager.GetUserContext(CallContext.Current.HttpContext, CallContext.Current.EffectiveCaller, true);
			DataProviderCallLogEvent logEvent = new DataProviderCallLogEvent("OneDrive.UpdateDocumentPermissions", userContext);
			return this.ExecuteAsyncFunctionSafeWithRetry<UpdatePermissionsAsyncResult>(cancellationToken, RequestContext.Current, CallContext.Current, userContext, delegate
			{
				UpdatePermissionsAsyncResult result;
				using (IClientContext clientContext = OneDriveProUtilities.CreateAndConfigureClientContext(userContext.LogonIdentity, this.EndPointUrl))
				{
					IEnumerable<IUserSharingResult> enumerable = null;
					Dictionary<string, IEnumerable<IUserSharingResult>> dictionary = new Dictionary<string, IEnumerable<IUserSharingResult>>();
					try
					{
						foreach (AttachmentPermissionAssignment attachmentPermissionAssignment in attachmentPermissionAssignments)
						{
							string providerEndpointUrl = string.IsNullOrEmpty(attachmentPermissionAssignment.ResourceEndpointUrl) ? this.EndPointUrl : attachmentPermissionAssignment.ResourceEndpointUrl;
							string itemAbsoulteUrl = this.GetItemAbsoulteUrl(userContext, attachmentPermissionAssignment.ResourceLocation, providerEndpointUrl, null, null);
							int j = 0;
							while (j < userIds.Length)
							{
								IList<UserRoleAssignment> list = new List<UserRoleAssignment>();
								int num = (j + 10 < userIds.Length) ? (j + 10) : userIds.Length;
								while (j < num)
								{
									UserRoleAssignment item = new UserRoleAssignment
									{
										UserId = userIds[j],
										Role = this.ConvertAttachmentPermissionLevelToRole(attachmentPermissionAssignment.PermissionLevel)
									};
									list.Add(item);
									j++;
								}
								if (list.Count != 0)
								{
									IList<IUserSharingResult> list2 = clientContext.DocumentSharingManagerUpdateDocumentSharingInfo(itemAbsoulteUrl, list, false, true, false, null, false);
									if (!dictionary.ContainsKey(itemAbsoulteUrl))
									{
										dictionary.Add(itemAbsoulteUrl, list2);
									}
									else
									{
										dictionary[itemAbsoulteUrl] = dictionary[itemAbsoulteUrl].Concat(list2);
									}
									enumerable = ((enumerable != null) ? enumerable.Concat(list2) : list2);
								}
							}
						}
						OneDriveProUtilities.ExecuteQueryWithTraces(userContext, clientContext, logEvent, "UpdateDocumentPermissions");
						if (enumerable == null)
						{
							logEvent.SetError("NoUserSharingResult");
							logEvent.ResultCode = AttachmentResultCode.UpdatePermissionsError;
							result = new UpdatePermissionsAsyncResult
							{
								ResultCode = AttachmentResultCode.UpdatePermissionsError
							};
						}
						else
						{
							result = new UpdatePermissionsAsyncResult
							{
								ResultCode = AttachmentResultCode.Success,
								ResultsDictionary = dictionary
							};
						}
					}
					catch (ServerException error)
					{
						logEvent.SetError(error);
						logEvent.ResultCode = AttachmentResultCode.UpdatePermissionsError;
						result = new UpdatePermissionsAsyncResult
						{
							ResultCode = AttachmentResultCode.UpdatePermissionsError
						};
					}
				}
				return result;
			}, (AttachmentResultCode resultCode) => new UpdatePermissionsAsyncResult
			{
				ResultCode = resultCode
			}, OneDriveProAttachmentDataProvider.LogMetadata.UploadItemAsync, false, logEvent);
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x0007C968 File Offset: 0x0007AB68
		public override string GetEndpointUrlFromItemLocation(string location)
		{
			UserContext userContext = UserContextManager.GetUserContext(CallContext.Current.HttpContext, CallContext.Current.EffectiveCaller, true);
			return this.GetEndpointUrlFromItemLocation(location, userContext);
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x0007C998 File Offset: 0x0007AB98
		internal override void PostInitialize(GetAttachmentDataProvidersRequest request)
		{
			base.DefaultScopeIndex = 0;
			Collection<AttachmentDataProviderScope> collection = new Collection<AttachmentDataProviderScope>();
			if (request != null && request.ClientVersion > 1)
			{
				collection.Add(new OneDriveProScope(OneDriveProScopeType.Recent, Strings.GetLocalizedString(945968721), Strings.GetLocalizedString(12212042)));
			}
			collection.Add(new OneDriveProScope(OneDriveProScopeType.MyDocuments, Strings.GetLocalizedString(-1557738081), Strings.GetLocalizedString(-1913834712)));
			collection.Add(new OneDriveProScope(OneDriveProScopeType.SharedWithMe, Strings.GetLocalizedString(-1529072712), Strings.GetLocalizedString(-334206725)));
			if (request != null && request.ClientVersion == 1)
			{
				UserContext userContext = UserContextManager.GetUserContext(CallContext.Current.HttpContext, CallContext.Current.EffectiveCaller, true);
				if (userContext.FeaturesManager.ClientServerSettings.ModernGroups.Enabled)
				{
					collection.Add(new OneDriveProScope(OneDriveProScopeType.Groups, Strings.GetLocalizedString(1495430923), Strings.GetLocalizedString(563916706)));
				}
			}
			base.Scopes = collection.ToArray<AttachmentDataProviderScope>();
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x0007CAA0 File Offset: 0x0007ACA0
		internal static bool TryGetBposDocumentsInfoFromBox(UserContext userContext, CallContext callContext, out string documentsUrl, out string displayName)
		{
			documentsUrl = null;
			displayName = null;
			NavBarData navBarData = null;
			if (!userContext.IsDisposed && userContext.BposShellInfoAssetReader != null)
			{
				BposShellInfo data = userContext.BposShellInfoAssetReader.GetData(callContext.EffectiveCaller);
				if (data != null)
				{
					navBarData = data.NavBarData;
				}
			}
			if (navBarData != null && navBarData.WorkloadLinks != null)
			{
				IEnumerable<NavBarLinkData> enumerable = from x in navBarData.WorkloadLinks
				where x.Id == "ShellDocuments"
				select x;
				if (enumerable != null && enumerable.Count<NavBarLinkData>() > 0)
				{
					NavBarLinkData navBarLinkData = enumerable.ElementAt(0);
					if (!string.IsNullOrEmpty(navBarLinkData.Url))
					{
						documentsUrl = navBarLinkData.Url;
						displayName = navBarLinkData.Text;
						if (!string.IsNullOrEmpty(navBarData.CompanyDisplayName))
						{
							displayName = displayName + " — " + navBarData.CompanyDisplayName;
						}
						return true;
					}
					OneDriveProAttachmentDataProvider.TraceError(OneDriveProAttachmentDataProvider.LogMetadata.OneDriveProUrlNull, null);
				}
			}
			else
			{
				OneDriveProAttachmentDataProvider.TraceError(OneDriveProAttachmentDataProvider.LogMetadata.NavBarDataNull, null);
			}
			return false;
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x0007CB80 File Offset: 0x0007AD80
		private Role ConvertAttachmentPermissionLevelToRole(AttachmentPermissionLevel permissionLevel)
		{
			switch (permissionLevel)
			{
			case AttachmentPermissionLevel.View:
				return 1;
			case AttachmentPermissionLevel.Edit:
				return 2;
			case AttachmentPermissionLevel.Owner:
				return 3;
			default:
				return 0;
			}
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x0007CBAC File Offset: 0x0007ADAC
		private AttachmentPermissionLevel ConvertRoleToAttachmentPermissionLevel(Role role)
		{
			switch (role)
			{
			case 1:
				return AttachmentPermissionLevel.View;
			case 2:
				return AttachmentPermissionLevel.Edit;
			case 3:
				return AttachmentPermissionLevel.Owner;
			default:
				return AttachmentPermissionLevel.None;
			}
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x0007CBD8 File Offset: 0x0007ADD8
		public override AttachmentDataProviderItem[] GetRecentItems()
		{
			UserContext userContext = UserContextManager.GetUserContext(CallContext.Current.HttpContext, CallContext.Current.EffectiveCaller, true);
			DataProviderCallLogEvent dataProviderCallLogEvent = new DataProviderCallLogEvent("OneDrive.GetRecentItems", userContext);
			dataProviderCallLogEvent.Reset();
			AttachmentDataProviderItem[] recentItemsInternal;
			try
			{
				recentItemsInternal = this.GetRecentItemsInternal(userContext, dataProviderCallLogEvent);
			}
			finally
			{
				dataProviderCallLogEvent.SetFinish();
				OwaServerTraceLogger.AppendToLog(dataProviderCallLogEvent);
			}
			return recentItemsInternal;
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x0007CD3C File Offset: 0x0007AF3C
		internal GetAttachmentDataProviderItemsResponse GetGroups(MailboxSession mailboxSession)
		{
			List<GroupMailbox> joinedGroupMailboxes = null;
			List<GroupMailbox> pinnedGroupMailboxes = null;
			CallContext callContext = CallContext.Current;
			IRecipientSession adSession = callContext.ADRecipientSessionContext.GetADRecipientSession();
			GroupMailboxAccessLayer.Execute("GetModernGroupsForAttachmentFilePicker", adSession, mailboxSession, delegate(GroupMailboxAccessLayer accessLayer)
			{
				UserMailboxLocator user = UserMailboxLocator.Instantiate(adSession, callContext.AccessingADUser);
				List<GroupMailbox> source = accessLayer.GetJoinedGroups(user, true).ToList<GroupMailbox>();
				pinnedGroupMailboxes = (from groupMailBox in source
				where groupMailBox.IsPinned
				orderby groupMailBox.JoinDate descending
				select groupMailBox).ToList<GroupMailbox>();
				joinedGroupMailboxes = (from groupMailBox in source
				where !groupMailBox.IsPinned
				orderby groupMailBox.JoinDate descending
				select groupMailBox).ToList<GroupMailbox>();
			});
			GetAttachmentDataProviderItemsResponse getAttachmentDataProviderItemsResponse = new GetAttachmentDataProviderItemsResponse();
			List<GroupAttachmentDataProviderItem> list = new List<GroupAttachmentDataProviderItem>();
			OneDriveProAttachmentDataProvider.AddGroupsToList(list, pinnedGroupMailboxes, Strings.GetLocalizedString(1571513972));
			OneDriveProAttachmentDataProvider.AddGroupsToList(list, joinedGroupMailboxes, Strings.GetLocalizedString(1612350458));
			getAttachmentDataProviderItemsResponse.ResultCode = AttachmentResultCode.Success;
			getAttachmentDataProviderItemsResponse.Items = list.ToArray();
			getAttachmentDataProviderItemsResponse.TotalItemCount = getAttachmentDataProviderItemsResponse.Items.Count<AttachmentDataProviderItem>();
			return getAttachmentDataProviderItemsResponse;
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x0007CDF8 File Offset: 0x0007AFF8
		internal string GetUploadFolderName(UserContext userContext)
		{
			DataProviderCallLogEvent dataProviderCallLogEvent = new DataProviderCallLogEvent("OneDrive.GetDefaultFolderName", userContext);
			dataProviderCallLogEvent.Reset();
			CallContext callContext = CallContext.Current;
			string text = base.EndPointUrl;
			if (userContext.IsGroupUserContext)
			{
				text = this.GetEndPointUrlForGroup(userContext, callContext, dataProviderCallLogEvent);
				if (string.IsNullOrEmpty(text))
				{
					return this.defaultUploadFolderName;
				}
			}
			string result;
			using (IClientContext clientContext = OneDriveProUtilities.CreateAndConfigureClientContext(userContext.LogonIdentity, text))
			{
				try
				{
					string attachmentsUploadFolderName = this.GetAttachmentsUploadFolderName(clientContext, userContext, callContext, dataProviderCallLogEvent);
					if (attachmentsUploadFolderName != null)
					{
						result = attachmentsUploadFolderName;
					}
					else
					{
						result = this.defaultUploadFolderName;
					}
				}
				finally
				{
					dataProviderCallLogEvent.SetFinish();
					OwaServerTraceLogger.AppendToLog(dataProviderCallLogEvent);
				}
			}
			return result;
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x0007CEA8 File Offset: 0x0007B0A8
		private AttachmentDataProviderItem[] GetRecentItemsInternal(UserContext userContext, DataProviderCallLogEvent logEvent)
		{
			DownloadResult downloadResult = OneDriveProUtilities.SendRestRequest("GET", string.Format("{0}/_api/me/getrecentdocs", base.EndPointUrl), userContext.LogonIdentity, null, logEvent, "GetRecentItems");
			if (!downloadResult.IsSucceeded)
			{
				logEvent.SetError("ErrorSendingRestCall");
				if (downloadResult.Exception != null)
				{
					logEvent.ErrorMessage = downloadResult.Exception.ToString();
				}
				OwaServerTraceLogger.AppendToLog(new TraceLogEvent("SP.GRI", userContext, "GetRecentItems", "Error sending REST call"));
				return null;
			}
			List<AttachmentDataProviderItem> list = new List<AttachmentDataProviderItem>();
			ExTimeZone userTimeZone = OneDriveProAttachmentDataProvider.GetUserTimeZone(userContext);
			try
			{
				using (XmlReader xmlReader = XmlReader.Create(downloadResult.ResponseStream))
				{
					while (xmlReader.Read())
					{
						if (xmlReader.IsStartElement("entry"))
						{
							AttachmentDataProviderItem attachmentDataProviderItem = new FileAttachmentDataProviderItem();
							using (XmlReader xmlReader2 = xmlReader.ReadSubtree())
							{
								while (xmlReader2.Read())
								{
									if (xmlReader.NodeType == XmlNodeType.Element)
									{
										if (xmlReader.LocalName.Equals("id"))
										{
											attachmentDataProviderItem.Id = xmlReader.ReadString();
										}
										else if (xmlReader.LocalName.Equals("FileName"))
										{
											attachmentDataProviderItem.Name = xmlReader.ReadString();
										}
										else if (xmlReader.LocalName.Equals("LinkLocation"))
										{
											attachmentDataProviderItem.Location = xmlReader.ReadString();
										}
										else if (xmlReader.LocalName.Equals("TimeStamp"))
										{
											attachmentDataProviderItem.Modified = this.GetIsoDateTimeString(xmlReader.ReadString(), userContext, userTimeZone);
										}
									}
								}
							}
							list.Add(attachmentDataProviderItem);
						}
					}
				}
				logEvent.NumberOfItems = new int?(list.Count);
			}
			catch (Exception ex)
			{
				logEvent.SetError(ex);
				OwaServerTraceLogger.AppendToLog(new TraceLogEvent("SP.GRI", userContext, "GetRecentItems", "Error parsing items:" + ex.Message));
				return null;
			}
			return list.ToArray();
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x0007D0D8 File Offset: 0x0007B2D8
		private GetAttachmentDataProviderItemsResponse GetRecentItemsResponse(UserContext userContext, DataProviderCallLogEvent logEvent)
		{
			GetAttachmentDataProviderItemsResponse getAttachmentDataProviderItemsResponse = new GetAttachmentDataProviderItemsResponse();
			try
			{
				AttachmentDataProviderItem[] recentItemsInternal = this.GetRecentItemsInternal(userContext, logEvent);
				getAttachmentDataProviderItemsResponse.Items = recentItemsInternal;
				getAttachmentDataProviderItemsResponse.TotalItemCount = getAttachmentDataProviderItemsResponse.Items.Count<AttachmentDataProviderItem>();
				getAttachmentDataProviderItemsResponse.ResultCode = AttachmentResultCode.Success;
			}
			catch (Exception)
			{
				getAttachmentDataProviderItemsResponse.ResultCode = AttachmentResultCode.GenericFailure;
			}
			return getAttachmentDataProviderItemsResponse;
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x0007D130 File Offset: 0x0007B330
		private string GetIsoDateTimeString(string originalString, UserContext userContext, ExTimeZone userTimeZone)
		{
			string result;
			try
			{
				result = userTimeZone.ConvertDateTime(ExDateTime.Parse(originalString)).ToString("yyyy-MM-ddTHH:mm:ss.fff");
			}
			catch (Exception)
			{
				OwaServerTraceLogger.AppendToLog(new TraceLogEvent("SP.GIDTS", userContext, "GetIsoDataTimeString", "Error parsing date time string " + originalString));
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x0007D194 File Offset: 0x0007B394
		private GetAttachmentDataProviderItemsResponse GetItemsNonPaged(string location, AttachmentItemsSort sort, UserContext userContext, string endpointUrl, string documentsLibrary, DataProviderCallLogEvent logEvent)
		{
			GetAttachmentDataProviderItemsResponse result;
			using (IClientContext clientContext = OneDriveProUtilities.CreateAndConfigureClientContext(userContext.LogonIdentity, endpointUrl))
			{
				IList documentsLibrary2 = OneDriveProUtilities.GetDocumentsLibrary(clientContext, documentsLibrary);
				CamlQuery query = OneDriveProUtilities.CreateCamlDataQuery(location, sort);
				IListItemCollection items = documentsLibrary2.GetItems(query);
				items.Load(clientContext, new Expression<Func<ListItemCollection, object>>[0]);
				OneDriveProUtilities.ExecuteQueryWithTraces(userContext, clientContext, logEvent, "GetItemsNonPaged");
				logEvent.NumberOfItems = new int?(items.Count());
				result = new GetAttachmentDataProviderItemsResponse
				{
					ResultCode = AttachmentResultCode.Success,
					Items = this.CreateItems(items, endpointUrl, OneDriveProAttachmentDataProvider.GetUserTimeZone(userContext)).ToArray(),
					PagingMetadata = null,
					TotalItemCount = items.Count()
				};
			}
			return result;
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x0007D258 File Offset: 0x0007B458
		private GetAttachmentDataProviderItemsResponse GetItemsPaged(AttachmentItemsPagingDetails paging, UserContext userContext, string endpointUrl, string documentsLibrary, DataProviderCallLogEvent logEvent)
		{
			OneDriveProItemsPagingMetadata oneDriveProItemsPagingMetadata = paging.PagingMetadata as OneDriveProItemsPagingMetadata;
			if (oneDriveProItemsPagingMetadata == null)
			{
				oneDriveProItemsPagingMetadata = new OneDriveProItemsPagingMetadata();
			}
			int num;
			IEnumerable<IListItem> items = oneDriveProItemsPagingMetadata.GetItems(userContext, endpointUrl, documentsLibrary, paging.Location, paging.RequestedData, paging.Sort, out num, logEvent);
			logEvent.NumberOfItems = new int?(num);
			if (endpointUrl.Equals(base.EndPointUrl))
			{
				endpointUrl = null;
			}
			return new GetAttachmentDataProviderItemsResponse
			{
				ResultCode = AttachmentResultCode.Success,
				Items = this.CreateItems(items, endpointUrl, OneDriveProAttachmentDataProvider.GetUserTimeZone(userContext)).ToArray(),
				PagingMetadata = oneDriveProItemsPagingMetadata,
				TotalItemCount = num
			};
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x0007D2F0 File Offset: 0x0007B4F0
		private GetAttachmentDataProviderItemsResponse GetFollowedItems(UserContext userContext)
		{
			return new GetAttachmentDataProviderItemsResponse
			{
				ResultCode = AttachmentResultCode.Success,
				Items = new AttachmentDataProviderItem[0],
				TotalItemCount = 0
			};
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x0007D320 File Offset: 0x0007B520
		private GetAttachmentDataProviderItemsResponse GetGroupItems(AttachmentItemsPagingDetails pagingDetails, UserContext userContext, DataProviderCallLogEvent logEvent)
		{
			OneDriveProGroupsPagingMetadata oneDriveProGroupsPagingMetadata = pagingDetails.PagingMetadata as OneDriveProGroupsPagingMetadata;
			if (oneDriveProGroupsPagingMetadata == null)
			{
				throw new InvalidOperationException("This method should only be called with non null OneDriveProGroupsPagingMetadata");
			}
			string text = oneDriveProGroupsPagingMetadata.GroupEndpointUrl;
			if (string.IsNullOrEmpty(text))
			{
				ADUser aduserFromGroupSmtpAddress = OneDriveProAttachmentDataProvider.GetADUserFromGroupSmtpAddress(oneDriveProGroupsPagingMetadata.GroupSmtpAddress);
				if (aduserFromGroupSmtpAddress == null || string.IsNullOrEmpty(aduserFromGroupSmtpAddress.SharePointDocumentsUrl))
				{
					AttachmentResultCode resultCode;
					if (aduserFromGroupSmtpAddress == null)
					{
						logEvent.SetError("ADUser not found for group " + oneDriveProGroupsPagingMetadata.GroupSmtpAddress);
						resultCode = AttachmentResultCode.GroupNotFound;
					}
					else
					{
						logEvent.SetError("SharepointDocumentsUrl not found for group " + oneDriveProGroupsPagingMetadata.GroupSmtpAddress);
						resultCode = AttachmentResultCode.GroupDocumentsUrlNotFound;
					}
					return new GetAttachmentDataProviderItemsResponse
					{
						ResultCode = resultCode,
						TotalItemCount = 0
					};
				}
				text = this.GetEndpointUrlFromItemLocation(aduserFromGroupSmtpAddress.SharePointDocumentsUrl, userContext);
			}
			return this.GetItemsPaged(pagingDetails, userContext, text, this.DocumentLibrary, logEvent);
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x0007D3E4 File Offset: 0x0007B5E4
		private GetAttachmentDataProviderItemsResponse GetSharedWithMeItems(UserContext userContext, DataProviderCallLogEvent logEvent)
		{
			GetAttachmentDataProviderItemsResponse getAttachmentDataProviderItemsResponse = new GetAttachmentDataProviderItemsResponse();
			List<AttachmentDataProviderItem> list = new List<AttachmentDataProviderItem>();
			int num = 0;
			int num2 = 0;
			DownloadResult downloadResult;
			for (;;)
			{
				OwaServerTraceLogger.AppendToLog(new TraceLogEvent("SP.GSWMI", userContext, "GetSharedWithMeItems", "Sending REST request. Offset = " + num));
				downloadResult = OneDriveProUtilities.SendRestRequest("GET", string.Format("{0}/_api/me/getsharedwithmedocs?offset={1}&rowLimit={2}", base.EndPointUrl, num, 100), userContext.LogonIdentity, null, logEvent, "GetSharedWithMeItems");
				if (!downloadResult.IsSucceeded)
				{
					break;
				}
				try
				{
					List<AttachmentDataProviderItem> list2 = this.ParseSharedWithMeItems(userContext, downloadResult);
					list.AddRange(list2);
					num2 = list2.Count;
					num += num2;
				}
				catch (Exception ex)
				{
					logEvent.SetError(ex);
					OwaServerTraceLogger.AppendToLog(new TraceLogEvent("SP.GSWMI", userContext, "GetSharedWithMeItems", "Error parsing the items:" + ex.Message));
					getAttachmentDataProviderItemsResponse.ResultCode = (logEvent.ResultCode = AttachmentResultCode.RestResponseParseError);
					return getAttachmentDataProviderItemsResponse;
				}
				if (num2 < 100)
				{
					goto Block_4;
				}
			}
			logEvent.SetError("ErrorSendingRestCall");
			OwaServerTraceLogger.AppendToLog(new TraceLogEvent("SP.GSWMI", userContext, "GetSharedWithMeItems", "Error sending REST call"));
			getAttachmentDataProviderItemsResponse.ResultCode = (logEvent.ResultCode = AttachmentResultCode.GenericFailure);
			WebException ex2 = downloadResult.Exception as WebException;
			if (ex2 != null)
			{
				logEvent.ErrorMessage = ex2.ToString();
				getAttachmentDataProviderItemsResponse.ResultCode = (logEvent.ResultCode = AttachmentDataProviderUtilities.GetResultCodeFromWebException(ex2, logEvent));
			}
			return getAttachmentDataProviderItemsResponse;
			Block_4:
			getAttachmentDataProviderItemsResponse.Items = list.ToArray();
			logEvent.NumberOfItems = new int?(getAttachmentDataProviderItemsResponse.TotalItemCount = getAttachmentDataProviderItemsResponse.Items.Count<AttachmentDataProviderItem>());
			getAttachmentDataProviderItemsResponse.ResultCode = AttachmentResultCode.Success;
			return getAttachmentDataProviderItemsResponse;
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x0007D590 File Offset: 0x0007B790
		private List<AttachmentDataProviderItem> ParseSharedWithMeItems(UserContext userContext, DownloadResult downloadResult)
		{
			List<AttachmentDataProviderItem> list = new List<AttachmentDataProviderItem>();
			ExTimeZone userTimeZone = OneDriveProAttachmentDataProvider.GetUserTimeZone(userContext);
			using (XmlReader xmlReader = XmlReader.Create(downloadResult.ResponseStream))
			{
				while (xmlReader.Read())
				{
					if (xmlReader.IsStartElement("entry"))
					{
						AttachmentDataProviderItem attachmentDataProviderItem = null;
						using (XmlReader xmlReader2 = xmlReader.ReadSubtree())
						{
							string uriString = string.Empty;
							string name = string.Empty;
							string id = string.Empty;
							string text = string.Empty;
							string location = string.Empty;
							string parentId = string.Empty;
							string originalString = string.Empty;
							while (xmlReader2.Read())
							{
								if (xmlReader.NodeType == XmlNodeType.Element)
								{
									if (xmlReader.LocalName.Equals("ListItemId"))
									{
										id = xmlReader.ReadString();
									}
									else if (xmlReader.LocalName.Equals("FileLeafRef"))
									{
										text = xmlReader.ReadString();
									}
									else if (xmlReader.LocalName.Equals("Title"))
									{
										name = xmlReader.ReadString();
									}
									else if (xmlReader.LocalName.Equals("FileRef"))
									{
										location = xmlReader.ReadString();
									}
									else if (xmlReader.LocalName.Equals("ParentLink"))
									{
										uriString = xmlReader.ReadString();
									}
									else if (xmlReader.LocalName.Equals("ListId"))
									{
										parentId = xmlReader.ReadString();
									}
									else if (xmlReader.LocalName.Equals("Modified"))
									{
										originalString = xmlReader.ReadString();
									}
									else if (xmlReader.LocalName.Equals("IsContainer"))
									{
										if (bool.Parse(xmlReader.ReadString()))
										{
											attachmentDataProviderItem = new FolderAttachmentDataProviderItem();
										}
										else
										{
											attachmentDataProviderItem = new FileAttachmentDataProviderItem();
										}
									}
								}
							}
							if (attachmentDataProviderItem == null)
							{
								OwaServerTraceLogger.AppendToLog(new TraceLogEvent("SP.GSWMI", userContext, "ParseSharedWithMeItems", "Error parsing item because isContainer property was not provided. Item name = " + text));
								continue;
							}
							attachmentDataProviderItem.Id = id;
							attachmentDataProviderItem.Name = text;
							attachmentDataProviderItem.Location = location;
							attachmentDataProviderItem.ProviderType = AttachmentDataProviderType.OneDrivePro;
							attachmentDataProviderItem.Modified = this.GetIsoDateTimeString(originalString, userContext, userTimeZone);
							attachmentDataProviderItem.ParentId = parentId;
							if (attachmentDataProviderItem.Name.Equals("DispForm.aspx", StringComparison.OrdinalIgnoreCase))
							{
								attachmentDataProviderItem.Name = name;
								attachmentDataProviderItem.Location = string.Empty;
							}
							string absoluteUri = new Uri(new Uri(uriString), attachmentDataProviderItem.Location).AbsoluteUri;
							attachmentDataProviderItem.Location = absoluteUri;
						}
						list.Add(attachmentDataProviderItem);
					}
				}
			}
			return list;
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x0007D82C File Offset: 0x0007BA2C
		private bool TryUpdateDataProvider(UserContext userContext, CallContext callContext)
		{
			if ((this.IsSystemGenerated == null || this.IsSystemGenerated.Value) && (this.LastUpdateTime == null || (DateTime.UtcNow - this.LastUpdateTime.Value).TotalHours > 1.0))
			{
				OneDriveProAttachmentDataProvider oneDriveProAttachmentDataProvider = OneDriveProAttachmentDataProvider.CreateFromBpos(userContext, callContext);
				if (oneDriveProAttachmentDataProvider != null && (oneDriveProAttachmentDataProvider.EndPointUrl != base.EndPointUrl || oneDriveProAttachmentDataProvider.DocumentLibrary != this.DocumentLibrary))
				{
					base.EndPointUrl = oneDriveProAttachmentDataProvider.EndPointUrl;
					this.DocumentLibrary = oneDriveProAttachmentDataProvider.DocumentLibrary;
					base.DisplayName = oneDriveProAttachmentDataProvider.DisplayName;
					this.IsSystemGenerated = new bool?(true);
					this.LastUpdateTime = new DateTime?(DateTime.UtcNow);
					this.NotifyOneDriveProviderChanged(userContext);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x0007D98C File Offset: 0x0007BB8C
		private void NotifyOneDriveProviderChanged(UserContext userContext)
		{
			if (this.AttachmentDataProviderChanged == null)
			{
				return;
			}
			try
			{
				OwaDiagnostics.SendWatsonReportsForGrayExceptions(delegate()
				{
					try
					{
						userContext.LockAndReconnectMailboxSession(3000);
						this.AttachmentDataProviderChanged(this, new AttachmentDataProviderChangedEventArgs(userContext.MailboxSession));
					}
					finally
					{
						if (userContext.MailboxSessionLockedByCurrentThread())
						{
							userContext.UnlockAndDisconnectMailboxSession();
						}
					}
				});
			}
			catch (GrayException ex)
			{
				OwaServerTraceLogger.AppendToLog(new TraceLogEvent("SP.NODPC", userContext, "NotifyOneDriveProviderChanged", string.Format("Error saving OneDrive provider updates:" + ex.Message, new object[0])));
			}
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x0007DB04 File Offset: 0x0007BD04
		private Task<T> ExecuteAsyncFunctionSafeWithRetry<T>(CancellationToken cancellationToken, RequestContext requestContext, CallContext callContext, UserContext userContext, Func<T> function, Func<AttachmentResultCode, T> errorFunction, OneDriveProAttachmentDataProvider.LogMetadata loggingContext, bool retry, DataProviderCallLogEvent logEvent)
		{
			return Task.Run<T>(delegate()
			{
				HttpContext.Current = requestContext.HttpContext;
				RequestDetailsLogger requestDetailsLogger = RequestDetailsLoggerBase<RequestDetailsLogger>.InitializeRequestLogger();
				CallContext.SetCurrent(callContext);
				T result;
				try
				{
					requestDetailsLogger.Set(ExtensibleLoggerMetadata.EventId, typeof(OneDriveProAttachmentDataProvider.LogMetadata));
					cancellationToken.ThrowIfCancellationRequested();
					result = this.ExecuteFunctionSafeWithRetry<T>(userContext, function, errorFunction, loggingContext, retry, logEvent);
				}
				finally
				{
					CallContext.SetCurrent(null);
					if (callContext != null)
					{
						callContext.Dispose();
						requestContext.HttpContext.Items.Remove("CallContext");
					}
					RequestDetailsLoggerBase<RequestDetailsLogger>.SetCurrent(requestContext.HttpContext, null);
					if (requestDetailsLogger != null)
					{
						requestDetailsLogger.Dispose();
					}
				}
				return result;
			});
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x0007DB74 File Offset: 0x0007BD74
		private T ExecuteFunctionSafeWithRetry<T>(UserContext userContext, Func<T> function, Func<AttachmentResultCode, T> errorFunction, OneDriveProAttachmentDataProvider.LogMetadata loggingContext, bool retry, DataProviderCallLogEvent logEvent)
		{
			Exception ex = null;
			AttachmentResultCode attachmentResultCode = AttachmentResultCode.Success;
			logEvent.Reset();
			try
			{
				return function();
			}
			catch (OAuthTokenRequestFailedException ex2)
			{
				ex = ex2;
				attachmentResultCode = AttachmentResultCode.ExchangeOAuthError;
			}
			catch (WebException ex3)
			{
				ex = ex3;
				attachmentResultCode = AttachmentDataProviderUtilities.GetResultCodeFromWebException(ex3, logEvent);
			}
			catch (ClientRequestException ex4)
			{
				ex = ex4;
				attachmentResultCode = AttachmentResultCode.GenericFailure;
			}
			catch (ServerException ex5)
			{
				ex = ex5;
				attachmentResultCode = AttachmentResultCode.GenericFailure;
			}
			catch (ServiceAccessDeniedException ex6)
			{
				ex = ex6;
				attachmentResultCode = AttachmentResultCode.AccessDenied;
			}
			finally
			{
				logEvent.SetFinish();
				if (attachmentResultCode != AttachmentResultCode.Success)
				{
					logEvent.ResultCode = attachmentResultCode;
				}
				if (ex != null)
				{
					logEvent.SetError(ex);
				}
				OwaServerTraceLogger.AppendToLog(logEvent);
			}
			return this.HandleExceptionWithRetry<T>(userContext, function, errorFunction, loggingContext, retry, ex, attachmentResultCode, logEvent);
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x0007DC50 File Offset: 0x0007BE50
		private T HandleExceptionWithRetry<T>(UserContext userContext, Func<T> function, Func<AttachmentResultCode, T> errorFunction, OneDriveProAttachmentDataProvider.LogMetadata loggingContext, bool retry, Exception exception, AttachmentResultCode resultCode, DataProviderCallLogEvent logEvent)
		{
			OneDriveProAttachmentDataProvider.TraceError(loggingContext, exception);
			if (retry && this.TryUpdateDataProvider(userContext, CallContext.Current))
			{
				return this.ExecuteFunctionSafeWithRetry<T>(userContext, function, errorFunction, loggingContext, false, logEvent);
			}
			return errorFunction(resultCode);
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x0007DC84 File Offset: 0x0007BE84
		private List<AttachmentDataProviderItem> CreateItems(IEnumerable<IListItem> listItems, string endpointUrl, ExTimeZone userTimeZone)
		{
			List<AttachmentDataProviderItem> list = new List<AttachmentDataProviderItem>();
			foreach (IListItem listItem in listItems)
			{
				string itemObjectType;
				if ((itemObjectType = OneDriveProAttachmentDataProvider.GetItemObjectType(listItem)) != null)
				{
					if (!(itemObjectType == "0"))
					{
						if (itemObjectType == "1")
						{
							list.Add(this.CreateFolderItem(listItem, endpointUrl, userTimeZone));
						}
					}
					else
					{
						list.Add(this.CreateFileItem(listItem, endpointUrl, userTimeZone));
					}
				}
			}
			return list;
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x0007DD14 File Offset: 0x0007BF14
		private AttachmentDataProviderItem CreateFileItem(IListItem listItem, string endpointUrl, ExTimeZone userTimeZone)
		{
			FileAttachmentDataProviderItem fileAttachmentDataProviderItem = new FileAttachmentDataProviderItem();
			this.PopulateItem(fileAttachmentDataProviderItem, listItem, endpointUrl, userTimeZone);
			long size;
			if (long.TryParse(listItem["File_x0020_Size"].ToString(), out size))
			{
				fileAttachmentDataProviderItem.Size = size;
			}
			return fileAttachmentDataProviderItem;
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x0007DD54 File Offset: 0x0007BF54
		private FileAttachmentDataProviderItem CreateFileItem(IFile file, string endpointUrl)
		{
			return new FileAttachmentDataProviderItem
			{
				Name = file.Name,
				Id = file.ListItemAllFields["ID"].ToString(),
				Location = file.ServerRelativeUrl,
				Size = file.Length,
				AttachmentProviderId = base.Id,
				ProviderEndpointUrl = endpointUrl
			};
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x0007DDBC File Offset: 0x0007BFBC
		private AttachmentDataProviderItem CreateFolderItem(IListItem listItem, string endpointUrl, ExTimeZone userTimeZone)
		{
			FolderAttachmentDataProviderItem folderAttachmentDataProviderItem = new FolderAttachmentDataProviderItem();
			this.PopulateItem(folderAttachmentDataProviderItem, listItem, endpointUrl, userTimeZone);
			int num;
			if (!int.TryParse((string)listItem["ItemChildCount"], out num))
			{
				num = 0;
			}
			int num2;
			if (!int.TryParse((string)listItem["FolderChildCount"], out num2))
			{
				num2 = 0;
			}
			folderAttachmentDataProviderItem.ChildCount = num + num2;
			return folderAttachmentDataProviderItem;
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x0007DE1C File Offset: 0x0007C01C
		private void PopulateItem(AttachmentDataProviderItem item, IListItem listItem, string endpointUrl, ExTimeZone userTimeZone)
		{
			item.Name = (string)listItem["FileLeafRef"];
			item.Id = listItem["ID"].ToString();
			item.ProviderEndpointUrl = endpointUrl;
			item.ProviderType = AttachmentDataProviderType.OneDrivePro;
			item.Location = (string)listItem["FileRef"];
			item.Modified = userTimeZone.ConvertDateTime(new ExDateTime(ExTimeZone.UtcTimeZone, (DateTime)listItem["Modified"])).ToString("yyyy-MM-ddTHH:mm:ss.fff");
			item.ModifiedBy = ((FieldUserValue)listItem["Editor"]).LookupValue;
			item.AttachmentProviderId = base.Id;
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x0007DED4 File Offset: 0x0007C0D4
		private string GetEndpointUrlFromItemLocation(string location, UserContext userContext)
		{
			if (location != null && OneDriveProAttachmentDataProvider.IsAbsoluteLocation(location))
			{
				using (IClientContext clientContext = OneDriveProUtilities.CreateAndConfigureClientContext(userContext.LogonIdentity, location))
				{
					Uri uri = clientContext.WebUrlFromFolderUrlDirect(new Uri(location));
					if (uri == null)
					{
						OwaServerTraceLogger.AppendToLog(new TraceLogEvent("SP.GEUFIL", userContext, "GetEndpointUrlFromItemLocation", string.Format("Error getting endpoint url from:{0}", location)));
						throw new ExInvalidOperationException(string.Format("No web url was found for: Location={0} EndpointUrl={1}", location, base.EndPointUrl));
					}
					return uri.AbsoluteUri;
				}
			}
			return base.EndPointUrl;
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x0007DF74 File Offset: 0x0007C174
		private string ExtractDocumentsLibrary(string absoluteLocation, string endpointUrl)
		{
			if (!OneDriveProAttachmentDataProvider.IsAbsoluteLocation(absoluteLocation))
			{
				return this.DocumentLibrary;
			}
			if (!absoluteLocation.StartsWith(endpointUrl))
			{
				OwaServerTraceLogger.AppendToLog(new TraceLogEvent("SP.EDL", null, "ExtractDocumentsLibrary", string.Format("Error extracting doc lib from:{0}", absoluteLocation)));
				throw new ArgumentException(string.Format("Error extracting Documents lib: Location={0} EndpointUrl={1}", absoluteLocation, endpointUrl));
			}
			return absoluteLocation.Substring(endpointUrl.Length + 1).Split(new char[]
			{
				Path.AltDirectorySeparatorChar
			})[0];
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x0007DFF0 File Offset: 0x0007C1F0
		private static string GetItemObjectType(IListItem item)
		{
			string result = item["FSObjType"].ToString();
			object obj = item["ProgId"];
			if (obj != null && obj.ToString().Equals("OneNote.Notebook"))
			{
				result = "0";
			}
			return result;
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x0007E038 File Offset: 0x0007C238
		private static void AddGroupsToList(List<GroupAttachmentDataProviderItem> groupsList, List<GroupMailbox> groupMailboxes, string groupCategory)
		{
			foreach (GroupMailbox groupMailbox in groupMailboxes)
			{
				OneDriveProGroupsPagingMetadata oneDriveProGroupsPagingMetadata = new OneDriveProGroupsPagingMetadata();
				oneDriveProGroupsPagingMetadata.GroupSmtpAddress = (string)groupMailbox.SmtpAddress;
				GroupAttachmentDataProviderItem item = new GroupAttachmentDataProviderItem
				{
					Name = groupMailbox.DisplayName,
					Id = (string)groupMailbox.SmtpAddress,
					PagingMetadata = oneDriveProGroupsPagingMetadata,
					GroupCategory = groupCategory,
					JoinDate = groupMailbox.JoinDate.ToString("yyyy-MM-ddTHH:mm:ss.fff")
				};
				groupsList.Add(item);
			}
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x0007E0EC File Offset: 0x0007C2EC
		private static void TraceError(OneDriveProAttachmentDataProvider.LogMetadata error, object data)
		{
			if (OwaApplication.GetRequestDetailsLogger != null)
			{
				OwaApplication.GetRequestDetailsLogger.Set(error, data);
			}
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x0007E108 File Offset: 0x0007C308
		private static bool TryGetEndPointUrlAndDocumentLibraryFromBposUrl(UserContext userContext, string bposUrl, out string endPointUrl, out string documentLibrary)
		{
			string userPrincipalName = userContext.LogonIdentity.GetOWAMiniRecipient().UserPrincipalName;
			if (userPrincipalName != null)
			{
				string stringToEscape = userPrincipalName.Split(new char[]
				{
					'@'
				})[0];
				endPointUrl = string.Format("{0}/personal/{1}_{2}/", new Uri(bposUrl).GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped), OneDriveProAttachmentDataProvider.GetEscaped(stringToEscape), OneDriveProAttachmentDataProvider.GetEscaped(userContext.LogonIdentity.DomainName));
				if (OneDriveProAttachmentDataProvider.ValidateEndPointUrl(userContext, endPointUrl, out documentLibrary, string.Format("UPN = {0}", userPrincipalName)))
				{
					return true;
				}
			}
			string alias = userContext.LogonIdentity.GetOWAMiniRecipient().Alias;
			endPointUrl = string.Format("{0}/personal/{1}_{2}/", new Uri(bposUrl).GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped), OneDriveProAttachmentDataProvider.GetEscaped(alias), OneDriveProAttachmentDataProvider.GetEscaped(userContext.LogonIdentity.DomainName));
			return OneDriveProAttachmentDataProvider.ValidateEndPointUrl(userContext, endPointUrl, out documentLibrary, string.Format("Alias = {0}", alias));
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x0007E1DC File Offset: 0x0007C3DC
		private static bool ValidateEndPointUrl(UserContext userContext, string endPointUrl, out string documentLibrary, string logInfo)
		{
			DataProviderCallLogEvent dataProviderCallLogEvent = new DataProviderCallLogEvent("OneDrive.ValidateEndPointUrl", userContext);
			dataProviderCallLogEvent.Reset();
			Exception ex = null;
			documentLibrary = null;
			try
			{
				using (IClientContext clientContext = OneDriveProUtilities.CreateAndConfigureClientContext(userContext.LogonIdentity, endPointUrl))
				{
					IWeb web = clientContext.Web;
					web.Load(clientContext, new Expression<Func<Web, object>>[]
					{
						(Web x) => x.Title
					});
					OneDriveProUtilities.ExecuteQueryWithTraces(userContext, clientContext, dataProviderCallLogEvent, "ValidateEndPointUrl");
					if (OneDriveProAttachmentDataProvider.TestDocumentLibrary(userContext, endPointUrl, "Documents", dataProviderCallLogEvent))
					{
						documentLibrary = "Documents";
						return true;
					}
					return false;
				}
			}
			catch (ClientRequestException ex2)
			{
				ex = ex2;
			}
			catch (ServerException ex3)
			{
				ex = ex3;
			}
			catch (WebException ex4)
			{
				ex = ex4;
			}
			finally
			{
				dataProviderCallLogEvent.SetFinish();
				if (ex != null)
				{
					dataProviderCallLogEvent.ErrorMessage = string.Format("EndpointUrl {0} failed using {1}", endPointUrl, logInfo);
					dataProviderCallLogEvent.SetError(ex);
				}
				OwaServerTraceLogger.AppendToLog(dataProviderCallLogEvent);
			}
			OneDriveProAttachmentDataProvider.TraceError(OneDriveProAttachmentDataProvider.LogMetadata.TryGetEndPointUrlAndDocumentLibraryFromBposUrl, ex);
			return false;
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x0007E330 File Offset: 0x0007C530
		private static string GetEscaped(string stringToEscape)
		{
			return stringToEscape.Replace('.', '_');
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x0007E33C File Offset: 0x0007C53C
		private static bool TestDocumentLibrary(UserContext userContext, string siteUrl, string documentLibraryName, DataProviderCallLogEvent logEvent)
		{
			Exception ex = null;
			try
			{
				using (IClientContext clientContext = OneDriveProUtilities.CreateAndConfigureClientContext(userContext.LogonIdentity, siteUrl))
				{
					IList documentsLibrary = OneDriveProUtilities.GetDocumentsLibrary(clientContext, documentLibraryName);
					documentsLibrary.Load(clientContext, new Expression<Func<List, object>>[0]);
					OneDriveProUtilities.ExecuteQueryWithTraces(userContext, clientContext, logEvent, "TestDocumentLibrary");
					return true;
				}
			}
			catch (ClientRequestException ex2)
			{
				ex = ex2;
			}
			catch (ServerException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				logEvent.SetError(ex);
			}
			OneDriveProAttachmentDataProvider.TraceError(OneDriveProAttachmentDataProvider.LogMetadata.TestDocumentLibrary, ex);
			return false;
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x0007E3D4 File Offset: 0x0007C5D4
		private static bool FileExists(UserContext userContext, IClientContext context, string fileUrl, DataProviderCallLogEvent logEvent)
		{
			try
			{
				IFile fileByServerRelativeUrl = context.Web.GetFileByServerRelativeUrl(fileUrl);
				fileByServerRelativeUrl.Load(context, new Expression<Func<File, object>>[]
				{
					(File x) => (object)x.Exists
				});
				OneDriveProUtilities.ExecuteQueryWithTraces(userContext, context, logEvent, "FileExists");
				return fileByServerRelativeUrl.Exists;
			}
			catch (ServerException)
			{
			}
			return false;
		}

		// Token: 0x06002220 RID: 8736 RVA: 0x0007E47C File Offset: 0x0007C67C
		private static bool FolderExists(UserContext userContext, IClientContext context, IFolderCollection folderCollection, string folderName, DataProviderCallLogEvent logEvent)
		{
			try
			{
				IFolder byUrl = folderCollection.GetByUrl(folderName);
				byUrl.Load(context, new Expression<Func<Folder, object>>[0]);
				OneDriveProUtilities.ExecuteQueryWithTraces(userContext, context, logEvent, "FolderExists");
				return true;
			}
			catch (ServerException)
			{
			}
			catch (MockServerException)
			{
			}
			return false;
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x0007E4D8 File Offset: 0x0007C6D8
		private static bool IsAbsoluteLocation(string location)
		{
			Uri uri;
			return Uri.TryCreate(location, UriKind.Absolute, out uri);
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x0007E4F0 File Offset: 0x0007C6F0
		private static ExTimeZone GetUserTimeZone(UserContext userContext)
		{
			ExTimeZone result = ExTimeZone.CurrentTimeZone;
			try
			{
				userContext.LockAndReconnectMailboxSession();
				result = TimeZoneHelper.GetUserTimeZone(userContext.MailboxSession);
			}
			catch (Exception)
			{
				OwaServerTraceLogger.AppendToLog(new TraceLogEvent("SP.GUTZ", userContext, "GetUserTimeZone", "Unable to get user time zone"));
			}
			finally
			{
				userContext.UnlockAndDisconnectMailboxSession();
			}
			return result;
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x0007E55C File Offset: 0x0007C75C
		private string GetDefaultAttachmentsFolderId(UserContext userContext, UserOptionsType config)
		{
			string defaultAttachmentsUploadFolderId;
			try
			{
				userContext.LockAndReconnectMailboxSession();
				config.Load(userContext.MailboxSession, new UserConfigurationPropertyDefinition[]
				{
					this.defaultAttachmentsFolderPropertyDefinition
				});
				defaultAttachmentsUploadFolderId = config.DefaultAttachmentsUploadFolderId;
			}
			finally
			{
				userContext.UnlockAndDisconnectMailboxSession();
			}
			return defaultAttachmentsUploadFolderId;
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x0007E5B0 File Offset: 0x0007C7B0
		private void SaveDefaultAttachmentsFolderId(UserContext userContext, string folderId, UserOptionsType config)
		{
			config.DefaultAttachmentsUploadFolderId = folderId;
			if (userContext.IsGroupUserContext)
			{
				MailboxSession mailboxSession = null;
				try
				{
					mailboxSession = MailboxSession.OpenAsAdmin(userContext.ExchangePrincipal, CultureInfo.InvariantCulture, "Client=OWA");
					config.Commit(mailboxSession, new UserConfigurationPropertyDefinition[]
					{
						this.defaultAttachmentsFolderPropertyDefinition
					});
					return;
				}
				finally
				{
					if (mailboxSession != null)
					{
						mailboxSession.Disconnect();
						mailboxSession.Dispose();
					}
				}
			}
			try
			{
				userContext.LockAndReconnectMailboxSession();
				config.Commit(userContext.MailboxSession, new UserConfigurationPropertyDefinition[]
				{
					this.defaultAttachmentsFolderPropertyDefinition
				});
			}
			finally
			{
				userContext.UnlockAndDisconnectMailboxSession();
			}
		}

		// Token: 0x040012C1 RID: 4801
		internal const string IdColumnName = "ID";

		// Token: 0x040012C2 RID: 4802
		internal const string FSObjTypeColumnName = "FSObjType";

		// Token: 0x040012C3 RID: 4803
		internal const string SortBehaviorColumnName = "SortBehavior";

		// Token: 0x040012C4 RID: 4804
		internal const string NameColumnName = "FileLeafRef";

		// Token: 0x040012C5 RID: 4805
		internal const string ItemChildCountColumnName = "ItemChildCount";

		// Token: 0x040012C6 RID: 4806
		internal const string FolderChildCountColumnName = "FolderChildCount";

		// Token: 0x040012C7 RID: 4807
		internal const string ModifiedColumnName = "Modified";

		// Token: 0x040012C8 RID: 4808
		internal const string EditorColumnName = "Editor";

		// Token: 0x040012C9 RID: 4809
		internal const string SizeColumnName = "File_x0020_Size";

		// Token: 0x040012CA RID: 4810
		internal const string UrlPathColumnName = "FileRef";

		// Token: 0x040012CB RID: 4811
		internal const string ProgIdColumnName = "ProgId";

		// Token: 0x040012CC RID: 4812
		private const string FileObjectType = "0";

		// Token: 0x040012CD RID: 4813
		private const string FolderObjectType = "1";

		// Token: 0x040012CE RID: 4814
		private const string LongDashWithSpaces = " — ";

		// Token: 0x040012CF RID: 4815
		private const int AmountToReadInAChunk = 1048576;

		// Token: 0x040012D0 RID: 4816
		private const int UpdateIntervalInHours = 1;

		// Token: 0x040012D1 RID: 4817
		private const string EndPointUrlFormat = "{0}/personal/{1}_{2}/";

		// Token: 0x040012D2 RID: 4818
		private const string RootDocumentLibrary = "Documents";

		// Token: 0x040012D3 RID: 4819
		private const string OneDriveProLinkId = "ShellDocuments";

		// Token: 0x040012D4 RID: 4820
		private const string GetSharedWithMeDocsUrlFormat = "{0}/_api/me/getsharedwithmedocs?offset={1}&rowLimit={2}";

		// Token: 0x040012D5 RID: 4821
		private const string GetRecentDocsUrlFormat = "{0}/_api/me/getrecentdocs";

		// Token: 0x040012D6 RID: 4822
		private const string EntryElementName = "entry";

		// Token: 0x040012D7 RID: 4823
		private const string IdElementName = "id";

		// Token: 0x040012D8 RID: 4824
		private const string ListIdElementName = "ListId";

		// Token: 0x040012D9 RID: 4825
		private const string ListItemIdElementName = "ListItemId";

		// Token: 0x040012DA RID: 4826
		private const string FileLeafRefElementName = "FileLeafRef";

		// Token: 0x040012DB RID: 4827
		private const string TitleElementName = "Title";

		// Token: 0x040012DC RID: 4828
		private const string FileRefElementName = "FileRef";

		// Token: 0x040012DD RID: 4829
		private const string ParentLinkElementName = "ParentLink";

		// Token: 0x040012DE RID: 4830
		private const string ModifiedElementName = "Modified";

		// Token: 0x040012DF RID: 4831
		private const string IsContainerElementName = "IsContainer";

		// Token: 0x040012E0 RID: 4832
		private const string FilenameElementName = "FileName";

		// Token: 0x040012E1 RID: 4833
		private const string LinkLocationElementName = "LinkLocation";

		// Token: 0x040012E2 RID: 4834
		private const string TimestampElementName = "TimeStamp";

		// Token: 0x040012E3 RID: 4835
		private const string ClientInfoString = "Client=OWA";

		// Token: 0x040012E4 RID: 4836
		private static readonly ConcurrentDictionary<string, object> attachmentFolderCreationLocks = new ConcurrentDictionary<string, object>();

		// Token: 0x040012E6 RID: 4838
		private readonly string defaultUploadFolderName = Strings.GetLocalizedString(524335195);

		// Token: 0x040012E7 RID: 4839
		private UserConfigurationPropertyDefinition defaultAttachmentsFolderPropertyDefinition = UserOptionPropertySchema.Instance.GetPropertyDefinition(UserConfigurationPropertyId.DefaultAttachmentsUploadFolderId);

		// Token: 0x02000404 RID: 1028
		private enum LogMetadata
		{
			// Token: 0x040012F1 RID: 4849
			[DisplayName("SDP.CFB.SDPUN")]
			OneDriveProUrlNull,
			// Token: 0x040012F2 RID: 4850
			[DisplayName("SDP.CFB.NBDN")]
			NavBarDataNull,
			// Token: 0x040012F3 RID: 4851
			[DisplayName("SDP.CFB.GEU")]
			TryGetEndPointUrlAndDocumentLibraryFromBposUrl,
			// Token: 0x040012F4 RID: 4852
			[DisplayName("SDF.CFB.TDL")]
			TestDocumentLibrary,
			// Token: 0x040012F5 RID: 4853
			[DisplayName("SDF.CFB.GI")]
			GetItems,
			// Token: 0x040012F6 RID: 4854
			[DisplayName("SDF.CFB.DIA")]
			DownloadItemAsync,
			// Token: 0x040012F7 RID: 4855
			[DisplayName("SDF.CFB.UIA")]
			UploadItemAsync
		}
	}
}
