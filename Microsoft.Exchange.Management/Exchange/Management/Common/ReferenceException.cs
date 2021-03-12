using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x0200012C RID: 300
	[Serializable]
	public class ReferenceException : LocalizedException
	{
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x000324CF File Offset: 0x000306CF
		// (set) Token: 0x06000AB9 RID: 2745 RVA: 0x000324D7 File Offset: 0x000306D7
		public string ReferenceValue { get; private set; }

		// Token: 0x06000ABA RID: 2746 RVA: 0x000324E0 File Offset: 0x000306E0
		public ReferenceException(string referenceValue, LocalizedException innerException) : base(innerException.LocalizedString, innerException)
		{
			this.ReferenceValue = referenceValue;
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x000324F6 File Offset: 0x000306F6
		public ReferenceException(string referenceValue, Exception innerException) : base(new LocalizedString(innerException.Message), innerException)
		{
			this.ReferenceValue = referenceValue;
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00032511 File Offset: 0x00030711
		public ReferenceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ReferenceValue = info.GetString("ReferenceValue");
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0003252C File Offset: 0x0003072C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ReferenceValue", this.ReferenceValue);
		}
	}
}
