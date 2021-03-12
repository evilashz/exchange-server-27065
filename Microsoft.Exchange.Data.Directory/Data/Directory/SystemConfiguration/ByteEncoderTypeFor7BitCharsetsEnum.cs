using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003FA RID: 1018
	public enum ByteEncoderTypeFor7BitCharsetsEnum
	{
		// Token: 0x04001F2B RID: 7979
		[LocDescription(DirectoryStrings.IDs.ByteEncoderTypeUse7Bit)]
		Use7Bit,
		// Token: 0x04001F2C RID: 7980
		[LocDescription(DirectoryStrings.IDs.ByteEncoderTypeUseQP)]
		UseQP,
		// Token: 0x04001F2D RID: 7981
		[LocDescription(DirectoryStrings.IDs.ByteEncoderTypeUseBase64)]
		UseBase64,
		// Token: 0x04001F2E RID: 7982
		[LocDescription(DirectoryStrings.IDs.ByteEncoderTypeUseQPHtmlDetectTextPlain)]
		UseQPHtmlDetectTextPlain = 5,
		// Token: 0x04001F2F RID: 7983
		[LocDescription(DirectoryStrings.IDs.ByteEncoderTypeUseBase64HtmlDetectTextPlain)]
		UseBase64HtmlDetectTextPlain,
		// Token: 0x04001F30 RID: 7984
		[LocDescription(DirectoryStrings.IDs.ByteEncoderTypeUseQPHtml7BitTextPlain)]
		UseQPHtml7BitTextPlain = 13,
		// Token: 0x04001F31 RID: 7985
		[LocDescription(DirectoryStrings.IDs.ByteEncoderTypeUseBase64Html7BitTextPlain)]
		UseBase64Html7BitTextPlain,
		// Token: 0x04001F32 RID: 7986
		[LocDescription(DirectoryStrings.IDs.ByteEncoderTypeUndefined)]
		Undefined
	}
}
