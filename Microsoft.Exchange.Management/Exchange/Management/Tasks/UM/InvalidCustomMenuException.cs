using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011CE RID: 4558
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidCustomMenuException : LocalizedException
	{
		// Token: 0x0600B8E9 RID: 47337 RVA: 0x002A5342 File Offset: 0x002A3542
		public InvalidCustomMenuException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600B8EA RID: 47338 RVA: 0x002A534B File Offset: 0x002A354B
		public InvalidCustomMenuException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600B8EB RID: 47339 RVA: 0x002A5355 File Offset: 0x002A3555
		protected InvalidCustomMenuException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B8EC RID: 47340 RVA: 0x002A535F File Offset: 0x002A355F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
