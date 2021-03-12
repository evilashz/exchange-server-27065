using System;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000F57 RID: 3927
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class PicwDynamicLoader
	{
		// Token: 0x170023A9 RID: 9129
		// (get) Token: 0x0600869B RID: 34459 RVA: 0x0024EC1C File Offset: 0x0024CE1C
		private static Assembly PicwAssembly
		{
			get
			{
				if (!PicwDynamicLoader.isAssemblyLoaded)
				{
					try
					{
						PicwDynamicLoader.assemblyInstance = Assembly.Load("Microsoft.Exchange.Inference.PeopleICommunicateWith");
					}
					catch (FileNotFoundException)
					{
						PicwDynamicLoader.assemblyInstance = null;
					}
					catch (Exception ex)
					{
						PicwDynamicLoader.assemblyInstance = null;
						PicwDynamicLoader.Tracer.TraceDebug<string>(0L, "Failed to load PICW assembly due to exception: '{0}'", ex.Message);
					}
					PicwDynamicLoader.isAssemblyLoaded = true;
				}
				return PicwDynamicLoader.assemblyInstance;
			}
		}

		// Token: 0x0600869C RID: 34460 RVA: 0x0024EC94 File Offset: 0x0024CE94
		private static Type LoadType(string typeName)
		{
			Type result;
			if (PicwDynamicLoader.PicwAssembly == null)
			{
				result = null;
			}
			else
			{
				try
				{
					result = PicwDynamicLoader.PicwAssembly.GetType(string.Join(".", new string[]
					{
						"Microsoft.Exchange.Inference.PeopleICommunicateWith",
						typeName
					}), true);
				}
				catch (Exception)
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0600869D RID: 34461 RVA: 0x0024ECF4 File Offset: 0x0024CEF4
		public static object CreateInstance(string typeName, params object[] paramList)
		{
			Type type = PicwDynamicLoader.LoadType(typeName);
			object result;
			if (type == null)
			{
				result = null;
			}
			else
			{
				try
				{
					result = Activator.CreateInstance(type, paramList);
				}
				catch (Exception)
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x04005A10 RID: 23056
		private const string AssemblyName = "Microsoft.Exchange.Inference.PeopleICommunicateWith";

		// Token: 0x04005A11 RID: 23057
		private static readonly Trace Tracer = ExTraceGlobals.StorageTracer;

		// Token: 0x04005A12 RID: 23058
		private static bool isAssemblyLoaded = false;

		// Token: 0x04005A13 RID: 23059
		private static Assembly assemblyInstance;
	}
}
