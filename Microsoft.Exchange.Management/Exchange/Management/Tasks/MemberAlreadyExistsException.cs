using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E4F RID: 3663
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MemberAlreadyExistsException : RecipientTaskException
	{
		// Token: 0x0600A683 RID: 42627 RVA: 0x00287840 File Offset: 0x00285A40
		public MemberAlreadyExistsException(string id, string group) : base(Strings.MemberAlreadyExistsException(id, group))
		{
			this.id = id;
			this.group = group;
		}

		// Token: 0x0600A684 RID: 42628 RVA: 0x0028785D File Offset: 0x00285A5D
		public MemberAlreadyExistsException(string id, string group, Exception innerException) : base(Strings.MemberAlreadyExistsException(id, group), innerException)
		{
			this.id = id;
			this.group = group;
		}

		// Token: 0x0600A685 RID: 42629 RVA: 0x0028787C File Offset: 0x00285A7C
		protected MemberAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.id = (string)info.GetValue("id", typeof(string));
			this.group = (string)info.GetValue("group", typeof(string));
		}

		// Token: 0x0600A686 RID: 42630 RVA: 0x002878D1 File Offset: 0x00285AD1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("id", this.id);
			info.AddValue("group", this.group);
		}

		// Token: 0x17003664 RID: 13924
		// (get) Token: 0x0600A687 RID: 42631 RVA: 0x002878FD File Offset: 0x00285AFD
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17003665 RID: 13925
		// (get) Token: 0x0600A688 RID: 42632 RVA: 0x00287905 File Offset: 0x00285B05
		public string Group
		{
			get
			{
				return this.group;
			}
		}

		// Token: 0x04005FCA RID: 24522
		private readonly string id;

		// Token: 0x04005FCB RID: 24523
		private readonly string group;
	}
}
