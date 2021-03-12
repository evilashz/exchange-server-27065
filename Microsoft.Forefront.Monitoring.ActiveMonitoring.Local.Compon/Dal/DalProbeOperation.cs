using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Dal
{
	// Token: 0x02000057 RID: 87
	public abstract class DalProbeOperation
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000EC9C File Offset: 0x0000CE9C
		// (set) Token: 0x06000244 RID: 580 RVA: 0x0000ECA4 File Offset: 0x0000CEA4
		[XmlAttribute]
		public string Return { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000ECB0 File Offset: 0x0000CEB0
		private static Assembly[] SafeAssemblies
		{
			get
			{
				if (DalProbeOperation.safeAssemblies == null)
				{
					Assembly[] array = new Assembly[DalProbeOperation.safeAssemblyNames.Length];
					int num = 0;
					foreach (string assemblyString in DalProbeOperation.safeAssemblyNames)
					{
						array[num++] = Assembly.Load(assemblyString);
					}
					DalProbeOperation.safeAssemblies = array;
				}
				return DalProbeOperation.safeAssemblies;
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000ED3C File Offset: 0x0000CF3C
		public static Type ResolveDataType(string typeName)
		{
			Type type2 = DalProbeOperation.safeTypes.FirstOrDefault((Type safeType) => safeType.FullName == typeName);
			if (type2 != null)
			{
				return type2;
			}
			type2 = (from assembly in DalProbeOperation.SafeAssemblies
			select assembly.GetType(typeName)).FirstOrDefault((Type type) => type != null);
			if (type2 != null)
			{
				return type2;
			}
			throw new TypeLoadException(string.Format("Unable to load type {0}. It must be a public or internal type defined in {1}. Add the assembly containing the type to safeAssemblyNames in DalProbeOperation class", typeName, string.Join(", ", DalProbeOperation.safeAssemblyNames)));
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000EDE0 File Offset: 0x0000CFE0
		public static bool IsVariable(string name)
		{
			return name.StartsWith("$");
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000EDF0 File Offset: 0x0000CFF0
		public static object GetValue(string str, IDictionary<string, object> variables)
		{
			if (!DalProbeOperation.IsVariable(str))
			{
				return str;
			}
			string[] array = str.Split(new char[]
			{
				'.'
			});
			object propertyValue;
			if (!variables.TryGetValue(array[0], out propertyValue))
			{
				return null;
			}
			propertyValue = DalProbeOperation.GetPropertyValue(propertyValue, array, 1);
			return propertyValue;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000EE34 File Offset: 0x0000D034
		public static object GetPropertyValue(object value, string[] parts, int index = 0)
		{
			for (int i = index; i < parts.Length; i++)
			{
				if (value == null)
				{
					throw new NullReferenceException(string.Format("{0} is null and hence could not evaluate {1}", string.Join(".", parts, 0, i), string.Join(".", parts, i, parts.Length - i)));
				}
				value = value.GetType().InvokeMember(parts[i], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.GetProperty, null, value, null);
			}
			return value;
		}

		// Token: 0x0600024A RID: 586
		public abstract void Execute(IDictionary<string, object> variables);

		// Token: 0x04000161 RID: 353
		private static readonly string[] safeAssemblyNames = new string[]
		{
			"Microsoft.Exchange.Hygiene.Data",
			"Microsoft.Exchange.Data.Directory",
			"Microsoft.Exchange.Data"
		};

		// Token: 0x04000162 RID: 354
		private static readonly Type[] safeTypes = new Type[]
		{
			typeof(byte),
			typeof(sbyte),
			typeof(short),
			typeof(ushort),
			typeof(int),
			typeof(uint),
			typeof(long),
			typeof(ulong),
			typeof(bool),
			typeof(float),
			typeof(double),
			typeof(decimal),
			typeof(DateTime),
			typeof(TimeSpan),
			typeof(Guid)
		};

		// Token: 0x04000163 RID: 355
		private static Assembly[] safeAssemblies;
	}
}
