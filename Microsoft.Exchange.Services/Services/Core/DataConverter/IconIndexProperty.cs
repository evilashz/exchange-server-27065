using System;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000108 RID: 264
	internal class IconIndexProperty : SimpleProperty
	{
		// Token: 0x0600078F RID: 1935 RVA: 0x00024CB0 File Offset: 0x00022EB0
		private IconIndexProperty(CommandContext commandContext, BaseConverter converter) : base(commandContext, converter)
		{
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00024CBA File Offset: 0x00022EBA
		public new static IconIndexProperty CreateCommand(CommandContext commandContext)
		{
			return new IconIndexProperty(commandContext, new IconIndexConverter());
		}
	}
}
