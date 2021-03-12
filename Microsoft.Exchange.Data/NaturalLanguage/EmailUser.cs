using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x02000064 RID: 100
	public class EmailUser : IEquatable<EmailUser>
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000DACB File Offset: 0x0000BCCB
		// (set) Token: 0x06000333 RID: 819 RVA: 0x0000DAD3 File Offset: 0x0000BCD3
		[XmlText]
		public string Name { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000DADC File Offset: 0x0000BCDC
		// (set) Token: 0x06000335 RID: 821 RVA: 0x0000DAE4 File Offset: 0x0000BCE4
		[XmlAttribute("Id")]
		public string UserId { get; set; }

		// Token: 0x06000336 RID: 822 RVA: 0x0000DAED File Offset: 0x0000BCED
		public bool Equals(EmailUser other)
		{
			return other != null && this.UserId == other.UserId;
		}
	}
}
