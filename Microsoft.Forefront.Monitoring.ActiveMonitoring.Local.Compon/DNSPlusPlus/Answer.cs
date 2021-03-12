using System;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.DNSPlusPlus
{
	// Token: 0x02000002 RID: 2
	internal class Answer
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		// (set) Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000002D8
		public string Domain { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020E1 File Offset: 0x000002E1
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020E9 File Offset: 0x000002E9
		public RecordType RecordType { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020F2 File Offset: 0x000002F2
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000020FA File Offset: 0x000002FA
		public RecordClass RecordClass { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002103 File Offset: 0x00000303
		// (set) Token: 0x06000008 RID: 8 RVA: 0x0000210B File Offset: 0x0000030B
		public int TTL { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002114 File Offset: 0x00000314
		// (set) Token: 0x0600000A RID: 10 RVA: 0x0000211C File Offset: 0x0000031C
		public ARecord Arecord { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002125 File Offset: 0x00000325
		// (set) Token: 0x0600000C RID: 12 RVA: 0x0000212D File Offset: 0x0000032D
		public AaaaRecord AaaaRecord { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002136 File Offset: 0x00000336
		// (set) Token: 0x0600000E RID: 14 RVA: 0x0000213E File Offset: 0x0000033E
		public NSRecord NSrecord { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002147 File Offset: 0x00000347
		// (set) Token: 0x06000010 RID: 16 RVA: 0x0000214F File Offset: 0x0000034F
		public SOARecord SOArecord { get; private set; }

		// Token: 0x06000011 RID: 17 RVA: 0x00002158 File Offset: 0x00000358
		public int ProcessMessage(byte[] message, int position)
		{
			int num = position;
			this.Domain = DnsHelper.ReadDomain(message, ref num);
			this.RecordType = (RecordType)DnsHelper.GetUShort(message, num);
			num += 2;
			this.RecordClass = (RecordClass)DnsHelper.GetUShort(message, num);
			num += 2;
			this.TTL = DnsHelper.GetInt(message, num);
			num += 4;
			DnsHelper.GetUShort(message, num);
			num += 2;
			RecordType recordType = this.RecordType;
			switch (recordType)
			{
			case RecordType.A:
				this.Arecord = new ARecord();
				num = this.Arecord.ProcessResponse(message, num);
				break;
			case RecordType.NS:
				this.NSrecord = new NSRecord();
				num = this.NSrecord.ProcessResponse(message, num);
				break;
			default:
				if (recordType != RecordType.SOA)
				{
					if (recordType != RecordType.AAAA)
					{
						throw new FormatException(string.Format("Invalid record type for answer, value={0}", this.RecordType));
					}
					this.AaaaRecord = new AaaaRecord();
					num = this.AaaaRecord.ProcessResponse(message, num);
				}
				else
				{
					this.SOArecord = new SOARecord();
					num = this.SOArecord.ProcessResponse(message, num);
				}
				break;
			}
			return num;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002260 File Offset: 0x00000460
		public override string ToString()
		{
			string text = string.Format("Domain={0}, RecordType={1}, RecordClass={2}, TTL={3} ", new object[]
			{
				this.Domain,
				this.RecordType,
				this.RecordClass,
				this.TTL
			});
			RecordType recordType = this.RecordType;
			switch (recordType)
			{
			case RecordType.A:
				text += this.Arecord.ToString();
				break;
			case RecordType.NS:
				text += this.NSrecord.ToString();
				break;
			default:
				if (recordType != RecordType.SOA)
				{
					if (recordType == RecordType.AAAA)
					{
						text += this.AaaaRecord.ToString();
					}
				}
				else
				{
					text += this.SOArecord.ToString();
				}
				break;
			}
			return text;
		}
	}
}
