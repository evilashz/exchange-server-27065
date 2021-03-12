using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020002B5 RID: 693
	[Serializable]
	public class TransportProcessingQuotaConfig : ConfigurableObject
	{
		// Token: 0x060018F9 RID: 6393 RVA: 0x0004EC30 File Offset: 0x0004CE30
		public TransportProcessingQuotaConfig() : base(new SimpleProviderPropertyBag())
		{
			this[TransportProcessingQuotaConfigSchema.Id] = this.identity;
			this[TransportProcessingQuotaConfigSchema.SettingName] = base.GetType().Name;
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x060018FA RID: 6394 RVA: 0x0004EC84 File Offset: 0x0004CE84
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.identity.ToString());
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x060018FB RID: 6395 RVA: 0x0004ECAA File Offset: 0x0004CEAA
		// (set) Token: 0x060018FC RID: 6396 RVA: 0x0004ECBC File Offset: 0x0004CEBC
		public bool ThrottlingEnabled
		{
			get
			{
				return (bool)this[TransportProcessingQuotaConfigSchema.ThrottlingEnabled];
			}
			set
			{
				this[TransportProcessingQuotaConfigSchema.ThrottlingEnabled] = value;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x060018FD RID: 6397 RVA: 0x0004ECCF File Offset: 0x0004CECF
		// (set) Token: 0x060018FE RID: 6398 RVA: 0x0004ECE1 File Offset: 0x0004CEE1
		public bool CalculationEnabled
		{
			get
			{
				return (bool)this[TransportProcessingQuotaConfigSchema.CalculationEnabled];
			}
			set
			{
				this[TransportProcessingQuotaConfigSchema.CalculationEnabled] = value;
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x060018FF RID: 6399 RVA: 0x0004ECF4 File Offset: 0x0004CEF4
		// (set) Token: 0x06001900 RID: 6400 RVA: 0x0004ED06 File Offset: 0x0004CF06
		public double AmWeight
		{
			get
			{
				return (double)this[TransportProcessingQuotaConfigSchema.AmWeight];
			}
			set
			{
				this[TransportProcessingQuotaConfigSchema.AmWeight] = value;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06001901 RID: 6401 RVA: 0x0004ED19 File Offset: 0x0004CF19
		// (set) Token: 0x06001902 RID: 6402 RVA: 0x0004ED2B File Offset: 0x0004CF2B
		public double AsWeight
		{
			get
			{
				return (double)this[TransportProcessingQuotaConfigSchema.AsWeight];
			}
			set
			{
				this[TransportProcessingQuotaConfigSchema.AsWeight] = value;
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06001903 RID: 6403 RVA: 0x0004ED3E File Offset: 0x0004CF3E
		// (set) Token: 0x06001904 RID: 6404 RVA: 0x0004ED50 File Offset: 0x0004CF50
		public int CalculationFrequency
		{
			get
			{
				return (int)this[TransportProcessingQuotaConfigSchema.CalculationFrequency];
			}
			set
			{
				this[TransportProcessingQuotaConfigSchema.CalculationFrequency] = value;
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06001905 RID: 6405 RVA: 0x0004ED63 File Offset: 0x0004CF63
		// (set) Token: 0x06001906 RID: 6406 RVA: 0x0004ED75 File Offset: 0x0004CF75
		public int CostThreshold
		{
			get
			{
				return (int)this[TransportProcessingQuotaConfigSchema.CostThreshold];
			}
			set
			{
				this[TransportProcessingQuotaConfigSchema.CostThreshold] = value;
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06001907 RID: 6407 RVA: 0x0004ED88 File Offset: 0x0004CF88
		// (set) Token: 0x06001908 RID: 6408 RVA: 0x0004ED9A File Offset: 0x0004CF9A
		public double EtrWeight
		{
			get
			{
				return (double)this[TransportProcessingQuotaConfigSchema.EtrWeight];
			}
			set
			{
				this[TransportProcessingQuotaConfigSchema.EtrWeight] = value;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06001909 RID: 6409 RVA: 0x0004EDAD File Offset: 0x0004CFAD
		// (set) Token: 0x0600190A RID: 6410 RVA: 0x0004EDBF File Offset: 0x0004CFBF
		public int TimeWindow
		{
			get
			{
				return (int)this[TransportProcessingQuotaConfigSchema.TimeWindow];
			}
			set
			{
				this[TransportProcessingQuotaConfigSchema.TimeWindow] = value;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x0600190B RID: 6411 RVA: 0x0004EDD2 File Offset: 0x0004CFD2
		// (set) Token: 0x0600190C RID: 6412 RVA: 0x0004EDE4 File Offset: 0x0004CFE4
		public double ThrottleFactor
		{
			get
			{
				return (double)this[TransportProcessingQuotaConfigSchema.ThrottleFactor];
			}
			set
			{
				this[TransportProcessingQuotaConfigSchema.ThrottleFactor] = value;
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x0600190D RID: 6413 RVA: 0x0004EDF7 File Offset: 0x0004CFF7
		// (set) Token: 0x0600190E RID: 6414 RVA: 0x0004EE09 File Offset: 0x0004D009
		public double RelativeCostThreshold
		{
			get
			{
				return (double)this[TransportProcessingQuotaConfigSchema.RelativeCostThreshold];
			}
			set
			{
				this[TransportProcessingQuotaConfigSchema.RelativeCostThreshold] = value;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x0600190F RID: 6415 RVA: 0x0004EE1C File Offset: 0x0004D01C
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return TransportProcessingQuotaConfig.SchemaObject;
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06001910 RID: 6416 RVA: 0x0004EE23 File Offset: 0x0004D023
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x04000EBE RID: 3774
		private static readonly TransportProcessingQuotaConfigSchema SchemaObject = ObjectSchema.GetInstance<TransportProcessingQuotaConfigSchema>();

		// Token: 0x04000EBF RID: 3775
		private readonly Guid identity = Guid.Parse("A6E1583F-3A26-DC67-A881-229DB7431D92");
	}
}
