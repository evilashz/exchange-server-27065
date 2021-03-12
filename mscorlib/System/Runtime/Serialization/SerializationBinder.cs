using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000711 RID: 1809
	[ComVisible(true)]
	[Serializable]
	public abstract class SerializationBinder
	{
		// Token: 0x060050B1 RID: 20657 RVA: 0x0011B39F File Offset: 0x0011959F
		public virtual void BindToName(Type serializedType, out string assemblyName, out string typeName)
		{
			assemblyName = null;
			typeName = null;
		}

		// Token: 0x060050B2 RID: 20658
		public abstract Type BindToType(string assemblyName, string typeName);
	}
}
