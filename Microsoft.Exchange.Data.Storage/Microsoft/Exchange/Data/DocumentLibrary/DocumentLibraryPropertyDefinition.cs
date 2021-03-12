using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x0200069E RID: 1694
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DocumentLibraryPropertyDefinition : PropertyDefinition
	{
		// Token: 0x0600451C RID: 17692 RVA: 0x0012606D File Offset: 0x0012426D
		internal DocumentLibraryPropertyDefinition(string name, Type type, object defaultValue, DocumentLibraryPropertyId propertyId) : base(name, type)
		{
			this.defaultValue = defaultValue;
			this.propertyId = propertyId;
			if (defaultValue != null && defaultValue.GetType() != type)
			{
				throw new ArgumentException();
			}
		}

		// Token: 0x17001412 RID: 5138
		// (get) Token: 0x0600451D RID: 17693 RVA: 0x0012609D File Offset: 0x0012429D
		public object DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
		}

		// Token: 0x17001413 RID: 5139
		// (get) Token: 0x0600451E RID: 17694 RVA: 0x001260A5 File Offset: 0x001242A5
		internal DocumentLibraryPropertyId PropertyId
		{
			get
			{
				return this.propertyId;
			}
		}

		// Token: 0x040025AE RID: 9646
		private readonly object defaultValue;

		// Token: 0x040025AF RID: 9647
		private readonly DocumentLibraryPropertyId propertyId;
	}
}
