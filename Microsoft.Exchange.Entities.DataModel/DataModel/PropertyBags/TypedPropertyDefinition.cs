using System;

namespace Microsoft.Exchange.Entities.DataModel.PropertyBags
{
	// Token: 0x020000A5 RID: 165
	public class TypedPropertyDefinition<TValue> : PropertyDefinition
	{
		// Token: 0x0600041C RID: 1052 RVA: 0x000076F4 File Offset: 0x000058F4
		public TypedPropertyDefinition(string name, TValue defaultValue = default(TValue), bool isLoggable = true) : base(name, typeof(TValue), isLoggable)
		{
			this.DefaultValue = defaultValue;
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0000770F File Offset: 0x0000590F
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x00007717 File Offset: 0x00005917
		public TValue DefaultValue { get; private set; }
	}
}
