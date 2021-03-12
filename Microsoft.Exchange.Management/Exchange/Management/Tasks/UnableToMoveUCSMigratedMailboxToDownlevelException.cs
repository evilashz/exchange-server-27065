using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E99 RID: 3737
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UnableToMoveUCSMigratedMailboxToDownlevelException : RecipientTaskException
	{
		// Token: 0x0600A7D8 RID: 42968 RVA: 0x00289229 File Offset: 0x00287429
		public UnableToMoveUCSMigratedMailboxToDownlevelException(string name) : base(Strings.UnableToMoveUCSMigratedMailboxToDownlevelError(name))
		{
			this.name = name;
		}

		// Token: 0x0600A7D9 RID: 42969 RVA: 0x0028923E File Offset: 0x0028743E
		public UnableToMoveUCSMigratedMailboxToDownlevelException(string name, Exception innerException) : base(Strings.UnableToMoveUCSMigratedMailboxToDownlevelError(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600A7DA RID: 42970 RVA: 0x00289254 File Offset: 0x00287454
		protected UnableToMoveUCSMigratedMailboxToDownlevelException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600A7DB RID: 42971 RVA: 0x0028927E File Offset: 0x0028747E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x17003691 RID: 13969
		// (get) Token: 0x0600A7DC RID: 42972 RVA: 0x00289299 File Offset: 0x00287499
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04005FF7 RID: 24567
		private readonly string name;
	}
}
