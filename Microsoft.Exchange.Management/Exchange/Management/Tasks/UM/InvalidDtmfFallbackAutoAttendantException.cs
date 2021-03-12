using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011C9 RID: 4553
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidDtmfFallbackAutoAttendantException : LocalizedException
	{
		// Token: 0x0600B8D5 RID: 47317 RVA: 0x002A526F File Offset: 0x002A346F
		public InvalidDtmfFallbackAutoAttendantException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600B8D6 RID: 47318 RVA: 0x002A5278 File Offset: 0x002A3478
		public InvalidDtmfFallbackAutoAttendantException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600B8D7 RID: 47319 RVA: 0x002A5282 File Offset: 0x002A3482
		protected InvalidDtmfFallbackAutoAttendantException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B8D8 RID: 47320 RVA: 0x002A528C File Offset: 0x002A348C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
