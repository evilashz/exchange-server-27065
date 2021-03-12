using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001BE RID: 446
	[Serializable]
	public sealed class OwaWin32Exception : OwaPermanentException
	{
		// Token: 0x06000F19 RID: 3865 RVA: 0x0005E800 File Offset: 0x0005CA00
		public OwaWin32Exception(int lastError, string message, Exception innerException) : base(string.Format("{0}, GetLastError()={1}", string.IsNullOrEmpty(message) ? "<n/a>" : message, lastError.ToString()), innerException)
		{
			this.lastError = lastError;
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0005E831 File Offset: 0x0005CA31
		public OwaWin32Exception(int lastError, string message) : this(lastError, message, null)
		{
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000F1B RID: 3867 RVA: 0x0005E83C File Offset: 0x0005CA3C
		public int LastError
		{
			get
			{
				return this.lastError;
			}
		}

		// Token: 0x04000A25 RID: 2597
		private int lastError;
	}
}
