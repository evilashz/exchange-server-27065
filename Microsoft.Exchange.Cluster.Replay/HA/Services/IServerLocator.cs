using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Microsoft.Exchange.HA.Services
{
	// Token: 0x0200032A RID: 810
	[ServiceContract(Name = "ServerLocator", Namespace = "http://www.outlook.com/highavailability/ServerLocator/v1/")]
	public interface IServerLocator
	{
		// Token: 0x06002136 RID: 8502
		[OperationContract]
		ServiceVersion GetVersion();

		// Token: 0x06002137 RID: 8503
		[OperationContract]
		[FaultContract(typeof(DatabaseServerInformationFault))]
		DatabaseServerInformation GetServerForDatabase(DatabaseServerInformation database);

		// Token: 0x06002138 RID: 8504
		[FaultContract(typeof(DatabaseServerInformationFault))]
		[OperationContract]
		List<DatabaseServerInformation> GetActiveCopiesForDatabaseAvailabilityGroup();

		// Token: 0x06002139 RID: 8505
		[FaultContract(typeof(DatabaseServerInformationFault))]
		[OperationContract]
		List<DatabaseServerInformation> GetActiveCopiesForDatabaseAvailabilityGroupExtended(GetActiveCopiesForDatabaseAvailabilityGroupParameters parameters);
	}
}
