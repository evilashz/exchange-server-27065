using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200018F RID: 399
	internal class TaskStatusDescriptionProperty : SimpleProperty
	{
		// Token: 0x06000B41 RID: 2881 RVA: 0x00035608 File Offset: 0x00033808
		private TaskStatusDescriptionProperty(CommandContext commandContext, BaseConverter converter) : base(commandContext, converter)
		{
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00035612 File Offset: 0x00033812
		public new static TaskStatusDescriptionProperty CreateCommand(CommandContext commandContext)
		{
			return new TaskStatusDescriptionProperty(commandContext, new StringConverter());
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0003561F File Offset: 0x0003381F
		protected override bool StorePropertyExists(StoreObject storeObject)
		{
			return true;
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00035624 File Offset: 0x00033824
		protected override object GetPropertyValue(StoreObject storeObject)
		{
			Task task = storeObject as Task;
			CallContext callContext = CallContext.Current;
			return task.StatusDescription.ToString(callContext.ClientCulture);
		}
	}
}
