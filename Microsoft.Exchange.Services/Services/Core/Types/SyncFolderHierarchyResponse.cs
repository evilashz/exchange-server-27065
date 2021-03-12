using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000568 RID: 1384
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("SyncFolderHierarchyResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class SyncFolderHierarchyResponse : BaseInfoResponse
	{
		// Token: 0x060026BA RID: 9914 RVA: 0x000A68F9 File Offset: 0x000A4AF9
		public SyncFolderHierarchyResponse() : base(ResponseType.SyncFolderHierarchyResponseMessage)
		{
		}

		// Token: 0x060026BB RID: 9915 RVA: 0x000A6903 File Offset: 0x000A4B03
		internal override ResponseMessage CreateResponseMessage<TValue>(ServiceResultCode code, ServiceError error, TValue value)
		{
			return new SyncFolderHierarchyResponseMessage(code, error, value as SyncFolderHierarchyChangesType);
		}

		// Token: 0x060026BC RID: 9916 RVA: 0x000A6917 File Offset: 0x000A4B17
		internal override void ProcessServiceResult<TValue>(ServiceResult<TValue> result)
		{
			base.AddResponse(this.CreateResponseMessage<TValue>(result.Code, result.Error, result.Value));
		}
	}
}
