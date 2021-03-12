using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Threading;

namespace System.Runtime.Remoting
{
	// Token: 0x02000795 RID: 1941
	[ComVisible(true)]
	public class ActivatedServiceTypeEntry : TypeEntry
	{
		// Token: 0x060054DD RID: 21725 RVA: 0x0012C61D File Offset: 0x0012A81D
		public ActivatedServiceTypeEntry(string typeName, string assemblyName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			base.TypeName = typeName;
			base.AssemblyName = assemblyName;
		}

		// Token: 0x060054DE RID: 21726 RVA: 0x0012C650 File Offset: 0x0012A850
		public ActivatedServiceTypeEntry(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"));
			}
			base.TypeName = type.FullName;
			base.AssemblyName = runtimeType.GetRuntimeAssembly().GetSimpleName();
		}

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x060054DF RID: 21727 RVA: 0x0012C6B4 File Offset: 0x0012A8B4
		public Type ObjectType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
				return RuntimeTypeHandle.GetTypeByName(base.TypeName + ", " + base.AssemblyName, ref stackCrawlMark);
			}
		}

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x060054E0 RID: 21728 RVA: 0x0012C6E0 File Offset: 0x0012A8E0
		// (set) Token: 0x060054E1 RID: 21729 RVA: 0x0012C6E8 File Offset: 0x0012A8E8
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

		// Token: 0x060054E2 RID: 21730 RVA: 0x0012C6F1 File Offset: 0x0012A8F1
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"type='",
				base.TypeName,
				", ",
				base.AssemblyName,
				"'"
			});
		}

		// Token: 0x040026C7 RID: 9927
		private IContextAttribute[] _contextAttributes;
	}
}
