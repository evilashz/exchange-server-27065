using System;
using System.Reflection;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000048 RID: 72
	public static class PropertyConstraintProvider
	{
		// Token: 0x060002D4 RID: 724 RVA: 0x0000A910 File Offset: 0x00008B10
		internal static PropertyDefinitionConstraint[] GetPropertyDefinitionConstraints(object constraintProvider, string propertyName)
		{
			PropertyDefinitionConstraint[] result = new PropertyDefinitionConstraint[0];
			if (constraintProvider != null)
			{
				IPropertyConstraintProvider propertyConstraintProvider = constraintProvider as IPropertyConstraintProvider;
				if (propertyConstraintProvider != null)
				{
					result = propertyConstraintProvider.GetPropertyDefinitionConstraints(propertyName);
				}
				else
				{
					Type type = (constraintProvider is Type) ? ((Type)constraintProvider) : constraintProvider.GetType();
					result = PropertyConstraintProvider.GetPropertyDefinitionConstraints(type, propertyName);
				}
			}
			return result;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000A95C File Offset: 0x00008B5C
		internal static PropertyDefinitionConstraint[] GetPropertyDefinitionConstraints(Type type, string propertyName)
		{
			PropertyDefinitionConstraint[] array = new PropertyDefinitionConstraint[0];
			ProviderPropertyDefinition propertyDefinition = PropertyConstraintProvider.GetPropertyDefinition(type, propertyName);
			if (propertyDefinition != null)
			{
				array = new PropertyDefinitionConstraint[propertyDefinition.AllConstraints.Count];
				propertyDefinition.AllConstraints.CopyTo(array, 0);
			}
			return array;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000A99C File Offset: 0x00008B9C
		internal static ProviderPropertyDefinition GetPropertyDefinition(Type type, string propertyName)
		{
			ProviderPropertyDefinition result = null;
			if (null != type)
			{
				Type type2 = type.Assembly.GetType(type.FullName + "Schema", false, true);
				if (null != type2)
				{
					FieldInfo field = type2.GetField(propertyName, BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
					if (null != field && typeof(ProviderPropertyDefinition).IsAssignableFrom(field.FieldType))
					{
						result = (ProviderPropertyDefinition)field.GetValue(null);
					}
				}
			}
			return result;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000AA14 File Offset: 0x00008C14
		internal static ExchangeObjectVersion GetPropertyDefinitionVersion(IVersionable versionableObject, string propertyName)
		{
			ExchangeObjectVersion result = ExchangeObjectVersion.Exchange2003;
			if (versionableObject != null && versionableObject.ObjectSchema != null)
			{
				FieldInfo field = versionableObject.ObjectSchema.GetType().GetField(propertyName, BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
				if (null != field)
				{
					ProviderPropertyDefinition providerPropertyDefinition = (ProviderPropertyDefinition)field.GetValue(null);
					result = providerPropertyDefinition.VersionAdded;
				}
			}
			return result;
		}
	}
}
