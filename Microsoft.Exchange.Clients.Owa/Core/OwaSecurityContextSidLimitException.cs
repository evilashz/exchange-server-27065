using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001BF RID: 447
	[Serializable]
	public sealed class OwaSecurityContextSidLimitException : OwaPermanentException
	{
		// Token: 0x06000F1C RID: 3868 RVA: 0x0005E844 File Offset: 0x0005CA44
		public OwaSecurityContextSidLimitException(string message, string name, string authenticationType) : base(message)
		{
			this.name = name;
			this.authenticationType = authenticationType;
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x0005E85B File Offset: 0x0005CA5B
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x0005E863 File Offset: 0x0005CA63
		public string AuthenticationType
		{
			get
			{
				return this.authenticationType;
			}
		}

		// Token: 0x04000A26 RID: 2598
		private string name;

		// Token: 0x04000A27 RID: 2599
		private string authenticationType;
	}
}
