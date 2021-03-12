using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x02000146 RID: 326
	public class VariantConfigurationTypeInformation
	{
		// Token: 0x06000F11 RID: 3857 RVA: 0x00025978 File Offset: 0x00023B78
		internal VariantConfigurationTypeInformation(Type type, IDictionary<string, Type> properties)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			this.properties = properties;
			this.Type = type;
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x000259B0 File Offset: 0x00023BB0
		// (set) Token: 0x06000F13 RID: 3859 RVA: 0x000259B8 File Offset: 0x00023BB8
		public Type Type { get; private set; }

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x000259C1 File Offset: 0x00023BC1
		public IEnumerable<string> ValidPropertyNames
		{
			get
			{
				return this.properties.Keys;
			}
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x000259CE File Offset: 0x00023BCE
		public static VariantConfigurationTypeInformation Create(Type type)
		{
			return new VariantConfigurationTypeInformation(type, VariantConfigurationTypeInformation.GetProperties(type));
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x000259DC File Offset: 0x00023BDC
		public bool IsValidPropertyName(string propertyName)
		{
			return this.properties.ContainsKey(propertyName);
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x000259EC File Offset: 0x00023BEC
		public bool IsValidPropertyValue(string propertyName, string propertyValue)
		{
			bool result;
			try
			{
				Type type = this.properties[propertyName];
				if (type == typeof(bool))
				{
					bool.Parse(propertyValue);
				}
				else if (type.IsEnum)
				{
					Enum.Parse(type, propertyValue);
				}
				result = true;
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x00025A4C File Offset: 0x00023C4C
		public Type GetParameterType(string parameterName)
		{
			if (!this.IsValidPropertyName(parameterName))
			{
				throw new ArgumentException(string.Format("{0} is not a valid parameter", parameterName));
			}
			return this.properties[parameterName];
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x00025A74 File Offset: 0x00023C74
		public override string ToString()
		{
			return string.Join<KeyValuePair<string, Type>>(" : ", this.properties);
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x00025A88 File Offset: 0x00023C88
		private static IDictionary<string, Type> GetProperties(Type type)
		{
			Dictionary<string, Type> dictionary = new Dictionary<string, Type>();
			List<Type> list = new List<Type>();
			Queue<Type> queue = new Queue<Type>();
			queue.Enqueue(type);
			while (queue.Count > 0)
			{
				Type type2 = queue.Dequeue();
				foreach (Type item in type2.GetInterfaces())
				{
					if (!list.Contains(item) && !queue.Contains(item))
					{
						queue.Enqueue(item);
					}
				}
				foreach (PropertyInfo propertyInfo in type2.GetProperties())
				{
					dictionary[propertyInfo.Name] = propertyInfo.PropertyType;
				}
				list.Add(type2);
			}
			return dictionary;
		}

		// Token: 0x04000504 RID: 1284
		private IDictionary<string, Type> properties;
	}
}
