using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001C4 RID: 452
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CouldNotGenerateExtensionException : LocalizedException
	{
		// Token: 0x06000EFB RID: 3835 RVA: 0x00035D44 File Offset: 0x00033F44
		public CouldNotGenerateExtensionException() : base(Strings.ExceptionCouldNotGenerateExtension)
		{
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x00035D51 File Offset: 0x00033F51
		public CouldNotGenerateExtensionException(Exception innerException) : base(Strings.ExceptionCouldNotGenerateExtension, innerException)
		{
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x00035D5F File Offset: 0x00033F5F
		protected CouldNotGenerateExtensionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x00035D69 File Offset: 0x00033F69
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
