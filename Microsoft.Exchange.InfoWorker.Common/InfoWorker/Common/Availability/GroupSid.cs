using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000CC RID: 204
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class GroupSid
	{
		// Token: 0x06000535 RID: 1333 RVA: 0x00016DC4 File Offset: 0x00014FC4
		public override string ToString()
		{
			return string.Format("Security Identifier: {0}, Attributes: {1:x}", this.SecurityIdentifier, this.Attributes);
		}

		// Token: 0x0400030A RID: 778
		[XmlElement]
		public string SecurityIdentifier;

		// Token: 0x0400030B RID: 779
		[XmlAttribute]
		public uint Attributes;
	}
}
