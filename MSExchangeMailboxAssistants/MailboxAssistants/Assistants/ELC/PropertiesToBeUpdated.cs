using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200007C RID: 124
	internal sealed class PropertiesToBeUpdated
	{
		// Token: 0x06000485 RID: 1157 RVA: 0x00020397 File Offset: 0x0001E597
		internal PropertiesToBeUpdated()
		{
			this.propertyDefinitions = new List<PropertyDefinition>();
			this.propertyValues = new List<object>();
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x000203B5 File Offset: 0x0001E5B5
		internal List<PropertyDefinition> PropertyDefinitions
		{
			get
			{
				return this.propertyDefinitions;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x000203BD File Offset: 0x0001E5BD
		internal List<object> PropertyValues
		{
			get
			{
				return this.propertyValues;
			}
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x000203C5 File Offset: 0x0001E5C5
		internal void Add(PropertyDefinition propDef, object propValue)
		{
			this.PropertyDefinitions.Add(propDef);
			this.propertyValues.Add(propValue);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x000203DF File Offset: 0x0001E5DF
		internal void Clear()
		{
			this.PropertyDefinitions.Clear();
			this.propertyValues.Clear();
		}

		// Token: 0x04000393 RID: 915
		private List<PropertyDefinition> propertyDefinitions;

		// Token: 0x04000394 RID: 916
		private List<object> propertyValues;
	}
}
