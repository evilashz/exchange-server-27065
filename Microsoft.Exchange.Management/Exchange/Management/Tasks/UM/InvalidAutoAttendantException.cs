using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011CA RID: 4554
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidAutoAttendantException : LocalizedException
	{
		// Token: 0x0600B8D9 RID: 47321 RVA: 0x002A5296 File Offset: 0x002A3496
		public InvalidAutoAttendantException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600B8DA RID: 47322 RVA: 0x002A529F File Offset: 0x002A349F
		public InvalidAutoAttendantException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600B8DB RID: 47323 RVA: 0x002A52A9 File Offset: 0x002A34A9
		protected InvalidAutoAttendantException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B8DC RID: 47324 RVA: 0x002A52B3 File Offset: 0x002A34B3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
