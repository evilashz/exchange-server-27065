using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000797 RID: 1943
	internal abstract class IdConverterDependencies
	{
		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x06003A62 RID: 14946 RVA: 0x000CEEAB File Offset: 0x000CD0AB
		// (set) Token: 0x06003A63 RID: 14947 RVA: 0x000CEEB3 File Offset: 0x000CD0B3
		public bool IsOwa { get; protected set; }

		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x06003A64 RID: 14948 RVA: 0x000CEEBC File Offset: 0x000CD0BC
		// (set) Token: 0x06003A65 RID: 14949 RVA: 0x000CEEC4 File Offset: 0x000CD0C4
		public bool IsExternalUser { get; protected set; }

		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x06003A66 RID: 14950 RVA: 0x000CEECD File Offset: 0x000CD0CD
		// (set) Token: 0x06003A67 RID: 14951 RVA: 0x000CEED5 File Offset: 0x000CD0D5
		public bool IsWSSecurityUser { get; protected set; }

		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x06003A68 RID: 14952 RVA: 0x000CEEDE File Offset: 0x000CD0DE
		// (set) Token: 0x06003A69 RID: 14953 RVA: 0x000CEEE6 File Offset: 0x000CD0E6
		public ADRecipientSessionContext ADRecipientSessionContext { get; protected set; }

		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x06003A6A RID: 14954 RVA: 0x000CEEEF File Offset: 0x000CD0EF
		// (set) Token: 0x06003A6B RID: 14955 RVA: 0x000CEEF7 File Offset: 0x000CD0F7
		public string ExternalId { get; protected set; }

		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x06003A6C RID: 14956 RVA: 0x000CEF00 File Offset: 0x000CD100
		// (set) Token: 0x06003A6D RID: 14957 RVA: 0x000CEF08 File Offset: 0x000CD108
		public string UserIdForTracing { get; protected set; }

		// Token: 0x06003A6E RID: 14958
		public abstract MailboxSession GetSystemMailboxSession(IdHeaderInformation headerInformation, bool unifiedLogon);

		// Token: 0x06003A6F RID: 14959
		public abstract MailboxSession GetMailboxSession(IdHeaderInformation headerInformation, bool unifiedLogon);

		// Token: 0x06003A70 RID: 14960
		public abstract PublicFolderSession GetPublicFolderSession(StoreId folderId, IdConverter.ConvertOption convertOption);

		// Token: 0x02000798 RID: 1944
		public class FromCallContext : IdConverterDependencies
		{
			// Token: 0x06003A72 RID: 14962 RVA: 0x000CEF1C File Offset: 0x000CD11C
			public FromCallContext(CallContext callContext)
			{
				this.callContext = callContext;
				base.IsOwa = this.callContext.IsOwa;
				base.IsExternalUser = this.callContext.IsExternalUser;
				base.IsWSSecurityUser = this.callContext.IsWSSecurityUser;
				base.ADRecipientSessionContext = this.callContext.ADRecipientSessionContext;
				ExternalCallContext externalCallContext = this.callContext as ExternalCallContext;
				base.ExternalId = ((externalCallContext == null) ? null : externalCallContext.ExternalId.ToString());
				base.UserIdForTracing = this.callContext.ToString();
			}

			// Token: 0x06003A73 RID: 14963 RVA: 0x000CEFB8 File Offset: 0x000CD1B8
			public override MailboxSession GetSystemMailboxSession(IdHeaderInformation headerInformation, bool unifiedLogon)
			{
				SessionAndAuthZ systemMailboxSessionAndAuthZ = this.callContext.SessionCache.GetSystemMailboxSessionAndAuthZ(headerInformation.MailboxId, unifiedLogon);
				return systemMailboxSessionAndAuthZ.Session as MailboxSession;
			}

			// Token: 0x06003A74 RID: 14964 RVA: 0x000CEFE8 File Offset: 0x000CD1E8
			public override MailboxSession GetMailboxSession(IdHeaderInformation headerInformation, bool unifiedLogon)
			{
				return this.callContext.SessionCache.GetMailboxSessionByMailboxId(headerInformation.MailboxId, unifiedLogon);
			}

			// Token: 0x06003A75 RID: 14965 RVA: 0x000CF004 File Offset: 0x000CD204
			public override PublicFolderSession GetPublicFolderSession(StoreId folderId, IdConverter.ConvertOption convertOption)
			{
				return IdConverter.GetPublicFolderSession(folderId, this.callContext, convertOption);
			}

			// Token: 0x04002097 RID: 8343
			private CallContext callContext;
		}

		// Token: 0x02000799 RID: 1945
		public class FromRawData : IdConverterDependencies
		{
			// Token: 0x06003A76 RID: 14966 RVA: 0x000CF020 File Offset: 0x000CD220
			public FromRawData(bool isExternalUser, bool isWSSecurityUser, ADRecipientSessionContext adRecipientSessionContext, string externalId, string userIdForTracing, MailboxSession systemMailboxSession, MailboxSession mailboxSession, PublicFolderSession publicFolderSession)
			{
				base.IsExternalUser = isExternalUser;
				base.IsWSSecurityUser = isWSSecurityUser;
				base.ADRecipientSessionContext = adRecipientSessionContext;
				base.ExternalId = externalId;
				base.UserIdForTracing = userIdForTracing;
				this.systemMailboxSession = systemMailboxSession;
				this.mailboxSession = mailboxSession;
				this.publicFolderSession = publicFolderSession;
			}

			// Token: 0x06003A77 RID: 14967 RVA: 0x000CF070 File Offset: 0x000CD270
			public override MailboxSession GetSystemMailboxSession(IdHeaderInformation headerInformation, bool unifiedLogon)
			{
				return this.systemMailboxSession;
			}

			// Token: 0x06003A78 RID: 14968 RVA: 0x000CF078 File Offset: 0x000CD278
			public override MailboxSession GetMailboxSession(IdHeaderInformation headerInformation, bool unifiedLogon)
			{
				return this.mailboxSession;
			}

			// Token: 0x06003A79 RID: 14969 RVA: 0x000CF080 File Offset: 0x000CD280
			public override PublicFolderSession GetPublicFolderSession(StoreId folderId, IdConverter.ConvertOption convertOption)
			{
				return this.publicFolderSession;
			}

			// Token: 0x04002098 RID: 8344
			private MailboxSession systemMailboxSession;

			// Token: 0x04002099 RID: 8345
			private MailboxSession mailboxSession;

			// Token: 0x0400209A RID: 8346
			private PublicFolderSession publicFolderSession;
		}
	}
}
