using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000E7B RID: 3707
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ValidationOrgCurrentOrgNotMatchException : LocalizedException
	{
		// Token: 0x0600A73A RID: 42810 RVA: 0x00288132 File Offset: 0x00286332
		public ValidationOrgCurrentOrgNotMatchException(string validationOrg, string currentOrg) : base(Strings.ValidationOrgCurrentOrgNotMatchException(validationOrg, currentOrg))
		{
			this.validationOrg = validationOrg;
			this.currentOrg = currentOrg;
		}

		// Token: 0x0600A73B RID: 42811 RVA: 0x0028814F File Offset: 0x0028634F
		public ValidationOrgCurrentOrgNotMatchException(string validationOrg, string currentOrg, Exception innerException) : base(Strings.ValidationOrgCurrentOrgNotMatchException(validationOrg, currentOrg), innerException)
		{
			this.validationOrg = validationOrg;
			this.currentOrg = currentOrg;
		}

		// Token: 0x0600A73C RID: 42812 RVA: 0x00288170 File Offset: 0x00286370
		protected ValidationOrgCurrentOrgNotMatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.validationOrg = (string)info.GetValue("validationOrg", typeof(string));
			this.currentOrg = (string)info.GetValue("currentOrg", typeof(string));
		}

		// Token: 0x0600A73D RID: 42813 RVA: 0x002881C5 File Offset: 0x002863C5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("validationOrg", this.validationOrg);
			info.AddValue("currentOrg", this.currentOrg);
		}

		// Token: 0x1700366B RID: 13931
		// (get) Token: 0x0600A73E RID: 42814 RVA: 0x002881F1 File Offset: 0x002863F1
		public string ValidationOrg
		{
			get
			{
				return this.validationOrg;
			}
		}

		// Token: 0x1700366C RID: 13932
		// (get) Token: 0x0600A73F RID: 42815 RVA: 0x002881F9 File Offset: 0x002863F9
		public string CurrentOrg
		{
			get
			{
				return this.currentOrg;
			}
		}

		// Token: 0x04005FD1 RID: 24529
		private readonly string validationOrg;

		// Token: 0x04005FD2 RID: 24530
		private readonly string currentOrg;
	}
}
