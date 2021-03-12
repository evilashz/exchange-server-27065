using System;
using System.Globalization;

namespace Microsoft.Exchange.MessagingPolicies.Rules.OutlookProtection
{
	// Token: 0x02000185 RID: 389
	internal sealed class RightsProtectMessageAction : Action
	{
		// Token: 0x06000A60 RID: 2656 RVA: 0x0002C3C0 File Offset: 0x0002A5C0
		public RightsProtectMessageAction(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0002C3CC File Offset: 0x0002A5CC
		public static RightsProtectMessageAction Create(Guid templateId, string templateName)
		{
			return new RightsProtectMessageAction(new ShortList<Argument>
			{
				new Value(templateId.ToString("D", CultureInfo.InvariantCulture)),
				new Value(templateName)
			});
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x0002C40D File Offset: 0x0002A60D
		public override string Name
		{
			get
			{
				return "RightsProtectMessage";
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x0002C414 File Offset: 0x0002A614
		public override Type[] ArgumentsType
		{
			get
			{
				return RightsProtectMessageAction.ArgumentTypes;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x0002C41B File Offset: 0x0002A61B
		public string TemplateId
		{
			get
			{
				if (base.Arguments == null || base.Arguments.Count < 1)
				{
					throw new InvalidOperationException("argument list is corrupted.");
				}
				return (string)base.Arguments[0].GetValue(null);
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x0002C455 File Offset: 0x0002A655
		public string TemplateName
		{
			get
			{
				if (base.Arguments == null || base.Arguments.Count < 2)
				{
					throw new InvalidOperationException("argument list is corrupted.");
				}
				return (string)base.Arguments[1].GetValue(null);
			}
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0002C48F File Offset: 0x0002A68F
		protected override ExecutionControl OnExecute(RulesEvaluationContext context)
		{
			throw new NotSupportedException("Outlook Protection rules are only executed on Outlook.");
		}

		// Token: 0x040007FF RID: 2047
		private static readonly Type[] ArgumentTypes = new Type[]
		{
			typeof(string),
			typeof(string)
		};
	}
}
