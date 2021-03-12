using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x020000AA RID: 170
	[CollectionDataContract(Name = "OrganizationRelationshipSettingsCollection", ItemName = "OrganizationRelationshipSettings", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class OrganizationRelationshipSettingsCollection : Collection<OrganizationRelationshipSettings>
	{
		// Token: 0x06000415 RID: 1045 RVA: 0x00017CA2 File Offset: 0x00015EA2
		public OrganizationRelationshipSettingsCollection()
		{
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00017CAC File Offset: 0x00015EAC
		public OrganizationRelationshipSettingsCollection(ICollection<OrganizationRelationshipSettings> settings)
		{
			foreach (OrganizationRelationshipSettings item in settings)
			{
				base.Add(item);
			}
		}
	}
}
