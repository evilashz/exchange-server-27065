using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200006E RID: 110
	public class NullTrackableOperation : IOperationExecutionTrackable
	{
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0001094B File Offset: 0x0000EB4B
		public static IOperationExecutionTrackable Instance
		{
			get
			{
				return NullTrackableOperation.operation;
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00010952 File Offset: 0x0000EB52
		public IOperationExecutionTrackingKey GetTrackingKey()
		{
			return null;
		}

		// Token: 0x04000603 RID: 1539
		private static IOperationExecutionTrackable operation = new NullTrackableOperation();
	}
}
