using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization.Formatters
{
	// Token: 0x02000736 RID: 1846
	[SecurityCritical]
	[ComVisible(true)]
	public sealed class InternalST
	{
		// Token: 0x060051EC RID: 20972 RVA: 0x0011F163 File Offset: 0x0011D363
		private InternalST()
		{
		}

		// Token: 0x060051ED RID: 20973 RVA: 0x0011F16B File Offset: 0x0011D36B
		[Conditional("_LOGGING")]
		public static void InfoSoap(params object[] messages)
		{
		}

		// Token: 0x060051EE RID: 20974 RVA: 0x0011F16D File Offset: 0x0011D36D
		public static bool SoapCheckEnabled()
		{
			return BCLDebug.CheckEnabled("Soap");
		}

		// Token: 0x060051EF RID: 20975 RVA: 0x0011F179 File Offset: 0x0011D379
		[Conditional("SER_LOGGING")]
		public static void Soap(params object[] messages)
		{
			if (!(messages[0] is string))
			{
				messages[0] = messages[0].GetType().Name + " ";
				return;
			}
			messages[0] = messages[0] + " ";
		}

		// Token: 0x060051F0 RID: 20976 RVA: 0x0011F1B0 File Offset: 0x0011D3B0
		[Conditional("_DEBUG")]
		public static void SoapAssert(bool condition, string message)
		{
		}

		// Token: 0x060051F1 RID: 20977 RVA: 0x0011F1B2 File Offset: 0x0011D3B2
		public static void SerializationSetValue(FieldInfo fi, object target, object value)
		{
			if (fi == null)
			{
				throw new ArgumentNullException("fi");
			}
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			FormatterServices.SerializationSetValue(fi, target, value);
		}

		// Token: 0x060051F2 RID: 20978 RVA: 0x0011F1EC File Offset: 0x0011D3EC
		public static Assembly LoadAssemblyFromString(string assemblyString)
		{
			return FormatterServices.LoadAssemblyFromString(assemblyString);
		}
	}
}
