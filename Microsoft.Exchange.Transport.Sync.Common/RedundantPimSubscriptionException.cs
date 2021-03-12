using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000010 RID: 16
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class RedundantPimSubscriptionException : LocalizedException
	{
		// Token: 0x060000FD RID: 253 RVA: 0x00004C79 File Offset: 0x00002E79
		public RedundantPimSubscriptionException(string emailAddress) : base(Strings.RedundantPimSubscription(emailAddress))
		{
			this.emailAddress = emailAddress;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004C8E File Offset: 0x00002E8E
		public RedundantPimSubscriptionException(string emailAddress, Exception innerException) : base(Strings.RedundantPimSubscription(emailAddress), innerException)
		{
			this.emailAddress = emailAddress;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004CA4 File Offset: 0x00002EA4
		protected RedundantPimSubscriptionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.emailAddress = (string)info.GetValue("emailAddress", typeof(string));
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004CCE File Offset: 0x00002ECE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("emailAddress", this.emailAddress);
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00004CE9 File Offset: 0x00002EE9
		public string EmailAddress
		{
			get
			{
				return this.emailAddress;
			}
		}

		// Token: 0x040000D3 RID: 211
		private readonly string emailAddress;
	}
}
