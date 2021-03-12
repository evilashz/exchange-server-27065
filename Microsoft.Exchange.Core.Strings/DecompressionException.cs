using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x02000026 RID: 38
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DecompressionException : LocalizedException
	{
		// Token: 0x060003DA RID: 986 RVA: 0x0000D463 File Offset: 0x0000B663
		public DecompressionException(int errCode) : base(CoreStrings.DecompressionError(errCode))
		{
			this.errCode = errCode;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000D478 File Offset: 0x0000B678
		public DecompressionException(int errCode, Exception innerException) : base(CoreStrings.DecompressionError(errCode), innerException)
		{
			this.errCode = errCode;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000D48E File Offset: 0x0000B68E
		protected DecompressionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errCode = (int)info.GetValue("errCode", typeof(int));
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000D4B8 File Offset: 0x0000B6B8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errCode", this.errCode);
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0000D4D3 File Offset: 0x0000B6D3
		public int ErrCode
		{
			get
			{
				return this.errCode;
			}
		}

		// Token: 0x04000369 RID: 873
		private readonly int errCode;
	}
}
