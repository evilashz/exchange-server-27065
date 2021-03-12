using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011CD RID: 4557
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidParameterException : LocalizedException
	{
		// Token: 0x0600B8E5 RID: 47333 RVA: 0x002A531B File Offset: 0x002A351B
		public InvalidParameterException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600B8E6 RID: 47334 RVA: 0x002A5324 File Offset: 0x002A3524
		public InvalidParameterException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600B8E7 RID: 47335 RVA: 0x002A532E File Offset: 0x002A352E
		protected InvalidParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B8E8 RID: 47336 RVA: 0x002A5338 File Offset: 0x002A3538
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
