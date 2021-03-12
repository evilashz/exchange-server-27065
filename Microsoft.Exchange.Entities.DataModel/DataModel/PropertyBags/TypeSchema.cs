using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Entities.DataModel.PropertyBags
{
	// Token: 0x02000018 RID: 24
	public abstract class TypeSchema
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002858 File Offset: 0x00000A58
		protected void RegisterPropertyDefinition(PropertyDefinition newProperty)
		{
			try
			{
				this.registeredProperties.Add(newProperty.Name, newProperty);
			}
			catch (ArgumentException innerException)
			{
				throw new InvalidOperationException(string.Format("A property with the same name has already been registered (Name: {0}).", newProperty.Name), innerException);
			}
		}

		// Token: 0x0400001D RID: 29
		private readonly Dictionary<string, PropertyDefinition> registeredProperties = new Dictionary<string, PropertyDefinition>();
	}
}
