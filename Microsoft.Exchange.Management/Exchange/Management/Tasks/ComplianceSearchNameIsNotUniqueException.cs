using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000F42 RID: 3906
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ComplianceSearchNameIsNotUniqueException : LocalizedException
	{
		// Token: 0x0600AB44 RID: 43844 RVA: 0x0028ED08 File Offset: 0x0028CF08
		public ComplianceSearchNameIsNotUniqueException(string name) : base(Strings.ComplianceSearchNameIsNotUnique(name))
		{
			this.name = name;
		}

		// Token: 0x0600AB45 RID: 43845 RVA: 0x0028ED1D File Offset: 0x0028CF1D
		public ComplianceSearchNameIsNotUniqueException(string name, Exception innerException) : base(Strings.ComplianceSearchNameIsNotUnique(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600AB46 RID: 43846 RVA: 0x0028ED33 File Offset: 0x0028CF33
		protected ComplianceSearchNameIsNotUniqueException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600AB47 RID: 43847 RVA: 0x0028ED5D File Offset: 0x0028CF5D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x17003759 RID: 14169
		// (get) Token: 0x0600AB48 RID: 43848 RVA: 0x0028ED78 File Offset: 0x0028CF78
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040060BF RID: 24767
		private readonly string name;
	}
}
