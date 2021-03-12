using System;
using System.Globalization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x02000263 RID: 611
	internal sealed class SipCultureInfo : SipCultureInfoBase
	{
		// Token: 0x06001546 RID: 5446 RVA: 0x0004EB14 File Offset: 0x0004CD14
		internal SipCultureInfo(CultureInfo parent, string segmentID) : base(parent, segmentID)
		{
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001547 RID: 5447 RVA: 0x0004EB1E File Offset: 0x0004CD1E
		internal override string SipSegmentID
		{
			get
			{
				return this.segmentID;
			}
		}
	}
}
