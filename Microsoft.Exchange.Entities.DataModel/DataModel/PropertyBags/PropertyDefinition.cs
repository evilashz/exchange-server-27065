using System;

namespace Microsoft.Exchange.Entities.DataModel.PropertyBags
{
	// Token: 0x020000A4 RID: 164
	public abstract class PropertyDefinition
	{
		// Token: 0x06000415 RID: 1045 RVA: 0x000076A4 File Offset: 0x000058A4
		protected PropertyDefinition(string name, Type valueType, bool isLoggable)
		{
			this.Name = name;
			this.ValueType = valueType;
			this.IsLoggable = isLoggable;
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x000076C1 File Offset: 0x000058C1
		// (set) Token: 0x06000417 RID: 1047 RVA: 0x000076C9 File Offset: 0x000058C9
		public string Name { get; private set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x000076D2 File Offset: 0x000058D2
		// (set) Token: 0x06000419 RID: 1049 RVA: 0x000076DA File Offset: 0x000058DA
		public Type ValueType { get; private set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x000076E3 File Offset: 0x000058E3
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x000076EB File Offset: 0x000058EB
		public bool IsLoggable { get; private set; }
	}
}
