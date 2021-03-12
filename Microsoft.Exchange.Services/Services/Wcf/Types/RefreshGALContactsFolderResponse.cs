using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Entities.People.EntitySets.ResponseTypes;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x020009FC RID: 2556
	[DataContract]
	public sealed class RefreshGALContactsFolderResponse : IExchangeWebMethodResponse
	{
		// Token: 0x06004840 RID: 18496 RVA: 0x00101554 File Offset: 0x000FF754
		internal RefreshGALContactsFolderResponse(ServiceResult<RefreshGALFolderResponseEntity> serviceResult)
		{
			this.serviceResult = serviceResult;
		}

		// Token: 0x06004841 RID: 18497 RVA: 0x00101563 File Offset: 0x000FF763
		public ResponseType GetResponseType()
		{
			return ResponseType.RefreshGALContactsFolderResponseMessage;
		}

		// Token: 0x06004842 RID: 18498 RVA: 0x0010156C File Offset: 0x000FF76C
		public ResponseCodeType GetErrorCodeToLog()
		{
			ResponseCodeType result = ResponseCodeType.NoError;
			if (this.serviceResult.Code != ServiceResultCode.Success)
			{
				result = this.serviceResult.Error.MessageKey;
			}
			return result;
		}

		// Token: 0x04002953 RID: 10579
		private readonly ServiceResult<RefreshGALFolderResponseEntity> serviceResult;
	}
}
