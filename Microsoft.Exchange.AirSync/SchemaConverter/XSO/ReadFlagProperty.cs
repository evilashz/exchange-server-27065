using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x020001F5 RID: 501
	internal class ReadFlagProperty : PropertyBase, IBooleanProperty, IProperty
	{
		// Token: 0x060013A4 RID: 5028 RVA: 0x00071042 File Offset: 0x0006F242
		public ReadFlagProperty(bool isRead)
		{
			this.isRead = isRead;
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x060013A5 RID: 5029 RVA: 0x00071051 File Offset: 0x0006F251
		public bool BooleanData
		{
			get
			{
				return this.isRead;
			}
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x0007105C File Offset: 0x0006F25C
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"( Base: ",
				base.ToString(),
				", IsRead: ",
				this.isRead,
				", state: ",
				base.State,
				")"
			});
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x000710BB File Offset: 0x0006F2BB
		public override void CopyFrom(IProperty srcProperty)
		{
			throw new InvalidOperationException("ReadFlagProperty is read-only.");
		}

		// Token: 0x04000C2E RID: 3118
		private bool isRead;
	}
}
