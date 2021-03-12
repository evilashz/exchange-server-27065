using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000421 RID: 1057
	[DataContract]
	public class MessageClassification : EnumValue
	{
		// Token: 0x06003552 RID: 13650 RVA: 0x000A59C4 File Offset: 0x000A3BC4
		public MessageClassification(MessageClassification classification) : base(classification.DisplayName, classification.Guid.ToString())
		{
			this.PermissionMenuVisible = classification.PermissionMenuVisible;
		}

		// Token: 0x170020EE RID: 8430
		// (get) Token: 0x06003553 RID: 13651 RVA: 0x000A59FD File Offset: 0x000A3BFD
		// (set) Token: 0x06003554 RID: 13652 RVA: 0x000A5A05 File Offset: 0x000A3C05
		internal bool PermissionMenuVisible { get; private set; }
	}
}
