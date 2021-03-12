using System;
using System.Text;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000812 RID: 2066
	[Serializable]
	public sealed class ServiceInstanceId : ObjectId
	{
		// Token: 0x0600661E RID: 26142 RVA: 0x00169246 File Offset: 0x00167446
		public ServiceInstanceId(string serviceInstanceId)
		{
			if (ServiceInstanceId.IsValidServiceInstanceId(serviceInstanceId))
			{
				this.InstanceId = serviceInstanceId;
				return;
			}
			throw new InvalidServiceInstanceIdException(serviceInstanceId);
		}

		// Token: 0x17002406 RID: 9222
		// (get) Token: 0x0600661F RID: 26143 RVA: 0x00169264 File Offset: 0x00167464
		// (set) Token: 0x06006620 RID: 26144 RVA: 0x0016926C File Offset: 0x0016746C
		public string InstanceId { get; private set; }

		// Token: 0x06006621 RID: 26145 RVA: 0x00169275 File Offset: 0x00167475
		public override byte[] GetBytes()
		{
			if (this.InstanceId != null)
			{
				return Encoding.Unicode.GetBytes(this.InstanceId);
			}
			return null;
		}

		// Token: 0x06006622 RID: 26146 RVA: 0x00169291 File Offset: 0x00167491
		public override string ToString()
		{
			return this.InstanceId;
		}

		// Token: 0x06006623 RID: 26147 RVA: 0x0016929C File Offset: 0x0016749C
		internal static string GetServiceInstanceId(string domainForestName)
		{
			string[] array = domainForestName.Split(new char[]
			{
				'.'
			});
			if (array.Length >= 2)
			{
				return string.Format("Exchange/{0}-{1}", array[0], 1);
			}
			return domainForestName;
		}

		// Token: 0x06006624 RID: 26148 RVA: 0x001692D8 File Offset: 0x001674D8
		internal static bool IsValidServiceInstanceId(string serviceInstanceId)
		{
			return !string.IsNullOrEmpty(serviceInstanceId) && serviceInstanceId.Length >= 12 && serviceInstanceId.Length <= 256 && serviceInstanceId.StartsWith("Exchange/", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06006625 RID: 26149 RVA: 0x00169308 File Offset: 0x00167508
		internal static string GetShortName(string serviceName)
		{
			if (serviceName.StartsWith("Exchange/", StringComparison.OrdinalIgnoreCase))
			{
				string text = serviceName.Substring("Exchange/".Length);
				if (text.LastIndexOf('-') > "Exchange/".Length)
				{
					text = text.Substring(0, text.LastIndexOf('-'));
				}
				return text;
			}
			return null;
		}

		// Token: 0x06006626 RID: 26150 RVA: 0x0016935C File Offset: 0x0016755C
		internal static SyncServiceInstance GetSyncServiceInstance(string serviceInstanceName)
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 160, "GetSyncServiceInstance", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Sync\\ServiceInstanceId.cs");
			topologyConfigurationSession.UseConfigNC = false;
			ADObjectId serviceInstanceObjectId = SyncServiceInstance.GetServiceInstanceObjectId(serviceInstanceName);
			return topologyConfigurationSession.Read<SyncServiceInstance>(serviceInstanceObjectId);
		}

		// Token: 0x06006627 RID: 26151 RVA: 0x001693A0 File Offset: 0x001675A0
		public bool Equals(ServiceInstanceId other)
		{
			return !object.ReferenceEquals(null, other) && (object.ReferenceEquals(this, other) || this.InstanceId.Equals(other.InstanceId, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x0400438A RID: 17290
		private const string ServiceInstanceFormat = "Exchange/{0}-{1}";

		// Token: 0x0400438B RID: 17291
		private const string ValidServiceInstanceFormatStartsWith = "Exchange/";

		// Token: 0x0400438C RID: 17292
		private const int ValidServiceInstanceMinimumLength = 12;

		// Token: 0x0400438D RID: 17293
		private const int ValidServiceInstanceMaximumLength = 256;
	}
}
