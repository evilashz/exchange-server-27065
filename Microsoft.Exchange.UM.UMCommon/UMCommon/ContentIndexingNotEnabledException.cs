using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001E1 RID: 481
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ContentIndexingNotEnabledException : LocalizedException
	{
		// Token: 0x06000F85 RID: 3973 RVA: 0x000368ED File Offset: 0x00034AED
		public ContentIndexingNotEnabledException() : base(Strings.ContentIndexingNotEnabled)
		{
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x000368FA File Offset: 0x00034AFA
		public ContentIndexingNotEnabledException(Exception innerException) : base(Strings.ContentIndexingNotEnabled, innerException)
		{
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x00036908 File Offset: 0x00034B08
		protected ContentIndexingNotEnabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x00036912 File Offset: 0x00034B12
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
