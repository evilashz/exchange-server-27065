using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Entities.DataProviders
{
	// Token: 0x02000006 RID: 6
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidRequestException : StoragePermanentException
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002445 File Offset: 0x00000645
		public InvalidRequestException(LocalizedString violation) : base(Strings.InvalidRequest(violation))
		{
			this.violation = violation;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000245A File Offset: 0x0000065A
		public InvalidRequestException(LocalizedString violation, Exception innerException) : base(Strings.InvalidRequest(violation), innerException)
		{
			this.violation = violation;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002470 File Offset: 0x00000670
		protected InvalidRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.violation = (LocalizedString)info.GetValue("violation", typeof(LocalizedString));
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000249A File Offset: 0x0000069A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("violation", this.violation);
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000024BA File Offset: 0x000006BA
		public LocalizedString Violation
		{
			get
			{
				return this.violation;
			}
		}

		// Token: 0x04000016 RID: 22
		private readonly LocalizedString violation;
	}
}
