using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Threading;

namespace System.Runtime.Remoting
{
	// Token: 0x02000794 RID: 1940
	[ComVisible(true)]
	public class ActivatedClientTypeEntry : TypeEntry
	{
		// Token: 0x060054D6 RID: 21718 RVA: 0x0012C4C8 File Offset: 0x0012A6C8
		public ActivatedClientTypeEntry(string typeName, string assemblyName, string appUrl)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			if (appUrl == null)
			{
				throw new ArgumentNullException("appUrl");
			}
			base.TypeName = typeName;
			base.AssemblyName = assemblyName;
			this._appUrl = appUrl;
		}

		// Token: 0x060054D7 RID: 21719 RVA: 0x0012C51C File Offset: 0x0012A71C
		public ActivatedClientTypeEntry(Type type, string appUrl)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (appUrl == null)
			{
				throw new ArgumentNullException("appUrl");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"));
			}
			base.TypeName = type.FullName;
			base.AssemblyName = runtimeType.GetRuntimeAssembly().GetSimpleName();
			this._appUrl = appUrl;
		}

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x060054D8 RID: 21720 RVA: 0x0012C595 File Offset: 0x0012A795
		public string ApplicationUrl
		{
			get
			{
				return this._appUrl;
			}
		}

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x060054D9 RID: 21721 RVA: 0x0012C5A0 File Offset: 0x0012A7A0
		public Type ObjectType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
				return RuntimeTypeHandle.GetTypeByName(base.TypeName + ", " + base.AssemblyName, ref stackCrawlMark);
			}
		}

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x060054DA RID: 21722 RVA: 0x0012C5CC File Offset: 0x0012A7CC
		// (set) Token: 0x060054DB RID: 21723 RVA: 0x0012C5D4 File Offset: 0x0012A7D4
		public IContextAttribute[] ContextAttributes
		{
			get
			{
				return this._contextAttributes;
			}
			set
			{
				this._contextAttributes = value;
			}
		}

		// Token: 0x060054DC RID: 21724 RVA: 0x0012C5DD File Offset: 0x0012A7DD
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"type='",
				base.TypeName,
				", ",
				base.AssemblyName,
				"'; appUrl=",
				this._appUrl
			});
		}

		// Token: 0x040026C5 RID: 9925
		private string _appUrl;

		// Token: 0x040026C6 RID: 9926
		private IContextAttribute[] _contextAttributes;
	}
}
