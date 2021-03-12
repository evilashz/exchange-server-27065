using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E50 RID: 3664
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MemberNotFoundException : RecipientTaskException
	{
		// Token: 0x0600A689 RID: 42633 RVA: 0x0028790D File Offset: 0x00285B0D
		public MemberNotFoundException(string id, string group) : base(Strings.MemberNotFoundException(id, group))
		{
			this.id = id;
			this.group = group;
		}

		// Token: 0x0600A68A RID: 42634 RVA: 0x0028792A File Offset: 0x00285B2A
		public MemberNotFoundException(string id, string group, Exception innerException) : base(Strings.MemberNotFoundException(id, group), innerException)
		{
			this.id = id;
			this.group = group;
		}

		// Token: 0x0600A68B RID: 42635 RVA: 0x00287948 File Offset: 0x00285B48
		protected MemberNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.id = (string)info.GetValue("id", typeof(string));
			this.group = (string)info.GetValue("group", typeof(string));
		}

		// Token: 0x0600A68C RID: 42636 RVA: 0x0028799D File Offset: 0x00285B9D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("id", this.id);
			info.AddValue("group", this.group);
		}

		// Token: 0x17003666 RID: 13926
		// (get) Token: 0x0600A68D RID: 42637 RVA: 0x002879C9 File Offset: 0x00285BC9
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17003667 RID: 13927
		// (get) Token: 0x0600A68E RID: 42638 RVA: 0x002879D1 File Offset: 0x00285BD1
		public string Group
		{
			get
			{
				return this.group;
			}
		}

		// Token: 0x04005FCC RID: 24524
		private readonly string id;

		// Token: 0x04005FCD RID: 24525
		private readonly string group;
	}
}
