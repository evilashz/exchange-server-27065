using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000631 RID: 1585
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "ResolutionSet")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", TypeName = "ResolutionSet")]
	[Serializable]
	public class ResolutionSet
	{
		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06003168 RID: 12648 RVA: 0x000B70CA File Offset: 0x000B52CA
		// (set) Token: 0x06003169 RID: 12649 RVA: 0x000B70D2 File Offset: 0x000B52D2
		[DataMember(Name = "TotalItemsInView", IsRequired = true)]
		[XmlAttribute]
		public int TotalItemsInView { get; set; }

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x0600316A RID: 12650 RVA: 0x000B70DB File Offset: 0x000B52DB
		// (set) Token: 0x0600316B RID: 12651 RVA: 0x000B70E3 File Offset: 0x000B52E3
		[DataMember(Name = "IncludesLastItemInRange", IsRequired = true)]
		[XmlAttribute]
		public bool IncludesLastItemInRange { get; set; }

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x0600316C RID: 12652 RVA: 0x000B70EC File Offset: 0x000B52EC
		// (set) Token: 0x0600316D RID: 12653 RVA: 0x000B70F9 File Offset: 0x000B52F9
		[DataMember(EmitDefaultValue = false, Order = 1)]
		[XmlElement("Resolution", IsNullable = false, Type = typeof(ResolutionType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public ResolutionType[] Resolutions
		{
			get
			{
				return this.resolutions.ToArray();
			}
			set
			{
				this.resolutions = new List<ResolutionType>(value);
			}
		}

		// Token: 0x0600316E RID: 12654 RVA: 0x000B7107 File Offset: 0x000B5307
		internal void Add(ResolutionType resolution)
		{
			if (this.resolutions == null)
			{
				this.resolutions = new List<ResolutionType>();
			}
			this.resolutions.Add(resolution);
		}

		// Token: 0x04001C50 RID: 7248
		private List<ResolutionType> resolutions;
	}
}
