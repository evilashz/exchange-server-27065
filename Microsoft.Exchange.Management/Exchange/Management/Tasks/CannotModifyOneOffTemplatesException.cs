using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010F7 RID: 4343
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotModifyOneOffTemplatesException : LocalizedException
	{
		// Token: 0x0600B3BD RID: 46013 RVA: 0x0029BA57 File Offset: 0x00299C57
		public CannotModifyOneOffTemplatesException() : base(Strings.CannotModifyOneOffTemplates)
		{
		}

		// Token: 0x0600B3BE RID: 46014 RVA: 0x0029BA64 File Offset: 0x00299C64
		public CannotModifyOneOffTemplatesException(Exception innerException) : base(Strings.CannotModifyOneOffTemplates, innerException)
		{
		}

		// Token: 0x0600B3BF RID: 46015 RVA: 0x0029BA72 File Offset: 0x00299C72
		protected CannotModifyOneOffTemplatesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B3C0 RID: 46016 RVA: 0x0029BA7C File Offset: 0x00299C7C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
