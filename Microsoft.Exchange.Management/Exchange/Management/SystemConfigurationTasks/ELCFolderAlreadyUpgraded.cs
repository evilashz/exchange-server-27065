using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000DED RID: 3565
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ELCFolderAlreadyUpgraded : LocalizedException
	{
		// Token: 0x0600A495 RID: 42133 RVA: 0x00284816 File Offset: 0x00282A16
		public ELCFolderAlreadyUpgraded(string folderName, string rptName) : base(Strings.ELCFolderAlreadyUpgraded(folderName, rptName))
		{
			this.folderName = folderName;
			this.rptName = rptName;
		}

		// Token: 0x0600A496 RID: 42134 RVA: 0x00284833 File Offset: 0x00282A33
		public ELCFolderAlreadyUpgraded(string folderName, string rptName, Exception innerException) : base(Strings.ELCFolderAlreadyUpgraded(folderName, rptName), innerException)
		{
			this.folderName = folderName;
			this.rptName = rptName;
		}

		// Token: 0x0600A497 RID: 42135 RVA: 0x00284854 File Offset: 0x00282A54
		protected ELCFolderAlreadyUpgraded(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.folderName = (string)info.GetValue("folderName", typeof(string));
			this.rptName = (string)info.GetValue("rptName", typeof(string));
		}

		// Token: 0x0600A498 RID: 42136 RVA: 0x002848A9 File Offset: 0x00282AA9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("folderName", this.folderName);
			info.AddValue("rptName", this.rptName);
		}

		// Token: 0x170035FE RID: 13822
		// (get) Token: 0x0600A499 RID: 42137 RVA: 0x002848D5 File Offset: 0x00282AD5
		public string FolderName
		{
			get
			{
				return this.folderName;
			}
		}

		// Token: 0x170035FF RID: 13823
		// (get) Token: 0x0600A49A RID: 42138 RVA: 0x002848DD File Offset: 0x00282ADD
		public string RptName
		{
			get
			{
				return this.rptName;
			}
		}

		// Token: 0x04005F64 RID: 24420
		private readonly string folderName;

		// Token: 0x04005F65 RID: 24421
		private readonly string rptName;
	}
}
