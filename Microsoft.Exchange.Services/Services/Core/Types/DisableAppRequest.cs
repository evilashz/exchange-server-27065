using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.ApplicationLogic.Extension;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000416 RID: 1046
	[XmlType("DisableAppRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class DisableAppRequest : BaseRequest
	{
		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001E00 RID: 7680 RVA: 0x0009FAEE File Offset: 0x0009DCEE
		// (set) Token: 0x06001E01 RID: 7681 RVA: 0x0009FAF6 File Offset: 0x0009DCF6
		[XmlElement]
		public string ID { get; set; }

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001E02 RID: 7682 RVA: 0x0009FAFF File Offset: 0x0009DCFF
		// (set) Token: 0x06001E03 RID: 7683 RVA: 0x0009FB07 File Offset: 0x0009DD07
		[IgnoreDataMember]
		[XmlElement]
		public DisableReasonType DisableReason { get; set; }

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001E04 RID: 7684 RVA: 0x0009FB10 File Offset: 0x0009DD10
		// (set) Token: 0x06001E05 RID: 7685 RVA: 0x0009FB1D File Offset: 0x0009DD1D
		[XmlIgnore]
		[DataMember(Name = "DisableReason", IsRequired = true)]
		public string DisableReasonString
		{
			get
			{
				return EnumUtilities.ToString<DisableReasonType>(this.DisableReason);
			}
			set
			{
				this.DisableReason = EnumUtilities.Parse<DisableReasonType>(value);
			}
		}

		// Token: 0x06001E06 RID: 7686 RVA: 0x0009FB2B File Offset: 0x0009DD2B
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new DisableApp(callContext, this);
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x0009FB34 File Offset: 0x0009DD34
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x0009FB37 File Offset: 0x0009DD37
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
