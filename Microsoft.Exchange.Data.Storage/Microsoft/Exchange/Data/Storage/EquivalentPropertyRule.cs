using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AA6 RID: 2726
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class EquivalentPropertyRule : PropertyRule
	{
		// Token: 0x06006379 RID: 25465 RVA: 0x001A35F4 File Offset: 0x001A17F4
		public EquivalentPropertyRule(string name, Action<ILocationIdentifierSetter> onSetWriteEnforceLocationIdentifier, params NativeStorePropertyDefinition[] properties) : base(name, onSetWriteEnforceLocationIdentifier, new PropertyReference[0])
		{
			if (properties.Length < 2)
			{
				throw new ArgumentException("At least 2 properties needed for EquivalentPropertyRule.");
			}
			List<PropertyDefinition> list = new List<PropertyDefinition>(properties.Length);
			list.Add(properties[0]);
			Type type = properties[0].Type;
			for (int i = 1; i < properties.Length; i++)
			{
				if (properties[i].Type != type)
				{
					throw new ArgumentException("Properties should be same type for EquivalentPropertyRule.");
				}
				if (list.Contains(properties[i]))
				{
					throw new ArgumentException("duplicate properties in collection");
				}
				list.Add(properties[i]);
			}
			base.ReadProperties = (base.WriteProperties = list);
		}

		// Token: 0x0600637A RID: 25466 RVA: 0x001A3694 File Offset: 0x001A1894
		protected override bool WriteEnforceRule(ICorePropertyBag propertyBag)
		{
			bool flag = false;
			object obj = null;
			foreach (PropertyDefinition propertyDefinition in base.ReadProperties)
			{
				if (propertyBag.IsPropertyDirty(propertyDefinition))
				{
					flag = true;
					obj = propertyBag.TryGetProperty(propertyDefinition);
					if (!PropertyError.IsPropertyNotFound(obj))
					{
						break;
					}
				}
			}
			if (flag)
			{
				foreach (PropertyDefinition property in base.WriteProperties)
				{
					propertyBag.SetOrDeleteProperty(property, obj);
				}
			}
			return flag;
		}
	}
}
