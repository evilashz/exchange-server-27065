using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000E6 RID: 230
	[DataContract]
	public abstract class SelfMailboxParameters : SetObjectProperties
	{
		// Token: 0x06001E35 RID: 7733 RVA: 0x0005B66C File Offset: 0x0005986C
		public SelfMailboxParameters()
		{
			this.OnDeserializing(default(StreamingContext));
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x0005B68E File Offset: 0x0005988E
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			base["Mailbox"] = Identity.FromExecutingUserId();
		}

		// Token: 0x04001C09 RID: 7177
		public const string RbacParameters = "?Mailbox";

		// Token: 0x04001C0A RID: 7178
		protected const string IdentitySuffix = "&Identity";

		// Token: 0x04001C0B RID: 7179
		public const string RbacParametersWithIdentity = "?Mailbox&Identity";
	}
}
