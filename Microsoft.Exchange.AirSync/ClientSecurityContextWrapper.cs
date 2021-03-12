using System;
using System.Security.Principal;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000057 RID: 87
	internal class ClientSecurityContextWrapper : IDisposable
	{
		// Token: 0x060004BA RID: 1210 RVA: 0x0001D3F7 File Offset: 0x0001B5F7
		private ClientSecurityContextWrapper(ClientSecurityContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			this.clientSecurityContext = context;
			this.AddRef();
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0001D426 File Offset: 0x0001B626
		public static ClientSecurityContextWrapper FromWindowsIdentity(WindowsIdentity identity)
		{
			return new ClientSecurityContextWrapper(new ClientSecurityContext(identity));
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001D433 File Offset: 0x0001B633
		public static ClientSecurityContextWrapper FromIdentity(IIdentity identity)
		{
			return new ClientSecurityContextWrapper(ClientSecurityContextWrapper.ClientSecurityContextFromIdentity(identity));
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001D440 File Offset: 0x0001B640
		public static ClientSecurityContextWrapper FromClientSecurityContext(ClientSecurityContext context)
		{
			return new ClientSecurityContextWrapper(context);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0001D448 File Offset: 0x0001B648
		public int AddRef()
		{
			int result;
			lock (this.lockObject)
			{
				this.ThrowIfDisposed();
				result = ++this.refCount;
			}
			return result;
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0001D4A0 File Offset: 0x0001B6A0
		public AirSyncUserSecurityContext SerializedSecurityContext
		{
			get
			{
				if (this.serializedSecurityContext == null)
				{
					lock (this.lockObject)
					{
						this.ThrowIfDisposed();
						if (this.serializedSecurityContext == null)
						{
							this.serializedSecurityContext = new AirSyncUserSecurityContext();
							this.clientSecurityContext.SetSecurityAccessToken(this.serializedSecurityContext);
						}
					}
				}
				return this.serializedSecurityContext;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x0001D514 File Offset: 0x0001B714
		public SecurityIdentifier UserSid
		{
			get
			{
				return this.clientSecurityContext.UserSid;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x0001D524 File Offset: 0x0001B724
		public byte[] UserSidBytes
		{
			get
			{
				if (this.userSidBytes == null)
				{
					lock (this.lockObject)
					{
						if (this.userSidBytes == null)
						{
							this.userSidBytes = new byte[this.UserSid.BinaryLength];
							this.UserSid.GetBinaryForm(this.userSidBytes, 0);
						}
					}
				}
				return this.userSidBytes;
			}
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0001D59C File Offset: 0x0001B79C
		public void Dispose()
		{
			this.InternalDispose(true);
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x0001D5A5 File Offset: 0x0001B7A5
		public ClientSecurityContext ClientSecurityContext
		{
			get
			{
				return this.clientSecurityContext;
			}
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001D5B0 File Offset: 0x0001B7B0
		private static ClientSecurityContext ClientSecurityContextFromIdentity(IIdentity identity)
		{
			if (GlobalSettings.UseOAuthMasterSidForSecurityContext)
			{
				OAuthIdentity oauthIdentity = identity as OAuthIdentity;
				if (oauthIdentity != null && oauthIdentity.ActAsUser != null && oauthIdentity.ActAsUser.MasterAccountSid != null)
				{
					return new GenericSidIdentity(oauthIdentity.ActAsUser.MasterAccountSid.ToString(), oauthIdentity.AuthenticationType, oauthIdentity.ActAsUser.MasterAccountSid).CreateClientSecurityContext();
				}
			}
			return identity.CreateClientSecurityContext(true);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0001D61C File Offset: 0x0001B81C
		private void ThrowIfDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("ClientSecurityContextWrapper was already disposed.");
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0001D634 File Offset: 0x0001B834
		private void InternalDispose(bool fromDispose)
		{
			if (fromDispose && !this.disposed)
			{
				lock (this.lockObject)
				{
					if (!this.disposed)
					{
						this.refCount--;
						if (this.refCount <= 0 && this.clientSecurityContext != null)
						{
							this.clientSecurityContext.Dispose();
							this.clientSecurityContext = null;
							this.disposed = true;
							GC.SuppressFinalize(this);
						}
					}
				}
			}
		}

		// Token: 0x04000398 RID: 920
		private ClientSecurityContext clientSecurityContext;

		// Token: 0x04000399 RID: 921
		private AirSyncUserSecurityContext serializedSecurityContext;

		// Token: 0x0400039A RID: 922
		private object lockObject = new object();

		// Token: 0x0400039B RID: 923
		private volatile int refCount;

		// Token: 0x0400039C RID: 924
		private bool disposed;

		// Token: 0x0400039D RID: 925
		private byte[] userSidBytes;
	}
}
