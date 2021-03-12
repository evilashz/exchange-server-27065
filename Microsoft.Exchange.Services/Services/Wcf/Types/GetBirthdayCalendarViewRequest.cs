using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A42 RID: 2626
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetBirthdayCalendarViewRequest : BaseRequest
	{
		// Token: 0x170010BC RID: 4284
		// (get) Token: 0x06004A30 RID: 18992 RVA: 0x00103696 File Offset: 0x00101896
		// (set) Token: 0x06004A31 RID: 18993 RVA: 0x0010369E File Offset: 0x0010189E
		[XmlIgnore]
		[DataMember(IsRequired = true)]
		public string StartRange { get; set; }

		// Token: 0x170010BD RID: 4285
		// (get) Token: 0x06004A32 RID: 18994 RVA: 0x001036A7 File Offset: 0x001018A7
		// (set) Token: 0x06004A33 RID: 18995 RVA: 0x001036AF File Offset: 0x001018AF
		[DataMember(IsRequired = true)]
		[XmlIgnore]
		public string EndRange { get; set; }

		// Token: 0x06004A34 RID: 18996 RVA: 0x001036B8 File Offset: 0x001018B8
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetBirthdayCalendarViewCommand(callContext, this);
		}

		// Token: 0x06004A35 RID: 18997 RVA: 0x001036C1 File Offset: 0x001018C1
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}

		// Token: 0x06004A36 RID: 18998 RVA: 0x001036CB File Offset: 0x001018CB
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}
	}
}
