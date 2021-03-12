using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011F8 RID: 4600
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TUC_InvalidDTMFSequenceReceived : LocalizedException
	{
		// Token: 0x0600B9B2 RID: 47538 RVA: 0x002A64C1 File Offset: 0x002A46C1
		public TUC_InvalidDTMFSequenceReceived() : base(Strings.InvalidDTMFSequenceReceived)
		{
		}

		// Token: 0x0600B9B3 RID: 47539 RVA: 0x002A64CE File Offset: 0x002A46CE
		public TUC_InvalidDTMFSequenceReceived(Exception innerException) : base(Strings.InvalidDTMFSequenceReceived, innerException)
		{
		}

		// Token: 0x0600B9B4 RID: 47540 RVA: 0x002A64DC File Offset: 0x002A46DC
		protected TUC_InvalidDTMFSequenceReceived(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B9B5 RID: 47541 RVA: 0x002A64E6 File Offset: 0x002A46E6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
