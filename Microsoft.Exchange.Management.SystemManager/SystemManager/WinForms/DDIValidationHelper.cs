using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000FD RID: 253
	internal static class DDIValidationHelper
	{
		// Token: 0x0600094F RID: 2383 RVA: 0x00020A74 File Offset: 0x0001EC74
		public static Type[] GetAssemblyTypes(Assembly assembly)
		{
			Type[] result = null;
			try
			{
				result = assembly.GetTypes();
			}
			catch (ReflectionTypeLoadException ex)
			{
				result = (from type in ex.Types
				where type != null
				select type).ToArray<Type>();
			}
			return result;
		}
	}
}
