using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02001169 RID: 4457
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CorruptFilterRuleException : LocalizedException
	{
		// Token: 0x0600B5F8 RID: 46584 RVA: 0x0029F153 File Offset: 0x0029D353
		public CorruptFilterRuleException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600B5F9 RID: 46585 RVA: 0x0029F15C File Offset: 0x0029D35C
		public CorruptFilterRuleException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600B5FA RID: 46586 RVA: 0x0029F166 File Offset: 0x0029D366
		protected CorruptFilterRuleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B5FB RID: 46587 RVA: 0x0029F170 File Offset: 0x0029D370
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
