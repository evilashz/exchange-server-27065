using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMPhoneSession
{
	// Token: 0x0200012C RID: 300
	[Serializable]
	public class UMPhoneSession : ConfigurableObject
	{
		// Token: 0x060009B4 RID: 2484 RVA: 0x00025BD0 File Offset: 0x00023DD0
		public UMPhoneSession() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x00025BDD File Offset: 0x00023DDD
		internal static UMPhoneSessionSchema Schema
		{
			get
			{
				return UMPhoneSession.schema;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x00025BE4 File Offset: 0x00023DE4
		// (set) Token: 0x060009B7 RID: 2487 RVA: 0x00025BF6 File Offset: 0x00023DF6
		public UMCallState CallState
		{
			get
			{
				return (UMCallState)this[UMPhoneSessionSchema.CallState];
			}
			set
			{
				this[UMPhoneSessionSchema.CallState] = value;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x00025C09 File Offset: 0x00023E09
		// (set) Token: 0x060009B9 RID: 2489 RVA: 0x00025C1B File Offset: 0x00023E1B
		public UMOperationResult OperationResult
		{
			get
			{
				return (UMOperationResult)this[UMPhoneSessionSchema.OperationResult];
			}
			set
			{
				this[UMPhoneSessionSchema.OperationResult] = value;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x00025C2E File Offset: 0x00023E2E
		// (set) Token: 0x060009BB RID: 2491 RVA: 0x00025C40 File Offset: 0x00023E40
		public UMEventCause EventCause
		{
			get
			{
				return (UMEventCause)this[UMPhoneSessionSchema.EventCause];
			}
			set
			{
				this[UMPhoneSessionSchema.EventCause] = value;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x00025C53 File Offset: 0x00023E53
		// (set) Token: 0x060009BD RID: 2493 RVA: 0x00025C65 File Offset: 0x00023E65
		internal string PhoneNumber
		{
			get
			{
				return (string)this[UMPhoneSessionSchema.PhoneNumber];
			}
			set
			{
				this[UMPhoneSessionSchema.PhoneNumber] = value;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x00025C73 File Offset: 0x00023E73
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return UMPhoneSession.schema;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x00025C7A File Offset: 0x00023E7A
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x04000563 RID: 1379
		private static UMPhoneSessionSchema schema = new UMPhoneSessionSchema();
	}
}
