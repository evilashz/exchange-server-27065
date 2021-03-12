using System;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000013 RID: 19
	internal sealed class SimplePropertyDefinition : PropertyDefinition
	{
		// Token: 0x0600007E RID: 126 RVA: 0x00002F45 File Offset: 0x00001145
		internal SimplePropertyDefinition(string name, Type type, PropertyFlag flags) : base(name, type, flags)
		{
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002F50 File Offset: 0x00001150
		internal SimplePropertyDefinition(string name, Type type) : base(name, type, PropertyFlag.None)
		{
		}
	}
}
