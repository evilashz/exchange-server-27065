using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020002CA RID: 714
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PooledConnectionOpenTimeoutException : TransientException
	{
		// Token: 0x0600195A RID: 6490 RVA: 0x0005CFD9 File Offset: 0x0005B1D9
		public PooledConnectionOpenTimeoutException(string msg) : base(Strings.PooledConnectionOpenTimeoutError(msg))
		{
			this.msg = msg;
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x0005CFEE File Offset: 0x0005B1EE
		public PooledConnectionOpenTimeoutException(string msg, Exception innerException) : base(Strings.PooledConnectionOpenTimeoutError(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x0005D004 File Offset: 0x0005B204
		protected PooledConnectionOpenTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x0005D02E File Offset: 0x0005B22E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x0600195E RID: 6494 RVA: 0x0005D049 File Offset: 0x0005B249
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x04000997 RID: 2455
		private readonly string msg;
	}
}
