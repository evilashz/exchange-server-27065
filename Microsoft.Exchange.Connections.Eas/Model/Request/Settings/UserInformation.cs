using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Eas.Model.Response.AirSync;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.Settings
{
	// Token: 0x020000A6 RID: 166
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "Settings", TypeName = "UserInformation")]
	public class UserInformation
	{
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x0000A750 File Offset: 0x00008950
		// (set) Token: 0x06000421 RID: 1057 RVA: 0x0000A761 File Offset: 0x00008961
		[XmlElement(ElementName = "Get")]
		public EmptyTag SerializableGet
		{
			get
			{
				if (!this.Get)
				{
					return null;
				}
				return new EmptyTag();
			}
			set
			{
				this.Get = (value != null);
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0000A770 File Offset: 0x00008970
		// (set) Token: 0x06000423 RID: 1059 RVA: 0x0000A778 File Offset: 0x00008978
		[XmlIgnore]
		public bool Get { get; set; }
	}
}
