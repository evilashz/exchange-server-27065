using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200006D RID: 109
	public sealed class OrderedMailboxProvisioningConstraint : MailboxProvisioningConstraint
	{
		// Token: 0x060004FF RID: 1279 RVA: 0x0001C69D File Offset: 0x0001A89D
		public OrderedMailboxProvisioningConstraint()
		{
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001C6A5 File Offset: 0x0001A8A5
		public OrderedMailboxProvisioningConstraint(int index, string value) : base(value)
		{
			this.Index = index;
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x0001C6B5 File Offset: 0x0001A8B5
		// (set) Token: 0x06000502 RID: 1282 RVA: 0x0001C6BD File Offset: 0x0001A8BD
		[XmlAttribute("P")]
		public int Index { get; set; }
	}
}
