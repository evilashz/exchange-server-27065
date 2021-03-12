using System;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x0200001B RID: 27
	public class AdfsConfigurationException : AdfsIdentityException
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00007635 File Offset: 0x00005835
		// (set) Token: 0x060000AC RID: 172 RVA: 0x0000763D File Offset: 0x0000583D
		public AdfsConfigErrorReason Reason { get; private set; }

		// Token: 0x060000AD RID: 173 RVA: 0x00007646 File Offset: 0x00005846
		public AdfsConfigurationException(AdfsConfigErrorReason reason, string message) : base(message)
		{
			this.Reason = reason;
		}
	}
}
