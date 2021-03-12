using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000037 RID: 55
	internal abstract class PoisonContext
	{
		// Token: 0x0600013F RID: 319 RVA: 0x000064DD File Offset: 0x000046DD
		internal PoisonContext(MessageProcessingSource msgSource)
		{
			this.source = msgSource;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000140 RID: 320 RVA: 0x000064EC File Offset: 0x000046EC
		internal MessageProcessingSource Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x040000AB RID: 171
		private MessageProcessingSource source;
	}
}
