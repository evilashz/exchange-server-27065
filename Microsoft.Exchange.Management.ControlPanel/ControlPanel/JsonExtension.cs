using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006AC RID: 1708
	public static class JsonExtension
	{
		// Token: 0x060048F9 RID: 18681 RVA: 0x000DF394 File Offset: 0x000DD594
		public static string ToJsonString(this object dataContract, IEnumerable<Type> types = null)
		{
			DataContractJsonSerializer serializer = new DataContractJsonSerializer(dataContract.GetType(), types, int.MaxValue, false, null, true);
			string text = JsonExtension.ToJsonStringCore(serializer, dataContract);
			if (!string.IsNullOrEmpty(text) && JsonExtension.unsafeCharactersRegex.IsMatch(text))
			{
				text = JsonExtension.unsafeCharactersRegex.Replace(text, new MatchEvaluator(JsonExtension.ReplaceWithEscapedCode));
			}
			return text;
		}

		// Token: 0x060048FA RID: 18682 RVA: 0x000DF3EC File Offset: 0x000DD5EC
		private static string ToJsonStringCore(DataContractJsonSerializer serializer, object dataContract)
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				serializer.WriteObject(memoryStream, dataContract);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				using (StreamReader streamReader = new StreamReader(memoryStream))
				{
					result = streamReader.ReadToEnd();
				}
			}
			return result;
		}

		// Token: 0x060048FB RID: 18683 RVA: 0x000DF454 File Offset: 0x000DD654
		private static string ReplaceWithEscapedCode(Match m)
		{
			string value = m.Value;
			string text = ((int)value[0]).ToString("X");
			return "\\u0000".Substring(0, 6 - text.Length) + text;
		}

		// Token: 0x060048FC RID: 18684 RVA: 0x000DF498 File Offset: 0x000DD698
		public static T JsonDeserialize<T>(this string text, IEnumerable<Type> types = null)
		{
			DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T), types);
			return (T)((object)JsonExtension.JsonDeserializeCore(text, serializer));
		}

		// Token: 0x060048FD RID: 18685 RVA: 0x000DF4C4 File Offset: 0x000DD6C4
		public static object JsonDeserialize(this string text, Type targetType, IEnumerable<Type> types = null)
		{
			DataContractJsonSerializer serializer = new DataContractJsonSerializer(targetType, types);
			return JsonExtension.JsonDeserializeCore(text, serializer);
		}

		// Token: 0x060048FE RID: 18686 RVA: 0x000DF4E0 File Offset: 0x000DD6E0
		private static object JsonDeserializeCore(string text, DataContractJsonSerializer serializer)
		{
			object result;
			using (MemoryStream memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(text)))
			{
				result = serializer.ReadObject(memoryStream);
			}
			return result;
		}

		// Token: 0x04003130 RID: 12592
		private const string UnsafeCharacters = "[\u007f-\u009f­؀-؄܏឴឵‌-‏\u2028-\u202f⁠-⁯﻿￰-￿]";

		// Token: 0x04003131 RID: 12593
		private static Regex unsafeCharactersRegex = new Regex("[\u007f-\u009f­؀-؄܏឴឵‌-‏\u2028-\u202f⁠-⁯﻿￰-￿]", RegexOptions.Compiled);
	}
}
