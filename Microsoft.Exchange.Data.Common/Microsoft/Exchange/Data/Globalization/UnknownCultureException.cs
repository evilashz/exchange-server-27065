using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.Globalization
{
	// Token: 0x02000112 RID: 274
	[Serializable]
	public class UnknownCultureException : ExchangeDataException
	{
		// Token: 0x06000B0B RID: 2827 RVA: 0x00066920 File Offset: 0x00064B20
		public UnknownCultureException(int localeId) : base(GlobalizationStrings.InvalidLocaleId(localeId))
		{
			this.localeId = localeId;
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00066935 File Offset: 0x00064B35
		public UnknownCultureException(string cultureName) : base(GlobalizationStrings.InvalidCultureName((cultureName == null) ? "<null>" : cultureName))
		{
			this.cultureName = cultureName;
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00066954 File Offset: 0x00064B54
		public UnknownCultureException(int localeId, string message) : base(message)
		{
			this.localeId = localeId;
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00066964 File Offset: 0x00064B64
		public UnknownCultureException(string cultureName, string message) : base(message)
		{
			this.cultureName = cultureName;
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x00066974 File Offset: 0x00064B74
		public UnknownCultureException(int localeId, string message, Exception innerException) : base(message, innerException)
		{
			this.localeId = localeId;
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x00066985 File Offset: 0x00064B85
		public UnknownCultureException(string cultureName, string message, Exception innerException) : base(message, innerException)
		{
			this.cultureName = cultureName;
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00066996 File Offset: 0x00064B96
		protected UnknownCultureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.localeId = info.GetInt32("localeId");
			this.cultureName = info.GetString("cultureName");
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x000669C2 File Offset: 0x00064BC2
		public int LocaleId
		{
			get
			{
				return this.localeId;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x000669CA File Offset: 0x00064BCA
		public string CultureName
		{
			get
			{
				return this.cultureName;
			}
		}

		// Token: 0x04000DF4 RID: 3572
		private int localeId;

		// Token: 0x04000DF5 RID: 3573
		private string cultureName;
	}
}
