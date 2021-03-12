using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B1B RID: 2843
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class FindTrendingConversationRequest : BaseRequest
	{
		// Token: 0x1700134E RID: 4942
		// (get) Token: 0x060050A3 RID: 20643 RVA: 0x00109CA5 File Offset: 0x00107EA5
		// (set) Token: 0x060050A4 RID: 20644 RVA: 0x00109CAD File Offset: 0x00107EAD
		[XmlIgnore]
		[DataMember(Name = "ParentFolderId", IsRequired = true)]
		public TargetFolderId ParentFolderId { get; set; }

		// Token: 0x1700134F RID: 4943
		// (get) Token: 0x060050A5 RID: 20645 RVA: 0x00109CB6 File Offset: 0x00107EB6
		// (set) Token: 0x060050A6 RID: 20646 RVA: 0x00109CBE File Offset: 0x00107EBE
		[XmlIgnore]
		[DataMember(Name = "PageSize", IsRequired = true)]
		public int PageSize { get; set; }

		// Token: 0x060050A7 RID: 20647 RVA: 0x00109CC7 File Offset: 0x00107EC7
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new FindTrendingConversationCommand(callContext, this);
		}

		// Token: 0x060050A8 RID: 20648 RVA: 0x00109CD0 File Offset: 0x00107ED0
		internal override void Validate()
		{
			base.Validate();
		}

		// Token: 0x060050A9 RID: 20649 RVA: 0x00109CD8 File Offset: 0x00107ED8
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}

		// Token: 0x060050AA RID: 20650 RVA: 0x00109CE2 File Offset: 0x00107EE2
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}
	}
}
