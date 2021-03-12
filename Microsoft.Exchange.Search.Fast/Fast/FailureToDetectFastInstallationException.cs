using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x02000033 RID: 51
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FailureToDetectFastInstallationException : ComponentFailedPermanentException
	{
		// Token: 0x06000298 RID: 664 RVA: 0x0000F302 File Offset: 0x0000D502
		public FailureToDetectFastInstallationException() : base(Strings.FailureToDetectFastInstallation)
		{
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000F30F File Offset: 0x0000D50F
		public FailureToDetectFastInstallationException(Exception innerException) : base(Strings.FailureToDetectFastInstallation, innerException)
		{
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000F31D File Offset: 0x0000D51D
		protected FailureToDetectFastInstallationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000F327 File Offset: 0x0000D527
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
