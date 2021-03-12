using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200002B RID: 43
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class NestedFoldersNotAllowedException : LocalizedException
	{
		// Token: 0x06000179 RID: 377 RVA: 0x000056A6 File Offset: 0x000038A6
		public NestedFoldersNotAllowedException() : base(Strings.NestedFoldersNotAllowedException)
		{
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000056B3 File Offset: 0x000038B3
		public NestedFoldersNotAllowedException(Exception innerException) : base(Strings.NestedFoldersNotAllowedException, innerException)
		{
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000056C1 File Offset: 0x000038C1
		protected NestedFoldersNotAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000056CB File Offset: 0x000038CB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
