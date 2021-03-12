using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x02000147 RID: 327
	internal class VariantConfigurationTypeResolver
	{
		// Token: 0x06000F1B RID: 3867 RVA: 0x00025B44 File Offset: 0x00023D44
		internal VariantConfigurationTypeResolver(IDictionary<Type, VariantConfigurationTypeInformation> typeData)
		{
			if (typeData == null)
			{
				throw new ArgumentNullException("typeData");
			}
			this.typeData = typeData;
			this.types = new Dictionary<string, Type>();
			foreach (Type type in this.typeData.Keys)
			{
				this.types.Add(type.Namespace + "." + type.Name, type);
			}
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x00025BD8 File Offset: 0x00023DD8
		public static VariantConfigurationTypeResolver Create(Assembly assembly)
		{
			Assembly[] assemblies = new Assembly[]
			{
				assembly
			};
			return new VariantConfigurationTypeResolver(VariantConfigurationTypeResolver.GetTypeData(assemblies));
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x00025BFD File Offset: 0x00023DFD
		public Type ResolveType(string typeName)
		{
			if (string.IsNullOrEmpty(typeName))
			{
				throw new ArgumentNullException("type");
			}
			if (!this.types.ContainsKey(typeName))
			{
				return null;
			}
			return this.types[typeName];
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x00025C2E File Offset: 0x00023E2E
		public VariantConfigurationTypeInformation GetTypeInformation(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!this.typeData.ContainsKey(type))
			{
				return null;
			}
			return this.typeData[type];
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x00025C60 File Offset: 0x00023E60
		private static IDictionary<Type, VariantConfigurationTypeInformation> GetTypeData(Assembly[] assemblies)
		{
			Dictionary<Type, VariantConfigurationTypeInformation> dictionary = new Dictionary<Type, VariantConfigurationTypeInformation>();
			foreach (Assembly assembly in assemblies)
			{
				foreach (Type type in assembly.GetTypes())
				{
					if (type.IsInterface && type.Name.StartsWith("I"))
					{
						dictionary.Add(type, VariantConfigurationTypeInformation.Create(type));
					}
				}
			}
			return dictionary;
		}

		// Token: 0x04000506 RID: 1286
		private IDictionary<Type, VariantConfigurationTypeInformation> typeData;

		// Token: 0x04000507 RID: 1287
		private IDictionary<string, Type> types;
	}
}
