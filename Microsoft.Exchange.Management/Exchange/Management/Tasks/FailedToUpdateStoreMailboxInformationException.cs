using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EF8 RID: 3832
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToUpdateStoreMailboxInformationException : LocalizedException
	{
		// Token: 0x0600A9C2 RID: 43458 RVA: 0x0028C4A6 File Offset: 0x0028A6A6
		public FailedToUpdateStoreMailboxInformationException(string identity) : base(Strings.ErrorFailedToUpdateStoreMailboxInformationException(identity))
		{
			this.identity = identity;
		}

		// Token: 0x0600A9C3 RID: 43459 RVA: 0x0028C4BB File Offset: 0x0028A6BB
		public FailedToUpdateStoreMailboxInformationException(string identity, Exception innerException) : base(Strings.ErrorFailedToUpdateStoreMailboxInformationException(identity), innerException)
		{
			this.identity = identity;
		}

		// Token: 0x0600A9C4 RID: 43460 RVA: 0x0028C4D1 File Offset: 0x0028A6D1
		protected FailedToUpdateStoreMailboxInformationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
		}

		// Token: 0x0600A9C5 RID: 43461 RVA: 0x0028C4FB File Offset: 0x0028A6FB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
		}

		// Token: 0x170036FF RID: 14079
		// (get) Token: 0x0600A9C6 RID: 43462 RVA: 0x0028C516 File Offset: 0x0028A716
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x04006065 RID: 24677
		private readonly string identity;
	}
}
