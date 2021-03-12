using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000E5 RID: 229
	internal abstract class HygieneFilterRuleFacade : ADObject
	{
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x0001CA84 File Offset: 0x0001AC84
		// (set) Token: 0x06000909 RID: 2313 RVA: 0x0001CA96 File Offset: 0x0001AC96
		public bool Enabled
		{
			get
			{
				return (bool)this[HygieneFilterRuleFacadeSchema.Enabled];
			}
			set
			{
				this[HygieneFilterRuleFacadeSchema.Enabled] = value;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x0001CAA9 File Offset: 0x0001ACA9
		// (set) Token: 0x0600090B RID: 2315 RVA: 0x0001CABB File Offset: 0x0001ACBB
		public int Priority
		{
			get
			{
				return (int)this[HygieneFilterRuleFacadeSchema.Priority];
			}
			set
			{
				this[HygieneFilterRuleFacadeSchema.Priority] = value;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x0001CACE File Offset: 0x0001ACCE
		// (set) Token: 0x0600090D RID: 2317 RVA: 0x0001CAE0 File Offset: 0x0001ACE0
		public string Comments
		{
			get
			{
				return (string)this[HygieneFilterRuleFacadeSchema.Comments];
			}
			set
			{
				this[HygieneFilterRuleFacadeSchema.Comments] = value;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x0001CAEE File Offset: 0x0001ACEE
		// (set) Token: 0x0600090F RID: 2319 RVA: 0x0001CB00 File Offset: 0x0001AD00
		public MultiValuedProperty<string> SentToMemberOf
		{
			get
			{
				return (MultiValuedProperty<string>)this[HygieneFilterRuleFacadeSchema.SentToMemberOf];
			}
			set
			{
				this[HygieneFilterRuleFacadeSchema.SentToMemberOf] = value;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x0001CB0E File Offset: 0x0001AD0E
		// (set) Token: 0x06000911 RID: 2321 RVA: 0x0001CB20 File Offset: 0x0001AD20
		public MultiValuedProperty<string> SentTo
		{
			get
			{
				return (MultiValuedProperty<string>)this[HygieneFilterRuleFacadeSchema.SentTo];
			}
			set
			{
				this[HygieneFilterRuleFacadeSchema.SentTo] = value;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x0001CB2E File Offset: 0x0001AD2E
		// (set) Token: 0x06000913 RID: 2323 RVA: 0x0001CB40 File Offset: 0x0001AD40
		public MultiValuedProperty<string> RecipientDomainIs
		{
			get
			{
				return (MultiValuedProperty<string>)this[HygieneFilterRuleFacadeSchema.RecipientDomainIs];
			}
			set
			{
				this[HygieneFilterRuleFacadeSchema.RecipientDomainIs] = value;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x0001CB4E File Offset: 0x0001AD4E
		// (set) Token: 0x06000915 RID: 2325 RVA: 0x0001CB60 File Offset: 0x0001AD60
		public MultiValuedProperty<string> ExceptIfRecipientDomainIs
		{
			get
			{
				return (MultiValuedProperty<string>)this[HygieneFilterRuleFacadeSchema.ExceptIfRecipientDomainIs];
			}
			set
			{
				this[HygieneFilterRuleFacadeSchema.ExceptIfRecipientDomainIs] = value;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x0001CB6E File Offset: 0x0001AD6E
		// (set) Token: 0x06000917 RID: 2327 RVA: 0x0001CB80 File Offset: 0x0001AD80
		public MultiValuedProperty<string> ExceptIfSentTo
		{
			get
			{
				return (MultiValuedProperty<string>)this[HygieneFilterRuleFacadeSchema.ExceptIfSentTo];
			}
			set
			{
				this[HygieneFilterRuleFacadeSchema.ExceptIfSentTo] = value;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x0001CB8E File Offset: 0x0001AD8E
		// (set) Token: 0x06000919 RID: 2329 RVA: 0x0001CBA0 File Offset: 0x0001ADA0
		public MultiValuedProperty<string> ExceptIfSentToMemberOf
		{
			get
			{
				return (MultiValuedProperty<string>)this[HygieneFilterRuleFacadeSchema.ExceptIfSentToMemberOf];
			}
			set
			{
				this[HygieneFilterRuleFacadeSchema.ExceptIfSentToMemberOf] = value;
			}
		}
	}
}
