using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200002A RID: 42
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MTOMParsingFailedException : LocalizedException
	{
		// Token: 0x06000175 RID: 373 RVA: 0x00005677 File Offset: 0x00003877
		public MTOMParsingFailedException() : base(Strings.MTOMParsingFailedException)
		{
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005684 File Offset: 0x00003884
		public MTOMParsingFailedException(Exception innerException) : base(Strings.MTOMParsingFailedException, innerException)
		{
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00005692 File Offset: 0x00003892
		protected MTOMParsingFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000569C File Offset: 0x0000389C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
