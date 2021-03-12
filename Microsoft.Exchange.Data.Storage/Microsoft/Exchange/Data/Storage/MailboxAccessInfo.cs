using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Mapi.Security;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200028B RID: 651
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxAccessInfo
	{
		// Token: 0x06001B1F RID: 6943 RVA: 0x0007DAAD File Offset: 0x0007BCAD
		public MailboxAccessInfo(ClientSecurityContext authenticatedUserContext)
		{
			Util.ThrowOnNullArgument(authenticatedUserContext, "authenticatedUserContext");
			this.Initialize(authenticatedUserContext, null, null, null, null, null);
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x0007DACC File Offset: 0x0007BCCC
		public MailboxAccessInfo(AuthzContextHandle authenticatedUserHandle)
		{
			Util.ThrowOnNullArgument(authenticatedUserHandle, "authenticatedUserHandle");
			this.Initialize(null, authenticatedUserHandle, null, null, null, null);
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x0007DAEB File Offset: 0x0007BCEB
		public MailboxAccessInfo(string accessingUserDn, ClientSecurityContext authenticatedUserContext)
		{
			Util.ThrowOnNullOrEmptyArgument(accessingUserDn, "accessingUserDn");
			Util.ThrowOnNullArgument(authenticatedUserContext, "authenticatedUserContext");
			this.Initialize(authenticatedUserContext, null, null, accessingUserDn, null, null);
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x0007DB15 File Offset: 0x0007BD15
		public MailboxAccessInfo(string accessingUserDn, AuthzContextHandle authenticatedUserHandle)
		{
			Util.ThrowOnNullOrEmptyArgument(accessingUserDn, "accessingUserDn");
			Util.ThrowOnNullArgument(authenticatedUserHandle, "authenticatedUserHandle");
			this.Initialize(null, authenticatedUserHandle, null, accessingUserDn, null, null);
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x0007DB3F File Offset: 0x0007BD3F
		public MailboxAccessInfo(string accessingUserDn, ClientIdentityInfo clientIdentityInfo)
		{
			Util.ThrowOnNullOrEmptyArgument(accessingUserDn, "accessingUserDn");
			Util.ThrowOnNullArgument(clientIdentityInfo, "clientIdentityInfo");
			this.Initialize(null, null, clientIdentityInfo, accessingUserDn, null, null);
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x0007DB69 File Offset: 0x0007BD69
		private void Initialize(ClientSecurityContext context, AuthzContextHandle handle, ClientIdentityInfo clientIdentityInfo, string userDn, IADOrgPerson adEntry, GenericIdentity auxiliaryIdentity)
		{
			this.context = context;
			this.authzContextHandle = handle;
			this.clientIdentityInfo = clientIdentityInfo;
			this.userDn = userDn;
			this.adEntry = adEntry;
			this.auxiliaryIdentity = auxiliaryIdentity;
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06001B25 RID: 6949 RVA: 0x0007DB98 File Offset: 0x0007BD98
		public ClientSecurityContext AuthenticatedUserContext
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06001B26 RID: 6950 RVA: 0x0007DBA0 File Offset: 0x0007BDA0
		public AuthzContextHandle AuthenticatedUserHandle
		{
			get
			{
				return this.authzContextHandle;
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06001B27 RID: 6951 RVA: 0x0007DBA8 File Offset: 0x0007BDA8
		public ClientIdentityInfo ClientIdentityInfo
		{
			get
			{
				return this.clientIdentityInfo;
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06001B28 RID: 6952 RVA: 0x0007DBB0 File Offset: 0x0007BDB0
		public string AccessingUserDn
		{
			get
			{
				return this.userDn;
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06001B29 RID: 6953 RVA: 0x0007DBB8 File Offset: 0x0007BDB8
		public IADOrgPerson AccessingUserAdEntry
		{
			get
			{
				return this.adEntry;
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06001B2A RID: 6954 RVA: 0x0007DBC0 File Offset: 0x0007BDC0
		public GenericIdentity AuxiliaryIdentity
		{
			get
			{
				return this.auxiliaryIdentity;
			}
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x0007DBC8 File Offset: 0x0007BDC8
		public MailboxAccessInfo(WindowsPrincipal authenticatedUserPrincipal)
		{
			Util.ThrowOnNullArgument(authenticatedUserPrincipal, "authenticatedUserPrincipal");
			this.Initialize(authenticatedUserPrincipal, null, null, null, null, null, null);
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x0007DBE8 File Offset: 0x0007BDE8
		public MailboxAccessInfo(WindowsPrincipal authenticatedUserPrincipal, GenericIdentity auxiliaryIdentity)
		{
			Util.ThrowOnNullArgument(authenticatedUserPrincipal, "authenticatedUserPrincipal");
			this.Initialize(authenticatedUserPrincipal, null, null, null, null, null, auxiliaryIdentity);
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x0007DC08 File Offset: 0x0007BE08
		public MailboxAccessInfo(ClientSecurityContext authenticatedUserContext, GenericIdentity auxiliaryIdentity)
		{
			Util.ThrowOnNullArgument(authenticatedUserContext, "authenticatedUserContext");
			this.Initialize(null, authenticatedUserContext, null, null, null, null, auxiliaryIdentity);
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x0007DC28 File Offset: 0x0007BE28
		public MailboxAccessInfo(string accessingUserDn, WindowsPrincipal authenticatedUserPrincipal)
		{
			Util.ThrowOnNullOrEmptyArgument(accessingUserDn, "accessingUserDn");
			Util.ThrowOnNullArgument(authenticatedUserPrincipal, "authenticatedUserPrincipal");
			this.Initialize(authenticatedUserPrincipal, null, null, null, accessingUserDn, null, null);
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x0007DC53 File Offset: 0x0007BE53
		public MailboxAccessInfo(string accessingUserDn, ClientSecurityContext authenticatedUserContext, GenericIdentity auxiliaryIdentity)
		{
			Util.ThrowOnNullOrEmptyArgument(accessingUserDn, "accessingUserDn");
			Util.ThrowOnNullArgument(authenticatedUserContext, "authenticatedUserContext");
			this.Initialize(null, authenticatedUserContext, null, null, accessingUserDn, null, auxiliaryIdentity);
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x0007DC7E File Offset: 0x0007BE7E
		public MailboxAccessInfo(IADOrgPerson accessingUserAdEntry, WindowsPrincipal authenticatedUserPrincipal)
		{
			Util.ThrowOnNullArgument(accessingUserAdEntry, "accessingUserAdEntry");
			Util.ThrowOnNullArgument(authenticatedUserPrincipal, "authenticatedUserPrincipal");
			this.Initialize(authenticatedUserPrincipal, null, null, null, null, accessingUserAdEntry, null);
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x0007DCA9 File Offset: 0x0007BEA9
		public MailboxAccessInfo(IADOrgPerson accessingUserAdEntry, ClientSecurityContext authenticatedUserContext)
		{
			Util.ThrowOnNullArgument(accessingUserAdEntry, "accessingUserAdEntry");
			Util.ThrowOnNullArgument(authenticatedUserContext, "authenticatedUserContext");
			this.Initialize(null, authenticatedUserContext, null, null, null, accessingUserAdEntry, null);
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x0007DCD4 File Offset: 0x0007BED4
		public MailboxAccessInfo(IADOrgPerson accessingUserAdEntry, AuthzContextHandle authenticatedUserHandle)
		{
			Util.ThrowOnNullArgument(accessingUserAdEntry, "accessingUserAdEntry");
			Util.ThrowOnNullArgument(authenticatedUserHandle, "authenticatedUserHandle");
			this.Initialize(null, null, authenticatedUserHandle, null, null, accessingUserAdEntry, null);
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x0007DCFF File Offset: 0x0007BEFF
		public MailboxAccessInfo(IADOrgPerson accessingUserAdEntry, ClientIdentityInfo clientIdentityInfo)
		{
			Util.ThrowOnNullArgument(accessingUserAdEntry, "accessingUserAdEntry");
			Util.ThrowOnNullArgument(clientIdentityInfo, "clientIdentityInfo");
			this.Initialize(null, null, null, clientIdentityInfo, null, accessingUserAdEntry, null);
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x0007DD2A File Offset: 0x0007BF2A
		private void Initialize(WindowsPrincipal principal, ClientSecurityContext context, AuthzContextHandle handle, ClientIdentityInfo clientIdentityInfo, string userDn, IADOrgPerson adEntry, GenericIdentity auxiliaryIdentity)
		{
			this.principal = principal;
			this.Initialize(context, handle, clientIdentityInfo, userDn, adEntry, auxiliaryIdentity);
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06001B35 RID: 6965 RVA: 0x0007DD43 File Offset: 0x0007BF43
		public WindowsPrincipal AuthenticatedUserPrincipal
		{
			get
			{
				return this.principal;
			}
		}

		// Token: 0x040012E5 RID: 4837
		private ClientSecurityContext context;

		// Token: 0x040012E6 RID: 4838
		private AuthzContextHandle authzContextHandle;

		// Token: 0x040012E7 RID: 4839
		private ClientIdentityInfo clientIdentityInfo;

		// Token: 0x040012E8 RID: 4840
		private string userDn;

		// Token: 0x040012E9 RID: 4841
		private IADOrgPerson adEntry;

		// Token: 0x040012EA RID: 4842
		private GenericIdentity auxiliaryIdentity;

		// Token: 0x040012EB RID: 4843
		private WindowsPrincipal principal;
	}
}
