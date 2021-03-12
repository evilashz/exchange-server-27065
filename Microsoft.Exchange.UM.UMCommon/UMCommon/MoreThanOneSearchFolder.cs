using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001BE RID: 446
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MoreThanOneSearchFolder : LocalizedException
	{
		// Token: 0x06000EE1 RID: 3809 RVA: 0x00035B8B File Offset: 0x00033D8B
		public MoreThanOneSearchFolder(int searchFolderCount, string searchFolderName) : base(Strings.MoreThanOneSearchFolder(searchFolderCount, searchFolderName))
		{
			this.searchFolderCount = searchFolderCount;
			this.searchFolderName = searchFolderName;
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x00035BA8 File Offset: 0x00033DA8
		public MoreThanOneSearchFolder(int searchFolderCount, string searchFolderName, Exception innerException) : base(Strings.MoreThanOneSearchFolder(searchFolderCount, searchFolderName), innerException)
		{
			this.searchFolderCount = searchFolderCount;
			this.searchFolderName = searchFolderName;
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x00035BC8 File Offset: 0x00033DC8
		protected MoreThanOneSearchFolder(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.searchFolderCount = (int)info.GetValue("searchFolderCount", typeof(int));
			this.searchFolderName = (string)info.GetValue("searchFolderName", typeof(string));
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x00035C1D File Offset: 0x00033E1D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("searchFolderCount", this.searchFolderCount);
			info.AddValue("searchFolderName", this.searchFolderName);
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x00035C49 File Offset: 0x00033E49
		public int SearchFolderCount
		{
			get
			{
				return this.searchFolderCount;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x00035C51 File Offset: 0x00033E51
		public string SearchFolderName
		{
			get
			{
				return this.searchFolderName;
			}
		}

		// Token: 0x04000799 RID: 1945
		private readonly int searchFolderCount;

		// Token: 0x0400079A RID: 1946
		private readonly string searchFolderName;
	}
}
