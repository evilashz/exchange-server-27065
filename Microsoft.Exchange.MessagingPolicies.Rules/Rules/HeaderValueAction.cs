using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000053 RID: 83
	internal abstract class HeaderValueAction : TransportAction
	{
		// Token: 0x06000301 RID: 769 RVA: 0x00010FDC File Offset: 0x0000F1DC
		protected HeaderValueAction(ShortList<Argument> arguments) : base(arguments)
		{
			if (!(base.Arguments[0] is Value) || !(base.Arguments[1] is Value))
			{
				throw new RulesValidationException(RulesStrings.ActionRequiresConstantArguments(this.Name));
			}
			string text = (string)base.Arguments[0].GetValue(null);
			string value = (string)base.Arguments[1].GetValue(null);
			if (!TransportUtils.IsHeaderValid(text))
			{
				throw new RulesValidationException(TransportRulesStrings.InvalidHeaderName(text));
			}
			if (!TransportUtils.IsHeaderSettable(text, value))
			{
				throw new RulesValidationException(TransportRulesStrings.CannotSetHeader(text, value));
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00011081 File Offset: 0x0000F281
		public override Type[] ArgumentsType
		{
			get
			{
				return HeaderValueAction.argumentTypes;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00011088 File Offset: 0x0000F288
		public override TransportActionType Type
		{
			get
			{
				return TransportActionType.BifurcationNeeded;
			}
		}

		// Token: 0x040001E9 RID: 489
		public const string SubjectHeader = "subject";

		// Token: 0x040001EA RID: 490
		private static Type[] argumentTypes = new Type[]
		{
			typeof(string),
			typeof(string)
		};
	}
}
