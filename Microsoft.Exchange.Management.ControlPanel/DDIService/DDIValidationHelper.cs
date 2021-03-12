using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000178 RID: 376
	internal static class DDIValidationHelper
	{
		// Token: 0x06002235 RID: 8757 RVA: 0x00067320 File Offset: 0x00065520
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
