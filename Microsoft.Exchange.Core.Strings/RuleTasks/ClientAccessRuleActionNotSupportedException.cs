using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core.RuleTasks
{
	// Token: 0x0200002C RID: 44
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClientAccessRuleActionNotSupportedException : LocalizedException
	{
		// Token: 0x06000618 RID: 1560 RVA: 0x000133C4 File Offset: 0x000115C4
		public ClientAccessRuleActionNotSupportedException(string action) : base(RulesTasksStrings.ClientAccessRuleActionNotSupported(action))
		{
			this.action = action;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x000133D9 File Offset: 0x000115D9
		public ClientAccessRuleActionNotSupportedException(string action, Exception innerException) : base(RulesTasksStrings.ClientAccessRuleActionNotSupported(action), innerException)
		{
			this.action = action;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x000133EF File Offset: 0x000115EF
		protected ClientAccessRuleActionNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.action = (string)info.GetValue("action", typeof(string));
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00013419 File Offset: 0x00011619
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("action", this.action);
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x00013434 File Offset: 0x00011634
		public string Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x040005A3 RID: 1443
		private readonly string action;
	}
}
