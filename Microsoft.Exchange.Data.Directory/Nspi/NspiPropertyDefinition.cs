using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Nspi
{
	// Token: 0x020001CF RID: 463
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class NspiPropertyDefinition : ADPropertyDefinition
	{
		// Token: 0x17000320 RID: 800
		// (get) Token: 0x060012E0 RID: 4832 RVA: 0x0005AAEC File Offset: 0x00058CEC
		public PropTag PropTag
		{
			get
			{
				return this.propTag;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060012E1 RID: 4833 RVA: 0x0005AAF4 File Offset: 0x00058CF4
		public bool MemberOfGlobalCatalog
		{
			get
			{
				return this.memberOfGlobalCatalog;
			}
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x0005AAFC File Offset: 0x00058CFC
		public NspiPropertyDefinition(PropTag propTag, Type type, string ldapDisplayName, ADPropertyDefinitionFlags flags, object defaultValue, bool memberOfGlobalCatalog) : base(propTag.ToString(), ExchangeObjectVersion.Exchange2003, type, ldapDisplayName, flags, defaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null)
		{
			this.propTag = propTag;
			this.memberOfGlobalCatalog = memberOfGlobalCatalog;
		}

		// Token: 0x04000ACA RID: 2762
		private readonly PropTag propTag;

		// Token: 0x04000ACB RID: 2763
		private readonly bool memberOfGlobalCatalog;
	}
}
