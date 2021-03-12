using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020002D0 RID: 720
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OperationRequiresGroupManagerException : LocalizedException
	{
		// Token: 0x0600197A RID: 6522 RVA: 0x0005D35C File Offset: 0x0005B55C
		public OperationRequiresGroupManagerException() : base(Strings.ErrorOperationRequiresManager)
		{
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x0005D369 File Offset: 0x0005B569
		public OperationRequiresGroupManagerException(Exception innerException) : base(Strings.ErrorOperationRequiresManager, innerException)
		{
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x0005D377 File Offset: 0x0005B577
		protected OperationRequiresGroupManagerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x0005D381 File Offset: 0x0005B581
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
