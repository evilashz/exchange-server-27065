using System;
using System.Globalization;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000027 RID: 39
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	public class NewMailSubscription : BaseSubscription
	{
		// Token: 0x060000DF RID: 223 RVA: 0x00003926 File Offset: 0x00001B26
		public NewMailSubscription() : base(NotificationType.NewMail)
		{
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000392F File Offset: 0x00001B2F
		protected override bool Validate()
		{
			return base.Validate() && base.CultureInfo.Equals(CultureInfo.InvariantCulture);
		}
	}
}
