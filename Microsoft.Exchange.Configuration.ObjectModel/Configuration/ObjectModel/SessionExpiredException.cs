using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x020002DE RID: 734
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SessionExpiredException : TransientException
	{
		// Token: 0x060019B8 RID: 6584 RVA: 0x0005D76E File Offset: 0x0005B96E
		public SessionExpiredException() : base(Strings.SessionExpiredException)
		{
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x0005D77B File Offset: 0x0005B97B
		public SessionExpiredException(Exception innerException) : base(Strings.SessionExpiredException, innerException)
		{
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x0005D789 File Offset: 0x0005B989
		protected SessionExpiredException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x0005D793 File Offset: 0x0005B993
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
