using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000F3E RID: 3902
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxSearchNameIsNotUniqueException : LocalizedException
	{
		// Token: 0x0600AB30 RID: 43824 RVA: 0x0028EB28 File Offset: 0x0028CD28
		public MailboxSearchNameIsNotUniqueException(string name) : base(Strings.MailboxSearchNameIsNotUnique(name))
		{
			this.name = name;
		}

		// Token: 0x0600AB31 RID: 43825 RVA: 0x0028EB3D File Offset: 0x0028CD3D
		public MailboxSearchNameIsNotUniqueException(string name, Exception innerException) : base(Strings.MailboxSearchNameIsNotUnique(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600AB32 RID: 43826 RVA: 0x0028EB53 File Offset: 0x0028CD53
		protected MailboxSearchNameIsNotUniqueException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600AB33 RID: 43827 RVA: 0x0028EB7D File Offset: 0x0028CD7D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x17003755 RID: 14165
		// (get) Token: 0x0600AB34 RID: 43828 RVA: 0x0028EB98 File Offset: 0x0028CD98
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040060BB RID: 24763
		private readonly string name;
	}
}
