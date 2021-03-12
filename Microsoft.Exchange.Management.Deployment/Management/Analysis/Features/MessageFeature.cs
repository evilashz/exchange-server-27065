using System;

namespace Microsoft.Exchange.Management.Analysis.Features
{
	// Token: 0x02000062 RID: 98
	internal class MessageFeature : Feature
	{
		// Token: 0x06000251 RID: 593 RVA: 0x00008475 File Offset: 0x00006675
		public MessageFeature(Func<Result, string> textFunction) : base(true, false)
		{
			this.TextFunction = textFunction;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00008486 File Offset: 0x00006686
		// (set) Token: 0x06000253 RID: 595 RVA: 0x0000848E File Offset: 0x0000668E
		public Func<Result, string> TextFunction { get; private set; }

		// Token: 0x06000254 RID: 596 RVA: 0x00008497 File Offset: 0x00006697
		public string Text(Result result)
		{
			return this.TextFunction(result);
		}
	}
}
