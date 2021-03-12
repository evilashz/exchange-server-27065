using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005A9 RID: 1449
	[Serializable]
	public class MRSHealthCheckOutcome : ConfigurableObject
	{
		// Token: 0x060032FB RID: 13051 RVA: 0x000D02A8 File Offset: 0x000CE4A8
		public MRSHealthCheckOutcome() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x060032FC RID: 13052 RVA: 0x000D02B5 File Offset: 0x000CE4B5
		internal MRSHealthCheckOutcome(string server, MRSHealthCheckId checkId, bool passed, LocalizedString msg) : this()
		{
			this[SimpleProviderObjectSchema.Identity] = new MRSHealthCheckOutcomeObjectId(server);
			this.Check = checkId;
			this.Passed = passed;
			this.Message = msg;
		}

		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x060032FD RID: 13053 RVA: 0x000D02E4 File Offset: 0x000CE4E4
		// (set) Token: 0x060032FE RID: 13054 RVA: 0x000D02F6 File Offset: 0x000CE4F6
		public MRSHealthCheckId Check
		{
			get
			{
				return (MRSHealthCheckId)this[MRSHealthCheckOutcomeSchema.Check];
			}
			private set
			{
				this[MRSHealthCheckOutcomeSchema.Check] = value;
			}
		}

		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x060032FF RID: 13055 RVA: 0x000D0309 File Offset: 0x000CE509
		// (set) Token: 0x06003300 RID: 13056 RVA: 0x000D0325 File Offset: 0x000CE525
		public bool Passed
		{
			get
			{
				return (bool)(this[MRSHealthCheckOutcomeSchema.Passed] ?? false);
			}
			private set
			{
				this[MRSHealthCheckOutcomeSchema.Passed] = value;
			}
		}

		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x06003301 RID: 13057 RVA: 0x000D0338 File Offset: 0x000CE538
		// (set) Token: 0x06003302 RID: 13058 RVA: 0x000D034A File Offset: 0x000CE54A
		public LocalizedString Message
		{
			get
			{
				return (LocalizedString)this[MRSHealthCheckOutcomeSchema.Message];
			}
			private set
			{
				this[MRSHealthCheckOutcomeSchema.Message] = value;
			}
		}

		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x06003303 RID: 13059 RVA: 0x000D035D File Offset: 0x000CE55D
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x06003304 RID: 13060 RVA: 0x000D0364 File Offset: 0x000CE564
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MRSHealthCheckOutcome.schema;
			}
		}

		// Token: 0x0400239C RID: 9116
		private static MRSHealthCheckOutcomeSchema schema = ObjectSchema.GetInstance<MRSHealthCheckOutcomeSchema>();
	}
}
