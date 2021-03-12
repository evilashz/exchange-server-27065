using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200029A RID: 666
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PublicFolderSessionCache : DisposeTrackableBase
	{
		// Token: 0x06001B8E RID: 7054 RVA: 0x0007F724 File Offset: 0x0007D924
		public PublicFolderSessionCache(OrganizationId organizationId, ExchangePrincipal connectAsPrincipal, ClientSecurityContext clientSecurityContext, CultureInfo cultureInfo, string clientInfoString, IBudget budget, ExTimeZone timeZone, bool openSessionAsAdmin)
		{
			Util.ThrowOnNullArgument(organizationId, "organizationId");
			if (!openSessionAsAdmin && connectAsPrincipal == null)
			{
				throw new ArgumentException("connectAsPrincipal must be set if openSessionAsAdmin is false");
			}
			this.organizationId = organizationId;
			this.connectAsPrincipal = connectAsPrincipal;
			this.clientSecurityContext = clientSecurityContext;
			this.cultureInfo = cultureInfo;
			this.clientInfoString = clientInfoString;
			this.budget = budget;
			this.timeZone = timeZone;
			this.openSessionAsAdmin = openSessionAsAdmin;
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x0007F7B4 File Offset: 0x0007D9B4
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing)
			{
				lock (this.lockObject)
				{
					foreach (PublicFolderSession publicFolderSession in this.openedSessions.Values)
					{
						publicFolderSession.Dispose();
					}
					this.openedSessions.Clear();
				}
			}
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x0007F844 File Offset: 0x0007DA44
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PublicFolderSessionCache>(this);
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x0007F84C File Offset: 0x0007DA4C
		public void DisconnectAllSessions()
		{
			using (this.CheckDisposed("DisconnectAllSessions"))
			{
				lock (this.lockObject)
				{
					foreach (PublicFolderSession publicFolderSession in this.openedSessions.Values)
					{
						if (publicFolderSession.IsConnected)
						{
							publicFolderSession.Disconnect();
						}
					}
				}
			}
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x0007F900 File Offset: 0x0007DB00
		public void UpdateTimeZoneOnAllSessions(ExTimeZone timeZone)
		{
			using (this.CheckDisposed("UpdateTimeZoneOnAllSessions"))
			{
				lock (this.lockObject)
				{
					foreach (PublicFolderSession publicFolderSession in this.openedSessions.Values)
					{
						publicFolderSession.ExTimeZone = timeZone;
					}
				}
			}
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x0007F9AC File Offset: 0x0007DBAC
		public PublicFolderSession GetPublicFolderHierarchySession()
		{
			PublicFolderSession publicFolderSession;
			using (this.CheckDisposed("GetPublicFolderHierarchySession"))
			{
				ExTraceGlobals.SessionTracer.TraceDebug((long)this.GetHashCode(), "PublicFolderSessionCache.GetPublicFolderHierarchySession called");
				publicFolderSession = this.GetPublicFolderSession(PublicFolderSession.HierarchyMailboxGuidAlias);
			}
			return publicFolderSession;
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x0007FA08 File Offset: 0x0007DC08
		public PublicFolderSession GetPublicFolderSession(Guid publicFolderMailboxGuid)
		{
			PublicFolderSession result;
			using (this.CheckDisposed("GetPublicFolderSession"))
			{
				ExTraceGlobals.SessionTracer.TraceDebug((long)this.GetHashCode(), "PublicFolderSessionCache.GetPublicFolderContentSession called");
				if (publicFolderMailboxGuid == PublicFolderSession.HierarchyMailboxGuidAlias)
				{
					publicFolderMailboxGuid = this.GetHierarchyMailboxGuidForUser();
				}
				PublicFolderSession publicFolderSession;
				if (this.openedSessions.TryGetValue(publicFolderMailboxGuid, out publicFolderSession))
				{
					result = publicFolderSession;
				}
				else
				{
					lock (this.lockObject)
					{
						if (this.openedSessions.TryGetValue(publicFolderMailboxGuid, out publicFolderSession))
						{
							return publicFolderSession;
						}
						if (this.openSessionAsAdmin)
						{
							publicFolderSession = PublicFolderSession.OpenAsAdmin(this.organizationId, this.connectAsPrincipal, publicFolderMailboxGuid, (this.clientSecurityContext == null) ? null : new WindowsPrincipal(this.clientSecurityContext.Identity), this.cultureInfo, this.clientInfoString, this.budget);
						}
						else
						{
							publicFolderSession = PublicFolderSession.Open(this.connectAsPrincipal, publicFolderMailboxGuid, this.clientSecurityContext, this.cultureInfo, this.clientInfoString);
							publicFolderSession.AccountingObject = this.budget;
						}
						if (this.timeZone != null)
						{
							publicFolderSession.ExTimeZone = this.timeZone;
						}
						if (this.firstPublicFolderSession == null)
						{
							this.firstPublicFolderSession = publicFolderSession;
						}
						this.openedSessions.Add(publicFolderSession.MailboxGuid, publicFolderSession);
					}
					result = publicFolderSession;
				}
			}
			return result;
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x0007FB8C File Offset: 0x0007DD8C
		public PublicFolderSession GetPublicFolderSession(StoreId folderId)
		{
			PublicFolderSession result;
			using (this.CheckDisposed("GetPublicFolderSession"))
			{
				ExTraceGlobals.SessionTracer.TraceDebug<StoreId>((long)this.GetHashCode(), "PublicFolderSessionCache.GetPublicFolderSession called for folder id: {0}", folderId);
				bool flag = false;
				Guid hierarchyMailboxGuidForUser = this.GetHierarchyMailboxGuidForUser();
				if (this.firstPublicFolderSession != null && this.firstPublicFolderSession.MailboxGuid != hierarchyMailboxGuidForUser)
				{
					try
					{
						using (Folder folder = Folder.Bind(this.firstPublicFolderSession, folderId))
						{
							flag = true;
							if (folder.IsContentAvailable())
							{
								return this.firstPublicFolderSession;
							}
							PublicFolderContentMailboxInfo contentMailboxInfo = folder.GetContentMailboxInfo();
							if (!contentMailboxInfo.IsValid)
							{
								throw new InvalidOperationException(string.Format("IsContentAvailable() should have returned true if content mailbox property (value={0}) was not parse-able as a guid", contentMailboxInfo));
							}
							return this.GetPublicFolderSession(contentMailboxInfo.MailboxGuid);
						}
					}
					catch (ObjectNotFoundException)
					{
					}
				}
				PublicFolderSession publicFolderSession = null;
				if (!flag)
				{
					publicFolderSession = this.GetPublicFolderHierarchySession();
					using (Folder folder2 = Folder.Bind(publicFolderSession, folderId))
					{
						if (!folder2.IsContentAvailable())
						{
							PublicFolderContentMailboxInfo contentMailboxInfo2 = folder2.GetContentMailboxInfo();
							if (!contentMailboxInfo2.IsValid)
							{
								throw new InvalidOperationException(string.Format("IsContentAvailable() should have returned true if content mailbox property (value={0}) was not parse-able as a guid", contentMailboxInfo2));
							}
							return this.GetPublicFolderSession(contentMailboxInfo2.MailboxGuid);
						}
					}
				}
				result = publicFolderSession;
			}
			return result;
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x0007FD24 File Offset: 0x0007DF24
		private ObjectAccessGuard CheckDisposed(string methodName)
		{
			if (base.IsDisposed)
			{
				StorageGlobals.TraceFailedCheckDisposed(this, methodName);
				throw new ObjectDisposedException(this.ToString());
			}
			return ObjectAccessGuard.Create(this.threadTracker, methodName);
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x0007FD50 File Offset: 0x0007DF50
		private Guid GetHierarchyMailboxGuidForUser()
		{
			Guid result;
			using (this.CheckDisposed("GetHierarchyMailboxGuidForUser"))
			{
				ExTraceGlobals.SessionTracer.TraceDebug((long)this.GetHashCode(), "PublicFolderSessionCache.GetHierarchyMailboxGuidForUser called");
				Guid empty = Guid.Empty;
				bool flag;
				if (this.openSessionAsAdmin)
				{
					flag = PublicFolderSession.TryGetPrimaryHierarchyMailboxGuid(this.organizationId, out empty);
				}
				else
				{
					flag = PublicFolderSession.TryGetHierarchyMailboxGuidForUser(this.organizationId, this.connectAsPrincipal.MailboxInfo.MailboxGuid, this.connectAsPrincipal.DefaultPublicFolderMailbox, out empty);
				}
				if (!flag)
				{
					throw new ObjectNotFoundException(PublicFolderSession.GetNoPublicFoldersProvisionedError(this.organizationId));
				}
				result = empty;
			}
			return result;
		}

		// Token: 0x04001325 RID: 4901
		private readonly ObjectThreadTracker threadTracker = new ObjectThreadTracker();

		// Token: 0x04001326 RID: 4902
		private readonly Dictionary<Guid, PublicFolderSession> openedSessions = new Dictionary<Guid, PublicFolderSession>(2);

		// Token: 0x04001327 RID: 4903
		private readonly OrganizationId organizationId;

		// Token: 0x04001328 RID: 4904
		private readonly ExchangePrincipal connectAsPrincipal;

		// Token: 0x04001329 RID: 4905
		private readonly ClientSecurityContext clientSecurityContext;

		// Token: 0x0400132A RID: 4906
		private readonly CultureInfo cultureInfo;

		// Token: 0x0400132B RID: 4907
		private readonly string clientInfoString;

		// Token: 0x0400132C RID: 4908
		private readonly IBudget budget;

		// Token: 0x0400132D RID: 4909
		private readonly ExTimeZone timeZone;

		// Token: 0x0400132E RID: 4910
		private readonly bool openSessionAsAdmin;

		// Token: 0x0400132F RID: 4911
		private readonly object lockObject = new object();

		// Token: 0x04001330 RID: 4912
		private PublicFolderSession firstPublicFolderSession;
	}
}
