using System;
using System.Reflection;
using System.Windows.Markup;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000146 RID: 326
	public class InvokeExtension : MarkupExtension
	{
		// Token: 0x0600213C RID: 8508 RVA: 0x000643ED File Offset: 0x000625ED
		public InvokeExtension(string target)
		{
			this.target = target;
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x000643FC File Offset: 0x000625FC
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			object result = null;
			if (!string.IsNullOrEmpty(this.target))
			{
				string[] array = this.target.Split(new char[]
				{
					'.'
				});
				if (array.Length == 2)
				{
					string typeName = "Microsoft.Exchange.Management.DDIService." + array[0];
					string name = array[1];
					Type type = Type.GetType(typeName, false);
					if (type != null)
					{
						FieldInfo field = type.GetField(name, BindingFlags.Static | BindingFlags.Public);
						result = field.GetValue(null);
					}
					DDIHelper.Trace("InvokeExtension invoked: {0}.", new object[]
					{
						this.target
					});
				}
				else
				{
					DDIHelper.Trace("InvokeExtension parse failed: {0}. The invoke target should be in 'Type.Method' format. Default namespace is {1} Update InvokeExtension if want to use type inother namespace.", new object[]
					{
						this.target,
						"Microsoft.Exchange.Management.DDIService."
					});
				}
			}
			return result;
		}

		// Token: 0x04001D14 RID: 7444
		private const string DefaultNameSpaceWithDot = "Microsoft.Exchange.Management.DDIService.";

		// Token: 0x04001D15 RID: 7445
		private readonly string target;
	}
}
