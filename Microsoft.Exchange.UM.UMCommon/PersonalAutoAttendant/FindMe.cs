using System;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x020000FE RID: 254
	internal class FindMe
	{
		// Token: 0x0600081E RID: 2078 RVA: 0x0001F95D File Offset: 0x0001DB5D
		internal FindMe()
		{
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0001F965 File Offset: 0x0001DB65
		internal FindMe(string number, int timeout) : this(number, timeout, string.Empty)
		{
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0001F974 File Offset: 0x0001DB74
		internal FindMe(string number, int timeout, string label)
		{
			this.number = number;
			this.timeout = timeout;
			this.label = label;
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x0001F991 File Offset: 0x0001DB91
		// (set) Token: 0x06000822 RID: 2082 RVA: 0x0001F999 File Offset: 0x0001DB99
		internal string Number
		{
			get
			{
				return this.number;
			}
			set
			{
				this.number = value;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x0001F9A2 File Offset: 0x0001DBA2
		// (set) Token: 0x06000824 RID: 2084 RVA: 0x0001F9AA File Offset: 0x0001DBAA
		internal int Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				this.timeout = value;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x0001F9B3 File Offset: 0x0001DBB3
		// (set) Token: 0x06000826 RID: 2086 RVA: 0x0001F9C4 File Offset: 0x0001DBC4
		internal string Label
		{
			get
			{
				return this.label ?? string.Empty;
			}
			set
			{
				this.label = value;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x0001F9CD File Offset: 0x0001DBCD
		// (set) Token: 0x06000828 RID: 2088 RVA: 0x0001F9D5 File Offset: 0x0001DBD5
		internal PAAValidationResult ValidationResult
		{
			get
			{
				return this.result;
			}
			set
			{
				this.result = value;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x0001F9DE File Offset: 0x0001DBDE
		// (set) Token: 0x0600082A RID: 2090 RVA: 0x0001F9E6 File Offset: 0x0001DBE6
		internal PhoneNumber PhoneNumber
		{
			get
			{
				return this.phoneNumber;
			}
			set
			{
				this.phoneNumber = value;
			}
		}

		// Token: 0x040004C2 RID: 1218
		private string number;

		// Token: 0x040004C3 RID: 1219
		private int timeout;

		// Token: 0x040004C4 RID: 1220
		private string label;

		// Token: 0x040004C5 RID: 1221
		private PAAValidationResult result;

		// Token: 0x040004C6 RID: 1222
		private PhoneNumber phoneNumber;
	}
}
