using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Assistants.Diagnostics
{
	// Token: 0x02000099 RID: 153
	internal abstract class DiagnosticsProcessorBase
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x00018683 File Offset: 0x00016883
		protected DiagnosticsArgument Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0001868B File Offset: 0x0001688B
		protected DiagnosticsProcessorBase(DiagnosticsArgument arguments)
		{
			ArgumentValidator.ThrowIfNull("arguments", arguments);
			this.arguments = arguments;
		}

		// Token: 0x040002B3 RID: 691
		private readonly DiagnosticsArgument arguments;
	}
}
