using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Common.DiskManagement
{
	// Token: 0x0200000D RID: 13
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class BitlockerUtilException : LocalizedException
	{
		// Token: 0x06000049 RID: 73 RVA: 0x0000447F File Offset: 0x0000267F
		public BitlockerUtilException(string errorMsg) : base(DiskManagementStrings.BitlockerUtilError(errorMsg))
		{
			this.errorMsg = errorMsg;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00004494 File Offset: 0x00002694
		public BitlockerUtilException(string errorMsg, Exception innerException) : base(DiskManagementStrings.BitlockerUtilError(errorMsg), innerException)
		{
			this.errorMsg = errorMsg;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000044AA File Offset: 0x000026AA
		protected BitlockerUtilException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMsg = (string)info.GetValue("errorMsg", typeof(string));
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000044D4 File Offset: 0x000026D4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMsg", this.errorMsg);
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000044EF File Offset: 0x000026EF
		public string ErrorMsg
		{
			get
			{
				return this.errorMsg;
			}
		}

		// Token: 0x0400004B RID: 75
		private readonly string errorMsg;
	}
}
