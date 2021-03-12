using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.GUI
{
	// Token: 0x02000020 RID: 32
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class PartiallyConfiguredException : LocalizedException
	{
		// Token: 0x06000168 RID: 360 RVA: 0x0000C46B File Offset: 0x0000A66B
		public PartiallyConfiguredException() : base(Strings.PartiallyConfiguredCannotRunUnInstall)
		{
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000C478 File Offset: 0x0000A678
		public PartiallyConfiguredException(Exception innerException) : base(Strings.PartiallyConfiguredCannotRunUnInstall, innerException)
		{
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000C486 File Offset: 0x0000A686
		protected PartiallyConfiguredException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000C490 File Offset: 0x0000A690
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
