using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000D58 RID: 3416
	[Serializable]
	public abstract class UMCallReportBase : ConfigurableObject
	{
		// Token: 0x060082F4 RID: 33524 RVA: 0x00217A77 File Offset: 0x00215C77
		public UMCallReportBase(ObjectId identity) : base(new SimpleProviderPropertyBag())
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.propertyBag.SetField(SimpleProviderObjectSchema.Identity, identity);
		}

		// Token: 0x170028B8 RID: 10424
		// (get) Token: 0x060082F5 RID: 33525 RVA: 0x00217AA4 File Offset: 0x00215CA4
		// (set) Token: 0x060082F6 RID: 33526 RVA: 0x00217AB6 File Offset: 0x00215CB6
		public float? NMOS
		{
			get
			{
				return (float?)this[UMCallReportBaseSchema.NMOS];
			}
			internal set
			{
				this[UMCallReportBaseSchema.NMOS] = value;
			}
		}

		// Token: 0x170028B9 RID: 10425
		// (get) Token: 0x060082F7 RID: 33527 RVA: 0x00217AC9 File Offset: 0x00215CC9
		// (set) Token: 0x060082F8 RID: 33528 RVA: 0x00217ADB File Offset: 0x00215CDB
		public float? NMOSDegradation
		{
			get
			{
				return (float?)this[UMCallReportBaseSchema.NMOSDegradation];
			}
			internal set
			{
				this[UMCallReportBaseSchema.NMOSDegradation] = value;
			}
		}

		// Token: 0x170028BA RID: 10426
		// (get) Token: 0x060082F9 RID: 33529 RVA: 0x00217AEE File Offset: 0x00215CEE
		// (set) Token: 0x060082FA RID: 33530 RVA: 0x00217B00 File Offset: 0x00215D00
		public float? PercentPacketLoss
		{
			get
			{
				return (float?)this[UMCallReportBaseSchema.PercentPacketLoss];
			}
			internal set
			{
				this[UMCallReportBaseSchema.PercentPacketLoss] = value;
			}
		}

		// Token: 0x170028BB RID: 10427
		// (get) Token: 0x060082FB RID: 33531 RVA: 0x00217B13 File Offset: 0x00215D13
		// (set) Token: 0x060082FC RID: 33532 RVA: 0x00217B25 File Offset: 0x00215D25
		public float? Jitter
		{
			get
			{
				return (float?)this[UMCallReportBaseSchema.Jitter];
			}
			internal set
			{
				this[UMCallReportBaseSchema.Jitter] = value;
			}
		}

		// Token: 0x170028BC RID: 10428
		// (get) Token: 0x060082FD RID: 33533 RVA: 0x00217B38 File Offset: 0x00215D38
		// (set) Token: 0x060082FE RID: 33534 RVA: 0x00217B4A File Offset: 0x00215D4A
		public float? RoundTripMilliseconds
		{
			get
			{
				return (float?)this[UMCallReportBaseSchema.RoundTripMilliseconds];
			}
			internal set
			{
				this[UMCallReportBaseSchema.RoundTripMilliseconds] = value;
			}
		}

		// Token: 0x170028BD RID: 10429
		// (get) Token: 0x060082FF RID: 33535 RVA: 0x00217B5D File Offset: 0x00215D5D
		// (set) Token: 0x06008300 RID: 33536 RVA: 0x00217B6F File Offset: 0x00215D6F
		public float? BurstLossDurationMilliseconds
		{
			get
			{
				return (float?)this[UMCallReportBaseSchema.BurstLossDurationMilliseconds];
			}
			internal set
			{
				this[UMCallReportBaseSchema.BurstLossDurationMilliseconds] = value;
			}
		}

		// Token: 0x170028BE RID: 10430
		// (get) Token: 0x06008301 RID: 33537 RVA: 0x00217B82 File Offset: 0x00215D82
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}
	}
}
