using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x0200024A RID: 586
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TransientDataProviderUnavailableException : TransientDALException
	{
		// Token: 0x0600173D RID: 5949 RVA: 0x000479BC File Offset: 0x00045BBC
		public TransientDataProviderUnavailableException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x000479C5 File Offset: 0x00045BC5
		public TransientDataProviderUnavailableException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x000479CF File Offset: 0x00045BCF
		protected TransientDataProviderUnavailableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x000479D9 File Offset: 0x00045BD9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
