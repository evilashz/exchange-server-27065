using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FF9 RID: 4089
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ErrorExceededTextScanLimitException : LocalizedException
	{
		// Token: 0x0600AE9E RID: 44702 RVA: 0x0029334F File Offset: 0x0029154F
		public ErrorExceededTextScanLimitException(int textScanLimits) : base(Strings.ErrorExceededTextScanLimit(textScanLimits))
		{
			this.textScanLimits = textScanLimits;
		}

		// Token: 0x0600AE9F RID: 44703 RVA: 0x00293364 File Offset: 0x00291564
		public ErrorExceededTextScanLimitException(int textScanLimits, Exception innerException) : base(Strings.ErrorExceededTextScanLimit(textScanLimits), innerException)
		{
			this.textScanLimits = textScanLimits;
		}

		// Token: 0x0600AEA0 RID: 44704 RVA: 0x0029337A File Offset: 0x0029157A
		protected ErrorExceededTextScanLimitException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.textScanLimits = (int)info.GetValue("textScanLimits", typeof(int));
		}

		// Token: 0x0600AEA1 RID: 44705 RVA: 0x002933A4 File Offset: 0x002915A4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("textScanLimits", this.textScanLimits);
		}

		// Token: 0x170037D7 RID: 14295
		// (get) Token: 0x0600AEA2 RID: 44706 RVA: 0x002933BF File Offset: 0x002915BF
		public int TextScanLimits
		{
			get
			{
				return this.textScanLimits;
			}
		}

		// Token: 0x0400613D RID: 24893
		private readonly int textScanLimits;
	}
}
