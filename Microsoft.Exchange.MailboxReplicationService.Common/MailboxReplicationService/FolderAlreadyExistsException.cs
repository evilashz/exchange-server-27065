using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000368 RID: 872
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FolderAlreadyExistsException : MailboxReplicationPermanentException
	{
		// Token: 0x060026C5 RID: 9925 RVA: 0x00053A42 File Offset: 0x00051C42
		public FolderAlreadyExistsException(string name) : base(MrsStrings.FolderAlreadyExists(name))
		{
			this.name = name;
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x00053A57 File Offset: 0x00051C57
		public FolderAlreadyExistsException(string name, Exception innerException) : base(MrsStrings.FolderAlreadyExists(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x00053A6D File Offset: 0x00051C6D
		protected FolderAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x00053A97 File Offset: 0x00051C97
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x060026C9 RID: 9929 RVA: 0x00053AB2 File Offset: 0x00051CB2
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0400106E RID: 4206
		private readonly string name;
	}
}
