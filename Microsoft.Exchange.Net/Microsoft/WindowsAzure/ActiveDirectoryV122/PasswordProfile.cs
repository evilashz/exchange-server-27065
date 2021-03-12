using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectoryV122
{
	// Token: 0x020005BC RID: 1468
	public class PasswordProfile
	{
		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001663 RID: 5731 RVA: 0x0002DF72 File Offset: 0x0002C172
		// (set) Token: 0x06001664 RID: 5732 RVA: 0x0002DF7A File Offset: 0x0002C17A
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

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001665 RID: 5733 RVA: 0x0002DF83 File Offset: 0x0002C183
		// (set) Token: 0x06001666 RID: 5734 RVA: 0x0002DF8B File Offset: 0x0002C18B
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

		// Token: 0x04001A25 RID: 6693
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _password;

		// Token: 0x04001A26 RID: 6694
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _forceChangePasswordNextLogin;
	}
}
