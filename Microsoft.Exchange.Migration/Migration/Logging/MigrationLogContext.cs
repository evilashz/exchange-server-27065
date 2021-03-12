using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration.Logging
{
	// Token: 0x02000077 RID: 119
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MigrationLogContext
	{
		// Token: 0x060006B3 RID: 1715 RVA: 0x0001E763 File Offset: 0x0001C963
		private MigrationLogContext()
		{
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x0001E77D File Offset: 0x0001C97D
		public static MigrationLogContext Current
		{
			get
			{
				MigrationLogContext result;
				if ((result = MigrationLogContext.context) == null)
				{
					result = (MigrationLogContext.context = new MigrationLogContext());
				}
				return result;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x0001E793 File Offset: 0x0001C993
		// (set) Token: 0x060006B6 RID: 1718 RVA: 0x0001E7AE File Offset: 0x0001C9AE
		public string Source
		{
			get
			{
				if (string.IsNullOrEmpty(this.source))
				{
					return "Default";
				}
				return this.source;
			}
			set
			{
				this.source = value;
				this.isDirty = true;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0001E7BE File Offset: 0x0001C9BE
		// (set) Token: 0x060006B8 RID: 1720 RVA: 0x0001E7C6 File Offset: 0x0001C9C6
		public ADObjectId Organization
		{
			get
			{
				return this.org;
			}
			set
			{
				this.org = value;
				this.isDirty = true;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x0001E7D6 File Offset: 0x0001C9D6
		// (set) Token: 0x060006BA RID: 1722 RVA: 0x0001E7DE File Offset: 0x0001C9DE
		public MigrationJob Job
		{
			get
			{
				return this.job;
			}
			set
			{
				this.job = value;
				this.isDirty = true;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x0001E7EE File Offset: 0x0001C9EE
		// (set) Token: 0x060006BC RID: 1724 RVA: 0x0001E7F6 File Offset: 0x0001C9F6
		public string JobItem
		{
			get
			{
				return this.jobItem;
			}
			set
			{
				this.jobItem = value;
				this.isDirty = true;
			}
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0001E806 File Offset: 0x0001CA06
		public static void Clear()
		{
			MigrationLogContext.context = null;
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0001E810 File Offset: 0x0001CA10
		public override string ToString()
		{
			if (!this.isDirty)
			{
				return this.cachedTostring;
			}
			this.isDirty = false;
			string[] value = new string[]
			{
				(this.org != null) ? this.org.Name : string.Empty,
				(this.job != null) ? (this.job.JobId.ToString() + "," + this.job.JobName) : string.Empty,
				(this.jobItem != null) ? this.jobItem : string.Empty
			};
			this.cachedTostring = string.Join(",", value);
			return this.cachedTostring;
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0001E8C8 File Offset: 0x0001CAC8
		public override int GetHashCode()
		{
			return ((this.org == null) ? 0 : this.org.GetHashCode()) + ((this.job == null) ? 0 : this.job.GetHashCode()) + ((this.jobItem == null) ? 0 : this.jobItem.GetHashCode());
		}

		// Token: 0x040002CB RID: 715
		internal const string DefaultSeparator = ",";

		// Token: 0x040002CC RID: 716
		private const string DefaultSource = "Default";

		// Token: 0x040002CD RID: 717
		[ThreadStatic]
		private static MigrationLogContext context;

		// Token: 0x040002CE RID: 718
		private string source;

		// Token: 0x040002CF RID: 719
		private ADObjectId org;

		// Token: 0x040002D0 RID: 720
		private MigrationJob job;

		// Token: 0x040002D1 RID: 721
		private string jobItem;

		// Token: 0x040002D2 RID: 722
		private string cachedTostring = string.Empty;

		// Token: 0x040002D3 RID: 723
		private bool isDirty = true;
	}
}
