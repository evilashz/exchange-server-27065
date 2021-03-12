using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200002E RID: 46
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class PartnerAuthenticationException : PermanentOperationLevelForItemsException
	{
		// Token: 0x06000180 RID: 384 RVA: 0x000056F2 File Offset: 0x000038F2
		public PartnerAuthenticationException(int statusCode) : base(Strings.PartnerAuthenticationException(statusCode))
		{
			this.statusCode = statusCode;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005707 File Offset: 0x00003907
		public PartnerAuthenticationException(int statusCode, Exception innerException) : base(Strings.PartnerAuthenticationException(statusCode), innerException)
		{
			this.statusCode = statusCode;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000571D File Offset: 0x0000391D
		protected PartnerAuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.statusCode = (int)info.GetValue("statusCode", typeof(int));
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00005747 File Offset: 0x00003947
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("statusCode", this.statusCode);
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00005762 File Offset: 0x00003962
		public int StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		// Token: 0x040000E5 RID: 229
		private readonly int statusCode;
	}
}
