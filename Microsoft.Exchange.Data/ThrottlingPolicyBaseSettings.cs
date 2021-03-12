using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001AC RID: 428
	internal abstract class ThrottlingPolicyBaseSettings
	{
		// Token: 0x06000E04 RID: 3588 RVA: 0x0002D688 File Offset: 0x0002B888
		protected ThrottlingPolicyBaseSettings()
		{
			this.settings = new Dictionary<string, string>();
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0002D69B File Offset: 0x0002B89B
		protected ThrottlingPolicyBaseSettings(string value) : this()
		{
			ThrottlingPolicyBaseSettings.InternalParse(value, this.settings);
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x0002D6B0 File Offset: 0x0002B8B0
		internal T Clone<T>() where T : ThrottlingPolicyBaseSettings, new()
		{
			if (typeof(T) != base.GetType())
			{
				throw new ArgumentException(string.Format("An object of type '{0}' could not be cloned to type '{1}'.", base.GetType(), typeof(T)), "T");
			}
			T t = Activator.CreateInstance<T>();
			t.settings.Clear();
			foreach (KeyValuePair<string, string> keyValuePair in this.settings)
			{
				t.settings.Add(keyValuePair.Key, keyValuePair.Value);
			}
			return t;
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0002D770 File Offset: 0x0002B970
		internal static void InternalParse(string stateToParse, Dictionary<string, string> propertyBag)
		{
			if (!string.IsNullOrEmpty(stateToParse))
			{
				string[] array = stateToParse.Split(new char[]
				{
					'~'
				});
				if (array.Length == 0 || array.Length % 2 != 0)
				{
					throw new FormatException(DataStrings.ThrottlingPolicyStateCorrupted(stateToParse));
				}
				for (int i = 0; i < array.Length; i += 2)
				{
					if (propertyBag.ContainsKey(array[i]))
					{
						throw new FormatException(DataStrings.ThrottlingPolicyStateCorrupted(stateToParse));
					}
					propertyBag[array[i]] = array[i + 1];
				}
			}
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0002D7F0 File Offset: 0x0002B9F0
		internal static Unlimited<uint> ParseValue(string valueToConvert)
		{
			if (valueToConvert.Equals("-1", StringComparison.OrdinalIgnoreCase))
			{
				return Unlimited<uint>.UnlimitedValue;
			}
			Unlimited<uint> result;
			if (!Unlimited<uint>.TryParse(valueToConvert, out result))
			{
				throw new FormatException(DataStrings.ThrottlingPolicyStateCorrupted(valueToConvert));
			}
			return result;
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0002D830 File Offset: 0x0002BA30
		protected Unlimited<uint>? GetValueFromPropertyBag(string key)
		{
			string valueToConvert;
			if (this.settings.TryGetValue(key, out valueToConvert))
			{
				return new Unlimited<uint>?(ThrottlingPolicyBaseSettings.ParseValue(valueToConvert));
			}
			return null;
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x0002D862 File Offset: 0x0002BA62
		protected void SetValueInPropertyBag(string key, Unlimited<uint>? value)
		{
			if (value != null)
			{
				this.settings[key] = value.ToString();
				return;
			}
			this.settings.Remove(key);
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x0002D894 File Offset: 0x0002BA94
		public override string ToString()
		{
			if (this.settings.Count == 0)
			{
				return null;
			}
			bool flag = true;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, string> keyValuePair in this.settings)
			{
				string text = keyValuePair.Value;
				if (text.Equals(Unlimited<uint>.UnlimitedString))
				{
					text = "-1";
				}
				if (flag)
				{
					stringBuilder.AppendFormat("{0}~{1}", keyValuePair.Key, text);
					flag = false;
				}
				else
				{
					stringBuilder.AppendFormat("~{0}~{1}", keyValuePair.Key, text);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400089A RID: 2202
		private const string UnthrottledStringInAD = "-1";

		// Token: 0x0400089B RID: 2203
		private Dictionary<string, string> settings;
	}
}
