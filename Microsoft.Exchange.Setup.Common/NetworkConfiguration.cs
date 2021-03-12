using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000045 RID: 69
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class NetworkConfiguration
	{
		// Token: 0x06000327 RID: 807 RVA: 0x0000B01E File Offset: 0x0000921E
		public NetworkConfiguration(IPAddress ipAddress, string networkName)
		{
			this.ipAddress = ipAddress;
			this.networkName = networkName;
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000B034 File Offset: 0x00009234
		public IPAddress IPAddress
		{
			get
			{
				return this.ipAddress;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000B03C File Offset: 0x0000923C
		public string NetworkName
		{
			get
			{
				return this.networkName;
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000B044 File Offset: 0x00009244
		public static bool Equals(NetworkConfiguration networkConfigurationA, NetworkConfiguration networkConfigurationB)
		{
			if (networkConfigurationA != null)
			{
				return networkConfigurationA.Equals(networkConfigurationB);
			}
			return networkConfigurationB == null;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000B058 File Offset: 0x00009258
		public bool Equals(NetworkConfiguration networkConfiguration)
		{
			return networkConfiguration != null && this.IPAddress == networkConfiguration.IPAddress && ((string.IsNullOrEmpty(this.NetworkName) && string.IsNullOrEmpty(networkConfiguration.NetworkName)) || this.NetworkName == networkConfiguration.NetworkName);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000B0A7 File Offset: 0x000092A7
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NetworkConfiguration);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000B0B8 File Offset: 0x000092B8
		public override int GetHashCode()
		{
			int num = (this.IPAddress == null) ? 0 : this.IPAddress.GetHashCode();
			int num2 = string.IsNullOrEmpty(this.NetworkName) ? 0 : this.NetworkName.GetHashCode();
			return num + num2;
		}

		// Token: 0x040000C0 RID: 192
		private IPAddress ipAddress;

		// Token: 0x040000C1 RID: 193
		private string networkName;
	}
}
