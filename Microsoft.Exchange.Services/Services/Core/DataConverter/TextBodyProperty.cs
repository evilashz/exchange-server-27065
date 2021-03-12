using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000168 RID: 360
	internal class TextBodyProperty : BodyProperty
	{
		// Token: 0x06000A3A RID: 2618 RVA: 0x00031B6C File Offset: 0x0002FD6C
		public TextBodyProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00031B75 File Offset: 0x0002FD75
		public new static TextBodyProperty CreateCommand(CommandContext commandContext)
		{
			return new TextBodyProperty(commandContext);
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00031B7D File Offset: 0x0002FD7D
		protected override BodyFormat ComputeBodyFormat(BodyResponseType bodyType, Item item)
		{
			return BodyFormat.TextPlain;
		}
	}
}
