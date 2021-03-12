using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000341 RID: 833
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Schema
	{
		// Token: 0x060024E8 RID: 9448 RVA: 0x00094F04 File Offset: 0x00093104
		protected internal Schema()
		{
			HashSet<PropertyDefinition> hashSet = new HashSet<PropertyDefinition>();
			HashSet<PropertyDefinition> hashSet2 = new HashSet<PropertyDefinition>();
			HashSet<PropertyDefinition> hashSet3 = new HashSet<PropertyDefinition>();
			List<StoreObjectConstraint> list = new List<StoreObjectConstraint>();
			HashSet<PropertyDefinition> propertySet = new HashSet<PropertyDefinition>();
			HashSet<PropertyDefinition> propertySet2 = new HashSet<PropertyDefinition>();
			HashSet<StorePropertyDefinition> propertySet3 = new HashSet<StorePropertyDefinition>();
			HashSet<StorePropertyDefinition> propertySet4 = new HashSet<StorePropertyDefinition>();
			foreach (FieldInfo fieldInfo in ReflectionHelper.AggregateTypeHierarchy<FieldInfo>(base.GetType(), new AggregateType<FieldInfo>(ReflectionHelper.AggregateStaticFields)))
			{
				object value = fieldInfo.GetValue(null);
				StorePropertyDefinition storePropertyDefinition = value as StorePropertyDefinition;
				if (storePropertyDefinition != null && (storePropertyDefinition.PropertyFlags & PropertyFlags.FilterOnly) == PropertyFlags.None)
				{
					hashSet2.Add(storePropertyDefinition);
					if (fieldInfo.IsPublic)
					{
						hashSet.Add(storePropertyDefinition);
						if (storePropertyDefinition is SmartPropertyDefinition)
						{
							hashSet3.Add(storePropertyDefinition);
						}
					}
					if (fieldInfo.GetCustomAttribute<DetectCodepageAttribute>() != null)
					{
						Schema.AddPropertyAndDependentPropertiesToSet<StorePropertyDefinition>(propertySet3, storePropertyDefinition);
						Schema.AddPropertyAndDependentPropertiesToSet<PropertyDefinition>(propertySet, storePropertyDefinition);
						Schema.AddPropertyAndDependentPropertiesToSet<PropertyDefinition>(propertySet2, storePropertyDefinition);
					}
					else if (fieldInfo.GetCustomAttribute<AutoloadAttribute>() != null)
					{
						Schema.AddPropertyAndDependentPropertiesToSet<PropertyDefinition>(propertySet, storePropertyDefinition);
						Schema.AddPropertyAndDependentPropertiesToSet<PropertyDefinition>(propertySet2, storePropertyDefinition);
					}
					else if (storePropertyDefinition is SmartPropertyDefinition)
					{
						SmartPropertyDefinition smartPropertyDefinition = (SmartPropertyDefinition)storePropertyDefinition;
						Schema.AddSmartPropertyToSet<PropertyDefinition>(propertySet, smartPropertyDefinition.Dependencies, PropertyDependencyType.NeedToReadForWrite);
						Schema.AddSmartPropertyToSet<PropertyDefinition>(propertySet2, smartPropertyDefinition.Dependencies, PropertyDependencyType.NeedToReadForWrite);
					}
					else if (fieldInfo.GetCustomAttribute<OptionalAutoloadAttribute>() != null)
					{
						Schema.AddPropertyAndDependentPropertiesToSet<PropertyDefinition>(propertySet, storePropertyDefinition);
					}
					if (fieldInfo.GetCustomAttribute<LegalTrackingAttribute>() != null)
					{
						Schema.AddPropertyAndDependentLegalTrackingPropertiesToSet<StorePropertyDefinition>(propertySet4, storePropertyDefinition);
					}
					foreach (object obj in fieldInfo.GetCustomAttributes(typeof(ConstraintAttribute), false))
					{
						ConstraintAttribute constraintAttribute = obj as ConstraintAttribute;
						if (constraintAttribute != null)
						{
							list.Add(constraintAttribute.GetConstraint(storePropertyDefinition));
						}
					}
				}
			}
			this.allProperties = hashSet;
			this.allPropertiesInternal = hashSet2;
			this.smartProperties = hashSet3;
			this.autoloadProperties = propertySet;
			this.requiredAutoloadProperties = propertySet2;
			this.detectCodepageProperties = propertySet3;
			this.legalTrackingProperties = propertySet4;
			this.AddConstraints(list);
			this.constraints = list.ToArray();
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x00095130 File Offset: 0x00093330
		protected virtual void AddConstraints(List<StoreObjectConstraint> constraints)
		{
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x00095134 File Offset: 0x00093334
		protected void RemoveConstraints(StoreObjectConstraint[] constraints)
		{
			List<StoreObjectConstraint> list = new List<StoreObjectConstraint>(this.constraints);
			for (int i = 0; i < constraints.Length; i++)
			{
				list.Remove(constraints[i]);
			}
			this.constraints = list.ToArray();
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x00095174 File Offset: 0x00093374
		private static void AddPropertyAndDependentLegalTrackingPropertiesToSet<T>(HashSet<T> propertySet, StorePropertyDefinition propertyDefinition) where T : PropertyDefinition
		{
			SmartPropertyDefinition smartPropertyDefinition = propertyDefinition as SmartPropertyDefinition;
			if (smartPropertyDefinition != null)
			{
				Schema.AddSmartPropertyToSet<T>(propertySet, smartPropertyDefinition.LegalTrackingDependencies, PropertyDependencyType.AllRead);
				return;
			}
			propertySet.Add(propertyDefinition as T);
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x000951AC File Offset: 0x000933AC
		private static void AddPropertyAndDependentPropertiesToSet<T>(HashSet<T> propertySet, StorePropertyDefinition propertyDefinition) where T : PropertyDefinition
		{
			SmartPropertyDefinition smartPropertyDefinition = propertyDefinition as SmartPropertyDefinition;
			if (smartPropertyDefinition != null)
			{
				Schema.AddSmartPropertyToSet<T>(propertySet, smartPropertyDefinition.Dependencies, PropertyDependencyType.AllRead);
				return;
			}
			propertySet.Add(propertyDefinition as T);
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x000951E4 File Offset: 0x000933E4
		private static void AddSmartPropertyToSet<T>(HashSet<T> propertySet, PropertyDependency[] dependencies, PropertyDependencyType dependencyType) where T : PropertyDefinition
		{
			if (dependencies == null)
			{
				return;
			}
			foreach (PropertyDependency propertyDependency in dependencies)
			{
				if ((propertyDependency.Type & dependencyType) != PropertyDependencyType.None)
				{
					propertySet.Add(propertyDependency.Property as T);
				}
			}
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x0009522C File Offset: 0x0009342C
		protected void AddDependencies(params Schema[] dependencies)
		{
			List<StoreObjectConstraint> list = new List<StoreObjectConstraint>(this.Constraints);
			foreach (Schema schema in dependencies)
			{
				this.allProperties.UnionWith(schema.AllProperties);
				this.allPropertiesInternal.UnionWith(schema.InternalAllProperties);
				this.autoloadProperties.UnionWith(schema.AutoloadProperties);
				this.requiredAutoloadProperties.UnionWith(schema.RequiredAutoloadProperties);
				this.detectCodepageProperties.UnionWith(schema.DetectCodepageProperties);
				this.legalTrackingProperties.UnionWith(schema.LegalTrackingProperties);
				list.AddRange(schema.Constraints);
			}
			this.constraints = list.ToArray();
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x000952D7 File Offset: 0x000934D7
		public static void FlushCache()
		{
			PropertyTagCache.Cache.Reset();
		}

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x060024F0 RID: 9456 RVA: 0x000952E3 File Offset: 0x000934E3
		public static Schema Instance
		{
			get
			{
				if (Schema.instance == null)
				{
					Schema.instance = new Schema();
				}
				return Schema.instance;
			}
		}

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x060024F1 RID: 9457 RVA: 0x000952FB File Offset: 0x000934FB
		public ICollection<PropertyDefinition> AllProperties
		{
			get
			{
				return this.allProperties;
			}
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x060024F2 RID: 9458 RVA: 0x00095303 File Offset: 0x00093503
		internal ICollection<PropertyDefinition> InternalAllProperties
		{
			get
			{
				return this.allPropertiesInternal;
			}
		}

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x060024F3 RID: 9459 RVA: 0x0009530B File Offset: 0x0009350B
		public ICollection<PropertyDefinition> SmartProperties
		{
			get
			{
				return this.smartProperties;
			}
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x060024F4 RID: 9460 RVA: 0x00095313 File Offset: 0x00093513
		public ICollection<PropertyDefinition> AutoloadProperties
		{
			get
			{
				return this.autoloadProperties;
			}
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x060024F5 RID: 9461 RVA: 0x0009531B File Offset: 0x0009351B
		internal ICollection<PropertyDefinition> RequiredAutoloadProperties
		{
			get
			{
				return this.requiredAutoloadProperties;
			}
		}

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x060024F6 RID: 9462 RVA: 0x00095323 File Offset: 0x00093523
		internal StoreObjectConstraint[] Constraints
		{
			get
			{
				return this.constraints;
			}
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x060024F7 RID: 9463 RVA: 0x0009532B File Offset: 0x0009352B
		internal ICollection<StorePropertyDefinition> DetectCodepageProperties
		{
			get
			{
				return this.detectCodepageProperties;
			}
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x060024F8 RID: 9464 RVA: 0x00095333 File Offset: 0x00093533
		internal ICollection<StorePropertyDefinition> LegalTrackingProperties
		{
			get
			{
				return this.legalTrackingProperties;
			}
		}

		// Token: 0x0400165D RID: 5725
		private static Schema instance;

		// Token: 0x0400165E RID: 5726
		private HashSet<PropertyDefinition> allProperties;

		// Token: 0x0400165F RID: 5727
		private HashSet<PropertyDefinition> allPropertiesInternal;

		// Token: 0x04001660 RID: 5728
		private HashSet<PropertyDefinition> smartProperties;

		// Token: 0x04001661 RID: 5729
		private HashSet<PropertyDefinition> autoloadProperties;

		// Token: 0x04001662 RID: 5730
		private HashSet<PropertyDefinition> requiredAutoloadProperties;

		// Token: 0x04001663 RID: 5731
		private HashSet<StorePropertyDefinition> detectCodepageProperties;

		// Token: 0x04001664 RID: 5732
		private HashSet<StorePropertyDefinition> legalTrackingProperties;

		// Token: 0x04001665 RID: 5733
		private StoreObjectConstraint[] constraints;
	}
}
