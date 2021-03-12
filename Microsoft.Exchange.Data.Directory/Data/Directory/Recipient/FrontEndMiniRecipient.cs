using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x0200024A RID: 586
	[Serializable]
	public class FrontEndMiniRecipient : MiniObject
	{
		// Token: 0x06001C9C RID: 7324 RVA: 0x00077DAB File Offset: 0x00075FAB
		internal FrontEndMiniRecipient(IRecipientSession session, PropertyBag propertyBag)
		{
			this.m_Session = session;
			this.propertyBag = (ADPropertyBag)propertyBag;
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x00077DC6 File Offset: 0x00075FC6
		public FrontEndMiniRecipient()
		{
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001C9E RID: 7326 RVA: 0x00077DCE File Offset: 0x00075FCE
		public Guid ExchangeGuid
		{
			get
			{
				return (Guid)this[FrontEndMiniRecipientSchema.ExchangeGuid];
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001C9F RID: 7327 RVA: 0x00077DE0 File Offset: 0x00075FE0
		public ADObjectId Database
		{
			get
			{
				return (ADObjectId)this[FrontEndMiniRecipientSchema.Database];
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001CA0 RID: 7328 RVA: 0x00077DF2 File Offset: 0x00075FF2
		public ADObjectId ArchiveDatabase
		{
			get
			{
				return (ADObjectId)this[FrontEndMiniRecipientSchema.ArchiveDatabase];
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001CA1 RID: 7329 RVA: 0x00077E04 File Offset: 0x00076004
		public DateTime? LastExchangeChangedTime
		{
			get
			{
				return (DateTime?)this[FrontEndMiniRecipientSchema.LastExchangeChangedTime];
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001CA2 RID: 7330 RVA: 0x00077E16 File Offset: 0x00076016
		internal override ADObjectSchema Schema
		{
			get
			{
				return FrontEndMiniRecipient.schema;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001CA3 RID: 7331 RVA: 0x00077E1D File Offset: 0x0007601D
		internal override string MostDerivedObjectClass
		{
			get
			{
				return FrontEndMiniRecipient.mostDerivedClass;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001CA4 RID: 7332 RVA: 0x00077E24 File Offset: 0x00076024
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return FrontEndMiniRecipient.implicitFilterInternal;
			}
		}

		// Token: 0x04000DB3 RID: 3507
		private static FrontEndMiniRecipientSchema schema = ObjectSchema.GetInstance<FrontEndMiniRecipientSchema>();

		// Token: 0x04000DB4 RID: 3508
		private static string mostDerivedClass = "user";

		// Token: 0x04000DB5 RID: 3509
		private static string objectCategoryNameInternal = "person";

		// Token: 0x04000DB6 RID: 3510
		private static QueryFilter implicitFilterInternal = new AndFilter(new QueryFilter[]
		{
			ADObject.ObjectClassFilter(FrontEndMiniRecipient.mostDerivedClass, true),
			ADObject.ObjectCategoryFilter(FrontEndMiniRecipient.objectCategoryNameInternal)
		});
	}
}
