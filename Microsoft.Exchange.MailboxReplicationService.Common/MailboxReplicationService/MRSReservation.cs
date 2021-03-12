using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200027E RID: 638
	internal class MRSReservation : ReservationBase
	{
		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06001F90 RID: 8080 RVA: 0x00042654 File Offset: 0x00040854
		public override bool IsActive
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x00042738 File Offset: 0x00040938
		protected override IEnumerable<ResourceBase> GetDependentResources()
		{
			yield return MRSResource.Cache.GetInstance(MRSResource.Id.ObjectGuid, base.WorkloadType);
			yield break;
		}
	}
}
