using System;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Format
{
	// Token: 0x0200028A RID: 650
	internal enum PropertyType : byte
	{
		// Token: 0x04001FD1 RID: 8145
		Null,
		// Token: 0x04001FD2 RID: 8146
		Calculated,
		// Token: 0x04001FD3 RID: 8147
		Bool,
		// Token: 0x04001FD4 RID: 8148
		String,
		// Token: 0x04001FD5 RID: 8149
		MultiValue,
		// Token: 0x04001FD6 RID: 8150
		Enum,
		// Token: 0x04001FD7 RID: 8151
		Color,
		// Token: 0x04001FD8 RID: 8152
		Integer,
		// Token: 0x04001FD9 RID: 8153
		Fractional,
		// Token: 0x04001FDA RID: 8154
		Percentage,
		// Token: 0x04001FDB RID: 8155
		AbsLength,
		// Token: 0x04001FDC RID: 8156
		RelLength,
		// Token: 0x04001FDD RID: 8157
		Pixels,
		// Token: 0x04001FDE RID: 8158
		Ems,
		// Token: 0x04001FDF RID: 8159
		Exs,
		// Token: 0x04001FE0 RID: 8160
		HtmlFontUnits,
		// Token: 0x04001FE1 RID: 8161
		RelHtmlFontUnits,
		// Token: 0x04001FE2 RID: 8162
		Multiple,
		// Token: 0x04001FE3 RID: 8163
		Milliseconds,
		// Token: 0x04001FE4 RID: 8164
		kHz,
		// Token: 0x04001FE5 RID: 8165
		Degrees,
		// Token: 0x04001FE6 RID: 8166
		FirstLength = 10,
		// Token: 0x04001FE7 RID: 8167
		LastLength = 17
	}
}
