using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x02000025 RID: 37
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CompressionException : LocalizedException
	{
		// Token: 0x060003D5 RID: 981 RVA: 0x0000D3EB File Offset: 0x0000B5EB
		public CompressionException(int errCode) : base(CoreStrings.CompressionError(errCode))
		{
			this.errCode = errCode;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000D400 File Offset: 0x0000B600
		public CompressionException(int errCode, Exception innerException) : base(CoreStrings.CompressionError(errCode), innerException)
		{
			this.errCode = errCode;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000D416 File Offset: 0x0000B616
		protected CompressionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errCode = (int)info.GetValue("errCode", typeof(int));
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000D440 File Offset: 0x0000B640
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errCode", this.errCode);
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000D45B File Offset: 0x0000B65B
		public int ErrCode
		{
			get
			{
				return this.errCode;
			}
		}

		// Token: 0x04000368 RID: 872
		private readonly int errCode;
	}
}
