using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000027 RID: 39
	[Flags]
	public enum FailoverFlags
	{
		// Token: 0x040000C0 RID: 192
		None = 0,
		// Token: 0x040000C1 RID: 193
		HRDRequest = 1,
		// Token: 0x040000C2 RID: 194
		HRDResponse = 2,
		// Token: 0x040000C3 RID: 195
		LiveIdRequest = 4,
		// Token: 0x040000C4 RID: 196
		LiveIdResponse = 8,
		// Token: 0x040000C5 RID: 197
		OrgIdRequest = 16,
		// Token: 0x040000C6 RID: 198
		OrgIdResponse = 32,
		// Token: 0x040000C7 RID: 199
		OfflineHRD = 64,
		// Token: 0x040000C8 RID: 200
		OfflineAuthentication = 128,
		// Token: 0x040000C9 RID: 201
		HRDRequestTimeout = 256,
		// Token: 0x040000CA RID: 202
		LiveIdRequestTimeout = 512,
		// Token: 0x040000CB RID: 203
		LowPasswordConfidence = 1024,
		// Token: 0x040000CC RID: 204
		OrgIdRequestTimeout = 2048,
		// Token: 0x040000CD RID: 205
		Random = 4196
	}
}
