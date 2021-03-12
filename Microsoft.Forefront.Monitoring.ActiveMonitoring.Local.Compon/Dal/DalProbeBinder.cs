using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Dal
{
	// Token: 0x0200005F RID: 95
	public class DalProbeBinder : Binder
	{
		// Token: 0x06000273 RID: 627 RVA: 0x0000F700 File Offset: 0x0000D900
		public override MethodBase BindToMethod(BindingFlags bindingAttr, MethodBase[] match, ref object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] names, out object state)
		{
			int argCount = (args != null) ? args.Length : 0;
			MethodBase[] array = (from method in match
			where method.GetParameters().Length == argCount
			select method).ToArray<MethodBase>();
			if (array.Length == 1)
			{
				state = null;
				return array[0];
			}
			return Type.DefaultBinder.BindToMethod(bindingAttr, match, ref args, modifiers, culture, names, out state);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000F75F File Offset: 0x0000D95F
		public override FieldInfo BindToField(BindingFlags bindingAttr, FieldInfo[] match, object value, CultureInfo culture)
		{
			return null;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000F762 File Offset: 0x0000D962
		public override MethodBase SelectMethod(BindingFlags bindingAttr, MethodBase[] match, Type[] types, ParameterModifier[] modifiers)
		{
			return Type.DefaultBinder.SelectMethod(bindingAttr, match, types, modifiers);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000F773 File Offset: 0x0000D973
		public override PropertyInfo SelectProperty(BindingFlags bindingAttr, PropertyInfo[] match, Type returnType, Type[] indexes, ParameterModifier[] modifiers)
		{
			return null;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000F776 File Offset: 0x0000D976
		public override object ChangeType(object value, Type myChangeType, CultureInfo culture)
		{
			return Convert.ChangeType(value, myChangeType);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000F77F File Offset: 0x0000D97F
		public override void ReorderArgumentArray(ref object[] args, object state)
		{
		}
	}
}
