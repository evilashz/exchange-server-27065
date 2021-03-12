using System;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000233 RID: 563
	[XmlRoot(IsNullable = false, Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlType(TypeName = "RequestServerVersion", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class NotificationRequestServerVersion : SoapHeader
	{
		// Token: 0x06000E92 RID: 3730 RVA: 0x0004767C File Offset: 0x0004587C
		public NotificationRequestServerVersion()
		{
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x00047684 File Offset: 0x00045884
		public NotificationRequestServerVersion(ExchangeVersionType version)
		{
			this.Version = version;
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000E94 RID: 3732 RVA: 0x00047693 File Offset: 0x00045893
		// (set) Token: 0x06000E95 RID: 3733 RVA: 0x0004769B File Offset: 0x0004589B
		[XmlAttribute(AttributeName = "Version")]
		public ExchangeVersionType Version
		{
			get
			{
				return this.versionField;
			}
			set
			{
				this.versionField = value;
			}
		}

		// Token: 0x04000B33 RID: 2867
		private ExchangeVersionType versionField;
	}
}
