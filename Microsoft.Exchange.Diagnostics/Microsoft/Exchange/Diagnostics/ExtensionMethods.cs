using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000131 RID: 305
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DebuggerNonUserCode]
	internal static class ExtensionMethods
	{
		// Token: 0x060008C0 RID: 2240 RVA: 0x00022354 File Offset: 0x00020554
		public static DisposeGuard Guard(this IDisposable disposable)
		{
			if (disposable == null)
			{
				throw new ArgumentNullException("disposable");
			}
			DisposeGuard result = default(DisposeGuard);
			result.Add<IDisposable>(disposable);
			return result;
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00022384 File Offset: 0x00020584
		public static string GetApplicationVersion(this Type type)
		{
			string result = string.Empty;
			IEnumerable<AssemblyFileVersionAttribute> customAttributes = type.GetTypeInfo().Assembly.GetCustomAttributes<AssemblyFileVersionAttribute>();
			if (customAttributes != null && customAttributes.Count<AssemblyFileVersionAttribute>() > 0)
			{
				AssemblyFileVersionAttribute assemblyFileVersionAttribute = customAttributes.First<AssemblyFileVersionAttribute>();
				if (assemblyFileVersionAttribute != null)
				{
					result = Regex.Replace(assemblyFileVersionAttribute.Version, "0*([0-9]+)", "$1");
				}
			}
			return result;
		}
	}
}
