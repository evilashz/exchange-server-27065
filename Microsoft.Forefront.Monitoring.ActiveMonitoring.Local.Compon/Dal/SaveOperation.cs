using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Hygiene.Data;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Dal
{
	// Token: 0x0200006A RID: 106
	public class SaveOperation : ConfigDataProviderOperation
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x000109FC File Offset: 0x0000EBFC
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x00010A04 File Offset: 0x0000EC04
		public string Object { get; set; }

		// Token: 0x060002C4 RID: 708 RVA: 0x00010A10 File Offset: 0x0000EC10
		protected override void ExecuteConfigDataProviderOperation(IConfigDataProvider configDataProvider, IDictionary<string, object> variables)
		{
			IConfigurable configurable = (IConfigurable)DalProbeOperation.GetValue(this.Object, variables);
			ADObject adobject = configurable as ADObject;
			if (adobject != null)
			{
				adobject.SetId(new ADObjectId(DalHelper.GetTenantDistinguishedName(base.OrganizationTag), Guid.Empty));
			}
			configDataProvider.Save(configurable);
		}
	}
}
