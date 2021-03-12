using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000680 RID: 1664
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MatchContainerClass : IValidator
	{
		// Token: 0x06004469 RID: 17513 RVA: 0x00123D04 File Offset: 0x00121F04
		internal MatchContainerClass(string containerClass)
		{
			this.containerClass = containerClass;
		}

		// Token: 0x0600446A RID: 17514 RVA: 0x00123D13 File Offset: 0x00121F13
		public virtual bool Validate(DefaultFolderContext context, PropertyBag propertyBag)
		{
			return propertyBag.TryGetProperty(InternalSchema.ContainerClass) as string == this.containerClass;
		}

		// Token: 0x0600446B RID: 17515 RVA: 0x00123D30 File Offset: 0x00121F30
		public void SetProperties(DefaultFolderContext context, Folder folder)
		{
			folder[InternalSchema.ContainerClass] = this.containerClass;
		}

		// Token: 0x04002551 RID: 9553
		private string containerClass;
	}
}
