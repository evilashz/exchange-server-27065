using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C84 RID: 3204
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct AuthIdentity : IDisposable
	{
		// Token: 0x060046D6 RID: 18134 RVA: 0x000BE568 File Offset: 0x000BC768
		internal AuthIdentity(string userName, SecureString password, string domain)
		{
			this.UserName = userName;
			this.UserNameLength = (string.IsNullOrEmpty(userName) ? 0 : userName.Length);
			this.Domain = domain;
			this.DomainLength = (string.IsNullOrEmpty(domain) ? 0 : domain.Length);
			if (password == null)
			{
				this.Password = IntPtr.Zero;
				this.PasswordLength = 0;
			}
			else
			{
				this.Password = Marshal.SecureStringToGlobalAllocUnicode(password);
				this.PasswordLength = password.Length;
			}
			this.Flags = 2;
		}

		// Token: 0x060046D7 RID: 18135 RVA: 0x000BE5E7 File Offset: 0x000BC7E7
		public void Dispose()
		{
			if (this.Password != IntPtr.Zero)
			{
				Marshal.ZeroFreeGlobalAllocUnicode(this.Password);
			}
		}

		// Token: 0x060046D8 RID: 18136 RVA: 0x000BE606 File Offset: 0x000BC806
		public override string ToString()
		{
			return ValidationHelper.ToString(this.Domain) + "\\" + ValidationHelper.ToString(this.UserName);
		}

		// Token: 0x04003BB3 RID: 15283
		internal string UserName;

		// Token: 0x04003BB4 RID: 15284
		internal int UserNameLength;

		// Token: 0x04003BB5 RID: 15285
		internal string Domain;

		// Token: 0x04003BB6 RID: 15286
		internal int DomainLength;

		// Token: 0x04003BB7 RID: 15287
		internal IntPtr Password;

		// Token: 0x04003BB8 RID: 15288
		internal int PasswordLength;

		// Token: 0x04003BB9 RID: 15289
		internal int Flags;

		// Token: 0x04003BBA RID: 15290
		public static AuthIdentity Default = new AuthIdentity(null, null, null);

		// Token: 0x04003BBB RID: 15291
		public static AuthIdentity LocalMachine = new AuthIdentity(Environment.MachineName + '$', null, null);
	}
}
