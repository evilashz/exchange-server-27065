using System;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000052 RID: 82
	internal class AddEnvelopeRecipient : TransportAction
	{
		// Token: 0x060002FB RID: 763 RVA: 0x00010F44 File Offset: 0x0000F144
		public AddEnvelopeRecipient(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002FC RID: 764 RVA: 0x00010F4D File Offset: 0x0000F14D
		public override string Name
		{
			get
			{
				return "AddEnvelopeRecipient";
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00010F54 File Offset: 0x0000F154
		public override TransportActionType Type
		{
			get
			{
				return TransportActionType.NonRecipientRelated;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002FE RID: 766 RVA: 0x00010F57 File Offset: 0x0000F157
		public override Type[] ArgumentsType
		{
			get
			{
				return AddEnvelopeRecipient.argumentTypes;
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00010F60 File Offset: 0x0000F160
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			string text = (string)base.Arguments[0].GetValue(transportRulesEvaluationContext);
			ExTraceGlobals.TransportRulesEngineTracer.TraceDebug<string>(0L, "AddEnvelopeRecipient: {0}", text);
			transportRulesEvaluationContext.RecipientsToAdd.Add(new TransportRulesEvaluationContext.AddedRecipient(text, text, RecipientP2Type.Bcc));
			return ExecutionControl.Execute;
		}

		// Token: 0x040001E8 RID: 488
		private static Type[] argumentTypes = new Type[]
		{
			typeof(string)
		};
	}
}
