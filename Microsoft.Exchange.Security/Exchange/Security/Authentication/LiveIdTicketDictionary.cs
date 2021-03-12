using System;
using System.Collections;
using System.Text;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x0200007B RID: 123
	internal class LiveIdTicketDictionary
	{
		// Token: 0x06000456 RID: 1110 RVA: 0x00023C10 File Offset: 0x00021E10
		public LiveIdTicketDictionary(string ticket)
		{
			if (ticket == null)
			{
				throw new ArgumentNullException("ticket");
			}
			string[] array = ticket.Split(new char[]
			{
				'&'
			});
			this.values = new Hashtable(array.Length);
			int i = 0;
			while (i < array.Length - 3)
			{
				string key = array[i++];
				string text = array[i++];
				string text2 = LiveIdTicketDictionary.ReadEscapedString(array, ref i);
				string a;
				if ((a = text) != null)
				{
					if (!(a == "string"))
					{
						uint num2;
						if (!(a == "UInt32"))
						{
							long num;
							if (!(a == "Int64"))
							{
								byte b;
								if (!(a == "Byte"))
								{
									if (a == "DateTime")
									{
										DateTime dateTime;
										if (DateTime.TryParse(text2, out dateTime))
										{
											this.values[key] = dateTime;
										}
									}
								}
								else if (byte.TryParse(text2, out b))
								{
									this.values[key] = b;
								}
							}
							else if (long.TryParse(text2, out num))
							{
								this.values[key] = num;
							}
						}
						else if (uint.TryParse(text2, out num2))
						{
							this.values[key] = num2;
						}
					}
					else
					{
						this.values[key] = text2;
					}
				}
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00023D64 File Offset: 0x00021F64
		public bool TryGetValue(string key, out object value)
		{
			value = null;
			if (this.values.ContainsKey(key))
			{
				value = this.values[key];
				return true;
			}
			return false;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00023D88 File Offset: 0x00021F88
		public bool TryGetValue<T>(string key, out T value)
		{
			value = default(T);
			if (this.values.ContainsKey(key))
			{
				object obj = this.values[key];
				if (obj.GetType() == typeof(T))
				{
					value = (T)((object)obj);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00023DE0 File Offset: 0x00021FE0
		private static string ReadEscapedString(string[] splits, ref int index)
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (index < splits.Length)
			{
				stringBuilder.Append(splits[index]);
				index++;
				if (index >= splits.Length - 1 || splits[index].Length != 0)
				{
					break;
				}
				stringBuilder.Append('&');
				index++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040004A1 RID: 1185
		private readonly Hashtable values;
	}
}
