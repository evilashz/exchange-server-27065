using System;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Security
{
	// Token: 0x02000255 RID: 597
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ClientIdentityInfo
	{
		// Token: 0x06000A9B RID: 2715 RVA: 0x000338C4 File Offset: 0x00031AC4
		public ClientIdentityInfo(IntPtr __hAuthZ, SecurityIdentifier __sidUser, SecurityIdentifier __sidGroup)
		{
			this._hAuthZ = __hAuthZ;
			this._sidUser = __sidUser;
			this._sidPrimaryGroup = __sidGroup;
			this._hToken = IntPtr.Zero;
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x000338EC File Offset: 0x00031AEC
		internal ClientIdentityInfo(IntPtr __hToken)
		{
			this._hAuthZ = IntPtr.Zero;
			this._sidUser = null;
			this._sidPrimaryGroup = null;
			this._hToken = __hToken;
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x00033914 File Offset: 0x00031B14
		public IntPtr hAuthZ
		{
			get
			{
				return this._hAuthZ;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x0003391C File Offset: 0x00031B1C
		public SecurityIdentifier sidPrimaryGroup
		{
			get
			{
				return this._sidPrimaryGroup;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x00033924 File Offset: 0x00031B24
		public SecurityIdentifier sidUser
		{
			get
			{
				return this._sidUser;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x0003392C File Offset: 0x00031B2C
		internal IntPtr hToken
		{
			get
			{
				return this._hToken;
			}
		}

		// Token: 0x04001085 RID: 4229
		private IntPtr _hAuthZ;

		// Token: 0x04001086 RID: 4230
		private SecurityIdentifier _sidUser;

		// Token: 0x04001087 RID: 4231
		private SecurityIdentifier _sidPrimaryGroup;

		// Token: 0x04001088 RID: 4232
		private IntPtr _hToken;
	}
}
