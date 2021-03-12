using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000A7 RID: 167
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal abstract class StorePropertyDefinition : PropertyDefinition, IComparable<StorePropertyDefinition>, IComparable
	{
		// Token: 0x06000B88 RID: 2952 RVA: 0x00050C18 File Offset: 0x0004EE18
		protected internal StorePropertyDefinition(PropertyTypeSpecifier propertyTypeSpecifier, string displayName, Type type, PropertyFlags childFlags, PropertyDefinitionConstraint[] constraints) : base(displayName, type)
		{
			if (constraints == null)
			{
				throw new ArgumentNullException("constraints");
			}
			this.propertyTypeSpecifier = propertyTypeSpecifier;
			this.childFlags = childFlags;
			if (constraints.Length != 0)
			{
				this.constraints = new ReadOnlyCollection<PropertyDefinitionConstraint>(constraints);
				return;
			}
			this.constraints = StorePropertyDefinition.EmptyConstraints;
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00050C90 File Offset: 0x0004EE90
		public static void PerformActionOnNativePropertyDefinitions<T>(PropertyDependencyType targetDependencyType, ICollection<T> propertyDefinitions, Action<NativeStorePropertyDefinition> action) where T : PropertyDefinition
		{
			EnumValidator.AssertValid<PropertyDependencyType>(targetDependencyType);
			if (propertyDefinitions == null)
			{
				return;
			}
			int actualDependencyCount = 0;
			foreach (T t in propertyDefinitions)
			{
				PropertyDefinition propertyDefinition = t;
				StorePropertyDefinition storePropertyDefinition = InternalSchema.ToStorePropertyDefinition(propertyDefinition);
				storePropertyDefinition.ForEachMatch(targetDependencyType, delegate(NativeStorePropertyDefinition item)
				{
					action(item);
					actualDependencyCount++;
				});
			}
			int num = (propertyDefinitions.Count >= StorePropertyDefinition.dependencyEstimates.Length) ? propertyDefinitions.Count : StorePropertyDefinition.dependencyEstimates[propertyDefinitions.Count];
			if (actualDependencyCount != num && propertyDefinitions.Count < StorePropertyDefinition.dependencyEstimates.Length)
			{
				Interlocked.Exchange(ref StorePropertyDefinition.dependencyEstimates[propertyDefinitions.Count], actualDependencyCount);
			}
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x00050D78 File Offset: 0x0004EF78
		public static IList<NativeStorePropertyDefinition> GetNativePropertyDefinitions<T>(PropertyDependencyType targetDependencyType, ICollection<T> propertyDefinitions, Predicate<NativeStorePropertyDefinition> addToCollection) where T : PropertyDefinition
		{
			return (IList<NativeStorePropertyDefinition>)StorePropertyDefinition.GetNativePropertyDefinitions<T>(targetDependencyType, propertyDefinitions, false, addToCollection);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00050D88 File Offset: 0x0004EF88
		public static ICollection<NativeStorePropertyDefinition> GetNativePropertyDefinitions<T>(PropertyDependencyType targetDependencyType, ICollection<T> propertyDefinitions) where T : PropertyDefinition
		{
			return StorePropertyDefinition.GetNativePropertyDefinitions<T>(targetDependencyType, propertyDefinitions, true, null);
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000B8C RID: 2956
		public abstract StorePropertyCapabilities Capabilities { get; }

		// Token: 0x06000B8D RID: 2957 RVA: 0x00050D94 File Offset: 0x0004EF94
		public int CompareTo(StorePropertyDefinition other)
		{
			if (other == null)
			{
				throw new ArgumentException(ServerStrings.ObjectMustBeOfType(base.GetType().Name));
			}
			int num = string.Compare(this.GetHashString(), other.GetHashString(), StringComparison.OrdinalIgnoreCase);
			if (num != 0)
			{
				return num;
			}
			return string.Compare(base.Type.ToString(), other.Type.ToString(), StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00050DF4 File Offset: 0x0004EFF4
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			StorePropertyDefinition other = obj as StorePropertyDefinition;
			return this.CompareTo(other);
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x00050E14 File Offset: 0x0004F014
		public PropertyFlags PropertyFlags
		{
			get
			{
				if (this.propertyFlags == PropertyFlags.None)
				{
					this.CalculatePropertyFlagsFromType(this.childFlags);
				}
				return this.propertyFlags;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x00050E30 File Offset: 0x0004F030
		public PropertyTypeSpecifier SpecifiedWith
		{
			get
			{
				return this.propertyTypeSpecifier;
			}
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00050E38 File Offset: 0x0004F038
		public override string ToString()
		{
			this.InitializePropertyDefinitionString();
			return string.Format("[{1}] {0}", base.Name, this.propertyDefinitionString);
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x00050E57 File Offset: 0x0004F057
		public IList<PropertyDefinitionConstraint> Constraints
		{
			get
			{
				return this.constraints;
			}
		}

		// Token: 0x06000B93 RID: 2963
		internal abstract SortBy[] GetNativeSortBy(SortOrder sortOrder);

		// Token: 0x06000B94 RID: 2964
		internal abstract NativeStorePropertyDefinition GetNativeGroupBy();

		// Token: 0x06000B95 RID: 2965
		internal abstract GroupSort GetNativeGroupSort(SortOrder sortOrder, Aggregate aggregate);

		// Token: 0x06000B96 RID: 2966 RVA: 0x00050E60 File Offset: 0x0004F060
		public PropertyValidationError[] Validate(ExchangeOperationContext context, object value)
		{
			if ((this.PropertyFlags & PropertyFlags.Multivalued) == PropertyFlags.Multivalued)
			{
				return this.ValidateMultiValue(context, value);
			}
			PropertyValidationError propertyValidationError = this.ValidateSingleValue(context, value, base.Type);
			if (propertyValidationError != null)
			{
				return new PropertyValidationError[]
				{
					propertyValidationError
				};
			}
			return StorePropertyDefinition.NoValidationError;
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00050EAD File Offset: 0x0004F0AD
		internal PropertyError GetNotFoundError()
		{
			if (this.errorNotFound == null)
			{
				this.errorNotFound = new PropertyError(this, PropertyErrorCode.NotFound);
			}
			return this.errorNotFound;
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00050ECA File Offset: 0x0004F0CA
		internal PropertyError GetNotEnoughMemoryError()
		{
			if (this.errorNotEnoughMemory == null)
			{
				this.errorNotEnoughMemory = new PropertyError(this, PropertyErrorCode.NotEnoughMemory);
			}
			return this.errorNotEnoughMemory;
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00050EE7 File Offset: 0x0004F0E7
		internal string GetHashString()
		{
			this.InitializePropertyDefinitionString();
			return this.propertyDefinitionString;
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00050EF8 File Offset: 0x0004F0F8
		protected internal void CalculatePropertyFlagsFromType(PropertyFlags childFlags)
		{
			PropertyFlags propertyFlags = childFlags & ~(PropertyFlags.Multivalued | PropertyFlags.Binary);
			if (base.Type.GetTypeInfo().IsSubclassOf(typeof(Array)))
			{
				if (base.Type.Equals(typeof(byte[])))
				{
					propertyFlags |= PropertyFlags.Binary;
				}
				else
				{
					propertyFlags |= PropertyFlags.Multivalued;
					if (base.Type.Equals(typeof(byte[][])))
					{
						propertyFlags |= PropertyFlags.Binary;
					}
				}
			}
			this.propertyFlags = propertyFlags;
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00051008 File Offset: 0x0004F208
		private static ICollection<NativeStorePropertyDefinition> GetNativePropertyDefinitions<T>(PropertyDependencyType targetDependencyType, ICollection<T> propertyDefinitions, bool hashSetOrList, Predicate<NativeStorePropertyDefinition> addToCollection) where T : PropertyDefinition
		{
			EnumValidator.AssertValid<PropertyDependencyType>(targetDependencyType);
			if (propertyDefinitions == null)
			{
				return StorePropertyDefinition.EmptyNativeStoreProperties;
			}
			int num = (propertyDefinitions.Count >= StorePropertyDefinition.dependencyEstimates.Length) ? propertyDefinitions.Count : StorePropertyDefinition.dependencyEstimates[propertyDefinitions.Count];
			ICollection<NativeStorePropertyDefinition> collection = null;
			Action<NativeStorePropertyDefinition> action;
			if (hashSetOrList)
			{
				HashSet<NativeStorePropertyDefinition> nativePropertyDefinitionsSet = new HashSet<NativeStorePropertyDefinition>(num);
				action = delegate(NativeStorePropertyDefinition item)
				{
					if (addToCollection == null)
					{
						nativePropertyDefinitionsSet.TryAdd(item);
						return;
					}
					if (addToCollection(item))
					{
						nativePropertyDefinitionsSet.TryAdd(item);
					}
				};
				collection = nativePropertyDefinitionsSet;
			}
			else
			{
				IList<NativeStorePropertyDefinition> loadList = new List<NativeStorePropertyDefinition>(num);
				action = delegate(NativeStorePropertyDefinition item)
				{
					if (addToCollection == null)
					{
						loadList.Add(item);
						return;
					}
					if (addToCollection(item))
					{
						loadList.Add(item);
					}
				};
				collection = loadList;
			}
			foreach (T t in propertyDefinitions)
			{
				PropertyDefinition propertyDefinition = t;
				StorePropertyDefinition storePropertyDefinition = InternalSchema.ToStorePropertyDefinition(propertyDefinition);
				storePropertyDefinition.ForEachMatch(targetDependencyType, action);
			}
			int count = collection.Count;
			if (count != num && propertyDefinitions.Count < StorePropertyDefinition.dependencyEstimates.Length)
			{
				Interlocked.Exchange(ref StorePropertyDefinition.dependencyEstimates[propertyDefinitions.Count], count);
			}
			return collection;
		}

		// Token: 0x06000B9C RID: 2972
		protected abstract void ForEachMatch(PropertyDependencyType targetDependencyType, Action<NativeStorePropertyDefinition> action);

		// Token: 0x06000B9D RID: 2973 RVA: 0x00051148 File Offset: 0x0004F348
		internal bool IsDirty(PropertyBag.BasicPropertyStore propertyBag)
		{
			return this.InternalIsDirty(propertyBag);
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x00051151 File Offset: 0x0004F351
		internal void Set(ExchangeOperationContext operationContext, PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			this.ValidateSetPropertyValue(operationContext, value);
			this.InternalSetValue(propertyBag, value);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00051163 File Offset: 0x0004F363
		internal void SetWithoutValidation(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			this.InternalSetValue(propertyBag, value);
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0005116D File Offset: 0x0004F36D
		internal object Get(PropertyBag.BasicPropertyStore propertyBag)
		{
			return this.InternalTryGetValue(propertyBag);
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x00051176 File Offset: 0x0004F376
		internal void Delete(PropertyBag.BasicPropertyStore propertyBag)
		{
			this.ValidateDeletePropertyValue();
			this.InternalDeleteValue(propertyBag);
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x00051188 File Offset: 0x0004F388
		private static int[] InitializeDependencyEstimates()
		{
			int[] array = new int[256];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = 128;
			}
			return array;
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x000511B7 File Offset: 0x0004F3B7
		private string InitializePropertyDefinitionString()
		{
			if (this.propertyDefinitionString == null)
			{
				this.propertyDefinitionString = this.GetPropertyDefinitionString();
			}
			return this.propertyDefinitionString;
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x000511D4 File Offset: 0x0004F3D4
		private PropertyValidationError[] ValidateMultiValue(ExchangeOperationContext context, object value)
		{
			if (value == null)
			{
				return new PropertyValidationError[]
				{
					new NullValueError(this, value)
				};
			}
			IEnumerable enumerable = value as IEnumerable;
			if (enumerable == null)
			{
				return new PropertyValidationError[]
				{
					new TypeMismatchError(this, value)
				};
			}
			if (!value.GetType().Equals(base.Type))
			{
				return new PropertyValidationError[]
				{
					new TypeMismatchError(this, value)
				};
			}
			int num = 0;
			List<PropertyValidationError> list = new List<PropertyValidationError>();
			Type elementType = base.Type.GetElementType();
			int num2 = 0;
			foreach (object value2 in enumerable)
			{
				num++;
				PropertyValidationError propertyValidationError = this.ValidateSingleValue(context, value2, elementType);
				if (propertyValidationError != null)
				{
					PropertyValidationError item = new InvalidMultivalueElementError(propertyValidationError, value, num2);
					list.Add(item);
				}
				num2++;
			}
			return list.ToArray();
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x000512D4 File Offset: 0x0004F4D4
		private PropertyValidationError ValidateSingleValue(ExchangeOperationContext context, object value, Type expectedType)
		{
			if (value == null)
			{
				return new NullValueError(this, value);
			}
			if (value is DateTime && expectedType.Equals(typeof(ExDateTime)))
			{
				expectedType = typeof(DateTime);
			}
			Type type = value.GetType();
			if (!expectedType.Equals(type) && !expectedType.GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
			{
				return new TypeMismatchError(this, value);
			}
			for (int i = 0; i < this.constraints.Count; i++)
			{
				PropertyValidationError propertyValidationError = this.constraints[i].Validate(context, value, this, null);
				if (propertyValidationError != null)
				{
					return propertyValidationError;
				}
			}
			return null;
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00051370 File Offset: 0x0004F570
		private void ValidateSetPropertyValue(ExchangeOperationContext context, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(base.Name);
			}
			PropertyValidationError[] array = this.Validate(context, value);
			if (array.Length > 0)
			{
				PropertyValidationError propertyValidationError = array[0];
				throw new PropertyValidationException(propertyValidationError.Description, propertyValidationError.PropertyDefinition, array);
			}
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x000513B8 File Offset: 0x0004F5B8
		private void ValidateDeletePropertyValue()
		{
			if ((this.PropertyFlags & PropertyFlags.ReadOnly) != PropertyFlags.None)
			{
				PropertyValidationError propertyValidationError = new PropertyValidationError(new LocalizedString(ServerStrings.PropertyIsReadOnly(base.Name)), this, null);
				throw new PropertyValidationException(ServerStrings.PropertyIsReadOnly(base.Name), this, new PropertyValidationError[]
				{
					propertyValidationError
				});
			}
		}

		// Token: 0x06000BA8 RID: 2984
		protected abstract string GetPropertyDefinitionString();

		// Token: 0x06000BA9 RID: 2985
		protected abstract void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value);

		// Token: 0x06000BAA RID: 2986
		protected abstract object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag);

		// Token: 0x06000BAB RID: 2987
		protected abstract void InternalDeleteValue(PropertyBag.BasicPropertyStore propertyBag);

		// Token: 0x06000BAC RID: 2988
		protected abstract bool InternalIsDirty(PropertyBag.BasicPropertyStore propertyBag);

		// Token: 0x04000330 RID: 816
		private static int[] dependencyEstimates = StorePropertyDefinition.InitializeDependencyEstimates();

		// Token: 0x04000331 RID: 817
		private static readonly NativeStorePropertyDefinition[] EmptyNativeStoreProperties = Array<NativeStorePropertyDefinition>.Empty;

		// Token: 0x04000332 RID: 818
		private readonly PropertyTypeSpecifier propertyTypeSpecifier;

		// Token: 0x04000333 RID: 819
		private PropertyFlags propertyFlags;

		// Token: 0x04000334 RID: 820
		private readonly PropertyFlags childFlags;

		// Token: 0x04000335 RID: 821
		private string propertyDefinitionString;

		// Token: 0x04000336 RID: 822
		private readonly ReadOnlyCollection<PropertyDefinitionConstraint> constraints;

		// Token: 0x04000337 RID: 823
		private static readonly PropertyValidationError[] NoValidationError = Array<PropertyValidationError>.Empty;

		// Token: 0x04000338 RID: 824
		private static readonly ReadOnlyCollection<PropertyDefinitionConstraint> EmptyConstraints = new ReadOnlyCollection<PropertyDefinitionConstraint>(PropertyDefinitionConstraint.None);

		// Token: 0x04000339 RID: 825
		private PropertyError errorNotFound;

		// Token: 0x0400033A RID: 826
		private PropertyError errorNotEnoughMemory;
	}
}
