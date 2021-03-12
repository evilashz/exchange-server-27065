using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x020000FF RID: 255
	internal class FindMeNumbers
	{
		// Token: 0x0600082B RID: 2091 RVA: 0x0001F9EF File Offset: 0x0001DBEF
		public FindMeNumbers(string number, int timeout) : this(number, timeout, string.Empty)
		{
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0001F9FE File Offset: 0x0001DBFE
		public FindMeNumbers(string number, int timeout, string label)
		{
			this.numbers = new List<FindMe>();
			this.Add(number, timeout, label);
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x0001FA1A File Offset: 0x0001DC1A
		public FindMe[] NumberList
		{
			get
			{
				return this.numbers.ToArray();
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x0001FA27 File Offset: 0x0001DC27
		public int Count
		{
			get
			{
				return this.numbers.Count;
			}
		}

		// Token: 0x170001FD RID: 509
		public FindMe this[int index]
		{
			get
			{
				if (index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.numbers[index];
			}
			set
			{
				this.numbers[index] = value;
			}
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0001FA65 File Offset: 0x0001DC65
		public void Add(string number, int timeout)
		{
			this.numbers.Add(new FindMe(number, timeout));
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001FA79 File Offset: 0x0001DC79
		public void Add(string number, int timeout, string label)
		{
			this.numbers.Add(new FindMe(number, timeout, label));
		}

		// Token: 0x040004C7 RID: 1223
		private List<FindMe> numbers;
	}
}
