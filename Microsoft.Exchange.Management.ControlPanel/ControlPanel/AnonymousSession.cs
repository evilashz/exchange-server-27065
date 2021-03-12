using System;
using System.Security.Principal;
using System.Threading;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000373 RID: 883
	internal sealed class AnonymousSession : IRbacSession, IPrincipal, IIdentity
	{
		// Token: 0x0600302C RID: 12332 RVA: 0x00092FE9 File Offset: 0x000911E9
		private AnonymousSession()
		{
		}

		// Token: 0x17001F27 RID: 7975
		// (get) Token: 0x0600302D RID: 12333 RVA: 0x00092FF1 File Offset: 0x000911F1
		IIdentity IPrincipal.Identity
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600302E RID: 12334 RVA: 0x00092FF4 File Offset: 0x000911F4
		bool IPrincipal.IsInRole(string role)
		{
			return false;
		}

		// Token: 0x17001F28 RID: 7976
		// (get) Token: 0x0600302F RID: 12335 RVA: 0x00092FF7 File Offset: 0x000911F7
		string IIdentity.AuthenticationType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001F29 RID: 7977
		// (get) Token: 0x06003030 RID: 12336 RVA: 0x00092FFA File Offset: 0x000911FA
		bool IIdentity.IsAuthenticated
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001F2A RID: 7978
		// (get) Token: 0x06003031 RID: 12337 RVA: 0x00092FFD File Offset: 0x000911FD
		string IIdentity.Name
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003032 RID: 12338 RVA: 0x00093000 File Offset: 0x00091200
		void IRbacSession.RequestReceived()
		{
		}

		// Token: 0x06003033 RID: 12339 RVA: 0x00093002 File Offset: 0x00091202
		void IRbacSession.RequestCompleted()
		{
		}

		// Token: 0x06003034 RID: 12340 RVA: 0x00093004 File Offset: 0x00091204
		void IRbacSession.SetCurrentThreadPrincipal()
		{
			Thread.CurrentPrincipal = this;
		}

		// Token: 0x0400234F RID: 9039
		public static readonly IRbacSession Instance = new AnonymousSession();
	}
}
