using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001D6 RID: 470
	public abstract class AssemblyResolver : MarshalByRefObject
	{
		// Token: 0x06000D20 RID: 3360 RVA: 0x000371B0 File Offset: 0x000353B0
		protected AssemblyResolver()
		{
			this.Name = base.GetType().Name;
			this.ErrorTracer = delegate(string error)
			{
			};
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000D21 RID: 3361 RVA: 0x00037204 File Offset: 0x00035404
		// (remove) Token: 0x06000D22 RID: 3362 RVA: 0x0003723C File Offset: 0x0003543C
		public event Action<object, AssemblyResolver.ResolutionCompletedEventArgs> ResolutionCompleted;

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x00037271 File Offset: 0x00035471
		// (set) Token: 0x06000D24 RID: 3364 RVA: 0x00037279 File Offset: 0x00035479
		public Func<string, bool> FileNameFilter { get; set; }

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x00037282 File Offset: 0x00035482
		// (set) Token: 0x06000D26 RID: 3366 RVA: 0x0003728A File Offset: 0x0003548A
		public Action<string> ErrorTracer { get; set; }

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x00037293 File Offset: 0x00035493
		// (set) Token: 0x06000D28 RID: 3368 RVA: 0x0003729B File Offset: 0x0003549B
		public string Name { get; set; }

		// Token: 0x06000D29 RID: 3369 RVA: 0x000372A4 File Offset: 0x000354A4
		public static AssemblyResolver[] Install(params AssemblyResolver[] resolvers)
		{
			foreach (AssemblyResolver assemblyResolver in resolvers)
			{
				assemblyResolver.Install();
			}
			return resolvers;
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x000372CC File Offset: 0x000354CC
		public static void Uninstall(params AssemblyResolver[] resolvers)
		{
			foreach (AssemblyResolver assemblyResolver in resolvers)
			{
				assemblyResolver.Uninstall();
			}
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x000372F3 File Offset: 0x000354F3
		public static bool ExchangePrefixedAssembliesOnly(string fileName)
		{
			return fileName.StartsWith("Microsoft.Exchange.", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x00037301 File Offset: 0x00035501
		public void Install()
		{
			AppDomain.CurrentDomain.AssemblyResolve += this.CurrentDomain_AssemblyResolve;
			AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += this.CurrentDomain_ReflectionOnlyAssemblyResolve;
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0003732F File Offset: 0x0003552F
		public void Uninstall()
		{
			AppDomain.CurrentDomain.AssemblyResolve -= this.CurrentDomain_AssemblyResolve;
			AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= this.CurrentDomain_ReflectionOnlyAssemblyResolve;
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x00037388 File Offset: 0x00035588
		internal string TestTryResolve(AssemblyName nameToResolve, out int filePathsTried, out int assembliesTried)
		{
			int localFilePathsTried = 0;
			int localAssembliesTried = 0;
			Assembly assembly = this.TryResolveAssembly(nameToResolve, new Func<string, Assembly>(Assembly.LoadFile), delegate(string filePath)
			{
				localFilePathsTried++;
			}, delegate(string filePath)
			{
				localAssembliesTried++;
			});
			filePathsTried = localFilePathsTried;
			assembliesTried = localAssembliesTried;
			if (!(assembly != null))
			{
				return null;
			}
			return assembly.Location;
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x000373F8 File Offset: 0x000355F8
		protected internal static bool CanLoadAssembly(AssemblyName reference, AssemblyName definition)
		{
			AssemblyResolver.VerifyNameOfARealAssembly(definition);
			return definition != null && AssemblyResolver.NameEqualityComparer.CompareNames(reference, definition) && (reference.GetPublicKeyToken() == null || AssemblyResolver.NameEqualityComparer.ComparePublicKeyTokens(reference, definition)) && (reference.CultureInfo == null || AssemblyResolver.NameEqualityComparer.IsCompatibleCulture(reference, definition, true)) && (reference.Version == null || AssemblyResolver.NameEqualityComparer.CompareVersions(reference, definition));
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x00037453 File Offset: 0x00035653
		protected static string GetAssemblyFileNameFromFullName(AssemblyName fullName)
		{
			return fullName.Name;
		}

		// Token: 0x06000D31 RID: 3377
		protected abstract IEnumerable<string> GetCandidateAssemblyPaths(AssemblyName assemblyFullName);

		// Token: 0x06000D32 RID: 3378 RVA: 0x00037644 File Offset: 0x00035844
		protected IEnumerable<string> FilterDirectoryPaths(IEnumerable<string> paths)
		{
			if (paths != null)
			{
				foreach (string path in paths)
				{
					if (!string.IsNullOrEmpty(path))
					{
						if (!Directory.Exists(path))
						{
							this.ErrorTracer(string.Format("{0}: assembly lookup path {1} does not exist", this.Name, path));
						}
						else
						{
							yield return path;
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x000379D4 File Offset: 0x00035BD4
		protected IEnumerable<string> FindAssembly(string directoryPath, string fileName, bool recurse)
		{
			yield return Path.Combine(directoryPath, fileName + ".dll");
			yield return Path.Combine(directoryPath, fileName + ".exe");
			if (recurse)
			{
				string[] subdirs = null;
				try
				{
					subdirs = Directory.GetDirectories(directoryPath);
				}
				catch (DirectoryNotFoundException)
				{
				}
				catch (UnauthorizedAccessException arg)
				{
					this.ErrorTracer(string.Format("{0}: swallowing exception {1} inside {2}::FindAssembly", this.Name, arg, base.GetType().Name));
				}
				foreach (string path in (subdirs ?? new string[0]).SelectMany((string subdir) => this.FindAssembly(subdir, fileName, recurse)))
				{
					yield return path;
				}
			}
			yield break;
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00037A06 File Offset: 0x00035C06
		private static bool AlwaysTrue(string v)
		{
			return true;
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x00037A20 File Offset: 0x00035C20
		private static IEnumerable<X> InvokeObserver<X>(IEnumerable<X> input, Action<X> observer)
		{
			if (observer == null)
			{
				return input;
			}
			return input.Select(delegate(X x)
			{
				observer(x);
				return x;
			});
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x00037A56 File Offset: 0x00035C56
		private static void VerifyNameOfARealAssembly(AssemblyName name)
		{
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x00037A58 File Offset: 0x00035C58
		private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			return this.TryHandleAssemblyResolution(args, false);
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x00037A62 File Offset: 0x00035C62
		private Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
		{
			return this.TryHandleAssemblyResolution(args, true);
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00037A6C File Offset: 0x00035C6C
		private Assembly TryHandleAssemblyResolution(ResolveEventArgs args, bool reflectionOnlyContext)
		{
			if (Thread.GetData(this.inThisResolver) == null)
			{
				Assembly assembly = null;
				try
				{
					Thread.SetData(this.inThisResolver, true);
					assembly = this.TryResolveAssembly(new AssemblyName(args.Name), reflectionOnlyContext ? new Func<string, Assembly>(Assembly.ReflectionOnlyLoadFrom) : new Func<string, Assembly>(Assembly.LoadFile), null, null);
				}
				finally
				{
					Thread.SetData(this.inThisResolver, null);
					Action<object, AssemblyResolver.ResolutionCompletedEventArgs> resolutionCompleted = this.ResolutionCompleted;
					if (resolutionCompleted != null)
					{
						resolutionCompleted(null, new AssemblyResolver.ResolutionCompletedEventArgs(assembly, args, reflectionOnlyContext));
					}
				}
				return assembly;
			}
			return null;
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x00037B08 File Offset: 0x00035D08
		private T TryDoAssemblyOperation<T>(string arg, Func<string, T> operation)
		{
			Exception arg2;
			try
			{
				return operation(arg);
			}
			catch (ArgumentException ex)
			{
				arg2 = ex;
			}
			catch (SecurityException ex2)
			{
				arg2 = ex2;
			}
			catch (PathTooLongException ex3)
			{
				arg2 = ex3;
			}
			catch (FileLoadException ex4)
			{
				arg2 = ex4;
			}
			catch (BadImageFormatException ex5)
			{
				arg2 = ex5;
			}
			catch (UnauthorizedAccessException ex6)
			{
				arg2 = ex6;
			}
			this.ErrorTracer(string.Format("{0}: swallowing exception {1} inside Task::AssemblyResolveEventHandler", this.Name, arg2));
			return default(T);
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00037BBC File Offset: 0x00035DBC
		private AssemblyName TryGetAssemblyName(string filePath)
		{
			return this.TryDoAssemblyOperation<AssemblyName>(filePath, new Func<string, AssemblyName>(AssemblyName.GetAssemblyName));
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x00037C08 File Offset: 0x00035E08
		private Assembly TryResolveAssembly(AssemblyName nameToResolve, Func<string, Assembly> loader, Action<string> candidatePathObserver, Action<string> testedAssemblyObserver)
		{
			string assemblyFileNameFromFullName = AssemblyResolver.GetAssemblyFileNameFromFullName(nameToResolve);
			if ((this.FileNameFilter ?? new Func<string, bool>(AssemblyResolver.AlwaysTrue))(assemblyFileNameFromFullName))
			{
				IEnumerable<string> input = AssemblyResolver.InvokeObserver<string>(this.GetCandidateAssemblyPaths(nameToResolve), candidatePathObserver).Where(new Func<string, bool>(File.Exists));
				return (from filePath in AssemblyResolver.InvokeObserver<string>(input, testedAssemblyObserver)
				where AssemblyResolver.CanLoadAssembly(nameToResolve, this.TryGetAssemblyName(filePath))
				select filePath into assemblyFilePath
				select this.TryDoAssemblyOperation<Assembly>(assemblyFilePath, loader)).FirstOrDefault<Assembly>();
			}
			return null;
		}

		// Token: 0x040009B4 RID: 2484
		private LocalDataStoreSlot inThisResolver = Thread.AllocateDataSlot();

		// Token: 0x020001D7 RID: 471
		public class ResolutionCompletedEventArgs : EventArgs
		{
			// Token: 0x06000D3E RID: 3390 RVA: 0x00037CC5 File Offset: 0x00035EC5
			public ResolutionCompletedEventArgs(Assembly assembly, ResolveEventArgs resolutionEventArgs, bool reflectionOnlyContext)
			{
				this.Assembly = assembly;
				this.ResolutionEventArgs = resolutionEventArgs;
				this.ReflectionOnlyContext = reflectionOnlyContext;
			}

			// Token: 0x170002B9 RID: 697
			// (get) Token: 0x06000D3F RID: 3391 RVA: 0x00037CE2 File Offset: 0x00035EE2
			// (set) Token: 0x06000D40 RID: 3392 RVA: 0x00037CEA File Offset: 0x00035EEA
			public Assembly Assembly { get; private set; }

			// Token: 0x170002BA RID: 698
			// (get) Token: 0x06000D41 RID: 3393 RVA: 0x00037CF3 File Offset: 0x00035EF3
			// (set) Token: 0x06000D42 RID: 3394 RVA: 0x00037CFB File Offset: 0x00035EFB
			public ResolveEventArgs ResolutionEventArgs { get; private set; }

			// Token: 0x170002BB RID: 699
			// (get) Token: 0x06000D43 RID: 3395 RVA: 0x00037D04 File Offset: 0x00035F04
			// (set) Token: 0x06000D44 RID: 3396 RVA: 0x00037D0C File Offset: 0x00035F0C
			public bool ReflectionOnlyContext { get; private set; }

			// Token: 0x06000D45 RID: 3397 RVA: 0x00037D18 File Offset: 0x00035F18
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.ResolutionEventArgs.Name);
				PropertyInfo property = this.ResolutionEventArgs.GetType().GetProperty("RequestingAssembly");
				Assembly assembly = null;
				if (property != null)
				{
					assembly = (Assembly)property.GetValue(this.ResolutionEventArgs, null);
				}
				if (assembly != null)
				{
					stringBuilder.AppendLine();
					stringBuilder.AppendFormat("\t(by {0})", assembly);
				}
				if (this.ReflectionOnlyContext)
				{
					stringBuilder.Append(" for reflection only");
				}
				stringBuilder.AppendLine();
				stringBuilder.Append("\t=> ");
				stringBuilder.Append((this.Assembly != null) ? this.Assembly.Location : "FAILED");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x020001D8 RID: 472
		protected internal class NameEqualityComparer : IEqualityComparer<AssemblyName>
		{
			// Token: 0x06000D46 RID: 3398 RVA: 0x00037DE2 File Offset: 0x00035FE2
			private NameEqualityComparer(bool honorPublicKeyToken, bool honorCultureInfo, bool honorVersion)
			{
				this.honorPublicKeyToken = honorPublicKeyToken;
				this.honorCultureInfo = honorCultureInfo;
				this.honorVersion = honorVersion;
			}

			// Token: 0x06000D47 RID: 3399 RVA: 0x00037DFF File Offset: 0x00035FFF
			public static bool CompareNames(AssemblyName x, AssemblyName y)
			{
				return string.Equals(x.Name, y.Name, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x06000D48 RID: 3400 RVA: 0x00037E13 File Offset: 0x00036013
			public static bool CompareVersions(AssemblyName x, AssemblyName y)
			{
				return object.Equals(x.Version, y.Version);
			}

			// Token: 0x06000D49 RID: 3401 RVA: 0x00037E26 File Offset: 0x00036026
			public static bool CompareCulture(AssemblyName x, AssemblyName y)
			{
				return AssemblyResolver.NameEqualityComparer.IsCompatibleCulture(x, y, false);
			}

			// Token: 0x06000D4A RID: 3402 RVA: 0x00037E30 File Offset: 0x00036030
			public static bool IsCompatibleCulture(AssemblyName requested, AssemblyName actual, bool fallbackToParent)
			{
				CultureInfo cultureInfo = requested.CultureInfo;
				while (!object.Equals(cultureInfo, actual.CultureInfo))
				{
					if (!fallbackToParent || cultureInfo == cultureInfo.Parent)
					{
						return false;
					}
					cultureInfo = cultureInfo.Parent;
				}
				return true;
			}

			// Token: 0x06000D4B RID: 3403 RVA: 0x00037E6C File Offset: 0x0003606C
			public static bool ComparePublicKeyTokens(AssemblyName x, AssemblyName y)
			{
				byte[] publicKeyToken = x.GetPublicKeyToken();
				byte[] publicKeyToken2 = y.GetPublicKeyToken();
				return publicKeyToken == publicKeyToken2 || (publicKeyToken != null && publicKeyToken2 != null && publicKeyToken.Length == publicKeyToken2.Length && (publicKeyToken.Length == 0 || (publicKeyToken.Length == 8 && BitConverter.ToUInt64(publicKeyToken, 0) == BitConverter.ToUInt64(publicKeyToken2, 0))));
			}

			// Token: 0x06000D4C RID: 3404 RVA: 0x00037EBC File Offset: 0x000360BC
			public bool Equals(AssemblyName x, AssemblyName y)
			{
				return x == y || (x != null && y != null && AssemblyResolver.NameEqualityComparer.CompareNames(x, y) && (!this.honorPublicKeyToken || AssemblyResolver.NameEqualityComparer.ComparePublicKeyTokens(x, y)) && (!this.honorCultureInfo || AssemblyResolver.NameEqualityComparer.CompareCulture(x, y)) && (!this.honorVersion || AssemblyResolver.NameEqualityComparer.CompareVersions(x, y)));
			}

			// Token: 0x06000D4D RID: 3405 RVA: 0x00037F13 File Offset: 0x00036113
			public int GetHashCode(AssemblyName obj)
			{
				if (obj != null)
				{
					return obj.Name.GetHashCode();
				}
				return 0;
			}

			// Token: 0x040009BD RID: 2493
			public static readonly AssemblyResolver.NameEqualityComparer Full = new AssemblyResolver.NameEqualityComparer(true, true, true);

			// Token: 0x040009BE RID: 2494
			public static readonly AssemblyResolver.NameEqualityComparer Identity = new AssemblyResolver.NameEqualityComparer(true, true, false);

			// Token: 0x040009BF RID: 2495
			public static readonly AssemblyResolver.NameEqualityComparer Partial = new AssemblyResolver.NameEqualityComparer(false, false, false);

			// Token: 0x040009C0 RID: 2496
			private readonly bool honorPublicKeyToken;

			// Token: 0x040009C1 RID: 2497
			private readonly bool honorCultureInfo;

			// Token: 0x040009C2 RID: 2498
			private readonly bool honorVersion;
		}
	}
}
