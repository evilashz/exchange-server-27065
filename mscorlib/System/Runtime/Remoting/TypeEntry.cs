using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	// Token: 0x02000793 RID: 1939
	[ComVisible(true)]
	public class TypeEntry
	{
		// Token: 0x060054CF RID: 21711 RVA: 0x0012C48A File Offset: 0x0012A68A
		protected TypeEntry()
		{
		}

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x060054D0 RID: 21712 RVA: 0x0012C492 File Offset: 0x0012A692
		// (set) Token: 0x060054D1 RID: 21713 RVA: 0x0012C49A File Offset: 0x0012A69A
		public string TypeName
		{
			get
			{
				return this._typeName;
			}
			set
			{
				this._typeName = value;
			}
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x060054D2 RID: 21714 RVA: 0x0012C4A3 File Offset: 0x0012A6A3
		// (set) Token: 0x060054D3 RID: 21715 RVA: 0x0012C4AB File Offset: 0x0012A6AB
		public string AssemblyName
		{
			get
			{
				return this._assemblyName;
			}
			set
			{
				this._assemblyName = value;
			}
		}

		// Token: 0x060054D4 RID: 21716 RVA: 0x0012C4B4 File Offset: 0x0012A6B4
		internal void CacheRemoteAppEntry(RemoteAppEntry entry)
		{
			this._cachedRemoteAppEntry = entry;
		}

		// Token: 0x060054D5 RID: 21717 RVA: 0x0012C4BD File Offset: 0x0012A6BD
		internal RemoteAppEntry GetRemoteAppEntry()
		{
			return this._cachedRemoteAppEntry;
		}

		// Token: 0x040026C2 RID: 9922
		private string _typeName;

		// Token: 0x040026C3 RID: 9923
		private string _assemblyName;

		// Token: 0x040026C4 RID: 9924
		private RemoteAppEntry _cachedRemoteAppEntry;
	}
}
