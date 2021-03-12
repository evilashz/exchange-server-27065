using System;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000010 RID: 16
	internal abstract class PropertyMapping
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00002EF8 File Offset: 0x000010F8
		protected PropertyMapping(PropertyDefinition property)
		{
			this.PropertyDefinition = property;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002F07 File Offset: 0x00001107
		public PropertyDefinition GenericPropertyDefinition
		{
			get
			{
				return this.PropertyDefinition;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000075 RID: 117
		public abstract bool IsReadOnly { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000076 RID: 118
		public abstract bool IsStreamable { get; }

		// Token: 0x04000032 RID: 50
		protected readonly PropertyDefinition PropertyDefinition;
	}
}
