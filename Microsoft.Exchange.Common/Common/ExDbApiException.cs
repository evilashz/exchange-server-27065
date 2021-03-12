using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000025 RID: 37
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExDbApiException : LocalizedException
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x0000417D File Offset: 0x0000237D
		public ExDbApiException(Win32Exception ex) : base(CommonStrings.ExDbApiException(ex))
		{
			this.ex = ex;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004192 File Offset: 0x00002392
		public ExDbApiException(Win32Exception ex, Exception innerException) : base(CommonStrings.ExDbApiException(ex), innerException)
		{
			this.ex = ex;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000041A8 File Offset: 0x000023A8
		protected ExDbApiException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ex = (Win32Exception)info.GetValue("ex", typeof(Win32Exception));
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000041D2 File Offset: 0x000023D2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ex", this.ex);
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000041ED File Offset: 0x000023ED
		public Win32Exception Ex
		{
			get
			{
				return this.ex;
			}
		}

		// Token: 0x0400007F RID: 127
		private readonly Win32Exception ex;
	}
}
