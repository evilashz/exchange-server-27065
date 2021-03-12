using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.StoreConfigurableType
{
	// Token: 0x020009C9 RID: 2505
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class SimplePropertyDefinition : ProviderPropertyDefinition, IEquatable<SimplePropertyDefinition>
	{
		// Token: 0x06005C83 RID: 23683 RVA: 0x00181758 File Offset: 0x0017F958
		public SimplePropertyDefinition(string name, ExchangeObjectVersion versionAdded, Type type, PropertyDefinitionFlags flags, object defaultValue, object initialValue) : base(name, versionAdded, type, defaultValue)
		{
			this.PropertyDefinitionFlags = flags;
			this.InitialValue = initialValue;
		}

		// Token: 0x06005C84 RID: 23684 RVA: 0x00181775 File Offset: 0x0017F975
		public SimplePropertyDefinition(string name, ExchangeObjectVersion versionAdded, Type type, PropertyDefinitionFlags flags, object defaultValue, object initialValue, PropertyDefinitionConstraint[] readConstraints, PropertyDefinitionConstraint[] writeConstraints) : base(name, versionAdded, type, defaultValue, readConstraints, writeConstraints)
		{
			this.PropertyDefinitionFlags = flags;
			this.InitialValue = initialValue;
		}

		// Token: 0x06005C85 RID: 23685 RVA: 0x00181798 File Offset: 0x0017F998
		public SimplePropertyDefinition(string name, ExchangeObjectVersion versionAdded, Type type, PropertyDefinitionFlags flags, object defaultValue, object initialValue, PropertyDefinitionConstraint[] readConstraints, PropertyDefinitionConstraint[] writeConstraints, ProviderPropertyDefinition[] supportingProperties, CustomFilterBuilderDelegate customFilterBuilderDelegate, GetterDelegate getterDelegate, SetterDelegate setterDelegate) : base(name, versionAdded, type, defaultValue, readConstraints, writeConstraints, supportingProperties, customFilterBuilderDelegate, getterDelegate, setterDelegate)
		{
			this.PropertyDefinitionFlags = flags;
			this.InitialValue = initialValue;
		}

		// Token: 0x17001964 RID: 6500
		// (get) Token: 0x06005C86 RID: 23686 RVA: 0x001817CC File Offset: 0x0017F9CC
		public override bool IsMultivalued
		{
			get
			{
				return PropertyDefinitionFlags.None != (PropertyDefinitionFlags.MultiValued & this.PropertyDefinitionFlags);
			}
		}

		// Token: 0x17001965 RID: 6501
		// (get) Token: 0x06005C87 RID: 23687 RVA: 0x001817DC File Offset: 0x0017F9DC
		public override bool IsReadOnly
		{
			get
			{
				return PropertyDefinitionFlags.None != (PropertyDefinitionFlags.ReadOnly & this.PropertyDefinitionFlags);
			}
		}

		// Token: 0x17001966 RID: 6502
		// (get) Token: 0x06005C88 RID: 23688 RVA: 0x001817EC File Offset: 0x0017F9EC
		public override bool IsCalculated
		{
			get
			{
				return PropertyDefinitionFlags.None != (PropertyDefinitionFlags.Calculated & this.PropertyDefinitionFlags);
			}
		}

		// Token: 0x17001967 RID: 6503
		// (get) Token: 0x06005C89 RID: 23689 RVA: 0x001817FC File Offset: 0x0017F9FC
		public override bool IsFilterOnly
		{
			get
			{
				return PropertyDefinitionFlags.None != (PropertyDefinitionFlags.FilterOnly & this.PropertyDefinitionFlags);
			}
		}

		// Token: 0x17001968 RID: 6504
		// (get) Token: 0x06005C8A RID: 23690 RVA: 0x0018180C File Offset: 0x0017FA0C
		public override bool IsMandatory
		{
			get
			{
				return PropertyDefinitionFlags.None != (PropertyDefinitionFlags.Mandatory & this.PropertyDefinitionFlags);
			}
		}

		// Token: 0x17001969 RID: 6505
		// (get) Token: 0x06005C8B RID: 23691 RVA: 0x0018181D File Offset: 0x0017FA1D
		public override bool PersistDefaultValue
		{
			get
			{
				return PropertyDefinitionFlags.None != (PropertyDefinitionFlags.PersistDefaultValue & this.PropertyDefinitionFlags);
			}
		}

		// Token: 0x1700196A RID: 6506
		// (get) Token: 0x06005C8C RID: 23692 RVA: 0x0018182E File Offset: 0x0017FA2E
		public override bool IsWriteOnce
		{
			get
			{
				return PropertyDefinitionFlags.None != (PropertyDefinitionFlags.WriteOnce & this.PropertyDefinitionFlags);
			}
		}

		// Token: 0x1700196B RID: 6507
		// (get) Token: 0x06005C8D RID: 23693 RVA: 0x0018183F File Offset: 0x0017FA3F
		public override bool IsBinary
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700196C RID: 6508
		// (get) Token: 0x06005C8E RID: 23694 RVA: 0x00181842 File Offset: 0x0017FA42
		// (set) Token: 0x06005C8F RID: 23695 RVA: 0x0018184A File Offset: 0x0017FA4A
		public PropertyDefinitionFlags PropertyDefinitionFlags { get; private set; }

		// Token: 0x1700196D RID: 6509
		// (get) Token: 0x06005C90 RID: 23696 RVA: 0x00181853 File Offset: 0x0017FA53
		// (set) Token: 0x06005C91 RID: 23697 RVA: 0x0018185B File Offset: 0x0017FA5B
		public object InitialValue { get; private set; }

		// Token: 0x06005C92 RID: 23698 RVA: 0x00181864 File Offset: 0x0017FA64
		public override int GetHashCode()
		{
			if (!string.IsNullOrEmpty(base.Name))
			{
				return base.Name.GetHashCode();
			}
			return string.Empty.GetHashCode();
		}

		// Token: 0x06005C93 RID: 23699 RVA: 0x00181889 File Offset: 0x0017FA89
		public bool Equals(SimplePropertyDefinition other)
		{
			return !object.ReferenceEquals(null, other) && string.Equals(base.Name, other.Name);
		}

		// Token: 0x06005C94 RID: 23700 RVA: 0x001818A7 File Offset: 0x0017FAA7
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SimplePropertyDefinition);
		}
	}
}
