using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Reflection;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000002 RID: 2
	public static class CmdletAssemblyHelper
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002143 File Offset: 0x00000343
		public static string[] ManagementCmdletAssemblyNames
		{
			get
			{
				return CmdletAssemblyHelper._managementCmdletAssemblyNames;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002164 File Offset: 0x00000364
		public static bool IsCmdletAssembly(string assemblyNameWithoutExtension)
		{
			return CmdletAssemblyHelper._managementCmdletAssemblyNamesWithoutExtension.Any((string cmdletAssemblyNameWithoutExtension) => string.Equals(assemblyNameWithoutExtension, cmdletAssemblyNameWithoutExtension, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002194 File Offset: 0x00000394
		public static bool EnsureTargetTypesLoaded(string[] assemblyNames, string[] typeNames)
		{
			if (assemblyNames == null)
			{
				throw new ArgumentNullException("assemblyNames");
			}
			if (typeNames == null)
			{
				throw new ArgumentNullException("typeNames");
			}
			if (assemblyNames.Length == 0)
			{
				assemblyNames = CmdletAssemblyHelper.ManagementCmdletAssemblyNames;
			}
			if (assemblyNames.Length == 0)
			{
				throw new ArgumentException("Cannot find any assembly to load types.");
			}
			TaskHelper.LoadExchangeAssemblyAndReferencesFromSpecificPathForAssemblies(ConfigurationContext.Setup.BinPath, assemblyNames);
			foreach (string valueToConvert in typeNames)
			{
				Type type;
				if (!LanguagePrimitives.TryConvertTo<Type>(valueToConvert, out type))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002214 File Offset: 0x00000414
		public static string[] AppendCmdletAssemblyNames(params string[] assemblyNames)
		{
			if (assemblyNames == null || assemblyNames.Length == 0)
			{
				return (from x in CmdletAssemblyHelper.ManagementCmdletAssemblyNames
				select x).ToArray<string>();
			}
			string[] array = new string[assemblyNames.Length + CmdletAssemblyHelper.ManagementCmdletAssemblyNames.Length];
			assemblyNames.CopyTo(array, 0);
			CmdletAssemblyHelper.ManagementCmdletAssemblyNames.CopyTo(array, assemblyNames.Length);
			return array;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000229C File Offset: 0x0000049C
		public static Assembly[] GetAllCmdletAssemblies(string basePath)
		{
			if (!Directory.Exists(basePath))
			{
				throw new ArgumentException(string.Format("Base path '{0}' doesn't exist, abort the operation.", basePath ?? "null"), "basePath");
			}
			return (from x in CmdletAssemblyHelper.ManagementCmdletAssemblyNames
			select Assembly.LoadFrom(Path.Combine(basePath, x))).ToArray<Assembly>();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002304 File Offset: 0x00000504
		public static Assembly[] LoadingAllCmdletAssembliesAndReference(string basePath, params string[] additionAssemblyNames)
		{
			if (!Directory.Exists(basePath))
			{
				throw new ArgumentException(string.Format("Base path '{0}' doesn't exist, abort the operation.", basePath ?? "null"), "basePath");
			}
			if (additionAssemblyNames == null || additionAssemblyNames.Length == 0)
			{
				return TaskHelper.LoadExchangeAssemblyAndReferencesFromSpecificPathForAssemblies(basePath, CmdletAssemblyHelper.ManagementCmdletAssemblyNames);
			}
			List<string> list = new List<string>(CmdletAssemblyHelper.ManagementCmdletAssemblyNames);
			list.AddRange(additionAssemblyNames);
			return TaskHelper.LoadExchangeAssemblyAndReferencesFromSpecificPathForAssemblies(basePath, list.ToArray());
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000238C File Offset: 0x0000058C
		public static Type[] GetAllCmdletTypes(string basePath)
		{
			if (!Directory.Exists(basePath))
			{
				throw new ArgumentException(string.Format("Base path '{0}' doesn't exist, abort the operation.", basePath ?? "null"), "basePath");
			}
			List<Type> list = new List<Type>();
			foreach (Assembly assembly in CmdletAssemblyHelper.GetAllCmdletAssemblies(basePath))
			{
				list.AddRange(from type in CmdletAssemblyHelper.GetAssemblyTypes(assembly)
				where !type.IsAbstract && type.GetCustomAttributes(typeof(CmdletAttribute), false).Any<object>()
				select type);
			}
			return list.ToArray();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002420 File Offset: 0x00000620
		public static string GetScriptForAllCmdlets(string followingPipeline)
		{
			string arg = string.Join(" -or ", from x in CmdletAssemblyHelper.ManagementCmdletAssemblyNames
			select string.Format("$_.DLL -like '*{0}'", x));
			string arg2 = string.IsNullOrEmpty(followingPipeline) ? string.Empty : followingPipeline;
			return string.Format("Get-Command | where {{ ( {0} ) -and  {1} }} {2}", arg, "$_.ModuleName -ne 'Microsoft.Exchange.Management.PowerShell.Setup'", arg2);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000248C File Offset: 0x0000068C
		private static Type[] GetAssemblyTypes(Assembly assembly)
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

		// Token: 0x04000001 RID: 1
		private static readonly string[] _managementCmdletAssemblyNames = new string[]
		{
			"Microsoft.Exchange.Management.dll",
			"Microsoft.Exchange.Management.Recipient.dll",
			"Microsoft.Exchange.Management.Mobility.dll",
			"Microsoft.Exchange.Management.Transport.dll"
		};

		// Token: 0x04000002 RID: 2
		private static readonly string[] _managementCmdletAssemblyNamesWithoutExtension = (from assembly in CmdletAssemblyHelper._managementCmdletAssemblyNames
		select Path.GetFileNameWithoutExtension(assembly)).ToArray<string>();
	}
}
