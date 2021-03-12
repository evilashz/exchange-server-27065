using System;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200070F RID: 1807
	[Serializable]
	public class FederatedDomain
	{
		// Token: 0x060054FB RID: 21755 RVA: 0x00133427 File Offset: 0x00131627
		public FederatedDomain(SmtpDomain domain)
		{
			this.Domain = domain;
		}

		// Token: 0x060054FC RID: 21756 RVA: 0x00133436 File Offset: 0x00131636
		public FederatedDomain(SmtpDomain domain, DomainState state)
		{
			this.Domain = domain;
			this.State = state;
			this.containsExtendedInfo = true;
		}

		// Token: 0x17001C57 RID: 7255
		// (get) Token: 0x060054FD RID: 21757 RVA: 0x00133453 File Offset: 0x00131653
		// (set) Token: 0x060054FE RID: 21758 RVA: 0x0013345B File Offset: 0x0013165B
		public SmtpDomain Domain { get; private set; }

		// Token: 0x17001C58 RID: 7256
		// (get) Token: 0x060054FF RID: 21759 RVA: 0x00133464 File Offset: 0x00131664
		// (set) Token: 0x06005500 RID: 21760 RVA: 0x0013346C File Offset: 0x0013166C
		public DomainState State { get; private set; }

		// Token: 0x06005501 RID: 21761 RVA: 0x00133478 File Offset: 0x00131678
		public override string ToString()
		{
			if (this.Domain == null || string.IsNullOrEmpty(this.Domain.Domain))
			{
				return string.Empty;
			}
			if (this.containsExtendedInfo)
			{
				return this.Domain + "=" + this.State.ToString();
			}
			return this.Domain.ToString();
		}

		// Token: 0x0400390C RID: 14604
		private bool containsExtendedInfo;
	}
}
