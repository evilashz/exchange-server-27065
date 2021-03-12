using System;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x0200009F RID: 159
	public class PolicyConfigChangeEventArgs : EventArgs
	{
		// Token: 0x060003FA RID: 1018 RVA: 0x0000C779 File Offset: 0x0000A979
		public PolicyConfigChangeEventArgs(PolicyConfigProvider sender, PolicyConfigBase policyConfig, ChangeType changeType)
		{
			ArgumentValidator.ThrowIfNull("sender", sender);
			ArgumentValidator.ThrowIfNull("policyConfig", policyConfig);
			this.Sender = sender;
			this.PolicyConfig = policyConfig;
			this.ChangeType = changeType;
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000C7AC File Offset: 0x0000A9AC
		// (set) Token: 0x060003FC RID: 1020 RVA: 0x0000C7B4 File Offset: 0x0000A9B4
		public ChangeType ChangeType { get; private set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0000C7BD File Offset: 0x0000A9BD
		// (set) Token: 0x060003FE RID: 1022 RVA: 0x0000C7C5 File Offset: 0x0000A9C5
		public PolicyConfigBase PolicyConfig { get; private set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000C7CE File Offset: 0x0000A9CE
		// (set) Token: 0x06000400 RID: 1024 RVA: 0x0000C7D6 File Offset: 0x0000A9D6
		public PolicyConfigProvider Sender { get; set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0000C7DF File Offset: 0x0000A9DF
		// (set) Token: 0x06000402 RID: 1026 RVA: 0x0000C7E7 File Offset: 0x0000A9E7
		public bool Handled { get; set; }
	}
}
