using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001E2 RID: 482
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ExclusionListException : LocalizedException
	{
		// Token: 0x06000F89 RID: 3977 RVA: 0x0003691C File Offset: 0x00034B1C
		public ExclusionListException(string msg) : base(Strings.ExclusionListException(msg))
		{
			this.msg = msg;
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x00036931 File Offset: 0x00034B31
		public ExclusionListException(string msg, Exception innerException) : base(Strings.ExclusionListException(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x00036947 File Offset: 0x00034B47
		protected ExclusionListException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x00036971 File Offset: 0x00034B71
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x0003698C File Offset: 0x00034B8C
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x040007B1 RID: 1969
		private readonly string msg;
	}
}
