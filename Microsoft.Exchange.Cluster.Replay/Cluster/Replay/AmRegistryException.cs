using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000466 RID: 1126
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmRegistryException : AmCommonException
	{
		// Token: 0x06002B94 RID: 11156 RVA: 0x000BDA6D File Offset: 0x000BBC6D
		public AmRegistryException(string apiName) : base(ReplayStrings.AmRegistryException(apiName))
		{
			this.apiName = apiName;
		}

		// Token: 0x06002B95 RID: 11157 RVA: 0x000BDA87 File Offset: 0x000BBC87
		public AmRegistryException(string apiName, Exception innerException) : base(ReplayStrings.AmRegistryException(apiName), innerException)
		{
			this.apiName = apiName;
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x000BDAA2 File Offset: 0x000BBCA2
		protected AmRegistryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.apiName = (string)info.GetValue("apiName", typeof(string));
		}

		// Token: 0x06002B97 RID: 11159 RVA: 0x000BDACC File Offset: 0x000BBCCC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("apiName", this.apiName);
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x06002B98 RID: 11160 RVA: 0x000BDAE7 File Offset: 0x000BBCE7
		public string ApiName
		{
			get
			{
				return this.apiName;
			}
		}

		// Token: 0x040014A3 RID: 5283
		private readonly string apiName;
	}
}
