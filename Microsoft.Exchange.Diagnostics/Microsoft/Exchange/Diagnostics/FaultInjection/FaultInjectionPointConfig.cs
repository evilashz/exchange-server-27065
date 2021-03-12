using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Diagnostics.FaultInjection
{
	// Token: 0x0200008B RID: 139
	internal class FaultInjectionPointConfig
	{
		// Token: 0x06000322 RID: 802 RVA: 0x0000B2F8 File Offset: 0x000094F8
		internal FaultInjectionPointConfig(FaultInjectionType type, List<string> parameters)
		{
			this.type = type;
			this.parameters = parameters;
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000B30E File Offset: 0x0000950E
		public FaultInjectionType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000B316 File Offset: 0x00009516
		public List<string> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x040002EE RID: 750
		private readonly FaultInjectionType type;

		// Token: 0x040002EF RID: 751
		private List<string> parameters;
	}
}
