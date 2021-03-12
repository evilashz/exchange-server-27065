using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000A6 RID: 166
	internal struct FolderHierarchySyncStateCustomDataInfo
	{
		// Token: 0x06000934 RID: 2356 RVA: 0x00036668 File Offset: 0x00034868
		internal static void HandlerCustomDataVersioning(FolderHierarchySyncState syncState)
		{
			if (syncState == null)
			{
				throw new ArgumentNullException("syncState");
			}
			if (syncState.CustomVersion == null)
			{
				return;
			}
			bool flag = true;
			if (syncState.CustomVersion < 2 || syncState.CustomVersion > 5)
			{
				flag = false;
			}
			else if (syncState.CustomVersion.Value != 5)
			{
				int valueOrDefault = syncState.CustomVersion.GetValueOrDefault();
				int? num;
				if (num != null)
				{
					switch (valueOrDefault)
					{
					case 2:
						syncState["RecoverySyncKey"] = new Int32Data(0);
						break;
					case 3:
						break;
					case 4:
						goto IL_DD;
					default:
						goto IL_11E;
					}
					syncState[CustomStateDatumType.AirSyncProtocolVersion] = new ConstStringData(StaticStringPool.Instance.Intern("2.0"));
					IL_DD:
					object obj = syncState[CustomStateDatumType.AirSyncProtocolVersion];
					if (obj is ConstStringData)
					{
						string data = syncState.GetData<ConstStringData, string>(CustomStateDatumType.AirSyncProtocolVersion, null);
						int data2 = 20;
						if (data != null)
						{
							data2 = AirSyncUtility.ParseVersionString(data);
						}
						syncState[CustomStateDatumType.AirSyncProtocolVersion] = new Int32Data(data2);
						goto IL_120;
					}
					goto IL_120;
				}
				IL_11E:
				flag = false;
			}
			IL_120:
			if (!flag)
			{
				syncState.HandleCorruptSyncState();
			}
		}

		// Token: 0x040005D1 RID: 1489
		internal const string AirSyncProtocolVersion = "AirSyncProtocolVersion";

		// Token: 0x040005D2 RID: 1490
		internal const string RecoverySyncKey = "RecoverySyncKey";

		// Token: 0x040005D3 RID: 1491
		internal const string SyncKey = "SyncKey";

		// Token: 0x040005D4 RID: 1492
		internal const int E12BaseVersion = 2;

		// Token: 0x040005D5 RID: 1493
		internal const int Version = 5;
	}
}
