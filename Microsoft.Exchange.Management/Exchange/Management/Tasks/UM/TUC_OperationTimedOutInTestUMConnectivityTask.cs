using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011F7 RID: 4599
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TUC_OperationTimedOutInTestUMConnectivityTask : LocalizedException
	{
		// Token: 0x0600B9AC RID: 47532 RVA: 0x002A63F5 File Offset: 0x002A45F5
		public TUC_OperationTimedOutInTestUMConnectivityTask(string operation, string timeout) : base(Strings.OperationTimedOutInTestUMConnectivityTask(operation, timeout))
		{
			this.operation = operation;
			this.timeout = timeout;
		}

		// Token: 0x0600B9AD RID: 47533 RVA: 0x002A6412 File Offset: 0x002A4612
		public TUC_OperationTimedOutInTestUMConnectivityTask(string operation, string timeout, Exception innerException) : base(Strings.OperationTimedOutInTestUMConnectivityTask(operation, timeout), innerException)
		{
			this.operation = operation;
			this.timeout = timeout;
		}

		// Token: 0x0600B9AE RID: 47534 RVA: 0x002A6430 File Offset: 0x002A4630
		protected TUC_OperationTimedOutInTestUMConnectivityTask(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.operation = (string)info.GetValue("operation", typeof(string));
			this.timeout = (string)info.GetValue("timeout", typeof(string));
		}

		// Token: 0x0600B9AF RID: 47535 RVA: 0x002A6485 File Offset: 0x002A4685
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("operation", this.operation);
			info.AddValue("timeout", this.timeout);
		}

		// Token: 0x17003A4D RID: 14925
		// (get) Token: 0x0600B9B0 RID: 47536 RVA: 0x002A64B1 File Offset: 0x002A46B1
		public string Operation
		{
			get
			{
				return this.operation;
			}
		}

		// Token: 0x17003A4E RID: 14926
		// (get) Token: 0x0600B9B1 RID: 47537 RVA: 0x002A64B9 File Offset: 0x002A46B9
		public string Timeout
		{
			get
			{
				return this.timeout;
			}
		}

		// Token: 0x04006468 RID: 25704
		private readonly string operation;

		// Token: 0x04006469 RID: 25705
		private readonly string timeout;
	}
}
