using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020000A0 RID: 160
	public class DeprecatedAttribute : Attribute
	{
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x00012C65 File Offset: 0x00010E65
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x00012C6D File Offset: 0x00010E6D
		public ExchangeVersionType Version { get; set; }

		// Token: 0x060003BA RID: 954 RVA: 0x00012C76 File Offset: 0x00010E76
		public DeprecatedAttribute(ExchangeVersionType version)
		{
			this.Version = version;
		}
	}
}
