using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000215 RID: 533
	[Serializable]
	public class DatabaseAvailabilityGroupSubnetId
	{
		// Token: 0x060012AA RID: 4778 RVA: 0x00039738 File Offset: 0x00037938
		public DatabaseAvailabilityGroupSubnetId(string expression)
		{
			this.m_ipRange = IPRange.Parse(expression);
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x0003974C File Offset: 0x0003794C
		public DatabaseAvailabilityGroupSubnetId(IPRange ipRange)
		{
			this.m_ipRange = ipRange;
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x0003975B File Offset: 0x0003795B
		public DatabaseAvailabilityGroupSubnetId(DatabaseAvailabilityGroupNetworkSubnet subnet)
		{
			this.m_ipRange = subnet.SubnetId.IPRange;
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x00039774 File Offset: 0x00037974
		public static bool TryParse(string expression, out DatabaseAvailabilityGroupSubnetId subnetId)
		{
			subnetId = null;
			IPRange ipRange = null;
			if (IPRange.TryParse(expression, out ipRange))
			{
				subnetId = new DatabaseAvailabilityGroupSubnetId(ipRange);
				return true;
			}
			return false;
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x0003979C File Offset: 0x0003799C
		public static DatabaseAvailabilityGroupSubnetId Parse(string expression)
		{
			IPRange ipRange = IPRange.Parse(expression);
			return new DatabaseAvailabilityGroupSubnetId(ipRange);
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x000397B6 File Offset: 0x000379B6
		public IPRange IPRange
		{
			get
			{
				return this.m_ipRange;
			}
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x000397BE File Offset: 0x000379BE
		public override string ToString()
		{
			return this.m_ipRange.ToString();
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x000397CC File Offset: 0x000379CC
		public static bool Equals(DatabaseAvailabilityGroupSubnetId s1, DatabaseAvailabilityGroupSubnetId s2)
		{
			int num = DagSubnetIdComparer.Comparer.Compare(s1, s2);
			return num == 0;
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x000397EA File Offset: 0x000379EA
		public bool Equals(DatabaseAvailabilityGroupSubnetId other)
		{
			return DatabaseAvailabilityGroupSubnetId.Equals(this, other);
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x000397F3 File Offset: 0x000379F3
		public override bool Equals(object other)
		{
			return this.Equals(other as DatabaseAvailabilityGroupSubnetId);
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x00039801 File Offset: 0x00037A01
		public override int GetHashCode()
		{
			return this.m_ipRange.GetHashCode();
		}

		// Token: 0x04000B1B RID: 2843
		private IPRange m_ipRange;
	}
}
