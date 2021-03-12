using System;

namespace Microsoft.Exchange.Rpc.ExchangeCertificate
{
	// Token: 0x02000247 RID: 583
	internal enum RpcParameters
	{
		// Token: 0x04000C83 RID: 3203
		GetByThumbprint,
		// Token: 0x04000C84 RID: 3204
		GetByCertificate,
		// Token: 0x04000C85 RID: 3205
		GetByDomains,
		// Token: 0x04000C86 RID: 3206
		CreateExportable = 100,
		// Token: 0x04000C87 RID: 3207
		CreateSubjectName,
		// Token: 0x04000C88 RID: 3208
		CreateFriendlyName,
		// Token: 0x04000C89 RID: 3209
		CreateDomains,
		// Token: 0x04000C8A RID: 3210
		CreateIncAccepted,
		// Token: 0x04000C8B RID: 3211
		CreateIncAutoDisc,
		// Token: 0x04000C8C RID: 3212
		CreateIncFqdn,
		// Token: 0x04000C8D RID: 3213
		CreateIncNetBios,
		// Token: 0x04000C8E RID: 3214
		CreateKeySize,
		// Token: 0x04000C8F RID: 3215
		CreateCloneCert,
		// Token: 0x04000C90 RID: 3216
		CreateBinary,
		// Token: 0x04000C91 RID: 3217
		CreateRequest,
		// Token: 0x04000C92 RID: 3218
		CreateServices,
		// Token: 0x04000C93 RID: 3219
		CreateSubjectKeyIdentifier,
		// Token: 0x04000C94 RID: 3220
		CreateAllowConfirmation,
		// Token: 0x04000C95 RID: 3221
		CreateWhatIf,
		// Token: 0x04000C96 RID: 3222
		RequireSsl,
		// Token: 0x04000C97 RID: 3223
		RemoveByThumbprint = 200,
		// Token: 0x04000C98 RID: 3224
		ExportByThumbprint = 300,
		// Token: 0x04000C99 RID: 3225
		ExportBinary,
		// Token: 0x04000C9A RID: 3226
		ImportCert = 401,
		// Token: 0x04000C9B RID: 3227
		ImportDescription,
		// Token: 0x04000C9C RID: 3228
		ImportExportable,
		// Token: 0x04000C9D RID: 3229
		EnableByThumbprint = 500,
		// Token: 0x04000C9E RID: 3230
		EnableServices,
		// Token: 0x04000C9F RID: 3231
		EnableAllowConfirmation,
		// Token: 0x04000CA0 RID: 3232
		EnableUpdateAD,
		// Token: 0x04000CA1 RID: 3233
		EnableNetworkService
	}
}
