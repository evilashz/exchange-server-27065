using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000023 RID: 35
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotDetermineExchangeModeException : LocalizedException
	{
		// Token: 0x060000AB RID: 171 RVA: 0x0000408D File Offset: 0x0000228D
		public CannotDetermineExchangeModeException(string reason) : base(CommonStrings.CannotDetermineExchangeModeException(reason))
		{
			this.reason = reason;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000040A2 File Offset: 0x000022A2
		public CannotDetermineExchangeModeException(string reason, Exception innerException) : base(CommonStrings.CannotDetermineExchangeModeException(reason), innerException)
		{
			this.reason = reason;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000040B8 File Offset: 0x000022B8
		protected CannotDetermineExchangeModeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000040E2 File Offset: 0x000022E2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000040FD File Offset: 0x000022FD
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x0400007D RID: 125
		private readonly string reason;
	}
}
