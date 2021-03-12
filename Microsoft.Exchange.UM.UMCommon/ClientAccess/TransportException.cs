using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.ClientAccess
{
	// Token: 0x020001AC RID: 428
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TransportException : LocalizedException
	{
		// Token: 0x06000E86 RID: 3718 RVA: 0x00035261 File Offset: 0x00033461
		public TransportException(string msg) : base(Strings.TransportException(msg))
		{
			this.msg = msg;
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x00035276 File Offset: 0x00033476
		public TransportException(string msg, Exception innerException) : base(Strings.TransportException(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0003528C File Offset: 0x0003348C
		protected TransportException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x000352B6 File Offset: 0x000334B6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000E8A RID: 3722 RVA: 0x000352D1 File Offset: 0x000334D1
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x04000786 RID: 1926
		private readonly string msg;
	}
}
