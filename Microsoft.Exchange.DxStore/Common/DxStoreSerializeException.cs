using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x020000A9 RID: 169
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DxStoreSerializeException : LocalizedException
	{
		// Token: 0x0600060D RID: 1549 RVA: 0x000148D2 File Offset: 0x00012AD2
		public DxStoreSerializeException(string err) : base(Strings.SerializeError(err))
		{
			this.err = err;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x000148E7 File Offset: 0x00012AE7
		public DxStoreSerializeException(string err, Exception innerException) : base(Strings.SerializeError(err), innerException)
		{
			this.err = err;
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x000148FD File Offset: 0x00012AFD
		protected DxStoreSerializeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.err = (string)info.GetValue("err", typeof(string));
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00014927 File Offset: 0x00012B27
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("err", this.err);
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x00014942 File Offset: 0x00012B42
		public string Err
		{
			get
			{
				return this.err;
			}
		}

		// Token: 0x04000297 RID: 663
		private readonly string err;
	}
}
