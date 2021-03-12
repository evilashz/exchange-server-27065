using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001C0 RID: 448
	[Serializable]
	public sealed class OwaExplicitLogonException : OwaPermanentException
	{
		// Token: 0x06000F1F RID: 3871 RVA: 0x0005E86B File Offset: 0x0005CA6B
		public OwaExplicitLogonException(string message, string localizedError, Exception innerException) : base(message, innerException)
		{
			this.localizedError = localizedError;
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x0005E87C File Offset: 0x0005CA7C
		public OwaExplicitLogonException(string message, string localizedError) : this(message, localizedError, null)
		{
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x0005E887 File Offset: 0x0005CA87
		public string LocalizedError
		{
			get
			{
				return this.localizedError;
			}
		}

		// Token: 0x04000A28 RID: 2600
		private string localizedError;
	}
}
