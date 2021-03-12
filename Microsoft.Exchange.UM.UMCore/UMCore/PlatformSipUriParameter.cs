using System;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002FF RID: 767
	internal class PlatformSipUriParameter
	{
		// Token: 0x0600175F RID: 5983 RVA: 0x00063811 File Offset: 0x00061A11
		public PlatformSipUriParameter(string name, string value)
		{
			ValidateArgument.NotNullOrEmpty(name, "name");
			ValidateArgument.NotNullOrEmpty(value, "value");
			this.Name = name;
			this.Value = value;
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001760 RID: 5984 RVA: 0x0006383D File Offset: 0x00061A3D
		// (set) Token: 0x06001761 RID: 5985 RVA: 0x00063845 File Offset: 0x00061A45
		public string Name { get; private set; }

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001762 RID: 5986 RVA: 0x0006384E File Offset: 0x00061A4E
		// (set) Token: 0x06001763 RID: 5987 RVA: 0x00063856 File Offset: 0x00061A56
		public string Value { get; private set; }
	}
}
