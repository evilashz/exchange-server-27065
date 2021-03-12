using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000022 RID: 34
	public abstract class Property : Argument
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x000038CE File Offset: 0x00001ACE
		public Property(string propertyName, Type type) : base(type)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new RulesValidationException(RulesStrings.EmptyPropertyName);
			}
			this.propertyName = propertyName;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000038F1 File Offset: 0x00001AF1
		public string Name
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000038FC File Offset: 0x00001AFC
		public override object GetValue(RulesEvaluationContext context)
		{
			object obj = this.OnGetValue(context);
			if (obj != null)
			{
				Type type = obj.GetType();
				if (type != base.Type && !base.Type.IsAssignableFrom(type))
				{
					if (context.Tracer != null)
					{
						context.Tracer.TraceError("Property value is of the wrong type: {0}", new object[]
						{
							type
						});
					}
					return null;
				}
			}
			return obj;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003960 File Offset: 0x00001B60
		public override int GetEstimatedSize()
		{
			int num = 0;
			if (this.propertyName != null)
			{
				num += this.propertyName.Length * 2;
				num += 18;
			}
			return num + base.GetEstimatedSize();
		}

		// Token: 0x060000B4 RID: 180
		protected abstract object OnGetValue(RulesEvaluationContext context);

		// Token: 0x0400002F RID: 47
		private string propertyName;
	}
}
