using System;
using System.CodeDom.Compiler;

namespace Microsoft.WindowsAzure.ActiveDirectory
{
	// Token: 0x0200059E RID: 1438
	public class PasswordProfile
	{
		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x0002C1E6 File Offset: 0x0002A3E6
		// (set) Token: 0x06001409 RID: 5129 RVA: 0x0002C1EE File Offset: 0x0002A3EE
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

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x0002C1F7 File Offset: 0x0002A3F7
		// (set) Token: 0x0600140B RID: 5131 RVA: 0x0002C1FF File Offset: 0x0002A3FF
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

		// Token: 0x0400190D RID: 6413
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _password;

		// Token: 0x0400190E RID: 6414
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private bool? _forceChangePasswordNextLogin;
	}
}
