using System;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009CB RID: 2507
	[ComVisible(false)]
	public class NamespaceResolveEventArgs : EventArgs
	{
		// Token: 0x17001142 RID: 4418
		// (get) Token: 0x060063C9 RID: 25545 RVA: 0x00153120 File Offset: 0x00151320
		public string NamespaceName
		{
			get
			{
				return this._NamespaceName;
			}
		}

		// Token: 0x17001143 RID: 4419
		// (get) Token: 0x060063CA RID: 25546 RVA: 0x00153128 File Offset: 0x00151328
		public Assembly RequestingAssembly
		{
			get
			{
				return this._RequestingAssembly;
			}
		}

		// Token: 0x17001144 RID: 4420
		// (get) Token: 0x060063CB RID: 25547 RVA: 0x00153130 File Offset: 0x00151330
		public Collection<Assembly> ResolvedAssemblies
		{
			get
			{
				return this._ResolvedAssemblies;
			}
		}

		// Token: 0x060063CC RID: 25548 RVA: 0x00153138 File Offset: 0x00151338
		public NamespaceResolveEventArgs(string namespaceName, Assembly requestingAssembly)
		{
			this._NamespaceName = namespaceName;
			this._RequestingAssembly = requestingAssembly;
			this._ResolvedAssemblies = new Collection<Assembly>();
		}

		// Token: 0x04002C71 RID: 11377
		private string _NamespaceName;

		// Token: 0x04002C72 RID: 11378
		private Assembly _RequestingAssembly;

		// Token: 0x04002C73 RID: 11379
		private Collection<Assembly> _ResolvedAssemblies;
	}
}
