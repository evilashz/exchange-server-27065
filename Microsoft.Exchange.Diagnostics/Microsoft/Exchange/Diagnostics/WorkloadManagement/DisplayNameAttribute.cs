using System;
using System.Reflection;

namespace Microsoft.Exchange.Diagnostics.WorkloadManagement
{
	// Token: 0x020001FB RID: 507
	[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	internal sealed class DisplayNameAttribute : Attribute
	{
		// Token: 0x06000EEE RID: 3822 RVA: 0x0003D0C0 File Offset: 0x0003B2C0
		public DisplayNameAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0003D0CF File Offset: 0x0003B2CF
		public DisplayNameAttribute(string attNamespace, string name)
		{
			this.Name = attNamespace + "." + name;
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x0003D0E9 File Offset: 0x0003B2E9
		// (set) Token: 0x06000EF1 RID: 3825 RVA: 0x0003D0F1 File Offset: 0x0003B2F1
		public string Name { get; private set; }

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0003D0FC File Offset: 0x0003B2FC
		internal static string GetEnumName(Enum value)
		{
			Type type = value.GetType();
			FieldInfo declaredField = value.GetType().GetTypeInfo().GetDeclaredField(value.ToString());
			DisplayNameAttribute[] array = (DisplayNameAttribute[])declaredField.GetCustomAttributes(typeof(DisplayNameAttribute), true);
			string result;
			if (array.Length > 0)
			{
				result = array[0].Name;
			}
			else
			{
				result = type.Name + "." + value.ToString();
			}
			return result;
		}
	}
}
