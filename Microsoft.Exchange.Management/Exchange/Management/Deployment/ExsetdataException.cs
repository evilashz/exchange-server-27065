using System;
using System.Globalization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001CD RID: 461
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Serializable]
	public sealed class ExsetdataException : LocalizedException
	{
		// Token: 0x0600100B RID: 4107 RVA: 0x000480D9 File Offset: 0x000462D9
		public ExsetdataException(uint sc, LocalizedString englishMessage, LocalizedString localizedMessage) : base(localizedMessage)
		{
			this.sc = sc;
			this.englishMessage = englishMessage;
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x0600100C RID: 4108 RVA: 0x000480F0 File Offset: 0x000462F0
		public override string Message
		{
			get
			{
				CultureInfo cultureInfo = (base.FormatProvider as CultureInfo) ?? CultureInfo.CurrentUICulture;
				if (cultureInfo.LCID == ExsetdataException.englishCulture.LCID)
				{
					return this.englishMessage.ToString(ExsetdataException.englishCulture);
				}
				return base.Message;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x0004813B File Offset: 0x0004633B
		public uint SC
		{
			get
			{
				return this.sc;
			}
		}

		// Token: 0x04000764 RID: 1892
		private readonly uint sc;

		// Token: 0x04000765 RID: 1893
		private static CultureInfo englishCulture = new CultureInfo("en-US");

		// Token: 0x04000766 RID: 1894
		private LocalizedString englishMessage;
	}
}
