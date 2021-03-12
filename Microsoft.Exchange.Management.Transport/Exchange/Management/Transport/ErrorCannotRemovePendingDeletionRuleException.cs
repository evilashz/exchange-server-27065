using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000172 RID: 370
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ErrorCannotRemovePendingDeletionRuleException : LocalizedException
	{
		// Token: 0x06000F36 RID: 3894 RVA: 0x00035FAD File Offset: 0x000341AD
		public ErrorCannotRemovePendingDeletionRuleException(string name) : base(Strings.ErrorCannotRemovePendingDeletionRule(name))
		{
			this.name = name;
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x00035FC2 File Offset: 0x000341C2
		public ErrorCannotRemovePendingDeletionRuleException(string name, Exception innerException) : base(Strings.ErrorCannotRemovePendingDeletionRule(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x00035FD8 File Offset: 0x000341D8
		protected ErrorCannotRemovePendingDeletionRuleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x00036002 File Offset: 0x00034202
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06000F3A RID: 3898 RVA: 0x0003601D File Offset: 0x0003421D
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0400067A RID: 1658
		private readonly string name;
	}
}
