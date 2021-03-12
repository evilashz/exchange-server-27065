using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AA3 RID: 2723
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ActivationPreferenceException : ADOperationException
	{
		// Token: 0x06007FF9 RID: 32761 RVA: 0x001A4B25 File Offset: 0x001A2D25
		public ActivationPreferenceException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007FFA RID: 32762 RVA: 0x001A4B2E File Offset: 0x001A2D2E
		public ActivationPreferenceException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007FFB RID: 32763 RVA: 0x001A4B38 File Offset: 0x001A2D38
		protected ActivationPreferenceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007FFC RID: 32764 RVA: 0x001A4B42 File Offset: 0x001A2D42
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
