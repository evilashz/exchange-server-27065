using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C02 RID: 3074
	internal class DnsRecordList : List<DnsRecord>
	{
		// Token: 0x170010D4 RID: 4308
		// (get) Token: 0x0600434D RID: 17229 RVA: 0x000B474C File Offset: 0x000B294C
		public IEnumerable<DnsRecord> AnswerRecords
		{
			get
			{
				foreach (DnsRecord record in this)
				{
					if (record.Section == DnsResponseSection.Answer)
					{
						yield return record;
					}
				}
				yield break;
			}
		}

		// Token: 0x170010D5 RID: 4309
		// (get) Token: 0x0600434E RID: 17230 RVA: 0x000B48FC File Offset: 0x000B2AFC
		public IEnumerable<DnsRecord> AdditionalRecords
		{
			get
			{
				foreach (DnsRecord record in this)
				{
					if (record.Section == DnsResponseSection.Additional)
					{
						yield return record;
					}
				}
				yield break;
			}
		}

		// Token: 0x0600434F RID: 17231 RVA: 0x000B4AC0 File Offset: 0x000B2CC0
		public static IEnumerable<DnsRecord> EnumerateAddresses(IEnumerable<DnsRecord> records)
		{
			foreach (DnsRecord record in records)
			{
				if (record.RecordType == DnsRecordType.A || record.RecordType == DnsRecordType.AAAA)
				{
					yield return record;
				}
			}
			yield break;
		}

		// Token: 0x06004350 RID: 17232 RVA: 0x000B4AE0 File Offset: 0x000B2CE0
		public List<T> ExtractRecords<T>(DnsRecordType type, IComparer<T> comparer) where T : class
		{
			List<T> list = new List<T>();
			foreach (DnsRecord dnsRecord in this)
			{
				if (dnsRecord.RecordType == type)
				{
					T item = dnsRecord as T;
					int num = list.BinarySearch(item, comparer);
					if (num < 0)
					{
						list.Insert(~num, item);
					}
				}
			}
			return list;
		}

		// Token: 0x06004351 RID: 17233 RVA: 0x000B4D0C File Offset: 0x000B2F0C
		public IEnumerable<DnsRecord> EnumerateAnswers(DnsRecordType type)
		{
			foreach (DnsRecord record in this)
			{
				if (record.Section == DnsResponseSection.Answer && record.RecordType == type)
				{
					yield return record;
				}
			}
			yield break;
		}

		// Token: 0x06004352 RID: 17234 RVA: 0x000B4ED0 File Offset: 0x000B30D0
		public IEnumerable<DnsRecord> Enumerate(DnsRecordType type)
		{
			foreach (DnsRecord record in this)
			{
				if (record.RecordType == type)
				{
					yield return record;
				}
			}
			yield break;
		}

		// Token: 0x06004353 RID: 17235 RVA: 0x000B4EF4 File Offset: 0x000B30F4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (DnsRecord value in this)
			{
				if (stringBuilder.Length != 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04003945 RID: 14661
		public static DnsRecordList Empty = new DnsRecordList();
	}
}
