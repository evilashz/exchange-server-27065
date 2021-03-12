using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004B2 RID: 1202
	[ServiceContract(Namespace = "ECP", Name = "UMCallDataRecord")]
	public interface IUMCallDataRecordService : IGetListService<UMCallDataRecordFilter, UMCallDataRecordRow>
	{
	}
}
