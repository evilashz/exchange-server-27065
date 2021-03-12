using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010D5 RID: 4309
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EtrHasE4eActionException : LocalizedException
	{
		// Token: 0x0600B31A RID: 45850 RVA: 0x0029ABDE File Offset: 0x00298DDE
		public EtrHasE4eActionException(string ruleName) : base(Strings.EtrHasE4eAction(ruleName))
		{
			this.ruleName = ruleName;
		}

		// Token: 0x0600B31B RID: 45851 RVA: 0x0029ABF3 File Offset: 0x00298DF3
		public EtrHasE4eActionException(string ruleName, Exception innerException) : base(Strings.EtrHasE4eAction(ruleName), innerException)
		{
			this.ruleName = ruleName;
		}

		// Token: 0x0600B31C RID: 45852 RVA: 0x0029AC09 File Offset: 0x00298E09
		protected EtrHasE4eActionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ruleName = (string)info.GetValue("ruleName", typeof(string));
		}

		// Token: 0x0600B31D RID: 45853 RVA: 0x0029AC33 File Offset: 0x00298E33
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ruleName", this.ruleName);
		}

		// Token: 0x170038E3 RID: 14563
		// (get) Token: 0x0600B31E RID: 45854 RVA: 0x0029AC4E File Offset: 0x00298E4E
		public string RuleName
		{
			get
			{
				return this.ruleName;
			}
		}

		// Token: 0x04006249 RID: 25161
		private readonly string ruleName;
	}
}
