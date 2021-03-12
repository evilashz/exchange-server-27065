using System;
using Microsoft.Exchange.Data.TextConverters.Internal.Format;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Rtf
{
	// Token: 0x02000275 RID: 629
	internal struct RtfFont
	{
		// Token: 0x06001983 RID: 6531 RVA: 0x000C98B8 File Offset: 0x000C7AB8
		public RtfFont(string fname)
		{
			this.Name = fname;
			this.Value = default(PropertyValue);
		}

		// Token: 0x04001EB9 RID: 7865
		public string Name;

		// Token: 0x04001EBA RID: 7866
		public PropertyValue Value;
	}
}
