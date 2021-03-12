using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200001A RID: 26
	[Serializable]
	public abstract class PropertyDefinition
	{
		// Token: 0x060000FA RID: 250 RVA: 0x000050A9 File Offset: 0x000032A9
		protected PropertyDefinition(string name, Type type)
		{
			this.name = name;
			this.type = type;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000FB RID: 251 RVA: 0x000050BF File Offset: 0x000032BF
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000FC RID: 252 RVA: 0x000050C7 File Offset: 0x000032C7
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000050CF File Offset: 0x000032CF
		public virtual ICollection<PropertyDefinition> RequiredPropertyDefinitionsWhenReading
		{
			get
			{
				if (this.requiredPropertyDefinitionsWhenReading == null)
				{
					this.requiredPropertyDefinitionsWhenReading = new List<PropertyDefinition>(1);
					this.requiredPropertyDefinitionsWhenReading.Add(this);
				}
				return this.requiredPropertyDefinitionsWhenReading;
			}
		}

		// Token: 0x04000051 RID: 81
		private string name;

		// Token: 0x04000052 RID: 82
		private Type type;

		// Token: 0x04000053 RID: 83
		private ICollection<PropertyDefinition> requiredPropertyDefinitionsWhenReading;
	}
}
