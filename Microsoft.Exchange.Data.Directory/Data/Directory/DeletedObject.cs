using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020000E1 RID: 225
	[Serializable]
	public class DeletedObject : ADObject
	{
		// Token: 0x06000B2E RID: 2862 RVA: 0x000336DC File Offset: 0x000318DC
		public DeletedObject()
		{
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x000336E4 File Offset: 0x000318E4
		internal DeletedObject(IDirectorySession session, PropertyBag propertyBag)
		{
			this.m_Session = session;
			this.propertyBag = (ADPropertyBag)propertyBag;
			base.ResetChangeTracking(true);
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x00033706 File Offset: 0x00031906
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return DeletedObject.filter;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x0003370D File Offset: 0x0003190D
		internal override string MostDerivedObjectClass
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x00033714 File Offset: 0x00031914
		internal override ADObjectSchema Schema
		{
			get
			{
				return DeletedObject.schema;
			}
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0003371B File Offset: 0x0003191B
		protected override void ValidateWrite(List<ValidationError> errors)
		{
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0003371D File Offset: 0x0003191D
		protected override void ValidateRead(List<ValidationError> errors)
		{
		}

		// Token: 0x04000469 RID: 1129
		private static readonly DeletedObjectSchema schema = ObjectSchema.GetInstance<DeletedObjectSchema>();

		// Token: 0x0400046A RID: 1130
		private static readonly QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, DeletedObjectSchema.IsDeleted, true);
	}
}
