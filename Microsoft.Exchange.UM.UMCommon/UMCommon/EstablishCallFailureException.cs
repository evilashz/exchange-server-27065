using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001DF RID: 479
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EstablishCallFailureException : LocalizedException
	{
		// Token: 0x06000F7B RID: 3963 RVA: 0x000367FD File Offset: 0x000349FD
		public EstablishCallFailureException(string s) : base(Strings.OutboundCallFailure(s))
		{
			this.s = s;
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x00036812 File Offset: 0x00034A12
		public EstablishCallFailureException(string s, Exception innerException) : base(Strings.OutboundCallFailure(s), innerException)
		{
			this.s = s;
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x00036828 File Offset: 0x00034A28
		protected EstablishCallFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.s = (string)info.GetValue("s", typeof(string));
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x00036852 File Offset: 0x00034A52
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("s", this.s);
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x0003686D File Offset: 0x00034A6D
		public string S
		{
			get
			{
				return this.s;
			}
		}

		// Token: 0x040007AF RID: 1967
		private readonly string s;
	}
}
