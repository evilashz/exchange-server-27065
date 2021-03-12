using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005A2 RID: 1442
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[ComVisible(true)]
	[Serializable]
	public abstract class Binder
	{
		// Token: 0x060043C0 RID: 17344
		public abstract MethodBase BindToMethod(BindingFlags bindingAttr, MethodBase[] match, ref object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] names, out object state);

		// Token: 0x060043C1 RID: 17345
		public abstract FieldInfo BindToField(BindingFlags bindingAttr, FieldInfo[] match, object value, CultureInfo culture);

		// Token: 0x060043C2 RID: 17346
		public abstract MethodBase SelectMethod(BindingFlags bindingAttr, MethodBase[] match, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x060043C3 RID: 17347
		public abstract PropertyInfo SelectProperty(BindingFlags bindingAttr, PropertyInfo[] match, Type returnType, Type[] indexes, ParameterModifier[] modifiers);

		// Token: 0x060043C4 RID: 17348
		public abstract object ChangeType(object value, Type type, CultureInfo culture);

		// Token: 0x060043C5 RID: 17349
		public abstract void ReorderArgumentArray(ref object[] args, object state);
	}
}
