using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000BC RID: 188
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DxStoreKeyApiOperationException : LocalizedException
	{
		// Token: 0x060006DE RID: 1758 RVA: 0x0001B265 File Offset: 0x00019465
		public DxStoreKeyApiOperationException(string operationName, string keyName) : base(Strings.DxStoreKeyApiOperationException(operationName, keyName))
		{
			this.operationName = operationName;
			this.keyName = keyName;
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0001B282 File Offset: 0x00019482
		public DxStoreKeyApiOperationException(string operationName, string keyName, Exception innerException) : base(Strings.DxStoreKeyApiOperationException(operationName, keyName), innerException)
		{
			this.operationName = operationName;
			this.keyName = keyName;
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0001B2A0 File Offset: 0x000194A0
		protected DxStoreKeyApiOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.operationName = (string)info.GetValue("operationName", typeof(string));
			this.keyName = (string)info.GetValue("keyName", typeof(string));
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0001B2F5 File Offset: 0x000194F5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("operationName", this.operationName);
			info.AddValue("keyName", this.keyName);
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x0001B321 File Offset: 0x00019521
		public string OperationName
		{
			get
			{
				return this.operationName;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x0001B329 File Offset: 0x00019529
		public string KeyName
		{
			get
			{
				return this.keyName;
			}
		}

		// Token: 0x0400070E RID: 1806
		private readonly string operationName;

		// Token: 0x0400070F RID: 1807
		private readonly string keyName;
	}
}
