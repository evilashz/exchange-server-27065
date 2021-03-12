using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000E2 RID: 226
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CsvValidationException : LocalizedException
	{
		// Token: 0x06000807 RID: 2055 RVA: 0x0001AE55 File Offset: 0x00019055
		public CsvValidationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001AE5E File Offset: 0x0001905E
		public CsvValidationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0001AE68 File Offset: 0x00019068
		protected CsvValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0001AE72 File Offset: 0x00019072
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
