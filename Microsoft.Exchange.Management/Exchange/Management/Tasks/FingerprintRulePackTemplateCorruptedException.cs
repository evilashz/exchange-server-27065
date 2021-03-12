using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FF0 RID: 4080
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FingerprintRulePackTemplateCorruptedException : LocalizedException
	{
		// Token: 0x0600AE73 RID: 44659 RVA: 0x00292F8D File Offset: 0x0029118D
		public FingerprintRulePackTemplateCorruptedException(string file) : base(Strings.ErrorFingerprintRulePackTemplateCorrupted(file))
		{
			this.file = file;
		}

		// Token: 0x0600AE74 RID: 44660 RVA: 0x00292FA2 File Offset: 0x002911A2
		public FingerprintRulePackTemplateCorruptedException(string file, Exception innerException) : base(Strings.ErrorFingerprintRulePackTemplateCorrupted(file), innerException)
		{
			this.file = file;
		}

		// Token: 0x0600AE75 RID: 44661 RVA: 0x00292FB8 File Offset: 0x002911B8
		protected FingerprintRulePackTemplateCorruptedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.file = (string)info.GetValue("file", typeof(string));
		}

		// Token: 0x0600AE76 RID: 44662 RVA: 0x00292FE2 File Offset: 0x002911E2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("file", this.file);
		}

		// Token: 0x170037D0 RID: 14288
		// (get) Token: 0x0600AE77 RID: 44663 RVA: 0x00292FFD File Offset: 0x002911FD
		public string File
		{
			get
			{
				return this.file;
			}
		}

		// Token: 0x04006136 RID: 24886
		private readonly string file;
	}
}
