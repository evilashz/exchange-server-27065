using System;
using Microsoft.Exchange.Data.ContentTypes.Internal;

namespace Microsoft.Exchange.Data.ContentTypes.iCalendar
{
	// Token: 0x020000B2 RID: 178
	public struct CalendarParameterReader
	{
		// Token: 0x06000731 RID: 1841 RVA: 0x0002871B File Offset: 0x0002691B
		internal CalendarParameterReader(ContentLineReader reader)
		{
			this.reader = reader;
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x00028724 File Offset: 0x00026924
		public ParameterId ParameterId
		{
			get
			{
				this.reader.AssertValidState(ContentLineNodeType.Parameter);
				return CalendarCommon.GetParameterEnum(this.reader.ParameterName);
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x00028742 File Offset: 0x00026942
		public string Name
		{
			get
			{
				this.reader.AssertValidState(ContentLineNodeType.Parameter);
				return this.reader.ParameterName;
			}
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0002875B File Offset: 0x0002695B
		public string ReadValue()
		{
			this.reader.AssertValidState(ContentLineNodeType.Parameter);
			return this.reader.ReadParameterValue(true);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00028775 File Offset: 0x00026975
		public bool ReadNextValue()
		{
			this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.DocumentEnd);
			return this.reader.ReadNextParameterValue();
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0002878F File Offset: 0x0002698F
		public bool ReadNextParameter()
		{
			this.reader.AssertValidState(ContentLineNodeType.Parameter | ContentLineNodeType.Property | ContentLineNodeType.DocumentEnd);
			return this.reader.ReadNextParameter();
		}

		// Token: 0x040005DD RID: 1501
		private ContentLineReader reader;
	}
}
