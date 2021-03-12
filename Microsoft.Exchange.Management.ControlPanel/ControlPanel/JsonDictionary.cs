using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006AB RID: 1707
	[Serializable]
	public class JsonDictionary<T> : ISerializable
	{
		// Token: 0x060048F2 RID: 18674 RVA: 0x000DF184 File Offset: 0x000DD384
		protected JsonDictionary(SerializationInfo info, StreamingContext context)
		{
			this.rawDictionary = new Dictionary<string, T>(info.MemberCount);
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				try
				{
					this.rawDictionary[enumerator.Name] = (T)((object)enumerator.Value);
				}
				catch (InvalidCastException)
				{
					if (typeof(T).IsArray && enumerator.Value is object[])
					{
						this.rawDictionary[enumerator.Name] = (T)((object)Activator.CreateInstance(typeof(T), new object[]
						{
							((object[])enumerator.Value).Length
						}));
						Array.Copy((object[])enumerator.Value, this.rawDictionary[enumerator.Name] as Array, ((object[])enumerator.Value).Length);
					}
				}
			}
		}

		// Token: 0x060048F3 RID: 18675 RVA: 0x000DF290 File Offset: 0x000DD490
		public JsonDictionary(Dictionary<string, T> dictionary)
		{
			this.rawDictionary = dictionary;
		}

		// Token: 0x170027C5 RID: 10181
		// (get) Token: 0x060048F4 RID: 18676 RVA: 0x000DF29F File Offset: 0x000DD49F
		internal Dictionary<string, T> RawDictionary
		{
			get
			{
				return this.rawDictionary;
			}
		}

		// Token: 0x060048F5 RID: 18677 RVA: 0x000DF2A7 File Offset: 0x000DD4A7
		public static implicit operator JsonDictionary<T>(Dictionary<string, T> dictionary)
		{
			return new JsonDictionary<T>(dictionary);
		}

		// Token: 0x060048F6 RID: 18678 RVA: 0x000DF2AF File Offset: 0x000DD4AF
		public static implicit operator Dictionary<string, T>(JsonDictionary<T> dictionary)
		{
			return dictionary.rawDictionary;
		}

		// Token: 0x060048F7 RID: 18679 RVA: 0x000DF2B8 File Offset: 0x000DD4B8
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			foreach (KeyValuePair<string, T> keyValuePair in this.rawDictionary)
			{
				info.AddValue(keyValuePair.Key, keyValuePair.Value, typeof(T));
			}
		}

		// Token: 0x060048F8 RID: 18680 RVA: 0x000DF328 File Offset: 0x000DD528
		public JsonDictionary<T> Merge(JsonDictionary<T> from)
		{
			if (from != null)
			{
				foreach (KeyValuePair<string, T> keyValuePair in from.rawDictionary)
				{
					this.rawDictionary.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			return this;
		}

		// Token: 0x0400312F RID: 12591
		private Dictionary<string, T> rawDictionary;
	}
}
