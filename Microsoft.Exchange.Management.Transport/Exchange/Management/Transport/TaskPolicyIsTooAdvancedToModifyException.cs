using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x0200017A RID: 378
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class TaskPolicyIsTooAdvancedToModifyException : TaskObjectIsTooAdvancedException
	{
		// Token: 0x06000F5B RID: 3931 RVA: 0x0003628A File Offset: 0x0003448A
		public TaskPolicyIsTooAdvancedToModifyException(string identity) : base(Strings.ErrorTaskPolicyIsTooAdvancedToModify(identity))
		{
			this.identity = identity;
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0003629F File Offset: 0x0003449F
		public TaskPolicyIsTooAdvancedToModifyException(string identity, Exception innerException) : base(Strings.ErrorTaskPolicyIsTooAdvancedToModify(identity), innerException)
		{
			this.identity = identity;
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x000362B5 File Offset: 0x000344B5
		protected TaskPolicyIsTooAdvancedToModifyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x000362DF File Offset: 0x000344DF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06000F5F RID: 3935 RVA: 0x000362FA File Offset: 0x000344FA
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x0400067F RID: 1663
		private readonly string identity;
	}
}
