using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000888 RID: 2184
	internal class SessionAndAuthZ : IDisposable
	{
		// Token: 0x06003E8F RID: 16015 RVA: 0x000D8E70 File Offset: 0x000D7070
		public SessionAndAuthZ(StoreSession session, AuthZClientInfo authZClientInfo, CultureInfo clientCultureInfo)
		{
			this.session = session;
			this.authZClientInfo = authZClientInfo;
			this.cultureInfo = clientCultureInfo;
			this.authZClientInfo.AddRef();
			ExTraceGlobals.ServerToServerAuthZTracer.TraceDebug<string, Guid>((long)this.GetHashCode(), "[SessionAndAuthZ::ctor]  AuthZClientInfo for '{0}' is being associated with Session '{1}'.  Adding reference count.", (this.authZClientInfo.ClientSecurityContext == null) ? "Application" : this.authZClientInfo.ClientSecurityContext.UserSid.ToString(), this.session.MailboxGuid);
		}

		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x06003E90 RID: 16016 RVA: 0x000D8EF8 File Offset: 0x000D70F8
		public StoreSession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x06003E91 RID: 16017 RVA: 0x000D8F00 File Offset: 0x000D7100
		public AuthZClientInfo ClientInfo
		{
			get
			{
				return this.authZClientInfo;
			}
		}

		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x06003E92 RID: 16018 RVA: 0x000D8F08 File Offset: 0x000D7108
		public CultureInfo CultureInfo
		{
			get
			{
				return this.cultureInfo;
			}
		}

		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x06003E93 RID: 16019 RVA: 0x000D8F10 File Offset: 0x000D7110
		// (set) Token: 0x06003E94 RID: 16020 RVA: 0x000D8F18 File Offset: 0x000D7118
		public bool IsFromBackingCache
		{
			get
			{
				return this.isFromBackingCache;
			}
			set
			{
				this.isFromBackingCache = value;
				if (this.isFromBackingCache)
				{
					this.refreshedDefaultFolders.Clear();
				}
			}
		}

		// Token: 0x06003E95 RID: 16021 RVA: 0x000D8F34 File Offset: 0x000D7134
		public StoreObjectId GetRefreshedDefaultFolderId(DefaultFolderType defaultFolderType)
		{
			MailboxSession mailboxSession = this.session as MailboxSession;
			StoreObjectId storeObjectId;
			if (defaultFolderType == DefaultFolderType.AdminAuditLogs)
			{
				storeObjectId = mailboxSession.GetAdminAuditLogsFolderId();
			}
			else
			{
				storeObjectId = mailboxSession.GetDefaultFolderId(defaultFolderType);
			}
			if (storeObjectId == null && defaultFolderType == DefaultFolderType.RecoverableItemsDeletions)
			{
				DumpsterFolderHelper.CheckAndCreateFolder(mailboxSession);
				storeObjectId = mailboxSession.GetDefaultFolderId(defaultFolderType);
			}
			if (storeObjectId == null && mailboxSession.LogonType == LogonType.Delegated && this.isFromBackingCache && !this.refreshedDefaultFolders.ContainsKey(defaultFolderType))
			{
				storeObjectId = mailboxSession.RefreshDefaultFolder(defaultFolderType);
				this.refreshedDefaultFolders[defaultFolderType] = true;
			}
			if (storeObjectId == null)
			{
				ExTraceGlobals.ExceptionTracer.TraceError<DefaultFolderType>(0L, "GetDefaultFolderId returned null for DefaultFolderType: '{0}'. FolderNotFoundException will be thrown.", defaultFolderType);
				throw new FolderNotFoundException();
			}
			return storeObjectId;
		}

		// Token: 0x06003E96 RID: 16022 RVA: 0x000D8FCC File Offset: 0x000D71CC
		public void Dispose()
		{
			if (this.session != null)
			{
				this.session.Dispose();
				this.session = null;
			}
			if (this.authZClientInfo != null)
			{
				ExTraceGlobals.ServerToServerAuthZTracer.TraceDebug((long)this.GetHashCode(), "[SessionAndAuthZ::Dispose] AuthZClientInfo member is being disposed of");
				this.authZClientInfo.Dispose();
				this.authZClientInfo = null;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x040023E8 RID: 9192
		private StoreSession session;

		// Token: 0x040023E9 RID: 9193
		private AuthZClientInfo authZClientInfo;

		// Token: 0x040023EA RID: 9194
		private Dictionary<DefaultFolderType, bool> refreshedDefaultFolders = new Dictionary<DefaultFolderType, bool>();

		// Token: 0x040023EB RID: 9195
		private bool isFromBackingCache;

		// Token: 0x040023EC RID: 9196
		private CultureInfo cultureInfo;
	}
}
