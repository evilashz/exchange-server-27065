using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200003C RID: 60
	internal class ForwardSyncCookieStxLogger : StxLoggerBase
	{
		// Token: 0x06000173 RID: 371 RVA: 0x000067C8 File Offset: 0x000049C8
		public ForwardSyncCookieStxLogger()
		{
			base.ExtendedFields.AddRange(new List<FieldInfo>
			{
				new FieldInfo(StxLoggerBase.MandatoryFields.Count, "ServiceInstanceName"),
				new FieldInfo(StxLoggerBase.MandatoryFields.Count + 1, "CompanyCookieStructureType"),
				new FieldInfo(StxLoggerBase.MandatoryFields.Count + 2, "CompanyCookieSize"),
				new FieldInfo(StxLoggerBase.MandatoryFields.Count + 3, "RecipientCookieStructureType"),
				new FieldInfo(StxLoggerBase.MandatoryFields.Count + 4, "RecipientCookieSize")
			});
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00006877 File Offset: 0x00004A77
		internal override string LogTypeName
		{
			get
			{
				return "ForwardSyncCookie Logs";
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000175 RID: 373 RVA: 0x0000687E File Offset: 0x00004A7E
		internal override string LogComponent
		{
			get
			{
				return "ForwardSyncCookie";
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00006885 File Offset: 0x00004A85
		internal override string LogFilePrefix
		{
			get
			{
				return "Test-ForwardSyncCookie_";
			}
		}
	}
}
