using System;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200026C RID: 620
	[DataContract]
	public class OptionalRetentionPolicyTagRow : RetentionPolicyTagBaseRow
	{
		// Token: 0x0600297F RID: 10623 RVA: 0x00082B59 File Offset: 0x00080D59
		public OptionalRetentionPolicyTagRow(PresentationRetentionPolicyTag rpt) : base(rpt)
		{
		}

		// Token: 0x17001CB0 RID: 7344
		// (get) Token: 0x06002980 RID: 10624 RVA: 0x00082B64 File Offset: 0x00080D64
		// (set) Token: 0x06002981 RID: 10625 RVA: 0x00082B96 File Offset: 0x00080D96
		[DataMember]
		public string Description
		{
			get
			{
				return base.RetentionPolicyTag.GetLocalizedFolderComment(new CultureInfo[]
				{
					Thread.CurrentThread.CurrentUICulture
				}.AsEnumerable<CultureInfo>());
			}
			private set
			{
				throw new NotSupportedException();
			}
		}
	}
}
