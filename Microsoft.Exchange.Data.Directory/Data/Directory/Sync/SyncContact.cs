using System;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000827 RID: 2087
	internal class SyncContact : SyncOrgPerson
	{
		// Token: 0x060067A9 RID: 26537 RVA: 0x0016DCBD File Offset: 0x0016BEBD
		public SyncContact(SyncDirection syncDirection) : base(syncDirection)
		{
		}

		// Token: 0x170024AD RID: 9389
		// (get) Token: 0x060067AA RID: 26538 RVA: 0x0016DCC6 File Offset: 0x0016BEC6
		public override SyncObjectSchema Schema
		{
			get
			{
				return SyncContact.schema;
			}
		}

		// Token: 0x170024AE RID: 9390
		// (get) Token: 0x060067AB RID: 26539 RVA: 0x0016DCCD File Offset: 0x0016BECD
		internal override DirectoryObjectClass ObjectClass
		{
			get
			{
				return DirectoryObjectClass.Contact;
			}
		}

		// Token: 0x060067AC RID: 26540 RVA: 0x0016DCD0 File Offset: 0x0016BED0
		protected override DirectoryObject CreateDirectoryObject()
		{
			return new Contact();
		}

		// Token: 0x060067AD RID: 26541 RVA: 0x0016DCD8 File Offset: 0x0016BED8
		public override SyncRecipient CreatePlaceHolder()
		{
			SyncContact syncContact = (SyncContact)base.CreatePlaceHolder();
			if (base.ExternalEmailAddress.HasValue)
			{
				syncContact.ExternalEmailAddress = base.ExternalEmailAddress.Value;
			}
			return syncContact;
		}

		// Token: 0x170024AF RID: 9391
		// (get) Token: 0x060067AE RID: 26542 RVA: 0x0016DD15 File Offset: 0x0016BF15
		// (set) Token: 0x060067AF RID: 26543 RVA: 0x0016DD27 File Offset: 0x0016BF27
		public SyncProperty<MultiValuedProperty<SyncLink>> Manager
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<SyncLink>>)base[SyncContactSchema.Manager];
			}
			set
			{
				base[SyncContactSchema.Manager] = value;
			}
		}

		// Token: 0x04004447 RID: 17479
		private static readonly SyncContactSchema schema = ObjectSchema.GetInstance<SyncContactSchema>();
	}
}
