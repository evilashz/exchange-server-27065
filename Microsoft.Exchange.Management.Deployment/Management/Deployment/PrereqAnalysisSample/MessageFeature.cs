using System;
using Microsoft.Exchange.Management.Deployment.Analysis;

namespace Microsoft.Exchange.Management.Deployment.PrereqAnalysisSample
{
	// Token: 0x02000075 RID: 117
	internal sealed class MessageFeature : Feature
	{
		// Token: 0x06000A85 RID: 2693 RVA: 0x000267A0 File Offset: 0x000249A0
		public MessageFeature(Func<Result, string> textFunction)
		{
			this.textFunction = textFunction;
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x000267AF File Offset: 0x000249AF
		public Func<Result, string> TextFunction
		{
			get
			{
				return this.textFunction;
			}
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x000267B7 File Offset: 0x000249B7
		public string Text(Result result)
		{
			return this.TextFunction(result);
		}

		// Token: 0x040005C5 RID: 1477
		private readonly Func<Result, string> textFunction;
	}
}
