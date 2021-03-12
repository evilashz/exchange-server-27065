using System;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000BE9 RID: 3049
	internal abstract class DnsRecord
	{
		// Token: 0x060042C9 RID: 17097 RVA: 0x000B1FD0 File Offset: 0x000B01D0
		protected DnsRecord(Win32DnsRecordHeader header)
		{
			this.name = header.name;
			this.timeToLive = Math.Max(1U, header.ttl);
			this.section = header.Section;
			this.recordType = header.recordType;
		}

		// Token: 0x060042CA RID: 17098 RVA: 0x000B201D File Offset: 0x000B021D
		protected DnsRecord(string value)
		{
			this.name = value;
			this.timeToLive = 1U;
		}

		// Token: 0x170010B4 RID: 4276
		// (get) Token: 0x060042CB RID: 17099 RVA: 0x000B2033 File Offset: 0x000B0233
		// (set) Token: 0x060042CC RID: 17100 RVA: 0x000B203B File Offset: 0x000B023B
		public string Name
		{
			get
			{
				return this.name;
			}
			protected set
			{
				this.name = value;
			}
		}

		// Token: 0x170010B5 RID: 4277
		// (get) Token: 0x060042CD RID: 17101 RVA: 0x000B2044 File Offset: 0x000B0244
		// (set) Token: 0x060042CE RID: 17102 RVA: 0x000B2053 File Offset: 0x000B0253
		public TimeSpan TimeToLive
		{
			get
			{
				return TimeSpan.FromSeconds(this.timeToLive);
			}
			set
			{
				this.timeToLive = (uint)value.TotalSeconds;
			}
		}

		// Token: 0x170010B6 RID: 4278
		// (get) Token: 0x060042CF RID: 17103 RVA: 0x000B2063 File Offset: 0x000B0263
		// (set) Token: 0x060042D0 RID: 17104 RVA: 0x000B206B File Offset: 0x000B026B
		public DnsResponseSection Section
		{
			get
			{
				return this.section;
			}
			set
			{
				this.section = value;
			}
		}

		// Token: 0x170010B7 RID: 4279
		// (get) Token: 0x060042D1 RID: 17105 RVA: 0x000B2074 File Offset: 0x000B0274
		// (set) Token: 0x060042D2 RID: 17106 RVA: 0x000B207C File Offset: 0x000B027C
		public DnsRecordType RecordType
		{
			get
			{
				return this.recordType;
			}
			set
			{
				this.recordType = value;
			}
		}

		// Token: 0x060042D3 RID: 17107 RVA: 0x000B2088 File Offset: 0x000B0288
		public override string ToString()
		{
			if (this.name == null)
			{
				return "(null) " + this.recordType.ToString();
			}
			return this.name + " " + this.recordType.ToString();
		}

		// Token: 0x040038FC RID: 14588
		private string name;

		// Token: 0x040038FD RID: 14589
		private uint timeToLive;

		// Token: 0x040038FE RID: 14590
		private DnsResponseSection section;

		// Token: 0x040038FF RID: 14591
		private DnsRecordType recordType;
	}
}
