using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.InfoWorker.Availability
{
	// Token: 0x0200000A RID: 10
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class FreeBusyResponse
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00003234 File Offset: 0x00001434
		// (set) Token: 0x06000052 RID: 82 RVA: 0x0000323C File Offset: 0x0000143C
		[DataMember]
		[XmlElement(IsNullable = false)]
		public ResponseMessage ResponseMessage
		{
			get
			{
				return this.responseMessage;
			}
			set
			{
				this.responseMessage = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00003245 File Offset: 0x00001445
		// (set) Token: 0x06000054 RID: 84 RVA: 0x0000324D File Offset: 0x0000144D
		[XmlElement(IsNullable = false)]
		[DataMember]
		public FreeBusyView FreeBusyView
		{
			get
			{
				return this.freeBusyView;
			}
			set
			{
				this.freeBusyView = value;
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003258 File Offset: 0x00001458
		internal static FreeBusyResponse CreateFrom(FreeBusyQueryResult freeBusyResult, int index)
		{
			if (freeBusyResult == null)
			{
				return null;
			}
			return new FreeBusyResponse
			{
				index = index,
				freeBusyView = FreeBusyView.CreateFrom(freeBusyResult),
				ResponseMessage = ResponseMessageBuilder.ResponseMessageFromExchangeException(freeBusyResult.ExceptionInfo)
			};
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00003295 File Offset: 0x00001495
		// (set) Token: 0x06000057 RID: 87 RVA: 0x0000329D File Offset: 0x0000149D
		internal int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000032A6 File Offset: 0x000014A6
		private FreeBusyResponse()
		{
		}

		// Token: 0x04000018 RID: 24
		private FreeBusyView freeBusyView;

		// Token: 0x04000019 RID: 25
		private ResponseMessage responseMessage;

		// Token: 0x0400001A RID: 26
		private int index;
	}
}
