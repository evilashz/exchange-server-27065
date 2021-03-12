using System;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x02000187 RID: 391
	[Serializable]
	internal struct ExtendedAttendeeData
	{
		// Token: 0x060010E2 RID: 4322 RVA: 0x0005D974 File Offset: 0x0005BB74
		public ExtendedAttendeeData(string emailAddress, string displayName, int status, int type, bool sendExtendedData)
		{
			this.emailAddress = emailAddress;
			this.displayName = displayName;
			this.status = ((status != -1) ? status : 5);
			this.type = ((type != -1) ? type : 1);
			this.sendExtendedData = sendExtendedData;
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x0005D9AA File Offset: 0x0005BBAA
		// (set) Token: 0x060010E4 RID: 4324 RVA: 0x0005D9B2 File Offset: 0x0005BBB2
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x0005D9BB File Offset: 0x0005BBBB
		// (set) Token: 0x060010E6 RID: 4326 RVA: 0x0005D9C3 File Offset: 0x0005BBC3
		public string EmailAddress
		{
			get
			{
				return this.emailAddress;
			}
			set
			{
				this.emailAddress = value;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x0005D9CC File Offset: 0x0005BBCC
		// (set) Token: 0x060010E8 RID: 4328 RVA: 0x0005D9D4 File Offset: 0x0005BBD4
		public bool SendExtendedData
		{
			get
			{
				return this.sendExtendedData;
			}
			set
			{
				this.sendExtendedData = value;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x0005D9DD File Offset: 0x0005BBDD
		// (set) Token: 0x060010EA RID: 4330 RVA: 0x0005D9E5 File Offset: 0x0005BBE5
		public int Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x060010EB RID: 4331 RVA: 0x0005D9EE File Offset: 0x0005BBEE
		// (set) Token: 0x060010EC RID: 4332 RVA: 0x0005D9F6 File Offset: 0x0005BBF6
		public int Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x04000AEA RID: 2794
		private string displayName;

		// Token: 0x04000AEB RID: 2795
		private string emailAddress;

		// Token: 0x04000AEC RID: 2796
		private bool sendExtendedData;

		// Token: 0x04000AED RID: 2797
		private int status;

		// Token: 0x04000AEE RID: 2798
		private int type;
	}
}
