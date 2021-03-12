using System;
using System.Reflection;
using System.Security;
using System.Text;

namespace System.Runtime.Remoting
{
	// Token: 0x020007A1 RID: 1953
	internal static class XmlNamespaceEncoder
	{
		// Token: 0x060055A4 RID: 21924 RVA: 0x0012F660 File Offset: 0x0012D860
		[SecurityCritical]
		internal static string GetXmlNamespaceForType(RuntimeType type, string dynamicUrl)
		{
			string fullName = type.FullName;
			RuntimeAssembly runtimeAssembly = type.GetRuntimeAssembly();
			StringBuilder stringBuilder = new StringBuilder(256);
			Assembly assembly = typeof(string).Module.Assembly;
			if (runtimeAssembly == assembly)
			{
				stringBuilder.Append(SoapServices.namespaceNS);
				stringBuilder.Append(fullName);
			}
			else
			{
				stringBuilder.Append(SoapServices.fullNS);
				stringBuilder.Append(fullName);
				stringBuilder.Append('/');
				stringBuilder.Append(runtimeAssembly.GetSimpleName());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060055A5 RID: 21925 RVA: 0x0012F6EC File Offset: 0x0012D8EC
		[SecurityCritical]
		internal static string GetXmlNamespaceForTypeNamespace(RuntimeType type, string dynamicUrl)
		{
			string @namespace = type.Namespace;
			RuntimeAssembly runtimeAssembly = type.GetRuntimeAssembly();
			StringBuilder stringBuilder = StringBuilderCache.Acquire(256);
			Assembly assembly = typeof(string).Module.Assembly;
			if (runtimeAssembly == assembly)
			{
				stringBuilder.Append(SoapServices.namespaceNS);
				stringBuilder.Append(@namespace);
			}
			else
			{
				stringBuilder.Append(SoapServices.fullNS);
				stringBuilder.Append(@namespace);
				stringBuilder.Append('/');
				stringBuilder.Append(runtimeAssembly.GetSimpleName());
			}
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x060055A6 RID: 21926 RVA: 0x0012F778 File Offset: 0x0012D978
		[SecurityCritical]
		internal static string GetTypeNameForSoapActionNamespace(string uri, out bool assemblyIncluded)
		{
			assemblyIncluded = false;
			string fullNS = SoapServices.fullNS;
			string namespaceNS = SoapServices.namespaceNS;
			if (uri.StartsWith(fullNS, StringComparison.Ordinal))
			{
				uri = uri.Substring(fullNS.Length);
				char[] separator = new char[]
				{
					'/'
				};
				string[] array = uri.Split(separator);
				if (array.Length != 2)
				{
					return null;
				}
				assemblyIncluded = true;
				return array[0] + ", " + array[1];
			}
			else
			{
				if (uri.StartsWith(namespaceNS, StringComparison.Ordinal))
				{
					string simpleName = ((RuntimeAssembly)typeof(string).Module.Assembly).GetSimpleName();
					assemblyIncluded = true;
					return uri.Substring(namespaceNS.Length) + ", " + simpleName;
				}
				return null;
			}
		}
	}
}
