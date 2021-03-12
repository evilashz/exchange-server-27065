using System;
using System.Collections.Generic;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000035 RID: 53
	internal class InferencePropertyBag
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x000037B8 File Offset: 0x000019B8
		public InferencePropertyBag()
		{
			this.propertyBag = new Dictionary<PropertyDefinition, object>();
		}

		// Token: 0x17000076 RID: 118
		public object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				Util.ThrowOnNullArgument(propertyDefinition, "propertyDefinition");
				return this.propertyBag[propertyDefinition];
			}
			set
			{
				Util.ThrowOnNullArgument(propertyDefinition, "propertyDefinition");
				this.propertyBag[propertyDefinition] = value;
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000037FE File Offset: 0x000019FE
		public void Add(PropertyDefinition propertyDefinition, object value)
		{
			Util.ThrowOnNullArgument(propertyDefinition, "propertyDefinition");
			this.propertyBag.Add(propertyDefinition, value);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00003818 File Offset: 0x00001A18
		public bool TryGetValue(PropertyDefinition propertyDefinition, out object value)
		{
			Util.ThrowOnNullArgument(propertyDefinition, "propertyDefinition");
			return this.propertyBag.TryGetValue(propertyDefinition, out value);
		}

		// Token: 0x040000DD RID: 221
		private readonly Dictionary<PropertyDefinition, object> propertyBag;
	}
}
