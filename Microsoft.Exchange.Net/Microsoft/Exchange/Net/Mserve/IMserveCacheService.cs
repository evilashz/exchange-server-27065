using System;
using System.ComponentModel;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Microsoft.Exchange.Net.Mserve
{
	// Token: 0x02000885 RID: 2181
	[ServiceContract(ConfigurationName = "Microsoft.Exchange.Net.Mserve.IMserveCacheService")]
	public interface IMserveCacheService
	{
		// Token: 0x06002E72 RID: 11890
		[Description("Get partner Id/minor partner Id from tenant name/domain name")]
		[WebGet]
		[OperationContract]
		string ReadMserveData(string requestName);

		// Token: 0x06002E73 RID: 11891
		[WebGet]
		[OperationContract]
		[Description("chunk size")]
		int GetChunkSize();
	}
}
