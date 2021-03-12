using System;
using System.Net;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200003C RID: 60
	internal class ApnsResponse
	{
		// Token: 0x06000242 RID: 578 RVA: 0x00008890 File Offset: 0x00006A90
		private ApnsResponse()
		{
			this.origStatus = byte.MaxValue;
			this.Status = ApnsResponseStatus.Unknown;
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000243 RID: 579 RVA: 0x000088AE File Offset: 0x00006AAE
		// (set) Token: 0x06000244 RID: 580 RVA: 0x000088B6 File Offset: 0x00006AB6
		public int Identifier { get; private set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000245 RID: 581 RVA: 0x000088BF File Offset: 0x00006ABF
		// (set) Token: 0x06000246 RID: 582 RVA: 0x000088C7 File Offset: 0x00006AC7
		public ApnsResponseStatus Status { get; private set; }

		// Token: 0x06000247 RID: 583 RVA: 0x000088D0 File Offset: 0x00006AD0
		public static ApnsResponse FromApnsFormat(byte[] binaryForm)
		{
			if (binaryForm == null || binaryForm.Length <= 0)
			{
				throw new ArgumentNullException("binaryForm");
			}
			if (binaryForm.Length != 6)
			{
				throw new ArgumentException(string.Format("Unexpected number of bytes: {0}", binaryForm.Length), "binaryForm");
			}
			ApnsResponse apnsResponse = new ApnsResponse();
			apnsResponse.origStatus = binaryForm[1];
			if (apnsResponse.origStatus <= 8)
			{
				apnsResponse.Status = (ApnsResponseStatus)apnsResponse.origStatus;
			}
			apnsResponse.Identifier = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(binaryForm, 2));
			return apnsResponse;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000894C File Offset: 0x00006B4C
		public override string ToString()
		{
			if (this.toStringCache == null)
			{
				this.toStringCache = string.Format("{{id:{0}; status:{1}; origStatus:{2}}}", this.Identifier.ToString(), this.Status.ToString(), this.origStatus.ToString());
			}
			return this.toStringCache;
		}

		// Token: 0x040000F1 RID: 241
		public const int Length = 6;

		// Token: 0x040000F2 RID: 242
		private byte origStatus;

		// Token: 0x040000F3 RID: 243
		private string toStringCache;
	}
}
