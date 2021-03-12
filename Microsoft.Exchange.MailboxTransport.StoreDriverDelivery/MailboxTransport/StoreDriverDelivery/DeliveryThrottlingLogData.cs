using System;
using System.Collections.Generic;
using Microsoft.Exchange.Transport.LoggingCommon;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000029 RID: 41
	internal class DeliveryThrottlingLogData
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000B440 File Offset: 0x00009640
		// (set) Token: 0x0600020F RID: 527 RVA: 0x0000B448 File Offset: 0x00009648
		public ThrottlingScope ThrottlingScope { get; private set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000B451 File Offset: 0x00009651
		// (set) Token: 0x06000211 RID: 529 RVA: 0x0000B459 File Offset: 0x00009659
		public ThrottlingResource ThrottlingResource { get; private set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000B462 File Offset: 0x00009662
		// (set) Token: 0x06000213 RID: 531 RVA: 0x0000B46A File Offset: 0x0000966A
		public double Threshold { get; private set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000B473 File Offset: 0x00009673
		// (set) Token: 0x06000215 RID: 533 RVA: 0x0000B47B File Offset: 0x0000967B
		public ThrottlingImpactUnits ImpactUnits { get; private set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000B484 File Offset: 0x00009684
		// (set) Token: 0x06000217 RID: 535 RVA: 0x0000B48C File Offset: 0x0000968C
		public uint Impact { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000B495 File Offset: 0x00009695
		// (set) Token: 0x06000219 RID: 537 RVA: 0x0000B49D File Offset: 0x0000969D
		public long Total { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000B4A6 File Offset: 0x000096A6
		// (set) Token: 0x0600021B RID: 539 RVA: 0x0000B4AE File Offset: 0x000096AE
		public Guid ExternalOrganizationId { get; private set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000B4B7 File Offset: 0x000096B7
		// (set) Token: 0x0600021D RID: 541 RVA: 0x0000B4BF File Offset: 0x000096BF
		public string Recipient { get; private set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000B4C8 File Offset: 0x000096C8
		// (set) Token: 0x0600021F RID: 543 RVA: 0x0000B4D0 File Offset: 0x000096D0
		public string MDBName { get; private set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000B4D9 File Offset: 0x000096D9
		// (set) Token: 0x06000221 RID: 545 RVA: 0x0000B4E1 File Offset: 0x000096E1
		public IList<KeyValuePair<string, double>> MDBHealth { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000B4EA File Offset: 0x000096EA
		// (set) Token: 0x06000223 RID: 547 RVA: 0x0000B4F2 File Offset: 0x000096F2
		public IList<KeyValuePair<string, string>> CustomData { get; set; }

		// Token: 0x06000224 RID: 548 RVA: 0x0000B4FC File Offset: 0x000096FC
		public DeliveryThrottlingLogData(ThrottlingScope scope, ThrottlingResource resource, double resourceThreshold, ThrottlingImpactUnits impactUnits, uint impact, long total, Guid externalOrganizationId, string recipient, string mdbName, IList<KeyValuePair<string, double>> mdbHealth, IList<KeyValuePair<string, string>> customData)
		{
			this.ThrottlingScope = scope;
			this.ThrottlingResource = resource;
			this.Threshold = resourceThreshold;
			this.ImpactUnits = impactUnits;
			this.Impact = impact;
			this.Total = total;
			this.ExternalOrganizationId = externalOrganizationId;
			this.Recipient = recipient;
			this.MDBName = mdbName;
			this.MDBHealth = mdbHealth;
			this.CustomData = customData;
		}
	}
}
