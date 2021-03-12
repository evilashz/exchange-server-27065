using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000181 RID: 385
	internal class MessageTraceTenantSettingFacade<T> : FacadeBase where T : ADObject, new()
	{
		// Token: 0x06000F93 RID: 3987 RVA: 0x000319E8 File Offset: 0x0002FBE8
		public MessageTraceTenantSettingFacade(IConfigurable configurable) : base(configurable ?? FacadeBase.NewADObject<T>())
		{
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x000319FF File Offset: 0x0002FBFF
		public MessageTraceTenantSettingFacade() : this(null)
		{
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x00031A08 File Offset: 0x0002FC08
		// (set) Token: 0x06000F96 RID: 3990 RVA: 0x00031A40 File Offset: 0x0002FC40
		public PropertyDefinition[] DeclaredProperties
		{
			get
			{
				if (this.declaredProperties == null)
				{
					IEnumerable<PropertyDefinition> declaredReflectedProperties = DalHelper.GetDeclaredReflectedProperties((ADObject)base.InnerConfigurable);
					this.declaredProperties = declaredReflectedProperties.ToArray<PropertyDefinition>();
				}
				return this.declaredProperties;
			}
			set
			{
				this.declaredProperties = value;
			}
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x00031A70 File Offset: 0x0002FC70
		public override IEnumerable<PropertyDefinition> GetPropertyDefinitions(bool isChangedOnly)
		{
			T innerSetting = base.InnerConfigurable as T;
			IEnumerable<PropertyDefinition> enumerable = this.DeclaredProperties;
			if (isChangedOnly)
			{
				enumerable = from property in enumerable.OfType<ProviderPropertyDefinition>()
				where !property.IsCalculated && innerSetting.IsModified(property)
				select property;
			}
			return enumerable.Concat(MessageTraceTenantSettingFacade<T>.stdProperties);
		}

		// Token: 0x04000732 RID: 1842
		public static readonly HygienePropertyDefinition WhenChangedAtSourceProp = new HygienePropertyDefinition("WhenChangedAtSource", typeof(DateTime?));

		// Token: 0x04000733 RID: 1843
		private static readonly PropertyDefinition[] stdProperties = new PropertyDefinition[]
		{
			ADObjectSchema.Id,
			ADObjectSchema.OrganizationalUnitRoot,
			ADObjectSchema.RawName,
			ADObjectSchema.WhenChangedRaw,
			ADObjectSchema.WhenCreatedRaw,
			ADObjectSchema.ObjectState,
			ADObjectSchema.WhenChangedUTC,
			MessageTraceTenantSettingFacade<T>.WhenChangedAtSourceProp
		};

		// Token: 0x04000734 RID: 1844
		private PropertyDefinition[] declaredProperties;
	}
}
