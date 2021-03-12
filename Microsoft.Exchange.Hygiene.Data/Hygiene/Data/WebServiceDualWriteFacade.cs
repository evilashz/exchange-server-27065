using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x0200011A RID: 282
	internal class WebServiceDualWriteFacade<T> : TenantSettingFacade<T> where T : ADObject, new()
	{
		// Token: 0x06000AE9 RID: 2793 RVA: 0x000216E9 File Offset: 0x0001F8E9
		public WebServiceDualWriteFacade(T adObject)
		{
			T t = adObject;
			if (adObject == null)
			{
				t = FacadeBase.NewADObject<T>();
			}
			base..ctor(t);
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00021708 File Offset: 0x0001F908
		public WebServiceDualWriteFacade() : this(default(T))
		{
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x00021724 File Offset: 0x0001F924
		public override IEnumerable<PropertyDefinition> GetPropertyDefinitions(bool isChangedOnly)
		{
			return base.GetPropertyDefinitions(isChangedOnly).Concat(new ADPropertyDefinition[]
			{
				ADObjectSchema.Id,
				ADObjectSchema.OrganizationalUnitRoot,
				ADObjectSchema.Name,
				WebServiceDualWriteFacade<T>.whenChangedUtc
			});
		}

		// Token: 0x0400058F RID: 1423
		private static readonly HygienePropertyDefinition whenChangedUtc = new HygienePropertyDefinition("WhenChangedUTC", typeof(DateTime), SqlDateTime.MinValue.Value, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
