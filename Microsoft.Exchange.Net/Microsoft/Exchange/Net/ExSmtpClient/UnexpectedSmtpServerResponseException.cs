using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net.ExSmtpClient
{
	// Token: 0x020000EC RID: 236
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UnexpectedSmtpServerResponseException : LocalizedException
	{
		// Token: 0x0600062A RID: 1578 RVA: 0x00016026 File Offset: 0x00014226
		public UnexpectedSmtpServerResponseException(int expectedResponseCode, int actualResponseCode, string wholeResponse) : base(NetException.UnexpectedSmtpServerResponseException(expectedResponseCode, actualResponseCode, wholeResponse))
		{
			this.expectedResponseCode = expectedResponseCode;
			this.actualResponseCode = actualResponseCode;
			this.wholeResponse = wholeResponse;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001604B File Offset: 0x0001424B
		public UnexpectedSmtpServerResponseException(int expectedResponseCode, int actualResponseCode, string wholeResponse, Exception innerException) : base(NetException.UnexpectedSmtpServerResponseException(expectedResponseCode, actualResponseCode, wholeResponse), innerException)
		{
			this.expectedResponseCode = expectedResponseCode;
			this.actualResponseCode = actualResponseCode;
			this.wholeResponse = wholeResponse;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00016074 File Offset: 0x00014274
		protected UnexpectedSmtpServerResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.expectedResponseCode = (int)info.GetValue("expectedResponseCode", typeof(int));
			this.actualResponseCode = (int)info.GetValue("actualResponseCode", typeof(int));
			this.wholeResponse = (string)info.GetValue("wholeResponse", typeof(string));
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x000160E9 File Offset: 0x000142E9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("expectedResponseCode", this.expectedResponseCode);
			info.AddValue("actualResponseCode", this.actualResponseCode);
			info.AddValue("wholeResponse", this.wholeResponse);
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x00016126 File Offset: 0x00014326
		public int ExpectedResponseCode
		{
			get
			{
				return this.expectedResponseCode;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x0001612E File Offset: 0x0001432E
		public int ActualResponseCode
		{
			get
			{
				return this.actualResponseCode;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x00016136 File Offset: 0x00014336
		public string WholeResponse
		{
			get
			{
				return this.wholeResponse;
			}
		}

		// Token: 0x040004FB RID: 1275
		private readonly int expectedResponseCode;

		// Token: 0x040004FC RID: 1276
		private readonly int actualResponseCode;

		// Token: 0x040004FD RID: 1277
		private readonly string wholeResponse;
	}
}
