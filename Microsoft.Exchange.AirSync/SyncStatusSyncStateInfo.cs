using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000282 RID: 642
	internal class SyncStatusSyncStateInfo : CustomSyncStateInfo
	{
		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x060017A8 RID: 6056 RVA: 0x0008C6AE File Offset: 0x0008A8AE
		// (set) Token: 0x060017A9 RID: 6057 RVA: 0x0008C6B5 File Offset: 0x0008A8B5
		public override string UniqueName
		{
			get
			{
				return "SyncStatus";
			}
			set
			{
				throw new InvalidOperationException("SyncStatusSyncStateInfo.UniqueName is not settable.");
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x060017AA RID: 6058 RVA: 0x0008C6C1 File Offset: 0x0008A8C1
		public override int Version
		{
			get
			{
				return 26;
			}
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x0008C6C8 File Offset: 0x0008A8C8
		public override void HandleSyncStateVersioning(SyncState syncState)
		{
			if (syncState == null)
			{
				throw new ArgumentNullException("syncState");
			}
			if (syncState.BackendVersion == null)
			{
				return;
			}
			bool flag = true;
			if (syncState.BackendVersion < 3 || syncState.BackendVersion > this.Version)
			{
				flag = false;
			}
			else if (syncState.BackendVersion.Value != this.Version)
			{
				int value = syncState.BackendVersion.Value;
				switch (value)
				{
				case 3:
					syncState["ClientCanSendUpEmptyRequests"] = new BooleanData(false);
					syncState["LastSyncRequestRandomString"] = new StringData(string.Empty);
					break;
				case 4:
					break;
				case 5:
					goto IL_114;
				default:
					switch (value)
					{
					case 20:
						goto IL_124;
					case 21:
					case 22:
						goto IL_150;
					case 23:
						goto IL_166;
					case 24:
						goto IL_172;
					case 25:
						goto IL_17D;
					default:
						flag = false;
						goto IL_18C;
					}
					break;
				}
				syncState["ClientCanSendUpEmptyRequests"] = new BooleanData(false);
				syncState.Remove("IsXmlValidBool");
				IL_114:
				syncState["LastClientIdsSent"] = new GenericListData<StringData, string>();
				IL_124:
				syncState["LastCachableWbxmlDocument"] = new ByteArrayData();
				syncState["ClientCanSendUpEmptyRequests"] = new BooleanData(false);
				syncState.Remove("XmlDocumentString");
				IL_150:
				syncState["LastAdUpdateTime"] = syncState.GetData<DateTimeData>("LastAdUpdateTime");
				IL_166:
				syncState["ClientCategoryList"] = null;
				IL_172:
				syncState.Remove("LastAdUpdateTime");
				IL_17D:
				syncState.Remove("MailboxLog");
			}
			IL_18C:
			if (!flag)
			{
				syncState.HandleCorruptSyncState();
			}
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x0008C86C File Offset: 0x0008AA6C
		internal static bool IsPreE14SyncState(SyncState syncState)
		{
			return syncState != null && syncState.BackendVersion != null && syncState.BackendVersion.Value < 20;
		}

		// Token: 0x04000E83 RID: 3715
		internal const string UniqueNameString = "SyncStatus";

		// Token: 0x04000E84 RID: 3716
		internal const int E14BaseVersion = 20;

		// Token: 0x02000283 RID: 643
		internal struct PropertyNames
		{
			// Token: 0x04000E85 RID: 3717
			internal const string ClientCanSendUpEmptyRequests = "ClientCanSendUpEmptyRequests";

			// Token: 0x04000E86 RID: 3718
			internal const string LastSyncAttemptTime = "LastSyncAttemptTime";

			// Token: 0x04000E87 RID: 3719
			internal const string LastSyncRequestRandomString = "LastSyncRequestRandomString";

			// Token: 0x04000E88 RID: 3720
			internal const string LastSyncSuccessTime = "LastSyncSuccessTime";

			// Token: 0x04000E89 RID: 3721
			internal const string UserAgent = "UserAgent";

			// Token: 0x04000E8A RID: 3722
			internal const string LastCachableWbxmlDocument = "LastCachableWbxmlDocument";

			// Token: 0x04000E8B RID: 3723
			internal const string LastClientIdsSent = "LastClientIdsSent";

			// Token: 0x04000E8C RID: 3724
			internal const string LastAdUpdateTime = "LastAdUpdateTime";

			// Token: 0x04000E8D RID: 3725
			internal const string ClientCategoryList = "ClientCategoryList";
		}
	}
}
