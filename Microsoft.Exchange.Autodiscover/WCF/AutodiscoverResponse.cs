using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000097 RID: 151
	[DataContract(Name = "AutodiscoverResponse", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class AutodiscoverResponse
	{
		// Token: 0x060003D7 RID: 983 RVA: 0x0001793C File Offset: 0x00015B3C
		public AutodiscoverResponse()
		{
			this.errorCode = ErrorCode.NoError;
			this.errorMessage = string.Empty;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x00017956 File Offset: 0x00015B56
		// (set) Token: 0x060003D9 RID: 985 RVA: 0x0001795E File Offset: 0x00015B5E
		[DataMember(Name = "ErrorCode", Order = 1)]
		public ErrorCode ErrorCode
		{
			get
			{
				return this.errorCode;
			}
			set
			{
				this.errorCode = value;
				RequestDetailsLoggerBase<RequestDetailsLogger>.Current.Set(ServiceCommonMetadata.ErrorCode, this.errorCode);
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003DA RID: 986 RVA: 0x00017984 File Offset: 0x00015B84
		// (set) Token: 0x060003DB RID: 987 RVA: 0x0001798C File Offset: 0x00015B8C
		[DataMember(Name = "ErrorMessage", Order = 2)]
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
			set
			{
				this.errorMessage = value;
				RequestDetailsLoggerBase<RequestDetailsLogger>.Current.AppendGenericError("ErrorMessage", this.errorMessage);
			}
		}

		// Token: 0x0400033A RID: 826
		private ErrorCode errorCode;

		// Token: 0x0400033B RID: 827
		private string errorMessage;
	}
}
