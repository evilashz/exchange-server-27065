using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000179 RID: 377
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class TaskRuleIsTooAdvancedToModifyException : TaskObjectIsTooAdvancedException
	{
		// Token: 0x06000F56 RID: 3926 RVA: 0x00036212 File Offset: 0x00034412
		public TaskRuleIsTooAdvancedToModifyException(string identity) : base(Strings.ErrorTaskRuleIsTooAdvancedToModify(identity))
		{
			this.identity = identity;
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x00036227 File Offset: 0x00034427
		public TaskRuleIsTooAdvancedToModifyException(string identity, Exception innerException) : base(Strings.ErrorTaskRuleIsTooAdvancedToModify(identity), innerException)
		{
			this.identity = identity;
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0003623D File Offset: 0x0003443D
		protected TaskRuleIsTooAdvancedToModifyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x00036267 File Offset: 0x00034467
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x00036282 File Offset: 0x00034482
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x0400067E RID: 1662
		private readonly string identity;
	}
}
