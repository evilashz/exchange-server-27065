using System;
using System.Net;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000CA7 RID: 3239
	internal struct WireDnsHeader
	{
		// Token: 0x170011E9 RID: 4585
		// (get) Token: 0x06004745 RID: 18245 RVA: 0x000BFFDB File Offset: 0x000BE1DB
		public int XId
		{
			get
			{
				return (int)this.xid;
			}
		}

		// Token: 0x170011EA RID: 4586
		// (get) Token: 0x06004746 RID: 18246 RVA: 0x000BFFE3 File Offset: 0x000BE1E3
		public int Questions
		{
			get
			{
				return (int)this.questionCount;
			}
		}

		// Token: 0x170011EB RID: 4587
		// (get) Token: 0x06004747 RID: 18247 RVA: 0x000BFFEB File Offset: 0x000BE1EB
		public int Answers
		{
			get
			{
				return (int)this.answerCount;
			}
		}

		// Token: 0x170011EC RID: 4588
		// (get) Token: 0x06004748 RID: 18248 RVA: 0x000BFFF3 File Offset: 0x000BE1F3
		public int AuthorityRecords
		{
			get
			{
				return (int)this.nameServerCount;
			}
		}

		// Token: 0x170011ED RID: 4589
		// (get) Token: 0x06004749 RID: 18249 RVA: 0x000BFFFB File Offset: 0x000BE1FB
		public int AdditionalRecords
		{
			get
			{
				return (int)this.additionalCount;
			}
		}

		// Token: 0x170011EE RID: 4590
		// (get) Token: 0x0600474A RID: 18250 RVA: 0x000C0003 File Offset: 0x000BE203
		public bool IsResponse
		{
			get
			{
				return (byte)(this.byteFlags1 & WireDnsHeader.ByteMask1.IsResponse) == 128;
			}
		}

		// Token: 0x170011EF RID: 4591
		// (get) Token: 0x0600474B RID: 18251 RVA: 0x000C0019 File Offset: 0x000BE219
		public bool IsTruncated
		{
			get
			{
				return (byte)(this.byteFlags1 & WireDnsHeader.ByteMask1.Truncation) == 2;
			}
		}

		// Token: 0x170011F0 RID: 4592
		// (get) Token: 0x0600474C RID: 18252 RVA: 0x000C0027 File Offset: 0x000BE227
		public bool IsAuthoritative
		{
			get
			{
				return (byte)(this.byteFlags1 & WireDnsHeader.ByteMask1.Authoritative) == 4;
			}
		}

		// Token: 0x170011F1 RID: 4593
		// (get) Token: 0x0600474D RID: 18253 RVA: 0x000C0035 File Offset: 0x000BE235
		public WireDnsHeader.Response ResponseCode
		{
			get
			{
				return (WireDnsHeader.Response)(this.byteFlags2 & WireDnsHeader.ByteMask2.ResponseCode);
			}
		}

		// Token: 0x170011F2 RID: 4594
		// (get) Token: 0x0600474E RID: 18254 RVA: 0x000C0041 File Offset: 0x000BE241
		public bool IsRecursionAvailable
		{
			get
			{
				return (byte)(this.byteFlags2 & WireDnsHeader.ByteMask2.RecursionAvailable) == 128;
			}
		}

		// Token: 0x170011F3 RID: 4595
		// (get) Token: 0x0600474F RID: 18255 RVA: 0x000C0057 File Offset: 0x000BE257
		public bool WasRecursionDesired
		{
			get
			{
				return (byte)(this.byteFlags1 & WireDnsHeader.ByteMask1.RecursionDesired) == 1;
			}
		}

		// Token: 0x06004750 RID: 18256 RVA: 0x000C0068 File Offset: 0x000BE268
		public static WireDnsHeader NetworkToHostOrder(byte[] buffer, int offset)
		{
			WireDnsHeader result = default(WireDnsHeader);
			result.xid = (ushort)IPAddress.NetworkToHostOrder((short)BitConverter.ToUInt16(buffer, offset));
			int num = offset + 2;
			result.byteFlags1 = (WireDnsHeader.ByteMask1)buffer[num];
			num++;
			result.byteFlags2 = (WireDnsHeader.ByteMask2)buffer[num];
			num++;
			result.questionCount = (ushort)IPAddress.NetworkToHostOrder((short)BitConverter.ToUInt16(buffer, num));
			num += 2;
			result.answerCount = (ushort)IPAddress.NetworkToHostOrder((short)BitConverter.ToUInt16(buffer, num));
			num += 2;
			result.nameServerCount = (ushort)IPAddress.NetworkToHostOrder((short)BitConverter.ToUInt16(buffer, num));
			num += 2;
			result.additionalCount = (ushort)IPAddress.NetworkToHostOrder((short)BitConverter.ToUInt16(buffer, num));
			buffer[offset] = (byte)result.xid;
			buffer[offset + 1] = (byte)(result.xid >> 8);
			buffer[offset + 4] = (byte)result.questionCount;
			buffer[offset + 5] = (byte)(result.questionCount >> 8);
			buffer[offset + 6] = (byte)result.answerCount;
			buffer[offset + 7] = (byte)(result.answerCount >> 8);
			buffer[offset + 8] = (byte)result.nameServerCount;
			buffer[offset + 9] = (byte)(result.nameServerCount >> 8);
			buffer[offset + 10] = (byte)result.additionalCount;
			buffer[offset + 11] = (byte)(result.additionalCount >> 8);
			return result;
		}

		// Token: 0x04003C68 RID: 15464
		public static readonly int MarshalSize = Marshal.SizeOf(typeof(WireDnsHeader));

		// Token: 0x04003C69 RID: 15465
		public ushort xid;

		// Token: 0x04003C6A RID: 15466
		private WireDnsHeader.ByteMask1 byteFlags1;

		// Token: 0x04003C6B RID: 15467
		private WireDnsHeader.ByteMask2 byteFlags2;

		// Token: 0x04003C6C RID: 15468
		private ushort questionCount;

		// Token: 0x04003C6D RID: 15469
		private ushort answerCount;

		// Token: 0x04003C6E RID: 15470
		private ushort nameServerCount;

		// Token: 0x04003C6F RID: 15471
		private ushort additionalCount;

		// Token: 0x02000CA8 RID: 3240
		public enum Response : byte
		{
			// Token: 0x04003C71 RID: 15473
			NoError,
			// Token: 0x04003C72 RID: 15474
			FormatError,
			// Token: 0x04003C73 RID: 15475
			ServerFailure,
			// Token: 0x04003C74 RID: 15476
			NameError,
			// Token: 0x04003C75 RID: 15477
			NotImplemented,
			// Token: 0x04003C76 RID: 15478
			Refused
		}

		// Token: 0x02000CA9 RID: 3241
		[Flags]
		private enum ByteMask1 : byte
		{
			// Token: 0x04003C78 RID: 15480
			RecursionDesired = 1,
			// Token: 0x04003C79 RID: 15481
			Truncation = 2,
			// Token: 0x04003C7A RID: 15482
			Authoritative = 4,
			// Token: 0x04003C7B RID: 15483
			IsResponse = 128
		}

		// Token: 0x02000CAA RID: 3242
		[Flags]
		private enum ByteMask2 : byte
		{
			// Token: 0x04003C7D RID: 15485
			ResponseCode = 15,
			// Token: 0x04003C7E RID: 15486
			Broadcast = 16,
			// Token: 0x04003C7F RID: 15487
			RecursionAvailable = 128
		}
	}
}
