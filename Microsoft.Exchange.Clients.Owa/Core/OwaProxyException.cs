using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001B5 RID: 437
	[Serializable]
	public sealed class OwaProxyException : OwaPermanentException
	{
		// Token: 0x06000F08 RID: 3848 RVA: 0x0005E747 File Offset: 0x0005C947
		public OwaProxyException(string message, string localizedError, Exception innerException, bool hideDebugInformation) : base(message, innerException)
		{
			this.localizedError = localizedError;
			this.hideDebugInformation = hideDebugInformation;
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x0005E767 File Offset: 0x0005C967
		public OwaProxyException(string message, string localizedError) : this(message, localizedError, null, true)
		{
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x0005E773 File Offset: 0x0005C973
		public string LocalizedError
		{
			get
			{
				return this.localizedError;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000F0B RID: 3851 RVA: 0x0005E77B File Offset: 0x0005C97B
		public bool HideDebugInformation
		{
			get
			{
				return this.hideDebugInformation;
			}
		}

		// Token: 0x04000A23 RID: 2595
		private string localizedError;

		// Token: 0x04000A24 RID: 2596
		private bool hideDebugInformation = true;
	}
}
