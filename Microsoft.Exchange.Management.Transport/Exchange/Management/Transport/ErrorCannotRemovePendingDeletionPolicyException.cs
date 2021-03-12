using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000173 RID: 371
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ErrorCannotRemovePendingDeletionPolicyException : LocalizedException
	{
		// Token: 0x06000F3B RID: 3899 RVA: 0x00036025 File Offset: 0x00034225
		public ErrorCannotRemovePendingDeletionPolicyException(string name) : base(Strings.ErrorCannotRemovePendingDeletionPolicy(name))
		{
			this.name = name;
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x0003603A File Offset: 0x0003423A
		public ErrorCannotRemovePendingDeletionPolicyException(string name, Exception innerException) : base(Strings.ErrorCannotRemovePendingDeletionPolicy(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x00036050 File Offset: 0x00034250
		protected ErrorCannotRemovePendingDeletionPolicyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x0003607A File Offset: 0x0003427A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06000F3F RID: 3903 RVA: 0x00036095 File Offset: 0x00034295
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0400067B RID: 1659
		private readonly string name;
	}
}
