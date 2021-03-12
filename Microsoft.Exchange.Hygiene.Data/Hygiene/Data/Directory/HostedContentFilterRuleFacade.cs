using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000E6 RID: 230
	internal class HostedContentFilterRuleFacade : HygieneFilterRuleFacade
	{
		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x0001CBB6 File Offset: 0x0001ADB6
		public override ObjectId Identity
		{
			get
			{
				return (ADObjectId)this[ADObjectSchema.Id];
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x0001CBC8 File Offset: 0x0001ADC8
		// (set) Token: 0x0600091D RID: 2333 RVA: 0x0001CBDA File Offset: 0x0001ADDA
		public string HostedContentFilterPolicy
		{
			get
			{
				return (string)this[HostedContentFilterRuleFacadeSchema.HostedContentFilterPolicy];
			}
			set
			{
				this[HostedContentFilterRuleFacadeSchema.HostedContentFilterPolicy] = value;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x0001CBE8 File Offset: 0x0001ADE8
		internal override ADObjectSchema Schema
		{
			get
			{
				return HostedContentFilterRuleFacade.schema;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x0001CBEF File Offset: 0x0001ADEF
		internal override string MostDerivedObjectClass
		{
			get
			{
				return HostedContentFilterRuleFacade.mostDerivedClass;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x0001CBF6 File Offset: 0x0001ADF6
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x040004AE RID: 1198
		private static readonly HostedContentFilterRuleFacadeSchema schema = ObjectSchema.GetInstance<HostedContentFilterRuleFacadeSchema>();

		// Token: 0x040004AF RID: 1199
		private static string mostDerivedClass = "HostedContentFilterRuleFacade";
	}
}
