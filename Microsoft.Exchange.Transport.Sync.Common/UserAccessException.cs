using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000034 RID: 52
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UserAccessException : PermanentOperationLevelForItemsException
	{
		// Token: 0x0600019E RID: 414 RVA: 0x000059C2 File Offset: 0x00003BC2
		public UserAccessException(int statusCode) : base(Strings.UserAccessException(statusCode))
		{
			this.statusCode = statusCode;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000059D7 File Offset: 0x00003BD7
		public UserAccessException(int statusCode, Exception innerException) : base(Strings.UserAccessException(statusCode), innerException)
		{
			this.statusCode = statusCode;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000059ED File Offset: 0x00003BED
		protected UserAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.statusCode = (int)info.GetValue("statusCode", typeof(int));
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00005A17 File Offset: 0x00003C17
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("statusCode", this.statusCode);
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00005A32 File Offset: 0x00003C32
		public int StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		// Token: 0x040000EB RID: 235
		private readonly int statusCode;
	}
}
