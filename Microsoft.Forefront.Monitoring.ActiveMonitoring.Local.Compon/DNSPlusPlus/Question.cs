using System;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.DNSPlusPlus
{
	// Token: 0x02000009 RID: 9
	internal class Question
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000384B File Offset: 0x00001A4B
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00003853 File Offset: 0x00001A53
		public int RequestID { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000385C File Offset: 0x00001A5C
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00003864 File Offset: 0x00001A64
		public RecordType QueryType { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000047 RID: 71 RVA: 0x0000386D File Offset: 0x00001A6D
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00003875 File Offset: 0x00001A75
		public RecordClass QueryClass { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000387E File Offset: 0x00001A7E
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00003886 File Offset: 0x00001A86
		public string Domain { get; private set; }

		// Token: 0x0600004B RID: 75 RVA: 0x00003890 File Offset: 0x00001A90
		public int ProcessResponse(byte[] message, int position)
		{
			int num = position;
			this.Domain = DnsHelper.ReadDomain(message, ref num);
			this.QueryType = (RecordType)DnsHelper.GetUShort(message, num);
			num += 2;
			this.QueryClass = (RecordClass)DnsHelper.GetUShort(message, num);
			num += 2;
			return num;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000038D0 File Offset: 0x00001AD0
		public byte[] GetQueryBytes(int id, string queryName, RecordType queryType, RecordClass queryClass)
		{
			int num = 18 + queryName.Length;
			byte[] array = new byte[num];
			int num2 = 0;
			array[num2++] = (byte)(id >> 8);
			array[num2++] = (byte)(id & 255);
			array[num2++] = 0;
			array[num2++] = 0;
			array[num2++] = 0;
			array[num2++] = 1;
			array[num2++] = 0;
			array[num2++] = 0;
			array[num2++] = 0;
			array[num2++] = 0;
			array[num2++] = 0;
			array[num2++] = 0;
			string[] array2 = queryName.Trim().Split(new char[]
			{
				'.'
			});
			foreach (string text in array2)
			{
				array[num2++] = (byte)text.Length;
				foreach (char c in text)
				{
					array[num2++] = (byte)c;
				}
			}
			array[num2++] = 0;
			array[num2++] = 0;
			array[num2++] = (byte)queryType;
			array[num2++] = 0;
			array[num2++] = (byte)queryClass;
			this.RequestID = id;
			this.QueryType = queryType;
			this.QueryClass = queryClass;
			this.Domain = queryName;
			return array;
		}
	}
}
