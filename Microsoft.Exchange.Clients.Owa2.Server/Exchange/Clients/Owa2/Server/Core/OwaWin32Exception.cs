using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000128 RID: 296
	[Serializable]
	public sealed class OwaWin32Exception : OwaPermanentException
	{
		// Token: 0x060009D8 RID: 2520 RVA: 0x00022D5E File Offset: 0x00020F5E
		public OwaWin32Exception(int lastError, string message, Exception innerException) : base(string.Format("{0}, GetLastError()={1}", string.IsNullOrEmpty(message) ? "<n/a>" : message, lastError.ToString()), innerException)
		{
			this.lastError = lastError;
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x00022D8F File Offset: 0x00020F8F
		public OwaWin32Exception(int lastError, string message) : this(lastError, message, null)
		{
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x00022D9A File Offset: 0x00020F9A
		public int LastError
		{
			get
			{
				return this.lastError;
			}
		}

		// Token: 0x040006A0 RID: 1696
		private readonly int lastError;
	}
}
