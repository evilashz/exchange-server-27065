using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004F0 RID: 1264
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("GetComplianceConfigurationResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetComplianceConfigurationResponseMessage : ResponseMessage
	{
		// Token: 0x060024C4 RID: 9412 RVA: 0x000A5246 File Offset: 0x000A3446
		public GetComplianceConfigurationResponseMessage()
		{
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x000A524E File Offset: 0x000A344E
		internal GetComplianceConfigurationResponseMessage(IEnumerable<RmsTemplate> value, ServiceResultCode code = ServiceResultCode.Success, ServiceError error = null) : base(code, error)
		{
			this.InitializeRmsComplianceEntry(value);
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x000A525F File Offset: 0x000A345F
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetComplianceConfigurationResponseMessage;
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x000A5296 File Offset: 0x000A3496
		private void InitializeRmsComplianceEntry(IEnumerable<RmsTemplate> templates)
		{
			this.RmsTemplates = (from template in templates
			select new RmsComplianceEntry(template.Id.ToString(), template.Name, template.Description)).ToArray<RmsComplianceEntry>();
		}

		// Token: 0x04001595 RID: 5525
		public RmsComplianceEntry[] RmsTemplates;
	}
}
