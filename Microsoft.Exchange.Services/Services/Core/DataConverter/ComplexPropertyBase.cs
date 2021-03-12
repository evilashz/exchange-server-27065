using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020000C8 RID: 200
	internal abstract class ComplexPropertyBase : PropertyCommand
	{
		// Token: 0x060005A2 RID: 1442 RVA: 0x0001DE30 File Offset: 0x0001C030
		internal ComplexPropertyBase(CommandContext commandContext) : base(commandContext)
		{
			this.propertyDefinitions = commandContext.GetPropertyDefinitions();
		}

		// Token: 0x04000697 RID: 1687
		protected PropertyDefinition[] propertyDefinitions;
	}
}
