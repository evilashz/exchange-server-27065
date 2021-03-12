using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200054A RID: 1354
	[Serializable]
	public abstract class ReplicationCheckException : LocalizedException
	{
		// Token: 0x06003049 RID: 12361 RVA: 0x000C4074 File Offset: 0x000C2274
		public ReplicationCheckException(LocalizedString localizedString) : this(localizedString, null)
		{
		}

		// Token: 0x0600304A RID: 12362 RVA: 0x000C407E File Offset: 0x000C227E
		public ReplicationCheckException(LocalizedString localizedString, Exception innerException) : base(localizedString, innerException)
		{
			this.localizedString = localizedString;
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x000C408F File Offset: 0x000C228F
		protected ReplicationCheckException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
		{
		}

		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x0600304C RID: 12364 RVA: 0x000C4099 File Offset: 0x000C2299
		public new LocalizedString LocalizedString
		{
			get
			{
				return this.localizedString;
			}
		}

		// Token: 0x04002269 RID: 8809
		private LocalizedString localizedString;
	}
}
