using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage.MailboxRules
{
	// Token: 0x02000C07 RID: 3079
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TagWorkItem : WorkItem
	{
		// Token: 0x06006DE8 RID: 28136 RVA: 0x001D8120 File Offset: 0x001D6320
		public TagWorkItem(IRuleEvaluationContext context, PropValue value, int actionIndex) : base(context, actionIndex)
		{
			this.value = value;
		}

		// Token: 0x17001DCF RID: 7631
		// (get) Token: 0x06006DE9 RID: 28137 RVA: 0x001D8131 File Offset: 0x001D6331
		public override ExecutionStage Stage
		{
			get
			{
				return ExecutionStage.OnCreatedMessage | ExecutionStage.OnPublicFolderAfter;
			}
		}

		// Token: 0x06006DEA RID: 28138 RVA: 0x001D8138 File Offset: 0x001D6338
		public override void Execute()
		{
			MessageItem deliveredMessage = base.Context.DeliveredMessage;
			StorePropertyDefinition propertyDefinitionForTag = base.Context.GetPropertyDefinitionForTag(this.value.PropTag);
			if (!RuleUtil.IsMultiValueTag(this.value.PropTag))
			{
				base.Context.TraceDebug<PropTag, object>("Setting single value property '{0}' to '{1}'", this.value.PropTag, this.value.Value ?? "null");
				deliveredMessage[propertyDefinitionForTag] = this.value.Value;
				return;
			}
			Array array = this.value.Value as Array;
			if (RuleUtil.IsNullOrEmpty(array))
			{
				base.Context.TraceDebug<PropTag>("Clearing the existing multi-value property '{0}' since new value specified in rule was null or empty.", this.value.PropTag);
				deliveredMessage[propertyDefinitionForTag] = this.value.Value;
				return;
			}
			Array array2 = deliveredMessage.TryGetProperty(propertyDefinitionForTag) as Array;
			if (RuleUtil.IsNullOrEmpty(array2))
			{
				base.Context.TraceDebug<PropTag, int>("Setting multi-value property '{0}' to {1} elements, since no existing value were found.", this.value.PropTag, array.Length);
				deliveredMessage[propertyDefinitionForTag] = array;
				return;
			}
			Type elementType = array2.GetType().GetElementType();
			Array array3 = Array.CreateInstance(elementType, array2.Length + array.Length);
			array2.CopyTo(array3, 0);
			array.CopyTo(array3, array2.Length);
			base.Context.TraceDebug<PropTag, int>("Setting multi-value property '{0}' to {1} elements, which contains the concatenated array of existing and new values.", this.value.PropTag, array3.Length);
			deliveredMessage[propertyDefinitionForTag] = array3;
		}

		// Token: 0x04003EAE RID: 16046
		private static readonly PropValue TestValue = new PropValue(PropTag.Sensitivity, 3);

		// Token: 0x04003EAF RID: 16047
		private PropValue value;
	}
}
