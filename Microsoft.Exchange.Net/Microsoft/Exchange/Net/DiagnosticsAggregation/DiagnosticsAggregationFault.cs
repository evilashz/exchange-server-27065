using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.DiagnosticsAggregation
{
	// Token: 0x02000830 RID: 2096
	[DataContract]
	internal class DiagnosticsAggregationFault
	{
		// Token: 0x06002C73 RID: 11379 RVA: 0x00064D7D File Offset: 0x00062F7D
		public DiagnosticsAggregationFault(ErrorCode errorCode, string message)
		{
			if (string.IsNullOrEmpty(message))
			{
				throw new ArgumentException("message");
			}
			this.ErrorCode = errorCode.ToString();
			this.Message = message;
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x06002C74 RID: 11380 RVA: 0x00064DB0 File Offset: 0x00062FB0
		// (set) Token: 0x06002C75 RID: 11381 RVA: 0x00064DB8 File Offset: 0x00062FB8
		[DataMember(IsRequired = true)]
		public string ErrorCode { get; private set; }

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x06002C76 RID: 11382 RVA: 0x00064DC1 File Offset: 0x00062FC1
		// (set) Token: 0x06002C77 RID: 11383 RVA: 0x00064DC9 File Offset: 0x00062FC9
		[DataMember(IsRequired = true)]
		public string Message { get; private set; }

		// Token: 0x06002C78 RID: 11384 RVA: 0x00064DD4 File Offset: 0x00062FD4
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "ErrorCode: {0}, Message: {1}", new object[]
			{
				this.ErrorCode,
				this.Message
			});
		}
	}
}
