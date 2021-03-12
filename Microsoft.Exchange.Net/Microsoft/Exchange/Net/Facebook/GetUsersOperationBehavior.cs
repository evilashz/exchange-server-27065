using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Facebook
{
	// Token: 0x0200072D RID: 1837
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetUsersOperationBehavior : Attribute, IOperationBehavior
	{
		// Token: 0x06002313 RID: 8979 RVA: 0x00047B6F File Offset: 0x00045D6F
		public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
		{
			clientOperation.Formatter = new GetUsersMessageFormatter(clientOperation.Formatter);
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x00047B82 File Offset: 0x00045D82
		public void Validate(OperationDescription operationDescription)
		{
		}

		// Token: 0x06002315 RID: 8981 RVA: 0x00047B84 File Offset: 0x00045D84
		public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
		{
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x00047B86 File Offset: 0x00045D86
		public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
		{
		}
	}
}
