using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002DD RID: 733
	[ServiceContract(Namespace = "ECP", Name = "RecipientTrackingDetails")]
	public interface IRecipientTrackingDetails : IGetObjectService<RecipientTrackingEventRow>
	{
	}
}
