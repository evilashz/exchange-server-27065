using System;
using System.Deployment.Internal.Isolation;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	// Token: 0x020000A4 RID: 164
	[ComVisible(false)]
	[Serializable]
	public sealed class ApplicationIdentity : ISerializable
	{
		// Token: 0x06000984 RID: 2436 RVA: 0x0001EF6C File Offset: 0x0001D16C
		private ApplicationIdentity()
		{
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0001EF74 File Offset: 0x0001D174
		[SecurityCritical]
		private ApplicationIdentity(SerializationInfo info, StreamingContext context)
		{
			string text = (string)info.GetValue("FullName", typeof(string));
			if (text == null)
			{
				throw new ArgumentNullException("fullName");
			}
			this._appId = IsolationInterop.AppIdAuthority.TextToDefinition(0U, text);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0001EFC2 File Offset: 0x0001D1C2
		[SecuritySafeCritical]
		public ApplicationIdentity(string applicationIdentityFullName)
		{
			if (applicationIdentityFullName == null)
			{
				throw new ArgumentNullException("applicationIdentityFullName");
			}
			this._appId = IsolationInterop.AppIdAuthority.TextToDefinition(0U, applicationIdentityFullName);
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0001EFEA File Offset: 0x0001D1EA
		[SecurityCritical]
		internal ApplicationIdentity(IDefinitionAppId applicationIdentity)
		{
			this._appId = applicationIdentity;
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x0001EFF9 File Offset: 0x0001D1F9
		public string FullName
		{
			[SecuritySafeCritical]
			get
			{
				return IsolationInterop.AppIdAuthority.DefinitionToText(0U, this._appId);
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x0001F00C File Offset: 0x0001D20C
		public string CodeBase
		{
			[SecuritySafeCritical]
			get
			{
				return this._appId.get_Codebase();
			}
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0001F019 File Offset: 0x0001D219
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x0001F021 File Offset: 0x0001D221
		internal IDefinitionAppId Identity
		{
			[SecurityCritical]
			get
			{
				return this._appId;
			}
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0001F029 File Offset: 0x0001D229
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("FullName", this.FullName, typeof(string));
		}

		// Token: 0x040003C7 RID: 967
		private IDefinitionAppId _appId;
	}
}
