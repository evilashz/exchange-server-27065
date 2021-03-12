using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.Protocols.DeltaSync.HMFolder;
using Microsoft.Exchange.Net.Protocols.DeltaSync.ItemOperationsResponse;
using Microsoft.Exchange.Net.Protocols.DeltaSync.SendResponse;
using Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse;
using Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessResponse;
using Microsoft.Exchange.Net.Protocols.DeltaSync.SyncResponse;
using Microsoft.Exchange.Transport.Sync.Common;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync
{
	// Token: 0x02000075 RID: 117
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class DeltaSyncResultData
	{
		// Token: 0x0600053B RID: 1339 RVA: 0x00018AE8 File Offset: 0x00016CE8
		internal DeltaSyncResultData(Sync syncResponse) : this(syncResponse.Status, syncResponse.Fault.Faultcode, syncResponse.Fault.Faultstring, syncResponse.Fault.Detail)
		{
			this.syncResponse = syncResponse;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00018B1E File Offset: 0x00016D1E
		internal DeltaSyncResultData(Send sendResponse) : this(sendResponse.Status, sendResponse.Fault.Faultcode, sendResponse.Fault.Faultstring, sendResponse.Fault.Detail)
		{
			this.sendResponse = sendResponse;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00018B54 File Offset: 0x00016D54
		internal DeltaSyncResultData(Settings settingsResponse) : this(settingsResponse.Status, settingsResponse.Fault.Faultcode, settingsResponse.Fault.Faultstring, settingsResponse.Fault.Detail)
		{
			this.settingsResponse = settingsResponse;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00018B8A File Offset: 0x00016D8A
		internal DeltaSyncResultData(ItemOperations itemOperationsResponse) : this(itemOperationsResponse.Status, itemOperationsResponse.Fault.Faultcode, itemOperationsResponse.Fault.Faultstring, itemOperationsResponse.Fault.Detail)
		{
			this.itemOperationsResponse = itemOperationsResponse;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00018BC0 File Offset: 0x00016DC0
		internal DeltaSyncResultData(Stateless statelessResponse) : this(statelessResponse.Status, (statelessResponse.Fault != null) ? statelessResponse.Fault.Faultcode : string.Empty, (statelessResponse.Fault != null) ? statelessResponse.Fault.Faultstring : string.Empty, (statelessResponse.Fault != null) ? statelessResponse.Fault.Detail : string.Empty)
		{
			this.stateLessResponse = statelessResponse;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00018C2E File Offset: 0x00016E2E
		private DeltaSyncResultData(int topLevelStatusCode, string faultCode, string faultString, string faultDetail)
		{
			this.topLevelStatusCode = topLevelStatusCode;
			this.faultCode = faultCode;
			this.faultString = faultString;
			this.faultDetail = faultDetail;
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x00018C53 File Offset: 0x00016E53
		internal bool IsTopLevelOperationSuccessful
		{
			get
			{
				return this.topLevelStatusCode == 1;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x00018C5E File Offset: 0x00016E5E
		internal bool IsAuthenticationError
		{
			get
			{
				return this.topLevelStatusCode == 3204;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x00018C6D File Offset: 0x00016E6D
		internal int TopLevelStatusCode
		{
			get
			{
				return this.topLevelStatusCode;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x00018C75 File Offset: 0x00016E75
		internal Sync SyncResponse
		{
			get
			{
				return this.syncResponse;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x00018C7D File Offset: 0x00016E7D
		internal Send SendResponse
		{
			get
			{
				return this.sendResponse;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x00018C85 File Offset: 0x00016E85
		internal Settings SettingsResponse
		{
			get
			{
				return this.settingsResponse;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x00018C8D File Offset: 0x00016E8D
		internal ItemOperations ItemOperationsResponse
		{
			get
			{
				return this.itemOperationsResponse;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x00018C95 File Offset: 0x00016E95
		internal Stateless StatelessResponse
		{
			get
			{
				return this.stateLessResponse;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x00018C9D File Offset: 0x00016E9D
		internal string FaultCode
		{
			get
			{
				return this.faultCode;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x00018CA5 File Offset: 0x00016EA5
		internal string FaultString
		{
			get
			{
				return this.faultString;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x00018CAD File Offset: 0x00016EAD
		internal string FaultDetail
		{
			get
			{
				return this.faultDetail;
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00018CB8 File Offset: 0x00016EB8
		internal static bool TryGetFolderEmailCollections(Sync syncResponse, out Collection folderCollection, out Collection emailCollection, out Exception exception)
		{
			folderCollection = null;
			emailCollection = null;
			exception = null;
			foreach (object obj in syncResponse.Collections.CollectionCollection)
			{
				Collection collection = (Collection)obj;
				if (collection.Class != null)
				{
					if (collection.Class.Equals(DeltaSyncCommon.FolderCollectionName, StringComparison.OrdinalIgnoreCase))
					{
						if (collection.internalStatusSpecified)
						{
							exception = DeltaSyncResultData.GetStatusException(collection.Status);
							folderCollection = collection;
						}
					}
					else if (collection.Class.Equals(DeltaSyncCommon.EmailCollectionName, StringComparison.OrdinalIgnoreCase) && collection.internalStatusSpecified)
					{
						exception = DeltaSyncResultData.GetStatusException(collection.Status);
						emailCollection = collection;
					}
					if (exception != null)
					{
						return false;
					}
				}
			}
			if (folderCollection != null && folderCollection.SyncKey != null && emailCollection != null && emailCollection.SyncKey != null)
			{
				return true;
			}
			exception = new InvalidServerResponseException();
			return false;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00018DA8 File Offset: 0x00016FA8
		internal static bool TryGetSettings(Settings settingsResponse, out DeltaSyncSettings deltaSyncSettings, out Exception exception)
		{
			deltaSyncSettings = null;
			exception = null;
			int status;
			if (settingsResponse.ServiceSettings.Properties.Status == 1)
			{
				if (settingsResponse.AccountSettings.Status == 1)
				{
					deltaSyncSettings = new DeltaSyncSettings(settingsResponse.ServiceSettings.Properties.Get, settingsResponse.AccountSettings.Get.Properties);
					return true;
				}
				status = settingsResponse.AccountSettings.Status;
			}
			else
			{
				status = settingsResponse.ServiceSettings.Properties.Status;
			}
			exception = DeltaSyncResultData.GetStatusException(status);
			return false;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00018E30 File Offset: 0x00017030
		internal static bool TryGetMessageStream(ItemOperations fetchResponse, out Stream messageStream, out Exception exception)
		{
			messageStream = null;
			exception = null;
			if (fetchResponse.Responses.Fetch.internalStatusSpecified)
			{
				if (fetchResponse.Responses.Fetch.Status != 1)
				{
					exception = DeltaSyncResultData.GetStatusException(fetchResponse.Responses.Fetch.Status);
					return false;
				}
				messageStream = fetchResponse.Responses.Fetch.Message.EmailMessage;
			}
			if (messageStream != null)
			{
				return true;
			}
			exception = new InvalidServerResponseException();
			return false;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00018EA8 File Offset: 0x000170A8
		internal static Exception GetStatusException(int statusCode)
		{
			if (statusCode == 1)
			{
				return null;
			}
			if (DeltaSyncResultData.ArgumentInRange(statusCode, 3100, 3199))
			{
				return new PartnerAuthenticationException(statusCode);
			}
			if (DeltaSyncResultData.ArgumentInRange(statusCode, 3200, 3299))
			{
				return new UserAccessException(statusCode);
			}
			if (DeltaSyncResultData.ArgumentInRange(statusCode, 4100, 4199))
			{
				return new RequestFormatException(statusCode);
			}
			if (DeltaSyncResultData.ArgumentInRange(statusCode, 4200, 4299))
			{
				return new RequestContentException(statusCode);
			}
			if (DeltaSyncResultData.ArgumentInRange(statusCode, 4300, 4399))
			{
				return new SettingsViolationException(statusCode);
			}
			if (DeltaSyncResultData.ArgumentInRange(statusCode, 4400, 4499))
			{
				return new DataOutOfSyncException(statusCode);
			}
			if (DeltaSyncResultData.ArgumentInRange(statusCode, 5000, 5999))
			{
				return new DeltaSyncServerException(statusCode);
			}
			return new UnknownDeltaSyncException(statusCode);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00018F70 File Offset: 0x00017170
		internal static string DecodeValue(Microsoft.Exchange.Net.Protocols.DeltaSync.HMFolder.DisplayName displayName)
		{
			if (displayName.encoding != null && displayName.encoding.Equals("2"))
			{
				byte[] bytes = Convert.FromBase64String(displayName.Value);
				return Encoding.UTF8.GetString(bytes);
			}
			return displayName.Value;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00018FB5 File Offset: 0x000171B5
		internal Exception GetStatusException()
		{
			return DeltaSyncResultData.GetStatusException(this.topLevelStatusCode);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00018FC2 File Offset: 0x000171C2
		private static bool ArgumentInRange(int arg, int lowerLimit, int upperLimit)
		{
			return arg >= lowerLimit && arg <= upperLimit;
		}

		// Token: 0x040002DC RID: 732
		private readonly int topLevelStatusCode;

		// Token: 0x040002DD RID: 733
		private readonly Sync syncResponse;

		// Token: 0x040002DE RID: 734
		private readonly Send sendResponse;

		// Token: 0x040002DF RID: 735
		private readonly Settings settingsResponse;

		// Token: 0x040002E0 RID: 736
		private readonly ItemOperations itemOperationsResponse;

		// Token: 0x040002E1 RID: 737
		private readonly Stateless stateLessResponse;

		// Token: 0x040002E2 RID: 738
		private readonly string faultCode;

		// Token: 0x040002E3 RID: 739
		private readonly string faultString;

		// Token: 0x040002E4 RID: 740
		private readonly string faultDetail;
	}
}
