using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000D5 RID: 213
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmOperationNotValidOnCurrentRole : AmServerException
	{
		// Token: 0x060012A7 RID: 4775 RVA: 0x0006805D File Offset: 0x0006625D
		public AmOperationNotValidOnCurrentRole(string error) : base(ServerStrings.AmOperationNotValidOnCurrentRole(error))
		{
			this.error = error;
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x00068077 File Offset: 0x00066277
		public AmOperationNotValidOnCurrentRole(string error, Exception innerException) : base(ServerStrings.AmOperationNotValidOnCurrentRole(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x00068092 File Offset: 0x00066292
		protected AmOperationNotValidOnCurrentRole(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x000680BC File Offset: 0x000662BC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x060012AB RID: 4779 RVA: 0x000680D7 File Offset: 0x000662D7
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04000965 RID: 2405
		private readonly string error;
	}
}
