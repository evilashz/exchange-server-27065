using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009FD RID: 2557
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class XsoMailboxConfigurationObjectSchema : UserConfigurationObjectSchema
	{
		// Token: 0x06005D7C RID: 23932 RVA: 0x0018BBC4 File Offset: 0x00189DC4
		public XsoMailboxConfigurationObjectSchema()
		{
			this.xsoPropertyMappings = new Dictionary<PropertyDefinition, XsoDriverPropertyDefinition>();
			foreach (PropertyDefinition propertyDefinition in base.AllProperties)
			{
				XsoDriverPropertyDefinition xsoDriverPropertyDefinition = propertyDefinition as XsoDriverPropertyDefinition;
				if (xsoDriverPropertyDefinition != null)
				{
					if (this.xsoPropertyMappings.ContainsKey(xsoDriverPropertyDefinition.StorePropertyDefinition))
					{
						throw new NotSupportedException("One XSO property is mapping to multiple XSO driver property.");
					}
					this.xsoPropertyMappings[xsoDriverPropertyDefinition.StorePropertyDefinition] = xsoDriverPropertyDefinition;
				}
			}
			this.cachedXsoProperties = this.xsoPropertyMappings.Keys.ToArray<PropertyDefinition>();
		}

		// Token: 0x17001995 RID: 6549
		// (get) Token: 0x06005D7D RID: 23933 RVA: 0x0018BC74 File Offset: 0x00189E74
		public PropertyDefinition[] AllDependentXsoProperties
		{
			get
			{
				return (PropertyDefinition[])this.cachedXsoProperties.Clone();
			}
		}

		// Token: 0x06005D7E RID: 23934 RVA: 0x0018BC88 File Offset: 0x00189E88
		public XsoDriverPropertyDefinition GetRelatedWrapperProperty(PropertyDefinition def)
		{
			if (def == null)
			{
				throw new ArgumentNullException("def");
			}
			if (!(def is StorePropertyDefinition))
			{
				throw new NotSupportedException("GetRelatedWrapperProperty: def: " + def.GetType().FullName);
			}
			XsoDriverPropertyDefinition result;
			if (this.xsoPropertyMappings.TryGetValue(def, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x04003437 RID: 13367
		private Dictionary<PropertyDefinition, XsoDriverPropertyDefinition> xsoPropertyMappings;

		// Token: 0x04003438 RID: 13368
		private PropertyDefinition[] cachedXsoProperties;

		// Token: 0x04003439 RID: 13369
		public static readonly SimpleProviderPropertyDefinition MailboxOwnerId = new SimpleProviderPropertyDefinition("MailboxOwnerId", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), PropertyDefinitionFlags.Mandatory, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
