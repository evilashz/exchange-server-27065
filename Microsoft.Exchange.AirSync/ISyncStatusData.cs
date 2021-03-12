using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000CF RID: 207
	internal interface ISyncStatusData
	{
		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06000C0D RID: 3085
		// (set) Token: 0x06000C0E RID: 3086
		string LastSyncRequestRandomString { get; set; }

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06000C0F RID: 3087
		// (set) Token: 0x06000C10 RID: 3088
		byte[] LastCachableWbxmlDocument { get; set; }

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06000C11 RID: 3089
		// (set) Token: 0x06000C12 RID: 3090
		ExDateTime? LastSyncAttemptTime { get; set; }

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06000C13 RID: 3091
		// (set) Token: 0x06000C14 RID: 3092
		ExDateTime? LastSyncSuccessTime { get; set; }

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06000C15 RID: 3093
		// (set) Token: 0x06000C16 RID: 3094
		string LastSyncUserAgent { get; set; }

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06000C17 RID: 3095
		// (set) Token: 0x06000C18 RID: 3096
		bool ClientCanSendUpEmptyRequests { get; set; }

		// Token: 0x06000C19 RID: 3097
		void AddClientId(string clientId);

		// Token: 0x06000C1A RID: 3098
		bool ContainsClientId(string clientId);

		// Token: 0x06000C1B RID: 3099
		bool ContainsClientCategoryHash(int hashName);

		// Token: 0x06000C1C RID: 3100
		void AddClientCategoryHash(int hashName);

		// Token: 0x06000C1D RID: 3101
		void ClearClientCategoryHash();

		// Token: 0x06000C1E RID: 3102
		void SaveAndDispose();
	}
}
