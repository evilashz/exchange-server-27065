using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.ClientAccessRules
{
	// Token: 0x020000EB RID: 235
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ClientAccessRulesBlockedConnectionException : LocalizedException
	{
		// Token: 0x06000835 RID: 2101 RVA: 0x0001B2E6 File Offset: 0x000194E6
		public ClientAccessRulesBlockedConnectionException(string ruleName) : base(DataStrings.ClientAccessRulesBlockedConnection(ruleName))
		{
			this.ruleName = ruleName;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0001B2FB File Offset: 0x000194FB
		public ClientAccessRulesBlockedConnectionException(string ruleName, Exception innerException) : base(DataStrings.ClientAccessRulesBlockedConnection(ruleName), innerException)
		{
			this.ruleName = ruleName;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0001B311 File Offset: 0x00019511
		protected ClientAccessRulesBlockedConnectionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ruleName = (string)info.GetValue("ruleName", typeof(string));
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0001B33B File Offset: 0x0001953B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ruleName", this.ruleName);
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x0001B356 File Offset: 0x00019556
		public string RuleName
		{
			get
			{
				return this.ruleName;
			}
		}

		// Token: 0x0400058F RID: 1423
		private readonly string ruleName;
	}
}
