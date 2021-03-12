using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000052 RID: 82
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class ServerObject : BaseObject, IServerObject
	{
		// Token: 0x06000315 RID: 789 RVA: 0x00019810 File Offset: 0x00017A10
		protected ServerObject()
		{
			this.logon = null;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0001981F File Offset: 0x00017A1F
		protected ServerObject(Logon logon)
		{
			Util.ThrowOnNullArgument(logon, "logon");
			this.logon = logon;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00019839 File Offset: 0x00017A39
		public Logon LogonObject
		{
			get
			{
				if (this.logon == null)
				{
					this.logon = (Logon)this;
				}
				return this.logon;
			}
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00019855 File Offset: 0x00017A55
		internal virtual void OnAccess()
		{
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000319 RID: 793 RVA: 0x00019857 File Offset: 0x00017A57
		public virtual Encoding String8Encoding
		{
			get
			{
				return this.LogonObject.LogonString8Encoding;
			}
		}

		// Token: 0x04000118 RID: 280
		private Logon logon;
	}
}
