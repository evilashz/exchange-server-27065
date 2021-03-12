using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000FFE RID: 4094
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotRemoveDefaultSharingPolicy : LocalizedException
	{
		// Token: 0x0600AEB3 RID: 44723 RVA: 0x00293483 File Offset: 0x00291683
		public CannotRemoveDefaultSharingPolicy() : base(Strings.CannotRemoveDefaultSharingPolicy)
		{
		}

		// Token: 0x0600AEB4 RID: 44724 RVA: 0x00293490 File Offset: 0x00291690
		public CannotRemoveDefaultSharingPolicy(Exception innerException) : base(Strings.CannotRemoveDefaultSharingPolicy, innerException)
		{
		}

		// Token: 0x0600AEB5 RID: 44725 RVA: 0x0029349E File Offset: 0x0029169E
		protected CannotRemoveDefaultSharingPolicy(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AEB6 RID: 44726 RVA: 0x002934A8 File Offset: 0x002916A8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
