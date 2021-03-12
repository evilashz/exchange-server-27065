using System;

namespace Microsoft.Exchange.UM.ClientAccess
{
	// Token: 0x02000029 RID: 41
	public class UMProperties
	{
		// Token: 0x06000261 RID: 609 RVA: 0x00009EC9 File Offset: 0x000080C9
		public UMProperties()
		{
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00009ED4 File Offset: 0x000080D4
		public UMProperties(UMPropertiesEx properties)
		{
			this.missedCallNotificationEnabled = properties.MissedCallNotificationEnabled;
			this.playOnPhoneDialString = properties.PlayOnPhoneDialString;
			this.telephoneAccessNumbers = properties.TelephoneAccessNumbers;
			this.telephoneAccessFolderEmail = properties.TelephoneAccessFolderEmail;
			this.oofStatus = properties.OofStatus;
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00009F23 File Offset: 0x00008123
		// (set) Token: 0x06000264 RID: 612 RVA: 0x00009F2B File Offset: 0x0000812B
		public bool OofStatus
		{
			get
			{
				return this.oofStatus;
			}
			set
			{
				this.oofStatus = value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00009F34 File Offset: 0x00008134
		// (set) Token: 0x06000266 RID: 614 RVA: 0x00009F3C File Offset: 0x0000813C
		public bool MissedCallNotificationEnabled
		{
			get
			{
				return this.missedCallNotificationEnabled;
			}
			set
			{
				this.missedCallNotificationEnabled = value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00009F45 File Offset: 0x00008145
		// (set) Token: 0x06000268 RID: 616 RVA: 0x00009F4D File Offset: 0x0000814D
		public string PlayOnPhoneDialString
		{
			get
			{
				return this.playOnPhoneDialString;
			}
			set
			{
				this.playOnPhoneDialString = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00009F56 File Offset: 0x00008156
		// (set) Token: 0x0600026A RID: 618 RVA: 0x00009F5E File Offset: 0x0000815E
		public string TelephoneAccessNumbers
		{
			get
			{
				return this.telephoneAccessNumbers;
			}
			set
			{
				this.telephoneAccessNumbers = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00009F67 File Offset: 0x00008167
		// (set) Token: 0x0600026C RID: 620 RVA: 0x00009F6F File Offset: 0x0000816F
		public string TelephoneAccessFolderEmail
		{
			get
			{
				return this.telephoneAccessFolderEmail;
			}
			set
			{
				this.telephoneAccessFolderEmail = value;
			}
		}

		// Token: 0x040000BE RID: 190
		private bool oofStatus;

		// Token: 0x040000BF RID: 191
		private bool missedCallNotificationEnabled;

		// Token: 0x040000C0 RID: 192
		private string playOnPhoneDialString;

		// Token: 0x040000C1 RID: 193
		private string telephoneAccessNumbers;

		// Token: 0x040000C2 RID: 194
		private string telephoneAccessFolderEmail;
	}
}
