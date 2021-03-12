using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009CD RID: 2509
	[AttributeUsage(AttributeTargets.Field)]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class SeverityLevelAttribute : Attribute
	{
		// Token: 0x06005CA8 RID: 23720 RVA: 0x00182060 File Offset: 0x00180260
		internal SeverityLevelAttribute() : this(SeverityLevel.Information)
		{
		}

		// Token: 0x06005CA9 RID: 23721 RVA: 0x00182069 File Offset: 0x00180269
		internal SeverityLevelAttribute(SeverityLevel level)
		{
			this.severityLevel = level;
		}

		// Token: 0x17001971 RID: 6513
		// (get) Token: 0x06005CAA RID: 23722 RVA: 0x00182078 File Offset: 0x00180278
		internal SeverityLevel SeverityLevel
		{
			get
			{
				return this.severityLevel;
			}
		}

		// Token: 0x06005CAB RID: 23723 RVA: 0x00182080 File Offset: 0x00180280
		public static SeverityLevel FromEnum(Type enumType, object value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.GetTypeInfo().IsEnum)
			{
				throw new ArgumentException("enumType must be an enum.", "enumType");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			object obj = Enum.ToObject(enumType, value);
			string[] array = obj.ToString().Split(new char[]
			{
				','
			});
			if (array.Length != 1)
			{
				throw new ArgumentException("value must be an single enum.", "value");
			}
			if (SeverityLevelAttribute.enum2SeverityLevelMap == null)
			{
				Dictionary<object, SeverityLevel> value2 = new Dictionary<object, SeverityLevel>();
				Interlocked.CompareExchange<Dictionary<object, SeverityLevel>>(ref SeverityLevelAttribute.enum2SeverityLevelMap, value2, null);
			}
			SeverityLevel severityLevel = SeverityLevel.Information;
			lock (SeverityLevelAttribute.enum2SeverityLevelMap)
			{
				if (SeverityLevelAttribute.enum2SeverityLevelMap.TryGetValue(obj, out severityLevel))
				{
					return severityLevel;
				}
				string name = array[0].Trim();
				FieldInfo field = enumType.GetField(name);
				if (null != field)
				{
					object[] customAttributes = field.GetCustomAttributes(typeof(SeverityLevelAttribute), false);
					if (customAttributes != null && customAttributes.Length > 0)
					{
						severityLevel = ((SeverityLevelAttribute)customAttributes[0]).SeverityLevel;
					}
				}
				SeverityLevelAttribute.enum2SeverityLevelMap.Add(obj, severityLevel);
			}
			return severityLevel;
		}

		// Token: 0x040032FC RID: 13052
		private static Dictionary<object, SeverityLevel> enum2SeverityLevelMap;

		// Token: 0x040032FD RID: 13053
		private SeverityLevel severityLevel;
	}
}
