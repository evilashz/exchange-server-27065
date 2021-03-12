using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectoryV142
{
	// Token: 0x020005E6 RID: 1510
	public class PasswordProfile
	{
		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x060018FD RID: 6397 RVA: 0x00030008 File Offset: 0x0002E208
		// (set) Token: 0x060018FE RID: 6398 RVA: 0x00030010 File Offset: 0x0002E210
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string password
		{
			get
			{
				return this._password;
			}
			set
			{
				this._password = value;
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x060018FF RID: 6399 RVA: 0x00030019 File Offset: 0x0002E219
		// (set) Token: 0x06001900 RID: 6400 RVA: 0x00030021 File Offset: 0x0002E221
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public bool? forceChangePasswordNextLogin
		{
			get
			{
				return this._forceChangePasswordNextLogin;
			}
			set
			{
				this._forceChangePasswordNextLogin = value;
			}
		}

		// Token: 0x04001B55 RID: 6997
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _password;

		// Token: 0x04001B56 RID: 6998
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _forceChangePasswordNextLogin;
	}
}
