using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200012C RID: 300
	[Serializable]
	public class ConnectedDomain
	{
		// Token: 0x06000A83 RID: 2691 RVA: 0x00020C76 File Offset: 0x0001EE76
		public ConnectedDomain(string organizationName, string adminGroupName, Guid routingGroupGuid, AddressSpace addressSpace)
		{
			this.organizationName = organizationName;
			this.adminGroupName = adminGroupName;
			this.routingGroupGuid = routingGroupGuid;
			this.addressSpace = addressSpace;
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x00020C9B File Offset: 0x0001EE9B
		public string PrintableName
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x00020CA3 File Offset: 0x0001EEA3
		public string OrganizationName
		{
			get
			{
				return this.organizationName;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x00020CAB File Offset: 0x0001EEAB
		public string AdminGroupName
		{
			get
			{
				return this.adminGroupName;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x00020CB3 File Offset: 0x0001EEB3
		public Guid RoutingGroupGuid
		{
			get
			{
				return this.routingGroupGuid;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x00020CBB File Offset: 0x0001EEBB
		public AddressSpace AddressSpace
		{
			get
			{
				return this.addressSpace;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x00020CC3 File Offset: 0x0001EEC3
		public int Cost
		{
			get
			{
				return this.addressSpace.Cost;
			}
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00020CD0 File Offset: 0x0001EED0
		public static ConnectedDomain Parse(string s)
		{
			ConnectedDomain result;
			if (!ConnectedDomain.TryParse(s, out result))
			{
				throw new FormatException(DataStrings.InvalidConnectedDomainFormat(s));
			}
			return result;
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x00020CFC File Offset: 0x0001EEFC
		public static bool TryParse(string s, out ConnectedDomain connectedDomain)
		{
			connectedDomain = null;
			int num = s.IndexOf(ConnectedDomain.Separator);
			if (-1 == num)
			{
				return false;
			}
			string text = s.Substring(0, num);
			int num2 = s.IndexOf(ConnectedDomain.Separator, num + 1);
			if (-1 == num2)
			{
				return false;
			}
			string text2 = s.Substring(num + 1, num2 - num - 1);
			int num3 = s.IndexOf(ConnectedDomain.Separator, num2 + 1);
			if (-1 == num3 || num3 - num2 < 4)
			{
				return false;
			}
			string g = s.Substring(num2 + 1, num3 - num2 - 1);
			Guid guid;
			if (!GuidHelper.TryParseGuid(g, out guid))
			{
				return false;
			}
			int num4 = 1;
			int num5 = s.IndexOf(ConnectedDomain.Separator, num3 + 1);
			if (-1 != num5)
			{
				string s2 = s.Substring(num3 + 1, num5 - num3 - 1);
				if (!int.TryParse(s2, out num4) || num4 < 1 || num4 > 100)
				{
					return false;
				}
			}
			else
			{
				num5 = num3;
			}
			string address = s.Substring(num5 + 1, s.Length - num5 - 1);
			AddressSpace addressSpace;
			if (!AddressSpace.TryParse(address, out addressSpace, false))
			{
				return false;
			}
			addressSpace.Cost = num4;
			connectedDomain = new ConnectedDomain(text, text2, guid, addressSpace);
			return true;
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x00020E0C File Offset: 0x0001F00C
		public bool Equals(ConnectedDomain value)
		{
			return object.ReferenceEquals(this, value) || (this.organizationName.Equals(value.organizationName, StringComparison.OrdinalIgnoreCase) && this.adminGroupName.Equals(value.adminGroupName, StringComparison.OrdinalIgnoreCase) && this.routingGroupGuid.Equals(value.routingGroupGuid) && this.addressSpace.Equals(value.addressSpace));
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x00020E74 File Offset: 0x0001F074
		public override bool Equals(object comparand)
		{
			ConnectedDomain connectedDomain = comparand as ConnectedDomain;
			return connectedDomain != null && this.Equals(connectedDomain);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00020E94 File Offset: 0x0001F094
		public override int GetHashCode()
		{
			return this.organizationName.GetHashCode() ^ this.adminGroupName.GetHashCode() ^ this.routingGroupGuid.GetHashCode() ^ this.addressSpace.GetHashCode();
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00020ECC File Offset: 0x0001F0CC
		public override string ToString()
		{
			return string.Format("{0}{1}{2}{1}{{{3}}}{1}{4}{1}{5}:{6}", new object[]
			{
				this.organizationName,
				ConnectedDomain.Separator,
				this.adminGroupName,
				this.routingGroupGuid,
				this.addressSpace.Cost,
				this.addressSpace.Type,
				this.addressSpace.Address
			});
		}

		// Token: 0x04000661 RID: 1633
		private static readonly char Separator = Convert.ToChar(167);

		// Token: 0x04000662 RID: 1634
		private string organizationName;

		// Token: 0x04000663 RID: 1635
		private string adminGroupName;

		// Token: 0x04000664 RID: 1636
		private Guid routingGroupGuid;

		// Token: 0x04000665 RID: 1637
		private AddressSpace addressSpace;
	}
}
