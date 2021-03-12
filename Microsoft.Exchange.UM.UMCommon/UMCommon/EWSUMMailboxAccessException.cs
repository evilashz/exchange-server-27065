using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001E5 RID: 485
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class EWSUMMailboxAccessException : LocalizedException
	{
		// Token: 0x06000F97 RID: 3991 RVA: 0x00036A38 File Offset: 0x00034C38
		public EWSUMMailboxAccessException(string reason) : base(Strings.EWSUMMailboxAccessException(reason))
		{
			this.reason = reason;
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x00036A4D File Offset: 0x00034C4D
		public EWSUMMailboxAccessException(string reason, Exception innerException) : base(Strings.EWSUMMailboxAccessException(reason), innerException)
		{
			this.reason = reason;
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x00036A63 File Offset: 0x00034C63
		protected EWSUMMailboxAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x00036A8D File Offset: 0x00034C8D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000F9B RID: 3995 RVA: 0x00036AA8 File Offset: 0x00034CA8
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x040007B3 RID: 1971
		private readonly string reason;
	}
}
