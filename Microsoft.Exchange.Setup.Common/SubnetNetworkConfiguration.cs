using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200005B RID: 91
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class SubnetNetworkConfiguration
	{
		// Token: 0x06000489 RID: 1161 RVA: 0x0000FA27 File Offset: 0x0000DC27
		public SubnetNetworkConfiguration(NetworkConfiguration ipv4NetworkConfiguration, NetworkConfiguration ipv6NetworkConfiguration)
		{
			this.ipv4NetworkConfiguration = ipv4NetworkConfiguration;
			this.ipv6NetworkConfiguration = ipv6NetworkConfiguration;
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x0000FA3D File Offset: 0x0000DC3D
		public NetworkConfiguration Ipv4NetworkConfiguration
		{
			get
			{
				return this.ipv4NetworkConfiguration;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0000FA45 File Offset: 0x0000DC45
		public NetworkConfiguration Ipv6NetworkConfiguration
		{
			get
			{
				return this.ipv6NetworkConfiguration;
			}
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000FA4D File Offset: 0x0000DC4D
		public static bool Equals(SubnetNetworkConfiguration subnetNetworkConfigurationA, SubnetNetworkConfiguration subnetNetworkConfigurationB)
		{
			if (subnetNetworkConfigurationA != null)
			{
				return subnetNetworkConfigurationA.Equals(subnetNetworkConfigurationB);
			}
			return subnetNetworkConfigurationB == null;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000FA5E File Offset: 0x0000DC5E
		public bool Equals(SubnetNetworkConfiguration subnetNetworkConfiguration)
		{
			return subnetNetworkConfiguration != null && NetworkConfiguration.Equals(this.Ipv4NetworkConfiguration, subnetNetworkConfiguration.Ipv4NetworkConfiguration) && NetworkConfiguration.Equals(this.Ipv6NetworkConfiguration, subnetNetworkConfiguration.Ipv6NetworkConfiguration);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000FA8B File Offset: 0x0000DC8B
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SubnetNetworkConfiguration);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000FA9C File Offset: 0x0000DC9C
		public override int GetHashCode()
		{
			int num = (this.Ipv4NetworkConfiguration == null) ? 0 : this.Ipv4NetworkConfiguration.GetHashCode();
			int num2 = (this.Ipv6NetworkConfiguration == null) ? 0 : this.Ipv6NetworkConfiguration.GetHashCode();
			return num + num2;
		}

		// Token: 0x0400018A RID: 394
		private NetworkConfiguration ipv4NetworkConfiguration;

		// Token: 0x0400018B RID: 395
		private NetworkConfiguration ipv6NetworkConfiguration;
	}
}
