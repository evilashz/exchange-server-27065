using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C9C RID: 3228
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AttendeeSchema : RecipientSchema
	{
		// Token: 0x17001E39 RID: 7737
		// (get) Token: 0x060070B4 RID: 28852 RVA: 0x001F3203 File Offset: 0x001F1403
		public new static AttendeeSchema Instance
		{
			get
			{
				if (AttendeeSchema.instance == null)
				{
					AttendeeSchema.instance = new AttendeeSchema();
				}
				return AttendeeSchema.instance;
			}
		}

		// Token: 0x04004DEB RID: 19947
		public static readonly StorePropertyDefinition AttendeeType = InternalSchema.RecipientType;

		// Token: 0x04004DEC RID: 19948
		internal static readonly StorePropertyDefinition ObjectType = InternalSchema.ObjectType;

		// Token: 0x04004DED RID: 19949
		private static AttendeeSchema instance = null;
	}
}
