using System;
using System.Collections;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Compliance.Xml;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020002B0 RID: 688
	internal static class SuppressingPiiProperty
	{
		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x060018C5 RID: 6341 RVA: 0x0004E537 File Offset: 0x0004C737
		// (set) Token: 0x060018C6 RID: 6342 RVA: 0x0004E53E File Offset: 0x0004C73E
		public static bool Initialized { get; private set; }

		// Token: 0x060018C7 RID: 6343 RVA: 0x0004E548 File Offset: 0x0004C748
		public static string Initialize(string redactionConfigFile)
		{
			string result;
			using (StreamReader streamReader = new StreamReader(redactionConfigFile))
			{
				result = SuppressingPiiProperty.Initialize(streamReader);
			}
			return result;
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x0004E580 File Offset: 0x0004C780
		public static string Initialize(TextReader reader)
		{
			if (!SuppressingPiiProperty.Initialized)
			{
				lock (SuppressingPiiProperty.syncObject)
				{
					if (!SuppressingPiiProperty.Initialized)
					{
						SuppressingPiiProperty.Initialized = true;
						SafeXmlSerializer safeXmlSerializer = new SafeXmlSerializer(typeof(SuppressingPiiConfig));
						SuppressingPiiProperty.piiDataRedaction = (SuppressingPiiConfig)safeXmlSerializer.Deserialize(reader);
					}
				}
			}
			if (SuppressingPiiProperty.piiDataRedaction != null)
			{
				return SuppressingPiiProperty.piiDataRedaction.DeserializationError;
			}
			return null;
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x0004E604 File Offset: 0x0004C804
		public static bool IsExcludedSchemaType(Type type)
		{
			return SuppressingPiiProperty.piiDataRedaction != null && SuppressingPiiProperty.piiDataRedaction.ExceptionSchemaTypes != null && SuppressingPiiProperty.piiDataRedaction.ExceptionSchemaTypes.Contains(type);
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x0004E62B File Offset: 0x0004C82B
		public static object TryRedact(PropertyDefinition property, object original)
		{
			return SuppressingPiiProperty.TryRedact(property, original, null);
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x0004E638 File Offset: 0x0004C838
		public static object TryRedact(PropertyDefinition property, object original, PiiMap piiMap)
		{
			if (original == null)
			{
				return original;
			}
			if (!SuppressingPiiProperty.Initialized || SuppressingPiiProperty.piiDataRedaction == null)
			{
				Type type = original.GetType();
				if (type.IsValueType)
				{
					return Activator.CreateInstance(type);
				}
				return null;
			}
			else
			{
				if (!SuppressingPiiProperty.piiDataRedaction.Enable)
				{
					return original;
				}
				object result = original;
				MethodInfo redactor;
				if (SuppressingPiiProperty.piiDataRedaction.TryGetRedactor(property, out redactor))
				{
					if (piiMap != null && !SuppressingPiiProperty.piiDataRedaction.NeedAddIntoPiiMap(property, original))
					{
						piiMap = null;
					}
					if (property.Type.IsArray)
					{
						result = SuppressingPiiProperty.RedactArray(original, redactor, piiMap);
					}
					else
					{
						result = SuppressingPiiProperty.RedactSingleOrListValue(original, redactor, piiMap);
					}
				}
				return result;
			}
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x0004E6C5 File Offset: 0x0004C8C5
		public static T TryRedactValue<T>(PropertyDefinition property, T original)
		{
			return (T)((object)SuppressingPiiProperty.TryRedact(property, original, null));
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x0004E6D9 File Offset: 0x0004C8D9
		public static int[] GetPiiStringParams(string fullName)
		{
			if (SuppressingPiiProperty.piiDataRedaction != null)
			{
				return SuppressingPiiProperty.piiDataRedaction.GetPiiStringParams(fullName);
			}
			return null;
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x0004E6F0 File Offset: 0x0004C8F0
		private static object RedactSingleOrListValue(object original, MethodInfo redactor, PiiMap piiMap)
		{
			IList list = original as IList;
			if (list != null)
			{
				bool flag = list is MultiValuedPropertyBase;
				if (list.IsReadOnly)
				{
					if (!flag)
					{
						return null;
					}
					((MultiValuedPropertyBase)list).SetIsReadOnly(false, null);
				}
				for (int i = 0; i < list.Count; i++)
				{
					object value = SuppressingPiiProperty.RedactSingleValue(list[i], redactor, piiMap);
					if (flag && !(original is ProxyAddressCollection))
					{
						list.RemoveAt(i);
						list.Insert(i, value);
					}
					else
					{
						list[i] = value;
					}
				}
			}
			else
			{
				original = SuppressingPiiProperty.RedactSingleValue(original, redactor, piiMap);
			}
			return original;
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x0004E788 File Offset: 0x0004C988
		private static object RedactSingleValue(object original, MethodInfo redactor, PiiMap piiMap)
		{
			object[] array = new object[3];
			array[0] = original;
			object[] array2 = array;
			object result = redactor.Invoke(null, array2);
			string value = (string)array2[1];
			string key = (string)array2[2];
			if (piiMap != null && !string.IsNullOrEmpty(value))
			{
				piiMap[key] = value;
			}
			return result;
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x0004E7D4 File Offset: 0x0004C9D4
		private static object RedactArray(object original, MethodInfo redactor, PiiMap piiMap)
		{
			object[] array = new object[3];
			array[0] = original;
			object[] array2 = array;
			object result = redactor.Invoke(null, array2);
			string[] array3 = (string[])array2[1];
			string[] array4 = (string[])array2[2];
			if (piiMap != null && array3 != null && array4 != null)
			{
				for (int i = 0; i < array3.Length; i++)
				{
					if (!string.IsNullOrEmpty(array3[i]))
					{
						piiMap[array4[i]] = array3[i];
					}
				}
			}
			return result;
		}

		// Token: 0x04000EA7 RID: 3751
		private static SuppressingPiiConfig piiDataRedaction;

		// Token: 0x04000EA8 RID: 3752
		private static object syncObject = new object();
	}
}
