using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Kes
{
	// Token: 0x020001EA RID: 490
	internal class ReputationListSettings : ConfigurablePropertyBag
	{
		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x00041544 File Offset: 0x0003F744
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.ReputationListSettingsID.ToString());
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x00041564 File Offset: 0x0003F764
		// (set) Token: 0x0600149D RID: 5277 RVA: 0x00041576 File Offset: 0x0003F776
		public int ReputationListSettingsID
		{
			get
			{
				return (int)this[ReputationListSettings.ReputationListSettingsIDProperty];
			}
			set
			{
				this[ReputationListSettings.ReputationListSettingsIDProperty] = value;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x00041589 File Offset: 0x0003F789
		// (set) Token: 0x0600149F RID: 5279 RVA: 0x0004159B File Offset: 0x0003F79B
		public string Filename
		{
			get
			{
				return (string)this[ReputationListSettings.FilenameProperty];
			}
			set
			{
				this[ReputationListSettings.FilenameProperty] = value;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x000415A9 File Offset: 0x0003F7A9
		// (set) Token: 0x060014A1 RID: 5281 RVA: 0x000415BB File Offset: 0x0003F7BB
		public string CommandOptions
		{
			get
			{
				return (string)this[ReputationListSettings.CommandOptionsProperty];
			}
			set
			{
				this[ReputationListSettings.CommandOptionsProperty] = value;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x000415C9 File Offset: 0x0003F7C9
		// (set) Token: 0x060014A3 RID: 5283 RVA: 0x000415DB File Offset: 0x0003F7DB
		public string ShareLocation
		{
			get
			{
				return (string)this[ReputationListSettings.ShareLocationProperty];
			}
			set
			{
				this[ReputationListSettings.ShareLocationProperty] = value;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x000415E9 File Offset: 0x0003F7E9
		// (set) Token: 0x060014A5 RID: 5285 RVA: 0x000415FB File Offset: 0x0003F7FB
		public bool? IsEnabled
		{
			get
			{
				return (bool?)this[ReputationListSettings.IsEnabledProperty];
			}
			set
			{
				this[ReputationListSettings.IsEnabledProperty] = value;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x0004160E File Offset: 0x0003F80E
		// (set) Token: 0x060014A7 RID: 5287 RVA: 0x00041620 File Offset: 0x0003F820
		public byte? ReputationListTypeID
		{
			get
			{
				return (byte?)this[ReputationListSettings.ReputationListTypeIDProperty];
			}
			set
			{
				this[ReputationListSettings.ReputationListTypeIDProperty] = value;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x00041633 File Offset: 0x0003F833
		// (set) Token: 0x060014A9 RID: 5289 RVA: 0x00041645 File Offset: 0x0003F845
		public byte? ReputationListID
		{
			get
			{
				return (byte?)this[ReputationListSettings.ReputationListIDProperty];
			}
			set
			{
				this[ReputationListSettings.ReputationListIDProperty] = value;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x060014AA RID: 5290 RVA: 0x00041658 File Offset: 0x0003F858
		// (set) Token: 0x060014AB RID: 5291 RVA: 0x0004166A File Offset: 0x0003F86A
		public int? Score
		{
			get
			{
				return (int?)this[ReputationListSettings.ScoreProperty];
			}
			set
			{
				this[ReputationListSettings.ScoreProperty] = value;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x060014AC RID: 5292 RVA: 0x0004167D File Offset: 0x0003F87D
		// (set) Token: 0x060014AD RID: 5293 RVA: 0x0004168F File Offset: 0x0003F88F
		public DateTime? LastFileModifiedDatetime
		{
			get
			{
				return (DateTime?)this[ReputationListSettings.LastFileModifiedDatetimeProperty];
			}
			set
			{
				this[ReputationListSettings.LastFileModifiedDatetimeProperty] = value;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x060014AE RID: 5294 RVA: 0x000416A2 File Offset: 0x0003F8A2
		// (set) Token: 0x060014AF RID: 5295 RVA: 0x000416B4 File Offset: 0x0003F8B4
		public DateTime? LastDownloadedDatetime
		{
			get
			{
				return (DateTime?)this[ReputationListSettings.LastDownloadedDatetimeProperty];
			}
			set
			{
				this[ReputationListSettings.LastDownloadedDatetimeProperty] = value;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x000416C7 File Offset: 0x0003F8C7
		// (set) Token: 0x060014B1 RID: 5297 RVA: 0x000416D9 File Offset: 0x0003F8D9
		public DateTime? LastGeneratedDatetime
		{
			get
			{
				return (DateTime?)this[ReputationListSettings.LastGeneratedDatetimeProperty];
			}
			set
			{
				this[ReputationListSettings.LastGeneratedDatetimeProperty] = value;
			}
		}

		// Token: 0x04000A38 RID: 2616
		public static readonly HygienePropertyDefinition ReputationListSettingsIDProperty = new HygienePropertyDefinition("i_ReputationListSettingsId", typeof(int?));

		// Token: 0x04000A39 RID: 2617
		public static readonly HygienePropertyDefinition FilenameProperty = new HygienePropertyDefinition("nvc_Filename", typeof(string));

		// Token: 0x04000A3A RID: 2618
		public static readonly HygienePropertyDefinition CommandOptionsProperty = new HygienePropertyDefinition("nvc_CommandOptions", typeof(string));

		// Token: 0x04000A3B RID: 2619
		public static readonly HygienePropertyDefinition ShareLocationProperty = new HygienePropertyDefinition("nvc_ShareLocation", typeof(string));

		// Token: 0x04000A3C RID: 2620
		public static readonly HygienePropertyDefinition IsEnabledProperty = new HygienePropertyDefinition("f_IsEnabled", typeof(bool?));

		// Token: 0x04000A3D RID: 2621
		public static readonly HygienePropertyDefinition ReputationListTypeIDProperty = new HygienePropertyDefinition("ti_ReputationListTypeId", typeof(byte?));

		// Token: 0x04000A3E RID: 2622
		public static readonly HygienePropertyDefinition ReputationListIDProperty = new HygienePropertyDefinition("ti_ReputationListId", typeof(byte?));

		// Token: 0x04000A3F RID: 2623
		public static readonly HygienePropertyDefinition ScoreProperty = new HygienePropertyDefinition("i_Score", typeof(int?));

		// Token: 0x04000A40 RID: 2624
		public static readonly HygienePropertyDefinition LastFileModifiedDatetimeProperty = new HygienePropertyDefinition("dt_LastFileModifiedDatetime", typeof(DateTime?));

		// Token: 0x04000A41 RID: 2625
		public static readonly HygienePropertyDefinition LastDownloadedDatetimeProperty = new HygienePropertyDefinition("dt_LastDownloadedDatetime", typeof(DateTime?));

		// Token: 0x04000A42 RID: 2626
		public static readonly HygienePropertyDefinition LastGeneratedDatetimeProperty = new HygienePropertyDefinition("dt_LastGeneratedDatetime", typeof(DateTime?));
	}
}
