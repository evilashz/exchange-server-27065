using System;
using System.Reflection;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000754 RID: 1876
	internal sealed class BinaryAssemblyInfo
	{
		// Token: 0x060052A3 RID: 21155 RVA: 0x0012235F File Offset: 0x0012055F
		internal BinaryAssemblyInfo(string assemblyString)
		{
			this.assemblyString = assemblyString;
		}

		// Token: 0x060052A4 RID: 21156 RVA: 0x0012236E File Offset: 0x0012056E
		internal BinaryAssemblyInfo(string assemblyString, Assembly assembly)
		{
			this.assemblyString = assemblyString;
			this.assembly = assembly;
		}

		// Token: 0x060052A5 RID: 21157 RVA: 0x00122384 File Offset: 0x00120584
		internal Assembly GetAssembly()
		{
			if (this.assembly == null)
			{
				this.assembly = FormatterServices.LoadAssemblyFromStringNoThrow(this.assemblyString);
				if (this.assembly == null)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_AssemblyNotFound", new object[]
					{
						this.assemblyString
					}));
				}
			}
			return this.assembly;
		}

		// Token: 0x040024FD RID: 9469
		internal string assemblyString;

		// Token: 0x040024FE RID: 9470
		private Assembly assembly;
	}
}
