using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004F6 RID: 1270
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "GetDiscoverySearchConfigurationResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetDiscoverySearchConfigurationResponse : ResponseMessage
	{
		// Token: 0x060024DB RID: 9435 RVA: 0x000A538C File Offset: 0x000A358C
		public GetDiscoverySearchConfigurationResponse()
		{
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x000A539F File Offset: 0x000A359F
		internal GetDiscoverySearchConfigurationResponse(ServiceResultCode code, ServiceError error, DiscoverySearchConfiguration[] configuration) : base(code, error)
		{
			if (configuration != null && configuration.Length > 0)
			{
				this.configurations.AddRange(configuration);
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x060024DD RID: 9437 RVA: 0x000A53C9 File Offset: 0x000A35C9
		// (set) Token: 0x060024DE RID: 9438 RVA: 0x000A53D6 File Offset: 0x000A35D6
		[DataMember(Name = "DiscoverySearchConfiguration", IsRequired = false)]
		[XmlArray]
		[XmlArrayItem("DiscoverySearchConfiguration", Type = typeof(DiscoverySearchConfiguration), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public DiscoverySearchConfiguration[] DiscoverySearchConfigurations
		{
			get
			{
				return this.configurations.ToArray();
			}
			set
			{
				this.configurations.Clear();
				if (value != null && value.Length > 0)
				{
					this.configurations.AddRange(value);
				}
			}
		}

		// Token: 0x0400159A RID: 5530
		private List<DiscoverySearchConfiguration> configurations = new List<DiscoverySearchConfiguration>();
	}
}
