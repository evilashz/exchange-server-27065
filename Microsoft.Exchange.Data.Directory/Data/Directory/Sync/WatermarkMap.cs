using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020007CB RID: 1995
	internal class WatermarkMap : Dictionary<Guid, long>
	{
		// Token: 0x1700234A RID: 9034
		// (get) Token: 0x06006342 RID: 25410 RVA: 0x00158844 File Offset: 0x00156A44
		public static WatermarkMap Empty
		{
			get
			{
				return new WatermarkMap();
			}
		}

		// Token: 0x06006343 RID: 25411 RVA: 0x0015884C File Offset: 0x00156A4C
		internal static WatermarkMap Parse(string rawstring)
		{
			WatermarkMap empty = WatermarkMap.Empty;
			string[] array = rawstring.Split(new string[]
			{
				";"
			}, StringSplitOptions.RemoveEmptyEntries);
			foreach (string text in array)
			{
				string[] array3 = text.Split(new string[]
				{
					":"
				}, StringSplitOptions.None);
				if (array3.Length != 2)
				{
					throw new FormatException();
				}
				Guid key = new Guid(array3[0]);
				long value = long.Parse(array3[1]);
				if (!empty.ContainsKey(key))
				{
					empty.Add(key, value);
				}
			}
			return empty;
		}

		// Token: 0x06006344 RID: 25412 RVA: 0x001588E8 File Offset: 0x00156AE8
		internal static WatermarkMap Parse(byte[] rawBytes)
		{
			string @string = Encoding.UTF8.GetString(rawBytes);
			return WatermarkMap.Parse(@string);
		}

		// Token: 0x06006345 RID: 25413 RVA: 0x00158908 File Offset: 0x00156B08
		internal string SerializeToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.Count * 56);
			foreach (KeyValuePair<Guid, long> keyValuePair in this)
			{
				stringBuilder.AppendFormat("{0}:{1};", keyValuePair.Key, keyValuePair.Value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006346 RID: 25414 RVA: 0x00158988 File Offset: 0x00156B88
		internal byte[] SerializeToBytes()
		{
			string s = this.SerializeToString();
			return Encoding.UTF8.GetBytes(s);
		}

		// Token: 0x06006347 RID: 25415 RVA: 0x001589A8 File Offset: 0x00156BA8
		internal bool ContainsAllChanges(WatermarkMap source)
		{
			foreach (KeyValuePair<Guid, long> keyValuePair in source)
			{
				long num;
				if (!base.TryGetValue(keyValuePair.Key, out num))
				{
					return false;
				}
				if (num < keyValuePair.Value)
				{
					return false;
				}
			}
			return true;
		}
	}
}
