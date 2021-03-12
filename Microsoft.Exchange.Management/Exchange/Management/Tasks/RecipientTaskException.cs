using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E44 RID: 3652
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RecipientTaskException : LocalizedException
	{
		// Token: 0x0600A657 RID: 42583 RVA: 0x00287693 File Offset: 0x00285893
		public RecipientTaskException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A658 RID: 42584 RVA: 0x0028769C File Offset: 0x0028589C
		public RecipientTaskException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A659 RID: 42585 RVA: 0x002876A6 File Offset: 0x002858A6
		protected RecipientTaskException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A65A RID: 42586 RVA: 0x002876B0 File Offset: 0x002858B0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
