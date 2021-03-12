using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000ED RID: 237
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SearchLogFileCreateException : LocalizedException
	{
		// Token: 0x06001336 RID: 4918 RVA: 0x000690E0 File Offset: 0x000672E0
		public SearchLogFileCreateException() : base(ServerStrings.SearchLogFileCreateException)
		{
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x000690ED File Offset: 0x000672ED
		public SearchLogFileCreateException(Exception innerException) : base(ServerStrings.SearchLogFileCreateException, innerException)
		{
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x000690FB File Offset: 0x000672FB
		protected SearchLogFileCreateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x00069105 File Offset: 0x00067305
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
