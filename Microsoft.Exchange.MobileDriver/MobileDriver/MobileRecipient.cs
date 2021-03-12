using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000037 RID: 55
	internal class MobileRecipient : IEquatable<MobileRecipient>
	{
		// Token: 0x06000108 RID: 264 RVA: 0x00006C65 File Offset: 0x00004E65
		public static string GetNumberString(MobileRecipient recipient)
		{
			if (recipient == null)
			{
				return null;
			}
			return recipient.E164Number.Number;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00006C77 File Offset: 0x00004E77
		public static MobileRecipient Parse(string number)
		{
			return new MobileRecipient(E164Number.Parse(number));
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00006C84 File Offset: 0x00004E84
		public static bool TryParse(string number, out MobileRecipient recipient)
		{
			recipient = null;
			E164Number number2 = null;
			if (!E164Number.TryParse(number, out number2))
			{
				return false;
			}
			recipient = new MobileRecipient(number2);
			return true;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00006CAB File Offset: 0x00004EAB
		private MobileRecipient()
		{
			this.Region = null;
			this.Carrier = -1;
			this.Exceptions = new List<Exception>();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006CCC File Offset: 0x00004ECC
		public MobileRecipient(E164Number number) : this()
		{
			if (null == number)
			{
				throw new ArgumentNullException("number");
			}
			this.E164Number = number;
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00006CEF File Offset: 0x00004EEF
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00006CF7 File Offset: 0x00004EF7
		public RegionInfo Region { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00006D00 File Offset: 0x00004F00
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00006D08 File Offset: 0x00004F08
		public int Carrier { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00006D11 File Offset: 0x00004F11
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00006D19 File Offset: 0x00004F19
		public IList<Exception> Exceptions { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00006D22 File Offset: 0x00004F22
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00006D2A File Offset: 0x00004F2A
		public E164Number E164Number { get; private set; }

		// Token: 0x06000115 RID: 277 RVA: 0x00006D33 File Offset: 0x00004F33
		public bool Equals(MobileRecipient other)
		{
			return other != null && this.E164Number == other.E164Number;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00006D4B File Offset: 0x00004F4B
		public override bool Equals(object other)
		{
			return this.Equals(other as MobileRecipient);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00006D59 File Offset: 0x00004F59
		public override int GetHashCode()
		{
			return this.E164Number.GetHashCode();
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006D66 File Offset: 0x00004F66
		public override string ToString()
		{
			return this.E164Number.ToString();
		}
	}
}
