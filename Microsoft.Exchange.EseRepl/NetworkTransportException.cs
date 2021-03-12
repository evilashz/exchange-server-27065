using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000044 RID: 68
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NetworkTransportException : TransientException
	{
		// Token: 0x06000242 RID: 578 RVA: 0x00008CF3 File Offset: 0x00006EF3
		public NetworkTransportException(string err) : base(Strings.NetworkTransportError(err))
		{
			this.err = err;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00008D08 File Offset: 0x00006F08
		public NetworkTransportException(string err, Exception innerException) : base(Strings.NetworkTransportError(err), innerException)
		{
			this.err = err;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00008D1E File Offset: 0x00006F1E
		protected NetworkTransportException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.err = (string)info.GetValue("err", typeof(string));
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00008D48 File Offset: 0x00006F48
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("err", this.err);
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00008D63 File Offset: 0x00006F63
		public string Err
		{
			get
			{
				return this.err;
			}
		}

		// Token: 0x04000158 RID: 344
		private readonly string err;
	}
}
