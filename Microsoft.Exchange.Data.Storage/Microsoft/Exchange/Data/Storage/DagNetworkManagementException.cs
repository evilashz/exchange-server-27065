using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000EB RID: 235
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagNetworkManagementException : LocalizedException
	{
		// Token: 0x0600132C RID: 4908 RVA: 0x00068FF0 File Offset: 0x000671F0
		public DagNetworkManagementException(string err) : base(ServerStrings.DagNetworkManagementError(err))
		{
			this.err = err;
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x00069005 File Offset: 0x00067205
		public DagNetworkManagementException(string err, Exception innerException) : base(ServerStrings.DagNetworkManagementError(err), innerException)
		{
			this.err = err;
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0006901B File Offset: 0x0006721B
		protected DagNetworkManagementException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.err = (string)info.GetValue("err", typeof(string));
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x00069045 File Offset: 0x00067245
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("err", this.err);
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001330 RID: 4912 RVA: 0x00069060 File Offset: 0x00067260
		public string Err
		{
			get
			{
				return this.err;
			}
		}

		// Token: 0x04000987 RID: 2439
		private readonly string err;
	}
}
