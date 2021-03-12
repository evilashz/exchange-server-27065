using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x02000131 RID: 305
	[Serializable]
	public class MultiReferenceParameterException : LocalizedException
	{
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x00032641 File Offset: 0x00030841
		// (set) Token: 0x06000ACF RID: 2767 RVA: 0x00032649 File Offset: 0x00030849
		public ReferenceParameterException[] ReferenceParameterExceptions { get; private set; }

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00032652 File Offset: 0x00030852
		public MultiReferenceParameterException(LocalizedString localizedString, ReferenceParameterException[] referenceParameterExceptions) : base(localizedString)
		{
			this.ReferenceParameterExceptions = referenceParameterExceptions;
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00032662 File Offset: 0x00030862
		public MultiReferenceParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ReferenceParameterExceptions = (ReferenceParameterException[])info.GetValue("ReferenceParameterExceptions", typeof(ReferenceParameterException[]));
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0003268C File Offset: 0x0003088C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ReferenceParameterExceptions", this.ReferenceParameterExceptions);
		}
	}
}
