using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200007E RID: 126
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SourceDirNotSpecifiedException : LocalizedException
	{
		// Token: 0x06000693 RID: 1683 RVA: 0x00016727 File Offset: 0x00014927
		public SourceDirNotSpecifiedException() : base(Strings.SourceDirNotSpecifiedError)
		{
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00016734 File Offset: 0x00014934
		public SourceDirNotSpecifiedException(Exception innerException) : base(Strings.SourceDirNotSpecifiedError, innerException)
		{
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00016742 File Offset: 0x00014942
		protected SourceDirNotSpecifiedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001674C File Offset: 0x0001494C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
