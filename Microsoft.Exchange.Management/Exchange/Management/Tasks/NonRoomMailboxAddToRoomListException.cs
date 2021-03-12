using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E53 RID: 3667
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NonRoomMailboxAddToRoomListException : RecipientTaskException
	{
		// Token: 0x0600A699 RID: 42649 RVA: 0x00287AC9 File Offset: 0x00285CC9
		public NonRoomMailboxAddToRoomListException(string group) : base(Strings.NonRoomMailboxAddToRoomListException(group))
		{
			this.group = group;
		}

		// Token: 0x0600A69A RID: 42650 RVA: 0x00287ADE File Offset: 0x00285CDE
		public NonRoomMailboxAddToRoomListException(string group, Exception innerException) : base(Strings.NonRoomMailboxAddToRoomListException(group), innerException)
		{
			this.group = group;
		}

		// Token: 0x0600A69B RID: 42651 RVA: 0x00287AF4 File Offset: 0x00285CF4
		protected NonRoomMailboxAddToRoomListException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.group = (string)info.GetValue("group", typeof(string));
		}

		// Token: 0x0600A69C RID: 42652 RVA: 0x00287B1E File Offset: 0x00285D1E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("group", this.group);
		}

		// Token: 0x1700366A RID: 13930
		// (get) Token: 0x0600A69D RID: 42653 RVA: 0x00287B39 File Offset: 0x00285D39
		public string Group
		{
			get
			{
				return this.group;
			}
		}

		// Token: 0x04005FD0 RID: 24528
		private readonly string group;
	}
}
