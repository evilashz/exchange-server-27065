using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000146 RID: 326
	[DataContract(Name = "UMReportTuple", Namespace = "http://schemas.microsoft.com/v1.0/UMReportAggregatedData")]
	internal class UMReportTuple : IEquatable<UMReportTuple>
	{
		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x0002711A File Offset: 0x0002531A
		// (set) Token: 0x06000A75 RID: 2677 RVA: 0x00027122 File Offset: 0x00025322
		[DataMember(Name = "DialPlanGuid")]
		public Guid DialPlanGuid { get; private set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x0002712B File Offset: 0x0002532B
		// (set) Token: 0x06000A77 RID: 2679 RVA: 0x00027133 File Offset: 0x00025333
		[DataMember(Name = "GatewayGuid")]
		public Guid GatewayGuid { get; private set; }

		// Token: 0x06000A78 RID: 2680 RVA: 0x0002713C File Offset: 0x0002533C
		public UMReportTuple() : this(Guid.Empty, Guid.Empty)
		{
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0002714E File Offset: 0x0002534E
		public UMReportTuple(Guid dpGuid, Guid gwGuid)
		{
			this.DialPlanGuid = dpGuid;
			this.GatewayGuid = gwGuid;
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00027164 File Offset: 0x00025364
		public static bool operator ==(UMReportTuple tuple1, UMReportTuple tuple2)
		{
			return tuple1.Equals(tuple2);
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0002716D File Offset: 0x0002536D
		public static bool operator !=(UMReportTuple tuple1, UMReportTuple tuple2)
		{
			return !tuple1.Equals(tuple2);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0002717C File Offset: 0x0002537C
		public static UMReportTuple[] GetTuplesToAddInReport(CDRData cdrData)
		{
			if (cdrData.DialPlanGuid == Guid.Empty)
			{
				throw new InvalidArgumentException("cdrData.DialPlanGuid cannot be empty.");
			}
			List<UMReportTuple> list = new List<UMReportTuple>
			{
				new UMReportTuple(),
				new UMReportTuple(cdrData.DialPlanGuid, Guid.Empty)
			};
			if (cdrData.GatewayGuid != Guid.Empty)
			{
				list.Add(new UMReportTuple(Guid.Empty, cdrData.GatewayGuid));
				list.Add(new UMReportTuple(cdrData.DialPlanGuid, cdrData.GatewayGuid));
			}
			return list.ToArray();
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x00027214 File Offset: 0x00025414
		public bool Equals(UMReportTuple other)
		{
			return this.DialPlanGuid == this.DialPlanGuid && this.GatewayGuid == other.GatewayGuid;
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0002723C File Offset: 0x0002543C
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return base.Equals(other);
			}
			UMReportTuple umreportTuple = other as UMReportTuple;
			if (umreportTuple == null)
			{
				throw new InvalidCastException("Comparison of only UMReportTuple type is allowed.");
			}
			return this.Equals(umreportTuple);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00027278 File Offset: 0x00025478
		public override int GetHashCode()
		{
			return this.DialPlanGuid.GetHashCode() ^ this.GatewayGuid.GetHashCode();
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x000272B0 File Offset: 0x000254B0
		public bool ShouldRemoveFromReport(OrganizationId orgId)
		{
			IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(orgId);
			return (this.DialPlanGuid != Guid.Empty && iadsystemConfigurationLookup.GetDialPlanFromId(new ADObjectId(this.DialPlanGuid)) == null) || (this.GatewayGuid != Guid.Empty && iadsystemConfigurationLookup.GetIPGatewayFromId(new ADObjectId(this.GatewayGuid)) == null);
		}
	}
}
