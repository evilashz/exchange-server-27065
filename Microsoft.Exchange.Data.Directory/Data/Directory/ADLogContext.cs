using System;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000033 RID: 51
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ADLogContext
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00011E34 File Offset: 0x00010034
		// (set) Token: 0x0600033B RID: 827 RVA: 0x00011E3C File Offset: 0x0001003C
		internal int FileLine { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00011E45 File Offset: 0x00010045
		// (set) Token: 0x0600033D RID: 829 RVA: 0x00011E4D File Offset: 0x0001004D
		internal string MemberName { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00011E56 File Offset: 0x00010056
		// (set) Token: 0x0600033F RID: 831 RVA: 0x00011E5E File Offset: 0x0001005E
		internal string FilePath { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00011E68 File Offset: 0x00010068
		// (set) Token: 0x06000341 RID: 833 RVA: 0x00011EC0 File Offset: 0x000100C0
		internal IActivityScope ActivityScope
		{
			get
			{
				IActivityScope result;
				try
				{
					result = (this.scope ?? ActivityContext.GetCurrentActivityScope());
				}
				catch (Exception ex)
				{
					Globals.LogEvent(DirectoryEventLogConstants.Tuple_GetActivityContextFailed, this.GetCallerInformation(), new object[]
					{
						ex.ToString()
					});
					result = null;
				}
				return result;
			}
			set
			{
				this.scope = value;
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00011ECC File Offset: 0x000100CC
		public string GetCallerInformation()
		{
			int num = 0;
			if (string.IsNullOrEmpty(this.FilePath) || (num = this.FilePath.LastIndexOf("\\")) <= 0)
			{
				return string.Empty;
			}
			IActivityScope activityScope = null;
			try
			{
				activityScope = (this.scope ?? ActivityContext.GetCurrentActivityScope());
			}
			catch
			{
			}
			if (activityScope != null && !string.IsNullOrEmpty(activityScope.Action))
			{
				return string.Format("{0}: Method {1}; Line {2}; Action {3}", new object[]
				{
					this.FilePath.Substring(num + 1),
					this.MemberName,
					this.FileLine,
					activityScope.Action
				});
			}
			return string.Format("{0}: Method {1}; Line {2}", this.FilePath.Substring(num + 1), this.MemberName, this.FileLine);
		}

		// Token: 0x040000E6 RID: 230
		private IActivityScope scope;
	}
}
