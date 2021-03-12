using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.RightsManagement;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000426 RID: 1062
	[DataContract]
	public class RmsTemplate : EnumValue
	{
		// Token: 0x0600355F RID: 13663 RVA: 0x000A5EE1 File Offset: 0x000A40E1
		public RmsTemplate(RmsTemplatePresentation taskRMSTemplate) : base(taskRMSTemplate.Name, taskRMSTemplate.Identity.ToString())
		{
		}
	}
}
