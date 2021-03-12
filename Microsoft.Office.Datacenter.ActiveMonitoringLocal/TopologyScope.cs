using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000042 RID: 66
	[Table]
	public class TopologyScope : TableEntity, IWorkData
	{
		// Token: 0x060004C7 RID: 1223 RVA: 0x00011EE8 File Offset: 0x000100E8
		public TopologyScope()
		{
			this.CreatedTime = DateTime.UtcNow;
			this.LastDataUpdateTime = new DateTime(1970, 1, 1);
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00011F0D File Offset: 0x0001010D
		// (set) Token: 0x060004C9 RID: 1225 RVA: 0x00011F15 File Offset: 0x00010115
		[Column(DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
		public int Id { get; set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x00011F1E File Offset: 0x0001011E
		// (set) Token: 0x060004CB RID: 1227 RVA: 0x00011F26 File Offset: 0x00010126
		[Range(0, 999, ErrorMessage = "{0} is a mandatory property, and must be between 0..999")]
		[Column]
		[Required(ErrorMessage = "{0} is a mandatory property")]
		public int AggregationLevel { get; set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x00011F2F File Offset: 0x0001012F
		// (set) Token: 0x060004CD RID: 1229 RVA: 0x00011F37 File Offset: 0x00010137
		[Column]
		[Required(AllowEmptyStrings = false, ErrorMessage = "{0} is a mandatory property")]
		[RegularExpression("^[-_a-zA-Z0-9]{1,255}$", ErrorMessage = "Only alpha-numeric characters, dash, and underscore are allowed, and length must be between 1 and 255 characters.")]
		public string Name { get; set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00011F40 File Offset: 0x00010140
		// (set) Token: 0x060004CF RID: 1231 RVA: 0x00011F48 File Offset: 0x00010148
		[RegularExpression("^[-_a-zA-Z0-9/]{3,1024}$", ErrorMessage = "Only alpha-numeric and front slash characters are allowed, and length must be between 3 and 255 characters.")]
		[Column]
		public string ParentScope { get; set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00011F51 File Offset: 0x00010151
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x00011F59 File Offset: 0x00010159
		[RegularExpression("^[a-zA-Z0-9]{1,255}$", ErrorMessage = "Only alpha-numeric characters are allowed, and length must be between 1 and 255 characters.")]
		[Column]
		[Required(AllowEmptyStrings = false, ErrorMessage = "{0} is a mandatory property")]
		public string ReplicationScopeKey { get; set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00011F62 File Offset: 0x00010162
		// (set) Token: 0x060004D3 RID: 1235 RVA: 0x00011F6A File Offset: 0x0001016A
		[Column]
		public DateTime LastDataUpdateTime { get; set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00011F73 File Offset: 0x00010173
		// (set) Token: 0x060004D5 RID: 1237 RVA: 0x00011F7B File Offset: 0x0001017B
		[Column]
		public DateTime CreatedTime { get; set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00011F84 File Offset: 0x00010184
		public string InternalStorageKey
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00011F8B File Offset: 0x0001018B
		public string ExternalStorageKey
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00011F92 File Offset: 0x00010192
		public string SecondaryExternalStorageKey
		{
			get
			{
				return string.Empty;
			}
		}
	}
}
