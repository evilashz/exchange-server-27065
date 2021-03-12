using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000111 RID: 273
	[Serializable]
	public sealed class OwaExplicitLogonException : OwaPermanentException
	{
		// Token: 0x060009AC RID: 2476 RVA: 0x00022B3B File Offset: 0x00020D3B
		public OwaExplicitLogonException(string message, string localizedError, Exception innerException) : base(message, innerException)
		{
			this.localizedError = localizedError;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00022B4C File Offset: 0x00020D4C
		public OwaExplicitLogonException(string message, string localizedError) : this(message, localizedError, null)
		{
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x00022B57 File Offset: 0x00020D57
		public string LocalizedError
		{
			get
			{
				return this.localizedError;
			}
		}

		// Token: 0x0400069B RID: 1691
		private string localizedError;
	}
}
