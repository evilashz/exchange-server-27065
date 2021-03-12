using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011F6 RID: 4598
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TUC_OperationFailed : LocalizedException
	{
		// Token: 0x0600B9A7 RID: 47527 RVA: 0x002A637D File Offset: 0x002A457D
		public TUC_OperationFailed(string operation) : base(Strings.OperationFailed(operation))
		{
			this.operation = operation;
		}

		// Token: 0x0600B9A8 RID: 47528 RVA: 0x002A6392 File Offset: 0x002A4592
		public TUC_OperationFailed(string operation, Exception innerException) : base(Strings.OperationFailed(operation), innerException)
		{
			this.operation = operation;
		}

		// Token: 0x0600B9A9 RID: 47529 RVA: 0x002A63A8 File Offset: 0x002A45A8
		protected TUC_OperationFailed(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.operation = (string)info.GetValue("operation", typeof(string));
		}

		// Token: 0x0600B9AA RID: 47530 RVA: 0x002A63D2 File Offset: 0x002A45D2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("operation", this.operation);
		}

		// Token: 0x17003A4C RID: 14924
		// (get) Token: 0x0600B9AB RID: 47531 RVA: 0x002A63ED File Offset: 0x002A45ED
		public string Operation
		{
			get
			{
				return this.operation;
			}
		}

		// Token: 0x04006467 RID: 25703
		private readonly string operation;
	}
}
