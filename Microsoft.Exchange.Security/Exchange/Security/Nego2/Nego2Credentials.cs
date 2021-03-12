using System;
using System.Globalization;
using System.Net;

namespace Microsoft.Exchange.Security.Nego2
{
	// Token: 0x02000027 RID: 39
	public class Nego2Credentials : ICredentials
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x000092D3 File Offset: 0x000074D3
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x000092DB File Offset: 0x000074DB
		public string UserName { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x000092E4 File Offset: 0x000074E4
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x000092EC File Offset: 0x000074EC
		public string Password { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x000092F5 File Offset: 0x000074F5
		// (set) Token: 0x060000FA RID: 250 RVA: 0x000092FD File Offset: 0x000074FD
		public bool IsBusinessInstance { get; private set; }

		// Token: 0x060000FB RID: 251 RVA: 0x00009306 File Offset: 0x00007506
		public Nego2Credentials(string userName, string password) : this(userName, password, false)
		{
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00009311 File Offset: 0x00007511
		public Nego2Credentials(string userName, string password, bool isBusinessInstance)
		{
			this.UserName = userName;
			this.Password = password;
			this.IsBusinessInstance = isBusinessInstance;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000932E File Offset: 0x0000752E
		public NetworkCredential GetCredential(Uri uri, string authType)
		{
			return this.GetAuthBuffer<NetworkCredential>(new Func<IntPtr, NetworkCredential>(Nego2NativeHelper.GetCredential));
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00009342 File Offset: 0x00007542
		public NetworkCredential GetCredential()
		{
			return this.GetAuthBuffer<NetworkCredential>(new Func<IntPtr, NetworkCredential>(Nego2NativeHelper.GetCredential));
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00009358 File Offset: 0x00007558
		private TResult GetAuthBuffer<TResult>(Func<IntPtr, TResult> create)
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			TResult result;
			try
			{
				Nego2NativeHelper.CreateLiveClientAuthBufferWithPlainPassword(this.UserName, this.Password, 0U, this.IsBusinessInstance, out zero, out zero2);
				result = create(zero);
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					int num = Nego2NativeHelper.FreeAuthBuffer(zero);
					if (num < 0)
					{
						throw new Exception(string.Format(CultureInfo.InvariantCulture, "Failure deallocating the authentication buffer. Error: {0}", new object[]
						{
							num
						}));
					}
				}
			}
			return result;
		}
	}
}
