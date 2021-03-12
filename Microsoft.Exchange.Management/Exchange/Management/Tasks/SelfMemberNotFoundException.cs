using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E52 RID: 3666
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SelfMemberNotFoundException : RecipientTaskException
	{
		// Token: 0x0600A694 RID: 42644 RVA: 0x00287A51 File Offset: 0x00285C51
		public SelfMemberNotFoundException(string group) : base(Strings.SelfMemberNotFoundException(group))
		{
			this.group = group;
		}

		// Token: 0x0600A695 RID: 42645 RVA: 0x00287A66 File Offset: 0x00285C66
		public SelfMemberNotFoundException(string group, Exception innerException) : base(Strings.SelfMemberNotFoundException(group), innerException)
		{
			this.group = group;
		}

		// Token: 0x0600A696 RID: 42646 RVA: 0x00287A7C File Offset: 0x00285C7C
		protected SelfMemberNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.group = (string)info.GetValue("group", typeof(string));
		}

		// Token: 0x0600A697 RID: 42647 RVA: 0x00287AA6 File Offset: 0x00285CA6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("group", this.group);
		}

		// Token: 0x17003669 RID: 13929
		// (get) Token: 0x0600A698 RID: 42648 RVA: 0x00287AC1 File Offset: 0x00285CC1
		public string Group
		{
			get
			{
				return this.group;
			}
		}

		// Token: 0x04005FCF RID: 24527
		private readonly string group;
	}
}
