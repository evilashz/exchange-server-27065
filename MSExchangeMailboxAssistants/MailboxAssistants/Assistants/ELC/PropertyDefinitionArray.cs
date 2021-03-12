using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000049 RID: 73
	internal class PropertyDefinitionArray : Dictionary<PropertyDefinition, int>
	{
		// Token: 0x060002A9 RID: 681 RVA: 0x0000FFBC File Offset: 0x0000E1BC
		public PropertyDefinitionArray(PropertyDefinition[] propertyDefinitions)
		{
			if (propertyDefinitions == null)
			{
				throw new ArgumentNullException("propertyDefinitions");
			}
			foreach (PropertyDefinition key in propertyDefinitions)
			{
				base.Add(key, base.Count);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000FFFE File Offset: 0x0000E1FE
		public PropertyDefinition[] PropertyDefinitions
		{
			get
			{
				return base.Keys.ToArray<PropertyDefinition>();
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0001000B File Offset: 0x0000E20B
		public bool Replace(PropertyDefinition currentProp, PropertyDefinition newProp)
		{
			if (currentProp == null)
			{
				throw new ArgumentNullException("currentProp");
			}
			if (currentProp == null)
			{
				throw new ArgumentNullException("newProp");
			}
			if (base.ContainsKey(currentProp))
			{
				base.Remove(currentProp);
				base.Add(newProp, base.Count);
				return true;
			}
			return false;
		}
	}
}
