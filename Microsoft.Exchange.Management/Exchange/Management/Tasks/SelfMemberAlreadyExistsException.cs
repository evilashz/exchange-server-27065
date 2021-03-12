using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E51 RID: 3665
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SelfMemberAlreadyExistsException : RecipientTaskException
	{
		// Token: 0x0600A68F RID: 42639 RVA: 0x002879D9 File Offset: 0x00285BD9
		public SelfMemberAlreadyExistsException(string group) : base(Strings.SelfMemberAlreadyExistsException(group))
		{
			this.group = group;
		}

		// Token: 0x0600A690 RID: 42640 RVA: 0x002879EE File Offset: 0x00285BEE
		public SelfMemberAlreadyExistsException(string group, Exception innerException) : base(Strings.SelfMemberAlreadyExistsException(group), innerException)
		{
			this.group = group;
		}

		// Token: 0x0600A691 RID: 42641 RVA: 0x00287A04 File Offset: 0x00285C04
		protected SelfMemberAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.group = (string)info.GetValue("group", typeof(string));
		}

		// Token: 0x0600A692 RID: 42642 RVA: 0x00287A2E File Offset: 0x00285C2E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("group", this.group);
		}

		// Token: 0x17003668 RID: 13928
		// (get) Token: 0x0600A693 RID: 42643 RVA: 0x00287A49 File Offset: 0x00285C49
		public string Group
		{
			get
			{
				return this.group;
			}
		}

		// Token: 0x04005FCE RID: 24526
		private readonly string group;
	}
}
