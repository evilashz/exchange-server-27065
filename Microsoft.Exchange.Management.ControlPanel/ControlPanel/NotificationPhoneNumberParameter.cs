using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000448 RID: 1096
	[DataContract]
	public class NotificationPhoneNumberParameter : FormletParameter
	{
		// Token: 0x06003628 RID: 13864 RVA: 0x000A79D0 File Offset: 0x000A5BD0
		public NotificationPhoneNumberParameter(string name) : base(name, LocalizedString.Empty, LocalizedString.Empty)
		{
		}
	}
}
