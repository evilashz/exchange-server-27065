using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.TypeConversion
{
	// Token: 0x02000067 RID: 103
	public class OfTypeTranslationRule<TLeft, TRight, TNewLeft, TNewRight> : IStorageTranslationRule<TLeft, TRight>, ITranslationRule<TLeft, TRight> where TNewLeft : class, TLeft where TNewRight : class, TRight
	{
		// Token: 0x0600023A RID: 570 RVA: 0x00007C5B File Offset: 0x00005E5B
		public OfTypeTranslationRule(ITranslationRule<TNewLeft, TNewRight> internalRule)
		{
			this.internalRule = internalRule;
			this.storageRule = (internalRule as IStorageTranslationRule<TNewLeft, TNewRight>);
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00007C76 File Offset: 0x00005E76
		IEnumerable<Microsoft.Exchange.Data.PropertyDefinition> IStorageTranslationRule<!0, !1>.StorageDependencies
		{
			get
			{
				if (this.storageRule == null)
				{
					return null;
				}
				return this.storageRule.StorageDependencies;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00007C8D File Offset: 0x00005E8D
		PropertyChangeMetadata.PropertyGroup IStorageTranslationRule<!0, !1>.StoragePropertyGroup
		{
			get
			{
				if (this.storageRule == null)
				{
					return null;
				}
				return this.storageRule.StoragePropertyGroup;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00007CA4 File Offset: 0x00005EA4
		IEnumerable<Microsoft.Exchange.Entities.DataModel.PropertyBags.PropertyDefinition> IStorageTranslationRule<!0, !1>.EntityProperties
		{
			get
			{
				if (this.storageRule == null)
				{
					return null;
				}
				return this.storageRule.EntityProperties;
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00007CBC File Offset: 0x00005EBC
		public void FromLeftToRightType(TLeft left, TRight right)
		{
			TNewLeft tnewLeft = left as TNewLeft;
			TNewRight tnewRight = right as TNewRight;
			if (tnewLeft != null && tnewRight != null)
			{
				this.internalRule.FromLeftToRightType(tnewLeft, tnewRight);
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00007D08 File Offset: 0x00005F08
		public void FromRightToLeftType(TLeft left, TRight right)
		{
			TNewLeft tnewLeft = left as TNewLeft;
			TNewRight tnewRight = right as TNewRight;
			if (tnewLeft != null && tnewRight != null)
			{
				this.internalRule.FromRightToLeftType(tnewLeft, tnewRight);
			}
		}

		// Token: 0x040000BA RID: 186
		private readonly ITranslationRule<TNewLeft, TNewRight> internalRule;

		// Token: 0x040000BB RID: 187
		private readonly IStorageTranslationRule<TNewLeft, TNewRight> storageRule;
	}
}
