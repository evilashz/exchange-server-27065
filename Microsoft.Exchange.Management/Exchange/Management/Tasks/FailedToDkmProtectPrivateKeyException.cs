using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010F3 RID: 4339
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToDkmProtectPrivateKeyException : LocalizedException
	{
		// Token: 0x0600B3AA RID: 45994 RVA: 0x0029B8C0 File Offset: 0x00299AC0
		public FailedToDkmProtectPrivateKeyException(Exception ex) : base(Strings.FailedToDkmProtectPrivateKey(ex))
		{
			this.ex = ex;
		}

		// Token: 0x0600B3AB RID: 45995 RVA: 0x0029B8D5 File Offset: 0x00299AD5
		public FailedToDkmProtectPrivateKeyException(Exception ex, Exception innerException) : base(Strings.FailedToDkmProtectPrivateKey(ex), innerException)
		{
			this.ex = ex;
		}

		// Token: 0x0600B3AC RID: 45996 RVA: 0x0029B8EB File Offset: 0x00299AEB
		protected FailedToDkmProtectPrivateKeyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ex = (Exception)info.GetValue("ex", typeof(Exception));
		}

		// Token: 0x0600B3AD RID: 45997 RVA: 0x0029B915 File Offset: 0x00299B15
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ex", this.ex);
		}

		// Token: 0x170038FB RID: 14587
		// (get) Token: 0x0600B3AE RID: 45998 RVA: 0x0029B930 File Offset: 0x00299B30
		public Exception Ex
		{
			get
			{
				return this.ex;
			}
		}

		// Token: 0x04006261 RID: 25185
		private readonly Exception ex;
	}
}
