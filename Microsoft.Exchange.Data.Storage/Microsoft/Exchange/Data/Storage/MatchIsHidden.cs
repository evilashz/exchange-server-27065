using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000682 RID: 1666
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MatchIsHidden : IValidator
	{
		// Token: 0x0600446E RID: 17518 RVA: 0x00123D71 File Offset: 0x00121F71
		internal MatchIsHidden(bool isHidden)
		{
			this.isHidden = isHidden;
		}

		// Token: 0x0600446F RID: 17519 RVA: 0x00123D80 File Offset: 0x00121F80
		public bool Validate(DefaultFolderContext context, PropertyBag propertyBag)
		{
			bool? valueAsNullable = propertyBag.GetValueAsNullable<bool>(InternalSchema.IsHidden);
			return valueAsNullable != null && valueAsNullable.Value == this.isHidden;
		}

		// Token: 0x06004470 RID: 17520 RVA: 0x00123DB3 File Offset: 0x00121FB3
		public void SetProperties(DefaultFolderContext context, Folder folder)
		{
			folder[InternalSchema.IsHidden] = this.isHidden;
		}

		// Token: 0x04002552 RID: 9554
		private bool isHidden;
	}
}
