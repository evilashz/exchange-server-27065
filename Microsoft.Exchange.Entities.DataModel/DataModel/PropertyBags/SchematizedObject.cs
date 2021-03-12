using System;

namespace Microsoft.Exchange.Entities.DataModel.PropertyBags
{
	// Token: 0x0200000E RID: 14
	public abstract class SchematizedObject<TSchema> : PropertyChangeTrackingObject, ISchematizedObject<TSchema>, IPropertyChangeTracker<PropertyDefinition> where TSchema : TypeSchema, new()
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000025CA File Offset: 0x000007CA
		// (set) Token: 0x06000034 RID: 52 RVA: 0x000025D1 File Offset: 0x000007D1
		public static TSchema SchemaInstance { get; private set; } = Activator.CreateInstance<TSchema>();

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000025D9 File Offset: 0x000007D9
		public TSchema Schema
		{
			get
			{
				return SchematizedObject<TSchema>.SchemaInstance;
			}
		}
	}
}
