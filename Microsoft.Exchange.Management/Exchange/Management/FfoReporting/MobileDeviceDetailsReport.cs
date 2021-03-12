using System;
using System.Data.Services.Common;
using Microsoft.Exchange.Management.FfoReporting.Common;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x0200124B RID: 4683
	[DataServiceKey("DeviceId")]
	[Serializable]
	public sealed class MobileDeviceDetailsReport : FfoReportObject
	{
		// Token: 0x17003B82 RID: 15234
		// (get) Token: 0x0600BC64 RID: 48228 RVA: 0x002ACFDA File Offset: 0x002AB1DA
		// (set) Token: 0x0600BC65 RID: 48229 RVA: 0x002ACFE2 File Offset: 0x002AB1E2
		[DalConversion("DefaultSerializer", "DeviceId", new string[]
		{

		})]
		[Redact]
		public Guid DeviceId { get; set; }

		// Token: 0x17003B83 RID: 15235
		// (get) Token: 0x0600BC66 RID: 48230 RVA: 0x002ACFEB File Offset: 0x002AB1EB
		// (set) Token: 0x0600BC67 RID: 48231 RVA: 0x002ACFF3 File Offset: 0x002AB1F3
		[Redact]
		[DalConversion("DefaultSerializer", "EASId", new string[]
		{

		})]
		public string EASId { get; set; }

		// Token: 0x17003B84 RID: 15236
		// (get) Token: 0x0600BC68 RID: 48232 RVA: 0x002ACFFC File Offset: 0x002AB1FC
		// (set) Token: 0x0600BC69 RID: 48233 RVA: 0x002AD004 File Offset: 0x002AB204
		[DalConversion("DefaultSerializer", "IntuneId", new string[]
		{

		})]
		[Redact]
		public Guid IntuneId { get; set; }

		// Token: 0x17003B85 RID: 15237
		// (get) Token: 0x0600BC6A RID: 48234 RVA: 0x002AD00D File Offset: 0x002AB20D
		// (set) Token: 0x0600BC6B RID: 48235 RVA: 0x002AD015 File Offset: 0x002AB215
		[Redact]
		[DalConversion("DefaultSerializer", "User", new string[]
		{

		})]
		public string User { get; set; }

		// Token: 0x17003B86 RID: 15238
		// (get) Token: 0x0600BC6C RID: 48236 RVA: 0x002AD01E File Offset: 0x002AB21E
		// (set) Token: 0x0600BC6D RID: 48237 RVA: 0x002AD026 File Offset: 0x002AB226
		[Redact]
		[DalConversion("DefaultSerializer", "DeviceName", new string[]
		{

		})]
		public string DeviceName { get; set; }

		// Token: 0x17003B87 RID: 15239
		// (get) Token: 0x0600BC6E RID: 48238 RVA: 0x002AD02F File Offset: 0x002AB22F
		// (set) Token: 0x0600BC6F RID: 48239 RVA: 0x002AD037 File Offset: 0x002AB237
		[DalConversion("DefaultSerializer", "DeviceModel", new string[]
		{

		})]
		public string DeviceModel { get; set; }

		// Token: 0x17003B88 RID: 15240
		// (get) Token: 0x0600BC70 RID: 48240 RVA: 0x002AD040 File Offset: 0x002AB240
		// (set) Token: 0x0600BC71 RID: 48241 RVA: 0x002AD048 File Offset: 0x002AB248
		[DalConversion("DefaultSerializer", "DeviceType", new string[]
		{

		})]
		public string DeviceType { get; set; }

		// Token: 0x17003B89 RID: 15241
		// (get) Token: 0x0600BC72 RID: 48242 RVA: 0x002AD051 File Offset: 0x002AB251
		// (set) Token: 0x0600BC73 RID: 48243 RVA: 0x002AD059 File Offset: 0x002AB259
		[DalConversion("DefaultSerializer", "FirstSyncTime", new string[]
		{

		})]
		public DateTime FirstSyncTime { get; set; }

		// Token: 0x17003B8A RID: 15242
		// (get) Token: 0x0600BC74 RID: 48244 RVA: 0x002AD062 File Offset: 0x002AB262
		// (set) Token: 0x0600BC75 RID: 48245 RVA: 0x002AD06A File Offset: 0x002AB26A
		[DalConversion("DefaultSerializer", "LastSyncTime", new string[]
		{

		})]
		public DateTime LastSyncTime { get; set; }

		// Token: 0x17003B8B RID: 15243
		// (get) Token: 0x0600BC76 RID: 48246 RVA: 0x002AD073 File Offset: 0x002AB273
		// (set) Token: 0x0600BC77 RID: 48247 RVA: 0x002AD07B File Offset: 0x002AB27B
		[DalConversion("DefaultSerializer", "IMEI", new string[]
		{

		})]
		[Redact]
		public string IMEI { get; set; }

		// Token: 0x17003B8C RID: 15244
		// (get) Token: 0x0600BC78 RID: 48248 RVA: 0x002AD084 File Offset: 0x002AB284
		// (set) Token: 0x0600BC79 RID: 48249 RVA: 0x002AD08C File Offset: 0x002AB28C
		[Redact]
		[DalConversion("DefaultSerializer", "PhoneNumber", new string[]
		{

		})]
		public string PhoneNumber { get; set; }

		// Token: 0x17003B8D RID: 15245
		// (get) Token: 0x0600BC7A RID: 48250 RVA: 0x002AD095 File Offset: 0x002AB295
		// (set) Token: 0x0600BC7B RID: 48251 RVA: 0x002AD09D File Offset: 0x002AB29D
		[DalConversion("DefaultSerializer", "MobileNetwork", new string[]
		{

		})]
		[Redact]
		public string MobileNetwork { get; set; }

		// Token: 0x17003B8E RID: 15246
		// (get) Token: 0x0600BC7C RID: 48252 RVA: 0x002AD0A6 File Offset: 0x002AB2A6
		// (set) Token: 0x0600BC7D RID: 48253 RVA: 0x002AD0AE File Offset: 0x002AB2AE
		[DalConversion("DefaultSerializer", "EASVersion", new string[]
		{

		})]
		public string EASVersion { get; set; }

		// Token: 0x17003B8F RID: 15247
		// (get) Token: 0x0600BC7E RID: 48254 RVA: 0x002AD0B7 File Offset: 0x002AB2B7
		// (set) Token: 0x0600BC7F RID: 48255 RVA: 0x002AD0BF File Offset: 0x002AB2BF
		[DalConversion("DefaultSerializer", "UserAgent", new string[]
		{

		})]
		public string UserAgent { get; set; }

		// Token: 0x17003B90 RID: 15248
		// (get) Token: 0x0600BC80 RID: 48256 RVA: 0x002AD0C8 File Offset: 0x002AB2C8
		// (set) Token: 0x0600BC81 RID: 48257 RVA: 0x002AD0D0 File Offset: 0x002AB2D0
		[DalConversion("DefaultSerializer", "DeviceLanguage", new string[]
		{

		})]
		public string DeviceLanguage { get; set; }

		// Token: 0x17003B91 RID: 15249
		// (get) Token: 0x0600BC82 RID: 48258 RVA: 0x002AD0D9 File Offset: 0x002AB2D9
		// (set) Token: 0x0600BC83 RID: 48259 RVA: 0x002AD0E1 File Offset: 0x002AB2E1
		[DalConversion("DefaultSerializer", "DeletedTime", new string[]
		{

		})]
		public DateTime? DeletedTime { get; set; }

		// Token: 0x17003B92 RID: 15250
		// (get) Token: 0x0600BC84 RID: 48260 RVA: 0x002AD0EA File Offset: 0x002AB2EA
		// (set) Token: 0x0600BC85 RID: 48261 RVA: 0x002AD0F2 File Offset: 0x002AB2F2
		[DalConversion("DefaultSerializer", "Platform", new string[]
		{

		})]
		public string Platform { get; set; }

		// Token: 0x17003B93 RID: 15251
		// (get) Token: 0x0600BC86 RID: 48262 RVA: 0x002AD0FB File Offset: 0x002AB2FB
		// (set) Token: 0x0600BC87 RID: 48263 RVA: 0x002AD103 File Offset: 0x002AB303
		[DalConversion("DefaultSerializer", "AccessState", new string[]
		{

		})]
		public int? AccessState { get; set; }

		// Token: 0x17003B94 RID: 15252
		// (get) Token: 0x0600BC88 RID: 48264 RVA: 0x002AD10C File Offset: 0x002AB30C
		// (set) Token: 0x0600BC89 RID: 48265 RVA: 0x002AD114 File Offset: 0x002AB314
		[DalConversion("DefaultSerializer", "AccessStateRason", new string[]
		{

		})]
		public int? AccessStateReason { get; set; }

		// Token: 0x17003B95 RID: 15253
		// (get) Token: 0x0600BC8A RID: 48266 RVA: 0x002AD11D File Offset: 0x002AB31D
		// (set) Token: 0x0600BC8B RID: 48267 RVA: 0x002AD125 File Offset: 0x002AB325
		[Redact]
		[DalConversion("DefaultSerializer", "AccessSetBy", new string[]
		{

		})]
		public string AccessSetBy { get; set; }

		// Token: 0x17003B96 RID: 15254
		// (get) Token: 0x0600BC8C RID: 48268 RVA: 0x002AD12E File Offset: 0x002AB32E
		// (set) Token: 0x0600BC8D RID: 48269 RVA: 0x002AD136 File Offset: 0x002AB336
		[DalConversion("DefaultSerializer", "PolicyApplied", new string[]
		{

		})]
		[Redact]
		public string PolicyApplied { get; set; }

		// Token: 0x17003B97 RID: 15255
		// (get) Token: 0x0600BC8E RID: 48270 RVA: 0x002AD13F File Offset: 0x002AB33F
		// (set) Token: 0x0600BC8F RID: 48271 RVA: 0x002AD147 File Offset: 0x002AB347
		public string Manufacturer { get; set; }

		// Token: 0x17003B98 RID: 15256
		// (get) Token: 0x0600BC90 RID: 48272 RVA: 0x002AD150 File Offset: 0x002AB350
		// (set) Token: 0x0600BC91 RID: 48273 RVA: 0x002AD158 File Offset: 0x002AB358
		[Redact]
		public string SerialNumber { get; set; }

		// Token: 0x17003B99 RID: 15257
		// (get) Token: 0x0600BC92 RID: 48274 RVA: 0x002AD161 File Offset: 0x002AB361
		// (set) Token: 0x0600BC93 RID: 48275 RVA: 0x002AD169 File Offset: 0x002AB369
		[ODataInput("StartDate")]
		public DateTime? StartDate { get; set; }

		// Token: 0x17003B9A RID: 15258
		// (get) Token: 0x0600BC94 RID: 48276 RVA: 0x002AD172 File Offset: 0x002AB372
		// (set) Token: 0x0600BC95 RID: 48277 RVA: 0x002AD17A File Offset: 0x002AB37A
		[ODataInput("EndDate")]
		public DateTime? EndDate { get; set; }
	}
}
