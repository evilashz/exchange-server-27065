using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C93 RID: 3219
	[AttributeUsage(AttributeTargets.Method)]
	public class OfflineClientAttribute : Attribute, IOperationBehavior
	{
		// Token: 0x17001443 RID: 5187
		// (get) Token: 0x06005741 RID: 22337 RVA: 0x00112F59 File Offset: 0x00111159
		// (set) Token: 0x06005742 RID: 22338 RVA: 0x00112F61 File Offset: 0x00111161
		public bool Queued { get; set; }

		// Token: 0x06005743 RID: 22339 RVA: 0x00112F6A File Offset: 0x0011116A
		void IOperationBehavior.AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06005744 RID: 22340 RVA: 0x00112F6C File Offset: 0x0011116C
		void IOperationBehavior.ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
		{
		}

		// Token: 0x06005745 RID: 22341 RVA: 0x00112F6E File Offset: 0x0011116E
		void IOperationBehavior.ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
		{
		}

		// Token: 0x06005746 RID: 22342 RVA: 0x00112F70 File Offset: 0x00111170
		void IOperationBehavior.Validate(OperationDescription operationDescription)
		{
		}
	}
}
