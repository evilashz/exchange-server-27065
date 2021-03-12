using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000154 RID: 340
	[Serializable]
	public abstract class MiniObject : ADObject
	{
		// Token: 0x06000E90 RID: 3728 RVA: 0x00045FC4 File Offset: 0x000441C4
		public MiniObject()
		{
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x00045FCC File Offset: 0x000441CC
		internal override void SetIsReadOnly(bool valueToSet)
		{
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x00045FCE File Offset: 0x000441CE
		internal override void Initialize()
		{
			this.propertyBag.SetField(ADObjectSchema.ObjectState, ObjectState.Unchanged);
			this.propertyBag.ResetChangeTracking(ADObjectSchema.ObjectState);
			base.Initialize();
		}

		// Token: 0x17000287 RID: 647
		internal override object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				ProviderPropertyDefinition providerPropertyDefinition = (ProviderPropertyDefinition)propertyDefinition;
				object obj = null;
				if (!this.propertyBag.TryGetField(providerPropertyDefinition, ref obj))
				{
					if (!this.HasSupportingProperties(providerPropertyDefinition) && !providerPropertyDefinition.IsTaskPopulated)
					{
						throw new ValueNotPresentException(propertyDefinition.Name, (string)this.propertyBag[ADObjectSchema.Name]);
					}
					obj = this.propertyBag[providerPropertyDefinition];
				}
				return obj ?? providerPropertyDefinition.DefaultValue;
			}
			set
			{
				base[propertyDefinition] = value;
			}
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0004607C File Offset: 0x0004427C
		internal bool HasSupportingProperties(ProviderPropertyDefinition propertyDefinition)
		{
			if (ADObjectSchema.IsCached == propertyDefinition || ADObjectSchema.WhenReadUTC == propertyDefinition || ADObjectSchema.OriginatingServer == propertyDefinition)
			{
				return true;
			}
			if (!propertyDefinition.IsCalculated)
			{
				return false;
			}
			foreach (ProviderPropertyDefinition key in propertyDefinition.SupportingProperties)
			{
				if (!this.propertyBag.Contains(key))
				{
					return false;
				}
			}
			return true;
		}
	}
}
