using System;
using System.Reflection;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000777 RID: 1911
	internal sealed class SerObjectInfoCache
	{
		// Token: 0x060053CB RID: 21451 RVA: 0x001294E6 File Offset: 0x001276E6
		internal SerObjectInfoCache(string typeName, string assemblyName, bool hasTypeForwardedFrom)
		{
			this.fullTypeName = typeName;
			this.assemblyString = assemblyName;
			this.hasTypeForwardedFrom = hasTypeForwardedFrom;
		}

		// Token: 0x060053CC RID: 21452 RVA: 0x00129504 File Offset: 0x00127704
		internal SerObjectInfoCache(Type type)
		{
			TypeInformation typeInformation = BinaryFormatter.GetTypeInformation(type);
			this.fullTypeName = typeInformation.FullTypeName;
			this.assemblyString = typeInformation.AssemblyString;
			this.hasTypeForwardedFrom = typeInformation.HasTypeForwardedFrom;
		}

		// Token: 0x0400264D RID: 9805
		internal string fullTypeName;

		// Token: 0x0400264E RID: 9806
		internal string assemblyString;

		// Token: 0x0400264F RID: 9807
		internal bool hasTypeForwardedFrom;

		// Token: 0x04002650 RID: 9808
		internal MemberInfo[] memberInfos;

		// Token: 0x04002651 RID: 9809
		internal string[] memberNames;

		// Token: 0x04002652 RID: 9810
		internal Type[] memberTypes;
	}
}
