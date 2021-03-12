using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200039F RID: 927
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class Datapoint
	{
		// Token: 0x06001D8A RID: 7562 RVA: 0x00075E56 File Offset: 0x00074056
		public Datapoint(DatapointConsumer consumers, string id, string time, string[] keys, string[] values)
		{
			this.Id = id;
			this.Time = time;
			this.Consumers = consumers;
			this.Keys = keys;
			this.Values = values;
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001D8B RID: 7563 RVA: 0x00075E83 File Offset: 0x00074083
		// (set) Token: 0x06001D8C RID: 7564 RVA: 0x00075E8B File Offset: 0x0007408B
		[DataMember]
		public string Id { get; private set; }

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001D8D RID: 7565 RVA: 0x00075E94 File Offset: 0x00074094
		// (set) Token: 0x06001D8E RID: 7566 RVA: 0x00075E9C File Offset: 0x0007409C
		[DataMember]
		public string Time { get; private set; }

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001D8F RID: 7567 RVA: 0x00075EA5 File Offset: 0x000740A5
		// (set) Token: 0x06001D90 RID: 7568 RVA: 0x00075EAD File Offset: 0x000740AD
		[DataMember]
		public DatapointConsumer Consumers { get; private set; }

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001D91 RID: 7569 RVA: 0x00075EB6 File Offset: 0x000740B6
		// (set) Token: 0x06001D92 RID: 7570 RVA: 0x00075EBE File Offset: 0x000740BE
		[DataMember]
		public string[] Keys { get; private set; }

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001D93 RID: 7571 RVA: 0x00075EC7 File Offset: 0x000740C7
		// (set) Token: 0x06001D94 RID: 7572 RVA: 0x00075ECF File Offset: 0x000740CF
		[DataMember]
		public string[] Values { get; private set; }

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001D95 RID: 7573 RVA: 0x00075EF4 File Offset: 0x000740F4
		public int Size
		{
			get
			{
				if (this.size == null)
				{
					int num;
					if (this.Keys != null)
					{
						num = this.Keys.Sum(delegate(string key)
						{
							if (key != null)
							{
								return key.Length;
							}
							return 0;
						});
					}
					else
					{
						num = 0;
					}
					int num2 = num;
					int num3;
					if (this.Values != null)
					{
						num3 = this.Values.Sum(delegate(string value)
						{
							if (value != null)
							{
								return value.Length;
							}
							return 0;
						});
					}
					else
					{
						num3 = 0;
					}
					int num4 = num3;
					int length = this.Consumers.ToString().Length;
					int num5 = (this.Id == null) ? 0 : this.Id.Length;
					int num6 = (this.Time == null) ? 0 : this.Time.Length;
					this.size = new int?(num5 + num6 + length + num2 + num4);
				}
				return this.size.Value;
			}
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x00075FDE File Offset: 0x000741DE
		public bool IsForConsumer(DatapointConsumer consumer)
		{
			return (this.Consumers & consumer) != DatapointConsumer.None;
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x00075FF0 File Offset: 0x000741F0
		public void AppendTo(StringBuilder buffer)
		{
			buffer.AppendFormat("{0},", this.Time);
			buffer.AppendFormat("{0},", Datapoint.GetCsvEscapedString(this.Id));
			buffer.AppendFormat("{0},", Datapoint.GetCsvEscapedString(this.Consumers));
			if (this.Keys == null && this.Values == null)
			{
				return;
			}
			string[] array = this.Keys ?? Datapoint.EmptyStringArray;
			string[] array2 = this.Values ?? Datapoint.EmptyStringArray;
			KeyValuePair<string, string>[] array3 = new KeyValuePair<string, string>[Math.Max(array.Length, array2.Length)];
			for (int i = 0; i < Math.Min(array.Length, array2.Length); i++)
			{
				array3[i] = Datapoint.NewKeyValuePair(array[i], array2[i]);
			}
			if (array.Length > array2.Length)
			{
				for (int j = array3.Length; j < array.Length; j++)
				{
					array3[j] = Datapoint.NewKeyValuePair(array[j], null);
				}
			}
			else
			{
				for (int k = array3.Length; k < array2.Length; k++)
				{
					array3[k] = Datapoint.NewKeyValuePair(null, array2[k]);
				}
			}
			bool flag;
			string text = LogRowFormatter.FormatCollection(array3, true, out flag);
			buffer.Append(flag ? Datapoint.GetCsvEscapedString(text) : text);
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x00076132 File Offset: 0x00074332
		private static KeyValuePair<string, string> NewKeyValuePair(string key, string value)
		{
			return new KeyValuePair<string, string>(key ?? "?", value ?? string.Empty);
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x0007614D File Offset: 0x0007434D
		private static string GetCsvEscapedString(object value)
		{
			return Encoding.UTF8.GetString(Utf8Csv.EncodeAndEscape(value.ToString(), true));
		}

		// Token: 0x040010B4 RID: 4276
		private static readonly string[] EmptyStringArray = new string[0];

		// Token: 0x040010B5 RID: 4277
		private int? size;
	}
}
