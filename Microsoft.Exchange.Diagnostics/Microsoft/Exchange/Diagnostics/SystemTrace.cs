using System;
using System.Diagnostics;
using System.Reflection;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000080 RID: 128
	internal abstract class SystemTrace
	{
		// Token: 0x060002D4 RID: 724 RVA: 0x0000A0E2 File Offset: 0x000082E2
		protected SystemTrace(string assemblyName)
		{
			this.assemblyName = assemblyName;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000A0F1 File Offset: 0x000082F1
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0000A0F9 File Offset: 0x000082F9
		public SourceLevels SourceLevels
		{
			get
			{
				return this.sourceLevels;
			}
			set
			{
				this.sourceLevels = value;
				this.SafeUpdate();
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000A108 File Offset: 0x00008308
		protected static bool GetFieldValue(FieldInfo field)
		{
			return field != null && (bool)field.GetValue(null);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000A121 File Offset: 0x00008321
		protected static void SetFieldValue(FieldInfo field, object value)
		{
			if (field != null)
			{
				field.SetValue(null, value);
			}
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000A134 File Offset: 0x00008334
		protected static T GetPropertyValue<T>(PropertyInfo property)
		{
			if (property != null)
			{
				return (T)((object)property.GetValue(null, null));
			}
			return default(T);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000A161 File Offset: 0x00008361
		protected static void SetPropertyValue(PropertyInfo property, object value)
		{
			if (property != null)
			{
				property.SetValue(null, value, null);
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000A178 File Offset: 0x00008378
		protected static Assembly GetAssembly(string assemblyName)
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			if (assemblies != null)
			{
				foreach (Assembly assembly in assemblies)
				{
					if (SystemTrace.IsMatchingAssemblyName(assembly, assemblyName))
					{
						return assembly;
					}
				}
			}
			return null;
		}

		// Token: 0x060002DC RID: 732
		protected abstract bool Initialize(Assembly assembly);

		// Token: 0x060002DD RID: 733
		protected abstract void Update();

		// Token: 0x060002DE RID: 734 RVA: 0x0000A1BC File Offset: 0x000083BC
		protected void ConnectListener(TraceSource source, TraceListener listener, bool connect)
		{
			if (source != null && listener != null)
			{
				if (connect)
				{
					if (!source.Listeners.Contains(listener))
					{
						source.Listeners.Add(listener);
						return;
					}
				}
				else if (source.Listeners.Contains(listener))
				{
					source.Listeners.Remove(listener);
				}
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000A208 File Offset: 0x00008408
		protected void Initialize()
		{
			Assembly assembly = this.GetAssembly();
			if (assembly != null)
			{
				this.SafeInitialize(assembly);
				return;
			}
			AppDomain.CurrentDomain.AssemblyLoad += this.AssemblyLoadHandler;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000A244 File Offset: 0x00008444
		protected void SafeUpdate()
		{
			if (this.initialized)
			{
				Exception ex = null;
				try
				{
					lock (this)
					{
						this.Update();
					}
				}
				catch (InvalidCastException ex2)
				{
					ex = ex2;
				}
				catch (TargetInvocationException ex3)
				{
					ex = ex3;
				}
				if (ex != null)
				{
					this.ReportFailure(ex);
				}
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000A2BC File Offset: 0x000084BC
		private static bool IsMatchingAssemblyName(Assembly assembly, string assemblyName)
		{
			AssemblyName name = assembly.GetName();
			return name != null && StringComparer.OrdinalIgnoreCase.Equals(name.Name, assemblyName);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000A2E9 File Offset: 0x000084E9
		private void AssemblyLoadHandler(object sender, AssemblyLoadEventArgs args)
		{
			if (SystemTrace.IsMatchingAssemblyName(args.LoadedAssembly, this.assemblyName))
			{
				AppDomain.CurrentDomain.AssemblyLoad -= this.AssemblyLoadHandler;
				this.SafeInitialize(args.LoadedAssembly);
				this.Update();
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000A326 File Offset: 0x00008526
		private Assembly GetAssembly()
		{
			return SystemTrace.GetAssembly(this.assemblyName);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000A334 File Offset: 0x00008534
		private void SafeInitialize(Assembly assembly)
		{
			Exception ex = null;
			try
			{
				this.initialized = this.Initialize(assembly);
			}
			catch (MemberAccessException ex2)
			{
				ex = ex2;
			}
			catch (ArgumentException ex3)
			{
				ex = ex3;
			}
			catch (TargetParameterCountException ex4)
			{
				ex = ex4;
			}
			catch (NotSupportedException ex5)
			{
				ex = ex5;
			}
			catch (InvalidCastException ex6)
			{
				ex = ex6;
			}
			catch (TargetInvocationException ex7)
			{
				ex = ex7;
			}
			if (ex != null)
			{
				this.ReportFailure(ex);
				this.initialized = false;
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000A3D4 File Offset: 0x000085D4
		private void ReportFailure(Exception exception)
		{
			ExTraceInternal.Trace<string, Exception>(0, TraceType.ErrorTrace, CommonTags.guid, 5, 0L, "Failed to setup trace capture from '{0}' component in the framework due exception: {1}", this.assemblyName, exception);
		}

		// Token: 0x040002A4 RID: 676
		private string assemblyName;

		// Token: 0x040002A5 RID: 677
		private bool initialized;

		// Token: 0x040002A6 RID: 678
		private SourceLevels sourceLevels;
	}
}
