using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001078 RID: 4216
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RemoveDagConfigurationNeedsZeroDagsException : LocalizedException
	{
		// Token: 0x0600B13D RID: 45373 RVA: 0x00297B4E File Offset: 0x00295D4E
		public RemoveDagConfigurationNeedsZeroDagsException(int dagCount) : base(Strings.RemoveDagConfigurationNeedsZeroDagsException(dagCount))
		{
			this.dagCount = dagCount;
		}

		// Token: 0x0600B13E RID: 45374 RVA: 0x00297B63 File Offset: 0x00295D63
		public RemoveDagConfigurationNeedsZeroDagsException(int dagCount, Exception innerException) : base(Strings.RemoveDagConfigurationNeedsZeroDagsException(dagCount), innerException)
		{
			this.dagCount = dagCount;
		}

		// Token: 0x0600B13F RID: 45375 RVA: 0x00297B79 File Offset: 0x00295D79
		protected RemoveDagConfigurationNeedsZeroDagsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dagCount = (int)info.GetValue("dagCount", typeof(int));
		}

		// Token: 0x0600B140 RID: 45376 RVA: 0x00297BA3 File Offset: 0x00295DA3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dagCount", this.dagCount);
		}

		// Token: 0x1700387A RID: 14458
		// (get) Token: 0x0600B141 RID: 45377 RVA: 0x00297BBE File Offset: 0x00295DBE
		public int DagCount
		{
			get
			{
				return this.dagCount;
			}
		}

		// Token: 0x040061E0 RID: 25056
		private readonly int dagCount;
	}
}
