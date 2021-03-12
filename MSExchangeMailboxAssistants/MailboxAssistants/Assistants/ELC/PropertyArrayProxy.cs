using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200004A RID: 74
	internal class PropertyArrayProxy
	{
		// Token: 0x060002AC RID: 684 RVA: 0x0001004A File Offset: 0x0000E24A
		public PropertyArrayProxy(PropertyDefinitionArray propertyDefinitions, object[] rawProperties)
		{
			if (propertyDefinitions == null)
			{
				throw new ArgumentNullException("propertyDefinitions");
			}
			if (rawProperties == null)
			{
				throw new ArgumentNullException("rawProperties");
			}
			this.PropertyDefinitions = propertyDefinitions;
			this.RawProperties = rawProperties;
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0001007C File Offset: 0x0000E27C
		// (set) Token: 0x060002AE RID: 686 RVA: 0x00010084 File Offset: 0x0000E284
		public PropertyDefinitionArray PropertyDefinitions { get; private set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0001008D File Offset: 0x0000E28D
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x00010095 File Offset: 0x0000E295
		public object[] RawProperties { get; private set; }

		// Token: 0x170000A7 RID: 167
		public object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				if (propertyDefinition == null)
				{
					throw new ArgumentNullException("propertyDefinition");
				}
				return this.RawProperties[this.PropertyDefinitions[propertyDefinition]];
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x000100C1 File Offset: 0x0000E2C1
		public T GetProperty<T>(PropertyDefinition propertyDefinition)
		{
			return (T)((object)this[propertyDefinition]);
		}
	}
}
