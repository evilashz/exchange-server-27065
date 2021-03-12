using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000121 RID: 289
	internal abstract class ObjectSchema
	{
		// Token: 0x06000A56 RID: 2646 RVA: 0x00020148 File Offset: 0x0001E348
		internal static TSchema GetInstance<TSchema>() where TSchema : ObjectSchema, new()
		{
			ObjectSchema.SchemaConstructorDelegate schemaConstructor = () => Activator.CreateInstance<TSchema>();
			ObjectSchema instanceImpl = ObjectSchema.GetInstanceImpl(typeof(TSchema), schemaConstructor);
			return (TSchema)((object)instanceImpl);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x00020194 File Offset: 0x0001E394
		internal static ObjectSchema GetInstance(Type schemaType)
		{
			if (null == schemaType)
			{
				throw new ArgumentNullException("schemaType");
			}
			if (!typeof(ObjectSchema).GetTypeInfo().IsAssignableFrom(schemaType.GetTypeInfo()))
			{
				throw new ArgumentException(string.Format("Invalid ObjectSchema Input Type: {0}", schemaType), "schemaType");
			}
			if (!ReflectionHelper.HasParameterlessConstructor(schemaType))
			{
				throw new ArgumentException(string.Format("Input type does not have a parameterless constructor: {0}", schemaType), "schemaType");
			}
			ObjectSchema.SchemaConstructorDelegate schemaConstructor = () => (ObjectSchema)Activator.CreateInstance(schemaType);
			return ObjectSchema.GetInstanceImpl(schemaType, schemaConstructor);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x00020248 File Offset: 0x0001E448
		private static ObjectSchema GetInstanceImpl(Type schemaType, ObjectSchema.SchemaConstructorDelegate schemaConstructor)
		{
			ObjectSchema result;
			if (!ObjectSchema.Instances.TryGetValue(schemaType, out result))
			{
				lock (ObjectSchema.InstancesLock)
				{
					if (!ObjectSchema.Instances.TryGetValue(schemaType, out result))
					{
						ObjectSchema objectSchema = schemaConstructor();
						ObjectSchema.Instances = new Dictionary<Type, ObjectSchema>(ObjectSchema.Instances)
						{
							{
								schemaType,
								objectSchema
							}
						};
						result = objectSchema;
					}
				}
			}
			return result;
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x000202C4 File Offset: 0x0001E4C4
		protected ObjectSchema()
		{
			HashSet<PropertyDefinition> hashSet = new HashSet<PropertyDefinition>();
			List<PropertyDefinition> list = new List<PropertyDefinition>();
			List<FieldInfo> list2 = ReflectionHelper.AggregateTypeHierarchy<FieldInfo>(base.GetType(), new AggregateType<FieldInfo>(ReflectionHelper.AggregateStaticFields));
			IEnumerable<FieldInfo> declaredFields = base.GetType().GetTypeInfo().DeclaredFields;
			foreach (FieldInfo fieldInfo in list2)
			{
				object value = fieldInfo.GetValue(null);
				if (typeof(ProviderPropertyDefinition).GetTypeInfo().IsAssignableFrom(fieldInfo.FieldType.GetTypeInfo()) && value == null)
				{
					throw new InvalidOperationException(string.Format("Property definition '{0}' is not initialized. This can be caused by loop dependency between initialization of one or more static fields.", fieldInfo.Name));
				}
				ProviderPropertyDefinition providerPropertyDefinition = value as ProviderPropertyDefinition;
				if (providerPropertyDefinition != null)
				{
					bool flag = false;
					foreach (FieldInfo fieldInfo2 in declaredFields)
					{
						if (fieldInfo2.Name.Equals(fieldInfo.Name) && !this.IsSameFieldHandle(fieldInfo2, fieldInfo))
						{
							flag = true;
							break;
						}
					}
					if (!this.containsCalculatedProperties && providerPropertyDefinition.IsCalculated)
					{
						this.containsCalculatedProperties = true;
					}
					if (!flag)
					{
						if (!providerPropertyDefinition.IsFilterOnly)
						{
							hashSet.TryAdd(providerPropertyDefinition);
							if (!providerPropertyDefinition.IsCalculated)
							{
								continue;
							}
							using (ReadOnlyCollection<ProviderPropertyDefinition>.Enumerator enumerator3 = providerPropertyDefinition.SupportingProperties.GetEnumerator())
							{
								while (enumerator3.MoveNext())
								{
									ProviderPropertyDefinition item = enumerator3.Current;
									hashSet.TryAdd(item);
								}
								continue;
							}
						}
						if (!providerPropertyDefinition.IsCalculated)
						{
							list.Add(providerPropertyDefinition);
						}
					}
				}
			}
			this.AllProperties = new ReadOnlyCollection<PropertyDefinition>(hashSet.ToArray());
			this.AllFilterOnlyProperties = new ReadOnlyCollection<PropertyDefinition>(list.ToArray());
			this.InitializePropertyCollections();
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x000204E0 File Offset: 0x0001E6E0
		protected void InitializePropertyCollections()
		{
			if (this.AllProperties == null)
			{
				throw new InvalidOperationException("Dev Bug: AllProperties should never be null");
			}
			if (this.AllFilterOnlyProperties == null)
			{
				throw new InvalidOperationException("Dev Bug: AllFilterOnlyProperties should never be null");
			}
			List<PropertyDefinition> list = new List<PropertyDefinition>();
			List<PropertyDefinition> list2 = new List<PropertyDefinition>();
			foreach (PropertyDefinition propertyDefinition in this.AllProperties)
			{
				ProviderPropertyDefinition providerPropertyDefinition = (ProviderPropertyDefinition)propertyDefinition;
				if (providerPropertyDefinition.IsFilterable)
				{
					list.Add(providerPropertyDefinition);
				}
				if (providerPropertyDefinition.IsMandatory && !providerPropertyDefinition.IsCalculated)
				{
					list2.Add(providerPropertyDefinition);
				}
			}
			foreach (PropertyDefinition propertyDefinition2 in this.AllFilterOnlyProperties)
			{
				ProviderPropertyDefinition providerPropertyDefinition2 = (ProviderPropertyDefinition)propertyDefinition2;
				if (providerPropertyDefinition2.IsFilterable)
				{
					list.Add(providerPropertyDefinition2);
				}
			}
			this.allFilterableProperties = new ReadOnlyCollection<PropertyDefinition>(list.ToArray());
			this.allMandatoryProperties = new ReadOnlyCollection<PropertyDefinition>(list2.ToArray());
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x000205FC File Offset: 0x0001E7FC
		// (set) Token: 0x06000A5C RID: 2652 RVA: 0x00020604 File Offset: 0x0001E804
		public ReadOnlyCollection<PropertyDefinition> AllProperties
		{
			get
			{
				return this.allProperties;
			}
			protected set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.allProperties = value;
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x0002061B File Offset: 0x0001E81B
		// (set) Token: 0x06000A5E RID: 2654 RVA: 0x00020623 File Offset: 0x0001E823
		public ReadOnlyCollection<PropertyDefinition> AllFilterOnlyProperties
		{
			get
			{
				return this.allFilterOnlyProperties;
			}
			protected set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.allFilterOnlyProperties = value;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x0002063A File Offset: 0x0001E83A
		public virtual ReadOnlyCollection<PropertyDefinition> AllFilterableProperties
		{
			get
			{
				return this.allFilterableProperties;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x00020642 File Offset: 0x0001E842
		public ReadOnlyCollection<PropertyDefinition> AllMandatoryProperties
		{
			get
			{
				return this.allMandatoryProperties;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x0002064A File Offset: 0x0001E84A
		public bool ContainsCalculatedProperties
		{
			get
			{
				return this.containsCalculatedProperties;
			}
		}

		// Token: 0x17000362 RID: 866
		public PropertyDefinition this[string propertyName]
		{
			get
			{
				if (this.namesToProperties == null)
				{
					Dictionary<string, PropertyDefinition> dictionary = new Dictionary<string, PropertyDefinition>(this.AllProperties.Count, StringComparer.OrdinalIgnoreCase);
					foreach (PropertyDefinition propertyDefinition in this.AllProperties)
					{
						dictionary[propertyDefinition.Name] = propertyDefinition;
					}
					Interlocked.CompareExchange<Dictionary<string, PropertyDefinition>>(ref this.namesToProperties, dictionary, null);
				}
				PropertyDefinition result = null;
				this.namesToProperties.TryGetValue(propertyName, out result);
				return result;
			}
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x000206EC File Offset: 0x0001E8EC
		private bool IsSameFieldHandle(FieldInfo left, FieldInfo right)
		{
			return left.FieldHandle.Value == right.FieldHandle.Value;
		}

		// Token: 0x04000632 RID: 1586
		private ReadOnlyCollection<PropertyDefinition> allProperties;

		// Token: 0x04000633 RID: 1587
		private ReadOnlyCollection<PropertyDefinition> allFilterableProperties;

		// Token: 0x04000634 RID: 1588
		private ReadOnlyCollection<PropertyDefinition> allMandatoryProperties;

		// Token: 0x04000635 RID: 1589
		private ReadOnlyCollection<PropertyDefinition> allFilterOnlyProperties;

		// Token: 0x04000636 RID: 1590
		private Dictionary<string, PropertyDefinition> namesToProperties;

		// Token: 0x04000637 RID: 1591
		private static Dictionary<Type, ObjectSchema> Instances = new Dictionary<Type, ObjectSchema>();

		// Token: 0x04000638 RID: 1592
		private static readonly object InstancesLock = new object();

		// Token: 0x04000639 RID: 1593
		private readonly bool containsCalculatedProperties;

		// Token: 0x02000122 RID: 290
		// (Invoke) Token: 0x06000A67 RID: 2663
		private delegate ObjectSchema SchemaConstructorDelegate();
	}
}
