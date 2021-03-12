using System;
using Microsoft.Exchange.InfoWorker.Common.MailTips;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000506 RID: 1286
	public class RenderingDisabledResolver : IRenderingDisabledResolver
	{
		// Token: 0x06003DCC RID: 15820 RVA: 0x000B9DFA File Offset: 0x000B7FFA
		private RenderingDisabledResolver()
		{
		}

		// Token: 0x17002440 RID: 9280
		// (get) Token: 0x06003DCD RID: 15821 RVA: 0x000B9E02 File Offset: 0x000B8002
		// (set) Token: 0x06003DCE RID: 15822 RVA: 0x000B9E09 File Offset: 0x000B8009
		internal static IRenderingDisabledResolver Instance { get; set; } = new RenderingDisabledResolver();

		// Token: 0x17002441 RID: 9281
		// (get) Token: 0x06003DCF RID: 15823 RVA: 0x000B9E11 File Offset: 0x000B8011
		public bool value
		{
			get
			{
				return Utility.RenderingDisabled;
			}
		}
	}
}
