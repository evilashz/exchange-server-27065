using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x0200024B RID: 587
	internal interface ISupportRecipientFilter
	{
		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001CA6 RID: 7334
		string RecipientFilter { get; }

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001CA7 RID: 7335
		string LdapRecipientFilter { get; }

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001CA8 RID: 7336
		// (set) Token: 0x06001CA9 RID: 7337
		WellKnownRecipientType? IncludedRecipients { get; set; }

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001CAA RID: 7338
		// (set) Token: 0x06001CAB RID: 7339
		MultiValuedProperty<string> ConditionalDepartment { get; set; }

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001CAC RID: 7340
		// (set) Token: 0x06001CAD RID: 7341
		MultiValuedProperty<string> ConditionalCompany { get; set; }

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06001CAE RID: 7342
		// (set) Token: 0x06001CAF RID: 7343
		MultiValuedProperty<string> ConditionalStateOrProvince { get; set; }

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001CB0 RID: 7344
		// (set) Token: 0x06001CB1 RID: 7345
		ADObjectId RecipientContainer { get; set; }

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001CB2 RID: 7346
		// (set) Token: 0x06001CB3 RID: 7347
		MultiValuedProperty<string> ConditionalCustomAttribute1 { get; set; }

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001CB4 RID: 7348
		// (set) Token: 0x06001CB5 RID: 7349
		MultiValuedProperty<string> ConditionalCustomAttribute2 { get; set; }

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001CB6 RID: 7350
		// (set) Token: 0x06001CB7 RID: 7351
		MultiValuedProperty<string> ConditionalCustomAttribute3 { get; set; }

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001CB8 RID: 7352
		// (set) Token: 0x06001CB9 RID: 7353
		MultiValuedProperty<string> ConditionalCustomAttribute4 { get; set; }

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001CBA RID: 7354
		// (set) Token: 0x06001CBB RID: 7355
		MultiValuedProperty<string> ConditionalCustomAttribute5 { get; set; }

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001CBC RID: 7356
		// (set) Token: 0x06001CBD RID: 7357
		MultiValuedProperty<string> ConditionalCustomAttribute6 { get; set; }

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001CBE RID: 7358
		// (set) Token: 0x06001CBF RID: 7359
		MultiValuedProperty<string> ConditionalCustomAttribute7 { get; set; }

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001CC0 RID: 7360
		// (set) Token: 0x06001CC1 RID: 7361
		MultiValuedProperty<string> ConditionalCustomAttribute8 { get; set; }

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001CC2 RID: 7362
		// (set) Token: 0x06001CC3 RID: 7363
		MultiValuedProperty<string> ConditionalCustomAttribute9 { get; set; }

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001CC4 RID: 7364
		// (set) Token: 0x06001CC5 RID: 7365
		MultiValuedProperty<string> ConditionalCustomAttribute10 { get; set; }

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001CC6 RID: 7366
		// (set) Token: 0x06001CC7 RID: 7367
		MultiValuedProperty<string> ConditionalCustomAttribute11 { get; set; }

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001CC8 RID: 7368
		// (set) Token: 0x06001CC9 RID: 7369
		MultiValuedProperty<string> ConditionalCustomAttribute12 { get; set; }

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001CCA RID: 7370
		// (set) Token: 0x06001CCB RID: 7371
		MultiValuedProperty<string> ConditionalCustomAttribute13 { get; set; }

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001CCC RID: 7372
		// (set) Token: 0x06001CCD RID: 7373
		MultiValuedProperty<string> ConditionalCustomAttribute14 { get; set; }

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001CCE RID: 7374
		// (set) Token: 0x06001CCF RID: 7375
		MultiValuedProperty<string> ConditionalCustomAttribute15 { get; set; }

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001CD0 RID: 7376
		WellKnownRecipientFilterType RecipientFilterType { get; }

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001CD1 RID: 7377
		ADPropertyDefinition RecipientFilterSchema { get; }

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001CD2 RID: 7378
		ADPropertyDefinition LdapRecipientFilterSchema { get; }

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001CD3 RID: 7379
		ADPropertyDefinition IncludedRecipientsSchema { get; }

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001CD4 RID: 7380
		ADPropertyDefinition ConditionalDepartmentSchema { get; }

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001CD5 RID: 7381
		ADPropertyDefinition ConditionalCompanySchema { get; }

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001CD6 RID: 7382
		ADPropertyDefinition ConditionalStateOrProvinceSchema { get; }

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001CD7 RID: 7383
		ADPropertyDefinition ConditionalCustomAttribute1Schema { get; }

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001CD8 RID: 7384
		ADPropertyDefinition ConditionalCustomAttribute2Schema { get; }

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001CD9 RID: 7385
		ADPropertyDefinition ConditionalCustomAttribute3Schema { get; }

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001CDA RID: 7386
		ADPropertyDefinition ConditionalCustomAttribute4Schema { get; }

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001CDB RID: 7387
		ADPropertyDefinition ConditionalCustomAttribute5Schema { get; }

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001CDC RID: 7388
		ADPropertyDefinition ConditionalCustomAttribute6Schema { get; }

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06001CDD RID: 7389
		ADPropertyDefinition ConditionalCustomAttribute7Schema { get; }

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001CDE RID: 7390
		ADPropertyDefinition ConditionalCustomAttribute8Schema { get; }

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001CDF RID: 7391
		ADPropertyDefinition ConditionalCustomAttribute9Schema { get; }

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001CE0 RID: 7392
		ADPropertyDefinition ConditionalCustomAttribute10Schema { get; }

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001CE1 RID: 7393
		ADPropertyDefinition ConditionalCustomAttribute11Schema { get; }

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001CE2 RID: 7394
		ADPropertyDefinition ConditionalCustomAttribute12Schema { get; }

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001CE3 RID: 7395
		ADPropertyDefinition ConditionalCustomAttribute13Schema { get; }

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001CE4 RID: 7396
		ADPropertyDefinition ConditionalCustomAttribute14Schema { get; }

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001CE5 RID: 7397
		ADPropertyDefinition ConditionalCustomAttribute15Schema { get; }
	}
}
