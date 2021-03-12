using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005C2 RID: 1474
	[Serializable]
	public class Status
	{
		// Token: 0x060033AC RID: 13228 RVA: 0x000D19DD File Offset: 0x000CFBDD
		public Status(StatusType statusType)
		{
			this.statusType = statusType;
		}

		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x060033AD RID: 13229 RVA: 0x000D19EC File Offset: 0x000CFBEC
		public StatusType StatusType
		{
			get
			{
				return this.statusType;
			}
		}

		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x060033AE RID: 13230 RVA: 0x000D19F4 File Offset: 0x000CFBF4
		// (set) Token: 0x060033AF RID: 13231 RVA: 0x000D19FC File Offset: 0x000CFBFC
		public string StatusMessage
		{
			get
			{
				return this.statusMessage;
			}
			internal set
			{
				this.statusMessage = value;
			}
		}

		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x060033B0 RID: 13232 RVA: 0x000D1A05 File Offset: 0x000CFC05
		// (set) Token: 0x060033B1 RID: 13233 RVA: 0x000D1A0D File Offset: 0x000CFC0D
		public DateTime StartDateTime
		{
			get
			{
				return this.startDateTime;
			}
			internal set
			{
				this.startDateTime = value;
			}
		}

		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x060033B2 RID: 13234 RVA: 0x000D1A16 File Offset: 0x000CFC16
		// (set) Token: 0x060033B3 RID: 13235 RVA: 0x000D1A1E File Offset: 0x000CFC1E
		public DateTime? EndDateTime
		{
			get
			{
				return this.endDateTime;
			}
			internal set
			{
				this.endDateTime = value;
			}
		}

		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x060033B4 RID: 13236 RVA: 0x000D1A27 File Offset: 0x000CFC27
		// (set) Token: 0x060033B5 RID: 13237 RVA: 0x000D1A2F File Offset: 0x000CFC2F
		public int ImpactedUserCount
		{
			get
			{
				return this.impactedUserCount;
			}
			internal set
			{
				this.impactedUserCount = value;
			}
		}

		// Token: 0x060033B6 RID: 13238 RVA: 0x000D1A38 File Offset: 0x000CFC38
		public override string ToString()
		{
			return string.Format("{0}: {1}", this.StatusType, this.StatusMessage);
		}

		// Token: 0x040023E4 RID: 9188
		private StatusType statusType;

		// Token: 0x040023E5 RID: 9189
		private string statusMessage;

		// Token: 0x040023E6 RID: 9190
		private DateTime startDateTime;

		// Token: 0x040023E7 RID: 9191
		private DateTime? endDateTime;

		// Token: 0x040023E8 RID: 9192
		private int impactedUserCount;
	}
}
