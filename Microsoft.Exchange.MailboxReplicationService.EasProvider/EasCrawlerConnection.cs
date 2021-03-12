using System;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Connections.Eas;
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

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000003 RID: 3
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class EasCrawlerConnection : IEasConnection, IConnection<IEasConnection>
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002D7C File Offset: 0x00000F7C
		internal EasCrawlerConnection(EasConnectionParameters connectionParameters, EasAuthenticationParameters authenticationParameters, EasDeviceParameters deviceParameters)
		{
			EasDeviceParameters deviceParameters2 = new EasDeviceParameters("FEDCBA9876543210", deviceParameters);
			this.innerConnection = EasConnection.CreateInstance(connectionParameters, authenticationParameters, deviceParameters2);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002DA9 File Offset: 0x00000FA9
		string IEasConnection.ServerName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002DB0 File Offset: 0x00000FB0
		UserSmtpAddress IEasConnection.UserSmtpAddress
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002DB7 File Offset: 0x00000FB7
		IEasConnection IConnection<IEasConnection>.Initialize()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002DBE File Offset: 0x00000FBE
		ConnectResponse IEasConnection.Connect(ConnectRequest connectRequest, IServerCapabilities capabilities)
		{
			return this.innerConnection.Connect(connectRequest, null);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002DCD File Offset: 0x00000FCD
		DisconnectResponse IEasConnection.Disconnect(DisconnectRequest disconnectRequest)
		{
			return this.innerConnection.Disconnect(disconnectRequest);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002DDB File Offset: 0x00000FDB
		FolderCreateResponse IEasConnection.FolderCreate(FolderCreateRequest folderCreateRequest)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002DE2 File Offset: 0x00000FE2
		FolderDeleteResponse IEasConnection.FolderDelete(FolderDeleteRequest folderDeleteRequest)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002DE9 File Offset: 0x00000FE9
		FolderSyncResponse IEasConnection.FolderSync(FolderSyncRequest folderSyncRequest)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002DF0 File Offset: 0x00000FF0
		FolderUpdateResponse IEasConnection.FolderUpdate(FolderUpdateRequest folderUpdateRequest)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002DF7 File Offset: 0x00000FF7
		GetItemEstimateResponse IEasConnection.GetItemEstimate(GetItemEstimateRequest getItemEstimateRequest)
		{
			return this.innerConnection.GetItemEstimate(getItemEstimateRequest);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002E05 File Offset: 0x00001005
		ItemOperationsResponse IEasConnection.ItemOperations(ItemOperationsRequest itemOperationsRequest)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002E0C File Offset: 0x0000100C
		MoveItemsResponse IEasConnection.MoveItems(MoveItemsRequest moveItemsRequest)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002E13 File Offset: 0x00001013
		OptionsResponse IEasConnection.Options(OptionsRequest optionsRequest)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002E1A File Offset: 0x0000101A
		SendMailResponse IEasConnection.SendMail(SendMailRequest sendMailRequest)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002E21 File Offset: 0x00001021
		SettingsResponse IEasConnection.Settings(SettingsRequest settingsRequest)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002E28 File Offset: 0x00001028
		SyncResponse IEasConnection.Sync(SyncRequest syncRequest)
		{
			SyncResponse result;
			try
			{
				result = this.innerConnection.Sync(syncRequest);
			}
			catch (EasRequiresFolderSyncException)
			{
				this.innerConnection.FolderSync(FolderSyncRequest.InitialSyncRequest);
				result = this.innerConnection.Sync(syncRequest);
			}
			return result;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002E78 File Offset: 0x00001078
		OperationStatusCode IEasConnection.TestLogon()
		{
			throw new NotImplementedException("This kind of connection does not support TestLogon functionality. Please use EasConnection if you need this functionality.");
		}

		// Token: 0x0400001A RID: 26
		private const string CrawlerDeviceId = "FEDCBA9876543210";

		// Token: 0x0400001B RID: 27
		private readonly IEasConnection innerConnection;
	}
}
