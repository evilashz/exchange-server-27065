using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000121 RID: 289
	[Serializable]
	public sealed class OwaSecurityContextSidLimitException : OwaPermanentException
	{
		// Token: 0x060009C9 RID: 2505 RVA: 0x00022C94 File Offset: 0x00020E94
		public OwaSecurityContextSidLimitException(string message, string name, string authenticationType) : base(message)
		{
			this.name = name;
			this.authenticationType = authenticationType;
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x00022CAB File Offset: 0x00020EAB
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x00022CB3 File Offset: 0x00020EB3
		public string AuthenticationType
		{
			get
			{
				return this.authenticationType;
			}
		}

		// Token: 0x0400069C RID: 1692
		private readonly string name;

		// Token: 0x0400069D RID: 1693
		private readonly string authenticationType;
	}
}
