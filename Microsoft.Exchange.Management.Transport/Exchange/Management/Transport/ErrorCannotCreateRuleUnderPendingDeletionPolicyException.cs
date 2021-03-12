using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000174 RID: 372
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ErrorCannotCreateRuleUnderPendingDeletionPolicyException : LocalizedException
	{
		// Token: 0x06000F40 RID: 3904 RVA: 0x0003609D File Offset: 0x0003429D
		public ErrorCannotCreateRuleUnderPendingDeletionPolicyException(string name) : base(Strings.ErrorCannotCreateRuleUnderPendingDeletionPolicy(name))
		{
			this.name = name;
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x000360B2 File Offset: 0x000342B2
		public ErrorCannotCreateRuleUnderPendingDeletionPolicyException(string name, Exception innerException) : base(Strings.ErrorCannotCreateRuleUnderPendingDeletionPolicy(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x000360C8 File Offset: 0x000342C8
		protected ErrorCannotCreateRuleUnderPendingDeletionPolicyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x000360F2 File Offset: 0x000342F2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06000F44 RID: 3908 RVA: 0x0003610D File Offset: 0x0003430D
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0400067C RID: 1660
		private readonly string name;
	}
}
