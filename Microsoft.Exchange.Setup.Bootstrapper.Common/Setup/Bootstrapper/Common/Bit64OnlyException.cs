using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.Bootstrapper.Common
{
	// Token: 0x0200000F RID: 15
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class Bit64OnlyException : LocalizedException
	{
		// Token: 0x060000AB RID: 171 RVA: 0x00004DC1 File Offset: 0x00002FC1
		public Bit64OnlyException() : base(Strings.Bit64Only)
		{
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004DCE File Offset: 0x00002FCE
		public Bit64OnlyException(Exception innerException) : base(Strings.Bit64Only, innerException)
		{
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004DDC File Offset: 0x00002FDC
		protected Bit64OnlyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004DE6 File Offset: 0x00002FE6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
