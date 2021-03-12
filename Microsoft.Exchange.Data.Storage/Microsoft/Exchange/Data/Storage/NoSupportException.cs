using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000115 RID: 277
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class NoSupportException : StoragePermanentException
	{
		// Token: 0x060013F9 RID: 5113 RVA: 0x0006A0F5 File Offset: 0x000682F5
		public NoSupportException(LocalizedString exceptionMessage) : base(ServerStrings.NoSupportException(exceptionMessage))
		{
			this.exceptionMessage = exceptionMessage;
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x0006A10A File Offset: 0x0006830A
		public NoSupportException(LocalizedString exceptionMessage, Exception innerException) : base(ServerStrings.NoSupportException(exceptionMessage), innerException)
		{
			this.exceptionMessage = exceptionMessage;
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x0006A120 File Offset: 0x00068320
		protected NoSupportException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.exceptionMessage = (LocalizedString)info.GetValue("exceptionMessage", typeof(LocalizedString));
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x0006A14A File Offset: 0x0006834A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("exceptionMessage", this.exceptionMessage);
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x060013FD RID: 5117 RVA: 0x0006A16A File Offset: 0x0006836A
		public LocalizedString ExceptionMessage
		{
			get
			{
				return this.exceptionMessage;
			}
		}

		// Token: 0x040009A4 RID: 2468
		private readonly LocalizedString exceptionMessage;
	}
}
