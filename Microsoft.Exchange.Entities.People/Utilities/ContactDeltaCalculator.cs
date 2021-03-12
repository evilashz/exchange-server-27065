using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Entities.People.Utilities
{
	// Token: 0x02000008 RID: 8
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ContactDeltaCalculator : IContactDeltaCalculator<IStorePropertyBag, PropertyDefinition>
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002C51 File Offset: 0x00000E51
		public ContactDeltaCalculator(IEnumerable<PropertyDefinition> propertiesToCompare)
		{
			ArgumentValidator.ThrowIfNull("propertiesToCompare", propertiesToCompare);
			this.propertiesToCompare = propertiesToCompare;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002C6C File Offset: 0x00000E6C
		public ICollection<Tuple<PropertyDefinition, object>> CalculateDelta(IStorePropertyBag source, IStorePropertyBag target)
		{
			List<Tuple<PropertyDefinition, object>> list = new List<Tuple<PropertyDefinition, object>>();
			if (object.ReferenceEquals(source, target))
			{
				return list;
			}
			ArgumentValidator.ThrowIfNull("source", source);
			ArgumentValidator.ThrowIfNull("target", target);
			foreach (PropertyDefinition propertyDefinition in this.propertiesToCompare)
			{
				object obj;
				try
				{
					obj = source.TryGetProperty(propertyDefinition);
				}
				catch (NotInBagPropertyErrorException)
				{
					list.Add(new Tuple<PropertyDefinition, object>(propertyDefinition, target.TryGetProperty(propertyDefinition)));
					continue;
				}
				object obj2;
				try
				{
					obj2 = target.TryGetProperty(propertyDefinition);
				}
				catch (NotInBagPropertyErrorException)
				{
					continue;
				}
				if (!obj.Equals(obj2) && !(obj2 is PropertyError))
				{
					list.Add(new Tuple<PropertyDefinition, object>(propertyDefinition, obj2));
				}
			}
			return list;
		}

		// Token: 0x04000016 RID: 22
		private readonly IEnumerable<PropertyDefinition> propertiesToCompare;
	}
}
