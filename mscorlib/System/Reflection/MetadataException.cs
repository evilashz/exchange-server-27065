using System;
using System.Globalization;

namespace System.Reflection
{
	// Token: 0x020005D2 RID: 1490
	internal class MetadataException : Exception
	{
		// Token: 0x060045B1 RID: 17841 RVA: 0x000FE55A File Offset: 0x000FC75A
		internal MetadataException(int hr)
		{
			this.m_hr = hr;
		}

		// Token: 0x060045B2 RID: 17842 RVA: 0x000FE569 File Offset: 0x000FC769
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "MetadataException HResult = {0:x}.", this.m_hr);
		}

		// Token: 0x04001C9B RID: 7323
		private int m_hr;
	}
}
