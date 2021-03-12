using System;
using System.Security.Principal;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000862 RID: 2146
	[Serializable]
	internal class CallContextSecurityData : ICloneable
	{
		// Token: 0x17000FE0 RID: 4064
		// (get) Token: 0x06005BBE RID: 23486 RVA: 0x00140E0B File Offset: 0x0013F00B
		// (set) Token: 0x06005BBF RID: 23487 RVA: 0x00140E13 File Offset: 0x0013F013
		internal IPrincipal Principal
		{
			get
			{
				return this._principal;
			}
			set
			{
				this._principal = value;
			}
		}

		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x06005BC0 RID: 23488 RVA: 0x00140E1C File Offset: 0x0013F01C
		internal bool HasInfo
		{
			get
			{
				return this._principal != null;
			}
		}

		// Token: 0x06005BC1 RID: 23489 RVA: 0x00140E28 File Offset: 0x0013F028
		public object Clone()
		{
			return new CallContextSecurityData
			{
				_principal = this._principal
			};
		}

		// Token: 0x04002927 RID: 10535
		private IPrincipal _principal;
	}
}
