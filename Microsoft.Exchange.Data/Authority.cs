using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200011B RID: 283
	[Serializable]
	internal class Authority : IEquatable<Authority>
	{
		// Token: 0x1700033F RID: 831
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x0001EDB1 File Offset: 0x0001CFB1
		public string Fqdn
		{
			get
			{
				return this.fqdn;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x0001EDB9 File Offset: 0x0001CFB9
		public int PortNumber
		{
			get
			{
				return this.portNumber;
			}
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0001EDC1 File Offset: 0x0001CFC1
		public Authority(string fqdn, int portNumber)
		{
			if (string.IsNullOrEmpty(fqdn))
			{
				throw new ArgumentNullException("fqdn");
			}
			this.fqdn = fqdn;
			this.portNumber = portNumber;
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0001EDEA File Offset: 0x0001CFEA
		public override int GetHashCode()
		{
			return this.fqdn.GetHashCode() ^ this.portNumber.GetHashCode();
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0001EE03 File Offset: 0x0001D003
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Authority);
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0001EE11 File Offset: 0x0001D011
		public bool Equals(Authority authority)
		{
			return authority != null && this.portNumber == authority.portNumber && string.Equals(this.fqdn, authority.fqdn, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0001EE3A File Offset: 0x0001D03A
		public override string ToString()
		{
			return this.fqdn + ":" + this.portNumber;
		}

		// Token: 0x04000627 RID: 1575
		private string fqdn;

		// Token: 0x04000628 RID: 1576
		private int portNumber;
	}
}
