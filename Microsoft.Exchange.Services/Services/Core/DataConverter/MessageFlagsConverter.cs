using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001F1 RID: 497
	internal class MessageFlagsConverter : BaseConverter
	{
		// Token: 0x06000D16 RID: 3350 RVA: 0x00042A2C File Offset: 0x00040C2C
		public MessageFlagsConverter(MessageFlags messageFlags)
		{
			this.flag = messageFlags;
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00042A3B File Offset: 0x00040C3B
		public override object ConvertToObject(string propertyString)
		{
			return (propertyString == "true" || propertyString == "1") ? this.flag : MessageFlags.None;
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x00042A65 File Offset: 0x00040C65
		public override string ConvertToString(object propertyValue)
		{
			if (this.flag == ((MessageFlags)propertyValue & this.flag))
			{
				return "true";
			}
			return "false";
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x00042A87 File Offset: 0x00040C87
		protected override object ConvertToServiceObjectValue(object propertyValue)
		{
			return this.flag == ((MessageFlags)propertyValue & this.flag);
		}

		// Token: 0x04000A7D RID: 2685
		private MessageFlags flag;
	}
}
