using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000139 RID: 313
	internal class DomainMatchMap<T> where T : class, DomainMatchMap<T>.IDomainEntry
	{
		// Token: 0x06000AC5 RID: 2757 RVA: 0x00021934 File Offset: 0x0001FB34
		public DomainMatchMap(IList<T> entries)
		{
			this.exact = new Dictionary<DomainMatchMap<T>.SubString, T>(entries.Count);
			this.wildcard = new Dictionary<DomainMatchMap<T>.SubString, T>(entries.Count);
			foreach (T value in entries)
			{
				if (value.DomainName.SmtpDomain == null)
				{
					this.star = value;
				}
				else
				{
					int num = DomainMatchMap<T>.CountDots(value.DomainName.Domain);
					if (num > this.maxDots)
					{
						this.maxDots = num;
					}
					DomainMatchMap<T>.SubString key = new DomainMatchMap<T>.SubString(value.DomainName.Domain, 0);
					if (value.DomainName.IncludeSubDomains)
					{
						this.wildcard[key] = value;
						if (!this.exact.ContainsKey(key))
						{
							this.exact.Add(key, value);
						}
					}
					else
					{
						this.exact[key] = value;
					}
				}
			}
			this.maxDots++;
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x00021A6C File Offset: 0x0001FC6C
		public T Star
		{
			get
			{
				return this.star;
			}
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00021A74 File Offset: 0x0001FC74
		public static int CountDots(string s)
		{
			int num = 0;
			for (int num2 = s.IndexOf('.'); num2 != -1; num2 = s.IndexOf('.', num2 + 1))
			{
				num++;
			}
			return num;
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00021AA4 File Offset: 0x0001FCA4
		public static int[] FindAllDots(string s)
		{
			int num = DomainMatchMap<T>.CountDots(s);
			int[] array = new int[num];
			int num2 = s.IndexOf('.');
			int num3 = 0;
			while (num2 != -1)
			{
				array[num3] = num2;
				num2 = s.IndexOf('.', num2 + 1);
				num3++;
			}
			return array;
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00021AE8 File Offset: 0x0001FCE8
		public T GetBestMatch(SmtpDomain domain)
		{
			if (domain == null)
			{
				return default(T);
			}
			DomainMatchMap<T>.SubString subString = new DomainMatchMap<T>.SubString(domain.Domain, 0);
			T result;
			if (this.exact.TryGetValue(subString, out result))
			{
				return result;
			}
			int[] array = DomainMatchMap<T>.FindAllDots(domain.Domain);
			for (int i = (array.Length > this.maxDots) ? (array.Length - this.maxDots) : 0; i < array.Length; i++)
			{
				subString.SetIndex(array[i] + 1);
				if (this.wildcard.TryGetValue(subString, out result))
				{
					return result;
				}
			}
			return this.star;
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00021B78 File Offset: 0x0001FD78
		public T GetBestMatch(string domainName)
		{
			SmtpDomain domain;
			SmtpDomain.TryParse(domainName, out domain);
			return this.GetBestMatch(domain);
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00021DE8 File Offset: 0x0001FFE8
		public IEnumerator<PublicT> GetAllDomains<PublicT>() where PublicT : class
		{
			foreach (T entry in this.exact.Values)
			{
				PublicT publicEntry = entry as PublicT;
				if (publicEntry != null)
				{
					yield return publicEntry;
				}
			}
			foreach (T entry2 in this.wildcard.Values)
			{
				PublicT publicEntry = entry2 as PublicT;
				if (publicEntry != null)
				{
					yield return publicEntry;
				}
			}
			yield break;
		}

		// Token: 0x04000692 RID: 1682
		private const char Dot = '.';

		// Token: 0x04000693 RID: 1683
		private readonly Dictionary<DomainMatchMap<T>.SubString, T> exact;

		// Token: 0x04000694 RID: 1684
		private readonly Dictionary<DomainMatchMap<T>.SubString, T> wildcard;

		// Token: 0x04000695 RID: 1685
		private readonly T star = default(T);

		// Token: 0x04000696 RID: 1686
		private readonly int maxDots;

		// Token: 0x0200013A RID: 314
		public interface IDomainEntry
		{
			// Token: 0x17000379 RID: 889
			// (get) Token: 0x06000ACC RID: 2764
			SmtpDomainWithSubdomains DomainName { get; }
		}

		// Token: 0x0200013B RID: 315
		public class SubString
		{
			// Token: 0x06000ACD RID: 2765 RVA: 0x00021E04 File Offset: 0x00020004
			public SubString(string s, int start)
			{
				this.s = s;
				this.start = start;
			}

			// Token: 0x06000ACE RID: 2766 RVA: 0x00021E1A File Offset: 0x0002001A
			public void SetIndex(int i)
			{
				this.start = i;
			}

			// Token: 0x06000ACF RID: 2767 RVA: 0x00021E24 File Offset: 0x00020024
			public override int GetHashCode()
			{
				int num = 0;
				for (int i = this.start; i < this.s.Length; i++)
				{
					num = num * 65599 + (int)char.ToLowerInvariant(this.s[i]);
				}
				return num;
			}

			// Token: 0x06000AD0 RID: 2768 RVA: 0x00021E6C File Offset: 0x0002006C
			public override bool Equals(object obj)
			{
				DomainMatchMap<T>.SubString subString = obj as DomainMatchMap<T>.SubString;
				return subString != null && 0 == string.Compare(this.s, this.start, subString.s, subString.start, int.MaxValue, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x04000697 RID: 1687
			private int start;

			// Token: 0x04000698 RID: 1688
			private string s;
		}
	}
}
