using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200041D RID: 1053
	[DataContract]
	public class MessageCategory : EnumValue
	{
		// Token: 0x0600354C RID: 13644 RVA: 0x000A5983 File Offset: 0x000A3B83
		public MessageCategory(MessageCategory category) : base(category.Name, category.Name)
		{
		}
	}
}
