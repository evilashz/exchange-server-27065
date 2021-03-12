using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200019F RID: 415
	internal class ContactsFolderShape : Shape
	{
		// Token: 0x06000B83 RID: 2947 RVA: 0x00037F14 File Offset: 0x00036114
		static ContactsFolderShape()
		{
			ContactsFolderShape.defaultProperties.Add(BaseFolderSchema.FolderId);
			ContactsFolderShape.defaultProperties.Add(BaseFolderSchema.DisplayName);
			ContactsFolderShape.defaultProperties.Add(BaseFolderSchema.TotalCount);
			ContactsFolderShape.defaultProperties.Add(BaseFolderSchema.ChildFolderCount);
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x00037F67 File Offset: 0x00036167
		public ContactsFolderShape() : base(Schema.ContactsFolder, ContactsFolderSchema.GetSchema(), new BaseFolderShape(), ContactsFolderShape.defaultProperties)
		{
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x00037F83 File Offset: 0x00036183
		internal static ContactsFolderShape CreateShape()
		{
			return new ContactsFolderShape();
		}

		// Token: 0x04000876 RID: 2166
		private static List<PropertyInformation> defaultProperties = new List<PropertyInformation>();
	}
}
