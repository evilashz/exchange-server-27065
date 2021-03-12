using System;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Format
{
	// Token: 0x0200029A RID: 666
	internal enum FormatContainerType : byte
	{
		// Token: 0x0400205D RID: 8285
		Null,
		// Token: 0x0400205E RID: 8286
		Root = 129,
		// Token: 0x0400205F RID: 8287
		Document,
		// Token: 0x04002060 RID: 8288
		Fragment,
		// Token: 0x04002061 RID: 8289
		Block,
		// Token: 0x04002062 RID: 8290
		BlockQuote,
		// Token: 0x04002063 RID: 8291
		HorizontalLine,
		// Token: 0x04002064 RID: 8292
		TableContainer = 7,
		// Token: 0x04002065 RID: 8293
		TableDefinition,
		// Token: 0x04002066 RID: 8294
		TableColumnGroup,
		// Token: 0x04002067 RID: 8295
		TableColumn,
		// Token: 0x04002068 RID: 8296
		TableCaption = 139,
		// Token: 0x04002069 RID: 8297
		TableExtraContent,
		// Token: 0x0400206A RID: 8298
		Table,
		// Token: 0x0400206B RID: 8299
		TableRow,
		// Token: 0x0400206C RID: 8300
		TableCell,
		// Token: 0x0400206D RID: 8301
		List,
		// Token: 0x0400206E RID: 8302
		ListItem,
		// Token: 0x0400206F RID: 8303
		Inline = 18,
		// Token: 0x04002070 RID: 8304
		HyperLink,
		// Token: 0x04002071 RID: 8305
		Bookmark,
		// Token: 0x04002072 RID: 8306
		Image = 85,
		// Token: 0x04002073 RID: 8307
		Area = 22,
		// Token: 0x04002074 RID: 8308
		Map = 151,
		// Token: 0x04002075 RID: 8309
		BaseFont = 24,
		// Token: 0x04002076 RID: 8310
		Form,
		// Token: 0x04002077 RID: 8311
		FieldSet,
		// Token: 0x04002078 RID: 8312
		Label,
		// Token: 0x04002079 RID: 8313
		Input,
		// Token: 0x0400207A RID: 8314
		Button,
		// Token: 0x0400207B RID: 8315
		Legend,
		// Token: 0x0400207C RID: 8316
		TextArea,
		// Token: 0x0400207D RID: 8317
		Select,
		// Token: 0x0400207E RID: 8318
		OptionGroup,
		// Token: 0x0400207F RID: 8319
		Option,
		// Token: 0x04002080 RID: 8320
		Text = 36,
		// Token: 0x04002081 RID: 8321
		PropertyContainer,
		// Token: 0x04002082 RID: 8322
		InlineObjectFlag = 64,
		// Token: 0x04002083 RID: 8323
		BlockFlag = 128
	}
}
