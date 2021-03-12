using System;
using System.Collections.Generic;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000002 RID: 2
	public class AttributeHelper
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public AttributeHelper(WorkDefinition workDefinition)
		{
			string arg = Utilities.NormalizeStringToValidFileOrRegistryKeyName(workDefinition.GetType().Name);
			string arg2 = Utilities.NormalizeStringToValidFileOrRegistryKeyName(workDefinition.Name);
			this.subkeyName = string.Format("{0}\\{1}", arg, arg2);
			string propertyAsString = RegistryHelper.GetPropertyAsString("IsOverrideEnabled", this.subkeyName, null, false);
			if (!string.IsNullOrEmpty(propertyAsString))
			{
				bool.TryParse(propertyAsString, out this.isRegitryOverrideEnabled);
			}
			this.attributes = workDefinition.Attributes;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002148 File Offset: 0x00000348
		public static string StringArrayToString(string[] array)
		{
			string result = null;
			if (array != null)
			{
				result = string.Join(';'.ToString(), array);
			}
			return result;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000216C File Offset: 0x0000036C
		public static T GetAttribute<T>(Dictionary<string, string> attributes, string attributeName, bool isMandatory, T defaultValue, Func<string, T> parseMethod)
		{
			T result = defaultValue;
			string arg;
			if (!string.IsNullOrEmpty(attributeName) && attributes != null && attributes.TryGetValue(attributeName, out arg))
			{
				result = parseMethod(arg);
			}
			else if (isMandatory)
			{
				throw new InvalidOperationException(string.Format("{0} is a mandatory parameter.", attributeName));
			}
			return result;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000021B4 File Offset: 0x000003B4
		public string GetString(string attributeName, bool isMandatory, string defaultValue)
		{
			return this.GetAttribute<string>(attributeName, isMandatory, defaultValue, (string s) => s);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021DC File Offset: 0x000003DC
		public string[] GetStrings(string attributeName, bool isMandatory, string[] defaultValue = null, char separatorChar = ';', bool isTrim = true)
		{
			string[] result = defaultValue;
			string @string = this.GetString(attributeName, isMandatory, null);
			if (!string.IsNullOrWhiteSpace(@string))
			{
				List<string> list = new List<string>();
				string[] array = @string.Split(new char[]
				{
					separatorChar
				});
				foreach (string text in array)
				{
					list.Add(text.Trim());
				}
				result = list.ToArray();
			}
			return result;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000224C File Offset: 0x0000044C
		public bool GetBool(string attributeName, bool isMandatory, bool defaultValue)
		{
			return this.GetAttribute<bool>(attributeName, isMandatory, defaultValue, new Func<string, bool>(bool.Parse));
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002264 File Offset: 0x00000464
		public int GetInt(string attributeName, bool isMandatory, int defaultValue, int? minimum = null, int? maximum = null)
		{
			int attribute = this.GetAttribute<int>(attributeName, isMandatory, defaultValue, new Func<string, int>(int.Parse));
			AttributeHelper.RangeCheck<int?>(minimum != null && attribute < minimum, maximum != null && attribute > maximum, attributeName, minimum, maximum, new int?(attribute));
			return attribute;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022E4 File Offset: 0x000004E4
		public double GetDouble(string attributeName, bool isMandatory, double defaultValue, double? minimum = null, double? maximum = null)
		{
			double attribute = this.GetAttribute<double>(attributeName, isMandatory, defaultValue, new Func<string, double>(double.Parse));
			bool minimumCheckFailed;
			if (minimum != null)
			{
				double num = attribute;
				double? num2 = minimum;
				minimumCheckFailed = (num < num2.GetValueOrDefault() && num2 != null);
			}
			else
			{
				minimumCheckFailed = false;
			}
			bool maximumCheckFailed;
			if (maximum != null)
			{
				double num3 = attribute;
				double? num4 = maximum;
				maximumCheckFailed = (num3 > num4.GetValueOrDefault() && num4 != null);
			}
			else
			{
				maximumCheckFailed = false;
			}
			AttributeHelper.RangeCheck<double?>(minimumCheckFailed, maximumCheckFailed, attributeName, minimum, maximum, new double?(attribute));
			return attribute;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002368 File Offset: 0x00000568
		public TimeSpan GetTimeSpan(string attributeName, bool isMandatory, TimeSpan defaultValue, TimeSpan? minimum = null, TimeSpan? maximum = null)
		{
			TimeSpan attribute = this.GetAttribute<TimeSpan>(attributeName, isMandatory, defaultValue, new Func<string, TimeSpan>(TimeSpan.Parse));
			AttributeHelper.RangeCheck<TimeSpan?>(minimum != null && attribute < minimum, maximum != null && attribute > maximum, attributeName, minimum, maximum, new TimeSpan?(attribute));
			return attribute;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002408 File Offset: 0x00000608
		public T GetEnum<T>(string attributeName, bool isMandatory, T defaultValue)
		{
			return this.GetAttribute<T>(attributeName, isMandatory, defaultValue, (string s) => (T)((object)Enum.Parse(typeof(T), s)));
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002420 File Offset: 0x00000620
		public T GetAttribute<T>(string attributeName, bool isMandatory, T defaultValue, Func<string, T> parseMethod)
		{
			T result = defaultValue;
			string text = null;
			if (this.isRegitryOverrideEnabled && !string.IsNullOrEmpty(this.subkeyName))
			{
				text = RegistryHelper.GetPropertyAsString(attributeName, this.subkeyName, null, false);
				if (text != null)
				{
					result = parseMethod(text);
				}
			}
			if (text == null)
			{
				result = AttributeHelper.GetAttribute<T>(this.attributes, attributeName, isMandatory, defaultValue, parseMethod);
			}
			return result;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002478 File Offset: 0x00000678
		internal static void RangeCheck<T>(bool minimumCheckFailed, bool maximumCheckFailed, string attributeName, T minimum, T maximum, T current)
		{
			if (minimumCheckFailed)
			{
				throw new ArgumentOutOfRangeException(string.Format("Minimum allowed value for attribute '{0}' is {1}. The value provided is {2}.", attributeName, minimum, current));
			}
			if (maximumCheckFailed)
			{
				throw new ArgumentOutOfRangeException(string.Format("Maximum allowed value for attribute '{0}' is {1}. The value provided is {2}.", attributeName, maximum, current));
			}
		}

		// Token: 0x04000001 RID: 1
		public const char DefaultStringSeparatorChar = ';';

		// Token: 0x04000002 RID: 2
		private readonly string subkeyName;

		// Token: 0x04000003 RID: 3
		private readonly bool isRegitryOverrideEnabled;

		// Token: 0x04000004 RID: 4
		private Dictionary<string, string> attributes;
	}
}
