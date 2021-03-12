using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Properties.XSO
{
	// Token: 0x02000092 RID: 146
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class XSOPropertyManager : IXSOPropertyManager
	{
		// Token: 0x060003D6 RID: 982 RVA: 0x00015BF9 File Offset: 0x00013DF9
		protected XSOPropertyManager()
		{
			this.allProperties = new List<PropertyDefinition>();
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00015C0C File Offset: 0x00013E0C
		public PropertyDefinition[] AllProperties
		{
			get
			{
				if (this.cachedAllProperties == null || this.cachedAllProperties.Length != this.allProperties.Count)
				{
					this.cachedAllProperties = new PropertyDefinition[this.allProperties.Count];
					this.allProperties.CopyTo(this.cachedAllProperties);
				}
				return this.cachedAllProperties;
			}
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00015C63 File Offset: 0x00013E63
		public void AddPropertyDefinition(PropertyDefinition propertyDefinition)
		{
			SyncUtilities.ThrowIfArgumentNull("propertyDefinition", propertyDefinition);
			this.allProperties.Add(propertyDefinition);
		}

		// Token: 0x040001ED RID: 493
		private List<PropertyDefinition> allProperties;

		// Token: 0x040001EE RID: 494
		private PropertyDefinition[] cachedAllProperties;
	}
}
