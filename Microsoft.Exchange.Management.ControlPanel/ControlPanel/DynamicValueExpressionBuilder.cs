using System;
using System.CodeDom;
using System.Reflection;
using System.Web.Compilation;
using System.Web.UI;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200054E RID: 1358
	public class DynamicValueExpressionBuilder : ExpressionBuilder
	{
		// Token: 0x170024CE RID: 9422
		// (get) Token: 0x06003FAF RID: 16303 RVA: 0x000C030A File Offset: 0x000BE50A
		public override bool SupportsEvaluate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003FB0 RID: 16304 RVA: 0x000C0310 File Offset: 0x000BE510
		public override object EvaluateExpression(object target, BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
		{
			string text = null;
			string text2 = null;
			this.ParseParameter(entry, out text, out text2);
			Module[] modules = Assembly.GetExecutingAssembly().GetModules(false);
			Module module = null;
			foreach (Module module2 in modules)
			{
				if (string.Compare(DynamicValueExpressionBuilder.SearchModuleName, module2.Name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					module = module2;
					break;
				}
			}
			if (null == module)
			{
				throw new ApplicationException("Could not find module by name " + DynamicValueExpressionBuilder.SearchModuleName + ". If the module name is changed, please change DynamicValueExpressionBuilder.SearchModuleName.");
			}
			Type type = module.GetType(DynamicValueExpressionBuilder.SearchNamespaceName + text);
			if (null == type)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Can not find your class ",
					text,
					". Please check 1. You defined your static class in namespace ",
					DynamicValueExpressionBuilder.SearchNamespaceName,
					". 2. You spell your class name correctly. "
				}));
			}
			PropertyInfo property = type.GetProperty(text2, BindingFlags.Static);
			if (null == property)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Can not find your property ",
					text2,
					" on class ",
					text,
					". Please make sure you spelled your static property name correctly. "
				}));
			}
			object value = property.GetValue(null, null);
			return value.ToString();
		}

		// Token: 0x06003FB1 RID: 16305 RVA: 0x000C0450 File Offset: 0x000BE650
		public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
		{
			string type = null;
			string propertyName = null;
			this.ParseParameter(entry, out type, out propertyName);
			CodePropertyReferenceExpression targetObject = new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(type), propertyName);
			return new CodeMethodInvokeExpression(targetObject, "ToString", new CodeExpression[0]);
		}

		// Token: 0x06003FB2 RID: 16306 RVA: 0x000C048C File Offset: 0x000BE68C
		private void ParseParameter(BoundPropertyEntry entry, out string className, out string propertyName)
		{
			string text = entry.Expression.Trim();
			string[] array = text.Split(new char[]
			{
				'.'
			});
			if (2 != array.Length)
			{
				throw new ArgumentException("DynamicValue formated is StaticClassName.StaticPropertyName");
			}
			className = array[0];
			propertyName = array[1];
		}

		// Token: 0x04002A3E RID: 10814
		private static readonly string SearchModuleName = "Microsoft.Exchange.Management.ControlPanel.DLL";

		// Token: 0x04002A3F RID: 10815
		private static readonly string SearchNamespaceName = "Microsoft.Exchange.Management.ControlPanel.";
	}
}
