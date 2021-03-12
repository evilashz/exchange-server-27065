using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.UserPhotos;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.AirSync;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.InfoWorker.Common.Availability.Proxy;
using Microsoft.Exchange.InfoWorker.Common.UserPhotos;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000B0 RID: 176
	internal sealed class AirSyncPhotoRetriever : DisposeTrackableBase
	{
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x00037B02 File Offset: 0x00035D02
		// (set) Token: 0x06000986 RID: 2438 RVA: 0x00037B0A File Offset: 0x00035D0A
		public IAirSyncContext Context { get; set; }

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x00037B13 File Offset: 0x00035D13
		// (set) Token: 0x06000988 RID: 2440 RVA: 0x00037B1B File Offset: 0x00035D1B
		public List<StatusCode> StatusCodes { get; set; }

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x00037B24 File Offset: 0x00035D24
		// (set) Token: 0x0600098A RID: 2442 RVA: 0x00037B2C File Offset: 0x00035D2C
		public Dictionary<string, byte[]> RetrievedPhotos { get; set; }

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x00037B35 File Offset: 0x00035D35
		// (set) Token: 0x0600098C RID: 2444 RVA: 0x00037B3D File Offset: 0x00035D3D
		public int NumberOfPhotosRequested { get; set; }

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x00037B46 File Offset: 0x00035D46
		// (set) Token: 0x0600098E RID: 2446 RVA: 0x00037B4E File Offset: 0x00035D4E
		public int NumberOfPhotosSuccess { get; set; }

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x00037B57 File Offset: 0x00035D57
		// (set) Token: 0x06000990 RID: 2448 RVA: 0x00037B5F File Offset: 0x00035D5F
		public int NumberOfPhotosFromCache { get; set; }

		// Token: 0x06000991 RID: 2449 RVA: 0x00037B68 File Offset: 0x00035D68
		internal AirSyncPhotoRetriever(IAirSyncContext context)
		{
			this.Context = context;
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x00037BAC File Offset: 0x00035DAC
		internal byte[] EndGetThumbnailPhotoFromMailbox(string targetPrimarySmtpAddress, TimeSpan waitTime, UserPhotoSize photoSize)
		{
			AirSyncPhotoRetriever.UserPhotoWithSize userPhotoWithSize = null;
			if ((!AirSyncPhotoRetriever.userPhotosCache.ContainsKey(targetPrimarySmtpAddress) || (from s in AirSyncPhotoRetriever.userPhotosCache[targetPrimarySmtpAddress]
			where s.PhotoSize == photoSize
			select s).Count<AirSyncPhotoRetriever.UserPhotoWithSize>() == 0) && waitTime.TotalMilliseconds > 0.0)
			{
				AirSyncDiagnostics.TraceDebug<string, double>(ExTraceGlobals.ProtocolTracer, "AirSyncPhotoRetriever::EndGetThumbnailPhotoFromMailbox - user photo not available in cache. TargetUser: {0},  PhotoSize: {1}, waittime:{2}", targetPrimarySmtpAddress, photoSize.ToString(), waitTime.TotalMilliseconds);
				IAsyncResult asyncResult;
				if (this.delegatesCollection.TryRemove(targetPrimarySmtpAddress, out asyncResult) && asyncResult.AsyncWaitHandle.WaitOne(waitTime))
				{
					AirSyncPhotoRetriever.GetThumbnailPhotoCompleted(asyncResult);
					this.NumberOfPhotosSuccess++;
					AirSyncDiagnostics.TraceDebug<int>(ExTraceGlobals.ProtocolTracer, "AirSyncPhotoRetriever::EndGetThumbnailPhotoFromMailbox - user photo successfully retrieved. user:{0}, numberofPhotosSuccess:{1}", targetPrimarySmtpAddress, this.NumberOfPhotosSuccess);
				}
				else
				{
					AirSyncDiagnostics.TraceDebug(ExTraceGlobals.ProtocolTracer, null, "AirSyncPhotoRetriever::EndGetThumbnailPhotoFromMailbox - user photo failed to retrieve.");
				}
			}
			else
			{
				AirSyncDiagnostics.TraceDebug<int>(ExTraceGlobals.ProtocolTracer, null, "AirSyncPhotoRetriever::EndGetThumbnailPhotoFromMailbox - photo retrieved from cache. number of photos from cache:{0}", this.NumberOfPhotosFromCache);
			}
			List<AirSyncPhotoRetriever.UserPhotoWithSize> source;
			if (this.VerifyUserPermissions(this.Context.User.ExchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString(), this.Context.User.OrganizationId, targetPrimarySmtpAddress) && AirSyncPhotoRetriever.userPhotosCache.TryGetValue(targetPrimarySmtpAddress, out source))
			{
				userPhotoWithSize = (from s in source
				where s.PhotoSize == photoSize
				select s).FirstOrDefault<AirSyncPhotoRetriever.UserPhotoWithSize>();
			}
			if (userPhotoWithSize == null)
			{
				return null;
			}
			return userPhotoWithSize.UserPhotoBytes;
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x00037D44 File Offset: 0x00035F44
		internal void BeginGetThumbnailPhotoFromMailbox(List<string> targetPrimarySmtpAddresses, UserPhotoSize photoSize)
		{
			foreach (string text in targetPrimarySmtpAddresses)
			{
				if (AirSyncPhotoRetriever.userPhotosCache.ContainsKey(text))
				{
					if ((from s in AirSyncPhotoRetriever.userPhotosCache[text]
					where s.PhotoSize == photoSize
					select s).Count<AirSyncPhotoRetriever.UserPhotoWithSize>() != 0)
					{
						goto IL_111;
					}
				}
				if (!this.delegatesCollection.ContainsKey(text))
				{
					AirSyncDiagnostics.TraceDebug<string, string, UserPhotoSize>(ExTraceGlobals.ProtocolTracer, null, "AirSyncPhotoRetriever::BeginGetThumbnailPhotoFromMailbox - {0} requesting photo for {1} , with photosize {2}", this.Context.User.ExchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString(), text, photoSize);
					AirSyncPhotoRetriever.GetThumbnailPhotoFromMailboxDelegate getThumbnailPhotoFromMailboxDelegate = new AirSyncPhotoRetriever.GetThumbnailPhotoFromMailboxDelegate(AirSyncPhotoRetriever.GetThumbnailPhotoFromMailbox);
					IAsyncResult value = getThumbnailPhotoFromMailboxDelegate.BeginInvoke(this.Context, text, photoSize, null, new AirSyncPhotoRetriever.UserPhotoWithSize
					{
						UserEmail = text,
						PhotoSize = photoSize
					});
					this.delegatesCollection.TryAdd(text, value);
					this.NumberOfPhotosRequested++;
					continue;
				}
				IL_111:
				this.NumberOfPhotosFromCache++;
			}
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00037EA8 File Offset: 0x000360A8
		private static void GetThumbnailPhotoCompleted(IAsyncResult result)
		{
			AirSyncPhotoRetriever.GetThumbnailPhotoFromMailboxDelegate getThumbnailPhotoFromMailboxDelegate = ((AsyncResult)result).AsyncDelegate as AirSyncPhotoRetriever.GetThumbnailPhotoFromMailboxDelegate;
			AirSyncPhotoRetriever.UserPhotoWithSize userPhotoWithSize = result.AsyncState as AirSyncPhotoRetriever.UserPhotoWithSize;
			try
			{
				byte[] userPhotoBytes = getThumbnailPhotoFromMailboxDelegate.EndInvoke(result);
				lock (AirSyncPhotoRetriever.lockObject)
				{
					List<AirSyncPhotoRetriever.UserPhotoWithSize> list;
					if (!AirSyncPhotoRetriever.userPhotosCache.ContainsKey(userPhotoWithSize.UserEmail))
					{
						list = new List<AirSyncPhotoRetriever.UserPhotoWithSize>();
					}
					else
					{
						list = AirSyncPhotoRetriever.userPhotosCache[userPhotoWithSize.UserEmail];
					}
					list.Add(new AirSyncPhotoRetriever.UserPhotoWithSize
					{
						UserEmail = userPhotoWithSize.UserEmail,
						PhotoSize = userPhotoWithSize.PhotoSize,
						UserPhotoBytes = userPhotoBytes
					});
					AirSyncPhotoRetriever.userPhotosCache.Add(userPhotoWithSize.UserEmail, list);
				}
			}
			catch (AccessDeniedException arg)
			{
				AirSyncDiagnostics.TraceError<AccessDeniedException>(ExTraceGlobals.ProtocolTracer, "AirSyncPhotoRetriever::GetThumbnailPhotoCompleted- Access denied retrieving thumbnailPhoto via GetUserPhoto. TargetUser: {0}.  Exception: {1}", userPhotoWithSize.UserEmail, arg);
			}
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00037FA0 File Offset: 0x000361A0
		private static IMailboxSession GetCachedMailboxSessionForPhotoRequest(ExchangePrincipal user)
		{
			return null;
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00037FA4 File Offset: 0x000361A4
		private static byte[] GetThumbnailPhotoFromMailbox(IAirSyncContext context, string targetPrimarySmtpAddress, UserPhotoSize photoSize = UserPhotoSize.HR48x48)
		{
			byte[] array = null;
			ClientContext clientContext = ClientContext.Create(context.User.ClientSecurityContextWrapper.ClientSecurityContext, context.User.ExchangePrincipal.MailboxInfo.OrganizationId, null, null, CultureInfo.InvariantCulture, Guid.NewGuid().ToString());
			clientContext.RequestSchemaVersion = ExchangeVersionType.Exchange2012;
			if (!SmtpAddress.IsValidSmtpAddress(targetPrimarySmtpAddress))
			{
				AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.ProtocolTracer, null, "Target SMTP address is not valid: {0}", targetPrimarySmtpAddress);
				return array;
			}
			GetUserPhotoQuery getUserPhotoQuery = new GetUserPhotoQuery(clientContext, new PhotoRequest
			{
				Requestor = new PhotoPrincipal
				{
					EmailAddresses = new string[]
					{
						context.User.ExchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString().ToString()
					},
					OrganizationId = context.User.ExchangePrincipal.MailboxInfo.OrganizationId
				},
				Size = photoSize,
				TargetSmtpAddress = targetPrimarySmtpAddress,
				Trace = ExTraceGlobals.ProtocolTracer.IsTraceEnabled(TraceType.DebugTrace),
				HostOwnedTargetMailboxSessionGetter = new Func<ExchangePrincipal, IMailboxSession>(AirSyncPhotoRetriever.GetCachedMailboxSessionForPhotoRequest)
			}, null, false, AirSyncPhotoRetriever.PhotosConfiguration, ExTraceGlobals.ProtocolTracer);
			try
			{
				array = getUserPhotoQuery.Execute();
				if (array == null || array.Length == 0)
				{
					AirSyncDiagnostics.TraceError(ExTraceGlobals.ProtocolTracer, "Unable to retrieve thumbnailPhoto via GetUserPhoto for {0}", targetPrimarySmtpAddress);
					return null;
				}
				return array;
			}
			catch (AccessDeniedException arg)
			{
				AirSyncDiagnostics.TraceError<AccessDeniedException>(ExTraceGlobals.ProtocolTracer, "Access denied retrieving thumbnailPhoto via GetUserPhoto for {0}.  Exception: {1}", targetPrimarySmtpAddress, arg);
			}
			return null;
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00038128 File Offset: 0x00036328
		internal static StatusCode GetThumbnailPhotoFromMailbox(IAirSyncContext context, string targetPrimarySmtpAddress, out byte[] thumbnailPhoto)
		{
			thumbnailPhoto = null;
			ClientContext clientContext = ClientContext.Create(context.User.ClientSecurityContextWrapper.ClientSecurityContext, context.User.ExchangePrincipal.MailboxInfo.OrganizationId, null, null, CultureInfo.InvariantCulture, Guid.NewGuid().ToString());
			clientContext.RequestSchemaVersion = ExchangeVersionType.Exchange2012;
			if (!SmtpAddress.IsValidSmtpAddress(targetPrimarySmtpAddress))
			{
				AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.ProtocolTracer, null, "Target SMTP address is not valid: {0}", targetPrimarySmtpAddress);
				return StatusCode.NoPicture;
			}
			GetUserPhotoQuery getUserPhotoQuery = new GetUserPhotoQuery(clientContext, new PhotoRequest
			{
				Requestor = new PhotoPrincipal
				{
					EmailAddresses = new string[]
					{
						context.User.ExchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString()
					},
					OrganizationId = context.User.ExchangePrincipal.MailboxInfo.OrganizationId
				},
				Size = UserPhotoSize.HR648x648,
				TargetSmtpAddress = targetPrimarySmtpAddress,
				Trace = ExTraceGlobals.ProtocolTracer.IsTraceEnabled(TraceType.DebugTrace),
				HostOwnedTargetMailboxSessionGetter = new Func<ExchangePrincipal, IMailboxSession>(AirSyncPhotoRetriever.GetCachedMailboxSessionForPhotoRequest)
			}, null, false, AirSyncPhotoRetriever.PhotosConfiguration, ExTraceGlobals.ProtocolTracer);
			StatusCode result;
			try
			{
				thumbnailPhoto = getUserPhotoQuery.Execute();
				if (thumbnailPhoto == null || thumbnailPhoto.Length == 0)
				{
					AirSyncDiagnostics.TraceError(ExTraceGlobals.ProtocolTracer, "Unable to retrieve thumbnailPhoto via GetUserPhoto for {0}", targetPrimarySmtpAddress);
					result = StatusCode.NoPicture;
				}
				else
				{
					result = StatusCode.Success;
				}
			}
			catch (AccessDeniedException arg)
			{
				AirSyncDiagnostics.TraceError<AccessDeniedException>(ExTraceGlobals.ProtocolTracer, "Access denied retrieving thumbnailPhoto via GetUserPhoto for {0}.  Exception: {1}", targetPrimarySmtpAddress, arg);
				result = StatusCode.NoPicture;
			}
			return result;
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x000382B0 File Offset: 0x000364B0
		private bool VerifyUserPermissions(string userEmail, OrganizationId userOrganizationId, string targetUserEmailAddress)
		{
			bool result;
			try
			{
				if (!SmtpAddress.IsValidSmtpAddress(targetUserEmailAddress))
				{
					result = false;
				}
				else
				{
					PhotoPrincipal requestor = new PhotoPrincipal
					{
						EmailAddresses = new List<string>
						{
							userEmail
						},
						OrganizationId = userOrganizationId
					};
					PhotoPrincipal target = new PhotoPrincipal
					{
						EmailAddresses = new List<string>
						{
							targetUserEmailAddress
						},
						OrganizationId = userOrganizationId
					};
					new PhotoAuthorization(OrganizationIdCache.Singleton, ExTraceGlobals.ProtocolTracer).Authorize(requestor, target);
					AirSyncDiagnostics.TraceDebug<string, string>(ExTraceGlobals.ProtocolTracer, null, "AirSyncPhotoRetriever::VerifyUserPermissions - {0} has permissiosn to retrieve photos for user {2}.", userEmail, targetUserEmailAddress);
					result = true;
				}
			}
			catch (AccessDeniedException arg)
			{
				AirSyncDiagnostics.TraceError<AccessDeniedException, string>(ExTraceGlobals.ProtocolTracer, "Access denied verifying user's permissions to retrieve thumbnailPhoto via GetUserPhoto for {0}.  Exception: {1}. Current user :{2}", targetUserEmailAddress, arg, userEmail);
				result = false;
			}
			return result;
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00038374 File Offset: 0x00036574
		private void LogDataToProtocolLogger()
		{
			if (this.Context != null)
			{
				this.Context.ProtocolLogger.SetValue(ProtocolLoggerData.PhotosFromCache, this.NumberOfPhotosFromCache);
				this.Context.ProtocolLogger.SetValue(ProtocolLoggerData.TotalPhotoRequests, this.NumberOfPhotosRequested);
				this.Context.ProtocolLogger.SetValue(ProtocolLoggerData.SuccessfulPhotoRequests, this.NumberOfPhotosSuccess);
			}
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x000383D1 File Offset: 0x000365D1
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.LogDataToProtocolLogger();
				this.delegatesCollection.Clear();
			}
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x000383E7 File Offset: 0x000365E7
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AirSyncPhotoRetriever>(this);
		}

		// Token: 0x040005F6 RID: 1526
		private static readonly PhotosConfiguration PhotosConfiguration = new PhotosConfiguration(ExchangeSetupContext.InstallPath);

		// Token: 0x040005F7 RID: 1527
		private static object lockObject = new object();

		// Token: 0x040005F8 RID: 1528
		private ConcurrentDictionary<string, IAsyncResult> delegatesCollection = new ConcurrentDictionary<string, IAsyncResult>();

		// Token: 0x040005F9 RID: 1529
		private static MruDictionaryCache<string, List<AirSyncPhotoRetriever.UserPhotoWithSize>> userPhotosCache = new MruDictionaryCache<string, List<AirSyncPhotoRetriever.UserPhotoWithSize>>(GlobalSettings.HDPhotoCacheMaxSize, (int)GlobalSettings.HDPhotoCacheExpirationTimeOut.TotalMinutes);

		// Token: 0x020000B1 RID: 177
		internal class UserPhotoWithSize
		{
			// Token: 0x1700037E RID: 894
			// (get) Token: 0x0600099D RID: 2461 RVA: 0x00038433 File Offset: 0x00036633
			// (set) Token: 0x0600099E RID: 2462 RVA: 0x0003843B File Offset: 0x0003663B
			public string UserEmail { get; set; }

			// Token: 0x1700037F RID: 895
			// (get) Token: 0x0600099F RID: 2463 RVA: 0x00038444 File Offset: 0x00036644
			// (set) Token: 0x060009A0 RID: 2464 RVA: 0x0003844C File Offset: 0x0003664C
			public byte[] UserPhotoBytes { get; set; }

			// Token: 0x17000380 RID: 896
			// (get) Token: 0x060009A1 RID: 2465 RVA: 0x00038455 File Offset: 0x00036655
			// (set) Token: 0x060009A2 RID: 2466 RVA: 0x0003845D File Offset: 0x0003665D
			public UserPhotoSize PhotoSize { get; set; }
		}

		// Token: 0x020000B2 RID: 178
		// (Invoke) Token: 0x060009A5 RID: 2469
		private delegate byte[] GetThumbnailPhotoFromMailboxDelegate(IAirSyncContext context, string targetSmtpAddress, UserPhotoSize photoSize);
	}
}
