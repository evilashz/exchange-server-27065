using System;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Connections.Eas.Commands.Connect;
using Microsoft.Exchange.Connections.Eas.Commands.Disconnect;
using Microsoft.Exchange.Connections.Eas.Commands.FolderCreate;
using Microsoft.Exchange.Connections.Eas.Commands.FolderDelete;
using Microsoft.Exchange.Connections.Eas.Commands.FolderSync;
using Microsoft.Exchange.Connections.Eas.Commands.FolderUpdate;
using Microsoft.Exchange.Connections.Eas.Commands.GetItemEstimate;
using Microsoft.Exchange.Connections.Eas.Commands.ItemOperations;
using Microsoft.Exchange.Connections.Eas.Commands.MoveItems;
using Microsoft.Exchange.Connections.Eas.Commands.Options;
using Microsoft.Exchange.Connections.Eas.Commands.SendMail;
using Microsoft.Exchange.Connections.Eas.Commands.Settings;
using Microsoft.Exchange.Connections.Eas.Commands.Sync;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas
{
	// Token: 0x02000002 RID: 2
	[ClassAccessLevel(AccessLevel.Implementation)]
	public interface IEasConnection : IConnection<IEasConnection>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		string ServerName { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2
		UserSmtpAddress UserSmtpAddress { get; }

		// Token: 0x06000003 RID: 3
		ConnectResponse Connect(ConnectRequest connectRequest, IServerCapabilities capabilities = null);

		// Token: 0x06000004 RID: 4
		OperationStatusCode TestLogon();

		// Token: 0x06000005 RID: 5
		DisconnectResponse Disconnect(DisconnectRequest disconnectRequest);

		// Token: 0x06000006 RID: 6
		FolderCreateResponse FolderCreate(FolderCreateRequest folderCreateRequest);

		// Token: 0x06000007 RID: 7
		FolderDeleteResponse FolderDelete(FolderDeleteRequest folderDeleteRequest);

		// Token: 0x06000008 RID: 8
		FolderSyncResponse FolderSync(FolderSyncRequest folderSyncRequest);

		// Token: 0x06000009 RID: 9
		FolderUpdateResponse FolderUpdate(FolderUpdateRequest folderUpdateRequest);

		// Token: 0x0600000A RID: 10
		GetItemEstimateResponse GetItemEstimate(GetItemEstimateRequest getItemEstimateRequest);

		// Token: 0x0600000B RID: 11
		ItemOperationsResponse ItemOperations(ItemOperationsRequest itemOperationsRequest);

		// Token: 0x0600000C RID: 12
		MoveItemsResponse MoveItems(MoveItemsRequest moveItemsRequest);

		// Token: 0x0600000D RID: 13
		OptionsResponse Options(OptionsRequest optionsRequest);

		// Token: 0x0600000E RID: 14
		SendMailResponse SendMail(SendMailRequest sendMailRequest);

		// Token: 0x0600000F RID: 15
		SettingsResponse Settings(SettingsRequest settingsRequest);

		// Token: 0x06000010 RID: 16
		SyncResponse Sync(SyncRequest syncRequest);
	}
}
