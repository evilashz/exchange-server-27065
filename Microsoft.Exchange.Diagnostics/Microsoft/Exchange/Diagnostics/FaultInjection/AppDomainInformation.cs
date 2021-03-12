using System;

namespace Microsoft.Exchange.Diagnostics.FaultInjection
{
	// Token: 0x0200008C RID: 140
	[Serializable]
	public class AppDomainInformation
	{
		// Token: 0x06000325 RID: 805 RVA: 0x0000B31E File Offset: 0x0000951E
		public AppDomainInformation(string appDomainName) : this(appDomainName, string.Empty)
		{
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000B32C File Offset: 0x0000952C
		public AppDomainInformation(string appDomainName, string identifier)
		{
			this.appDomainName = appDomainName;
			this.identifier = identifier;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000B342 File Offset: 0x00009542
		public string AppDomainName
		{
			get
			{
				return this.appDomainName;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000B34A File Offset: 0x0000954A
		public string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x040002F0 RID: 752
		private readonly string appDomainName;

		// Token: 0x040002F1 RID: 753
		private readonly string identifier;
	}
}
