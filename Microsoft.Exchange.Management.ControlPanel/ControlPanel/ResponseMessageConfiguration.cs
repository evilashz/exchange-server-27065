using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000B0 RID: 176
	[DataContract]
	public class ResponseMessageConfiguration : ResourceConfigurationBase
	{
		// Token: 0x06001C48 RID: 7240 RVA: 0x000586E4 File Offset: 0x000568E4
		public ResponseMessageConfiguration(CalendarConfiguration calendarConfiguration) : base(calendarConfiguration)
		{
		}

		// Token: 0x170018E5 RID: 6373
		// (get) Token: 0x06001C49 RID: 7241 RVA: 0x000586ED File Offset: 0x000568ED
		// (set) Token: 0x06001C4A RID: 7242 RVA: 0x000586FA File Offset: 0x000568FA
		[DataMember]
		public bool AddAdditionalResponse
		{
			get
			{
				return base.CalendarConfiguration.AddAdditionalResponse;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170018E6 RID: 6374
		// (get) Token: 0x06001C4B RID: 7243 RVA: 0x00058701 File Offset: 0x00056901
		// (set) Token: 0x06001C4C RID: 7244 RVA: 0x00058717 File Offset: 0x00056917
		[DataMember]
		public string AdditionalResponse
		{
			get
			{
				return base.CalendarConfiguration.AdditionalResponse ?? string.Empty;
			}
			set
			{
				throw new NotSupportedException();
			}
		}
	}
}
