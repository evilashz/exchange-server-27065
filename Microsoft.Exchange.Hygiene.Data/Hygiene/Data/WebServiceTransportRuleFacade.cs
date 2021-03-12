using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x0200011B RID: 283
	internal class WebServiceTransportRuleFacade : TenantSettingFacade<TransportRule>
	{
		// Token: 0x06000AED RID: 2797 RVA: 0x000217A2 File Offset: 0x0001F9A2
		public WebServiceTransportRuleFacade(TransportRule transportRule) : base(transportRule ?? FacadeBase.NewADObject<TransportRule>())
		{
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x000217B4 File Offset: 0x0001F9B4
		public WebServiceTransportRuleFacade() : this(null)
		{
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x000217C0 File Offset: 0x0001F9C0
		public override IEnumerable<PropertyDefinition> GetPropertyDefinitions(bool isChangedOnly)
		{
			return base.GetPropertyDefinitions(isChangedOnly).Concat(new ADPropertyDefinition[]
			{
				ADObjectSchema.Id,
				ADObjectSchema.OrganizationalUnitRoot,
				ADObjectSchema.Name,
				WebServiceTransportRuleFacade.whenChangedUtc
			});
		}

		// Token: 0x04000590 RID: 1424
		private static readonly HygienePropertyDefinition whenChangedUtc = new HygienePropertyDefinition("WhenChangedUTC", typeof(DateTime), SqlDateTime.MinValue.Value, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
