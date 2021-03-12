using System;
using System.CodeDom.Compiler;
using System.ServiceModel;
using www.outlook.com.highavailability.ServerLocator.v1;

// Token: 0x02000D39 RID: 3385
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
[ServiceContract(Namespace = "http://www.outlook.com/highavailability/ServerLocator/v1/", ConfigurationName = "ServerLocator")]
public interface ServerLocator
{
	// Token: 0x0600755D RID: 30045
	[OperationContract(Action = "http://www.outlook.com/highavailability/ServerLocator/v1/ServerLocator/GetVersion", ReplyAction = "http://www.outlook.com/highavailability/ServerLocator/v1/ServerLocator/GetVersionResponse")]
	ServiceVersion GetVersion();

	// Token: 0x0600755E RID: 30046
	[OperationContract(AsyncPattern = true, Action = "http://www.outlook.com/highavailability/ServerLocator/v1/ServerLocator/GetVersion", ReplyAction = "http://www.outlook.com/highavailability/ServerLocator/v1/ServerLocator/GetVersionResponse")]
	IAsyncResult BeginGetVersion(AsyncCallback callback, object asyncState);

	// Token: 0x0600755F RID: 30047
	ServiceVersion EndGetVersion(IAsyncResult result);

	// Token: 0x06007560 RID: 30048
	[OperationContract(Action = "http://www.outlook.com/highavailability/ServerLocator/v1/ServerLocator/GetServerForDatabase", ReplyAction = "http://www.outlook.com/highavailability/ServerLocator/v1/ServerLocator/GetServerForDatabaseResponse")]
	[FaultContract(typeof(DatabaseServerInformationFault), Action = "http://www.outlook.com/highavailability/ServerLocator/v1/ServerLocator/GetServerForDatabaseDatabaseServerInformationFaultFault", Name = "DatabaseServerInformationFault")]
	DatabaseServerInformation GetServerForDatabase(DatabaseServerInformation database);

	// Token: 0x06007561 RID: 30049
	[OperationContract(AsyncPattern = true, Action = "http://www.outlook.com/highavailability/ServerLocator/v1/ServerLocator/GetServerForDatabase", ReplyAction = "http://www.outlook.com/highavailability/ServerLocator/v1/ServerLocator/GetServerForDatabaseResponse")]
	IAsyncResult BeginGetServerForDatabase(DatabaseServerInformation database, AsyncCallback callback, object asyncState);

	// Token: 0x06007562 RID: 30050
	DatabaseServerInformation EndGetServerForDatabase(IAsyncResult result);

	// Token: 0x06007563 RID: 30051
	[FaultContract(typeof(DatabaseServerInformationFault), Action = "http://www.outlook.com/highavailability/ServerLocator/v1/ServerLocator/GetActiveCopiesForDatabaseAvailabilityGroupDatabaseServerInformationFaultFault", Name = "DatabaseServerInformationFault")]
	[OperationContract(Action = "http://www.outlook.com/highavailability/ServerLocator/v1/ServerLocator/GetActiveCopiesForDatabaseAvailabilityGroup", ReplyAction = "http://www.outlook.com/highavailability/ServerLocator/v1/ServerLocator/GetActiveCopiesForDatabaseAvailabilityGroupResponse")]
	DatabaseServerInformation[] GetActiveCopiesForDatabaseAvailabilityGroup();

	// Token: 0x06007564 RID: 30052
	[OperationContract(AsyncPattern = true, Action = "http://www.outlook.com/highavailability/ServerLocator/v1/ServerLocator/GetActiveCopiesForDatabaseAvailabilityGroup", ReplyAction = "http://www.outlook.com/highavailability/ServerLocator/v1/ServerLocator/GetActiveCopiesForDatabaseAvailabilityGroupResponse")]
	IAsyncResult BeginGetActiveCopiesForDatabaseAvailabilityGroup(AsyncCallback callback, object asyncState);

	// Token: 0x06007565 RID: 30053
	DatabaseServerInformation[] EndGetActiveCopiesForDatabaseAvailabilityGroup(IAsyncResult result);

	// Token: 0x06007566 RID: 30054
	[OperationContract(Action = "http://www.outlook.com/highavailability/ServerLocator/v1/ServerLocator/GetActiveCopiesForDatabaseAvailabilityGroupExtended", ReplyAction = "http://www.outlook.com/highavailability/ServerLocator/v1/ServerLocator/GetActiveCopiesForDatabaseAvailabilityGroupExtendedResponse")]
	[FaultContract(typeof(DatabaseServerInformationFault), Action = "http://www.outlook.com/highavailability/ServerLocator/v1/ServerLocator/GetActiveCopiesForDatabaseAvailabilityGroupExtendedDatabaseServerInformationFaultFault", Name = "DatabaseServerInformationFault")]
	DatabaseServerInformation[] GetActiveCopiesForDatabaseAvailabilityGroupExtended(GetActiveCopiesForDatabaseAvailabilityGroupParameters parameters);

	// Token: 0x06007567 RID: 30055
	[OperationContract(AsyncPattern = true, Action = "http://www.outlook.com/highavailability/ServerLocator/v1/ServerLocator/GetActiveCopiesForDatabaseAvailabilityGroupExtended", ReplyAction = "http://www.outlook.com/highavailability/ServerLocator/v1/ServerLocator/GetActiveCopiesForDatabaseAvailabilityGroupExtendedResponse")]
	IAsyncResult BeginGetActiveCopiesForDatabaseAvailabilityGroupExtended(GetActiveCopiesForDatabaseAvailabilityGroupParameters parameters, AsyncCallback callback, object asyncState);

	// Token: 0x06007568 RID: 30056
	DatabaseServerInformation[] EndGetActiveCopiesForDatabaseAvailabilityGroupExtended(IAsyncResult result);
}
