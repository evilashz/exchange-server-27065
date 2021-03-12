using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A24 RID: 2596
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetCalendarFolderConfigurationResponse : CalendarActionResponse
	{
		// Token: 0x06004930 RID: 18736 RVA: 0x001024B8 File Offset: 0x001006B8
		internal GetCalendarFolderConfigurationResponse(CalendarFolderType folder, MasterCategoryListType masterList = null)
		{
			this.CalendarFolder = folder;
			this.MasterList = masterList;
		}

		// Token: 0x06004931 RID: 18737 RVA: 0x001024CE File Offset: 0x001006CE
		internal GetCalendarFolderConfigurationResponse(CalendarActionError errorCode) : base(errorCode)
		{
		}

		// Token: 0x17001059 RID: 4185
		// (get) Token: 0x06004932 RID: 18738 RVA: 0x001024D7 File Offset: 0x001006D7
		// (set) Token: 0x06004933 RID: 18739 RVA: 0x001024DF File Offset: 0x001006DF
		[DataMember]
		public CalendarFolderType CalendarFolder { get; set; }

		// Token: 0x1700105A RID: 4186
		// (get) Token: 0x06004934 RID: 18740 RVA: 0x001024E8 File Offset: 0x001006E8
		// (set) Token: 0x06004935 RID: 18741 RVA: 0x001024F0 File Offset: 0x001006F0
		[DataMember]
		public MasterCategoryListType MasterList { get; set; }
	}
}
