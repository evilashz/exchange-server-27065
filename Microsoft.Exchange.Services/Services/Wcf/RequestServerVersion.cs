using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DC5 RID: 3525
	[XmlType(TypeName = "RequestServerVersion", AnonymousType = true, Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlRoot(IsNullable = false, Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RequestServerVersion
	{
		// Token: 0x1700147F RID: 5247
		// (get) Token: 0x060059A3 RID: 22947 RVA: 0x001183AE File Offset: 0x001165AE
		// (set) Token: 0x060059A4 RID: 22948 RVA: 0x001183B6 File Offset: 0x001165B6
		[DefaultValue(ExchangeVersionType.Exchange2010)]
		[XmlAttribute]
		[IgnoreDataMember]
		public ExchangeVersionType Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
				this.versionSpecified = true;
			}
		}

		// Token: 0x17001480 RID: 5248
		// (get) Token: 0x060059A5 RID: 22949 RVA: 0x001183C6 File Offset: 0x001165C6
		// (set) Token: 0x060059A6 RID: 22950 RVA: 0x001183CE File Offset: 0x001165CE
		[IgnoreDataMember]
		[XmlIgnore]
		public bool VersionSpecified
		{
			get
			{
				return this.versionSpecified;
			}
			set
			{
				this.versionSpecified = value;
			}
		}

		// Token: 0x17001481 RID: 5249
		// (get) Token: 0x060059A7 RID: 22951 RVA: 0x001183D7 File Offset: 0x001165D7
		// (set) Token: 0x060059A8 RID: 22952 RVA: 0x001183E4 File Offset: 0x001165E4
		[XmlIgnore]
		[DataMember(Name = "Version", IsRequired = true)]
		public string VersionString
		{
			get
			{
				return EnumUtilities.ToString<ExchangeVersionType>(this.Version);
			}
			set
			{
				this.Version = EnumUtilities.Parse<ExchangeVersionType>(value);
			}
		}

		// Token: 0x040031A3 RID: 12707
		private ExchangeVersionType version;

		// Token: 0x040031A4 RID: 12708
		private bool versionSpecified;
	}
}
