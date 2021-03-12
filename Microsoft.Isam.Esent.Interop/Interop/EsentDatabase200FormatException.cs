using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000195 RID: 405
	[Serializable]
	public sealed class EsentDatabase200FormatException : EsentObsoleteException
	{
		// Token: 0x0600093D RID: 2365 RVA: 0x00012CDE File Offset: 0x00010EDE
		public EsentDatabase200FormatException() : base("The database is in an older (200) format", JET_err.Database200Format)
		{
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00012CF0 File Offset: 0x00010EF0
		private EsentDatabase200FormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
