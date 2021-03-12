using System;
using Microsoft.Exchange.Collections;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200005C RID: 92
	internal abstract class ADPropertyUnionSchema : ADObjectSchema
	{
		// Token: 0x06000488 RID: 1160 RVA: 0x00019F24 File Offset: 0x00018124
		protected ADPropertyUnionSchema()
		{
			HashSet<PropertyDefinition> hashSet = new HashSet<PropertyDefinition>();
			HashSet<PropertyDefinition> hashSet2 = new HashSet<PropertyDefinition>();
			foreach (ObjectSchema objectSchema in this.ObjectSchemas)
			{
				foreach (PropertyDefinition item in objectSchema.AllProperties)
				{
					hashSet.TryAdd(item);
				}
				foreach (PropertyDefinition item2 in objectSchema.AllFilterOnlyProperties)
				{
					hashSet2.TryAdd(item2);
				}
			}
			base.AllProperties = new ReadOnlyCollection<PropertyDefinition>(hashSet.ToArray());
			base.AllFilterOnlyProperties = new ReadOnlyCollection<PropertyDefinition>(hashSet2.ToArray());
			base.InitializePropertyCollections();
			base.InitializeADObjectSchemaProperties();
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000489 RID: 1161
		public abstract ReadOnlyCollection<ADObjectSchema> ObjectSchemas { get; }
	}
}
