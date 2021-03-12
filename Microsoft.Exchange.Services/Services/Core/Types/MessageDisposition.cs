using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008E0 RID: 2272
	internal static class MessageDisposition
	{
		// Token: 0x06003FB1 RID: 16305 RVA: 0x000DC2C8 File Offset: 0x000DA4C8
		public static MessageDispositionType ConvertToEnum(string messageDispositionValue)
		{
			MessageDispositionType result = MessageDispositionType.SendOnly;
			if (messageDispositionValue != null)
			{
				if (!(messageDispositionValue == "SendOnly"))
				{
					if (!(messageDispositionValue == "SaveOnly"))
					{
						if (messageDispositionValue == "SendAndSaveCopy")
						{
							result = MessageDispositionType.SendAndSaveCopy;
						}
					}
					else
					{
						result = MessageDispositionType.SaveOnly;
					}
				}
				else
				{
					result = MessageDispositionType.SendOnly;
				}
			}
			return result;
		}

		// Token: 0x04002541 RID: 9537
		public const string SendOnlyValue = "SendOnly";

		// Token: 0x04002542 RID: 9538
		public const string SaveOnlyValue = "SaveOnly";

		// Token: 0x04002543 RID: 9539
		public const string SendAndSaveCopyValue = "SendAndSaveCopy";
	}
}
