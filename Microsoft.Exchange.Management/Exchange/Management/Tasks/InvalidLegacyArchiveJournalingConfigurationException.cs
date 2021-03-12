using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FC6 RID: 4038
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidLegacyArchiveJournalingConfigurationException : LocalizedException
	{
		// Token: 0x0600ADB6 RID: 44470 RVA: 0x00292212 File Offset: 0x00290412
		public InvalidLegacyArchiveJournalingConfigurationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600ADB7 RID: 44471 RVA: 0x0029221B File Offset: 0x0029041B
		public InvalidLegacyArchiveJournalingConfigurationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600ADB8 RID: 44472 RVA: 0x00292225 File Offset: 0x00290425
		protected InvalidLegacyArchiveJournalingConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ADB9 RID: 44473 RVA: 0x0029222F File Offset: 0x0029042F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
