using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FE7 RID: 4071
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClassificationRuleCollectionInternalValidationException : LocalizedException
	{
		// Token: 0x0600AE48 RID: 44616 RVA: 0x00292BEB File Offset: 0x00290DEB
		public ClassificationRuleCollectionInternalValidationException(int error) : base(Strings.ClassificationRuleCollectionInternalFailure(error))
		{
			this.error = error;
		}

		// Token: 0x0600AE49 RID: 44617 RVA: 0x00292C00 File Offset: 0x00290E00
		public ClassificationRuleCollectionInternalValidationException(int error, Exception innerException) : base(Strings.ClassificationRuleCollectionInternalFailure(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x0600AE4A RID: 44618 RVA: 0x00292C16 File Offset: 0x00290E16
		protected ClassificationRuleCollectionInternalValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (int)info.GetValue("error", typeof(int));
		}

		// Token: 0x0600AE4B RID: 44619 RVA: 0x00292C40 File Offset: 0x00290E40
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x170037C9 RID: 14281
		// (get) Token: 0x0600AE4C RID: 44620 RVA: 0x00292C5B File Offset: 0x00290E5B
		public int Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x0400612F RID: 24879
		private readonly int error;
	}
}
