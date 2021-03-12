using System;
using System.Net;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Connections.Eas.Commands;
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
	// Token: 0x02000003 RID: 3
	[ClassAccessLevel(AccessLevel.Implementation)]
	public sealed class EasConnection : IEasConnection, IConnection<IEasConnection>
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000020D0 File Offset: 0x000002D0
		private EasConnection(EasConnectionParameters connectionParameters, EasAuthenticationParameters authenticationParameters, EasDeviceParameters deviceParameters)
		{
			this.ConnectionParameters = connectionParameters;
			this.AuthenticationParameters = authenticationParameters;
			this.DeviceParameters = deviceParameters;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000020ED File Offset: 0x000002ED
		public string ServerName
		{
			get
			{
				return this.EasEndpointSettings.Domain;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000020FA File Offset: 0x000002FA
		public UserSmtpAddress UserSmtpAddress
		{
			get
			{
				return this.AuthenticationParameters.UserSmtpAddress;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002107 File Offset: 0x00000307
		// (set) Token: 0x06000015 RID: 21 RVA: 0x0000210F File Offset: 0x0000030F
		internal EasEndpointSettings EasEndpointSettings { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002118 File Offset: 0x00000318
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002120 File Offset: 0x00000320
		private EasConnectionParameters ConnectionParameters { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002129 File Offset: 0x00000329
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002131 File Offset: 0x00000331
		private EasDeviceParameters DeviceParameters { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000213A File Offset: 0x0000033A
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002142 File Offset: 0x00000342
		private EasAuthenticationParameters AuthenticationParameters { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000214B File Offset: 0x0000034B
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002153 File Offset: 0x00000353
		private EasConnectionSettings EasConnectionSettings { get; set; }

		// Token: 0x0600001E RID: 30 RVA: 0x0000215C File Offset: 0x0000035C
		public static IEasConnection CreateInstance(EasConnectionParameters connectionParameters, EasAuthenticationParameters authenticationParameters, EasDeviceParameters deviceParameters)
		{
			return EasConnection.hookableFactory.Value(connectionParameters, authenticationParameters, deviceParameters);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002170 File Offset: 0x00000370
		public IEasConnection Initialize()
		{
			this.EasEndpointSettings = new EasEndpointSettings(this.AuthenticationParameters);
			this.EasConnectionSettings = new EasConnectionSettings(this.EasEndpointSettings, this.ConnectionParameters, this.AuthenticationParameters, this.DeviceParameters);
			return this;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000021A8 File Offset: 0x000003A8
		ConnectResponse IEasConnection.Connect(ConnectRequest connectRequest, IServerCapabilities capabilities)
		{
			ConnectCommand connectCommand = new ConnectCommand(this.EasConnectionSettings);
			return connectCommand.Execute(connectRequest, capabilities ?? EasConnection.DefaultCapabilities);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000021D4 File Offset: 0x000003D4
		OperationStatusCode IEasConnection.TestLogon()
		{
			ConnectStatus connectStatus;
			HttpStatus httpStatus;
			try
			{
				ConnectResponse connectResponse = ((IEasConnection)this).Connect(ConnectRequest.Default, null);
				connectStatus = connectResponse.ConnectStatus;
				httpStatus = connectResponse.HttpStatus;
			}
			catch (WebException ex)
			{
				HttpWebResponse httpWebResponse = ex.Response as HttpWebResponse;
				connectStatus = ConnectStatus.IsPermanent;
				httpStatus = (HttpStatus)httpWebResponse.StatusCode;
			}
			((IEasConnection)this).Disconnect(DisconnectRequest.Default);
			if (connectStatus == ConnectStatus.Success)
			{
				return OperationStatusCode.Success;
			}
			if (connectStatus == ConnectStatus.AutodiscoverFailed)
			{
				return OperationStatusCode.ErrorInvalidRemoteServer;
			}
			HttpStatus httpStatus2 = httpStatus;
			switch (httpStatus2)
			{
			case HttpStatus.Unauthorized:
			case HttpStatus.Forbidden:
			case HttpStatus.NotFound:
				break;
			case (HttpStatus)402:
				return OperationStatusCode.ErrorCannotCommunicateWithRemoteServer;
			default:
				if (httpStatus2 != HttpStatus.NeedProvisioning)
				{
					switch (httpStatus2)
					{
					case HttpStatus.ProxyError:
					case HttpStatus.ServiceUnavailable:
						return OperationStatusCode.ErrorInvalidRemoteServer;
					case (HttpStatus)504:
						return OperationStatusCode.ErrorCannotCommunicateWithRemoteServer;
					case HttpStatus.VersionNotSupported:
						return OperationStatusCode.ErrorUnsupportedProtocolVersion;
					default:
						return OperationStatusCode.ErrorCannotCommunicateWithRemoteServer;
					}
				}
				break;
			}
			return OperationStatusCode.ErrorInvalidCredentials;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002290 File Offset: 0x00000490
		DisconnectResponse IEasConnection.Disconnect(DisconnectRequest disconnectRequest)
		{
			DisconnectCommand disconnectCommand = new DisconnectCommand(this.EasConnectionSettings);
			return disconnectCommand.Execute(disconnectRequest);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000022B4 File Offset: 0x000004B4
		FolderCreateResponse IEasConnection.FolderCreate(FolderCreateRequest folderCreateRequest)
		{
			FolderCreateCommand folderCreateCommand = new FolderCreateCommand(this.EasConnectionSettings);
			return folderCreateCommand.Execute(folderCreateRequest);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000022D8 File Offset: 0x000004D8
		FolderDeleteResponse IEasConnection.FolderDelete(FolderDeleteRequest folderDeleteRequest)
		{
			FolderDeleteCommand folderDeleteCommand = new FolderDeleteCommand(this.EasConnectionSettings);
			return folderDeleteCommand.Execute(folderDeleteRequest);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000022FC File Offset: 0x000004FC
		FolderSyncResponse IEasConnection.FolderSync(FolderSyncRequest folderSyncRequest)
		{
			FolderSyncCommand folderSyncCommand = new FolderSyncCommand(this.EasConnectionSettings);
			return folderSyncCommand.Execute(folderSyncRequest);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002320 File Offset: 0x00000520
		FolderUpdateResponse IEasConnection.FolderUpdate(FolderUpdateRequest folderUpdateRequest)
		{
			FolderUpdateCommand folderUpdateCommand = new FolderUpdateCommand(this.EasConnectionSettings);
			return folderUpdateCommand.Execute(folderUpdateRequest);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002344 File Offset: 0x00000544
		GetItemEstimateResponse IEasConnection.GetItemEstimate(GetItemEstimateRequest getItemEstimateRequest)
		{
			GetItemEstimateCommand getItemEstimateCommand = new GetItemEstimateCommand(this.EasConnectionSettings);
			return getItemEstimateCommand.Execute(getItemEstimateRequest);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002368 File Offset: 0x00000568
		ItemOperationsResponse IEasConnection.ItemOperations(ItemOperationsRequest itemOperationsRequest)
		{
			ItemOperationsCommand itemOperationsCommand = new ItemOperationsCommand(this.EasConnectionSettings);
			return itemOperationsCommand.Execute(itemOperationsRequest);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000238C File Offset: 0x0000058C
		MoveItemsResponse IEasConnection.MoveItems(MoveItemsRequest moveItemsRequest)
		{
			MoveItemsCommand moveItemsCommand = new MoveItemsCommand(this.EasConnectionSettings);
			return moveItemsCommand.Execute(moveItemsRequest);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000023B0 File Offset: 0x000005B0
		OptionsResponse IEasConnection.Options(OptionsRequest optionsRequest)
		{
			OptionsCommand optionsCommand = new OptionsCommand(this.EasConnectionSettings);
			return optionsCommand.Execute(optionsRequest);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000023D4 File Offset: 0x000005D4
		SendMailResponse IEasConnection.SendMail(SendMailRequest sendMailRequest)
		{
			SendMailCommand sendMailCommand = new SendMailCommand(this.EasConnectionSettings);
			return sendMailCommand.Execute(sendMailRequest);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000023F8 File Offset: 0x000005F8
		SettingsResponse IEasConnection.Settings(SettingsRequest settingsRequest)
		{
			SettingsCommand settingsCommand = new SettingsCommand(this.EasConnectionSettings);
			return settingsCommand.Execute(settingsRequest);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000241C File Offset: 0x0000061C
		SyncResponse IEasConnection.Sync(SyncRequest syncRequest)
		{
			SyncCommand syncCommand = new SyncCommand(this.EasConnectionSettings);
			return syncCommand.Execute(syncRequest);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000243E File Offset: 0x0000063E
		internal static IDisposable SetTestHook(Func<EasConnectionParameters, EasAuthenticationParameters, EasDeviceParameters, IEasConnection> newFactory)
		{
			return EasConnection.hookableFactory.SetTestHook(newFactory);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000244B File Offset: 0x0000064B
		private static IEasConnection Factory(EasConnectionParameters connectionParameters, EasAuthenticationParameters authenticationParameters, EasDeviceParameters deviceParameters)
		{
			return new EasConnection(connectionParameters, authenticationParameters, deviceParameters).Initialize();
		}

		// Token: 0x04000001 RID: 1
		private static readonly EasServerCapabilities DefaultCapabilities = new EasServerCapabilities(new string[]
		{
			"FolderCreate",
			"FolderDelete",
			"FolderSync",
			"FolderUpdate",
			"GetItemEstimate",
			"ItemOperations",
			"MoveItems",
			"SendMail",
			"Settings",
			"Sync"
		});

		// Token: 0x04000002 RID: 2
		private static Hookable<Func<EasConnectionParameters, EasAuthenticationParameters, EasDeviceParameters, IEasConnection>> hookableFactory = Hookable<Func<EasConnectionParameters, EasAuthenticationParameters, EasDeviceParameters, IEasConnection>>.Create(true, new Func<EasConnectionParameters, EasAuthenticationParameters, EasDeviceParameters, IEasConnection>(EasConnection.Factory));
	}
}
