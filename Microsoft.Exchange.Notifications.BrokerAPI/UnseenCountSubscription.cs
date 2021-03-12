using System;
using System.Globalization;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200002B RID: 43
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	public class UnseenCountSubscription : BaseSubscription
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x00003A71 File Offset: 0x00001C71
		public UnseenCountSubscription() : base(NotificationType.UnseenCount)
		{
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00003A7A File Offset: 0x00001C7A
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00003A82 File Offset: 0x00001C82
		[DataMember(EmitDefaultValue = false)]
		public string UserExternalDirectoryObjectId { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00003A8B File Offset: 0x00001C8B
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00003A93 File Offset: 0x00001C93
		[DataMember(EmitDefaultValue = false)]
		public string UserLegacyDN { get; set; }

		// Token: 0x060000F8 RID: 248 RVA: 0x00003A9C File Offset: 0x00001C9C
		protected override bool Validate()
		{
			return base.Validate() && (!string.IsNullOrEmpty(this.UserExternalDirectoryObjectId) || !string.IsNullOrEmpty(this.UserLegacyDN)) && base.CultureInfo.Equals(CultureInfo.InvariantCulture);
		}
	}
}
