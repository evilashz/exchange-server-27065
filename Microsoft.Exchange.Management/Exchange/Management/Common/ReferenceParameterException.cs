using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x02000130 RID: 304
	[Serializable]
	public class ReferenceParameterException : LocalizedException
	{
		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x000325A1 File Offset: 0x000307A1
		// (set) Token: 0x06000AC8 RID: 2760 RVA: 0x000325A9 File Offset: 0x000307A9
		public string Parameter { get; private set; }

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x000325B2 File Offset: 0x000307B2
		// (set) Token: 0x06000ACA RID: 2762 RVA: 0x000325BA File Offset: 0x000307BA
		public ReferenceException[] ReferenceExceptions { get; private set; }

		// Token: 0x06000ACB RID: 2763 RVA: 0x000325C3 File Offset: 0x000307C3
		public ReferenceParameterException(LocalizedString localizedString, string parameter, ReferenceException[] referenceExceptions) : base(localizedString)
		{
			this.Parameter = parameter;
			this.ReferenceExceptions = referenceExceptions;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x000325DA File Offset: 0x000307DA
		public ReferenceParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.Parameter = info.GetString("Parameter");
			this.ReferenceExceptions = (ReferenceException[])info.GetValue("ReferenceExceptions", typeof(ReferenceException[]));
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00032615 File Offset: 0x00030815
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("Parameter", this.Parameter);
			info.AddValue("ReferenceExceptions", this.ReferenceExceptions);
		}
	}
}
