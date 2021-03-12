using System;
using System.Collections.Generic;
using System.Management;
using System.Net;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B37 RID: 2871
	[Serializable]
	public class NetworkConnectionInfo : IConfigurable, IEquatable<NetworkConnectionInfo>
	{
		// Token: 0x06006730 RID: 26416 RVA: 0x001AB638 File Offset: 0x001A9838
		public NetworkConnectionInfo(string name, Guid adapterGuid, IPAddress[] ipAddresses, IPAddress[] dnsServers, string macAddress)
		{
			this.adapterGuid = adapterGuid;
			this.ipAddresses = ipAddresses;
			this.name = name;
			this.dnsServers = dnsServers;
			this.macAddress = macAddress;
		}

		// Token: 0x17001FA6 RID: 8102
		// (get) Token: 0x06006731 RID: 26417 RVA: 0x001AB665 File Offset: 0x001A9865
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17001FA7 RID: 8103
		// (get) Token: 0x06006732 RID: 26418 RVA: 0x001AB66D File Offset: 0x001A986D
		public IPAddress[] DnsServers
		{
			get
			{
				return this.dnsServers;
			}
		}

		// Token: 0x17001FA8 RID: 8104
		// (get) Token: 0x06006733 RID: 26419 RVA: 0x001AB675 File Offset: 0x001A9875
		public IPAddress[] IPAddresses
		{
			get
			{
				return this.ipAddresses;
			}
		}

		// Token: 0x17001FA9 RID: 8105
		// (get) Token: 0x06006734 RID: 26420 RVA: 0x001AB67D File Offset: 0x001A987D
		public Guid AdapterGuid
		{
			get
			{
				return this.adapterGuid;
			}
		}

		// Token: 0x17001FAA RID: 8106
		// (get) Token: 0x06006735 RID: 26421 RVA: 0x001AB685 File Offset: 0x001A9885
		public string MacAddress
		{
			get
			{
				return this.macAddress;
			}
		}

		// Token: 0x17001FAB RID: 8107
		// (get) Token: 0x06006736 RID: 26422 RVA: 0x001AB68D File Offset: 0x001A988D
		bool IConfigurable.IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001FAC RID: 8108
		// (get) Token: 0x06006737 RID: 26423 RVA: 0x001AB690 File Offset: 0x001A9890
		ObjectState IConfigurable.ObjectState
		{
			get
			{
				return ObjectState.Unchanged;
			}
		}

		// Token: 0x17001FAD RID: 8109
		// (get) Token: 0x06006738 RID: 26424 RVA: 0x001AB694 File Offset: 0x001A9894
		ObjectId IConfigurable.Identity
		{
			get
			{
				return new ConfigObjectId(this.adapterGuid.ToString());
			}
		}

		// Token: 0x06006739 RID: 26425 RVA: 0x001AB6BC File Offset: 0x001A98BC
		internal static IList<NetworkConnectionInfo> GetConnectionInfo(ManagementScope scope)
		{
			NetworkConnectionInfo[] array = null;
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(scope, new ObjectQuery("select * from Win32_NetworkAdapterConfiguration where IPEnabled = true")))
			{
				using (ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get())
				{
					array = new NetworkConnectionInfo[managementObjectCollection.Count];
					int num = 0;
					foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
					{
						ManagementObject managementObject = (ManagementObject)managementBaseObject;
						string text = (string)managementObject["Description"];
						Guid guid = new Guid((string)managementObject["SettingID"]);
						string[] array2 = (string[])managementObject["IPAddress"];
						IPAddress[] array3;
						if (array2 != null)
						{
							array3 = new IPAddress[array2.Length];
							for (int i = 0; i < array2.Length; i++)
							{
								array3[i] = IPAddress.Parse(array2[i]);
							}
						}
						else
						{
							array3 = new IPAddress[0];
						}
						string[] array4 = (string[])managementObject["DNSServerSearchOrder"];
						IPAddress[] array5;
						if (array4 != null)
						{
							array5 = new IPAddress[array4.Length];
							for (int j = 0; j < array4.Length; j++)
							{
								array5[j] = IPAddress.Parse(array4[j]);
							}
						}
						else
						{
							array5 = new IPAddress[0];
						}
						string text2 = (string)managementObject["MACAddress"];
						array[num++] = new NetworkConnectionInfo(text, guid, array3, array5, text2);
					}
				}
			}
			return array;
		}

		// Token: 0x0600673A RID: 26426 RVA: 0x001AB87C File Offset: 0x001A9A7C
		ValidationError[] IConfigurable.Validate()
		{
			return ValidationError.None;
		}

		// Token: 0x0600673B RID: 26427 RVA: 0x001AB883 File Offset: 0x001A9A83
		void IConfigurable.CopyChangesFrom(IConfigurable source)
		{
		}

		// Token: 0x0600673C RID: 26428 RVA: 0x001AB885 File Offset: 0x001A9A85
		void IConfigurable.ResetChangeTracking()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600673D RID: 26429 RVA: 0x001AB88C File Offset: 0x001A9A8C
		public bool Equals(NetworkConnectionInfo networkConnectionInfo)
		{
			return networkConnectionInfo != null && this.AdapterGuid.Equals(networkConnectionInfo.AdapterGuid);
		}

		// Token: 0x0600673E RID: 26430 RVA: 0x001AB8B2 File Offset: 0x001A9AB2
		public override bool Equals(object obj)
		{
			return this.Equals(obj as NetworkConnectionInfo);
		}

		// Token: 0x0600673F RID: 26431 RVA: 0x001AB8C0 File Offset: 0x001A9AC0
		public override int GetHashCode()
		{
			return this.AdapterGuid.GetHashCode();
		}

		// Token: 0x04003661 RID: 13921
		internal const int HresultRpcUnavailable = -2147023174;

		// Token: 0x04003662 RID: 13922
		private const string Query = "select * from Win32_NetworkAdapterConfiguration where IPEnabled = true";

		// Token: 0x04003663 RID: 13923
		private readonly Guid adapterGuid;

		// Token: 0x04003664 RID: 13924
		private readonly string name;

		// Token: 0x04003665 RID: 13925
		private IPAddress[] dnsServers;

		// Token: 0x04003666 RID: 13926
		private readonly string macAddress;

		// Token: 0x04003667 RID: 13927
		private IPAddress[] ipAddresses;
	}
}
